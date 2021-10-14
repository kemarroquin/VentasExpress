using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
// HEAD
using System.Security.Cryptography; // FOR USE SHA256Managed Class
using System.Text.RegularExpressions; // FOR USE Regex Validations
//
// cbf5cf7b69ae235b5d14a09a011f73cea193ddcb

namespace VentasExpress
{
    static class Program
    {
        //// HEAD
        public static string convertHash256(string stringText)
        {
            string hash = "";

            if (String.IsNullOrEmpty(stringText)) { return ""; }

            SHA256Managed sha = new SHA256Managed(); // USE CLASS
            // CONVERT TEXT TO BYTE ARRAY
            byte[] tb = System.Text.Encoding.UTF8.GetBytes(stringText);
            byte[] hb = sha.ComputeHash(tb);

            //CONVERT BYTE TO STRING
            hash = BitConverter.ToString(hb).Replace("-", "");

            return hash;
        }
        public static class DataLogin //Login Variables
        {
            private static bool inLogin = false;
            public static Dictionary<string, string> users = new Dictionary<string, string>() { // LIKE ASSOC ARRAY ["user" => "password"]
                { "admin" , convertHash256("admin123") },
                { "vendedor" , convertHash256("Vendedor123") },
                { "invitado" , convertHash256("invitadoinvitado123") }
            };
            public static Dictionary<string, int> keysMenu = new Dictionary<string, int>() { // LIKE ASSOC ARRAY ["user" => "password"]
                { "huevos" , 1 },
                { "pollo" , 2 },
                { "aceite" , 3 },
                { "fósforos" , 4 },
                { "dulces" , 5 },
                { "margarina" , 6 },
                { "jabón" , 7 },
                { "carne" , 8 },
                { "gaseosa" , 9 },
                { "desechables" , 10 }
            };
            public static Dictionary<int, Object[]> menu = new Dictionary<int, Object[]>() {
                {1, new object[] { "Huevos", 0.10, 100 } },
                {2, new object[] { "Pollo", 5.00, 100 } },
                {3, new object[] { "Aceite", 3.00, 100 } },
                {4, new object[] { "Fósforos", 0.50, 100 } },
                {5, new object[] { "Dulces", 0.80, 100 } },
                {6, new object[] { "Margarina", 0.30, 100 } },
                {7, new object[] { "Jabón", 2.25, 100 } },
                {8, new object[] { "Carne", 2.75, 100 } },
                {9, new object[] { "Gaseosa", 1.80, 100 } },
                {10, new object[] { "Desechables", 3.25, 100 } }
            };
            private static string loginUser = "";

            public static bool Login(string user, string password)
            {
                if (!users.ContainsKey(user)) return false;
                if (users[user.Trim()].ToLower() != convertHash256(password).ToLower()) return false;

                inLogin = true;
                loginUser = user.Trim();
                return true;
            }
            public static void Logout()
            {
                inLogin = true;
                loginUser = "";
            }

            public static string Comprar(int[] product, int[] cuantity, int totalProducts)
            {
                string msg = "";
                double t_sum = 0.00;

                msg += "\t Factura de compra";
                msg += Environment.NewLine;
                msg += "\t Supermercado Don Diego";
                msg += Environment.NewLine + Environment.NewLine;

                string giftMsg = "DULCES     " + "------    " + "0" + " x " + "0.00" + "    " + "=" + "    " + "$" + "0.00 -> (Regalo por llevar Pollo y Gaseosa)";
                int ifgift = 0;
                for (int i = 0; i < totalProducts; i++)
                {
                    string c_msg = "";
                    int c_id = product[i];
                    int c_cuantity = cuantity[i];
                    Object[] c_menuinfo = menu[c_id];
                    string c_nombre = Convert.ToString(c_menuinfo[0]);
                    double c_price = Convert.ToDouble(c_menuinfo[1]);
                    int m_cuantity = Convert.ToInt32(c_menuinfo[2]);

                    if (m_cuantity == 0)
                    {
                        c_msg += c_nombre.ToUpper() + "    " + "------    " + "0 x " + c_price.ToString("0.00") + "    " + "\"Ya no ha existencias :( (LO SENTIMOS MUCHO)\"";
                        c_msg += Environment.NewLine;
                        t_sum += 0.00;
                    }
                    else if (m_cuantity < c_cuantity)
                    {
                        c_msg += c_nombre.ToUpper() + "    " + "------    " + (m_cuantity) + " x " + c_price.ToString("0.00") + "  " + "=" + "  " + "$" + (m_cuantity * c_price).ToString("0.00") + "    \"No se le pudo cobrar la cantidad de: [" + c_cuantity + "]; Por este motivo se le agrego el total de existencias.\"";
                        c_msg += Environment.NewLine;
                        t_sum += (m_cuantity * c_price);
                        if (c_nombre.ToLower() == "pollo" || c_nombre.ToLower() == "gaseosa") { ++ifgift; }
                        menu[c_id][2] = 0;
                    }
                    else
                    {
                        c_msg += c_nombre.ToUpper() + "    " + "------    " + (c_cuantity) + " x " + c_price.ToString("0.00") + "  " + "=" + "  " + "$" + (c_cuantity * c_price).ToString("0.00");
                        c_msg += Environment.NewLine;
                        t_sum += (c_cuantity * c_price);
                        if (c_nombre.ToLower() == "pollo" || c_nombre.ToLower() == "gaseosa") { ++ifgift; }
                        menu[c_id][2] = m_cuantity - c_cuantity;
                    }
                    msg += c_msg;
                }
                if (ifgift >= 2)
                {
                    msg += giftMsg;
                    msg += Environment.NewLine;
                }
                msg += Environment.NewLine;
                msg += "______________________________";
                msg += Environment.NewLine;
                msg += "\t\tToltal:    $" + t_sum.ToString("0.00");

                return msg;
            }
            public static string getLoginUser() { return loginUser; }
            public static bool isInLogin() { return inLogin; }
        }
        //
        // cbf5cf7b69ae235b5d14a09a011f73cea193ddcb
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmLogin());
        }
    }
}
