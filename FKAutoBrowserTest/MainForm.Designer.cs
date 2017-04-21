namespace FKAutoBrowserTest
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.m_HideWebPanelBtn = new System.Windows.Forms.Button();
            this.m_MainTabContorl = new System.Windows.Forms.TabControl();
            this.m_ScriptControlTab = new System.Windows.Forms.TabPage();
            this.m_ScriptTabContorl = new System.Windows.Forms.TabControl();
            this.m_ScriptEditTabPage = new System.Windows.Forms.TabPage();
            this.m_ScriptEditor = new FastColoredTextBoxNS.FastColoredTextBox();
            this.m_ScriptLogTabPage = new System.Windows.Forms.TabPage();
            this.m_ScriptProcessBar = new System.Windows.Forms.ProgressBar();
            this.m_LogRichBox = new System.Windows.Forms.RichTextBox();
            this.m_ScriptHelpTabPage = new System.Windows.Forms.TabPage();
            this.m_HelpRichBox = new System.Windows.Forms.RichTextBox();
            this.m_StopScriptBtn = new System.Windows.Forms.Button();
            this.m_PauseScriptBtn = new System.Windows.Forms.Button();
            this.m_RunScriptBtn = new System.Windows.Forms.Button();
            this.m_SaveScriptBtn = new System.Windows.Forms.Button();
            this.m_OpenScriptBtn = new System.Windows.Forms.Button();
            this.m_OthersControlTab = new System.Windows.Forms.TabPage();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件FToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于本软件F12ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_WebLoadingPB = new System.Windows.Forms.ProgressBar();
            this.m_WebUrlEdit = new System.Windows.Forms.TextBox();
            this.m_HideControlPanelBtn = new System.Windows.Forms.Button();
            this.m_WebBrowserContainer = new System.Windows.Forms.Panel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.m_FKIEWebBrowser = new FKAutoBrowser.FKIEWebDriver();
            this.m_IEHelperTabControl = new System.Windows.Forms.TabControl();
            this.m_PostGetTabPage = new System.Windows.Forms.TabPage();
            this.m_PostGetListBox = new System.Windows.Forms.ListBox();
            this.m_WebSourcePage = new System.Windows.Forms.TabPage();
            this.m_WebSourceRichTextBox = new System.Windows.Forms.RichTextBox();
            this.m_XPathPage = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.m_bIsSaveXPathTree = new System.Windows.Forms.CheckBox();
            this.m_XPathDataGridView = new System.Windows.Forms.DataGridView();
            this.XPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_VisitXPathBtn = new System.Windows.Forms.Button();
            this.m_XPathRichBox = new System.Windows.Forms.RichTextBox();
            this.m_GetXPathBtn = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.m_MainTabContorl.SuspendLayout();
            this.m_ScriptControlTab.SuspendLayout();
            this.m_ScriptTabContorl.SuspendLayout();
            this.m_ScriptEditTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_ScriptEditor)).BeginInit();
            this.m_ScriptLogTabPage.SuspendLayout();
            this.m_ScriptHelpTabPage.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.m_WebBrowserContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.m_IEHelperTabControl.SuspendLayout();
            this.m_PostGetTabPage.SuspendLayout();
            this.m_WebSourcePage.SuspendLayout();
            this.m_XPathPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_XPathDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.m_HideWebPanelBtn);
            this.splitContainer1.Panel1.Controls.Add(this.m_MainTabContorl);
            this.splitContainer1.Panel1.Controls.Add(this.menuStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.m_WebLoadingPB);
            this.splitContainer1.Panel2.Controls.Add(this.m_WebUrlEdit);
            this.splitContainer1.Panel2.Controls.Add(this.m_HideControlPanelBtn);
            this.splitContainer1.Panel2.Controls.Add(this.m_WebBrowserContainer);
            this.splitContainer1.Size = new System.Drawing.Size(1272, 741);
            this.splitContainer1.SplitterDistance = 448;
            this.splitContainer1.TabIndex = 0;
            // 
            // m_HideWebPanelBtn
            // 
            this.m_HideWebPanelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_HideWebPanelBtn.BackColor = System.Drawing.Color.Silver;
            this.m_HideWebPanelBtn.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_HideWebPanelBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_HideWebPanelBtn.Location = new System.Drawing.Point(421, 0);
            this.m_HideWebPanelBtn.Name = "m_HideWebPanelBtn";
            this.m_HideWebPanelBtn.Size = new System.Drawing.Size(24, 23);
            this.m_HideWebPanelBtn.TabIndex = 3;
            this.m_HideWebPanelBtn.Text = "<";
            this.m_HideWebPanelBtn.UseVisualStyleBackColor = false;
            this.m_HideWebPanelBtn.Click += new System.EventHandler(this.m_HideWebPanelBtn_Click);
            // 
            // m_MainTabContorl
            // 
            this.m_MainTabContorl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_MainTabContorl.Controls.Add(this.m_ScriptControlTab);
            this.m_MainTabContorl.Controls.Add(this.m_OthersControlTab);
            this.m_MainTabContorl.Location = new System.Drawing.Point(3, 32);
            this.m_MainTabContorl.Name = "m_MainTabContorl";
            this.m_MainTabContorl.SelectedIndex = 0;
            this.m_MainTabContorl.Size = new System.Drawing.Size(443, 709);
            this.m_MainTabContorl.TabIndex = 0;
            // 
            // m_ScriptControlTab
            // 
            this.m_ScriptControlTab.Controls.Add(this.m_ScriptTabContorl);
            this.m_ScriptControlTab.Controls.Add(this.m_StopScriptBtn);
            this.m_ScriptControlTab.Controls.Add(this.m_PauseScriptBtn);
            this.m_ScriptControlTab.Controls.Add(this.m_RunScriptBtn);
            this.m_ScriptControlTab.Controls.Add(this.m_SaveScriptBtn);
            this.m_ScriptControlTab.Controls.Add(this.m_OpenScriptBtn);
            this.m_ScriptControlTab.Location = new System.Drawing.Point(4, 22);
            this.m_ScriptControlTab.Name = "m_ScriptControlTab";
            this.m_ScriptControlTab.Padding = new System.Windows.Forms.Padding(3);
            this.m_ScriptControlTab.Size = new System.Drawing.Size(435, 683);
            this.m_ScriptControlTab.TabIndex = 0;
            this.m_ScriptControlTab.Text = "脚本管理";
            this.m_ScriptControlTab.UseVisualStyleBackColor = true;
            // 
            // m_ScriptTabContorl
            // 
            this.m_ScriptTabContorl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_ScriptTabContorl.Controls.Add(this.m_ScriptEditTabPage);
            this.m_ScriptTabContorl.Controls.Add(this.m_ScriptLogTabPage);
            this.m_ScriptTabContorl.Controls.Add(this.m_ScriptHelpTabPage);
            this.m_ScriptTabContorl.Location = new System.Drawing.Point(0, 74);
            this.m_ScriptTabContorl.Name = "m_ScriptTabContorl";
            this.m_ScriptTabContorl.SelectedIndex = 0;
            this.m_ScriptTabContorl.Size = new System.Drawing.Size(436, 610);
            this.m_ScriptTabContorl.TabIndex = 5;
            // 
            // m_ScriptEditTabPage
            // 
            this.m_ScriptEditTabPage.Controls.Add(this.m_ScriptEditor);
            this.m_ScriptEditTabPage.Location = new System.Drawing.Point(4, 22);
            this.m_ScriptEditTabPage.Name = "m_ScriptEditTabPage";
            this.m_ScriptEditTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.m_ScriptEditTabPage.Size = new System.Drawing.Size(428, 584);
            this.m_ScriptEditTabPage.TabIndex = 0;
            this.m_ScriptEditTabPage.Text = "脚本面板";
            this.m_ScriptEditTabPage.UseVisualStyleBackColor = true;
            // 
            // m_ScriptEditor
            // 
            this.m_ScriptEditor.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.m_ScriptEditor.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.m_ScriptEditor.BackBrush = null;
            this.m_ScriptEditor.CharHeight = 14;
            this.m_ScriptEditor.CharWidth = 8;
            this.m_ScriptEditor.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.m_ScriptEditor.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.m_ScriptEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_ScriptEditor.IsReplaceMode = false;
            this.m_ScriptEditor.Location = new System.Drawing.Point(3, 3);
            this.m_ScriptEditor.Name = "m_ScriptEditor";
            this.m_ScriptEditor.Paddings = new System.Windows.Forms.Padding(0);
            this.m_ScriptEditor.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_ScriptEditor.Size = new System.Drawing.Size(422, 578);
            this.m_ScriptEditor.TabIndex = 0;
            this.m_ScriptEditor.Zoom = 100;
            this.m_ScriptEditor.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.m_ScriptEditor_TextChanged);
            this.m_ScriptEditor.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_ScriptEditor_MouseDown);
            this.m_ScriptEditor.MouseMove += new System.Windows.Forms.MouseEventHandler(this.m_ScriptEditor_MouseMove);
            // 
            // m_ScriptLogTabPage
            // 
            this.m_ScriptLogTabPage.Controls.Add(this.m_ScriptProcessBar);
            this.m_ScriptLogTabPage.Controls.Add(this.m_LogRichBox);
            this.m_ScriptLogTabPage.Location = new System.Drawing.Point(4, 22);
            this.m_ScriptLogTabPage.Name = "m_ScriptLogTabPage";
            this.m_ScriptLogTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.m_ScriptLogTabPage.Size = new System.Drawing.Size(428, 584);
            this.m_ScriptLogTabPage.TabIndex = 1;
            this.m_ScriptLogTabPage.Text = "运行日志";
            this.m_ScriptLogTabPage.UseVisualStyleBackColor = true;
            // 
            // m_ScriptProcessBar
            // 
            this.m_ScriptProcessBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_ScriptProcessBar.Location = new System.Drawing.Point(1, 557);
            this.m_ScriptProcessBar.Name = "m_ScriptProcessBar";
            this.m_ScriptProcessBar.Size = new System.Drawing.Size(430, 29);
            this.m_ScriptProcessBar.TabIndex = 6;
            // 
            // m_LogRichBox
            // 
            this.m_LogRichBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_LogRichBox.Location = new System.Drawing.Point(3, 3);
            this.m_LogRichBox.Name = "m_LogRichBox";
            this.m_LogRichBox.Size = new System.Drawing.Size(422, 548);
            this.m_LogRichBox.TabIndex = 0;
            this.m_LogRichBox.Text = "";
            this.m_LogRichBox.TextChanged += new System.EventHandler(this.m_LogRichBox_TextChanged);
            // 
            // m_ScriptHelpTabPage
            // 
            this.m_ScriptHelpTabPage.Controls.Add(this.m_HelpRichBox);
            this.m_ScriptHelpTabPage.Location = new System.Drawing.Point(4, 22);
            this.m_ScriptHelpTabPage.Name = "m_ScriptHelpTabPage";
            this.m_ScriptHelpTabPage.Size = new System.Drawing.Size(428, 584);
            this.m_ScriptHelpTabPage.TabIndex = 2;
            this.m_ScriptHelpTabPage.Text = "脚本帮助";
            this.m_ScriptHelpTabPage.UseVisualStyleBackColor = true;
            // 
            // m_HelpRichBox
            // 
            this.m_HelpRichBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_HelpRichBox.Location = new System.Drawing.Point(0, 0);
            this.m_HelpRichBox.Name = "m_HelpRichBox";
            this.m_HelpRichBox.Size = new System.Drawing.Size(428, 584);
            this.m_HelpRichBox.TabIndex = 0;
            this.m_HelpRichBox.Text = "";
            // 
            // m_StopScriptBtn
            // 
            this.m_StopScriptBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_StopScriptBtn.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_StopScriptBtn.Image = global::FKAutoBrowserTest.Properties.Resources.Stop;
            this.m_StopScriptBtn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.m_StopScriptBtn.Location = new System.Drawing.Point(367, 6);
            this.m_StopScriptBtn.Name = "m_StopScriptBtn";
            this.m_StopScriptBtn.Size = new System.Drawing.Size(62, 62);
            this.m_StopScriptBtn.TabIndex = 4;
            this.m_StopScriptBtn.Text = "停止脚本";
            this.m_StopScriptBtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.m_StopScriptBtn.UseVisualStyleBackColor = true;
            this.m_StopScriptBtn.Click += new System.EventHandler(this.m_StopScriptBtn_Click);
            // 
            // m_PauseScriptBtn
            // 
            this.m_PauseScriptBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_PauseScriptBtn.Image = global::FKAutoBrowserTest.Properties.Resources.Pause;
            this.m_PauseScriptBtn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.m_PauseScriptBtn.Location = new System.Drawing.Point(299, 6);
            this.m_PauseScriptBtn.Name = "m_PauseScriptBtn";
            this.m_PauseScriptBtn.Size = new System.Drawing.Size(62, 62);
            this.m_PauseScriptBtn.TabIndex = 3;
            this.m_PauseScriptBtn.Text = "暂停脚本";
            this.m_PauseScriptBtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.m_PauseScriptBtn.UseVisualStyleBackColor = true;
            this.m_PauseScriptBtn.Click += new System.EventHandler(this.m_PauseScriptBtn_Click);
            // 
            // m_RunScriptBtn
            // 
            this.m_RunScriptBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_RunScriptBtn.Image = global::FKAutoBrowserTest.Properties.Resources.Go;
            this.m_RunScriptBtn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.m_RunScriptBtn.Location = new System.Drawing.Point(231, 6);
            this.m_RunScriptBtn.Name = "m_RunScriptBtn";
            this.m_RunScriptBtn.Size = new System.Drawing.Size(62, 62);
            this.m_RunScriptBtn.TabIndex = 2;
            this.m_RunScriptBtn.Text = "运行脚本";
            this.m_RunScriptBtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.m_RunScriptBtn.UseVisualStyleBackColor = true;
            this.m_RunScriptBtn.Click += new System.EventHandler(this.m_RunScriptBtn_Click);
            // 
            // m_SaveScriptBtn
            // 
            this.m_SaveScriptBtn.Image = global::FKAutoBrowserTest.Properties.Resources.Save;
            this.m_SaveScriptBtn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.m_SaveScriptBtn.Location = new System.Drawing.Point(74, 6);
            this.m_SaveScriptBtn.Name = "m_SaveScriptBtn";
            this.m_SaveScriptBtn.Size = new System.Drawing.Size(62, 62);
            this.m_SaveScriptBtn.TabIndex = 1;
            this.m_SaveScriptBtn.Text = "保存脚本";
            this.m_SaveScriptBtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.m_SaveScriptBtn.UseVisualStyleBackColor = true;
            this.m_SaveScriptBtn.Click += new System.EventHandler(this.m_SaveScriptBtn_Click);
            // 
            // m_OpenScriptBtn
            // 
            this.m_OpenScriptBtn.Image = global::FKAutoBrowserTest.Properties.Resources.Open;
            this.m_OpenScriptBtn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.m_OpenScriptBtn.Location = new System.Drawing.Point(6, 6);
            this.m_OpenScriptBtn.Name = "m_OpenScriptBtn";
            this.m_OpenScriptBtn.Size = new System.Drawing.Size(62, 62);
            this.m_OpenScriptBtn.TabIndex = 0;
            this.m_OpenScriptBtn.Text = "打开脚本";
            this.m_OpenScriptBtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.m_OpenScriptBtn.UseVisualStyleBackColor = true;
            this.m_OpenScriptBtn.Click += new System.EventHandler(this.m_OpenScriptBtn_Click);
            // 
            // m_OthersControlTab
            // 
            this.m_OthersControlTab.Location = new System.Drawing.Point(4, 22);
            this.m_OthersControlTab.Name = "m_OthersControlTab";
            this.m_OthersControlTab.Padding = new System.Windows.Forms.Padding(3);
            this.m_OthersControlTab.Size = new System.Drawing.Size(435, 683);
            this.m_OthersControlTab.TabIndex = 1;
            this.m_OthersControlTab.Text = "其他";
            this.m_OthersControlTab.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件FToolStripMenuItem,
            this.关于AToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(448, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件FToolStripMenuItem
            // 
            this.文件FToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.退出ToolStripMenuItem});
            this.文件FToolStripMenuItem.Name = "文件FToolStripMenuItem";
            this.文件FToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.文件FToolStripMenuItem.Text = "文件(&F)";
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // 关于AToolStripMenuItem
            // 
            this.关于AToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.关于本软件F12ToolStripMenuItem});
            this.关于AToolStripMenuItem.Name = "关于AToolStripMenuItem";
            this.关于AToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.关于AToolStripMenuItem.Text = "关于(&A)";
            // 
            // 关于本软件F12ToolStripMenuItem
            // 
            this.关于本软件F12ToolStripMenuItem.Name = "关于本软件F12ToolStripMenuItem";
            this.关于本软件F12ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F12;
            this.关于本软件F12ToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.关于本软件F12ToolStripMenuItem.Text = "关于本软件";
            this.关于本软件F12ToolStripMenuItem.Click += new System.EventHandler(this.关于本软件F12ToolStripMenuItem_Click);
            // 
            // m_WebLoadingPB
            // 
            this.m_WebLoadingPB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_WebLoadingPB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.m_WebLoadingPB.Location = new System.Drawing.Point(649, 34);
            this.m_WebLoadingPB.Maximum = 10000;
            this.m_WebLoadingPB.Name = "m_WebLoadingPB";
            this.m_WebLoadingPB.Size = new System.Drawing.Size(171, 23);
            this.m_WebLoadingPB.TabIndex = 5;
            // 
            // m_WebUrlEdit
            // 
            this.m_WebUrlEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_WebUrlEdit.Location = new System.Drawing.Point(3, 34);
            this.m_WebUrlEdit.Name = "m_WebUrlEdit";
            this.m_WebUrlEdit.Size = new System.Drawing.Size(640, 21);
            this.m_WebUrlEdit.TabIndex = 4;
            this.m_WebUrlEdit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_WebUrlEdit_KeyDown);
            // 
            // m_HideControlPanelBtn
            // 
            this.m_HideControlPanelBtn.BackColor = System.Drawing.Color.Silver;
            this.m_HideControlPanelBtn.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_HideControlPanelBtn.Location = new System.Drawing.Point(4, 0);
            this.m_HideControlPanelBtn.Name = "m_HideControlPanelBtn";
            this.m_HideControlPanelBtn.Size = new System.Drawing.Size(24, 23);
            this.m_HideControlPanelBtn.TabIndex = 2;
            this.m_HideControlPanelBtn.Text = ">";
            this.m_HideControlPanelBtn.UseVisualStyleBackColor = false;
            this.m_HideControlPanelBtn.Click += new System.EventHandler(this.m_HideControlPanelBtn_Click);
            // 
            // m_WebBrowserContainer
            // 
            this.m_WebBrowserContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_WebBrowserContainer.Controls.Add(this.splitContainer2);
            this.m_WebBrowserContainer.Location = new System.Drawing.Point(0, 60);
            this.m_WebBrowserContainer.Name = "m_WebBrowserContainer";
            this.m_WebBrowserContainer.Size = new System.Drawing.Size(820, 681);
            this.m_WebBrowserContainer.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.m_FKIEWebBrowser);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.m_IEHelperTabControl);
            this.splitContainer2.Size = new System.Drawing.Size(820, 681);
            this.splitContainer2.SplitterDistance = 519;
            this.splitContainer2.TabIndex = 1;
            // 
            // m_FKIEWebBrowser
            // 
            this.m_FKIEWebBrowser.AllowWebBrowserDrop = false;
            this.m_FKIEWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_FKIEWebBrowser.IsDocumentCompleted = false;
            this.m_FKIEWebBrowser.IsGetXPathMode = false;
            this.m_FKIEWebBrowser.IsWebBrowserContextMenuEnabled = false;
            this.m_FKIEWebBrowser.Location = new System.Drawing.Point(0, 0);
            this.m_FKIEWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.m_FKIEWebBrowser.Name = "m_FKIEWebBrowser";
            this.m_FKIEWebBrowser.PageSourcePanel = null;
            this.m_FKIEWebBrowser.RequestRecorderList = null;
            this.m_FKIEWebBrowser.ScriptErrorsSuppressed = true;
            this.m_FKIEWebBrowser.Size = new System.Drawing.Size(820, 519);
            this.m_FKIEWebBrowser.TabIndex = 0;
            this.m_FKIEWebBrowser.XPathDateGridView = null;
            this.m_FKIEWebBrowser.XPathInfoShowPanel = null;
            this.m_FKIEWebBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.m_FKIEWebBrowser_DocumentCompleted);
            this.m_FKIEWebBrowser.ProgressChanged += new System.Windows.Forms.WebBrowserProgressChangedEventHandler(this.m_FKIEWebBrowser_ProgressChanged);
            // 
            // m_IEHelperTabControl
            // 
            this.m_IEHelperTabControl.Controls.Add(this.m_PostGetTabPage);
            this.m_IEHelperTabControl.Controls.Add(this.m_WebSourcePage);
            this.m_IEHelperTabControl.Controls.Add(this.m_XPathPage);
            this.m_IEHelperTabControl.Controls.Add(this.tabPage2);
            this.m_IEHelperTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_IEHelperTabControl.Location = new System.Drawing.Point(0, 0);
            this.m_IEHelperTabControl.Name = "m_IEHelperTabControl";
            this.m_IEHelperTabControl.SelectedIndex = 0;
            this.m_IEHelperTabControl.Size = new System.Drawing.Size(820, 158);
            this.m_IEHelperTabControl.TabIndex = 0;
            // 
            // m_PostGetTabPage
            // 
            this.m_PostGetTabPage.Controls.Add(this.m_PostGetListBox);
            this.m_PostGetTabPage.Location = new System.Drawing.Point(4, 22);
            this.m_PostGetTabPage.Name = "m_PostGetTabPage";
            this.m_PostGetTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.m_PostGetTabPage.Size = new System.Drawing.Size(812, 132);
            this.m_PostGetTabPage.TabIndex = 0;
            this.m_PostGetTabPage.Text = "POST/GET请求";
            this.m_PostGetTabPage.UseVisualStyleBackColor = true;
            // 
            // m_PostGetListBox
            // 
            this.m_PostGetListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_PostGetListBox.FormattingEnabled = true;
            this.m_PostGetListBox.ItemHeight = 12;
            this.m_PostGetListBox.Location = new System.Drawing.Point(3, 3);
            this.m_PostGetListBox.Name = "m_PostGetListBox";
            this.m_PostGetListBox.Size = new System.Drawing.Size(806, 126);
            this.m_PostGetListBox.TabIndex = 0;
            // 
            // m_WebSourcePage
            // 
            this.m_WebSourcePage.Controls.Add(this.m_WebSourceRichTextBox);
            this.m_WebSourcePage.Location = new System.Drawing.Point(4, 22);
            this.m_WebSourcePage.Name = "m_WebSourcePage";
            this.m_WebSourcePage.Size = new System.Drawing.Size(812, 132);
            this.m_WebSourcePage.TabIndex = 2;
            this.m_WebSourcePage.Text = "网页源码";
            this.m_WebSourcePage.UseVisualStyleBackColor = true;
            // 
            // m_WebSourceRichTextBox
            // 
            this.m_WebSourceRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_WebSourceRichTextBox.Location = new System.Drawing.Point(0, 0);
            this.m_WebSourceRichTextBox.Name = "m_WebSourceRichTextBox";
            this.m_WebSourceRichTextBox.Size = new System.Drawing.Size(812, 132);
            this.m_WebSourceRichTextBox.TabIndex = 0;
            this.m_WebSourceRichTextBox.Text = "";
            // 
            // m_XPathPage
            // 
            this.m_XPathPage.Controls.Add(this.splitContainer3);
            this.m_XPathPage.Location = new System.Drawing.Point(4, 22);
            this.m_XPathPage.Name = "m_XPathPage";
            this.m_XPathPage.Size = new System.Drawing.Size(812, 132);
            this.m_XPathPage.TabIndex = 3;
            this.m_XPathPage.Text = "XPath列表";
            this.m_XPathPage.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.m_bIsSaveXPathTree);
            this.splitContainer3.Panel1.Controls.Add(this.m_XPathDataGridView);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.m_VisitXPathBtn);
            this.splitContainer3.Panel2.Controls.Add(this.m_XPathRichBox);
            this.splitContainer3.Panel2.Controls.Add(this.m_GetXPathBtn);
            this.splitContainer3.Size = new System.Drawing.Size(812, 132);
            this.splitContainer3.SplitterDistance = 521;
            this.splitContainer3.TabIndex = 1;
            // 
            // m_bIsSaveXPathTree
            // 
            this.m_bIsSaveXPathTree.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_bIsSaveXPathTree.AutoSize = true;
            this.m_bIsSaveXPathTree.Location = new System.Drawing.Point(404, 3);
            this.m_bIsSaveXPathTree.Name = "m_bIsSaveXPathTree";
            this.m_bIsSaveXPathTree.Size = new System.Drawing.Size(114, 16);
            this.m_bIsSaveXPathTree.TabIndex = 4;
            this.m_bIsSaveXPathTree.Text = "开启XPath树记录";
            this.m_bIsSaveXPathTree.UseVisualStyleBackColor = true;
            this.m_bIsSaveXPathTree.CheckedChanged += new System.EventHandler(this.m_bIsSaveXPathTree_CheckedChanged);
            // 
            // m_XPathDataGridView
            // 
            this.m_XPathDataGridView.AllowUserToAddRows = false;
            this.m_XPathDataGridView.AllowUserToDeleteRows = false;
            this.m_XPathDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.m_XPathDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.XPath});
            this.m_XPathDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_XPathDataGridView.Location = new System.Drawing.Point(0, 0);
            this.m_XPathDataGridView.Name = "m_XPathDataGridView";
            this.m_XPathDataGridView.ReadOnly = true;
            this.m_XPathDataGridView.RowTemplate.Height = 23;
            this.m_XPathDataGridView.Size = new System.Drawing.Size(521, 132);
            this.m_XPathDataGridView.TabIndex = 0;
            // 
            // XPath
            // 
            this.XPath.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.XPath.HeaderText = "XPath路径";
            this.XPath.Name = "XPath";
            this.XPath.ReadOnly = true;
            // 
            // m_VisitXPathBtn
            // 
            this.m_VisitXPathBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_VisitXPathBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.m_VisitXPathBtn.Image = global::FKAutoBrowserTest.Properties.Resources.Find;
            this.m_VisitXPathBtn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.m_VisitXPathBtn.Location = new System.Drawing.Point(214, 3);
            this.m_VisitXPathBtn.Name = "m_VisitXPathBtn";
            this.m_VisitXPathBtn.Size = new System.Drawing.Size(70, 62);
            this.m_VisitXPathBtn.TabIndex = 3;
            this.m_VisitXPathBtn.Text = "访问XPath";
            this.m_VisitXPathBtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.m_VisitXPathBtn.UseVisualStyleBackColor = false;
            this.m_VisitXPathBtn.Click += new System.EventHandler(this.m_VisitXPathBtn_Click);
            // 
            // m_XPathRichBox
            // 
            this.m_XPathRichBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_XPathRichBox.Location = new System.Drawing.Point(9, 71);
            this.m_XPathRichBox.Name = "m_XPathRichBox";
            this.m_XPathRichBox.Size = new System.Drawing.Size(270, 55);
            this.m_XPathRichBox.TabIndex = 2;
            this.m_XPathRichBox.Text = "";
            // 
            // m_GetXPathBtn
            // 
            this.m_GetXPathBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_GetXPathBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.m_GetXPathBtn.Image = global::FKAutoBrowserTest.Properties.Resources.Get;
            this.m_GetXPathBtn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.m_GetXPathBtn.Location = new System.Drawing.Point(138, 3);
            this.m_GetXPathBtn.Name = "m_GetXPathBtn";
            this.m_GetXPathBtn.Size = new System.Drawing.Size(70, 62);
            this.m_GetXPathBtn.TabIndex = 1;
            this.m_GetXPathBtn.Text = "获取XPath";
            this.m_GetXPathBtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.m_GetXPathBtn.UseVisualStyleBackColor = false;
            this.m_GetXPathBtn.Click += new System.EventHandler(this.m_GetXPathBtn_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(812, 132);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "其他";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1272, 741);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FK自动浏览器测试窗口";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.m_MainTabContorl.ResumeLayout(false);
            this.m_ScriptControlTab.ResumeLayout(false);
            this.m_ScriptTabContorl.ResumeLayout(false);
            this.m_ScriptEditTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_ScriptEditor)).EndInit();
            this.m_ScriptLogTabPage.ResumeLayout(false);
            this.m_ScriptHelpTabPage.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.m_WebBrowserContainer.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.m_IEHelperTabControl.ResumeLayout(false);
            this.m_PostGetTabPage.ResumeLayout(false);
            this.m_WebSourcePage.ResumeLayout(false);
            this.m_XPathPage.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_XPathDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl m_MainTabContorl;
        private System.Windows.Forms.TabPage m_ScriptControlTab;
        private System.Windows.Forms.TabPage m_OthersControlTab;
        private FKAutoBrowser.FKIEWebDriver m_FKIEWebBrowser;
        private System.Windows.Forms.Button m_OpenScriptBtn;
        private System.Windows.Forms.Button m_SaveScriptBtn;
        private System.Windows.Forms.TabControl m_ScriptTabContorl;
        private System.Windows.Forms.TabPage m_ScriptEditTabPage;
        private FastColoredTextBoxNS.FastColoredTextBox m_ScriptEditor;
        private System.Windows.Forms.TabPage m_ScriptLogTabPage;
        private System.Windows.Forms.TabPage m_ScriptHelpTabPage;
        private System.Windows.Forms.Button m_StopScriptBtn;
        private System.Windows.Forms.Button m_PauseScriptBtn;
        private System.Windows.Forms.Button m_RunScriptBtn;
        private System.Windows.Forms.Panel m_WebBrowserContainer;
        private System.Windows.Forms.RichTextBox m_LogRichBox;
        private System.Windows.Forms.RichTextBox m_HelpRichBox;
        private System.Windows.Forms.ProgressBar m_ScriptProcessBar;
        private System.Windows.Forms.TextBox m_WebUrlEdit;
        private System.Windows.Forms.Button m_HideWebPanelBtn;
        private System.Windows.Forms.Button m_HideControlPanelBtn;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TabControl m_IEHelperTabControl;
        private System.Windows.Forms.TabPage m_PostGetTabPage;
        private System.Windows.Forms.ListBox m_PostGetListBox;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button m_GetXPathBtn;
        private System.Windows.Forms.TabPage m_WebSourcePage;
        private System.Windows.Forms.RichTextBox m_WebSourceRichTextBox;
        private System.Windows.Forms.TabPage m_XPathPage;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.DataGridView m_XPathDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn XPath;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件FToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关于AToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关于本软件F12ToolStripMenuItem;
        private System.Windows.Forms.RichTextBox m_XPathRichBox;
        private System.Windows.Forms.ProgressBar m_WebLoadingPB;
        private System.Windows.Forms.Button m_VisitXPathBtn;
        private System.Windows.Forms.CheckBox m_bIsSaveXPathTree;
    }
}

