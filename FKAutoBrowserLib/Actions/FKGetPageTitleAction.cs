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
    /// 获取当前页面标题名
    /// Returns the title of the current page
    /// </summary>
    class FKGetPageTitleAction : FKBaseAction
    {
        public override void PreAction()
        {
            PreActionMessage = "Getting the page title";
        }

        /// <summary>
        /// Executes the action
        /// </summary>
        public override void Execute()
        {
            try
            {
                string strTitle = AutoUnit.Browser.GetPageTitle();
                if (strTitle == "" || strTitle == string.Empty || strTitle == null)
                {
                    PostActionMessage = "找不到本页标题";
                    Success = false;
                }
                else
                {
                    PostActionMessage = String.Format("当前页面标题为 {0} ", strTitle);
                    Success = true;
                }
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
