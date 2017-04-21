//---------------------------------------------------------------
// author: FreeKnight
// date: 2017-4-1
//---------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
//---------------------------------------------------------------
namespace FKAutoBrowser
{
    /// <summary>
    /// 获取当前元素的外部HTML
    /// Returns the outer HTML of the current element
    /// </summary>
    class FKGetElemOuterHtmlAction : FKBaseAction
    {
        public override void PreAction()
        {
            PreActionMessage = "Getting the outer HTML of the current element";
        }

        /// <summary>
        /// Executes the action
        /// </summary>
        public override void Execute()
        {
            try
            {
                if (AutoUnit.CurrentElements != null && AutoUnit.CurrentElements.Any())
                {
                    string html = AutoUnit.CurrentElements.First().OuterHTML();
                    PostActionMessage = String.Format("元素外部HTML为: {0}", html);
                    Success = true;
                }
                else
                {
                    PostActionMessage = "当前没有选中任何元素";
                    Success = false;
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
                return parameters;
            }
        }
    }
}
