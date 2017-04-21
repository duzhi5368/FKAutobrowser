//---------------------------------------------------------------
// author: FreeKnight
// date: 2017-3-30
//---------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using HtmlAgilityPack;
using AgilityHtmlDocument = HtmlAgilityPack.HtmlDocument;
using SHDocVw;
using System.Threading;
using System.Drawing;
using mshtml;
using Microsoft.Win32;
using System.Text.RegularExpressions;
//---------------------------------------------------------------
namespace FKAutoBrowser
{
    /// <summary>
    /// C# 的WebBrowser继承实现
    /// Derive the system WebBrowser
    /// </summary>
    public class FKIEWebDriver : System.Windows.Forms.WebBrowser
    {
        #region 主线程传输的数据同步
        public ListBox RequestRecorderList { set; get; }    // HTTP请求信息输出列表
        public RichTextBox XPathInfoShowPanel { set; get; } // 设置XPath路径显示面板
        public RichTextBox PageSourcePanel { set; get; }    // 设置网页源码显示面板
        public DataGridView XPathDateGridView { set; get; } // 网页XPath列表面板
        public bool IsGetXPathMode { set; get; }            // 是否处于取得XPath模式
        public bool IsDocumentCompleted { get; set; }       // 是否当前页面已加载完成
        public bool IsNeedRecordXPath { get; set; }         // 是都需要记录当前XPath树
        #endregion

        #region 变量
        private int m_OpenUrlTimeoutCount = 0;              // 打开网页的超时检查计数
        private FKAgilXPath m_AgilXPath;                    // XPath管理器
        private bool m_bIsShowXPathMode = false;            // 是否是在显示节点
        private HtmlElement m_LastMovedElem = null;         // 上一次移动过的元素
        private string m_LastBackgroundColor;               // 上一次移动过的元素的背景色
        #endregion

        #region 常量
        private const int MAX_OPENURL_TICK_COUNT = 600;    // 限制网页600 * 50 / 1000 = 30秒内必须打开
        #endregion

        #region 控件函数
        public FKIEWebDriver()
        {
            // 重置浏览器版本
            ResetIEVersion();
            // 设置默认配置
            SetDefaultSetting();
            // 记录网页源码
            RecordWebSource();
            // 记录全部HTTP请求
            RecordRequestInfo();
            // 记录鼠标点击行为
            ResigteMouseClickEvent();
        }
        /// <summary>
        /// 重置本浏览器IE内核版本
        /// </summary>
        public void ResetIEVersion()
        {
            // 设置启动为本机最高版本IE
            int nLocalIEVersion = this.Version.Major;
            int nValue = 8888;
            switch (nLocalIEVersion)
            {
                case 8:
                    nValue = 8888;  // 8000
                    break;
                case 7:
                    nValue = 7000;
                    break;
                case 9:
                    nValue = 9999;
                    break;
                case 10:
                    nValue = 10000; // 10001
                    break;
                case 11:
                    nValue = 11000; // 11001
                    break;
                default:
                    nValue = 8888;
                    break;
            }
            // 注册本APP到注册表
            //string strAppName = Process.GetCurrentProcess().ProcessName + ".exe";
            string strAppName = System.IO.Path.GetFileName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            RegistryKey key = Registry.LocalMachine.OpenSubKey("HKEY_CURRENT_USER\\Software\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_BROWSER_EMULATION", 
                RegistryKeyPermissionCheck.ReadWriteSubTree);
            if(key != null)
            {
                try
                {
                    key.SetValue(strAppName, nValue, RegistryValueKind.DWord);
                    key.Close();
                }
                catch { }
            }
        }
        /// <summary>
        /// 设置WebBrowser默认配置
        /// </summary>
        public void SetDefaultSetting()
        {
            // 打开默认网页
            this.Navigate("http://www.google.com");
            // 屏蔽全部错误 JS弹出框等
            this.ScriptErrorsSuppressed = true;
            // 屏蔽右键菜单
            this.IsWebBrowserContextMenuEnabled = false;
            // 禁止快捷键
            //this.WebBrowserShortcutsEnabled = false;
            // 禁止拖放
            this.AllowWebBrowserDrop = false;
        }
        /// <summary>
        /// 按键消息筛选，对快捷键进行筛选
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public void RecordWebSource()
        {
            this.DocumentCompleted -= FKOnDocumentCompleted;
            this.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(FKOnDocumentCompleted);
        }
        public override bool PreProcessMessage(ref Message msg)
        {
            if((Control.ModifierKeys & Keys.Control) == Keys.Control)
            {
                switch (msg.WParam.ToInt32())
                {
                    case (int)Keys.ControlKey:  // 仅按下Ctrl键
                        break;
                    case (int)Keys.C:           // 复制
                        break;
                    case (int)Keys.V:           // 粘贴
                        break;
                    case (int)Keys.D0:          // 恢复zoom
                        break;
                    case (int)Keys.Oemplus:     // 放大zoom
                        break;
                    case (int)Keys.OemMinus:    // 缩小zoom
                        break;
                    case (int)Keys.Add:         // 放大zoom
                        break;
                    case (int)Keys.Subtract:    // 缩小zoom
                        break;
                    default:
                        return false;
                }
            }
            return base.PreProcessMessage(ref msg);
        }
        #endregion

        #region 外部函数
        /// <summary>
        /// 获取 DocumentText/PageSource 网页源码
        /// </summary>
        /// <param name="nFunctionType">获取的三种方式</param>
        /// <returns></returns>
        public string PageSource(int nFunctionType = 2)
        {
            string result = string.Empty;
            if (nFunctionType == 1)
            {
                this.Invoke(new Action(() =>
                {
                    result = DocumentText;
                }));
            }
            else if(nFunctionType == 2)
            {
                this.Invoke(new Action(() =>
                {
                    if (Document != null)
                    {
                        HtmlElementCollection elems = Document.GetElementsByTagName("HTML");
                        HtmlElement elem;
                        if (elems.Count == 1)
                        {
                            elem = elems[0];
                            result = elem.OuterHtml;
                        }
                    }
                }));
            }
            else
            {
                this.Invoke(new Action(() =>
                {
                    var documentRootNodes = Document.GetElementsByTagName("*");
                    result = string.Empty;
                    foreach (HtmlElement node in documentRootNodes)
                    {
                        if (node.Parent == null)
                        {
                            result += node.OuterHtml;
                        }
                    }

                    foreach (HtmlWindow frame in Document.Window.Frames)
                    {
                        var frameRootNodes = frame.Document.GetElementsByTagName("*");
                        var frameHtml = string.Empty;
                        foreach (HtmlElement node in frameRootNodes)
                        {
                            if (node.Parent == null)
                            {
                                frameHtml += node.OuterHtml;
                            }
                        }

                        result += frameHtml;
                    }
                }));
            }
            return result;
        }
        /// <summary>
        /// 获取标题
        /// </summary>
        /// <returns></returns>
        public string GetTitle()
        {
            string result = string.Empty;
            this.Invoke(new Action(() =>
            {
                HtmlElementCollection elems = Document.GetElementsByTagName("title");
                HtmlElement elem;
                if (elems.Count == 1)
                {
                    elem = elems[0];
                    result = elem.InnerText;
                }
                else
                {
                    result = "";
                }
            }));
            return result;
        }
        /// <summary>
        /// 执行一段JS脚本函数
        /// </summary>
        /// <param name="strJs"></param>
        /// <returns></returns>
        public string ExecuteScript(string strJs)
        {
            string result = string.Empty;
            this.Invoke(new Action(() =>
            {
                object[] args = { strJs };
                object returnValue = this.Document.InvokeScript("eval", args);
                if (returnValue != null)
                {
                    result = returnValue.ToString();
                }
                else { result = "";  }
            }));
            return result;
        }
        /// <summary>
        /// 设置CSS样式
        /// </summary>
        /// <param name="strJs"></param>
        /// <returns></returns>
        public bool SetCSSStyle(string strJs)
        {
            bool result = false;
            this.Invoke(new Action(() =>
            {
                mshtml.HTMLDocument myDocument = (mshtml.HTMLDocument)(this.Document.DomDocument);
                if(myDocument.styleSheets.length < 31)  // css样式数量不允许超过31份
                {
                    mshtml.IHTMLStyleSheet css = (mshtml.IHTMLStyleSheet)myDocument.createStyleSheet("", 0);
                    css.cssText = strJs;
                    result = true;
                }
                else
                {
                    result = false;
                }
            }));
            return result;
        }
        /// <summary>
        /// 保存一张图片
        /// </summary>
        /// <param name="strFilename"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public string SaveImage(string strFilename)
        {
            try
            {
                int width, height;
                width = this.ClientRectangle.Width;
                height = this.ClientRectangle.Height;
                using (Bitmap image = new Bitmap(width, height))
                {
                    using (Graphics graphics = Graphics.FromImage(image))
                    {
                        Point p, upperLeftSource, upperLeftDest;
                        p = new Point(0, 0);
                        upperLeftSource = new Point(0, 0);
                        this.Invoke(new Action(() => {
                            upperLeftSource = this.PointToScreen(p);
                        }));
                        upperLeftDest = new Point(0, 0);
                        Size blockRegionSize = ClientRectangle.Size;
                        graphics.CopyFromScreen(upperLeftSource, upperLeftDest, blockRegionSize);
                    }
                    image.Save(strFilename);
                    return "";
                }
            }catch (Exception e)
            {
                return e.ToString();
            }
        }
        /// <summary>
        /// 窗口最大化
        /// </summary>
        public void Maximize()
        {
            this.Invoke(new Action(() =>
            {
                this.Size = this.MaximumSize;
            }));
        }
        /// <summary>
        /// 窗口最小化
        /// </summary>
        public void Minimize()
        {
            this.Invoke(new Action(() =>
            {
                this.Size = new Size(1,1);
            }));
        }
        /// <summary>
        /// 重置浏览器大小
        /// </summary>
        /// <param name="newSize"></param>
        public void MyResize(Size newSize)
        {
            this.Invoke(new Action(() =>
            {
                this.Size = newSize;
            }));
        }
        #endregion

        #region XPath支持
        /*
        /// <summary>
        /// 委托获取DocumentBody
        /// </summary>
        /// <returns></returns>
        public HtmlElement GetDocumentBody()
        {
            HtmlElement strResult = null;
            this.Invoke(new Action(() =>
            {
                strResult = Document.Body;
            }));
            return strResult;
        }
        public HtmlElement FindElementsByXPath(string strXPath)
        {
            HtmlElement htmlElement = null;
            this.Invoke(new Action(() =>
            {
                htmlElement = this.Document.GetElementsByTagName("html")[0];
            }));
            return FindElementsByXPath(strXPath, htmlElement);
        }
        public HtmlElement FindElementsByXPath(string strXPath, HtmlElement htmlElement)
        {
            HtmlElement result = null;
            string currentNode;
            int indexOfElement;
            string lowerXPath = strXPath.ToLower();

                //get string representation of current Tag.
                if (lowerXPath.Substring(1, lowerXPath.Length - 2).Contains('/'))
                    currentNode = lowerXPath.Substring(1, lowerXPath.IndexOf('/', 1) - 1);
                else
                    currentNode = lowerXPath.Substring(1, lowerXPath.Length - 1);
                //gets the depth of current lowerXPath
                int numOfOccurence = Regex.Matches(lowerXPath, "/").Count;

                //gets the children's index
                int.TryParse(Regex.Match(currentNode, @"\d+").Value, out indexOfElement);

                    //if i have to select nth-child ex: /tr[4]
                    if (indexOfElement > 1)
                    {
                        currentNode = currentNode.Substring(0, lowerXPath.IndexOf('[') - 1);
                        //the tag that i want to get
                        if (numOfOccurence == 1 || numOfOccurence == 0)
                        {
                            result = htmlElement.Children[indexOfElement - 1];
                        }
                        //still has some children tags
                        if (numOfOccurence > 1)
                        {
                            int i = 1;
                            //select nth-child
                            foreach (HtmlElement tempElement in htmlElement.Children)
                            {
                                if (tempElement.TagName.ToLower() == currentNode && i == indexOfElement)
                                {
                                    result = FindElementsByXPath(lowerXPath.Substring(lowerXPath.IndexOf('/', 1)), tempElement);
                                    return result;
                                }
                                else if (tempElement.TagName.ToLower() == currentNode && i < indexOfElement)
                                {
                                    i++;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (numOfOccurence == 1 || numOfOccurence == 0)
                        {
                            result = htmlElement.FirstChild;
                        }
                        if (numOfOccurence > 1)
                        {
                            foreach (HtmlElement tempElement in htmlElement.Children)
                            {
                                if (tempElement.TagName.ToLower() == currentNode)
                                {
                                    result = FindElementsByXPath(lowerXPath.Substring(lowerXPath.IndexOf('/', 1)), tempElement);
                                    return result;
                                }
                            }
                        }
                    }
            return result;
        }
        */
        /// <summary>
        /// 查找指定XPath路径的元素
        /// </summary>
        /// <param name="rootXPathQuery"></param>
        /// <returns></returns>
        public IEnumerable<HtmlElement> FindElementsByXPath2(string rootXPathQuery)
        {
            HtmlNode.ElementsFlags.Remove("form");              // 必须有这行，不然Agility的BUG会忽略form

            var htmlDocument = new AgilityHtmlDocument();
            htmlDocument.LoadHtml(PageSource());

            string path = rootXPathQuery.ToLower();
            var documentNode = htmlDocument.DocumentNode;
            if (documentNode != null)
            {
                var nodes = documentNode.SelectNodes(path);     // 找到了符合XPath要求的 AgilityHtmlNode
                if (nodes != null)
                {
                    // 要将 AgilityHtmlNode 转为 HtmlElement 返回才行
                    foreach (HtmlNode node in nodes)
                    {
                        HtmlElement equivalent = FindFromNode2(node);
                        yield return equivalent;
                    }
                }

            }
        }
        struct DocNode
        {
            public string Name;
            public int Pos;
        }
        /// <summary>
        /// 通过 AgilityHtmlNode 查找其对应的 HtmlElement 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private HtmlElement FindFromNode2(HtmlNode node)
        {
            HtmlElement html = null;
            this.Invoke(new Action(() =>
            {
                html = this.Document.GetElementsByTagName("html")[0];
            }));

            var pattern = @"/(.*?)\[(.*?)\]"; // 查找正则，类似 div[1] 这种格式
            var matches = Regex.Matches(node.XPath, pattern);
            List<DocNode> PathToNode = new List<DocNode>();
            foreach (Match m in matches) // 将整个XPath进行拆分，将 /HTML[1]/BODY[1]/DIV[2] 拆分为{html, 0},{body, 0},{div, 1}的数组
            {
                DocNode n = new DocNode();
                n.Name = m.Groups[1].Value;
                n.Pos = Convert.ToInt32(m.Groups[2].Value) - 1;
                PathToNode.Add(n);
            }

            // 开始对拆解后的数组进行查找
            HtmlElement elem = null; 
            if (PathToNode.Count > 0)
            {
                elem = html; // 从Document的<HTML>级别开始向下找
                foreach (DocNode n in PathToNode)
                {
                    if (elem == null)   // 如果为null，说明父节点已经找不到，子节点没有查找必要了
                        break;
                    // 循环递归向下查找
                    elem = GetChild(elem, n);
                }
            }
            return elem;    // 返回结果
        }
         private HtmlElement GetChild(HtmlElement el, DocNode node)
        {
            if (el == null)
                return null;
            if(node.Name.Equals("html", StringComparison.OrdinalIgnoreCase)){       // 如果是Html则不查，直接返回
                return el;
            }

            // 根据元素的索引编号和TagName进行查找
            int childPos = 0;
            HtmlElement result = null;
            this.Invoke(new Action(() =>
            {
                foreach (HtmlElement child in el.Children)
                {
                    if (child.TagName.Equals(node.Name,
                       StringComparison.OrdinalIgnoreCase))
                    {
                        if (childPos == node.Pos)
                        {
                            result = child;
                            return;
                        }
                        childPos++;
                    }
                }
            }));
            // 最终返回找到的子节点结果
            return result;
        }
        /*
        private HtmlElement FindFromNode(HtmlNode node)
        {
            var parent = node.ParentNode;
            if (parent == null)
            {
                parent = node;
            }
            int parentOffset = 0;
            HtmlElement[] childElementsWithSameType = null;

            this.Invoke(new Action(() =>
            {
                List<HtmlNode> childNodesWithSameType = null;
                childNodesWithSameType = parent.ChildNodes.Where(n => string.Equals(n.Name, node.Name, StringComparison.OrdinalIgnoreCase))
                    .ToList();
                parentOffset = childNodesWithSameType.IndexOf(node);

                HtmlElement parentResult;
                if (string.Equals(parent.Name, "BODY", StringComparison.OrdinalIgnoreCase))
                {
                    parentResult = GetDocumentBody();
                }
                else
                {
                    parentResult = FindFromNode(parent);
                }

                Debug.Assert(parentResult != null, "parentResult != null");

                childElementsWithSameType = parentResult.All.Cast<HtmlElement>()
                    .Where(e => string.Equals(e.TagName, node.Name, StringComparison.OrdinalIgnoreCase))
                    .ToArray();
            }));
            return childElementsWithSameType[parentOffset];
        }
        */
        /// <summary>
        /// 网页加载完成事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FKOnDocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {          
            // 显示源码
            string html = this.Document.Body.Parent.OuterHtml;
            if (PageSourcePanel != null)
                PageSourcePanel.Text = html;


            if ((!m_bIsShowXPathMode) && IsNeedRecordXPath)
            {
                // 开启form显示
                HtmlNode.ElementsFlags.Remove("form");

                // 更新显示XPath树
                m_AgilXPath = FKAgilXPath.LoadFromString(html);
                m_AgilXPath.findText("");
                
                XPathDateGridView.CellDoubleClick -= XPathDateGridView_CellDoubleClick;
                XPathDateGridView.CellDoubleClick += new DataGridViewCellEventHandler(XPathDateGridView_CellDoubleClick);
                XPathDateGridView.CellClick -= XPathDateGridView_CellClick;
                XPathDateGridView.CellClick += new DataGridViewCellEventHandler(XPathDateGridView_CellClick);
                
                XPathDateGridView.DataSource = m_AgilXPath.Xpaths.Select(x => new { Value = x }).ToList();
                XPathDateGridView.Columns["XPath"].DataPropertyName = "Value";
                foreach (int i in m_AgilXPath.FoundNodes)
                {
                    XPathDateGridView.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                }
            }

            m_bIsShowXPathMode = false;
        }
        /// <summary>
        /// 双击 单元格控件 事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XPathDateGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string path = XPathDateGridView[0, e.RowIndex].Value.ToString();
            VisitXPath(path);
        }
        /// <summary>
        /// 单击  单元格控件 事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XPathDateGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string path = XPathDateGridView[0, e.RowIndex].Value.ToString();

            if (XPathInfoShowPanel != null)
            {
                XPathInfoShowPanel.Clear();
                XPathInfoShowPanel.AppendText(path);
            }
        }
        /// <summary>
        /// 访问指定XPath以内元素
        /// </summary>
        public void VisitXPath(string strXPath)
        {
            // 开启form显示
            HtmlNode.ElementsFlags.Remove("form");

            if (m_AgilXPath == null)
            {
                string html = this.Document.Body.Parent.OuterHtml;
                m_AgilXPath = FKAgilXPath.LoadFromString(html);
            }
            HtmlNode node = m_AgilXPath[strXPath];
            if (node == null)
                return;

            this.DocumentText = node.OuterHtml;
            m_bIsShowXPathMode = true;
        }
        #endregion

        #region POST/GET获取支持
        /// <summary>
        /// 记录Get/Post请求数据
        /// </summary>
        private void RecordRequestInfo()
        {
            // 需要添加引用：Windows\System32\Shdocvw.dll
            SHDocVw.WebBrowser wb = (SHDocVw.WebBrowser)this.ActiveXInstance;
            wb.BeforeNavigate2 += new DWebBrowserEvents2_BeforeNavigate2EventHandler(
                (object pDisp,
                 ref object URL,
                 ref object Flags,
                 ref object TargetFrameName,
                 ref object PostData,
                 ref object Headers,
                 ref bool Cancel) =>
                {
                    if (PostData == null)
                    {
                        if (RequestRecorderList != null)
                        {
                            RequestRecorderList.Items.Add("[GET] " + URL);
                        }
                    }
                    else
                    {
                        string PostDATAStr = System.Text.Encoding.ASCII.GetString((Byte[])PostData);
                        if (RequestRecorderList != null)
                        {
                            RequestRecorderList.Items.Add("[POST] " + URL);
                            RequestRecorderList.Items.Add("[POST DATA] " + PostDATAStr);
                        }
                    }
                });
        }
        #endregion

        #region 鼠标点击事件处理
        private void ResigteMouseClickEvent()
        {
            // 注册鼠标点击行为
            this.DocumentCompleted += (s, x) =>
            {
                this.Document.Click -= OnClickDocument;
                this.Document.Click += OnClickDocument;
                this.Document.MouseMove -= OnMouseMoveOnDocument;
                this.Document.MouseMove += OnMouseMoveOnDocument;
            };
        }
        /// <summary>
        ///  鼠标左键点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClickDocument(object sender, HtmlElementEventArgs e)
        {
            // 非获取XPath模式
            if (!IsGetXPathMode) { return; }

            // 开启form显示
            HtmlNode.ElementsFlags.Remove("form");

            string strXPath = "";
            var element = this.Document.GetElementFromPoint(e.ClientMousePosition);

            var savedId = element.Id;
            var uniqueId = Guid.NewGuid().ToString();
            element.Id = uniqueId;
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(element.Document.GetElementsByTagName("html")[0].OuterHtml);
            element.Id = savedId;

            var node = doc.GetElementbyId(uniqueId);
            strXPath = node.XPath;

            // 显示当前XPath
            if (XPathInfoShowPanel != null)
            {
                string strStyle = element.Style;
                element.Style = strStyle + "; background-color: #ff8; border-style: 2px solid yellow;";
                XPathInfoShowPanel.Clear();
                XPathInfoShowPanel.AppendText(strXPath);
            }

            // 点击一次，取消获取模式
            IsGetXPathMode = false;
        }
        /// <summary>
        /// 鼠标移动
        /// </summary>
        private void OnMouseMoveOnDocument(object sender, HtmlElementEventArgs e) {
            // 非获取XPath模式
            if (!IsGetXPathMode) {
                return;
            }

            // 获取当前选择元素
            HtmlElement element = this.Document.GetElementFromPoint(e.ClientMousePosition);
            if(element == m_LastMovedElem) {
                return;
            }

            // 恢复先前元素
            if (m_LastMovedElem != null)
            {
                string strLastStyle = m_LastMovedElem.Style;
                strLastStyle.Replace("; background: rgba(0, 0, 0, 0.5);", "");
                if (m_LastBackgroundColor == null || m_LastBackgroundColor == "")
                {
                    m_LastMovedElem.Style = strLastStyle + "; background: rgba(0, 0, 0, 0);";
                }
                else
                {
                    m_LastMovedElem.Style = strLastStyle + "; background: " + m_LastBackgroundColor + ";";
                }
            }

            // 记录
            m_LastMovedElem = element;
            m_LastBackgroundColor = element.GetAttribute("background");

            // 设置当前元素
            string strStyle = element.Style;
            element.Style = strStyle + "; background: rgba(0, 0, 0, 0.5);";
        }
        #endregion

        #region 跨线程启动Url
        public void SafetyOpenUrl(Uri uri)
        {
            try
            {
                this.Navigate(uri);
            }
            catch { return; }
            System.Threading.Timer timer = new System.Threading.Timer(OpenUrlTimeoutTicker, null, 0, 50);
            IsDocumentCompleted = false;
            while (!IsDocumentCompleted)
            {
                Thread.Sleep(50);
                Application.DoEvents();
            }
            // 销毁定时器
            timer.Dispose();
            // 清除计数
            m_OpenUrlTimeoutCount = 0;
        }
        void OpenUrlTimeoutTicker(object state)
        {
            m_OpenUrlTimeoutCount++;
            if (m_OpenUrlTimeoutCount > MAX_OPENURL_TICK_COUNT)
            {
                // 打开超时
                IsDocumentCompleted = true;
            }
        }
        #endregion
        
    }
}
