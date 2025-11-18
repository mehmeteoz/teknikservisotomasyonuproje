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
        // Validates an email address
        public bool CheckEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email; // ensures exact match
            }
            catch
            {
                return false;
            }
        }

        // Validates a 10-digit phone number that does not start with 0
        public bool CheckPhone(string phone)
        {
            return Regex.IsMatch(phone, @"^[1-9][0-9]{9}$");
        }
    }
}
