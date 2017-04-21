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
    /// 删除一个Cookie
    /// Deletes a cookie
    /// </summary>
    class FKDeleteCookieAction : FKBaseAction
    {
        public string CookieName;

        public FKDeleteCookieAction(string cookieName)
        {
            CookieName = cookieName.Trim().Trim('\'');
        }

        public override void PreAction()
        {
            PreActionMessage = String.Format("Deleting cookie: {0}", CookieName);
        }

        public override void Execute()
        {
            try
            {
                bool bSuccessed = AutoUnit.Browser.DeleteCookie(CookieName);
                if (!bSuccessed)
                {
                    Success = false;
                    PostActionMessage = String.Format("Cookie '{0}' 未找到", CookieName);
                }
                else
                {
                    Success = true;
                    PostActionMessage = String.Format("Cookie '{0}' 已删除完成", CookieName);
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
                    Description = "The name of the cookie to delete",
                    IsOptional = false,
                    DefaultValue = null
                });
                return parameters;
            }
        }
    }
}
