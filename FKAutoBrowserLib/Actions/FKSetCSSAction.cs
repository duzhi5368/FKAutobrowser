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
    /// 修改CSS样式
    /// </summary>
    class FKSetCSSAction : FKBaseAction
    {
        private string Styles;

        public FKSetCSSAction(string styles)
        {
            Styles = styles;
        }

        public override void PreAction()
        {
            PreActionMessage = "Applying CSS styles";
        }

        public override void Execute()
        {
            try
            {
                AutoUnit.Browser.SetCSSStyle(Styles);
                PostActionMessage = "设置新CSS样式完成";
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
                    Name = "CSS样式文本",
                    Type = typeof(string),
                    Description = "The CSS styles to apply",
                    IsOptional = false,
                    DefaultValue = null
                });
                return parameters;
            }
        }
    }
}
