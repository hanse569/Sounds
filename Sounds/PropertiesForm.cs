using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sounds
{
    public partial class PropertiesForm : Form
    {
        public PropertiesForm()
        {
            InitializeComponent();
            fileSelector.DisplayMember = "Name";
        }

        public PropertiesForm(TagLib.File select, params TagLib.File[] files) : this()
        {
            fileSelector.Items.AddRange(files);
            fileSelector.SelectedIndex = Array.IndexOf(files, select);

            int largestWidth = 0;
            foreach(var i in files)
            {
                var width = TextRenderer.MeasureText(i.Name, fileSelector.Font).Width;
                largestWidth = Math.Max(width, largestWidth);
            }
            fileSelector.DropDownWidth = largestWidth;
        }

        public void SwitchFile(TagLib.File f)
        {          
            albumArtSelector.DisplayMember = "Type";
            albumArtSelector.Items.Clear();
            if (f.Tag.Pictures.Count() > 0)
            {
                albumArtSelector.Enabled = true;
                albumArtSelector.Items.AddRange(f.Tag.Pictures);
                if (albumArtSelector.Items.Count > 0)
                    albumArtSelector.SelectedIndex = 0;
            }
            else
            {
                albumArtSelector.Enabled = false;
                albumArtBox.Image = null;
            }
        }

        private void albumArtSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picture = (TagLib.IPicture)albumArtSelector.SelectedItem;

            var pictureStream = picture?.Data?.Data;
            if (pictureStream != null)
            {
                using (var ms = new MemoryStream(pictureStream))
                {
                    var b = Image.FromStream(ms);
                    albumArtBox.Image = b;
                }
            }
        }

        private void fileSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            SwitchFile((TagLib.File)fileSelector.SelectedItem);
        }

    }
}
