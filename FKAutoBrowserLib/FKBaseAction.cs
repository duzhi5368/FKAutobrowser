//---------------------------------------------------------------
// author: FreeKnight
// date: 2017-3-30
//---------------------------------------------------------------
using System.Collections.Generic;
using System.Linq;
//---------------------------------------------------------------
namespace FKAutoBrowser
{
    /// <summary>
    /// FKAutoUnit的动作基类，强制被所有动作继承
    /// The base Action from which all internal FKAutoUnit Action derive
    /// </summary>
    public abstract class FKBaseAction : IFKAction
    {
        public const int ACTION_IDLE_TIME = 300;                // 动作间闲置时间
        public const string SAVE_HTML_DIR = "\\PageSave\\";     // 保存页面所在目录文件夹
        public const string SCREENSHOT_DIR = "\\Screenshot\\";  // 截图所在目录文件夹
        /// <summary>
        /// 获取本Action所属的FKAutoUnit单元
        /// Gets or sets the FKAutoUnit that this Action belongs to
        /// </summary>
        public FKAutoUnit AutoUnit { get; set; }

        /// <summary>
        /// 执行该动作之前的输出信息
        /// Gets or sets the message shown just before the action is invoked
        /// </summary>
        public string PreActionMessage { get; set; }

        /// <summary>
        /// 执行该动作之后的输出信息
        /// Gets or sets the message shown just after the action is invoked
        /// </summary>
        public string PostActionMessage { get; set; }

        /// <summary>
        /// 该动作是否执行成功
        /// Gets or sets a boolean value indicating the succes or failure of the Action
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 该动作执行前的一些预处理函数
        /// The method that is called just before the action is executed
        /// </summary>
        public abstract void PreAction();

        /// <summary>
        /// 动作执行函数
        /// The method that is invoked when the Action is run
        /// </summary>
        public abstract void Execute();

        /// <summary>
        /// 查找元素的选择器
        /// The selector given to find the elements on which to perform this action
        /// </summary>
        public string Selector;

        /// <summary>
        /// 该动作将操作的元素列表
        /// The elements on which the action will be performed
        /// </summary>
        public IEnumerable<IFKWebElement> Elements;

        /// <summary>
        /// 获取本动作将操作的第一个元素
        /// Gets the first element in the elements found for the current selection
        /// </summary>
        public IFKWebElement FirstElement
        {
            get
            {
                IEnumerable<IFKWebElement> elements = GetElements();
                if (elements == null)
                {
                    return null;
                }

                return elements.FirstOrDefault();
            }
        }

        /// <summary>
        /// 获取本动作将进行操作的最后一个元素
        /// Gets the last element in the elements found for the current selection
        /// </summary>
        public IFKWebElement LastElement
        {
            get
            {
                IEnumerable<IFKWebElement> elements = GetElements();
                if (elements == null)
                {
                    return null;
                }

                return elements.LastOrDefault();
            }
        }

        /// <summary>
        /// 查找一个指定的元素
        /// Finds elements with the given selector and assigns them to the Elements property
        /// </summary>
        /// <param name="selector"></param>
        protected void FindElements(string selector)
        {
            if (!string.IsNullOrWhiteSpace(selector))
            {
                Elements = AutoUnit.Browser.FindElements(selector);
            }
        }

        /// <summary>
        /// 获取正在被使用操作的元素对象
        /// Gets the elements to be used
        /// </summary>
        /// <returns></returns>
        protected IEnumerable<IFKWebElement> GetElements()
        {
            if (Elements != null && Elements.Any())
            {
                return Elements;
            }

            return AutoUnit.CurrentElements;
        }
    }
}
