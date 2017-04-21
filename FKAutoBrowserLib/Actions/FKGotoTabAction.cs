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
    class FKGotoTabAction : FKBaseAction
    {
        private int TabNumber;

        /// <summary>
        /// 打开指定编号的选项卡
        /// Go to the tab with the specified number
        /// </summary>
        public FKGotoTabAction(int tabNumber)
        {
            TabNumber = tabNumber;
        }

        public override void PreAction()
        {
            PreActionMessage = "Going to the tab number " + TabNumber;
        }

        public override void Execute()
        {
            try
            {
                if (AutoUnit.Browser.IsSupportMutilTab())
                {
                    Success = true;
                    Thread.Sleep(ACTION_IDLE_TIME);
                    SendKeys.SendWait(String.Format("^{0}", TabNumber));
                    Thread.Sleep(ACTION_IDLE_TIME);
                    PostActionMessage = "打开选项卡 " + TabNumber + " 完成";
                }
                else
                {
                    Success = true;
                    PostActionMessage = String.Format("本模式浏览器不支持多Tab页面...Tab页面切换执行无效");
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
                parameters.Add(new FKActionParameter
                {
                    Name = "Tab页面编号,从1开始",
                    Type = typeof(int),
                    Description = "The tab number to go to",
                    IsOptional = false,
                    DefaultValue = null
                });
                return parameters;
            }
        }
    }
}
