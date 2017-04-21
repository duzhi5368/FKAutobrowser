//---------------------------------------------------------------
// author: FreeKnight
// date: 2017-3-31
//---------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
//---------------------------------------------------------------
namespace FKAutoBrowser
{
    /// <summary>
    /// 关闭当前选项卡
    /// </summary>
    class FKCloseCurTabAction : FKBaseAction
    {
        public override void PreAction()
        {
            PreActionMessage = "Closing the current tab";
        }

        public override void Execute()
        {
            try
            {
                Success = true;
                Thread.Sleep(ACTION_IDLE_TIME);
                SendKeys.SendWait("^{w}");
                Thread.Sleep(ACTION_IDLE_TIME);
                PostActionMessage = "关闭当前选项卡完成";
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
