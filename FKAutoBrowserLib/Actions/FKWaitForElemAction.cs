//---------------------------------------------------------------
// author: FreeKnight
// date: 2017-4-1
//---------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
//---------------------------------------------------------------
namespace FKAutoBrowser
{
    /// <summary>
    /// 持续等待，直到指定元素可见
    /// Waits until the given element is present on the page
    /// </summary>
    class FKWaitForElemAction : FKBaseAction
    {
        /// <summary>
        /// 等待超时时间，默认为30秒
        /// The timeout period in seconds, default: 30
        /// </summary>
        public int Timeout = 30;

        public FKWaitForElemAction(string selector)
        {
            Selector = selector;
        }
        public FKWaitForElemAction(string selector, int timeout)
        {
            Selector = selector;
            Timeout = timeout;
        }

        public override void PreAction()
        {
            PreActionMessage = String.Format("Waiting until selector '{0}' finds a valid element, timeout: {1}", Selector, Timeout);
        }

        public override void Execute()
        {
            try
            {
                AutoUnit.CurrentElements = AutoUnit.Browser.FindElements(Selector, Timeout);
                if (AutoUnit.CurrentElements.Any())
                {
                    PostActionMessage = String.Format("已找到指定元素 '{0}'", Selector);
                    Success = true;
                }
                else
                {
                    PostActionMessage = String.Format("等待 {0} 秒超时，依然无法找到指定元素 '{1}'", Timeout, Selector);
                    Success = false;
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
                    Name = "元素XPath",
                    Type = typeof(string),
                    Description = "The selector to search for",
                    IsOptional = false,
                    DefaultValue = null
                });
                parameters.Add(new FKActionParameter
                {
                    Name = "超时时间",
                    Type = typeof(int),
                    Description = "The number of seconds to wait until timing out",
                    IsOptional = true,
                    DefaultValue = 30
                });
                return parameters;
            }
        }
    }
}
