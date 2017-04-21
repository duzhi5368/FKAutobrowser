//---------------------------------------------------------------
// author: FreeKnight
// date: 2017-3-31
//---------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Drawing;
//---------------------------------------------------------------
namespace FKAutoBrowser
{
    class FKResizeAction : FKBaseAction
    {
        private int Width;
        private int Height;

        /// <summary>
        /// 重置浏览器窗口大小
        /// Resizes the browser window
        /// </summary>
        /// <param name="width">The new window width</param>
        /// <param name="height">The new window height</param>
        public FKResizeAction(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public override void PreAction()
        {
            PreActionMessage = String.Format("Resizing window to {0}x{1}", Width, Height);
        }

        public override void Execute()
        {
            try
            {
                // 记录当前浏览器大小
                AutoUnit.BrowserOldSize = AutoUnit.Browser.Size();

                Success = AutoUnit.Browser.Resize(new Size(Width, Height));
                PostActionMessage = String.Format("重新设置浏览器窗口大小 {0}x{1}", Width, Height);
            }
            catch (Exception ex)
            {
                PostActionMessage = ex.Message;
                Success = false;
            }
        }

        /// <summary>
        /// 函数参数
        /// The parameters for this method
        /// </summary>
        internal static List<FKActionParameter> Parameters
        {
            get
            {
                List<FKActionParameter> parameters = new List<FKActionParameter>();
                parameters.Add(new FKActionParameter
                {
                    Name = "宽度像素",
                    Type = typeof(int),
                    Description = "The new width of the window",
                    IsOptional = false,
                    DefaultValue = null
                });
                parameters.Add(new FKActionParameter
                {
                    Name = "高度像素",
                    Type = typeof(int),
                    Description = "The new height of the window",
                    IsOptional = false,
                    DefaultValue = null
                });
                return parameters;
            }
        }
    }
}
