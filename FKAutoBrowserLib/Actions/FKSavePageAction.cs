//---------------------------------------------------------------
// author: FreeKnight
// date: 2017-3-31
//---------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;
//---------------------------------------------------------------
namespace FKAutoBrowser
{
    /// <summary>
    /// 保存当前页面为
    /// </summary>
    class FKSavePageAction : FKBaseAction
    {
        public string FileName;

        public FKSavePageAction(string fileName)
        {
            FileName = fileName;
        }

        public override void PreAction()
        {
            PreActionMessage = String.Format("Saving HTML to {0}", FileName);
        }

        public override void Execute()
        {
            try
            {
                Success = true;
                string strDir = FKCommon.GetWorkdir() + SAVE_HTML_DIR;
                FKCommon.CreateDir(strDir);
                File.WriteAllText(FKCommon.GetWorkdir() + SAVE_HTML_DIR + FileName, AutoUnit.Browser.GetPage());
                PostActionMessage = String.Format("保存当前HTML到 {0} 完成", SAVE_HTML_DIR + FileName);
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
                    Name = "文件名",
                    Type = typeof(string),
                    Description = "The filename to save the HTML to",
                    IsOptional = false,
                    DefaultValue = null
                });
                return parameters;
            }
        }
    }
}
