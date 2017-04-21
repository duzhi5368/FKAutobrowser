//---------------------------------------------------------------
// author: FreeKnight
// date: 2017-3-30
//---------------------------------------------------------------
using System;
using System.Collections.Generic;
//---------------------------------------------------------------
namespace FKAutoBrowser
{
    /// <summary>
    /// 打开指定网址
    /// Loads the page at the given URI
    /// </summary>
    public class FKOpenUrlAction : FKBaseAction
    {
        private Uri Uri;
        private string BrowserType;

        public FKOpenUrlAction(string uri)
        {
            Uri = GetUri(uri);
        }
        public FKOpenUrlAction(string uri, string browserType)
        {
            Uri = GetUri(uri);
            BrowserType = browserType;
        }

        private Uri GetUri(string uri)
        {
            // 本地请求
            if (uri.StartsWith("/"))
            {
                if(AutoUnit != null)
                    uri = AutoUnit.CurrentUri.Host + uri;
            }
            // 默认视为HTTP请求
            else if (!uri.StartsWith("http") && !uri.StartsWith("https"))
            {
                uri = "http://" + uri;
            }
            return new Uri(uri);
        }

        public override void PreAction()
        {
            AutoUnit.CurrentUri = Uri;
            PreActionMessage = BrowserType == "" ? String.Format("Loading URI '{0}'", Uri) : String.Format("Loading URI '{0}' in browser '{1}'", Uri, BrowserType);
        }

        /// <summary>
        /// 实际执行
        /// Executes the action
        /// </summary>
        public override void Execute()
        {
            AutoUnit.CurrentUri = Uri;
            try
            {
                AutoUnit.Browser.OpenUrl(AutoUnit.CurrentUri);
                Success = true;
                PostActionMessage = String.Format("打开网址 '{0}' 完成", AutoUnit.CurrentUri);
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
                    Name = "链接Url",
                    Type = typeof(string),
                    Description = "The URI to load",
                    IsOptional = false,
                    DefaultValue = null
                });
                parameters.Add(new FKActionParameter
                {
                    Name = "浏览器类型: localie, ie, chrome, firefox, safari",
                    Type = typeof(string),
                    Description = "The type of browser to use (either 'firefox', 'ie' or 'chrome')",
                    IsOptional = true,
                    DefaultValue = null
                });
                return parameters;
            }
        }
    }
}
