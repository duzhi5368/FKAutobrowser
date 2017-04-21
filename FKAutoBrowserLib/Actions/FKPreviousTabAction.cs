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
    /// 进入上一个选项卡
    /// </summary>
    class FKPreviousTabAction : FKBaseAction
    {
        public override void PreAction()
        {
            PreActionMessage = "Going to the previous tab";
        }

        public override void Execute()
        {
            try
            {
                if (AutoUnit.Browser.IsSupportMutilTab())
                {
                    Success = true;
                    Thread.Sleep(ACTION_IDLE_TIME);
                    SendKeys.SendWait("^+{TAB}");
                    Thread.Sleep(ACTION_IDLE_TIME);
                    PostActionMessage = "打开上一个选项卡";
                }
                else
                {
                    Success = true;
                    PostActionMessage = String.Format("本模式浏览器不支持多Tab页面...打开上一个选项卡行为取消");
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
