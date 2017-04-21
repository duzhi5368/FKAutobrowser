//---------------------------------------------------------------
// author: FreeKnight
// date: 2017-3-30
//---------------------------------------------------------------
using System;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
//---------------------------------------------------------------
namespace FKAutoBrowser
{
    /// <summary>
    /// 对Selenium WebDriver的加强
    /// Extensions for a web driver instance
    /// </summary>
    /// <remarks>
    /// From: http://stackoverflow.com/questions/6992993/selenium-c-sharp-webdriver-wait-until-element-is-present
    /// </remarks>
    public static class FKSeleniumWebDriverEx
    {
        /// <summary>
        /// 等待一个元素可见
        /// </summary>
        /// <param name="driver">Selenium WebDriver实例</param>
        /// <param name="by">等待的元素对象</param>
        /// <param name="timeoutInSeconds">最大等待超时时间</param>
        /// <returns></returns>
        public static IWebElement FindElement(this IWebDriver driver, By by, int timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv => drv.FindElement(by));
            }
            return driver.FindElement(by);
        }
        /// <summary>
        /// 等待一个元素可见
        /// </summary>
        /// <param name="driver">Selenium WebDriver实例</param>
        /// <param name="by">等待的元素对象</param>
        /// <param name="timeoutInSeconds">最大等待超时时间</param>
        /// <returns></returns>
        public static ReadOnlyCollection<IWebElement> FindElements(this IWebDriver driver, By by, int timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv => (drv.FindElements(by).Count > 0) ? drv.FindElements(by) : null);
            }
            return driver.FindElements(by);
        }
    }
}
