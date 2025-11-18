using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TeknikServisOtomasyonuProje
{
    internal class Fonksiyonlar
    {
        public bool checkEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        public bool CheckPhone(string phone)
        {
            return Regex.IsMatch(phone, @"^[1-9][0-9]{9}$");
        }
    }
}
