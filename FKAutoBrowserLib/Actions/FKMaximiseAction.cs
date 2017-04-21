//---------------------------------------------------------------
// author: FreeKnight
// date: 2017-3-31
//---------------------------------------------------------------
using System;
using System.Collections.Generic;
//---------------------------------------------------------------
namespace FKAutoBrowser
{
    /// <summary>
    /// 最大化浏览器窗口
    /// </summary>
    class FKMaximiseAction : FKBaseAction
    {
        public override void PreAction()
        {
            PreActionMessage = "Maximising the window";
        }

        public override void Execute()
        {
            try
            {
                // 记录当前浏览器大小
                AutoUnit.BrowserOldSize = AutoUnit.Browser.Size();

                Success = AutoUnit.Browser.Maximize();
                PostActionMessage = "最大化浏览器窗口完成";
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
