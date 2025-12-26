using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace TeknikServisOtomasyonuProje
{
    public partial class KullaniciGirisForm : Form
    {
        SQLConnect sqlConnect = new SQLConnect();
        SqlConnection con;

        Fonksiyonlar fonksiyonlar = new Fonksiyonlar();
        public KullaniciGirisForm()
        {
            InitializeComponent();
            WindowDragHelper.EnableDrag(label1, this);
            passwordTB1.UseSystemPasswordChar = true;
            con = sqlConnect.connectToSQL();
            passwordTB2.UseSystemPasswordChar = true;
            confirmPasswordTB.UseSystemPasswordChar = true;
            AcceptButton = button1; // Set the default button to the login button
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
            if (checkBox2.Checked)
            {
                passwordTB2.UseSystemPasswordChar = false;
                confirmPasswordTB.UseSystemPasswordChar = false;
                checkBox2.Text = "Gizle";
            }
            else
            {
                passwordTB2.UseSystemPasswordChar = true;
                confirmPasswordTB.UseSystemPasswordChar = true;
                checkBox2.Text = "Göster";
            }
        }

        private void phoneNumMTB_Enter(object sender, EventArgs e)
        {
            phoneNumberMTBSetIndexTo(0);
        }

        private void kayitBtn_Click(object sender, EventArgs e)
        {
            if (!checkUserRegisterCredentials()) return;



            // Kullanıcı bilgilerini TextBox'lardan al
            //string username = usernameTB2.Text.Trim();
            string role = "Customer"; // Örn: Customer, Staff, Admin
            string email = EmailTB2.Text.Trim();
            string password = passwordTB2.Text.Trim();
            string firstName = firstNameTB.Text.Trim();
            string lastName = lastNameTB.Text.Trim();
            string phone = phoneNumMTB.Text.Trim();

            // Şifreyi hashlemek iyi olur, burada basit örnek (SHA256)
            //string passwordHash = ComputeSha256Hash(password);
            // ama şuanlık değil
            string passwordHash = password; // basitlik için düz metin şifre kullanılıyor

            SqlCommand cmd = new SqlCommand();

            try
            {
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = @"INSERT INTO Users (Role, Email, PasswordHash, FirstName, LastName, Phone) 
                            VALUES (@Role, @Email, @PasswordHash, @FirstName, @LastName, @Phone)";

                //cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Role", role);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@LastName", lastName);
                cmd.Parameters.AddWithValue("@Phone", string.IsNullOrEmpty(phone) ? (object)DBNull.Value : phone);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Kayıt başarılı!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                // Bağlantıyı kapat
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
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
            /*
            if (string.IsNullOrWhiteSpace(usernameTB2.Text) || !Regex.IsMatch(usernameTB2.Text.Trim() , @"^[A-Za-z0-9]+$")) // checks if username is empty or has a char other than letters and numbers
            {                                                              //be sure to trim username when adding to database
                MessageBox.Show("Kullanıcı adı geçersiz.\nLütfen sadece harf ve rakam kullanın.",
                "Hata",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
                return false;
            }*/
            if (!fonksiyonlar.CheckEmail(EmailTB2.Text))
            {
                MessageBox.Show("E-mail geçersiz. \nLütfen geçerli bir E-mail giriniz.",
                "Hata",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
                return false;
            }
            else if (!fonksiyonlar.CheckPhone(phoneNumMTB.Text))
            {
                MessageBox.Show("Telefon numarası geçersiz. \nLütfen geçerli bir telefon numarası giriniz.",
                "Hata",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
                return false;
            }
            else if (string.IsNullOrWhiteSpace(firstNameTB.Text))
            {
                MessageBox.Show("Lütfen isim giriniz.",
                "Hata",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
                return false;
            }
            else if (string.IsNullOrWhiteSpace(lastNameTB.Text))
            {
                MessageBox.Show("Lütfen soyisim giriniz.",
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
            string email = emailTB1.Text.Trim();
            string password = passwordTB1.Text.Trim();

            if (!fonksiyonlar.CheckEmail(emailTB1.Text.Trim())) // checks if username is empty or has a char other than letters and numbers
            {
                MessageBox.Show("Geçerli bir email giriniz.",
                "Hata",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
                Debug.WriteLine(emailTB1.Text.Trim());
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

            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = null;



            try
            {
                int currentUserId = -1;
                string userRole;

                con.Open();
                cmd.Connection = con;
                cmd.CommandText = @"SELECT UserId, Role
                    FROM Users 
                    WHERE Email = @Email AND PasswordHash = @Password";

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);

                reader = cmd.ExecuteReader();

                if (reader.Read()) // HasRows + Read yerine direkt Read
                {
                    currentUserId = reader.GetInt32(0); // 0 = UserId kolon indexi
                    userRole = reader.GetString(1); // 1 = Role kolon indexi

                    //MessageBox.Show("Giriş başarılı!");
                    this.Hide();
                    if (userRole == "Customer")
                    {
                        new KullanıcıArayuzForm(currentUserId).Show();
                    }
                    else if (userRole == "Staff")
                    {
                        new TeknisyenArayuz(currentUserId).Show();
                    }
                    else if (userRole == "Accountant")
                    {
                        new TeknisyenArayuz(currentUserId, userRole).Show();
                    }
                    else if (userRole == "Warehouse")
                    {
                        new TeknisyenArayuz(currentUserId, userRole).Show();
                    }
                    else if (userRole == "Admin")
                    {
                        new TeknisyenArayuz(currentUserId, userRole).Show();
                    }
                }
                else
                {
                    MessageBox.Show("Kullanıcı adı veya şifre hatalı!",
                                    "Hata",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                if (reader != null) reader.Close();
                if (con.State == ConnectionState.Open) con.Close();
            }
        
        }
    
        
            // database code to login user goes here
        

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void KullaniciGirisForm_Load(object sender, EventArgs e)
        {
            tabControl1.Appearance = TabAppearance.Normal;
            tabControl1.Padding = new Point(0, 0);
        }

        private void giris_Click(object sender, EventArgs e)
        {

        }
    }
}
