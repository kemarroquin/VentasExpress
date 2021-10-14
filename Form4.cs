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
    public partial class frmInventory : Form
    {
        public frmInventory()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            Dictionary<int, Object[]> menu = Program.DataLogin.menu;

            for (int i = 0; i < menu.Count(); i++)
            {
                int key = menu.ElementAt(i).Key;
                string product = menu[key][0].ToString();
                string price = ((double)menu[key][1]).ToString("0.00");
                string cuantity = ((int)menu[key][2]).ToString();
                dataGMenu.Rows.Add(
                    new Object[] { key, product, price, cuantity }
                );
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string txt = txtSearch.Text.Trim();
            Dictionary<string, int> keyMenu = Program.DataLogin.keysMenu;
            Dictionary<int, Object[]> menu = Program.DataLogin.menu;
            dataGMenu.Rows.Clear();
            dataGMenu.Refresh();

            if (txt.Length <= 0)
            {
                MessageBox.Show("Ingrese el producto a buscar", "Ventas Don Diego dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (txt.Length == 1)
            {
                if (!txt.All(char.IsDigit))
                {
                    MessageBox.Show("El ID debe ser un número", "Ventas Don Diego dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    if (!menu.ContainsKey(Convert.ToInt32(txt)))
                    {
                        MessageBox.Show("No se encontro el producto", "Ventas Don Diego dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        int key = menu.ElementAt(Convert.ToInt32(txt)).Key;
                        string product = menu[key][0].ToString();
                        string price = ((double)menu[key][1]).ToString("0.00");
                        string cuantity = ((int)menu[key][2]).ToString();
                        dataGMenu.Rows.Add(
                            new Object[] { key, product, price, cuantity }
                        );
                    }
                }
            }
            else if (txt.ToLower() == "todos")
            {
                for (int i = 0; i < menu.Count(); i++)
                {
                    int key = menu.ElementAt(i).Key;
                    string product = menu[key][0].ToString();
                    string price = ((double)menu[key][1]).ToString("0.00");
                    string cuantity = ((int)menu[key][2]).ToString();
                    dataGMenu.Rows.Add(
                        new Object[] { key, product, price, cuantity }
                    );
                }
            } 
            else
            {
                if (!keyMenu.ContainsKey(txt.ToLower()))
                {
                    MessageBox.Show("No se encontro el producto", "Ventas Don Diego dice:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    int newKey = keyMenu[txt];
                    int key = menu.ElementAt(Convert.ToInt32(newKey)).Key;
                    string product = menu[key][0].ToString();
                    string price = ((double)menu[key][1]).ToString("0.00");
                    string cuantity = ((int)menu[key][2]).ToString();
                    dataGMenu.Rows.Add(
                        new Object[] { key, product, price, cuantity }
                    );
                }
            }

        }
    }
}
