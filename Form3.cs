using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions; // FOR USE Regex Validations

namespace VentasExpress
{
    public partial class frmSale : Form
    {
        public frmSale()
        {
            InitializeComponent();
        }

        private void frmSale_Load(object sender, EventArgs e)
        {
            if (!Program.DataLogin.isInLogin())
            {
                MessageBox.Show("Inicie sesión porfavor.");
                Application.Exit();
            }
            Dictionary<int, Object[]> menu = Program.DataLogin.menu;

            for (int i = 0; i < menu.Count(); i++)
            {
                int key = menu.ElementAt(i).Key;
                string product = menu[key][0].ToString();
                string price = ((double)menu[key][1]).ToString("0.00");
                dataGMenu.Rows.Add(
                    new Object[] { key, product, price }
                );
            }
        }
        private void txtNV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnComprar.PerformClick();
            }
        }

        private void btnComprar_Click(object sender, EventArgs e)
        {
            string value = txtNV.Text.Replace(" ", "").Trim(',');

            //INPUT EXAMPLE: 1,20,2,5
            //INPUT EXAMPLE: 1,20,2,5,9,15

            if (value.Length == 0)
            {
                MessageBox.Show("Ingrese la venta", "Ventas Don Diego dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!Regex.IsMatch(value, @"^[\d,]+$"))
            {
                MessageBox.Show("Use solo números y coma", "Ventas Don Diego dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string[] compra = value.Split(',');
            if ((compra.Length % 2) != 0)
            {
                MessageBox.Show("Para cada producto, debe agregar su cantidad.", "Ventas Don Diego dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int cuantityIsZero = 0;
            int realLenght = compra.Length / 2;
            int[] product = new int[realLenght];
            int[] cuantity = new int[realLenght];
            MessageBox.Show(string.Join(" ", product));
            MessageBox.Show(string.Join(" ", cuantity));
            for (int i = 0; i < compra.Length; i++)
            {
                if ((i % 2) == 0)
                {
                    product[i/2] = Convert.ToInt32(compra[i]);
                }
                else
                {
                    cuantity[(int)Math.Floor((double)(i / 2))] = Convert.ToInt32(compra[i]);
                }
            }

            for (int i = 0; i < cuantity.Length; i++) { if (cuantity[i] <= 0) { ++cuantityIsZero; } }
            if (cuantityIsZero > 0)
            {
                MessageBox.Show("No puede ingresar cantidades iguales o menores a cero.", "Ventas Don Diego dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int ifexistproduct = 0;
            Dictionary<int, Object[]> menu = Program.DataLogin.menu;
            for (int i = 0; i < product.Length; i++)
            {
                if (!menu.ContainsKey(product[i]))
                {
                    ++ifexistproduct;
                }
            }
            if (ifexistproduct > 0)
            {
                MessageBox.Show("Un producto ingresado no existe en el menú", "Ventas Don Diego dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string recibo = Program.DataLogin.Comprar(product, cuantity, realLenght);
            txtNV.ResetText();
            txtRecibo.Text = recibo;

        }

        private void frmSale_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmHome frm_Home = new frmHome();
            frm_Home.Show();
        }
    }
}
