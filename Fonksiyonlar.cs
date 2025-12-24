using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        //gets user info from database
        public List<User> GetUserInfo(int userID, SqlConnection sqlConnection)
        {
            List<User> users = new List<User>();

            try
            {
                sqlConnection.Open();

                string query = "SELECT * FROM Users WHERE UserID = @UserID";
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@UserID", userID);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    User user = new User
                    {
                        UserID = Convert.ToInt32(reader["UserID"]),
                        Role = reader["Role"].ToString(),
                        Email = reader["Email"].ToString(),
                        Password = reader["PasswordHash"].ToString(),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        CreatedAt = reader["CreatedAt"].ToString()
                    };

                    users.Add(user);
                }

                reader.Close();
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                    sqlConnection.Close();
            }

            return users;
        }

        public string ResmiBase64eCevir(Image image, ImageFormat format)
        {
            if (image == null)
                return null;

            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, format);
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        public Image Base64tenResmeCevir(string base64)
        {
            byte[] bytes = Convert.FromBase64String(base64);

            using (MemoryStream ms = new MemoryStream(bytes))
            using (Image temp = Image.FromStream(ms))
            {
                return new Bitmap(temp);
            }
        }


        public void ResimYukle(
            PictureBox pictureBox,
            int maxGenislik = 1920,
            int maxYukseklik = 1080,
            long maxDosyaBoyutuBayt = 4_000_000
            )
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Resim Seç";
                ofd.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png";
                ofd.Multiselect = false;

                if (ofd.ShowDialog() != DialogResult.OK)
                    return;

                // 1️⃣ Dosya uzantısı kontrolü
                string uzanti = Path.GetExtension(ofd.FileName).ToLower();
                string[] izinliUzantilar = { ".jpg", ".jpeg", ".png" };

                if (!izinliUzantilar.Contains(uzanti))
                {
                    MessageBox.Show("Geçersiz dosya türü!", "Hata");
                    return;
                }

                // 2️⃣ Dosya boyutu kontrolü
                FileInfo dosya = new FileInfo(ofd.FileName);
                if (dosya.Length > maxDosyaBoyutuBayt)
                {
                    MessageBox.Show(("Dosya boyutu çok büyük!" + " Dosya " + maxDosyaBoyutuBayt/1000000 + " Megabayttan Küçük Olmalıdır"), "Hata");
                    return;
                }

                // 3️⃣ Resim çözünürlüğü kontrolü (dosyayı kilitlemeden)
                using (Image tempResim = Image.FromFile(ofd.FileName))
                {
                    if (tempResim.Width > maxGenislik || tempResim.Height > maxYukseklik)
                    {
                        MessageBox.Show(
                            $"Resim çözünürlüğü çok büyük!\nMaksimum: {maxGenislik} x {maxYukseklik}",
                            "Hata",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        return;
                    }

                    // 4️⃣ Resmi güvenli şekilde yükle
                    pictureBox.Image?.Dispose();
                    pictureBox.Image = new Bitmap(tempResim);
                }

                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        public void formGetir(Form frm, Panel panel)
        {
            panel.Controls.Clear();

            frm.TopLevel = false;                 
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;

            panel.Controls.Add(frm);
            frm.Show();
        }


    }
}
