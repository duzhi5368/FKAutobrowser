//---------------------------------------------------------------
// author: FreeKnight
// date: 2017-3-30
//---------------------------------------------------------------
using System.Windows.Forms;
using OpenQA.Selenium;
using System;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
//---------------------------------------------------------------
namespace FKAutoBrowser
{
    /// <summary>
    /// 封装页面元素对象
    /// Wrapper of Selenium WebElement and C# form HtmlElement object
    /// </summary>
    public class IFKWebElement
    {
        public IWebElement WebElement { get; set; }
        public HtmlElement HtmlElement { get; set; }
        public IFKWebElement(IWebElement ele) {
            WebElement = ele;
            HtmlElement = null;
        }
        public IFKWebElement(HtmlElement ele) {
            WebElement = null;
            HtmlElement = ele;
        }

        /// <summary>
        /// 获取元素内部HTML
        /// </summary>
        /// <returns></returns>
        public string InnerHTML()
        {
            if(WebElement != null) { return WebElement.GetAttribute("innerHTML"); }
            if(HtmlElement != null) { return HtmlElement.InnerHtml; }
            throw new Exception("InnerHTML failed...");
        }
        /// <summary>
        /// 获取元素外部HTML
        /// </summary>
        /// <returns></returns>
        public string OuterHTML()
        {
            if (WebElement != null) { return WebElement.GetAttribute("outerHTML"); }
            if (HtmlElement != null) { return HtmlElement.OuterHtml; }
            throw new Exception("OuterHTML failed...");
        }
        /// <summary>
        /// 获取元素文本
        /// </summary>
        /// <returns></returns>
        public string Text()
        {
            if (WebElement != null) { return WebElement.Text; }
            if (HtmlElement != null) { return HtmlElement.InnerText; }
            throw new Exception("Text failed...");
        }
        /// <summary>
        /// 获取元素TagName
        /// </summary>
        /// <returns></returns>
        public string TagName()
        {
            if (WebElement != null) { return WebElement.TagName; }
            if (HtmlElement != null) { return HtmlElement.TagName; }
            throw new Exception("TagName failed...");
        }
        /// <summary>
        /// 点击元素
        /// </summary>
        public void Click()
        {
            if (WebElement != null) {
                WebElement.Click();
                return;
            }
            if (HtmlElement != null) {
                HtmlElement.InvokeMember("click");
                return;
            }
            throw new Exception("Click failed...");
        }
        /// <summary>
        /// Select类型的元素进行随机选项选择
        /// </summary>
        /// <returns></returns>
        public bool RandomSelect()
        {
            if (WebElement != null)
            {
                SelectElement select = new SelectElement(WebElement);
                List<int> indexes = new List<int>();
                for (int x = 0; x < select.Options.Count; x++)
                {
                    IWebElement option = select.Options[x];
                    if (!string.IsNullOrWhiteSpace(option.GetAttribute("value")))
                    {
                        indexes.Add(x);
                    }
                }
                Random rand = new Random();
                select.SelectByIndex(rand.Next(indexes.Count));
                return true;
            }
            if(HtmlElement != null)
            {
                // TODO
                return false;
            }
            throw new Exception("RandomSelect failed...");
        }
        /// <summary>
        /// Select类型的元素根据值设置选项
        /// </summary>
        /// <returns></returns>
        public bool SelectByValue(string strValue)
        {
            if (WebElement != null)
            {
                SelectElement select = new SelectElement(WebElement);
                select.SelectByValue(strValue);
                return true;
            }
            if (HtmlElement != null)
            {
                HtmlElementCollection child = HtmlElement.Children.GetElementsByName("");
                bool bFound = false;
                int nCount = child.Count;
                int nIndex = 0;
                for (int i = 0; i < nCount; ++i)
                {
                    if (child[i].GetAttribute("value").Equals(strValue, StringComparison.OrdinalIgnoreCase))
                    {
                        bFound = true;
                        nIndex = i;
                        break;
                    }
                }
                if (bFound == false)
                {
                    return false;
                }
                HtmlElement.Focus();
                HtmlElement.SetAttribute("selectedIndex", nIndex.ToString());
                SendKeys.SendWait("{TAB}");
                return true;
            }
            throw new Exception("SelectByValue failed...");
        }
        /// <summary>
        /// Select类型的元素根据值设置选项
        /// </summary>
        /// <returns></returns>
        public bool SelectByText(string strText)
        {
            if (WebElement != null)
            {
                SelectElement select = new SelectElement(WebElement);
                select.SelectByText(strText);
                return true;
            }
            if (HtmlElement != null)
            {
                HtmlElementCollection child = HtmlElement.Children.GetElementsByName("");
                bool bFound = false;
                int nCount = child.Count;
                int nIndex = 0;
                for(int i = 0; i < nCount; ++i)
                {
                    if(child[i].InnerText.Equals(strText, StringComparison.OrdinalIgnoreCase))
                    {
                        bFound = true;
                        nIndex = i;
                        break;
                    }
                }
                if(bFound == false)
                {
                    return false;
                }
                HtmlElement.Focus();
                HtmlElement.SetAttribute("selectedIndex", nIndex.ToString());
                SendKeys.SendWait("{TAB}");
                return true;
            }
            throw new Exception("SelectByText failed...");
        }
        /// <summary>
        /// Select类型的元素根据索引设置选项
        /// </summary>
        /// <returns></returns>
        public bool SelectByIndex(int nIndex)
        {
            if (WebElement != null)
            {
                SelectElement select = new SelectElement(WebElement);
                select.SelectByIndex(nIndex);
                return true;
            }
            if (HtmlElement != null)
            {
                HtmlElementCollection child = HtmlElement.Children.GetElementsByName("");
                if(nIndex >= child.Count)
                {
                    return false;
                }
                HtmlElement.Focus();
                HtmlElement.SetAttribute("selectedIndex", nIndex.ToString());
                SendKeys.SendWait("{TAB}");
                return true;
            }
            throw new Exception("SelectByIndex failed...");
        }
        /// <summary>
        /// 设置元素文字值
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        public bool SetText(string strText)
        {
            if (WebElement != null)
            {
                WebElement.SendKeys(strText);
                return true;
            }
            if (HtmlElement != null)
            {
                HtmlElement.SetAttribute("value", strText);
                return true;
            }
            throw new Exception("SetText failed...");
        }
    }
}
