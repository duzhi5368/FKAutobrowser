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
    /// 点击当前指定元素
    /// Activates a click event on the current selected element
    /// </summary>
    class FKClickAction : FKBaseAction
    {
        public FKClickAction(string selector)
        {
            Selector = selector;
        }

        public override void PreAction()
        {
            FindElements(Selector);
        }

        public override void Execute()
        {
            try
            {
                if (FirstElement != null)
                {
                    Success = true;
                    FirstElement.Click();
                    PostActionMessage = String.Format("点击指定元素: <{0}>",
                        FirstElement.TagName());
                }
                else
                {
                    PostActionMessage = "当前没有选中任何元素";
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
                return parameters;
            }
        }
    }
}
