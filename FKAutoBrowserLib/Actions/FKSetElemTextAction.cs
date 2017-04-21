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
    /// 设置当前元素文字内容
    /// Types text into the current element
    /// </summary>
    class FKSetElemTextAction : FKBaseAction
    {
        public string Text;

        public FKSetElemTextAction(string text)
        {
            Text = text;
        }

        /// <summary>
        /// 对指定元素，设置其文字内容值
        /// Types text into the elements matching the given selector
        /// </summary>
        public FKSetElemTextAction(string selector, string text)
        {
            Selector = selector;
            Text = text;
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
                    try
                    {
                        if (element.SetText(Text))
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

                // 没有元素成功
                if (succeededElements == 0)
                {
                    Success = false;
                    PostActionMessage = lastException == null ?
                        "当前选中元素没有可以正确输入的元素" :
                        String.Format("当前选中元素没有可以正确输入的元素 ( 错误: {0})", lastException.Message);
                    return;
                }

                // 最少有一个元素成功了
                PostActionMessage = String.Format(
                        "向元素输入文本字符 '{0}'， {1} 个元素成功, {2} 个元素失败",
                        Text,
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
                    Name = "输入文本",
                    Type = typeof(string),
                    Description = "The text to type",
                    IsOptional = false,
                    DefaultValue = null
                });
                return parameters;
            }
        }
    }
}
