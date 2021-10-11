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

        private void frmLogin_Load(object sender, EventArgs e)
        {
            MessageBox.Show("" + Program.DataLogin.inLogin);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Program.DataLogin.inLogin = true;
            this.Hide();
            frmHome frm_Home = new frmHome();
            frm_Home.Show();

        }

        private void frmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }

}
