//---------------------------------------------------------------
// author: FreeKnight
// date: 2017-3-30
//---------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Threading;
//---------------------------------------------------------------
namespace FKAutoBrowser
{
    /// <summary>
    /// 暂停动作一段时间
    /// Pauses the processing of actions by a number of seconds
    /// </summary>
    public class FKPauseAction : FKBaseAction
    {
        private int Interval;

        public FKPauseAction(int interval)
        {
            Interval = interval;
        }
        public override void PreAction()
        {
            PreActionMessage = String.Format("Pausing for {0} seconds", Interval);
        }

        /// <summary>
        /// 执行动作
        /// Executes the action
        /// </summary>
        public override void Execute()
        {
            try
            {
                Success = true;
                Thread.Sleep(Interval * 1000);
                PostActionMessage = String.Format("暂停 {0} 秒完成", Interval);
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
                    Name = "等待秒数",
                    Type = typeof(int),
                    Description = "The number of seconds to pause test execution",
                    IsOptional = false,
                    DefaultValue = null
                });
                return parameters;
            }
        }
    }
}
