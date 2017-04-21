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
    ///  高亮当前指定元素
    /// </summary>
    class FKHighlightElemAction : FKBaseAction
    {
        public FKHighlightElemAction()
        {
            Selector = string.Empty;
        }
        public FKHighlightElemAction(string selector)
        {
            Selector = selector;
        }

        public override void PreAction()
        {
            if(Selector != string.Empty)
            {
                FindElements(Selector);
            }
            PreActionMessage = "Highlighting the current element(s)";
        }

        public override void Execute()
        {
            try
            {
                IEnumerable<IFKWebElement> ele = Elements;
                if(ele == null || !ele.Any()) {
                    ele = AutoUnit.CurrentElements;
                }
                if (ele == null || !ele.Any())
                {
                    PostActionMessage = "当前没有选中任何元素";
                    Success = false;
                    return;
                }

                int x = 0;
                foreach (IFKWebElement el in ele)
                {
                    AutoUnit.Browser.SetHighlightElem(el);
                    x++;
                }
                PostActionMessage = String.Format(" {0} 个元素被设置为高亮", x);
                Success = true;
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
