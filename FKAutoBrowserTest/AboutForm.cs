using System;
using System.Windows.Forms;
using System.Reflection;

namespace FKAutoBrowserTest
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            m_VersionLabel.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
    }
}
