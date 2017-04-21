//---------------------------------------------------------------
// author: FreeKnight
// date: 2017-3-30
//---------------------------------------------------------------
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Safari;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
//---------------------------------------------------------------
namespace FKAutoBrowser
{
    /// <summary>
    /// 一个自动单元，即进行一系列动作的单个脚本
    /// Instantiates a FKAutoUnit instance
    /// </summary>
    public class FKAutoUnit : IFKAutoUnit
    {
        #region 属性 | Properties
        /// <summary>
        /// 本自动单元需要被调用的动作列表类型
        /// The list of possible action types that can be called
        /// </summary>
        public Dictionary<string, FKActionType> ActionTypes = new Dictionary<string, FKActionType>();
        /// <summary>
        /// 本自动单元将被调用的动作列表
        /// The List of Actions that will be performed in this test
        /// </summary>
        public List<FKBaseAction> Actions = new List<FKBaseAction>();
        /// <summary>
        /// 当前选择的元素列表
        /// The currently selected elements
        /// </summary>
        public IEnumerable<IFKWebElement> CurrentElements;
        /// <summary>
        /// 当前选择的元素索引
        /// The index of the currently selected element
        /// </summary>
        public int CurrentElementIndex;
        /// <summary>
        /// 当前加载的Uri地址
        /// Gets the currenntly loaded Uri
        /// </summary>
        public Uri CurrentUri;
        /// <summary>
        /// 当前打开的浏览器实例
        /// The currently active browser instance
        /// </summary>
        public IFKWebDriver Browser;
        /// <summary>
        /// 浏览器之前的大小记录
        /// </summary>
        public Size BrowserOldSize;
        /// <summary>
        /// 当前打开的浏览器对象类型
        /// Gets the type of the current browser instance
        /// </summary>
        public ENUM_FKBowserType BrowserType;
        /// <summary>
        /// 最后一个动作是否执行成功
        /// Stores the success or failure of the last action
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 最后一个动作执行结果信息
        /// Stores the message from the last action
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 本自动单元解析器
        /// The parser for this FKAutoUnit
        /// </summary>
        public FKScriptParser Parser { get; set; }
        /// <summary>
        /// 当前测试单元文档行
        /// The lines in the current FKAutoUnit document
        /// </summary>
        private List<string>    m_Lines = new List<string>();
        /// <summary>
        /// 可执行的总单元数量
        /// The total number of items to run
        /// </summary>
        private int m_nTotalItemsToRun;
        /// <summary>
        /// 当前执行的单元
        /// The current item
        /// </summary>
        private int m_nCurrentItem;
        /// <summary>
        /// 外部引入的IEWebDriver
        /// </summary>
        private FKIEWebDriver m_extandIEWebDriver = null;
        #endregion

        #region 构造函数 | Constructors
        /// <summary>
        /// 创建一个空的自动单元实例
        /// Create a new FKAutoUnit instance
        /// </summary>
        public FKAutoUnit()
        {
            Initialise();
        }
        public FKAutoUnit(string[] lines)
        {
            this.m_Lines = lines.ToList();
            Initialise();
        }
        public FKAutoUnit(string fileName)
        {
            LoadFile(fileName);
            Initialise();
        }
        #endregion

        #region 事件通知函数 | Event functions

        /// <summary>
        /// The event that is fired as the test progresses
        /// </summary>
        public event ProgressChangedEventHandler TestProgress;
        private SendOrPostCallback m_funcProgressReporter;

        private void ProgressReporter(object arg)
        {
            OnProgressChanged((ProgressChangedEventArgs)arg);
        }

        protected virtual void OnProgressChanged(ProgressChangedEventArgs e)
        {
            if (TestProgress != null) TestProgress(this, e);
        }
        #endregion

        #region 核心函数 | Core functions
        /// <summary>
        /// 获取外界IEWebDriver对象
        /// </summary>
        /// <returns></returns>
        FKIEWebDriver GetExtandFKIEDriver()
        {
            return m_extandIEWebDriver;
        }
        /// <summary>
        /// 读取一个脚本文件
        /// Load actions from a script file
        /// </summary>
        /// <param name="fileName"></param>
        private void LoadFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException("The specified WTest document could not be loaded", fileName);
            }
            else
            {
                this.m_Lines = File.ReadAllLines(fileName).ToList();
            }
        }
        /// <summary>
        /// 获取被注册的动作列表名
        /// Returns the list of functions for registered actions
        /// </summary>
        private List<string> FunctionNames
        {
            get { return ActionTypes.Select(a => a.Key).ToList(); }
        }
        /// <summary>
        /// 初始化当前测试单元实例
        /// Initialise this FKAutoUnit instance
        /// </summary>
        private void Initialise()
        {
            CurrentUri = new Uri("about:blank");
            RegisterStandardActions();
        }
        /// <summary>
        /// 注册标准动作
        /// Register the standard actions
        /// </summary>
        private void RegisterStandardActions()
        {
            RegisterAction("pause", "FKAutoBrowser.FKPauseAction",
                "Pause",
                "暂停脚本一段时间",
                FKPauseAction.Parameters);
            RegisterAction("open", "FKAutoBrowser.FKOpenUrlAction",
                "Open",
                "自动打开指定链接",
                FKOpenUrlAction.Parameters);
            RegisterAction("close", "FKAutoBrowser.FKCloseAction",
                "Close",
                "关闭当前浏览器",
                FKCloseAction.Parameters);
            RegisterAction("refresh", "FKAutoBrowser.FKRefreshAction",
                "Refresh",
                "刷新当前页面",
                FKRefreshAction.Parameters);
            RegisterAction("back", "FKAutoBrowser.FKBackPageAction",
                "Back",
                "回到上一页面",
                FKBackPageAction.Parameters);
            RegisterAction("forward", "FKAutoBrowser.FKForwardPageAction",
                "Forward",
                "进入下一页面",
                FKForwardPageAction.Parameters);
            RegisterAction("resize", "FKAutoBrowser.FKResizeAction",
                "Resize",
                "重置设置浏览器窗口大小",
                FKResizeAction.Parameters);
            RegisterAction("rotate", "FKAutoBrowser.FKRotateAction",
                "Rotate",
                "旋转浏览器：横屏或竖屏",
                FKRotateAction.Parameters);
            RegisterAction("maximise", "FKAutoBrowser.FKMaximiseAction",
                "Maximise",
                "浏览器窗口最大化",
                FKMaximiseAction.Parameters);
            RegisterAction("minimise", "FKAutoBrowser.FKMinimiseAction",
                "Minimise",
                "浏览器窗口最小化",
                FKMinimiseAction.Parameters);
            RegisterAction("reset", "FKAutoBrowser.FKResetSizeAction",
                "Reset",
                "恢复浏览器窗口大小为默认大小",
                FKResetSizeAction.Parameters);
            RegisterAction("newtab", "FKAutoBrowser.FKNewTabAction",
                "NewTab",
                "打开一个新的选项卡，并打开指定网页",
                FKNewTabAction.Parameters);
            RegisterAction("gototab", "FKAutoBrowser.FKGotoTabAction",
                "GoToTab",
                "打开指定索引编号的选项卡",
                FKGotoTabAction.Parameters);
            RegisterAction("closetab", "FKAutoBrowser.FKCloseCurTabAction",
                "CloseTab",
                "关闭当前选项卡",
                FKCloseCurTabAction.Parameters);
            RegisterAction("nexttab", "FKAutoBrowser.FKNextTabAction",
                "NextTab",
                "打开下一个选项卡",
                FKNextTabAction.Parameters);
            RegisterAction("previoustab", "FKAutoBrowser.FKPreviousTabAction",
                "PreviousTab",
                "打开上一个选项卡",
                FKPreviousTabAction.Parameters);
            RegisterAction("keypress", "FKAutoBrowser.FKKeyPressAction",
                "KeyPress",
                "向浏览器发送指定按键( 通常用于浏览器快捷键 )",
                FKKeyPressAction.Parameters);
            RegisterAction("css", "FKAutoBrowser.FKSetCSSAction",
                "CSS",
                "设置网页CSS样式",
                FKSetCSSAction.Parameters);
            RegisterAction("zoomin", "FKAutoBrowser.FKZoomInAction",
                "ZoomIn",
                "放大网页显示",
                FKZoomInAction.Parameters);
            RegisterAction("zoomout", "FKAutoBrowser.FKZoomOutAction",
                "ZoomOut",
                "缩小网页显示",
                FKZoomOutAction.Parameters);
            RegisterAction("resetzoom", "FKAutoBrowser.FKResetZoomAction",
                "ResetZoom",
                "重置网页显示为100%",
                FKResetZoomAction.Parameters);
            RegisterAction("screenshot", "FKAutoBrowser.FKScreenShotAction",
                "Screenshot",
                "截屏当前页面",
                FKScreenShotAction.Parameters);
            RegisterAction("save", "FKAutoBrowser.FKSavePageAction",
                "Save",
                "保存当前HTML页面",
                FKSavePageAction.Parameters);
            RegisterAction("eval", "FKAutoBrowser.FKEvalJSAction",
                "Eval",
                "执行指定的JavaScript脚本",
                FKEvalJSAction.Parameters);
            RegisterAction("findbyxpath", "FKAutoBrowser.FKFindElemAction",
                "Find",
                "查找当前页面中指定XPath的元素",
                FKFindElemAction.Parameters);
            RegisterAction("getcookie", "FKAutoBrowser.FKGetCookieAction",
                "GetCookie",
                "获取一个指定Cookie的值",
                FKGetCookieAction.Parameters);
            RegisterAction("setcookie", "FKAutoBrowser.FKSetCookieAction",
                "SetCookie",
                "设置一个指定Cookie的值",
                FKSetCookieAction.Parameters);
            RegisterAction("deletecookie", "FKAutoBrowser.FKDeleteCookieAction",
                "DeleteCookie",
                "删除一个指定Cookie",
                FKDeleteCookieAction.Parameters);
            RegisterAction("search", "FKAutoBrowser.FKSearchTextAction",
                "Search",
                "在当前页面进行文本查找",
                FKSearchTextAction.Parameters);
            RegisterAction("title", "FKAutoBrowser.FKGetPageTitleAction",
                "Title",
                "获取当前页面标题文字",
                FKGetPageTitleAction.Parameters);
            RegisterAction("highlight", "FKAutoBrowser.FKHighlightElemAction",
                "Highlight",
                "高亮当前元素",
                FKHighlightElemAction.Parameters);
            RegisterAction("innerhtml", "FKAutoBrowser.FKGetElemInnerHtmlAction",
                "InnerHtml",
                "获取当前元素内部Html",
                FKGetElemInnerHtmlAction.Parameters);
            RegisterAction("outerhtml", "FKAutoBrowser.FKGetElemOuterHtmlAction",
                "OuterHtml",
                "获取当前元素外部Html",
                FKGetElemOuterHtmlAction.Parameters);
            RegisterAction("gettext", "FKAutoBrowser.FKGetElemTextAction",
                "GetText",
                "获取当前元素文本",
                FKGetElemTextAction.Parameters);
            RegisterAction("waitfor", "FKAutoBrowser.FKWaitForElemAction",
                "WaitFor",
                "等待指定元素可见",
                FKWaitForElemAction.Parameters);
            RegisterAction("click", "FKAutoBrowser.FKClickAction",
               "Click",
               "点击当前元素",
                FKClickAction.Parameters);
            RegisterAction("clicklast", "FKAutoBrowser.FKClickLastElemAction",
                "ClickLast",
                "点击上一次选中的元素",
                FKClickLastElemAction.Parameters);
            RegisterAction("settext", "FKAutoBrowser.FKSetElemTextAction",
                "SetText",
                "向当前元素输入文本",
                FKSetElemTextAction.Parameters);
            RegisterAction("setrandomtext", "FKAutoBrowser.FKSetElemRandomTextAction",
                "SetRandomText",
                "向当前元素输入随机文本",
                FKSetElemRandomTextAction.Parameters);
            RegisterAction("firstname", "FKAutoBrowser.FKSetElemWesternFNTextAction",
                "FirstName",
                "向当前元素输入一个随机英文First name",
                FKSetElemWesternFNTextAction.Parameters);
            RegisterAction("lastname", "FKAutoBrowser.FKSetElemWesternLNTextAction",
                "LastName",
                "向当前元素输入一个随机英文Last name",
                FKSetElemWesternLNTextAction.Parameters);
            RegisterAction("selectvalue", "FKAutoBrowser.FKSelectValueAction",
                "SelectValue",
                "根据值设置一个选项单的选项",
                FKSelectValueAction.Parameters);
            RegisterAction("selecttext", "FKAutoBrowser.FKSelectTextAction",
                "SelectText",
                "根据文本设置一个选项单的选项",
                FKSelectTextAction.Parameters);
            RegisterAction("selectindex", "FKAutoBrowser.FKSelectIndexAction",
                "SelectIndex",
                "根据索引设置一个选项单的选项",
                FKSelectIndexAction.Parameters);
            RegisterAction("selectrandom", "FKAutoBrowser.FKSelectRandomOptionAction",
                "SelectRandom",
                "对一个选项单进行随机选择",
                FKSelectRandomOptionAction.Parameters);
            // TODO： 注册其他函数
        }
        /// <summary>
        /// 注册一个动作
        /// Register an action to be performed when a given function name is invoked
        /// </summary>
        /// <param name="functionName">注册的动作名 | The name of the function that will call this action</param>
        /// <param name="typeName">注册的动作类型 | The type name of the action to be called</param>
        private void RegisterAction(string functionName, string typeName, string name = null, 
            string description = null, List<FKActionParameter> parameters = null)
        {
            if (!ActionTypes.ContainsKey(functionName))
            {
                FKActionType actionType = new FKActionType(functionName, typeName);
                if (description != null)
                {
                    actionType.Name = name;
                }
                if (description != null)
                {
                    actionType.Description = description;
                }
                if (parameters != null)
                {
                    actionType.Parameters = parameters;
                }
                ActionTypes.Add(functionName, actionType);
            }
        }
        /// <summary>
        /// 创建浏览器实例
        /// Create the browser of the given type
        /// </summary>
        /// <param name="browserType">浏览器类型，参见 ENUM_FKBowserType | Browser type, please see ENUM_FKBowserType define</param>
        private void CreateBrowser(ENUM_FKBowserType browserType)
        {
            try
            {
                switch (browserType)
                {
                    case ENUM_FKBowserType.eFKBrowserType_IEDriver_Chrome:
                        Browser = new IFKWebDriver(new ChromeDriver());
                        break;
                    case ENUM_FKBowserType.eFKBrowserType_IEDriver_Firefox:
                        Browser = new IFKWebDriver(new FirefoxDriver());
                        break;
                    case ENUM_FKBowserType.eFKBrowserType_IEDriver_Safari:
                        Browser = new IFKWebDriver(new SafariDriver());
                        break;
                    case ENUM_FKBowserType.eFKBrowserType_IEDriver_IE:
                        Browser = new IFKWebDriver(new InternetExplorerDriver(new InternetExplorerOptions() { IntroduceInstabilityByIgnoringProtectedModeSettings = true }));
                        break;
                    case ENUM_FKBowserType.eFKBrowserType_System_IE:
                    default:
                        FKIEWebDriver driver = GetExtandFKIEDriver();
                        if (driver == null)
                            driver = new FKIEWebDriver();
                        Browser = new IFKWebDriver(driver);
                        break;
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
        /// <summary>
        /// 根据字符串获取浏览器类型
        /// Get browser type by string desc
        /// </summary>
        /// <param name="strType"></param>
        /// <returns></returns>
        private ENUM_FKBowserType GetBrowserType(string strType)
        {
            if (strType.ToLower() == "firefox")
            {
                return ENUM_FKBowserType.eFKBrowserType_IEDriver_Firefox;
            }
            if (strType.ToLower() == "chrome")
            {
                return ENUM_FKBowserType.eFKBrowserType_IEDriver_Chrome;
            }
            if (strType.ToLower() == "safari")
            {
                return ENUM_FKBowserType.eFKBrowserType_IEDriver_Safari;
            }
            if (strType.ToLower() == "ie")
            {
                return ENUM_FKBowserType.eFKBrowserType_IEDriver_IE;
            }
            if (strType.ToLower() == "system_ie")
            {
                return ENUM_FKBowserType.eFKBrowserType_System_IE;
            }
            return ENUM_FKBowserType.eFKBrowserType_System_IE;
        }
        /// <summary>
        /// 运行一个自动执行单元
        /// Runs a test with the AutoUnit
        /// </summary>
        public void Run(Delegate preActionDelegate, Delegate actionResultDelegate)
        {
            try
            {
                // 创建新解析器
                Parser = new FKScriptParser();
                // 创建通知回调
                m_funcProgressReporter = new SendOrPostCallback(ProgressReporter);

                // 添加测试命令
                var resultParameters = new List<object>
                    {
                        "test",
                        new List<object>(),
                        true,
                        "开始测试脚本,请稍候..."
                    };
                actionResultDelegate.DynamicInvoke(resultParameters.ToArray());

                // 创建浏览器类型
                var browsers = new List<ENUM_FKBowserType>();
                // 第一个命令必须是创建浏览器类型
                if (m_Lines.Any() && m_Lines.First().StartsWith("$.browser"))
                {
                    // 获取命令参数
                    List<object> parameters = Parser.GetParameters(m_Lines.First());
                    // 仅仅一个参数，则创建
                    if (parameters.Count == 1)
                    {
                        BrowserType = GetBrowserType(parameters[0].ToString());
                        browsers.Add(BrowserType);
                    }

                    // 多于一个参数，则全部记录
                    if (parameters.Count > 1)
                    {
                        foreach (object o in parameters)
                        {
                            BrowserType = GetBrowserType(o.ToString());
                            browsers.Add(BrowserType);
                        }
                    }

                    // 移除第一行，不再创建浏览器
                    m_Lines.RemoveAt(0);
                }

                // 总共需要执行的命令数量
                m_nTotalItemsToRun = browsers.Count * m_Lines.Count;

                // 逐个浏览器执行测试脚本
                foreach (ENUM_FKBowserType browserType in browsers)
                {
                    // 关闭已经存在的脚本对象
                    if (Browser != null)
                    {
                        Browser.Close();
                        Browser = null;
                    }
                    // 创建新的浏览器对象实例
                    CreateBrowser(browserType);

                    // 通知当前执行的进度状况
                    resultParameters = new List<object>
                    {
                        "browser",
                        new List<object> {browserType.ToString()},
                        true,
                        "创建浏览器对象"
                    };
                    actionResultDelegate.DynamicInvoke(resultParameters.ToArray());

                    // 循环执行单行脚本
                    ProcessLines(preActionDelegate, actionResultDelegate);
                }

                // 通知当前测试进度
                m_funcProgressReporter(new ProgressChangedEventArgs(100, null));
            }
            catch(ThreadAbortException)
            {
                Thread.ResetAbort();    // 中断消息
            }
            catch(Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
        /// <summary>
        /// 循环执行脚本动作
        /// Process the lines of the script
        /// </summary>
        /// <param name="preActionDelegate"></param>
        /// <param name="actionResultDelegate"></param>
        void ProcessLines(Delegate preActionDelegate, Delegate actionResultDelegate)
        {
            foreach (string line in m_Lines)
            {
                // 检查本行是否有效
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                // 如果以 // 开头，则是注释
                if (line.StartsWith("//"))
                {
                    // 创建注释参数
                    var resultParameters = new List<object> { "注释:", new List<object>(), true, line };
                    actionResultDelegate.DynamicInvoke(resultParameters.ToArray());
                    // 直接进入下一行
                    continue;
                }

                // 每一行开头必须是$.
                if (line.StartsWith("$."))
                {
                    // 获取动作
                    List<string> actions = Parser.Split(line.Substring(2), ".", "\"", true);

                    // 休眠50毫秒
                    Thread.Sleep(50);

                    // 单行多动作，逐动作执行
                    foreach (string action in actions)
                    {
                        // 获取动作名
                        string thisAction = action.Trim();
                        string functionName = Parser.GetFunctionName(thisAction);

                        // 检查动作函数是否被注册
                        if (functionName != null && ActionTypes.ContainsKey(functionName))
                        {
                            // 获取其参数和元素索引
                            List<object> parameters = Parser.GetParameters(thisAction);
                            int? index = Parser.GetIndex(thisAction);
                            if (index != null)
                                CurrentElementIndex = index.Value;

                            if (Browser != null)
                            {
                                // 创建FKAction动作对象
                                var invoker = new FKActionInvoker();
                                IFKAction actionObject = invoker.Invoke(this, ActionTypes[functionName].MethodName, parameters);

                                if (actionObject == null)
                                {
                                    // 函数动作执行错误
                                    var resultParameters = new List<object>
                                    {
                                        functionName,
                                        new List<object>(),
                                        false,
                                        string.Format("函数查找执行失败 '{0}' ：{1}", functionName, this.Message)
                                    };
                                    actionResultDelegate.DynamicInvoke(resultParameters.ToArray());
                                }
                                else
                                {
                                    // 执行PreAction
                                    actionObject = invoker.PreAction(this, actionObject);

                                    // 如果设置了动作处理前的回调，则进行回调处理
                                    if (preActionDelegate != null)
                                    {
                                        // 创建回调参数
                                        var preActionParameters = new List<object>
                                        {
                                            functionName,
                                            parameters,
                                            actionObject.PreActionMessage
                                        };
                                        preActionDelegate.DynamicInvoke(preActionParameters.ToArray());
                                    }

                                    // 实际执行FKAction
                                    actionObject = invoker.Execute(this, actionObject);

                                    // 获取Action结果
                                    var resultParameters = new List<object> { functionName, parameters };
                                    if (actionObject != null)
                                    {
                                        resultParameters.Add(actionObject.Success);
                                        resultParameters.Add(actionObject.PostActionMessage);
                                    }
                                    else
                                    {
                                        resultParameters.Add(Success);
                                        resultParameters.Add(Message);
                                    }
                                    actionResultDelegate.DynamicInvoke(resultParameters.ToArray());
                                }
                            }
                        }
                        else
                        {
                            // 函数未注册错误
                            var resultParameters = new List<object>
                                {
                                    functionName,
                                    new List<object>(),
                                    false,
                                    string.Format("函数 '{0}' 没有注册，是否输入了错误的函数名", functionName)
                                };
                            actionResultDelegate.DynamicInvoke(resultParameters.ToArray());
                        }
                    }
                }
                else
                {
                    // 本行格式有误
                    var resultParameters = new List<object>
                        {
                            "",
                            new List<object>(),
                            false,
                            "每行起始必须以 '$.' 开头"
                        };
                    actionResultDelegate.DynamicInvoke(resultParameters.ToArray());
                }

                // 更新测试脚本的进度
                m_nCurrentItem++;
                int progress = (200 * m_nCurrentItem + 1) / (m_nTotalItemsToRun * 2);
                m_funcProgressReporter(new ProgressChangedEventArgs(progress, null));
            }
        }
        #endregion

        #region 对外接口 | Interface
        /// <summary>
        /// 外部注入IEWebDriver
        /// </summary>
        /// <param name="driver"></param>
        public void SetExtandIEWebDirver(FKIEWebDriver driver) { m_extandIEWebDriver = driver; }
        /// <summary>
        /// 运行一个自动执行单元
        /// Runs a test with the AutoUnit
        /// </summary>
        public void RunScript(string strFile, Delegate preActionDelegate, Delegate actionResultDelegate)
        {
            try
            {
                LoadFile(strFile);
                Run(preActionDelegate, actionResultDelegate);
            }catch(Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
        /// <summary>
        /// 执行每个动作之前的对外接口函数
        /// The delegate that may be called before each action is invoked
        /// </summary>
        /// <param name="functionName"></param>
        /// <param name="parameters"></param>
        /// <param name="message"></param>
        public delegate void PreActionDelegate(string functionName, List<object> parameters, string message);

        /// <summary>
        /// 执行每个动作之后的对外接口函数
        /// The delegate that must be called for each action
        /// </summary>
        /// <param name="functionName"></param>
        /// <param name="parameters"></param>
        /// <param name="success"></param>
        /// <param name="message"></param>
        public delegate void ActionResultDelegate(string functionName, List<object> parameters, bool success, string message);
        #endregion
    }
}
