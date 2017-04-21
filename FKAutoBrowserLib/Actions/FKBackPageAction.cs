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
    /// 返回上一页面
    /// Goes the the previous page
    /// </summary>
    public class FKBackPageAction : FKBaseAction
    {
        public override void PreAction()
        {
            PreActionMessage = "Going to the previous page";
        }

        /// <summary>
        /// 执行该动作
        /// Executes the action
        /// </summary>
        public override void Execute()
        {
            try
            {
                Success = AutoUnit.Browser.BackPage();
                PostActionMessage = "返回上一页面完成";
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
