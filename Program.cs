using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeknikServisOtomasyonuProje
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Application.Run(new KullanıcıArayuzForm(1));  // debug için kullanıcı userıd 1 olan kullanıcıyla giriş yap
            //Application.Run(new TeknisyenArayuz(2)); // debug için teknisyen userıd 2 olan kullanıcıyla giriş yap
            //Application.Run(new TeknisyenArayuz(3, "Accountant")); // debug için muhasebeci userıd 3 olan kullanıcıyla giriş yap
            Application.Run(new KullaniciGirisForm());
        }
    }
}
