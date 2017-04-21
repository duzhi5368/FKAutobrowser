//---------------------------------------------------------------
// author: FreeKnight
// date: 2017-3-31
//---------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
//---------------------------------------------------------------
namespace FKAutoBrowser
{
    /// <summary>
    /// 截屏并保存图片
    /// </summary>
    class FKScreenShotAction : FKBaseAction
    {
        public string FileName;

        public FKScreenShotAction(string fileName)
        {
            FileName = fileName;
        }

        public override void PreAction()
        {
            PreActionMessage = String.Format("Saving screenshot to {0}", FileName);
        }

        public override void Execute()
        {
            try
            {
                Success = false;

                ImageFormat format = ImageFormat.Jpeg;
                string ext = Path.GetExtension(FileName);
                switch (ext.ToLower())
                {
                    case ".png":
                        format = ImageFormat.Png;
                        break;
                    case ".bmp":
                        format = ImageFormat.Bmp;
                        break;
                }
                string strDir = FKCommon.GetWorkdir() + SCREENSHOT_DIR;
                FKCommon.CreateDir(strDir);
                string fileName = FKCommon.GetWorkdir() + SCREENSHOT_DIR + FileName;
                fileName = AutoUnit.Browser.FormatFileName(fileName);

                string strResult = AutoUnit.Browser.ScreenShot(fileName, format, AutoUnit.BrowserType);
                if (strResult == "")
                {
                    Success = true;
                    PostActionMessage = String.Format("截图保存在 {0}", FileName);
                }
                else
                {
                    Success = false;
                    PostActionMessage = strResult;
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
                    Name = "文件名",
                    Type = typeof(string),
                    Description = "The filename to save the screenshot to",
                    IsOptional = false,
                    DefaultValue = null
                });
                return parameters;
            }
        }
    }
}
