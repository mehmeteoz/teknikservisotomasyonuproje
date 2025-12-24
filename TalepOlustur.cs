using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace TeknikServisOtomasyonuProje
{
    public partial class TalepOlustur : Form
    {
        int currentUserID;
        SqlConnection con;
        Fonksiyonlar fonksiyonlar = new Fonksiyonlar();
        public TalepOlustur(int UserID, SqlConnection conn)
        {
            InitializeComponent();
            currentUserID = UserID;
            con = conn;
        }

        private void TalepOlustur_Load(object sender, EventArgs e)
        {

        }

        bool isTextEmpitied = false;
        private void textBox3_Click(object sender, EventArgs e)
        {
            if (isTextEmpitied == true) return;


            descTBx.Text = string.Empty;
            isTextEmpitied = true;
        }

        private void fotoUploadBtn_Click(object sender, EventArgs e)
        {
            fonksiyonlar.ResimYukle(pictureBox1);
        }

        private void submitBtn_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(modelTBx.Text) || string.IsNullOrEmpty(descTBx.Text) || 
                pictureBox1.Image == null || string.IsNullOrEmpty(cihazTipCBx.Text) || 
                string.IsNullOrEmpty(markaCBx.Text) || string.IsNullOrEmpty(seriNoTBx.Text))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun ve bir resim yükleyin.", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tip = cihazTipCBx.Text;
            string marka = markaCBx.Text;
            string model = modelTBx.Text;
            string seriNo = seriNoTBx.Text;
            string desc = descTBx.Text;
            string imageBase64 = fonksiyonlar.ResmiBase64eCevir(pictureBox1.Image, System.Drawing.Imaging.ImageFormat.Jpeg);

            SqlCommand cmd = new SqlCommand();

            try
            {
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = @"INSERT INTO ServiceRecords (CustomerId, DeviceType, Brand, Model, SerialNumber, ProblemDescription, Status) 
                            VALUES (@CustomerID, @DeviceType , @Brand , @Model , @SerialNumber , @ProblemDescription, @Status)";

                //cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@CustomerID", currentUserID);
                cmd.Parameters.AddWithValue("@DeviceType", tip);
                cmd.Parameters.AddWithValue("@Brand", marka);
                cmd.Parameters.AddWithValue("@Model", model);
                cmd.Parameters.AddWithValue("@SerialNumber", seriNo);
                cmd.Parameters.AddWithValue("@ProblemDescription", desc);
                cmd.Parameters.AddWithValue("@Status", "Talep Alındı");


                cmd.ExecuteNonQuery();
                MessageBox.Show("Talep Başarıyla Alındı.");
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
    }
}
