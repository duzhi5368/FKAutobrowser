//---------------------------------------------------------------
// author: FreeKnight
// date: 2017-3-30
//---------------------------------------------------------------
using System;
//---------------------------------------------------------------
namespace FKAutoBrowser
{
    /// <summary>
    /// 动作参数
    /// Represents the details of a parameter that can be passed to an action type
    /// </summary>
    public class FKActionParameter
    {
        /// <summary>
        /// 参数名
        /// The name of this parameter
        /// </summary>
        public string Name;

        /// <summary>
        /// 参数描述
        /// The description of this parameter
        /// </summary>
        public string Description;

        /// <summary>
        /// 参数类型
        /// The type of this parameter
        /// </summary>
        public Type Type;

        /// <summary>
        /// 本参数是否是可选参数
        /// Whether this parameter is optional
        /// </summary>
        public bool IsOptional;

        /// <summary>
        /// 本参数默认值
        /// The default value of this parameter
        /// </summary>
        public object DefaultValue;
    }
}
