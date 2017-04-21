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
    /// 向元素中写入指定随机字符
    /// Types random alphabetical text into the current element
    /// </summary>
    class FKSetElemRandomTextAction : FKBaseAction
    {
        public int MinLength = 2;
        public int MaxLength = 10;
        public bool WithSpaces = false;

        public FKSetElemRandomTextAction(int maxlength)
        {
            MaxLength = maxlength;
        }

        /// <summary>
        /// 向元素中写入指定长度的随机字符
        /// Types random text into the current element with the given minimum and maximum lengths
        /// </summary>
        public FKSetElemRandomTextAction(int minlength, int maxlength)
        {
            MinLength = minlength;
            MaxLength = maxlength;
        }

        /// <summary>
        /// 向指定元素中写入指定长度范围的随机字符
        /// Types random text into the elements matching the given selector with the given maximum length
        /// </summary>
        public FKSetElemRandomTextAction(string selector, int maxlength)
        {
            Selector = selector;
            MaxLength = maxlength;
        }

        /// <summary>
        /// 向指定元素中写入指定长度范围的随机字符
        /// Types random text into the elements matching the given selector with the given minimum and maximum lengths
        /// </summary>
        public FKSetElemRandomTextAction(string selector, int minlength, int maxlength)
        {
            Selector = selector;
            MinLength = minlength;
            MaxLength = maxlength;
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
                        // 生成随机字符
                        FKRandomiser random = new FKRandomiser();
                        string randomText = random.GetAlphabeticalText(MinLength, MaxLength, WithSpaces);
                        if (element.SetText(randomText))
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
                        "向元素输入随机长度为 {0}-{1} 文本字符 ， {2} 个元素成功, {3} 个元素失败",
                        MinLength, MaxLength,
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
                    Name = "输入字符最小长度",
                    Type = typeof(int),
                    Description = "The minimum length of the random string",
                    IsOptional = true,
                    DefaultValue = 2
                });
                parameters.Add(new FKActionParameter
                {
                    Name = "输入字符最大长度",
                    Type = typeof(int),
                    Description = "The maximum length of the random string",
                    IsOptional = false,
                    DefaultValue = 10
                });
                return parameters;
            }
        }
    }
}
