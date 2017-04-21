//---------------------------------------------------------------
// author: FreeKnight
// date: 2017-3-30
//---------------------------------------------------------------
#define DevelopModel    // 开发者模式
//---------------------------------------------------------------
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using FastColoredTextBoxNS;
using System.Text;
using FKAutoBrowser;
//---------------------------------------------------------------
namespace FKAutoBrowserTest
{
    public partial class MainForm : Form
    {
        #region 类成员变量
        /// <summary>
        /// 测试线程
        /// The thread the test will run on
        /// </summary>
        private Thread m_TestThread;
        /// <summary>
        /// 智能提示面板
        /// </summary>
        private FastColoredTextBoxNS.AutocompleteMenu m_ScriptAutoPopupMenu;
        /// <summary>
        /// 是否控制面板可见
        /// </summary>
        private bool m_IsControlWindowShow = true;
        /// <summary>
        /// 是否网页可见
        /// </summary>
        private bool m_IsWebWindowShow = true;
        /// <summary>
        /// 之前的Form大小，缓存下来以便恢复
        /// </summary>
        private Size m_oldFormSize;
        /// <summary>
        /// 是否当前处于获取XPath状态
        /// </summary>
        private bool m_bIsGetXPath = false;
        #endregion

        #region 常量
        /// <summary>
        /// 美元符号颜色
        /// The style for highlighting the dollar
        /// </summary>
        TextStyle dollarStyle = new TextStyle(Brushes.Gray, null, FontStyle.Regular);

        /// <summary>
        /// 字符串颜色
        /// The style for highlighting strings
        /// </summary>
        TextStyle stringStyle = new TextStyle(Brushes.Brown, null, FontStyle.Bold);

        /// <summary>
        /// 链接颜色
        /// The style for highlighting links
        /// </summary>
        TextStyle linkStyle = new TextStyle(Brushes.Blue, null, FontStyle.Italic);

        /// <summary>
        /// 注释颜色
        /// The style for highlighting comments
        /// </summary>
        TextStyle commentStyle = new TextStyle(Brushes.Green, null, FontStyle.Regular);

        /// <summary>
        /// 数字颜色
        /// The style for highlighting numbers
        /// </summary>
        TextStyle numberStyle = new TextStyle(Brushes.Magenta, null, FontStyle.Regular);

        /// <summary>
        /// 替代符颜色
        /// The style for place holder keys
        /// </summary>
        TextStyle placeHolderStyle = new TextStyle(Brushes.Pink, null, FontStyle.Underline);
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            // 设置部分默认属性
            m_PauseScriptBtn.Enabled = false;
            m_StopScriptBtn.Enabled = false;
            m_ScriptProcessBar.Enabled = false;
            m_XPathDataGridView.AutoGenerateColumns = false;

            // 加载帮助文件
            try
            {
                m_HelpRichBox.LoadFile(File.OpenRead("help.rtf"), RichTextBoxStreamType.RichText);
            }
            catch { m_HelpRichBox.AppendText("无法加载 Help.rtf 文件，请确保文件在本目录下... "); }

            // 初始化编辑器属性
            InitScriptEditor();
        }
        /// <summary>
        /// Form加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.splitContainer2.Panel2Collapsed = true;
            this.splitContainer2.Panel2.Hide();
            this.m_ScriptEditor.ImeMode = ImeMode.On;
            this.m_ScriptControlTab.Parent = null;
#if DevelopModel
            m_FKIEWebBrowser.RequestRecorderList = m_PostGetListBox;
            m_FKIEWebBrowser.XPathInfoShowPanel = m_XPathRichBox;
            m_FKIEWebBrowser.PageSourcePanel = m_WebSourceRichTextBox;
            m_FKIEWebBrowser.XPathDateGridView = m_XPathDataGridView;
            this.splitContainer2.Panel2Collapsed = false;
            this.splitContainer2.Panel2.Show();
            this.m_ScriptControlTab.Parent = this.m_MainTabContorl;
#endif
        }

        /// <summary>
        /// 初始化编辑器的部分属性
        /// Init the Scintilla editor
        /// </summary>
        private void InitScriptEditor()
        {
            m_ScriptAutoPopupMenu = new FastColoredTextBoxNS.AutocompleteMenu(m_ScriptEditor);
            m_ScriptAutoPopupMenu.MinFragmentLength = 1;

            m_ScriptAutoPopupMenu.AppearInterval = 150;
            m_ScriptAutoPopupMenu.ToolTipDuration = 1000 * 20; // 20秒
            m_ScriptAutoPopupMenu.Items.MaximumSize = new System.Drawing.Size(300, 300);
            m_ScriptAutoPopupMenu.Width = 300;
            m_ScriptAutoPopupMenu.Items.SetAutocompleteItems(GetRegisteredActions());

            // 设置默认字符
            m_ScriptEditor.Text = "$.browser(\"ie\")" + Environment.NewLine + "$.open(\"http://www.google.com\")";

            // 设置字体
            m_ScriptEditor.Font = new Font(FontFamily.GenericSerif, 10);
        }
        /// <summary>
        /// 获取已注册的动作名
        /// Gets the registered action names
        /// </summary>
        /// <returns></returns>
        private List<AutocompleteItem> GetRegisteredActions()
        {
            List<AutocompleteItem> items = new List<AutocompleteItem>();
            FKAutoUnit autoUnit = new FKAutoUnit();
            foreach (KeyValuePair<string, FKActionType> kvp in autoUnit.ActionTypes)
            {
                StringBuilder parameterNames = new StringBuilder();
                StringBuilder description = new StringBuilder();
                description.Append(kvp.Value.Description);
                if (kvp.Value.Parameters != null && kvp.Value.Parameters.Any())
                {
                    int x = 0;
                    foreach (FKActionParameter parameter in kvp.Value.Parameters)
                    {
                        if (x > 0)
                        {
                            parameterNames.Append(", ");
                        }
                        if (parameter.Type == typeof(string))
                        {
                            parameterNames.Append("\"" + parameter.Name + "\"");
                        }
                        else
                        {
                            parameterNames.Append(parameter.Name);
                        }
                        description.Append(Environment.NewLine);
                        description.AppendFormat("- {0} ({1}, {2})",
                            parameter.Name,
                            parameter.Type.Name,
                            parameter.IsOptional ? "可选参数" : "必需参数");
                        x++;
                    }
                }
                items.Add(new AutocompleteItem(
                    string.Format(".{0}({1})", kvp.Key, parameterNames.ToString()),
                    0,
                    kvp.Key + kvp.Value.ParameterString,
                    kvp.Value.Name,
                    description.ToString()));
            }
            return items.OrderBy(i => i.MenuText).ToList();
        }

        #region 辅助函数
        /// <summary>
        /// 检查是否位置是否是超链接
        /// Check if the given place in the editor is in a hyperlink
        /// </summary>
        /// <param name="place"></param>
        /// <returns></returns>
        private bool CharIsHyperlinkInEditor(Place place)
        {
            var mask = m_ScriptEditor.GetStyleIndexMask(new Style[] { linkStyle });
            if (place.iChar < m_ScriptEditor.GetLineLength(place.iLine))
            {
                if ((m_ScriptEditor[place].style & mask) != 0)
                {
                    return true;
                }
            }

            return false;
        }
        /// <summary>
        /// 更新脚本执行进度
        /// When the test thread reports on progress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="progressChangedEventArgs"></param>
        private void TestProgress(object sender, ProgressChangedEventArgs progressChangedEventArgs)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<object, ProgressChangedEventArgs>(TestProgress), new object[] { sender, progressChangedEventArgs });
                return;
            }

            if (progressChangedEventArgs.ProgressPercentage == 100)
            {
                m_StopScriptBtn.Enabled = false;
                m_PauseScriptBtn.Enabled = false;
                m_RunScriptBtn.Enabled = true;
                m_ScriptProcessBar.Value = 0;
                m_ScriptProcessBar.Enabled = false;
                return;
            }
            m_ScriptProcessBar.Value = progressChangedEventArgs.ProgressPercentage;
        }
        /// <summary>
        /// 输出单次Action运行结果
        /// Writes out the result to the GUI
        /// </summary>
        /// <param name="functionName"></param>
        /// <param name="parameters"></param>
        /// <param name="success"></param>
        /// <param name="message"></param>
        private void WriteResult(string functionName, List<object> parameters, bool success, string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string, List<object>, bool, string>(WriteResult), new object[] { functionName, parameters, success, message });
                return;
            }

            string callingFunction = "$." + functionName + "(";
            foreach (object parameter in parameters)
            {
                callingFunction += parameter is string ? "\"" + parameter + "\", " : parameter + ", ";
            }
            callingFunction = callingFunction.Trim().Trim(',');
            callingFunction += ")";

            m_LogRichBox.SelectionColor = Color.Gray;
            m_LogRichBox.AppendText(callingFunction + Environment.NewLine);

            m_LogRichBox.SelectionColor = success ? Color.Green : Color.Red;
            m_LogRichBox.AppendText(message + Environment.NewLine);
        }
        #endregion

        #region 控件事件
        /// <summary>
        /// 脚本编辑面板 鼠标移动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_ScriptEditor_MouseMove(object sender, MouseEventArgs e)
        {
            var p = m_ScriptEditor.PointToPlace(e.Location);
            if (CharIsHyperlinkInEditor(p))
            {
                m_ScriptEditor.Cursor = Cursors.Hand;
            }
            else
            {
                m_ScriptEditor.Cursor = Cursors.IBeam;
            }
        }
        /// <summary>
        /// 脚本编辑面板 鼠标按下事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_ScriptEditor_MouseDown(object sender, MouseEventArgs e)
        {
            var p = m_ScriptEditor.PointToPlace(e.Location);
            if (CharIsHyperlinkInEditor(p))
            {
                string line = m_ScriptEditor.GetRange(p, p).GetFragment(@"[\S]").Text;
                Match match = Regex.Match(line, @"(http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?", RegexOptions.IgnoreCase);
                if (!match.Success)
                {
                    return;
                }
                string url = match.Value;
                System.Diagnostics.Process.Start(url);
            }
        }
        /// <summary>
        /// 脚本编辑面板 文字更变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_ScriptEditor_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            // 默认样式
            //m_ScriptEditor.SyntaxHighlighter.InitStyleSchema(Language.JS);
            //m_ScriptEditor.SyntaxHighlighter.JScriptSyntaxHighlight(m_ScriptEditor.Range);

            e.ChangedRange.ClearStyle(commentStyle, dollarStyle, stringStyle, numberStyle, linkStyle, placeHolderStyle);

            // 注释高亮
            e.ChangedRange.SetStyle(commentStyle, new Regex(@"^[\s]*//.*$", RegexOptions.Multiline | RegexOptions.Compiled));
            // 函数标头
            e.ChangedRange.SetStyle(dollarStyle, @"\$\.|\)\.");
            // 替代符高亮
            e.ChangedRange.SetStyle(placeHolderStyle, @"\#([^\]]*)#");
            // 超链接高亮
            e.ChangedRange.SetStyle(linkStyle, @"['""](http|https):\/\/([^""']+)", RegexOptions.Compiled);
            // 字符串高亮
            e.ChangedRange.SetStyle(stringStyle, new Regex(@"['""][^'""]*['""]", RegexOptions.Compiled));
            // 数字高亮
            e.ChangedRange.SetStyle(numberStyle, new Regex(@"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b", RegexOptions.Compiled));

            // 当前行背景色
            m_ScriptEditor.CurrentLineColor = Color.Yellow;
        }
        /// <summary>
        /// 打开脚本 按钮点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_OpenScriptBtn_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "FKScript files (*.fks)|*.fks|All files|*.*";
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var sr = new StreamReader(openFileDialog1.FileName);
                var text = sr.ReadToEnd();
                string normalized = Regex.Replace(text, @"\r\n|\n\r|\n|\r", "\r\n");
                m_ScriptEditor.Text = normalized;
                sr.Close();
            }
        }
        /// <summary>
        /// 保存脚本 按钮点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_SaveScriptBtn_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "FKScript files (*.fks)|*.fks|All files|*.*";
            saveFileDialog1.AddExtension = false;
            saveFileDialog1.DefaultExt = ".fks";
            DialogResult result = saveFileDialog1.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                string filename = saveFileDialog1.FileName;
                if (!filename.ToLower().EndsWith(".fks"))
                {
                    filename += ".fks";
                }
                File.WriteAllText(filename, m_ScriptEditor.Text);
            }
        }
        /// <summary>
        /// 运行脚本 按钮点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_RunScriptBtn_Click(object sender, EventArgs e)
        {
            m_RunScriptBtn.Enabled = false;
            m_PauseScriptBtn.Enabled = true;
            m_StopScriptBtn.Enabled = true;
            m_ScriptProcessBar.Enabled = true;

            if (m_TestThread != null && m_TestThread.ThreadState == ThreadState.Suspended)
            {
                m_TestThread.Resume();
                return;
            }

            m_LogRichBox.Text = "";
            m_ScriptTabContorl.SelectTab(1);

            m_TestThread = new Thread(delegate ()
            {
                var autoUnit = new FKAutoUnit(m_ScriptEditor.Text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None));
                autoUnit.SetExtandIEWebDirver(m_FKIEWebBrowser);
                autoUnit.TestProgress += TestProgress;
                try
                {
                    autoUnit.Run(null, new FKAutoUnit.ActionResultDelegate(WriteResult));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            );
            m_TestThread.Name = "脚本执行线程";
            m_TestThread.SetApartmentState(ApartmentState.STA);
            m_TestThread.Start();
        }
        /// <summary>
        /// 暂停脚本 按钮点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_PauseScriptBtn_Click(object sender, EventArgs e)
        {
            m_PauseScriptBtn.Enabled = false;
            m_RunScriptBtn.Enabled = true;
            if (m_TestThread != null && m_TestThread.ThreadState == ThreadState.Running)
            {
                m_TestThread.Suspend();
            }
        }
        /// <summary>
        /// 停止脚本 按钮按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_StopScriptBtn_Click(object sender, EventArgs e)
        {
            m_StopScriptBtn.Enabled = false;
            m_PauseScriptBtn.Enabled = false;
            m_RunScriptBtn.Enabled = true;
            m_ScriptProcessBar.Value = 0;
            m_ScriptProcessBar.Enabled = false;
            if (m_TestThread != null)
            {
                m_TestThread.Abort();
            }
        }
        /// <summary>
        /// 隐藏控制面板  按钮按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_HideControlPanelBtn_Click(object sender, EventArgs e)
        {
            if (m_IsControlWindowShow)
            {
                this.splitContainer1.Panel1Collapsed = true;
                this.splitContainer1.Panel1.Hide();
            }
            else
            {
                this.splitContainer1.Panel1Collapsed = false;
                this.splitContainer1.Panel1.Show();
            }

            // 修改值
            m_IsControlWindowShow = !m_IsControlWindowShow;
        }
        /// <summary>
        /// 隐藏网页面板 按钮按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_HideWebPanelBtn_Click(object sender, EventArgs e)
        {
            if (m_IsWebWindowShow)
            {
                this.splitContainer1.Panel2Collapsed = true;
                this.splitContainer1.Panel2.Hide();
                // 保存旧框架大小以便恢复
                m_oldFormSize = this.Size;
                // 调整窗口大小
                this.Size = new Size(this.Width - splitContainer1.Panel2.ClientSize.Width + 14, this.Height);
            }
            else
            {
                this.splitContainer1.Panel2Collapsed = false;
                this.splitContainer1.Panel2.Show();
                // 调整窗口大小
                this.Size = m_oldFormSize;
            }

            // 修改值
            m_IsWebWindowShow = !m_IsWebWindowShow;
        }
        /// <summary>
        /// 日志 文本发生改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_LogRichBox_TextChanged(object sender, EventArgs e)
        {
            // 日志自动滚动最后一行
            m_LogRichBox.SelectionStart = m_LogRichBox.Text.Length;
            m_LogRichBox.ScrollToCaret();
        }
        /// <summary>
        /// 地址栏按键消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_WebUrlEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                string strUrl = m_WebUrlEdit.Text;
                if (!strUrl.StartsWith("http") && !strUrl.StartsWith("https") && !strUrl.StartsWith("/"))
                {
                    strUrl = "http://" + strUrl;
                }

                m_FKIEWebBrowser.Navigate(new Uri(strUrl));
            }
        }
        /// <summary>
        /// 按下获取XPath按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_GetXPathBtn_Click(object sender, EventArgs e)
        {
            m_bIsGetXPath = !m_bIsGetXPath;

            // 通知IE
            m_FKIEWebBrowser.IsGetXPathMode = m_bIsGetXPath;
        }
        /// <summary>
        /// 页面加载完毕事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_FKIEWebBrowser_DocumentCompleted(object sender, 
            WebBrowserDocumentCompletedEventArgs e)
        {
            var br = sender as FKIEWebDriver;
            if(br.Url == e.Url)
            {
                m_WebUrlEdit.Text = e.Url.ToString();
                m_FKIEWebBrowser.IsDocumentCompleted = true;
                Console.WriteLine("打开页面完成：" + e.Url);
            }
        }
        /// <summary>
        /// 退出 按钮按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var msgExit = MessageBox.Show("您确定退出本软件吗?", "FK自动浏览器测试窗口", MessageBoxButtons.YesNo);
            if (msgExit == DialogResult.Yes)
            {
                Program.Close();
            }
        }
        /// <summary>
        /// 关于 按钮按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 关于本软件F12ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.ShowAboutBox();
        }
        /// <summary>
        /// 网页加载进度发生更变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_FKIEWebBrowser_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            if (e.CurrentProgress < 0 || e.CurrentProgress > 10000)
                return;
            m_WebLoadingPB.Value = (int)e.CurrentProgress;
        }
        /// <summary>
        /// 点击访问XPath按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_VisitXPathBtn_Click(object sender, EventArgs e)
        {
            string strXPath = m_XPathRichBox.Text;
            m_FKIEWebBrowser.VisitXPath(strXPath);
        }
        /// <summary>
        /// 记录XPath树 复选框发生更变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_bIsSaveXPathTree_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            m_FKIEWebBrowser.IsNeedRecordXPath = cb.Checked;
        }
        #endregion
    }
}
