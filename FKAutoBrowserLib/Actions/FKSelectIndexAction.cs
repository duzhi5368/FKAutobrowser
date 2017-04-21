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
    /// 为当前元素选择指定选项
    /// Selects the option at the given index in the current select element
    /// </summary>
    class FKSelectIndexAction : FKBaseAction
    {
        public int Index;

        public FKSelectIndexAction(int index)
        {
            Index = index;
        }

        /// <summary>
        /// 对指定元素，设置指定选项值
        /// Selects the text in the elements matching the given selector
        /// </summary>
        public FKSelectIndexAction(string selector, int index)
        {
            Selector = selector;
            Index = index;
        }

        public override void PreAction()
        {
            FindElements(Selector);
        }

        public override void Execute()
        {
            try
            {
                IEnumerable<IFKWebElement> elements = GetElements();
                if (elements == null || !elements.Any())
                {
                    PostActionMessage = "当前没有选中任何元素";
                    Success = false;
                    return;
                }

                int succeededElements = 0;
                int failedElements = 0;
                Exception lastException = null;

                foreach (IFKWebElement element in elements)
                {
                    string tag = element.TagName().ToLower();
                    if (tag != "select")
                    {
                        failedElements++;
                    }
                    else
                    {
                        try
                        {
                            if (element.SelectByIndex(Index))
                            {
                                succeededElements++;
                            }
                            else
                            {
                                failedElements++;
                            }
                        }
                        catch (Exception ex)
                        {
                            lastException = ex;
                            failedElements++;
                        }
                    }
                }

                // 没有元素成功
                if (succeededElements == 0)
                {
                    Success = false;
                    PostActionMessage = lastException == null ?
                        "当前选中元素没有 select 类型元素" :
                        String.Format("没有 select 类型元素被选择 ( 错误: {0}",
                        lastException.Message);
                    return;
                }

                // 最少有一个元素成功了
                PostActionMessage = String.Format(
                        "设置元素选项值为{0}，{1} 个元素成功, {2} 个元素失败",
                        Index,
                        succeededElements,
                        failedElements);
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
                    IsOptional = true,
                    DefaultValue = null
                });
                parameters.Add(new FKActionParameter
                {
                    Name = "索引值",
                    Type = typeof(int),
                    Description = "The index of the option to select",
                    IsOptional = false,
                    DefaultValue = null
                });
                return parameters;
            }
        }
    }
}
