using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VentasExpress
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e) {}

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string user = txtbUser.Text.Trim();
            string password = txtbPassword.Text.Trim();

            if (user.Length == 0) {
                MessageBox.Show("Ingrese el usuario.", "Ventas Don Diego dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (password.Length == 0) {
                MessageBox.Show("Ingrese la contraseña.", "Ventas Don Diego dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(!Program.DataLogin.Login(user, password))
            {
                MessageBox.Show("Usuario o Contraseña incorrectos.", "Ventas Don Diego dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.Hide();
            frmHome frm_Home = new frmHome();
            frm_Home.Show();

        }
        private void txtbUser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnLogin.PerformClick();
        }

        private void txtbPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnLogin.PerformClick();
        }

        private void frmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

    }

}
