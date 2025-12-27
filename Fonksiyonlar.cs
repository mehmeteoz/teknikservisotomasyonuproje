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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

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
                    MessageBox.Show(("Dosya boyutu çok büyük!" + " Dosya " + maxDosyaBoyutuBayt / 1000000 + " Megabayttan Küçük Olmalıdır"), "Hata");
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

        public List<UserServices> GetTechniciansServices(int technicianID, SqlConnection sqlConnection)
        {
            List<UserServices> services = new List<UserServices>();
            string query = @"
                SELECT * 
                FROM ServiceRecords 
                WHERE AssignedStaffID = @AssignedStaffID
                AND Status = 'Müşteriden Cihaz Bekleniyor'
                OR Status = 'Cihaz Kontrol Ediliyor'
                OR Status = 'İşlemde'
                ORDER BY CreatedAt DESC";

            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            cmd.Parameters.AddWithValue("@AssignedStaffID", technicianID); // veya başka bir şekilde teknisyen ID'si alınabilir

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

        

        public List<UserServices> GetGotServices(SqlConnection sqlConnection, string userRole = "Staff",
            /* filtreler */ string cihazTipi = "Tümü", string marka = "Tümü", string tarih = "En Yeni", string musteriTelNo = "")
        {
            List<UserServices> services = new List<UserServices>();
            string query = string.Empty;
            query = @"
            SELECT SR.* 
            FROM ServiceRecords SR
            INNER JOIN Users U ON SR.CustomerID = U.UserID
            WHERE 1 = 1 ";
            if (userRole == "Staff")
            {

                query += @"
                    AND Status = 'Talep Alındı' 
                    ";//ORDER BY CreatedAt DESC

            }
            else if (userRole == "Accountant")
            {
                query += @"
                    WHERE Status = 'Ücret Hesaplanıyor' 
                    ";
            }
            else if (userRole == "Warehouse")
            {
                query += @"
                    WHERE (Status = 'Müşteriden Cihaz Bekleniyor' 
                    OR Status = 'Teslime Hazır') 
                    ";
            }
            else if (userRole == "Admin")
            {
                query += @"
                    WHERE Status = 'Rapor Edildi' 
                    ";
            }

            if (cihazTipi != "Tümü")
            {
                query += $" AND DeviceType = '{cihazTipi}'";
            }
            if (marka != "Tümü")
            {
                query += $" AND Brand = '{marka}'";
            }
            /*if (durum != "Tümü")
            {
                query += $" OR Status = '{durum}'";
            }*/
            if (!string.IsNullOrWhiteSpace(musteriTelNo))
            {
                query += $" AND U.Phone LIKE '{musteriTelNo}'";
            }
            if (tarih == "En Yeni")
            {
                query += $" ORDER BY CreatedAt DESC";
            }
            else if (tarih == "En Eski")
            {
                query += $" ORDER BY CreatedAt ASC";
            }
            

            SqlCommand cmd = new SqlCommand(query, sqlConnection);

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



        public List<UserServices> GetServiceById(int serviceId, SqlConnection sqlConnection)
        {
            List<UserServices> services = new List<UserServices>();

            string query = @"
        SELECT *
        FROM ServiceRecords
        WHERE ServiceID = @ServiceID";

            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            cmd.Parameters.AddWithValue("@ServiceID", serviceId);

            SqlDataReader reader = null;

            try
            {
                sqlConnection.Open();

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
                if (sqlConnection.State == ConnectionState.Open)
                    sqlConnection.Close();
            }

            return services;
        }



        public Panel CreateServiceCard(UserServices service, Form formToOpen)
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
            lblDetail.Click += (s, e) =>
            {
                try
                {
                    formToOpen.Show();
                }
                catch
                {
                    MessageBox.Show("Detaylar açılamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            };


            // === CONTROLLERİ EKLE (TERSTEN) ===
            panel.Controls.Add(lblStatus);
            panel.Controls.Add(lblModel);
            panel.Controls.Add(lblBrand);
            panel.Controls.Add(pic);
            panel.Controls.Add(lblTitle);
            panel.Controls.Add(lblDetail);

            // === TÜM KARTA TIKLAMA ===
            //AddClickRecursive(panel, service.ServiceID);

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

        public string GetUserRole(int userId, SqlConnection con)
        {
            string role = string.Empty;
            try
            {
                con.Open();
                string query = "SELECT Role FROM Users WHERE UserID = @UserID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserID", userId);
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    role = result.ToString();
                }
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return role;
        }

        public bool DeleteServiceRecord(int serviceId, SqlConnection con)
        {
            bool success = false;
            try
            {
                con.Open();
                string query = "DELETE FROM ServiceRecords WHERE ServiceID = @ServiceID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ServiceID", serviceId);
                int rowsAffected = cmd.ExecuteNonQuery();
                success = rowsAffected > 0;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return success;
        }

        public void ResimKaydet(Image image, string imageName = "Resim")
        {
            if (image == null)
            {
                MessageBox.Show("Kaydedilecek resim yok.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Title = "Resim Kaydet";
                sfd.Filter = "PNG Görsel (*.png)|*.png|JPEG Görsel (*.jpg;*.jpeg)|*.jpg;*.jpeg"; // |Bitmap (*.bmp)|*.bmp
                sfd.DefaultExt = "jpg";
                sfd.AddExtension = true;
                sfd.FileName = imageName;

                if (sfd.ShowDialog() != DialogResult.OK)
                    return;

                ImageFormat format = ImageFormat.Png;
                string ext = Path.GetExtension(sfd.FileName)?.ToLowerInvariant();

                if (ext == ".jpg" || ext == ".jpeg")
                    format = ImageFormat.Jpeg;
                //else if (ext == ".bmp")
                //format = ImageFormat.Bmp;

                try
                {
                    // Image.Save will overwrite if the file exists
                    image.Save(sfd.FileName, format);
                    MessageBox.Show("Resim başarıyla kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Resim kaydedilemedi: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public bool AccpetRequestByTechnicianID(int ServiceId, int TechnicianId, SqlConnection con)
        {
            try
            {
                con.Open();
                string query = "UPDATE ServiceRecords SET AssignedStaffID = @AssignedStaffID, Status = @Status WHERE ServiceID = @ServiceID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@AssignedStaffID", TechnicianId);
                cmd.Parameters.AddWithValue("@Status", "Müşteriden Cihaz Bekleniyor");
                cmd.Parameters.AddWithValue("@ServiceID", ServiceId);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
                return false;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
        }

        public bool AddToServiceOperations(int ServiceId, string Description, SqlConnection con, double Cost = 0)
        {
            SqlCommand cmd = new SqlCommand();

            try
            {
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = @"INSERT INTO ServiceOperations (ServiceID, Description, Cost) 
                            VALUES (@ServiceID, @Description, @Cost)";

                cmd.Parameters.AddWithValue("@ServiceID", ServiceId);
                cmd.Parameters.AddWithValue("@Description", Description);
                cmd.Parameters.AddWithValue("@Cost", Cost);

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
                return false;
            }
            finally
            {
                // Bağlantıyı kapat
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
        }

        public bool ChangeServiceStatus(int ServiceId, string NewStatus, SqlConnection con)
        {
            try
            {
                con.Open();
                string query = "UPDATE ServiceRecords SET Status = @Status WHERE ServiceID = @ServiceID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Status", NewStatus);
                cmd.Parameters.AddWithValue("@ServiceID", ServiceId);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
                return false;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
        }

        public bool MuhasebeyeGonder(int ServiceId, string Description, SqlConnection con)
        {
            /*
            if(!AddToServiceOperations(ServiceId, Description, con))
                return false;
            else if(!ChangeServiceStatus(ServiceId, "Ücret Hesaplanıyor", con))
                return false;
            else
                return true;
            */
            return AddToServiceOperations(ServiceId, Description, con) && ChangeServiceStatus(ServiceId, "Ücret Hesaplanıyor", con);
        }

        public bool ChangeServiceOperationDescription(int OperationId, string NewDescription, SqlConnection con)
        {
            try
            {
                con.Open();
                string query = "UPDATE ServiceOperations SET Description = @Description WHERE OperationID = @OperationID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Description", NewDescription);
                cmd.Parameters.AddWithValue("@OperationID", OperationId);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
                return false;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
        }

        public bool CloseService(int serviceId, SqlConnection con)
        {
            try
            {
                con.Open();
                string query = "UPDATE ServiceRecords SET ClosedAt = GETDATE() WHERE ServiceID = @ServiceID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ServiceID", serviceId);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
                return false;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
        }

        public bool TeslimeHazirEt(int ServiceId, string Description, SqlConnection con)
        {
            return ChangeServiceStatus(ServiceId, "Teslime Hazır", con)
                && ChangeServiceOperationDescription(ServiceId, Description, con)
                && CloseService(ServiceId, con);
        }

        public List<ServiceOperations> GetServiceOperationsByServiceId(int serviceId, SqlConnection con)
        {
            List<ServiceOperations> operations = new List<ServiceOperations>();

            string query = @"
            SELECT *
            FROM ServiceOperations
            WHERE ServiceID = @ServiceID";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ServiceID", serviceId);

            SqlDataReader reader = null;

            try
            {
                con.Open();

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ServiceOperations operation = new ServiceOperations();

                    operation.OperationID = Convert.ToInt32(reader["OperationID"]);
                    operation.ServiceID = Convert.ToInt32(reader["ServiceID"]);
                    operation.Description = reader["Description"].ToString();
                    operation.Cost = Convert.ToDouble(reader["Cost"]);
                    operation.PerformedAt = Convert.ToDateTime(reader["PerformedAt"]);

                    operations.Add(operation);
                }
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();

                cmd.Dispose();
                if (con.State == ConnectionState.Open)
                    con.Close();
            }

            return operations;



        }

        public bool UcretBelirle(int ServiceId, double Cost, SqlConnection con)
        {
            if (!ChangeServiceStatus(ServiceId, "Ücret Onayı Bekleniyor", con)) return false;

            try
            {
                con.Open();
                string query = "UPDATE ServiceOperations SET Cost = @Cost WHERE ServiceID = @ServiceID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Cost", Cost);
                cmd.Parameters.AddWithValue("@ServiceID", ServiceId);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
                return false;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }

        }

        public bool AcceptPriceOfService(int serviceID, SqlConnection con)
        {
            return ChangeServiceStatus(serviceID, "İşlemde", con);
        }

        public bool RejectPriceOfService(int serviceID, SqlConnection con)
        {
            return ChangeServiceStatus(serviceID, "Teslime Hazır", con);
        }

        public bool GetDeviceFromCustomer(int serviceID, SqlConnection con)
        {
            return ChangeServiceStatus(serviceID, "Cihaz Kontrol Ediliyor", con);
        }

        public bool GiveDeviceToCustomer(int serviceID, SqlConnection con)
        {
            return ChangeServiceStatus(serviceID, "Tamamlandı", con);
        }

        public bool ReportServiceByTechnicianID(int serviceId, int technicianId, string Description, SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand();

            try
            {
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = @"
                INSERT INTO ServiceReports (ServiceID, TechnicianID, Description)
                VALUES (@ServiceID, @TechnicianID, @Description);

                UPDATE ServiceRecords SET Status = @Status WHERE ServiceID = @ServiceID;";

                cmd.Parameters.AddWithValue("@ServiceID", serviceId);
                cmd.Parameters.AddWithValue("@TechnicianID", technicianId);
                cmd.Parameters.AddWithValue("@Description", Description);
                cmd.Parameters.AddWithValue("@Status", "Rapor Edildi");


                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
                return false;
            }
            finally
            {
                // Bağlantıyı kapat
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }

        }

        public void ChangeRadioButtonColor(RadioButton radioButton, Color? enableColor = null, Color? disableColor = null)
        {
            Color enabled = enableColor ?? Color.Yellow;
            Color disabled = disableColor ?? Color.White;

            radioButton.ForeColor = radioButton.Checked ? enabled : disabled;
        }

        public bool CommentServiceByID(int ServiceId, int UserId, string Description, int Rating, SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand();

            try
            {
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = @"
                INSERT INTO ServiceComments (ServiceID, CustomerID, Rating, CommentText)
                VALUES (@ServiceID, @CustomerID, @Rating, @CommentText);";

                cmd.Parameters.AddWithValue("@ServiceID", ServiceId);
                cmd.Parameters.AddWithValue("@CustomerID", UserId);
                cmd.Parameters.AddWithValue("@CommentText", Description);
                cmd.Parameters.AddWithValue("@Rating", Rating);


                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
                return false;
            }
            finally
            {
                // Bağlantıyı kapat
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
        }

        public bool isServiceCommented(int ServiceId, SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = null;

            try
            {
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = @"SELECT *
                    FROM ServiceComments 
                    WHERE ServiceID = @ServiceID";

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ServiceID", ServiceId);

                reader = cmd.ExecuteReader();

                if (reader.HasRows) // HasRows + Read yerine direkt Read
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
                return false;
            }
            finally
            {
                if (reader != null) reader.Close();
                if (con.State == ConnectionState.Open) con.Close();
            }

        }

        public bool CancelServiceByID(int ServiceId, SqlConnection con)
        {
            return ChangeServiceStatus(ServiceId, "İptal Edildi", con);
        }

        public bool CancelServiceReportByID(int ServiceId, SqlConnection con)
        {
            try
            {
                con.Open();
                string query = @"DELETE FROM ServiceReports WHERE ServiceID = @ServiceID; 
                    UPDATE ServiceRecords SET Status = 'Talep Alındı' WHERE ServiceID = @ServiceID";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@ServiceID", ServiceId);
                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
                return false;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }

        }

        public List<ServiceReports> GetServiceReportByID(int ServiceId, SqlConnection con)
        {
            List<ServiceReports> reports = new List<ServiceReports>();

            string query = @"
            SELECT *
            FROM ServiceReports
            WHERE ServiceID = @ServiceID";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ServiceID", ServiceId);

            SqlDataReader reader = null;

            try
            {
                con.Open();

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ServiceReports report = new ServiceReports();

                    report.ServiceID = Convert.ToInt32(reader["ServiceID"]);
                    report.TechnicianID = Convert.ToInt32(reader["TechnicianID"]);
                    report.Description = reader["Description"].ToString();
                    report.CreatedAt = Convert.ToDateTime(reader["CreatedAt"]);

                    reports.Add(report);
                }
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();

                cmd.Dispose();
                if (con.State == ConnectionState.Open)
                    con.Close();
            }

            return reports;
        }
    }
}
