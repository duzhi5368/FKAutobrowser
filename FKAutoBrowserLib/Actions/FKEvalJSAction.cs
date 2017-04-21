//---------------------------------------------------------------
// author: FreeKnight
// date: 2017-3-31
//---------------------------------------------------------------
using System;
using System.Collections.Generic;
//---------------------------------------------------------------
namespace FKAutoBrowser
{
    /// <summary>
    /// 执行一段给定的JavsScript
    /// Executes the given JavaScript
    /// </summary>
    class FKEvalJSAction : FKBaseAction
    {
        private string Script;

        public FKEvalJSAction(string script)
        {
            if (!script.EndsWith(";")) script = script + ";";
            Script = script;
        }

        public override void PreAction()
        {
            PreActionMessage = "Executing JavaScript";
        }

        public override void Execute()
        {
            try
            {
                string strResult = AutoUnit.Browser.EvalJS(Script);
                PostActionMessage = string.Format("执行脚本完成. 结果为: {0}", strResult);
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
                    Name = "脚本文本",
                    Type = typeof(string),
                    Description = "The JavaScript to execute",
                    IsOptional = false,
                    DefaultValue = null
                });
                return parameters;
            }
        }
    }
}
