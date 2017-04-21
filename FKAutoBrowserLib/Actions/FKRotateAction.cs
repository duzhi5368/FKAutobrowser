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
    /// 浏览器横竖转屏
    /// </summary>
    class FKRotateAction : FKBaseAction
    {
        public override void PreAction()
        {
            PreActionMessage = "Rotating the window";
        }

        public override void Execute()
        {
            try
            {
                Success = AutoUnit.Browser.Rotate();
                PostActionMessage = "旋转窗口完成";
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
                return parameters;
            }
        }
    }
}
