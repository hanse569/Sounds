namespace Sounds
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.positionLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.errorMessageLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.volumeStatusButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.listView1 = new System.Windows.Forms.ListView();
            this.titleHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.trackNoHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.albumHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.artistHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.artistLabel = new System.Windows.Forms.Label();
            this.albumLabel = new System.Windows.Forms.Label();
            this.titleLabel = new System.Windows.Forms.Label();
            this.positionTrackBar = new System.Windows.Forms.TrackBar();
            this.albumArtBox = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.playToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.pauseToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.stopToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.previousToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.nextToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.volumeButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.playlistContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.playContextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertiesContextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.cutContextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyContextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeContextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addFilesDialog = new System.Windows.Forms.OpenFileDialog();
            this.trackBarSyncTimer = new System.Windows.Forms.Timer(this.components);
            this.openPlaylistDialog = new System.Windows.Forms.OpenFileDialog();
            this.savePlaylistDialog = new System.Windows.Forms.SaveFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.playlistUnselectedContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pasteContextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.selectAllContextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.positionTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.albumArtBox)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.playlistContextMenu.SuspendLayout();
            this.playlistUnselectedContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStrip1);
            // 
            // toolStripContainer1.ContentPanel
            // 
            resources.ApplyResources(this.toolStripContainer1.ContentPanel, "toolStripContainer1.ContentPanel");
            this.toolStripContainer1.ContentPanel.Controls.Add(this.listView1);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.panel1);
            resources.ApplyResources(this.toolStripContainer1, "toolStripContainer1");
            this.toolStripContainer1.Name = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // statusStrip1
            // 
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.positionLabel,
            this.errorMessageLabel,
            this.volumeStatusButton});
            this.statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStrip1.Name = "statusStrip1";
            // 
            // positionLabel
            // 
            this.positionLabel.Name = "positionLabel";
            resources.ApplyResources(this.positionLabel, "positionLabel");
            // 
            // errorMessageLabel
            // 
            this.errorMessageLabel.Name = "errorMessageLabel";
            resources.ApplyResources(this.errorMessageLabel, "errorMessageLabel");
            // 
            // volumeStatusButton
            // 
            this.volumeStatusButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            resources.ApplyResources(this.volumeStatusButton, "volumeStatusButton");
            this.volumeStatusButton.Image = global::Sounds.Properties.Resources.Volume;
            this.volumeStatusButton.Name = "volumeStatusButton";
            // 
            // listView1
            // 
            this.listView1.AllowDrop = true;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.titleHeader,
            this.trackNoHeader,
            this.albumHeader,
            this.artistHeader});
            resources.ApplyResources(this.listView1, "listView1");
            this.listView1.Name = "listView1";
            this.listView1.ShowItemToolTips = true;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ItemActivate += new System.EventHandler(this.listView1_ItemActivate);
            this.listView1.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.listView1_ItemDrag);
            this.listView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.listView1_DragDrop);
            this.listView1.DragEnter += new System.Windows.Forms.DragEventHandler(this.listView1_DragEnter);
            this.listView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseUp);
            // 
            // titleHeader
            // 
            resources.ApplyResources(this.titleHeader, "titleHeader");
            // 
            // trackNoHeader
            // 
            resources.ApplyResources(this.trackNoHeader, "trackNoHeader");
            // 
            // albumHeader
            // 
            resources.ApplyResources(this.albumHeader, "albumHeader");
            // 
            // artistHeader
            // 
            resources.ApplyResources(this.artistHeader, "artistHeader");
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.artistLabel);
            this.panel1.Controls.Add(this.albumLabel);
            this.panel1.Controls.Add(this.titleLabel);
            this.panel1.Controls.Add(this.positionTrackBar);
            this.panel1.Controls.Add(this.albumArtBox);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // artistLabel
            // 
            resources.ApplyResources(this.artistLabel, "artistLabel");
            this.artistLabel.AutoEllipsis = true;
            this.artistLabel.Name = "artistLabel";
            this.artistLabel.UseMnemonic = false;
            // 
            // albumLabel
            // 
            resources.ApplyResources(this.albumLabel, "albumLabel");
            this.albumLabel.AutoEllipsis = true;
            this.albumLabel.Name = "albumLabel";
            this.albumLabel.UseMnemonic = false;
            // 
            // titleLabel
            // 
            resources.ApplyResources(this.titleLabel, "titleLabel");
            this.titleLabel.AutoEllipsis = true;
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.UseMnemonic = false;
            // 
            // positionTrackBar
            // 
            resources.ApplyResources(this.positionTrackBar, "positionTrackBar");
            this.positionTrackBar.LargeChange = 15;
            this.positionTrackBar.Name = "positionTrackBar";
            this.positionTrackBar.TickFrequency = 60;
            this.positionTrackBar.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // albumArtBox
            // 
            resources.ApplyResources(this.albumArtBox, "albumArtBox");
            this.albumArtBox.Name = "albumArtBox";
            this.albumArtBox.TabStop = false;
            this.albumArtBox.Click += new System.EventHandler(this.albumArtBox_Click);
            // 
            // menuStrip1
            // 
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewToolStripMenuItem});
            this.menuStrip1.Name = "menuStrip1";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.propertiesToolStripMenuItem,
            this.toolStripSeparator3});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            resources.ApplyResources(this.viewToolStripMenuItem, "viewToolStripMenuItem");
            // 
            // propertiesToolStripMenuItem
            // 
            this.propertiesToolStripMenuItem.Image = global::Sounds.Properties.Resources.Properties;
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            resources.ApplyResources(this.propertiesToolStripMenuItem, "propertiesToolStripMenuItem");
            this.propertiesToolStripMenuItem.Click += new System.EventHandler(this.propertiesToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // toolStrip1
            // 
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playToolStripButton,
            this.pauseToolStripButton,
            this.stopToolStripButton,
            this.toolStripSeparator6,
            this.previousToolStripButton,
            this.nextToolStripButton,
            this.toolStripSeparator7,
            this.volumeButton});
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Stretch = true;
            // 
            // playToolStripButton
            // 
            this.playToolStripButton.Image = global::Sounds.Properties.Resources.Play;
            resources.ApplyResources(this.playToolStripButton, "playToolStripButton");
            this.playToolStripButton.Name = "playToolStripButton";
            this.playToolStripButton.Click += new System.EventHandler(this.playToolStripMenuItem_Click);
            // 
            // pauseToolStripButton
            // 
            this.pauseToolStripButton.Image = global::Sounds.Properties.Resources.Pause;
            resources.ApplyResources(this.pauseToolStripButton, "pauseToolStripButton");
            this.pauseToolStripButton.Name = "pauseToolStripButton";
            this.pauseToolStripButton.Click += new System.EventHandler(this.pauseToolStripMenuItem_Click);
            // 
            // stopToolStripButton
            // 
            this.stopToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.stopToolStripButton.Image = global::Sounds.Properties.Resources.Stop;
            resources.ApplyResources(this.stopToolStripButton, "stopToolStripButton");
            this.stopToolStripButton.Name = "stopToolStripButton";
            this.stopToolStripButton.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            resources.ApplyResources(this.toolStripSeparator6, "toolStripSeparator6");
            // 
            // previousToolStripButton
            // 
            this.previousToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.previousToolStripButton.Image = global::Sounds.Properties.Resources.Previous;
            resources.ApplyResources(this.previousToolStripButton, "previousToolStripButton");
            this.previousToolStripButton.Name = "previousToolStripButton";
            this.previousToolStripButton.Click += new System.EventHandler(this.previousToolStripMenuItem_Click);
            // 
            // nextToolStripButton
            // 
            this.nextToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.nextToolStripButton.Image = global::Sounds.Properties.Resources.Next;
            resources.ApplyResources(this.nextToolStripButton, "nextToolStripButton");
            this.nextToolStripButton.Name = "nextToolStripButton";
            this.nextToolStripButton.Click += new System.EventHandler(this.nextToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            resources.ApplyResources(this.toolStripSeparator7, "toolStripSeparator7");
            // 
            // volumeButton
            // 
            this.volumeButton.Image = global::Sounds.Properties.Resources.Volume;
            resources.ApplyResources(this.volumeButton, "volumeButton");
            this.volumeButton.Name = "volumeButton";
            // 
            // playlistContextMenu
            // 
            this.playlistContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playContextToolStripMenuItem,
            this.propertiesContextToolStripMenuItem,
            this.toolStripSeparator5,
            this.cutContextToolStripMenuItem,
            this.copyContextToolStripMenuItem,
            this.removeContextToolStripMenuItem});
            this.playlistContextMenu.Name = "playlistContextMenu";
            resources.ApplyResources(this.playlistContextMenu, "playlistContextMenu");
            // 
            // playContextToolStripMenuItem
            // 
            this.playContextToolStripMenuItem.Image = global::Sounds.Properties.Resources.Play;
            this.playContextToolStripMenuItem.Name = "playContextToolStripMenuItem";
            resources.ApplyResources(this.playContextToolStripMenuItem, "playContextToolStripMenuItem");
            this.playContextToolStripMenuItem.Click += new System.EventHandler(this.playContextToolStripMenuItem_Click);
            // 
            // propertiesContextToolStripMenuItem
            // 
            this.propertiesContextToolStripMenuItem.Image = global::Sounds.Properties.Resources.Properties;
            this.propertiesContextToolStripMenuItem.Name = "propertiesContextToolStripMenuItem";
            resources.ApplyResources(this.propertiesContextToolStripMenuItem, "propertiesContextToolStripMenuItem");
            this.propertiesContextToolStripMenuItem.Click += new System.EventHandler(this.propertiesToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            // 
            // cutContextToolStripMenuItem
            // 
            this.cutContextToolStripMenuItem.Image = global::Sounds.Properties.Resources.Cut;
            this.cutContextToolStripMenuItem.Name = "cutContextToolStripMenuItem";
            resources.ApplyResources(this.cutContextToolStripMenuItem, "cutContextToolStripMenuItem");
            // 
            // copyContextToolStripMenuItem
            // 
            this.copyContextToolStripMenuItem.Image = global::Sounds.Properties.Resources.Copy;
            this.copyContextToolStripMenuItem.Name = "copyContextToolStripMenuItem";
            resources.ApplyResources(this.copyContextToolStripMenuItem, "copyContextToolStripMenuItem");
            this.copyContextToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // removeContextToolStripMenuItem
            // 
            this.removeContextToolStripMenuItem.Image = global::Sounds.Properties.Resources.Delete;
            this.removeContextToolStripMenuItem.Name = "removeContextToolStripMenuItem";
            resources.ApplyResources(this.removeContextToolStripMenuItem, "removeContextToolStripMenuItem");
            // 
            // addFilesDialog
            // 
            this.addFilesDialog.Multiselect = true;
            // 
            // trackBarSyncTimer
            // 
            this.trackBarSyncTimer.Tick += new System.EventHandler(this.trackBarSyncTimer_Tick);
            // 
            // openPlaylistDialog
            // 
            this.openPlaylistDialog.DefaultExt = "m3u";
            resources.ApplyResources(this.openPlaylistDialog, "openPlaylistDialog");
            // 
            // savePlaylistDialog
            // 
            this.savePlaylistDialog.DefaultExt = "m3u";
            resources.ApplyResources(this.savePlaylistDialog, "savePlaylistDialog");
            // 
            // folderBrowserDialog1
            // 
            resources.ApplyResources(this.folderBrowserDialog1, "folderBrowserDialog1");
            this.folderBrowserDialog1.ShowNewFolderButton = false;
            // 
            // playlistUnselectedContextMenu
            // 
            this.playlistUnselectedContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pasteContextToolStripMenuItem,
            this.toolStripMenuItem4,
            this.selectAllContextToolStripMenuItem});
            this.playlistUnselectedContextMenu.Name = "playlistUnselectedContextMenu";
            resources.ApplyResources(this.playlistUnselectedContextMenu, "playlistUnselectedContextMenu");
            // 
            // pasteContextToolStripMenuItem
            // 
            this.pasteContextToolStripMenuItem.Image = global::Sounds.Properties.Resources.PasteHS;
            this.pasteContextToolStripMenuItem.Name = "pasteContextToolStripMenuItem";
            resources.ApplyResources(this.pasteContextToolStripMenuItem, "pasteContextToolStripMenuItem");
            this.pasteContextToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            resources.ApplyResources(this.toolStripMenuItem4, "toolStripMenuItem4");
            // 
            // selectAllContextToolStripMenuItem
            // 
            this.selectAllContextToolStripMenuItem.Name = "selectAllContextToolStripMenuItem";
            resources.ApplyResources(this.selectAllContextToolStripMenuItem, "selectAllContextToolStripMenuItem");
            this.selectAllContextToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStripContainer1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.positionTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.albumArtBox)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.playlistContextMenu.ResumeLayout(false);
            this.playlistUnselectedContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog addFilesDialog;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader titleHeader;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TrackBar positionTrackBar;
        private System.Windows.Forms.PictureBox albumArtBox;
        private System.Windows.Forms.ColumnHeader albumHeader;
        private System.Windows.Forms.ColumnHeader artistHeader;
        private System.Windows.Forms.Timer trackBarSyncTimer;
        private System.Windows.Forms.Label artistLabel;
        private System.Windows.Forms.Label albumLabel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel positionLabel;
        private System.Windows.Forms.OpenFileDialog openPlaylistDialog;
        private System.Windows.Forms.SaveFileDialog savePlaylistDialog;
        private System.Windows.Forms.ColumnHeader trackNoHeader;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ToolStripStatusLabel errorMessageLabel;
        private System.Windows.Forms.ContextMenuStrip playlistContextMenu;
        private System.Windows.Forms.ToolStripMenuItem playContextToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem propertiesContextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeContextToolStripMenuItem;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton playToolStripButton;
        private System.Windows.Forms.ToolStripButton pauseToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton previousToolStripButton;
        private System.Windows.Forms.ToolStripButton nextToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripDropDownButton volumeButton;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton volumeStatusButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton stopToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem cutContextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyContextToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip playlistUnselectedContextMenu;
        private System.Windows.Forms.ToolStripMenuItem pasteContextToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem selectAllContextToolStripMenuItem;
    }
}

