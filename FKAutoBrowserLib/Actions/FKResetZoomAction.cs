//---------------------------------------------------------------
// author: FreeKnight
// date: 2017-3-31
//---------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
//---------------------------------------------------------------
namespace FKAutoBrowser
{
    /// <summary>
    /// 重置网页大小比例
    /// </summary>
    class FKResetZoomAction : FKBaseAction
    {
        public override void PreAction()
        {
            PreActionMessage = "Resetting zoom";
        }

        public override void Execute()
        {
            try
            {
                Success = true;
                Thread.Sleep(ACTION_IDLE_TIME);
                SendKeys.SendWait("^{0}");
                Thread.Sleep(ACTION_IDLE_TIME);
                PostActionMessage = "恢复网页大小为100%完成";
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
