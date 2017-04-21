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
    /// <summary>
    /// 重置浏览器窗口大小
    /// </summary>
    class FKResetSizeAction : FKBaseAction
    {
        public override void PreAction()
        {
            PreActionMessage = "Resetting the window size";
        }

        public override void Execute()
        {
            try
            {
                Size oldSize = AutoUnit.BrowserOldSize;
                // 记录当前浏览器大小
                AutoUnit.BrowserOldSize = AutoUnit.Browser.Size();

                Success = AutoUnit.Browser.Resize(oldSize);
                PostActionMessage = String.Format("重置浏览器窗口大小 {0} * {1}", oldSize.Width, oldSize.Height );
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
                return parameters;
            }
        }
    }
}
