//---------------------------------------------------------------
// author: FreeKnight
// date: 2017-4-4
//---------------------------------------------------------------
using System;
using System.IO;
//---------------------------------------------------------------
namespace FKAutoBrowser
{
    class FKCommon
    {
        // 获取本应用程序工作目录
        public static string GetWorkdir()
        {
            return Environment.CurrentDirectory;
        }
        // 安全创建文件夹
        public static void CreateDir(string strPath)
        {
            if (!Directory.Exists(strPath))
            {
                Directory.CreateDirectory(strPath);
            }
        }
    }
}
