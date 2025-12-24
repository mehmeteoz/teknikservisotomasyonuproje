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
                        CreatedAt = reader["CreatedAt"].ToString() // burayı düzelt datetime yap
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
            int maxYukseklik = 1920,
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

        public List<UserServices> GetUserServices(int userId, SqlConnection sqlConnection)
        {
            List<UserServices> services = new List<UserServices>();

            string query = @"
        SELECT *
        FROM ServiceRecords
        WHERE CustomerID = @CustomerID
        ORDER BY CreatedAt DESC";

            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            cmd.Parameters.AddWithValue("@CustomerID", userId);

            SqlDataReader reader = null;

            try
            {
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    UserServices service = new UserServices();

                    service.ServiceID = Convert.ToInt32(reader["ServiceID"]);
                    service.CustomerID = Convert.ToInt32(reader["CustomerID"]);

                    service.AssignedStaffID = reader["AssignedStaffID"] == DBNull.Value
                        ? (int?)null
                        : Convert.ToInt32(reader["AssignedStaffID"]);

                    service.DeviceType = reader["DeviceType"].ToString();
                    service.Brand = reader["Brand"].ToString();
                    service.Model = reader["Model"].ToString();
                    service.SerialNumber = reader["SerialNumber"].ToString();
                    service.ProblemDescription = reader["ProblemDescription"].ToString();
                    service.Status = reader["Status"].ToString();
                    service.CreatedAt = Convert.ToDateTime(reader["CreatedAt"]);

                    service.ClosedAt = reader["ClosedAt"] == DBNull.Value
                        ? (DateTime?)null
                        : Convert.ToDateTime(reader["ClosedAt"]);

                    service.Picture64 = reader["Picture64"]?.ToString();

                    services.Add(service);
                }
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();

                cmd.Dispose();
            }

            return services;
        }




        public Panel CreateServiceCard(UserServices service)
        {
            // === PANEL (KART) ===
            Panel panel = new Panel();
            panel.Width = 220;
            panel.Height = 300;
            panel.Margin = new Padding(10);
            panel.BackColor = Color.FromArgb(64, 64, 64);
            panel.BorderStyle = BorderStyle.FixedSingle;

            // Hover efekti 
            //panel.MouseEnter += (s, e) => panel.BackColor = Color.Gainsboro;
            //panel.MouseLeave += (s, e) => panel.BackColor = Color.White;

            // === BAŞLIK (CİHAZ TÜRÜ) ===
            Label lblTitle = new Label();
            lblTitle.Text = service.DeviceType;
            lblTitle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblTitle.Height = 28;
            lblTitle.Dock = DockStyle.Top;
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            lblTitle.ForeColor = Color.White;

            // === FOTOĞRAF (BASE64) ===
            PictureBox pic = new PictureBox();
            pic.Height = 140;
            pic.Dock = DockStyle.Top;
            pic.SizeMode = PictureBoxSizeMode.Zoom;

            if (!string.IsNullOrEmpty(service.Picture64))
            {
                try
                {
                    byte[] bytes = Convert.FromBase64String(service.Picture64);
                    using (MemoryStream ms = new MemoryStream(bytes))
                    {
                        pic.Image = Image.FromStream(ms);
                    }
                }
                catch
                {
                    // Base64 hatalıysa boş bırak
                }
            }

            // === MARKA ===
            Label lblBrand = new Label();
            lblBrand.Text = "Marka: " + service.Brand;
            lblBrand.Dock = DockStyle.Top;
            lblBrand.Padding = new Padding(5);
            lblBrand.ForeColor = Color.White;

            // === MODEL ===
            Label lblModel = new Label();
            lblModel.Text = "Model: " + service.Model;
            lblModel.Dock = DockStyle.Top;
            lblModel.Padding = new Padding(5);
            lblModel.ForeColor = Color.White;


            // === DURUM ===
            Label lblStatus = new Label();
            lblStatus.Text = "Durum: " + service.Status;
            lblStatus.Dock = DockStyle.Top;
            lblStatus.Padding = new Padding(5);
            lblStatus.ForeColor = Color.White;

            if (service.Status == "Tamamlandı")
                lblStatus.ForeColor = Color.Green;
            else
                lblStatus.ForeColor = Color.White;

            // === DETAY BUTTON ===
            Button lblDetail = new Button();
            lblDetail.Text = "Detayları Görüntüle";
            lblDetail.Font = new Font("Arial", 15, FontStyle.Bold);
            lblDetail.Height = 60;
            lblDetail.ForeColor = Color.White;
            lblDetail.Dock = DockStyle.Bottom;
            lblDetail.BackColor = Color.FromArgb(64, 64, 64);
            lblDetail.FlatStyle = FlatStyle.Popup;
            lblDetail.Margin = new Padding(20);


            // === CONTROLLERİ EKLE (TERSTEN) ===
            panel.Controls.Add(lblStatus);
            panel.Controls.Add(lblModel);
            panel.Controls.Add(lblBrand);
            panel.Controls.Add(pic);
            panel.Controls.Add(lblTitle);
            panel.Controls.Add(lblDetail);

            // === TÜM KARTA TIKLAMA ===
            AddClickRecursive(panel, service.ServiceID);

            return panel;
        }


        public void AddClickRecursive(Control control, int id)
        {
            control.Click += (s, e) => OpenDetail(id);

            foreach (Control child in control.Controls)
            {
                AddClickRecursive(child, id);
            }
        }

        public void OpenDetail(int serviceId)
        {
            // Detay formunu açma işlemi burada yapılacak
        }


    }
}
