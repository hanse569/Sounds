namespace Sounds
{
    partial class PropertiesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PropertiesForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.picturesTab = new System.Windows.Forms.TabPage();
            this.albumArtSelector = new System.Windows.Forms.ComboBox();
            this.albumArtBox = new System.Windows.Forms.PictureBox();
            this.fileSelector = new System.Windows.Forms.ComboBox();
            this.tabControl1.SuspendLayout();
            this.picturesTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.albumArtBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.picturesTab);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // picturesTab
            // 
            this.picturesTab.Controls.Add(this.fileSelector);
            this.picturesTab.Controls.Add(this.albumArtSelector);
            this.picturesTab.Controls.Add(this.albumArtBox);
            resources.ApplyResources(this.picturesTab, "picturesTab");
            this.picturesTab.Name = "picturesTab";
            this.picturesTab.UseVisualStyleBackColor = true;
            // 
            // albumArtSelector
            // 
            this.albumArtSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.albumArtSelector.FormattingEnabled = true;
            resources.ApplyResources(this.albumArtSelector, "albumArtSelector");
            this.albumArtSelector.Name = "albumArtSelector";
            this.albumArtSelector.SelectedIndexChanged += new System.EventHandler(this.albumArtSelector_SelectedIndexChanged);
            // 
            // albumArtBox
            // 
            resources.ApplyResources(this.albumArtBox, "albumArtBox");
            this.albumArtBox.Name = "albumArtBox";
            this.albumArtBox.TabStop = false;
            // 
            // fileSelector
            // 
            this.fileSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fileSelector.FormattingEnabled = true;
            resources.ApplyResources(this.fileSelector, "fileSelector");
            this.fileSelector.Name = "fileSelector";
            this.fileSelector.SelectedIndexChanged += new System.EventHandler(this.fileSelector_SelectedIndexChanged);
            // 
            // PropertiesForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PropertiesForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.tabControl1.ResumeLayout(false);
            this.picturesTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.albumArtBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage picturesTab;
        private System.Windows.Forms.PictureBox albumArtBox;
        private System.Windows.Forms.ComboBox albumArtSelector;
        private System.Windows.Forms.ComboBox fileSelector;
    }
}