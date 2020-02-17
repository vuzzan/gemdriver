using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SecsGemDriver
{
    public partial class Login : MetroFramework.Forms.MetroForm
    {
        public Login()
        {
            Main.connString = string.Format("server={0};database={1};user={2};password={3}", "192.168.100.253", "gem", "root", "ok");
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            txtID.Focus();
        }

        private void btnLOGIN_Click(object sender, EventArgs e)
        {
            doLogin();
            
        }

        private void doLogin()
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                txtID.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPW.Text))
            {
                txtPW.Focus();
                return;
            }
            //str.Replace("'", "\\'")
            string id = txtID.Text.Trim().Replace("'", "\\'");
            string pw = txtPW.Text.Trim().Replace("'", "\\'");
            List<Users> listUser = Users.load("select * from users where user_name='" + id + "' and user_pass='" + pw + "'");
            if (listUser.Count == 0)
            {
                MessageBox.Show("Can not found any userId or password");
                this.DialogResult = DialogResult.None;
            }
            else
            {
                Main.setLoginUser(listUser.ToArray()[0]);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            // END LOGIN 
        }

        private void txtID_Leave(object sender, EventArgs e)
        {
            txtPW.Focus();
        }

        private void txtPW_Leave(object sender, EventArgs e)
        {
            btnLOGIN.Focus();
        }

        private void txtID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                doLogin();
            }
        }

        private void txtPW_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                doLogin();
            }
        }
    }
}
