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
    /// 缩小网页显示
    /// </summary>
    class FKZoomInAction : FKBaseAction
    {
        public override void PreAction()
        {
            PreActionMessage = "Zooming in";
        }

        public override void Execute()
        {
            try
            {
                Success = true;
                Thread.Sleep(ACTION_IDLE_TIME);
                SendKeys.SendWait("^{+}");
                Thread.Sleep(ACTION_IDLE_TIME);
                PostActionMessage = "放大网页完成";
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
