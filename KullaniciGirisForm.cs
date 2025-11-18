using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeknikServisOtomasyonuProje
{
    public partial class KullaniciGirisForm : Form
    {
        Fonksiyonlar fonksiyonlar = new Fonksiyonlar();
        public KullaniciGirisForm()
        {
            InitializeComponent();
            passwordTB1.UseSystemPasswordChar = true;
        }

        private void KullaniciGirisForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                passwordTB1.UseSystemPasswordChar = false;
                checkBox1.Text = "Gizle";
            }
            else
            {
                passwordTB1.UseSystemPasswordChar = true;
                checkBox1.Text = "Göster";
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void phoneNumMTB_Enter(object sender, EventArgs e)
        {
            phoneNumberMTBSetIndexTo(0);
        }

        private void kayitBtn_Click(object sender, EventArgs e)
        {
            if (!checkUserRegisterCredentials()) return;

            // database code to register user goes here
        }

        private void phoneNumMTB_Click(object sender, EventArgs e)
        {
            phoneNumberMTBSetIndexTo(0);
        }

        private void phoneNumberMTBSetIndexTo(int index)
        {
            phoneNumMTB.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals; // so it returns just the digits

            if (phoneNumMTB.Text != "") return; // if the user entered a number it wont go to the beginning

            // Move caret to the start so the user can enter the number from the beginning
            phoneNumMTB.SelectionStart = index;
            phoneNumMTB.SelectionLength = index;
            phoneNumMTB.Select(index, index);
        }

        private bool checkUserRegisterCredentials() // this only checks client side. if there are uniqeness errors with the database it will be handled elsewhere.
        {
            if (string.IsNullOrWhiteSpace(usernameTB2.Text) || !Regex.IsMatch(usernameTB2.Text, @"^[A-Za-z0-9]+$")) // checks if username is empty or has a char other than letters and numbers
            {
                MessageBox.Show("Kullanıcı adı geçersiz.\nLütfen sadece harf ve rakam kullanın.",
                "Hata",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
                return false;
            }
            else if (fonksiyonlar.checkEmail(EmailTB.Text))
            {
                MessageBox.Show("E-mail geçersiz. \nLütfen geçerli bir E-mail giriniz.",
                "Hata",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
                return false;
            }
            else if (fonksiyonlar.CheckPhone(phoneNumMTB.Text))
            {
                MessageBox.Show("Telefon numarası geçersiz. \nLütfen geçerli bir telefon numarası giriniz.",
                "Hata",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
                return false;
            }
            else if (string.IsNullOrWhiteSpace(passwordTB2.Text) || passwordTB2.Text.Length < 6)
            {
                MessageBox.Show("Şifre geçersiz. \nLütfen en az 6 karakter uzunluğunda bir şifre giriniz.",
                "Hata",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
                return false;
            }
            else if (passwordTB2.Text != confirmPasswordTB.Text)
            {
                MessageBox.Show("Şifreler eşleşmiyor. \nLütfen şifreleri kontrol ediniz.",
                "Hata",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(usernameTB1.Text) || !Regex.IsMatch(usernameTB2.Text, @"^[A-Za-z0-9]+$")) // checks if username is empty or has a char other than letters and numbers
            {
                MessageBox.Show("Kullanıcı adı geçersiz.\nLütfen sadece harf ve rakam kullanın.",
                "Hata",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
                return;
            }
            else if (string.IsNullOrWhiteSpace(passwordTB1.Text) || passwordTB1.Text.Length < 6)
            {
                MessageBox.Show("Şifre geçersiz. \nLütfen en az 6 karakter uzunluğunda bir şifre giriniz.",
                "Hata",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
                return;
            }

            // database code to login user goes here
        }
    }
}
