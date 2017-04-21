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
    /// 为当前元素输入一个西方的LastName
    /// Types a random last name into the current element
    /// </summary>
    class FKSetElemWesternLNTextAction : FKBaseAction
    {
        public FKSetElemWesternLNTextAction()
        {
        }

        public FKSetElemWesternLNTextAction(string selector)
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
                        FKRandomiser random = new FKRandomiser();
                        string name = random.GetWesternLastName();
                        if (element.SetText(name))
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
                        "向元素输入随机西方LastName, {0} 个元素成功, {1} 个元素失败",
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
                return parameters;
            }
        }
    }
}
