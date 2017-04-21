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
    ///  最小化浏览器窗口
    /// </summary>
    class FKMinimiseAction : FKBaseAction
    {
        public override void PreAction()
        {
            PreActionMessage = "Minimising the window";
        }

        public override void Execute()
        {
            try
            {
                // 记录当前浏览器大小
                AutoUnit.BrowserOldSize = AutoUnit.Browser.Size();

                Success = AutoUnit.Browser.Minimize();
                PostActionMessage = "最小化浏览器窗口完成";
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
