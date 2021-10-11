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
            MessageBox.Show("" + Program.DataLogin.inLogin);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.DataLogin.inLogin = false;
            this.Hide();
            frmLogin frm_Login = new frmLogin();
            frm_Login.Show();
        }
    }
}
