//---------------------------------------------------------------
// author: FreeKnight
// date: 2017-3-30
//---------------------------------------------------------------
using System.Collections.Generic;
using System.Linq;
using System.Text;
//---------------------------------------------------------------
namespace FKAutoBrowser
{
    /// <summary>
    /// 动作基本类型
    /// Represents the details of an action type that can be performed
    /// </summary>
    public class FKActionType
    {
        public FKActionType(string functionName, string methodName)
        {
            Name = functionName;
            FunctionName = functionName;
            MethodName = methodName;
        }

        /// <summary>
        /// 动作名
        /// The name of this action
        /// </summary>
        public string Name;

        /// <summary>
        /// 动作函数名
        /// The name of the function that will call this action
        /// </summary>
        public string FunctionName;

        /// <summary>
        /// 动作方法名
        /// The fully-qualified method name that will be invoked for this action type
        /// </summary>
        public string MethodName;

        /// <summary>
        /// 动作类型文字描述
        /// The description of the action type
        /// </summary>
        public string Description;

        /// <summary>
        /// 动作全部参数
        /// The lost of parameters this action type takes
        /// </summary>
        public List<FKActionParameter> Parameters;

        /// <summary>
        /// 动作的详细信息输出
        /// Gets the string of parameters with their types and names for display in the autocomplete list
        /// </summary>
        public string ParameterString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("(");
                if (Parameters != null && Parameters.Any())
                {
                    int x = 0;
                    foreach (FKActionParameter parameter in Parameters)
                    {
                        if (x > 0)
                        {
                            sb.Append(", ");
                        }
                        sb.Append(parameter.Type.Name.ToString());
                        if (parameter.IsOptional)
                        {
                            sb.Append("?");
                        }
                        sb.Append(" ");
                        sb.Append(parameter.Name);
                        x++;
                    }
                }
                sb.Append(")");
                return sb.ToString();
            }
        }
    }
}
