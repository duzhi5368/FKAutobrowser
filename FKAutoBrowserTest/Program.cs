using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FKAutoBrowserTest
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        public static void Close()
        {
            Form.ActiveForm.Dispose();
            Environment.Exit(0);
        }

        public static void ShowAboutBox()
        {
            AboutForm form = new AboutForm();
            form.Show();
        }
    }
}
