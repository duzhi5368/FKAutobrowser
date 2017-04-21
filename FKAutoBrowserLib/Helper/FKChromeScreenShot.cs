//---------------------------------------------------------------
// author: FreeKnight
// date: 2017-3-31
//---------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using OpenQA.Selenium;
//---------------------------------------------------------------
namespace FKAutoBrowser
{
    class FKChromeScreenShot
    {
        private IWebDriver _driver;

        /// <summary>
        /// 获取Chrome当前页面截图
        /// Gets a screenshot of the entire page for a Chrome browser window
        /// </summary>
        /// <remarks>
        /// Code from http://dev.flauschig.ch/wordpress/?p=341
        /// </remarks>
        /// <param name="test"></param>
        /// <returns></returns>
        public Bitmap GetScreenshot(IWebDriver driver)
        {
            _driver = driver;

            // 获取Document总大小
            int totalWidth = (int)EvalScript<long>("return Math.max(document.body.scrollWidth,document.documentElement.scrollWidth,document.body.offsetWidth,document.documentElement.offsetWidth,document.body.clientWidth,document.documentElement.clientWidth);");
            int totalHeight = (int)EvalScript<long>("return Math.max(document.body.scrollHeight,document.documentElement.scrollHeight,document.body.offsetHeight,document.documentElement.offsetHeight,document.body.clientHeight,document.documentElement.clientHeight);");

            // 获取视图大小
            int viewportWidth = (int)EvalScript<long>("return Math.max(document.body.clientWidth,document.documentElement.clientWidth);");
            int viewportHeight = (int)EvalScript<long>("return Math.max(document.body.clientHeight,document.documentElement.clientHeight);");

            // 进行窗口切分（若网页长度超过一个视口区域）
            List<Rectangle> rectangles = new List<Rectangle>();
            // 按照视图高度，循环处理
            for (int i = 0; i < totalHeight; i += viewportHeight)
            {
                int newHeight = viewportHeight;
                // 若元素高度太高
                if (i + viewportHeight > totalHeight)
                {
                    newHeight = totalHeight - i;
                }
                // 按照视图宽度，循环处理
                for (int ii = 0; ii < totalWidth; ii += viewportWidth)
                {
                    int newWidth = viewportWidth;
                    // 若元素宽度太宽
                    if (ii + viewportWidth > totalWidth)
                    {
                        newWidth = totalWidth - ii;
                    }

                    // 创建并记录矩阵列表
                    Rectangle currRect = new Rectangle(ii, i, newWidth, newHeight);
                    rectangles.Add(currRect);
                }
            }

            // 创建巨大图片
            var stitchedImage = new Bitmap(totalWidth, totalHeight);
            // 逐矩阵截图并记录组合
            Rectangle previous = Rectangle.Empty;
            foreach (var rectangle in rectangles)
            {
                // 计算需要的滚动值
                if (previous != Rectangle.Empty)
                {
                    int xDiff = rectangle.Right - previous.Right;
                    int yDiff = rectangle.Bottom - previous.Bottom;

                    // 开始滚动
                    EvalScript<object>(String.Format("window.scrollBy({0}, {1})", xDiff, yDiff));
                    System.Threading.Thread.Sleep(200);
                }

                // 截屏
                var screenshot = ((ITakesScreenshot)_driver).GetScreenshot();

                Image screenshotImage;
                using (MemoryStream memStream = new MemoryStream(screenshot.AsByteArray))
                {
                    screenshotImage = Image.FromStream(memStream);
                }

                // 计算本图在巨大图中的位置
                Rectangle sourceRectangle = new Rectangle(viewportWidth - rectangle.Width, viewportHeight - rectangle.Height, rectangle.Width, rectangle.Height);

                // 图片拷贝
                using (Graphics g = Graphics.FromImage(stitchedImage))
                {
                    g.DrawImage(screenshotImage, rectangle, sourceRectangle, GraphicsUnit.Pixel);
                }

                // 记录刚处理的矩形区
                previous = rectangle;
            }
            // 得到了巨大的全页面图
            return stitchedImage;
        }

        private T EvalScript<T>(string script)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            return (T)js.ExecuteScript(script);
        }
    }
}
