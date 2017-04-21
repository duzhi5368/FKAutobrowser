//---------------------------------------------------------------
// author: FreeKnight
// date: 2017-3-31
//---------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
//---------------------------------------------------------------
namespace FKAutoBrowser
{
    /// <summary>
    /// 按键消息
    /// </summary>
    class FKKeyPressAction : FKBaseAction
    {
        public string KeysText;
        public string Keys;

        /// <summary>
        /// 按下指定键
        /// Presses the given keys
        /// </summary>
        public FKKeyPressAction(string keysText)
        {
            KeysText = keysText;

            if (!KeysText.Contains("{"))
            {
                GetKeys();
            }
        }

        public override void PreAction()
        {
            PreActionMessage = "Pressing keys";
        }

        /// <summary>
        /// 进行按键的字符串转义
        /// </summary>
        private void GetKeys()
        {
            if (string.IsNullOrWhiteSpace(KeysText))
            {
                return;
            }

            try
            {
                MemberInfo[] keysMembers = typeof(OpenQA.Selenium.Keys).GetMembers(BindingFlags.Static | BindingFlags.Public);
                foreach (MemberInfo keysMember in keysMembers)
                {
                    if (keysMember.MemberType == MemberTypes.Field && keysMember.Name.ToLower() == KeysText.ToLower())
                    {
                        Keys = ((FieldInfo)keysMember).GetValue(null).ToString();
                        return;
                    }
                }
            }
            catch
            {

            }
        }

        public override void Execute()
        {
            try
            {
                Success = true;
                Thread.Sleep(ACTION_IDLE_TIME);
                SendKeys.SendWait(Keys == null ? KeysText : Keys);
                Thread.Sleep(ACTION_IDLE_TIME);
                PostActionMessage = String.Format("按下指定按键 '{0}' 完成", KeysText);
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
                    Name = "按键文本表达式",
                    Type = typeof(string),
                    Description = "The keys to send to the tab",
                    IsOptional = false,
                    DefaultValue = null
                });
                return parameters;
            }
        }
    }
}
