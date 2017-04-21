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
    /// 刷新当前页面
    /// Refreshes the current page
    /// </summary>
    public class FKRefreshAction : FKBaseAction
    {
        public override void PreAction()
        {
            PreActionMessage = "Refreshing the page";
        }

        /// <summary>
        /// 执行本行为
        /// Executes the action
        /// </summary>
        public override void Execute()
        {
            try
            {
                AutoUnit.Browser.Refresh();
                Success = true;
                PostActionMessage = "页面刷新完成";
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
