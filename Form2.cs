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
    public partial class frmHome : Form
    {
        public frmHome()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (!Program.DataLogin.isInLogin())
            {
                MessageBox.Show("Inicie sesión porfavor.");
                this.FormClosing -= frmHome_FormClosing;
                this.FormClosed -= frmHome_FormClosed;
                this.FormClosed += frmHome_FormClosed_two;
                this.Close();
            }
            lblUserName.Text = "[ " + Program.DataLogin.getLoginUser().ToUpper() + " ]";
        }
        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmHome_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.DataLogin.Logout();
            MessageBox.Show("¡Adiós!", "Ventas Don Diego dice:", MessageBoxButtons.OK, MessageBoxIcon.Information);
            frmLogin frm_Login = new frmLogin();
            frm_Login.Show();
        }
        private void frmHome_FormClosed_two(object sender, FormClosedEventArgs e)
        {
            Program.DataLogin.Logout();
            frmLogin frm_Login = new frmLogin();
            frm_Login.Show();
        }

        private void frmHome_FormClosing(object sender, FormClosingEventArgs e)
        {
            string msg = "¿Esta seguro de querer cerrar sesión?";
            string tt = "¿Cerrar Sesión?";
            var rs = MessageBox.Show(msg, tt, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rs == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmSale frm_Sale = new frmSale();
            frm_Sale.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmInventory frm_Inventory = new frmInventory();
            frm_Inventory.Show();
        }
    }
}
