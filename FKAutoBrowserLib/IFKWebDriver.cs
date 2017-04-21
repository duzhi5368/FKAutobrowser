//---------------------------------------------------------------
// author: FreeKnight
// date: 2017-3-30
//---------------------------------------------------------------
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
//---------------------------------------------------------------
namespace FKAutoBrowser
{
    /// <summary>
    /// 封装Web浏览器元素对象
    /// Wrapper of Selenium WebDriver and C# form WebBrowser object
    /// </summary>
    public class IFKWebDriver
    {
        public IWebDriver WebDriver { get; set; }
        public FKIEWebDriver HtmlWebDriver { get; set; }

        public IFKWebDriver(IWebDriver driver) {
            WebDriver = driver;
            HtmlWebDriver = null;
        }
        public IFKWebDriver(FKIEWebDriver driver) {
            WebDriver = null;
            HtmlWebDriver = driver;
        }

        #region 核心功能函数 | Core function
        /// <summary>
        /// 通过元素标示查找元素
        /// find elements by selector
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        public IEnumerable<IFKWebElement> FindElements(string selector)
        {
            if (WebDriver != null)
            {
                IEnumerable<IWebElement> elems = WebDriver.FindElements(By.XPath(selector));
                List<IWebElement> list = elems.ToList();
                List<IFKWebElement> result = new List<IFKWebElement>();
                foreach (var elem in elems)
                {
                    result.Add(new IFKWebElement(elem));
                }
                return result as IEnumerable<IFKWebElement>;
            }
            if (HtmlWebDriver != null)
            {
                IEnumerable<HtmlElement> elems = HtmlWebDriver.FindElementsByXPath2(selector);
                List<HtmlElement> list = elems.ToList();
                List<IFKWebElement> result = new List<IFKWebElement>();
                foreach (var elem in elems)
                {
                    result.Add(new IFKWebElement(elem));
                }
                /*
                List<IFKWebElement> result = new List<IFKWebElement>();
                result.Add(new IFKWebElement(HtmlWebDriver.FindElementsByXPath(selector)));
                */
                return result as IEnumerable<IFKWebElement>;
            }
            throw new Exception("FindElements failed...");
        }
        /// <summary>
        /// 查找指定元素
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="nTimeOut"></param>
        /// <returns></returns>
        public IEnumerable<IFKWebElement> FindElements(string selector, int nTimeOut)
        {
            IEnumerable<IFKWebElement> results = Enumerable.Empty<IFKWebElement>();
            if (WebDriver != null)
            {
                if (nTimeOut == 0)
                {
                    ReadOnlyCollection<IWebElement> elems = WebDriver.FindElements(By.XPath(selector));
                    List<IWebElement> list = elems.ToList();
                    List<IFKWebElement> result = new List<IFKWebElement>();
                    foreach (var elem in elems)
                    {
                        result.Add(new IFKWebElement(elem));
                    }
                    return result as IEnumerable<IFKWebElement>;
                }
                else
                {
                    ReadOnlyCollection<IWebElement> elems = WebDriver.FindElements(By.XPath(selector), nTimeOut);
                    List<IWebElement> list = elems.ToList();
                    List<IFKWebElement> result = new List<IFKWebElement>();
                    foreach (var elem in elems)
                    {
                        result.Add(new IFKWebElement(elem));
                    }
                    return result as IEnumerable<IFKWebElement>;
                }
            }
            if (HtmlWebDriver != null)
            {
                IEnumerable<HtmlElement> elems = HtmlWebDriver.FindElementsByXPath2(selector);
                List<HtmlElement> list = elems.ToList();
                List<IFKWebElement> result = new List<IFKWebElement>();
                foreach (var elem in elems)
                {
                    result.Add(new IFKWebElement(elem));
                }
                /*
                List<IFKWebElement> result = new List<IFKWebElement>();
                result.Add(new IFKWebElement(HtmlWebDriver.FindElementsByXPath(selector)));
                */
                return result as IEnumerable<IFKWebElement>;
            }
            throw new Exception("FindElements failed...");
        }
        /// <summary>
        /// 打开一个网址
        /// Open a uri
        /// </summary>
        /// <param name="uri"></param>
        public void OpenUrl(Uri uri)
        {
            if (WebDriver != null)
            {
                WebDriver.Navigate().GoToUrl(uri);
                return;
            }
            if(HtmlWebDriver != null)
            {
                HtmlWebDriver.SafetyOpenUrl(uri);
                return;
            }
            throw new Exception("OpenUrl failed...");
        }
        public void OpenUrl(string url)
        {
            if (!url.StartsWith("http") && !url.StartsWith("https"))
            {
                url = "http://" + url;
            }
            Uri uri = new Uri(url);
            if (WebDriver != null)
            {
                WebDriver.Navigate().GoToUrl(uri);
                return;
            }
            if (HtmlWebDriver != null)
            {
                HtmlWebDriver.SafetyOpenUrl(uri);
                return;
            }
            throw new Exception("OpenUrl failed...");
        }
        /// <summary>
        /// 关闭当前浏览器实例
        /// Close the current browser instance
        /// </summary>
        public void Close()
        {
            if (WebDriver != null)
            {
                // 关闭全部Tab，除第一个之外
                for (;;)
                {
                    ReadOnlyCollection<string> handles = WebDriver.WindowHandles;
                    if(handles.Count >= 2)
                    {
                        WebDriver.SwitchTo().Window(handles[1]);
                        WebDriver.Close();
                        WebDriver.SwitchTo().Window(handles[0]);
                    }
                    else
                    {
                        break;
                    }
                }
                // 对第一个 Tab 进行关闭
                WebDriver.Close();
                // 完全释放
                WebDriver.Quit();
                WebDriver.Dispose();
                WebDriver = null;
                return;
            }
            if (HtmlWebDriver != null)
            {
                HtmlWebDriver.Stop();
                HtmlWebDriver.Navigate("about:blank");
                HtmlWebDriver = null;
                return;
            }
            throw new Exception("Close failed...");
        }
        /// <summary>
        /// 刷新当前页面
        /// Refreshes the current page
        /// </summary>
        public void Refresh()
        {
            if (WebDriver != null)
            {
                WebDriver.Navigate().Refresh();
                return;
            }
            if (HtmlWebDriver != null)
            {
                HtmlWebDriver.Refresh();
                return;
            }
            throw new Exception("Refresh failed...");
        }
        /// <summary>
        /// 返回上一页面
        /// Goes the the previous page
        /// </summary>
        public bool BackPage()
        {
            if (WebDriver != null)
            {
                WebDriver.Navigate().Back();
                return true;
            }
            if (HtmlWebDriver != null)
            {
                if (!HtmlWebDriver.CanGoBack)
                {
                    return false;
                }
                return HtmlWebDriver.GoBack();
            }
            throw new Exception("BackPage failed...");
        }
        /// <summary>
        /// 进入下一页面
        /// Goes the the next page (after previously going back)
        /// </summary>
        public bool ForwardPage()
        {
            if (WebDriver != null)
            {
                WebDriver.Navigate().Forward();
                return true;
            }
            if (HtmlWebDriver != null)
            {
                if (!HtmlWebDriver.CanGoForward)
                {
                    return false;
                }
                return HtmlWebDriver.GoForward();
            }
            throw new Exception("ForwardPage failed...");
        }
        public Size Size()
        {
            if (WebDriver != null)
            {
                return WebDriver.Manage().Window.Size;
            }
            if (HtmlWebDriver != null)
            {
                return HtmlWebDriver.Size;
            }
            throw new Exception("Size failed...");
        }
        /// <summary>
        /// 重置浏览器窗口大小
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public bool Resize(Size size)
        {
            if (WebDriver != null)
            {
                WebDriver.Manage().Window.Size = size;
                return true;
            }
            if (HtmlWebDriver != null)
            {
                HtmlWebDriver.MyResize(size);
                return true;
            }
            throw new Exception("Resize failed...");
        }
        /// <summary>
        /// 横竖转屏
        /// </summary>
        /// <returns></returns>
        public bool Rotate()
        {
            if (WebDriver != null)
            {
                var rot = WebDriver as IRotatable;
                if (rot == null)
                    return false;
                rot.Orientation = rot.Orientation == OpenQA.Selenium.ScreenOrientation.Portrait
                                      ? OpenQA.Selenium.ScreenOrientation.Landscape
                                      : OpenQA.Selenium.ScreenOrientation.Portrait;
                return true;
            }
            if (HtmlWebDriver != null)
            {
                // 暂不处理
                return true;
            }
            throw new Exception("Rotate failed...");
        }
        /// <summary>
        /// 最大化浏览器窗口
        /// </summary>
        /// <returns></returns>
        public bool Maximize()
        {
            if (WebDriver != null)
            {
                WebDriver.Manage().Window.Maximize();
                return true;
            }
            if (HtmlWebDriver != null)
            {
                HtmlWebDriver.Maximize();
                return true;
            }
            throw new Exception("Maximize failed...");
        }
        /// <summary>
        /// 最小化浏览器窗口
        /// </summary>
        /// <returns></returns>
        public bool Minimize()
        {
            if (WebDriver != null)
            {
                WebDriver.Manage().Window.Size = new Size(1, 1);
                return true;
            }
            if (HtmlWebDriver != null)
            {
                HtmlWebDriver.Minimize();
                return true;
            }
            throw new Exception("Minimize failed...");
        }
        /// <summary>
        /// 设置CSS样式
        /// </summary>
        /// <param name="strStyle"></param>
        /// <returns></returns>
        public bool SetCSSStyle(string strStyle)
        {
            if (WebDriver != null)
            {
                var js = WebDriver as IJavaScriptExecutor;
                string styleScript = "var head = document.getElementsByTagName('head')[0].innerHTML; document.getElementsByTagName('head')[0].innerHTML = head + '<style type=\"text/css\" class=\"wtesterstyles\">{0}</style>';";
                styleScript = string.Format(styleScript, strStyle);
                js.ExecuteScript(styleScript, null);
                return true;
            }
            if (HtmlWebDriver != null)
            {
                return HtmlWebDriver.SetCSSStyle(strStyle);
            }
            throw new Exception("SetCSSStyle failed...");
        }
        /// <summary>
        /// 执行一段JS脚本
        /// </summary>
        /// <param name="strJS"></param>
        /// <returns></returns>
        public string EvalJS(string strJS)
        {
            if (WebDriver != null)
            {
                var js = WebDriver as IJavaScriptExecutor;
                var result = js.ExecuteScript(strJS, null) as string;
                return result;
            }
            if (HtmlWebDriver != null)
            {
                var result = HtmlWebDriver.ExecuteScript(strJS);
                return result;
            }
            throw new Exception("EvalJS failed...");
        }
        /// <summary>
        /// 获取当前页面源码
        /// </summary>
        /// <returns></returns>
        public string GetPage()
        {
            if (WebDriver != null)
            {
                return WebDriver.PageSource;
            }
            if (HtmlWebDriver != null)
            {
                return HtmlWebDriver.PageSource();
            }
            throw new Exception("GetPage failed...");
        }
        /// <summary>
        /// 格式化文件名
        /// </summary>
        /// <param name="strFilename"></param>
        /// <returns></returns>
        public string FormatFileName(string strFilename)
        {
            if (WebDriver != null)
            {
                strFilename = strFilename.Replace("[width]", WebDriver.Manage().Window.Size.Width.ToString());
                strFilename = strFilename.Replace("[height]", WebDriver.Manage().Window.Size.Height.ToString());
                var cap = WebDriver as IHasCapabilities;
                strFilename = strFilename.Replace("[browser]", cap.Capabilities.BrowserName);
                strFilename = strFilename.Replace("[version]", cap.Capabilities.Version);
                return strFilename;
            }
            if (HtmlWebDriver != null)
            {
                // 暂不处理
                return strFilename;
            }
            throw new Exception("FormatFileName failed...");
        }
        /// <summary>
        /// 进行浏览器页面截屏
        /// </summary>
        /// <param name="strFilename"></param>
        /// <param name="format"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public string ScreenShot(string strFilename, ImageFormat format, ENUM_FKBowserType type)
        {
            if (WebDriver != null)
            {
                if(type == ENUM_FKBowserType.eFKBrowserType_IEDriver_Chrome)
                {
                    // Chrome has a bug which stops it capturing the whole of the screen
                    // see https://code.google.com/p/chromedriver/issues/detail?id=294
                    var ss = new FKChromeScreenShot();
                    Bitmap bitmap = ss.GetScreenshot(WebDriver);
                    if (bitmap == null)
                    {
                        return "Chrome截图失败";
                    }

                    bitmap.Save(strFilename, format);
                    if (!File.Exists(strFilename))
                    {
                        return String.Format("Chrome截图文件无法保存到 {0}", strFilename);
                    }
                    return "";
                }
                else
                {
                    var shot = WebDriver as ITakesScreenshot;

                    if (shot == null)
                    {
                        return  String.Format("当前浏览器类型 ({0}) 不支持进行截图处理", type);
                    }

                    var ss = shot.GetScreenshot();
                    ss.SaveAsFile(strFilename, format);
                    if (!File.Exists(strFilename))
                    {
                        return String.Format("截图文件无法保存到 {0}", strFilename);
                    }
                    return "";
                }
            }
            if (HtmlWebDriver != null)
            {
                return HtmlWebDriver.SaveImage(strFilename);
            }
            throw new Exception("ScreenShot failed...");
        }
        /// <summary>
        /// 获取Cookie值
        /// </summary>
        /// <param name="strCookieName"></param>
        /// <returns></returns>
        public string GetCookieValue(string strCookieName)
        {
            if (WebDriver != null)
            {
                Cookie cookie = WebDriver.Manage().Cookies.GetCookieNamed(strCookieName);
                if (cookie == null)
                    return "";
                return cookie.Value;
            }
            if (HtmlWebDriver != null)
            {
                return "";
            }
            throw new Exception("GetCookieValue failed...");
        }
        /// <summary>
        /// 设置指定Cookie值
        /// </summary>
        /// <param name="strCookieName"></param>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public bool SetCookieValue(string strCookieName, string strValue)
        {
            if (WebDriver != null)
            {
                Cookie cookie = WebDriver.Manage().Cookies.GetCookieNamed(strCookieName);
                if(cookie != null)
                {
                    // 删除原Cookie
                    WebDriver.Manage().Cookies.DeleteCookie(cookie);
                    WebDriver.Manage().Cookies.AddCookie(new Cookie(strCookieName, strValue));
                    return true;
                }
                else
                {
                    WebDriver.Manage().Cookies.AddCookie(new Cookie(strCookieName, strValue));
                    return true;
                }
            }
            if (HtmlWebDriver != null)
            {
                return false;
            }
            throw new Exception("SetCookieValue failed...");
        }
        /// <summary>
        /// 删除指定Cookie
        /// </summary>
        /// <param name="strCookieName"></param>
        /// <returns></returns>
        public bool DeleteCookie(string strCookieName)
        {
            if (WebDriver != null)
            {
                Cookie cookie = WebDriver.Manage().Cookies.GetCookieNamed(strCookieName);
                if (cookie != null)
                {
                    // 删除原Cookie
                    WebDriver.Manage().Cookies.DeleteCookieNamed(strCookieName);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (HtmlWebDriver != null)
            {
                return false;
            }
            throw new Exception("DeleteCookie failed...");
        }
        /// <summary>
        /// 获取本页标题栏文字
        /// </summary>
        /// <returns></returns>
        public string GetPageTitle()
        {
            if (WebDriver != null)
            {
                IWebElement el = WebDriver.FindElement(By.TagName("title"));
                if (el == null)
                    return "";
                return el.Text;
            }
            if (HtmlWebDriver != null)
            {            
                return HtmlWebDriver.GetTitle();
            }
            throw new Exception("GetPageTitle failed...");
        }
        /// <summary>
        /// 设置指定元素高亮
        /// </summary>
        /// <param name="elem"></param>
        public bool SetHighlightElem(IFKWebElement elem)
        {
            if (WebDriver != null)
            {
                var js = WebDriver as IJavaScriptExecutor;
                string strHighlightJS = @"arguments[0].style.cssText = ""border: 2px solid yellow"";";
                js.ExecuteScript(strHighlightJS, new object[] { elem.WebElement });
                return true; 
            }
            if (HtmlWebDriver != null)
            {
                string strStyle = elem.HtmlElement.Style;
                elem.HtmlElement.Style = strStyle + "; border: 2px solid yellow;";
                return true;
            }
            throw new Exception("SetHighlightElem failed...");
        }
        /// <summary>
        /// 是否支持多Tab页面
        /// </summary>
        /// <returns></returns>
        public bool IsSupportMutilTab()
        {
            if (WebDriver != null)
                return true;
            return false;
        }
        #endregion
    }
}
