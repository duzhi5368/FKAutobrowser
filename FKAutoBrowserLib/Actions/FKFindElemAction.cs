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
    /// 查找一个元素
    /// </summary>
    class FKFindElemAction : FKBaseAction
    {
        public int Timeout;

        /// <summary>
        /// 查找一个元素
        /// Find a single element
        /// </summary>
        /// <returns></returns>
        public FKFindElemAction(string selector)
        {
            Selector = selector.Trim().Trim('\'');
        }

        /// <summary>
        /// 查找一个元素，并有超时检查
        /// Find a single element, waiting for the specified period for the element to appear
        /// </summary>
        /// <returns></returns>
        public FKFindElemAction(string selector, int timeout)
        {
            Selector = selector.Trim().Trim('\'');
            Timeout = timeout;
        }

        public override void PreAction()
        {
            PreActionMessage = String.Format("Finding the element(s) matching this selector: {0}[{1}]", Selector, AutoUnit.CurrentElementIndex);
        }

        public override void Execute()
        {
            try
            {
                if (Timeout == 0)
                {
                    AutoUnit.CurrentElements = AutoUnit.Browser.FindElements(Selector);
                }
                else
                {
                    AutoUnit.CurrentElements = AutoUnit.Browser.FindElements(Selector, Timeout);
                }
                if (AutoUnit.CurrentElements.Any())
                {
                    Success = true;
                    PostActionMessage = String.Format("根据当前条件 '{0}'，找到 {1} 个元素",  Selector, AutoUnit.CurrentElements.Count());
                }
                else
                {
                    Success = false;
                    PostActionMessage = String.Format("根据当前条件 '{0}'，找不到任何元素", Selector);
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
                    Description = "The number of seconds to wait for the element to be visible until timing out",
                    IsOptional = true,
                    DefaultValue = 0
                });
                return parameters;
            }
        }
    }
}
