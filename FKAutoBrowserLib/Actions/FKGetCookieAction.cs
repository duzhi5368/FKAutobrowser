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
    /// 从Cookie中获取一个值
    /// Gets the value of a cookie
    /// </summary>
    class FKGetCookieAction : FKBaseAction
    {
        public string CookieName;

        public FKGetCookieAction(string cookieName)
        {
            CookieName = cookieName.Trim().Trim('\'');
        }

        public override void PreAction()
        {
            PreActionMessage = String.Format("Getting the value from cookie: {0}", CookieName);
        }

        public override void Execute()
        {
            try
            {
                string value = AutoUnit.Browser.GetCookieValue(CookieName);
                if(value == "")
                {
                    Success = false;
                    PostActionMessage = String.Format("查找Cookie '{0}' 未曾找到", CookieName);
                }
                else
                {
                    Success = true;
                    PostActionMessage = String.Format("找到Cookie '{0}' 值: '{1}'", CookieName, value);
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
                    Description = "The name of the cookie to get",
                    IsOptional = false,
                    DefaultValue = null
                });
                return parameters;
            }
        }
    }
}
