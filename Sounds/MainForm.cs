using Microsoft.WindowsAPICodePack.Taskbar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace Sounds
{
    public partial class MainForm : Form
    {
        const string ClipboardType = "SoundsItem";

        // some take Icons, not Bitmaps
        Icon stopIcon = Icon.FromHandle(Properties.Resources.Stop.GetHicon());
        Icon playIcon = Icon.FromHandle(Properties.Resources.Play.GetHicon());
        Icon prevIcon = Icon.FromHandle(Properties.Resources.Previous.GetHicon());
        Icon nextIcon = Icon.FromHandle(Properties.Resources.Next.GetHicon());
        Icon pauseIcon = Icon.FromHandle(Properties.Resources.Pause.GetHicon());

        MediaPlayer mp = new MediaPlayer();
        TagLib.File activeFile = null;
        // not if the MediaPlayer is, but if we should at all
        bool playing = false;
        double vol; // we need to keep this ourselves; mp.Stop resets mp.Volume

        // setings
        int volIncrement = 5; // for trackbar/keyboard
        int timeIncrement = 15;
        bool repeat = false;
        bool deleteOnNext = false;

        string playlistFile = null;
        bool dirty = false;

        bool Paused
        {
            get
            {
                // HACK HACK
                try
                {
                    var mps = mp.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance).Where(x => x.Name == "_mediaPlayerState").First().GetValue(mp);
                    var paused = (bool)mps.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance).Where(x => x.Name == "_paused").First().GetValue(mps);
                    return paused;
                }
                catch (Exception)
                {
                    // god knows what could have gone wrong
                    return false;
                }
            }
        }

        double Volume
        {
            get
            {
                return vol;
            }
            set
            {
                // clamp value
                vol = Math.Min(Math.Max(value, 0), 1);
                mp.Volume = vol;
                var simplePercent = new CultureInfo(CultureInfo.InvariantCulture.Name);
                simplePercent.NumberFormat.PercentDecimalDigits = 0;
                simplePercent.NumberFormat.PercentPositivePattern = 1;
                volumeButton.Text = string.Format(simplePercent, "{0:P}", mp.Volume);
                volumeStatusButton.Text = string.Format(simplePercent, "{0:P}", mp.Volume);
            }
        }

        int VolumeIncrement
        {
            get { return volIncrement; }
            set
            {
                volIncrement = value;
            }
        }

        int TimeIncrement
        {
            get { return timeIncrement; }
            set
            {
                timeIncrement = value;
                positionTrackBar.LargeChange = TimeIncrement;
            }
        }

        public MainForm()
        {
            InitializeComponent();

            // a new dropdown w/ a system rendering looks more like a panel
            var dd = new ToolStripDropDown();
            dd.RenderMode = ToolStripRenderMode.System;
            volumeButton.DropDown = dd;
            volumeStatusButton.DropDown = dd;

            mp.MediaEnded += (o, e) =>
            {
                // avoid race coondition
                if (!repeat)
                    trackBarSyncTimer.Enabled = false;
                if (playing)
                    Next();
            };
            mp.MediaFailed += (o, e) =>
            {
                Stop();
                errorMessageLabel.Text = e.ErrorException.Message;
            };
            mp.MediaOpened += (o, e) =>
            {
                if (mp.NaturalDuration.HasTimeSpan)
                {
                    positionTrackBar.Maximum = Convert.ToInt32(mp.NaturalDuration.TimeSpan.TotalSeconds);
                    positionTrackBar.Enabled = true;
                    positionTrackBar.Visible = true;
                    trackBarSyncTimer.Enabled = true;
                }
                else
                {
                    trackBarSyncTimer.Enabled = false;
                    positionTrackBar.Enabled = false;
                    positionTrackBar.Visible = false;
                }
            };

            // finally init UI by creating PL (args will override it)
            //NewPlaylist();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            // we need to do it when the form is visible so taskbar updates work
            // do initial init of menubar and such
            UpdateUI();
            UpdateTitle();
            // mp's value is, but not the UI bits; do this to update them
            Volume = 0.50;
        }

        public bool AddFile(string fileName, bool update = true)
        {
            // HACK: ignore Mac droppings that confuse TagLib when we get
            // aggressive with adding directories. (consider making a pref?)
            if (Path.GetFileName(fileName).StartsWith("._"))
                return false;

            var doAdd = false;
            try
            {
                var f = TagLib.File.Create(fileName);
                if (f.Properties?.MediaTypes != TagLib.MediaTypes.Audio)
                {
                    // we don't want it
                    return false;
                }
                var lvi = new ListViewItem();
                // fall back to filename
                lvi.Text = f.Tag.Title ?? f.Name;
                var trackNumber = f.Tag.TrackCount > 0 ?
                    string.Format("{0}/{1}", f.Tag.Track, f.Tag.TrackCount)
                    : f.Tag.Track.ToString();
                if (f.Tag.Disc > 0)
                    trackNumber += f.Tag.DiscCount > 0 ?
                        string.Format(" ({0}/{1})", f.Tag.Disc, f.Tag.DiscCount)
                        : string.Format(" ({0})" ,f.Tag.Disc.ToString());
                lvi.SubItems.Add(trackNumber);
                lvi.SubItems.Add(f.Tag.Album);
                lvi.SubItems.Add(f.Tag.Performers.Count() > 0 ? string.Join(", ", f.Tag.Performers) : string.Empty);
                lvi.UseItemStyleForSubItems = false;
                lvi.ToolTipText = f.Name;
                lvi.Tag = f;
                listView1.Items.Add(lvi);
                doAdd = true;
            }
            catch (TagLib.UnsupportedFormatException)
            {
                // not needed
            }
            catch (TagLib.CorruptFileException)
            {
                // should warn the user
            }
            finally
            {
                if (doAdd && update)
                {
                    Dirty = true; // will get unset by Open if so
                }
            }
            return doAdd;
        }

        public bool AddDirectory(string name, bool update = true)
        {
            var didAdd = false;
            foreach (var f in Directory.EnumerateFiles(name).OrderBy(x => x))
            {
                didAdd = AddFile(f, false) || didAdd;
            }
            if (didAdd && update)
            {
                Dirty = true;
            }
            return didAdd;
        }

        void DeleteOnChange(TagLib.File old)
        {
            if (deleteOnNext && old != null)
            {
                listView1.Items.Cast<ListViewItem>().Where(x => x.Tag == old).First().Remove();
                Dirty = listView1.Items.Count > 0 && playlistFile != null;
            }
        }

        public void PlayAndSet(bool playSelected)
        {
            if (!playSelected && Paused)
            {
                mp.Play();
            }
            else
            {
                var oldActiveFile = activeFile;
                DeleteOnChange(oldActiveFile);

                if (listView1.SelectedItems.Count > 0)
                {
                    activeFile = (TagLib.File)listView1.SelectedItems[0].Tag;
                }
                else if (listView1.Items.Count > 0)
                {
                    activeFile = (TagLib.File)listView1.Items[0].Tag;
                }
                else return;

                playing = true;
                PlayActive();
            }
        }

        /// <summary>
        /// Plays the song set as the active track.
        /// </summary>
        /// <remarks>
        /// It's the caller's responsibility to set the active track.
        /// </remarks>
        public void PlayActive()
        {
#pragma warning disable CS0618
            // HACK: It's deprecated, but MediaPlayer doesn't like escaped URIs
            var u = new Uri(activeFile.Name, true);
#pragma warning restore CS0618
            mp.Open(u);
            mp.Volume = vol; // as Stop might have reset it
            mp.Play();
            UpdateTitle();
            UpdateUI();
        }

        public void Pause()
        {
            mp.Pause();
        }

        public void UpdateTitle()
        {
            var fileNameTitle = string.IsNullOrEmpty(playlistFile) ?
                MiscStrings.untitledPlaylist : Path.GetFileName(playlistFile);
            // Recommended if dirty if turned into a property?
            var fileNameFinalTitle = string.Format("{0}{1}",
                fileNameTitle, Dirty ? "*" : "");
            if (activeFile != null)
            {
                var title = activeFile.Tag.Title;
                var album = activeFile.Tag.Album;
                var artist = activeFile.Tag.Performers.Count() > 0 ?
                    string.Join(", ", activeFile.Tag.Performers) : string.Empty;
                
                if (title != null && album != null)
                    Text = string.Format("{0} - {1} [{2}]",
                        title, artist, fileNameFinalTitle);
                else
                    Text = string.Format("{0} [{1}]",
                        activeFile.Name, fileNameFinalTitle);
            }
            else
            {
                // nop it out
                Text = fileNameFinalTitle;
            }
        }

        // Metadata and such
        // TODO: needs optimization. profiler says we're churning, but
        // album art is fine. My guess is emboldening. [album art change
        // moved to Title.]
        public void UpdateUI()
        {
            if (activeFile != null)
            {
                var title = activeFile.Tag.Title;
                var album = activeFile.Tag.Album;
                var artist = activeFile.Tag.Performers.Count() > 0 ?
                    string.Join(", ", activeFile.Tag.Performers) : string.Empty;

                titleLabel.Text = title ?? activeFile.Name;
                albumLabel.Text = album;
                artistLabel.Text = artist;

                albumArtBox.Image = AlbumArt;

                panel1.Visible = true;

                // embolden the active song
                var newBoldItem = listView1.Items.Cast<ListViewItem>().FirstOrDefault(x => x.Tag == activeFile);
                if (newBoldItem != null)
                    newBoldItem.Font = new Font(listView1.Font, FontStyle.Bold);
            }
            else
            {
                // nop it out
                titleLabel.Text = string.Empty;
                albumLabel.Text = string.Empty;
                artistLabel.Text = string.Empty;

                positionLabel.Text = string.Empty;
                positionTrackBar.Enabled = false;
                positionTrackBar.Visible = false;

                albumArtBox.Image = null;

                panel1.Visible = false;

            }

            // we can run this regardless
            errorMessageLabel.Text = string.Empty;
            foreach (var lvi in listView1.Items.Cast<ListViewItem>().Where(x => x.Tag != activeFile))
            {
                lvi.Font = listView1.Font;
            }
        }

        public Bitmap AlbumArt
        {
            get
            {
                // generating an image can be complicated
                var pictureStream = activeFile.Tag.Pictures.Where(x => x.Type == TagLib.PictureType.FrontCover).FirstOrDefault()?.Data?.Data;
                if (pictureStream != null)
                {
                    using (var ms = new MemoryStream(pictureStream))
                    {
                        var b = new Bitmap(Image.FromStream(ms));
                        return b;
                    }
                }
                else
                {
                    // reasonable fallback
                    if (activeFile != null)
                    {
                        var containing = Path.GetDirectoryName(activeFile.Name);
                        // TODO, add more of these, and make them more scalable
                        // also figure out how to integrate this into the properties dialog
                        if (File.Exists(Path.Combine(containing, "cover.jpg")))
                        {
                            return new Bitmap(Path.Combine(containing, "cover.jpg"));
                        }
                        else if (File.Exists(Path.Combine(containing, "front.jpg")))
                        {
                            return new Bitmap(Path.Combine(containing, "front.jpg"));
                        }
                        else if (File.Exists(Path.Combine(containing, "folder.jpg")))
                        {
                            return new Bitmap(Path.Combine(containing, "folder.jpg"));
                        }
                    }
                    return new Bitmap(100, 100);
                }
            }
        }

        public bool Dirty
        {
            get
            {
                return dirty;
            }

            set
            {
                dirty = value;
                UpdateTitle();
            }
        }

        public void Stop()
        {
            trackBarSyncTimer.Enabled = false;
            mp.Stop();
            mp.Close();
            playing = false;
            activeFile = null;

            UpdateTitle();
            UpdateUI();
        }

        public void Previous()
        {
            var oldActiveFile = activeFile;
            activeFile = (TagLib.File)listView1.Items.Cast<ListViewItem>().TakeWhile(x => x.Tag != activeFile).LastOrDefault()?.Tag;
            DeleteOnChange(oldActiveFile);

            if (activeFile != null && playing)
            {
                PlayActive();
            }
            else
            {
                Stop();
            }
        }

        public void Next()
        {
            var oldActiveFile = activeFile;
            activeFile = (TagLib.File)listView1.Items.Cast<ListViewItem>().SkipWhile(x => x.Tag != activeFile).Skip(1).FirstOrDefault()?.Tag;
            DeleteOnChange(oldActiveFile);

            if (activeFile != null && playing)
            {
                PlayActive();
            }
            else if (playing && repeat)
            {
                activeFile = (TagLib.File)listView1.Items.Cast<ListViewItem>().FirstOrDefault()?.Tag;
                if (activeFile != null)
                {
                    PlayActive();
                }
                else
                {
                    Stop();
                }
            }
            else
            {
                Stop();
            }
        }

        public void ShowPropertiesDialog(bool forcePlayingSong)
        {
            if (!forcePlayingSong && listView1.SelectedItems.Count > 0)
            {
                new PropertiesForm(listView1.SelectedItems.Cast<ListViewItem>().Select(x => (TagLib.File)x.Tag).FirstOrDefault(),
                    listView1.Items.Cast<ListViewItem>().Select(x => (TagLib.File)x.Tag).ToArray()).Show(this);
            }
            else if (playing)
            {
                new PropertiesForm(activeFile, listView1.Items.Cast<ListViewItem>().Select(x => (TagLib.File)x.Tag).ToArray()).Show(this);
            }
        }

        private void playToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlayAndSet(false);
        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pause();
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // the Stop function is called by Next and such which will call
            // delete themselves. if the user manually stops, do deletion here.
            DeleteOnChange(activeFile);
            Stop();
        }

        private void nextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Next();
        }

        private void trackBarSyncTimer_Tick(object sender, EventArgs e)
        {
            var value = Convert.ToInt32(mp.Position.TotalSeconds);
            // only update the trackbar if value =< max, to avoid races
            if (positionTrackBar.Maximum >= value)
            {
                positionTrackBar.Value = value;
            }

            if (mp.NaturalDuration.HasTimeSpan)
            {
                positionLabel.Text = string.Format("{0} / {1}",
                    mp.Position, mp.NaturalDuration.TimeSpan);
            }
            else
            {
                positionLabel.Text = string.Format("{0}",
                    mp.Position);
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            // this is only invoked when the user scrolls
            mp.Position = new TimeSpan(0, 0, 0, positionTrackBar.Value);
        }

        private void previousToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Previous();
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPropertiesDialog(false);
        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            PlayAndSet(true);
        }

        private void listView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void listView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else if (e.AllowedEffect == DragDropEffects.Move)
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void listView1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Effect == DragDropEffects.Copy)
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    var data = (string[])e.Data.GetData(DataFormats.FileDrop);
                    foreach (var f in data.OrderBy(x => x))
                    {
                        if (Directory.Exists(f))
                        {
                            AddDirectory(f);
                        }
                        else if(File.Exists(f))
                        {
                            AddFile(f);
                        }
                    }
                }
            }
            else if (e.Effect == DragDropEffects.Move)
            {
                var cp = listView1.PointToClient(new Point(e.X, e.Y));
                ListViewItem dragToItem = listView1.GetItemAt(cp.X, cp.Y);
                if (!listView1.SelectedItems.Contains(dragToItem))
                {
                    var selectedItems = listView1.SelectedItems.Cast<ListViewItem>().ToList();

                    var dropIndex = dragToItem?.Index ?? listView1.Items.Count;

                    foreach (var i in selectedItems)
                        listView1.Items.Insert(dropIndex++, (ListViewItem)i.Clone());

                    foreach (var i in selectedItems)
                        i.Remove();

                    Dirty = true;
                }
            }
        }

        private void albumArtBox_Click(object sender, EventArgs e)
        {
            ShowPropertiesDialog(true);
        }

        private void playContextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // the menu/toolbar item will play the same track when paused; this
            // forcibly plays the selected track
            PlayAndSet(true);
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem i in listView1.Items)
                i.Selected = true;
        }

        public void CopySelected(bool updateMenus = true)
        {
            // TODO: we parse this later using existing code because easier
            // but we could instead just pass TagLib.File to them directly
            // (if those even serialize?)
            var items = listView1.SelectedItems.Cast<ListViewItem>()
                .Select(x => ((TagLib.File)x.Tag).Name);
            Clipboard.SetData(ClipboardType, items.ToList());
            // to update paste; cut will call delete which does this for us
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopySelected();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsData(ClipboardType))
            {
                var items = (List<string>)Clipboard.GetData(ClipboardType);
                if (items != null && items.Count > 0)
                {
                    var shouldUpdate = false;
                    foreach (var i in items)
                    {
                        shouldUpdate = AddFile(i, false) || shouldUpdate;
                    }
                    if (shouldUpdate)
                    {
                        Dirty = true;
                    }
                }
            }
        }

        private void listView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var item = listView1.GetItemAt(e.X, e.Y);
                if (item != null)
                {
                    // focused on an item
                    playlistContextMenu.Show(listView1, e.Location);
                }
                // we could also test for headers?
                else
                {
                    // not focused on an item
                    playlistUnselectedContextMenu.Show(listView1, e.Location);
                }
            }
        }
    }
}
