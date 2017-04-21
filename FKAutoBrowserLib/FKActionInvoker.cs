//---------------------------------------------------------------
// author: FreeKnight
// date: 2017-3-30
//---------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Reflection;
//---------------------------------------------------------------
namespace FKAutoBrowser
{
    /// <summary>
    /// 执行一个动作
    /// Invokes the right Action for a function
    /// </summary>
    class FKActionInvoker
    {
        public FKActionInvoker() { }

        /// <summary>
        /// 执行一个动作对象
        /// Invoke a new instance of the type for this action
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IFKAction Invoke(FKAutoUnit autoUnitObject, string typeName, List<object> parameters)
        {
            if (typeName != null)
            {
                Type type = Type.GetType(typeName);
                if (type != null)
                {
                    try
                    {
                        // 创建动作对象
                        IFKAction action = type.InvokeMember("", BindingFlags.CreateInstance, null, this, parameters.ToArray()) as IFKAction;
                        action.AutoUnit = autoUnitObject;
                        return action;
                    }
                    catch (Exception ex)
                    {
                        autoUnitObject.Success = false;
                        autoUnitObject.Message = ex.Message;
                        if (ex.InnerException != null)
                        {
                            autoUnitObject.Message += ": " + ex.InnerException.Message;
                        }
                        return null;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 执行一个FKAction的预处理动作
        /// Run the PreAction() method of the FKAction
        /// </summary>
        /// <param name="autoUnitObject"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public IFKAction PreAction(FKAutoUnit autoUnitObject, IFKAction action)
        {
            try
            {
                action.PreAction();
            }
            catch (Exception ex)
            {
                autoUnitObject.Success = false;
                autoUnitObject.Message = ex.Message;
                if (ex.InnerException != null)
                {
                    autoUnitObject.Message += ": " + ex.InnerException.Message;
                }
            }
            return action;
        }

        /// <summary>
        /// 实际执行一个FKAction
        /// Run the Execute() method of the FKAction
        /// </summary>
        /// <param name="autoUnitObject"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public IFKAction Execute(FKAutoUnit autoUnitObject, IFKAction action)
        {
            try
            {
                // 实际执行动作
                action.Execute();

                // 记录动作执行结果
                autoUnitObject.Success = action.Success;
                autoUnitObject.Message = action.PostActionMessage;
            }
            catch (Exception ex)
            {
                autoUnitObject.Success = false;
                autoUnitObject.Message = ex.Message;
                if (ex.InnerException != null)
                {
                    autoUnitObject.Message += ": " + ex.InnerException.Message;
                }
            }
            return action;
        }

    }
}
