using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Media;

namespace Sounds
{
    public partial class MainForm : Form
    {
        const string ClipboardType = "SoundsItem";

        MediaPlayer mp = new MediaPlayer();
        TagLib.File activeFile = null;
        // not if the MediaPlayer is, but if we should at all
        bool playing = false;

        // setings
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

            AddDirectory("E:\\hanse\\Video\\HardStyle", true);
            
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            UpdateUI();
            UpdateTitle();
        }

        public bool AddFile(string fileName, bool update = true)
        {
            if (Path.GetFileName(fileName).StartsWith("._"))
                return false;

            var doAdd = false;
            try
            {
                var f = TagLib.File.Create(fileName);
                if (f.Properties?.MediaTypes != TagLib.MediaTypes.Audio)
                {
                    return false;
                }
                var lvi = new ListViewItem();
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
            catch (TagLib.UnsupportedFormatException){}
            catch (TagLib.CorruptFileException){}
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

        public void PlayActive()
        {
#pragma warning disable CS0618
            var u = new Uri(activeFile.Name, true);
#pragma warning restore CS0618
            mp.Open(u);
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
                Text = fileNameFinalTitle;
            }
        }

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

                var newBoldItem = listView1.Items.Cast<ListViewItem>().FirstOrDefault(x => x.Tag == activeFile);
                if (newBoldItem != null)
                    newBoldItem.Font = new Font(listView1.Font, FontStyle.Bold);
            }
            else
            {
                titleLabel.Text = string.Empty;
                albumLabel.Text = string.Empty;
                artistLabel.Text = string.Empty;

                positionLabel.Text = string.Empty;
                positionTrackBar.Enabled = false;
                positionTrackBar.Visible = false;

                albumArtBox.Image = null;

                panel1.Visible = false;

            }
            
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
                    if (activeFile != null)
                    {
                        var containing = Path.GetDirectoryName(activeFile.Name);
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
            mp.Position = new TimeSpan(0, 0, 0, positionTrackBar.Value);
        }

        private void previousToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Previous();
        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            PlayAndSet(true);
        }

        private void albumArtBox_Click(object sender, EventArgs e)
        {
            ShowPropertiesDialog(true);
        }

    }
}
