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
    /// 进入下一页面
    /// Goes the the next page (after previously going back)
    /// </summary>
    public class FKForwardPageAction : FKBaseAction
    {
        public override void PreAction()
        {
            PreActionMessage = "Going forward to the next page";
        }

        /// <summary>
        /// 执行动作
        /// Executes the action
        /// </summary>
        public override void Execute()
        {
            try
            {
                Success = AutoUnit.Browser.ForwardPage();
                PostActionMessage = "前进到下一页面完成";
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
