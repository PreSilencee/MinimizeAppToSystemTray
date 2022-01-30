using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace minimizeAppToSystemTrayCSharp
{
    public partial class Authentication : Form
    {
        public Authentication()
        {
            InitializeComponent();
            AuthenticationTextBox.PasswordChar = '*';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Equals(AuthenticationTextBox.Text, "1234"))
            {
                Program.RestoreMenu.Enabled = false;
                Program.HideMenu.Enabled = true;
                Close();
                Program.ShowWindow(Program.WinConsole, Program.SW_SHOW);
            }
            else
            {
                MessageBox.Show("Invalid authentication");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
