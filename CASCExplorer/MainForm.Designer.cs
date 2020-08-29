namespace CASCExplorer
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.folderTree = new System.Windows.Forms.TreeView();
            this.iconsList = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.extractToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.filterToolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.filterToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.openStorageToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openStorageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openOnlineStorageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openRecentStorageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeStorageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.localeFlagsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.useLVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmShowPreview = new System.Windows.Forms.ToolStripMenuItem();
            this.openListFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scanFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.analyseUnknownFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extractInstallFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CDNBuildsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extractCASCSystemFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bruteforceNamesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportListfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportFoldersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.analyzeSoundFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addFileDataIDToSoundFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.storageFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.fileList = new CASCExplorer.NoFlickerListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.folderTree);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1402, 728);
            this.splitContainer1.SplitterDistance = 212;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 0;
            // 
            // folderTree
            // 
            this.folderTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.folderTree.HideSelection = false;
            this.folderTree.ImageIndex = 0;
            this.folderTree.ImageList = this.iconsList;
            this.folderTree.ItemHeight = 16;
            this.folderTree.Location = new System.Drawing.Point(0, 0);
            this.folderTree.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.folderTree.Name = "folderTree";
            this.folderTree.SelectedImageIndex = 0;
            this.folderTree.Size = new System.Drawing.Size(212, 728);
            this.folderTree.TabIndex = 0;
            this.folderTree.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeExpand);
            this.folderTree.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeSelect);
            // 
            // iconsList
            // 
            this.iconsList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.iconsList.ImageSize = new System.Drawing.Size(15, 15);
            this.iconsList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.fileList);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.AutoScroll = true;
            this.splitContainer2.Size = new System.Drawing.Size(1184, 728);
            this.splitContainer2.SplitterDistance = 704;
            this.splitContainer2.SplitterWidth = 6;
            this.splitContainer2.TabIndex = 1;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.extractToolStripMenuItem,
            this.copyNameToolStripMenuItem,
            this.getSizeToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(179, 100);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // extractToolStripMenuItem
            // 
            this.extractToolStripMenuItem.Name = "extractToolStripMenuItem";
            this.extractToolStripMenuItem.Size = new System.Drawing.Size(178, 32);
            this.extractToolStripMenuItem.Text = "Extract...";
            this.extractToolStripMenuItem.Click += new System.EventHandler(this.extractToolStripMenuItem_Click);
            // 
            // copyNameToolStripMenuItem
            // 
            this.copyNameToolStripMenuItem.Name = "copyNameToolStripMenuItem";
            this.copyNameToolStripMenuItem.Size = new System.Drawing.Size(178, 32);
            this.copyNameToolStripMenuItem.Text = "Copy Name";
            this.copyNameToolStripMenuItem.Click += new System.EventHandler(this.copyNameToolStripMenuItem_Click);
            // 
            // getSizeToolStripMenuItem
            // 
            this.getSizeToolStripMenuItem.Name = "getSizeToolStripMenuItem";
            this.getSizeToolStripMenuItem.Size = new System.Drawing.Size(178, 32);
            this.getSizeToolStripMenuItem.Text = "Get Size";
            this.getSizeToolStripMenuItem.Click += new System.EventHandler(this.getSizeToolStripMenuItem_Click);
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
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
            this.toolStripContainer1.ContentPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(1402, 728);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(1402, 826);
            this.toolStripContainer1.TabIndex = 3;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip2);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel,
            this.statusProgress});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1402, 32);
            this.statusStrip1.TabIndex = 0;
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(64, 25);
            this.statusLabel.Text = "Ready.";
            // 
            // statusProgress
            // 
            this.statusProgress.Name = "statusProgress";
            this.statusProgress.Size = new System.Drawing.Size(100, 24);
            this.statusProgress.Visible = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filterToolStripLabel,
            this.filterToolStripTextBox});
            this.toolStrip1.Location = new System.Drawing.Point(61, 33);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(249, 31);
            this.toolStrip1.TabIndex = 1;
            // 
            // filterToolStripLabel
            // 
            this.filterToolStripLabel.Name = "filterToolStripLabel";
            this.filterToolStripLabel.Size = new System.Drawing.Size(102, 26);
            this.filterToolStripLabel.Text = "Files mask: ";
            // 
            // filterToolStripTextBox
            // 
            this.filterToolStripTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.filterToolStripTextBox.Name = "filterToolStripTextBox";
            this.filterToolStripTextBox.Size = new System.Drawing.Size(125, 31);
            this.filterToolStripTextBox.Text = "*";
            this.filterToolStripTextBox.TextChanged += new System.EventHandler(this.filterToolStripTextBox_TextChanged);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openStorageToolStripButton});
            this.toolStrip2.Location = new System.Drawing.Point(9, 33);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(52, 33);
            this.toolStrip2.TabIndex = 2;
            // 
            // openStorageToolStripButton
            // 
            this.openStorageToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openStorageToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openStorageToolStripButton.Image")));
            this.openStorageToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openStorageToolStripButton.Name = "openStorageToolStripButton";
            this.openStorageToolStripButton.Size = new System.Drawing.Size(34, 28);
            this.openStorageToolStripButton.Text = "&Open Storage";
            this.openStorageToolStripButton.Click += new System.EventHandler(this.openStorageToolStripButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1402, 33);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openStorageToolStripMenuItem,
            this.openOnlineStorageToolStripMenuItem,
            this.openRecentStorageToolStripMenuItem,
            this.closeStorageToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(54, 29);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openStorageToolStripMenuItem
            // 
            this.openStorageToolStripMenuItem.Name = "openStorageToolStripMenuItem";
            this.openStorageToolStripMenuItem.Size = new System.Drawing.Size(281, 34);
            this.openStorageToolStripMenuItem.Text = "Open Storage...";
            this.openStorageToolStripMenuItem.Click += new System.EventHandler(this.openStorageToolStripMenuItem_Click);
            // 
            // openOnlineStorageToolStripMenuItem
            // 
            this.openOnlineStorageToolStripMenuItem.Enabled = false;
            this.openOnlineStorageToolStripMenuItem.Name = "openOnlineStorageToolStripMenuItem";
            this.openOnlineStorageToolStripMenuItem.Size = new System.Drawing.Size(281, 34);
            this.openOnlineStorageToolStripMenuItem.Text = "Open Online Storage";
            this.openOnlineStorageToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.openOnlineStorageToolStripMenuItem_DropDownItemClicked);
            // 
            // openRecentStorageToolStripMenuItem
            // 
            this.openRecentStorageToolStripMenuItem.Enabled = false;
            this.openRecentStorageToolStripMenuItem.Name = "openRecentStorageToolStripMenuItem";
            this.openRecentStorageToolStripMenuItem.Size = new System.Drawing.Size(281, 34);
            this.openRecentStorageToolStripMenuItem.Text = "Open Recent Storage";
            this.openRecentStorageToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.openRecentStorageToolStripMenuItem_DropDownItemClicked);
            // 
            // closeStorageToolStripMenuItem
            // 
            this.closeStorageToolStripMenuItem.Name = "closeStorageToolStripMenuItem";
            this.closeStorageToolStripMenuItem.Size = new System.Drawing.Size(281, 34);
            this.closeStorageToolStripMenuItem.Text = "Close Storage";
            this.closeStorageToolStripMenuItem.Click += new System.EventHandler(this.closeStorageToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(281, 34);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.findToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(58, 29);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // findToolStripMenuItem
            // 
            this.findToolStripMenuItem.Name = "findToolStripMenuItem";
            this.findToolStripMenuItem.Size = new System.Drawing.Size(160, 34);
            this.findToolStripMenuItem.Text = "Find...";
            this.findToolStripMenuItem.Click += new System.EventHandler(this.findToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.localeFlagsToolStripMenuItem,
            this.useLVToolStripMenuItem,
            this.tsmShowPreview,
            this.openListFileToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(65, 29);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // localeFlagsToolStripMenuItem
            // 
            this.localeFlagsToolStripMenuItem.Enabled = false;
            this.localeFlagsToolStripMenuItem.Name = "localeFlagsToolStripMenuItem";
            this.localeFlagsToolStripMenuItem.Size = new System.Drawing.Size(224, 34);
            this.localeFlagsToolStripMenuItem.Text = "Locale";
            this.localeFlagsToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.localeToolStripMenuItem_DropDownItemClicked);
            // 
            // useLVToolStripMenuItem
            // 
            this.useLVToolStripMenuItem.Enabled = false;
            this.useLVToolStripMenuItem.Name = "useLVToolStripMenuItem";
            this.useLVToolStripMenuItem.Size = new System.Drawing.Size(224, 34);
            this.useLVToolStripMenuItem.Text = "Use LV";
            this.useLVToolStripMenuItem.Click += new System.EventHandler(this.contentFlagsToolStripMenuItem_Click);
            // 
            // tsmShowPreview
            // 
            this.tsmShowPreview.Checked = true;
            this.tsmShowPreview.CheckOnClick = true;
            this.tsmShowPreview.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsmShowPreview.Name = "tsmShowPreview";
            this.tsmShowPreview.Size = new System.Drawing.Size(224, 34);
            this.tsmShowPreview.Text = "Show preview";
            this.tsmShowPreview.CheckedChanged += new System.EventHandler(this.tsmShowPreview_CheckedChanged);
            // 
            // openListFileToolStripMenuItem
            // 
            this.openListFileToolStripMenuItem.Name = "openListFileToolStripMenuItem";
            this.openListFileToolStripMenuItem.Size = new System.Drawing.Size(224, 34);
            this.openListFileToolStripMenuItem.Text = "Open list file";
            this.openListFileToolStripMenuItem.Click += new System.EventHandler(this.openListFileToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scanFilesToolStripMenuItem,
            this.analyseUnknownFilesToolStripMenuItem,
            this.extractInstallFilesToolStripMenuItem,
            this.CDNBuildsToolStripMenuItem,
            this.extractCASCSystemFilesToolStripMenuItem,
            this.bruteforceNamesToolStripMenuItem,
            this.exportListfileToolStripMenuItem,
            this.exportFoldersToolStripMenuItem,
            this.analyzeSoundFilesToolStripMenuItem,
            this.addFileDataIDToSoundFilesToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(69, 29);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // scanFilesToolStripMenuItem
            // 
            this.scanFilesToolStripMenuItem.Enabled = false;
            this.scanFilesToolStripMenuItem.Name = "scanFilesToolStripMenuItem";
            this.scanFilesToolStripMenuItem.Size = new System.Drawing.Size(352, 34);
            this.scanFilesToolStripMenuItem.Text = "Scan Files";
            this.scanFilesToolStripMenuItem.Click += new System.EventHandler(this.scanFilesToolStripMenuItem_Click);
            // 
            // analyseUnknownFilesToolStripMenuItem
            // 
            this.analyseUnknownFilesToolStripMenuItem.Enabled = false;
            this.analyseUnknownFilesToolStripMenuItem.Name = "analyseUnknownFilesToolStripMenuItem";
            this.analyseUnknownFilesToolStripMenuItem.Size = new System.Drawing.Size(352, 34);
            this.analyseUnknownFilesToolStripMenuItem.Text = "Analyse Unknown Files";
            this.analyseUnknownFilesToolStripMenuItem.Click += new System.EventHandler(this.analyseUnknownFilesToolStripMenuItem_Click);
            // 
            // extractInstallFilesToolStripMenuItem
            // 
            this.extractInstallFilesToolStripMenuItem.Enabled = false;
            this.extractInstallFilesToolStripMenuItem.Name = "extractInstallFilesToolStripMenuItem";
            this.extractInstallFilesToolStripMenuItem.Size = new System.Drawing.Size(352, 34);
            this.extractInstallFilesToolStripMenuItem.Text = "Extract Install Files";
            this.extractInstallFilesToolStripMenuItem.Click += new System.EventHandler(this.extractInstallFilesToolStripMenuItem_Click);
            // 
            // CDNBuildsToolStripMenuItem
            // 
            this.CDNBuildsToolStripMenuItem.Enabled = false;
            this.CDNBuildsToolStripMenuItem.Name = "CDNBuildsToolStripMenuItem";
            this.CDNBuildsToolStripMenuItem.Size = new System.Drawing.Size(352, 34);
            this.CDNBuildsToolStripMenuItem.Text = "CDN Builds";
            // 
            // extractCASCSystemFilesToolStripMenuItem
            // 
            this.extractCASCSystemFilesToolStripMenuItem.Enabled = false;
            this.extractCASCSystemFilesToolStripMenuItem.Name = "extractCASCSystemFilesToolStripMenuItem";
            this.extractCASCSystemFilesToolStripMenuItem.Size = new System.Drawing.Size(352, 34);
            this.extractCASCSystemFilesToolStripMenuItem.Text = "Extract CASC System Files";
            this.extractCASCSystemFilesToolStripMenuItem.Click += new System.EventHandler(this.extractCASCSystemFilesToolStripMenuItem_Click);
            // 
            // bruteforceNamesToolStripMenuItem
            // 
            this.bruteforceNamesToolStripMenuItem.Enabled = false;
            this.bruteforceNamesToolStripMenuItem.Name = "bruteforceNamesToolStripMenuItem";
            this.bruteforceNamesToolStripMenuItem.Size = new System.Drawing.Size(352, 34);
            this.bruteforceNamesToolStripMenuItem.Text = "Bruteforce Names";
            this.bruteforceNamesToolStripMenuItem.Click += new System.EventHandler(this.bruteforceNamesToolStripMenuItem_Click);
            // 
            // exportListfileToolStripMenuItem
            // 
            this.exportListfileToolStripMenuItem.Enabled = false;
            this.exportListfileToolStripMenuItem.Name = "exportListfileToolStripMenuItem";
            this.exportListfileToolStripMenuItem.Size = new System.Drawing.Size(352, 34);
            this.exportListfileToolStripMenuItem.Text = "Export listfile";
            this.exportListfileToolStripMenuItem.Click += new System.EventHandler(this.exportListfileToolStripMenuItem_Click);
            // 
            // exportFoldersToolStripMenuItem
            // 
            this.exportFoldersToolStripMenuItem.Name = "exportFoldersToolStripMenuItem";
            this.exportFoldersToolStripMenuItem.Size = new System.Drawing.Size(352, 34);
            this.exportFoldersToolStripMenuItem.Text = "Export folders";
            this.exportFoldersToolStripMenuItem.Click += new System.EventHandler(this.exportFoldersToolStripMenuItem_Click);
            // 
            // analyzeSoundFilesToolStripMenuItem
            // 
            this.analyzeSoundFilesToolStripMenuItem.Checked = true;
            this.analyzeSoundFilesToolStripMenuItem.CheckOnClick = true;
            this.analyzeSoundFilesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.analyzeSoundFilesToolStripMenuItem.Enabled = false;
            this.analyzeSoundFilesToolStripMenuItem.Name = "analyzeSoundFilesToolStripMenuItem";
            this.analyzeSoundFilesToolStripMenuItem.Size = new System.Drawing.Size(352, 34);
            this.analyzeSoundFilesToolStripMenuItem.Text = "Analyze Sound Files";
            this.analyzeSoundFilesToolStripMenuItem.CheckedChanged += new System.EventHandler(this.analyzeSoundFilesToolStripMenuItem_CheckedChanged);
            // 
            // addFileDataIDToSoundFilesToolStripMenuItem
            // 
            this.addFileDataIDToSoundFilesToolStripMenuItem.Checked = true;
            this.addFileDataIDToSoundFilesToolStripMenuItem.CheckOnClick = true;
            this.addFileDataIDToSoundFilesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.addFileDataIDToSoundFilesToolStripMenuItem.Enabled = false;
            this.addFileDataIDToSoundFilesToolStripMenuItem.Name = "addFileDataIDToSoundFilesToolStripMenuItem";
            this.addFileDataIDToSoundFilesToolStripMenuItem.Size = new System.Drawing.Size(352, 34);
            this.addFileDataIDToSoundFilesToolStripMenuItem.Text = "Add FileDataID to Sound Files";
            this.addFileDataIDToSoundFilesToolStripMenuItem.CheckedChanged += new System.EventHandler(this.addFileDataIDToSoundFilesToolStripMenuItem_CheckedChanged);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(65, 29);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(176, 34);
            this.aboutToolStripMenuItem.Text = "About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // fileList
            // 
            this.fileList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader5,
            this.columnHeader4});
            this.fileList.ContextMenuStrip = this.contextMenuStrip1;
            this.fileList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileList.FullRowSelect = true;
            this.fileList.HideSelection = false;
            this.fileList.Location = new System.Drawing.Point(0, 0);
            this.fileList.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.fileList.Name = "fileList";
            this.fileList.SelectedIndex = -1;
            this.fileList.Size = new System.Drawing.Size(704, 728);
            this.fileList.SmallImageList = this.iconsList;
            this.fileList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.fileList.TabIndex = 0;
            this.fileList.UseCompatibleStateImageBehavior = false;
            this.fileList.View = System.Windows.Forms.View.Details;
            this.fileList.VirtualMode = true;
            this.fileList.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);
            this.fileList.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.listView1_RetrieveVirtualItem);
            this.fileList.SearchForVirtualItem += new System.Windows.Forms.SearchForVirtualItemEventHandler(this.fileList_SearchForVirtualItem);
            this.fileList.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.fileList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView1_KeyDown);
            this.fileList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "File Name";
            this.columnHeader1.Width = 250;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Type";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Locale Flags";
            this.columnHeader3.Width = 115;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Content Flags";
            this.columnHeader5.Width = 115;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Size";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader4.Width = 80;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1402, 826);
            this.Controls.Add(this.toolStripContainer1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.Text = "CASC Explorer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView folderTree;
        private System.Windows.Forms.ImageList iconsList;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripProgressBar statusProgress;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem extractToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyNameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scanFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem analyseUnknownFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem localeFlagsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getSizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem useLVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openStorageToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog storageFolderBrowserDialog;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extractInstallFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CDNBuildsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extractCASCSystemFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bruteforceNamesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openOnlineStorageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeStorageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openRecentStorageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportListfileToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel filterToolStripLabel;
        private System.Windows.Forms.ToolStripTextBox filterToolStripTextBox;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton openStorageToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem exportFoldersToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private NoFlickerListView fileList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ToolStripMenuItem tsmShowPreview;
        private System.Windows.Forms.ToolStripMenuItem openListFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem analyzeSoundFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addFileDataIDToSoundFilesToolStripMenuItem;
    }
}

