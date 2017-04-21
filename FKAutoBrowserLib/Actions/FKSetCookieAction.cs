//---------------------------------------------------------------
// author: FreeKnight
// date: 2017-4-1
//---------------------------------------------------------------
using System;
using System.Collections.Generic;
//---------------------------------------------------------------
namespace FKAutoBrowser
{
    /// <summary>
    /// 设置指定Cookie的值
    /// Sets the value of a cookie
    /// </summary>
    class FKSetCookieAction : FKBaseAction
    {
        public string CookieName;
        public string Value;

        public FKSetCookieAction(string cookieName, string value)
        {
            CookieName = cookieName.Trim().Trim('\'');
            Value = value.Trim().Trim('\'');
        }

        public override void PreAction()
        {
            PreActionMessage = String.Format("Setting the value of cookie '{0}' to '{1}'", CookieName, Value);
        }

        public override void Execute()
        {
            try
            {
                bool bSuccessed = AutoUnit.Browser.SetCookieValue(CookieName, Value);
                if (!bSuccessed)
                {
                    Success = false;
                    PostActionMessage = String.Format("设置Cookie '{0}' 失败", CookieName);
                }
                else
                {
                    Success = true;
                    PostActionMessage = String.Format("设置Cookie '{0}' 值: '{1}' 完成", CookieName, Value);
                }
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
                    Name = "cookie字段名",
                    Type = typeof(string),
                    Description = "The name of the cookie to set",
                    IsOptional = false,
                    DefaultValue = null
                });
                parameters.Add(new FKActionParameter
                {
                    Name = "值",
                    Type = typeof(string),
                    Description = "The value of the cookie",
                    IsOptional = false,
                    DefaultValue = null
                });
                return parameters;
            }
        }
    }
}
