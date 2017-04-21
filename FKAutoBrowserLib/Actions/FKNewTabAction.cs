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
    class FKNewTabAction : FKBaseAction
    {
        private string URI;

        /// <summary>
        /// 打开一个新选项卡，并在该选项卡打开新页面
        /// Opens a new tab with a specified URL
        /// </summary>
        public FKNewTabAction(string uri)
        {
            URI = uri;
        }

        public override void PreAction()
        {
            PreActionMessage = String.Format("Opening a new tab for {0}", URI);
        }

        public override void Execute()
        {
            try
            {
                if(AutoUnit.Browser.IsSupportMutilTab())
                {
                    Thread.Sleep(ACTION_IDLE_TIME);
                    // 打开新选项卡
                    SendKeys.SendWait("^{t}");
                    // 休眠500毫秒
                    Thread.Sleep(ACTION_IDLE_TIME);
                    // 选择地址栏
                    SendKeys.SendWait("%{d}");
                    // 休眠500毫秒
                    Thread.Sleep(ACTION_IDLE_TIME);
                    // 输入新地址
                    SendKeys.SendWait(URI);
                    // 休眠500毫秒
                    Thread.Sleep(ACTION_IDLE_TIME);
                    // 按下回车
                    SendKeys.SendWait("{ENTER}");
                    Thread.Sleep(ACTION_IDLE_TIME);

                    Success = true;
                    PostActionMessage = String.Format("打开新选项卡并打开链接 {0} 完成", URI);
                }
                else
                {
                    AutoUnit.Browser.OpenUrl(URI);
                    Success = true;
                    PostActionMessage = String.Format("本模式浏览器不支持多Tab页面...当前打开链接 {0} 完成", URI);
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
                    Name = "链接Url",
                    Type = typeof(string),
                    Description = "The URI to open in the new tab",
                    IsOptional = false,
                    DefaultValue = null
                });
                return parameters;
            }
        }
    }
}
