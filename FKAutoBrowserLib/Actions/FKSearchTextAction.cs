//---------------------------------------------------------------
// author: FreeKnight
// date: 2017-4-1
//---------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
//---------------------------------------------------------------
namespace FKAutoBrowser
{
    /// <summary>
    /// 查找本页面文字
    /// Searches for text on the current page
    /// </summary>
    /// <returns></returns>
    class FKSearchTextAction : FKBaseAction
    {
        public string Query;
        public string SearchType;

        public FKSearchTextAction(string query)
        {
            Query = query;
            SearchType = "standard";
        }
        public FKSearchTextAction(string query, string searchType)
        {
            Query = query;
            SearchType = searchType;
        }

        public override void PreAction()
        {
            if (SearchType == "regex")
            {
                PreActionMessage = String.Format("Searching for '{0}' as a Regular Expression", Query);
            }
            else
            {
                PreActionMessage = String.Format("Searching for '{0}'", Query);
            }
        }

        public override void Execute()
        {
            try
            {
                if (SearchType == "" || SearchType.ToLower() == "standard")
                {
                    Success = AutoUnit.Browser.GetPage().Contains(Query);
                    if(Success)
                        PostActionMessage = String.Format("查找字符 '{0}' 完成，该字符存在", Query);
                    else
                        PostActionMessage = String.Format("查找字符 '{0}' 完成，未找到该字符", Query);
                }
                if (SearchType.ToLower() == "regex")
                {
                    Success = Regex.IsMatch(AutoUnit.Browser.GetPage(), Query);
                    if (Success)
                        PostActionMessage = String.Format("正则表达式查找 '{0}'，该正则表达式匹配项存在 ", Query);
                    else
                        PostActionMessage = String.Format("正则表达式查找 '{0}'，该正则表达式匹配项不存在 ", Query);
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
                    Name = "查找文本",
                    Type = typeof(string),
                    Description = "The text to search for",
                    IsOptional = false,
                    DefaultValue = null
                });
                parameters.Add(new FKActionParameter
                {
                    Name = "查找方式: standard, regex",
                    Type = typeof(string),
                    Description = "The type of search, either 'standard' or 'regex'",
                    IsOptional = true,
                    DefaultValue = "standard"
                });
                return parameters;
            }
        }
    }
}
