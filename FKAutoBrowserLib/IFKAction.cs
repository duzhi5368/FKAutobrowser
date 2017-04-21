//---------------------------------------------------------------
// author: FreeKnight
// date: 2017-3-30
//---------------------------------------------------------------
namespace FKAutoBrowser
{
    /// <summary>
    /// FKAutoUnit的动作基类，强制被所有动作继承
    /// The base Action from which all FKAutoUnit Actions must derive
    /// </summary>
    public interface IFKAction
    {
        /// <summary>
        /// 当前执行的FKAutoUnit
        /// The currently executing FKAutoUnit
        /// </summary>
        FKAutoUnit AutoUnit { get; set; }

        /// <summary>
        /// 执行该动作之前的输出信息
        /// The message written just before the action is invoked
        /// </summary>
        string PreActionMessage { get; set; }

        /// <summary>
        /// 执行该动作之后的输出信息
        /// The message written just after the action is invoked
        /// </summary>
        string PostActionMessage { get; set; }

        /// <summary>
        /// 该动作是否执行成功
        /// A value indicating whether the action has been successful
        /// </summary>
        bool Success { get; set; }

        /// <summary>
        /// 该动作执行前的一些预处理函数
        /// The method which is called just before the action is executed
        /// </summary>
        void PreAction();

        /// <summary>
        /// 动作执行函数
        /// The method that executes the action
        /// </summary>
        void Execute();
    }
}
