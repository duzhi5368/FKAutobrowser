//---------------------------------------------------------------
// author: FreeKnight
// date: 2017-3-30
//---------------------------------------------------------------
using System;
using System.Collections.Generic;
//---------------------------------------------------------------
namespace FKAutoBrowser
{
    /// <summary>
    /// 关闭当前浏览器实例
    /// Closes the current browser instance
    /// </summary>
    public class FKCloseAction : FKBaseAction
    {
        public override void PreAction()
        {
            PreActionMessage = "Closing the browser";
        }

        /// <summary>
        /// Executes the action
        /// </summary>
        public override void Execute()
        {
            try
            {
                AutoUnit.Browser.Close();
                AutoUnit.Browser = null;
                Success = true;
                PostActionMessage = "关闭浏览器完成";
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
