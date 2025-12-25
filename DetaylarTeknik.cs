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
    public partial class DetaylarTeknik : Form
    {
        SQLConnect sQLConnect = new SQLConnect();
        Fonksiyonlar fonksiyonlar = new Fonksiyonlar();
        SqlConnection con;
        int serviceID;
        int userID;

        public DetaylarTeknik(int ServiceId, int UserId)
        {
            InitializeComponent();
            con = sQLConnect.connectToSQL();
            serviceID = ServiceId;
            userID = UserId;
            WindowDragHelper.EnableDrag(panel1, this, formTitle);
        }

        string resimIsim = "Resim";
        private void DetaylarTeknik_Load(object sender, EventArgs e)
        {
            silBtn.Enabled = false; //
            silBtn.Visible = false;
            raporBtn.Enabled = false; //
            raporBtn.Visible = false;
            talepIptalBtn.Visible = false; // 
            talepIptalBtn.Enabled = false;
            fotoDownloadBtn.Enabled = false; //
            fotoDownloadBtn.Visible = false;
            string userRole = fonksiyonlar.GetUserRole(userID, con);
            if (userRole == "Staff")
            {
                raporBtn.Enabled = true; //
                raporBtn.Visible = true;
                fotoDownloadBtn.Enabled = true;
                fotoDownloadBtn.Visible = true;
            }
            else if (userRole == "Admin")
            {
                talepIptalBtn.Enabled = true; //
                talepIptalBtn.Visible = true;
                fotoDownloadBtn.Enabled = true; // 
                fotoDownloadBtn.Visible = true;
            }

            muhasebeyeGonderPanel.Enabled = false; //
            muhasebeyeGonderPanel.Visible = false;
            tamamlandıPanel.Enabled = false; //
            tamamlandıPanel.Visible = false;


            List<UserServices> service = new List<UserServices>();
            service = fonksiyonlar.GetServiceById(serviceID, con);
            if (userRole == "Staff")
            {
                if (service[0].Status == "Cihaz Kontrol Ediliyor")
                {
                    muhasebeyeGonderPanel.Enabled = true;
                    muhasebeyeGonderPanel.Visible = true;
                }
                else if (service[0].Status == "İşlemde")
                {
                    tamamlandıPanel.Enabled = true;
                    tamamlandıPanel.Visible = true;
                }
                
            }

            List<User> customer = new List<User>();
            customer = fonksiyonlar.GetUserInfo(service[0].CustomerID, con);

            resimIsim = "Servis_" + serviceID.ToString() + "_" + service[0].Brand + "_" + service[0].DeviceType;

            cihazTipiLbl.Text = service[0].DeviceType;
            markaLbl.Text = service[0].Brand;
            modelLbl.Text = service[0].Model;
            serialLbl.Text = service[0].SerialNumber;
            descTxtBx.Text = service[0].ProblemDescription;
            statusLbl.Text = service[0].Status;
            pictureBox1.Image = fonksiyonlar.Base64tenResmeCevir(service[0].Picture64);

            adLbl.Text = customer[0].FirstName;
            soyadLbl.Text = customer[0].LastName;
            telefonLbl.Text = customer[0].Phone;
            emailLbl.Text = customer[0].Email;


        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void statusLbl_MouseHover(object sender, EventArgs e)
        {
            // Show tooltip with full status text
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(statusLbl, statusLbl.Text);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // muhasebeye gönder butonu
            if (textBox1.Text == "Arıza sebebi ve gereken malzemeleri yazınız." || string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Lütfen arıza açıklamasını giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // arıza açıklamasını ekle bunu göndermek istediğinize emin misiniz diye sor
            DialogResult dialogResult = MessageBox.Show(("Arıza açıklamasını eklemek ve cihazı muhasebeye göndermek istediğinize emin misiniz?\n" + textBox1.Text), "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.No)
                return;

            if (!fonksiyonlar.MuhasebeyeGonder(serviceID, textBox1.Text, con))
            {
                MessageBox.Show("Muhasebeye gönderilirken bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.Close();

        }

        // Placeholder handling for arizaAciklamaTxtBx
        private const string ArizaPlaceholder = "Arıza sebebi ve gereken malzemeleri yazınız.";
        private bool isArizaCleared = false;

        private void arizaAciklamaTxtBx_Click(object sender, EventArgs e)
        {
            // Only clear when the textbox currently contains the placeholder
            if (isArizaCleared) return;

            if (string.Equals(arizaAciklamaTxtBx.Text, ArizaPlaceholder, StringComparison.Ordinal))
            {
                arizaAciklamaTxtBx.Text = string.Empty;
                isArizaCleared = true;
            }
        }


        private void tamamlandıBtn_Click(object sender, EventArgs e)
        {
            //tamamlandı butonu
            // "Arızanın sebebini ve cihazda nelerin tamir veya değişim edildiğini yazınız."
            if (textBox1.Text == ArizaPlaceholder2 || string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Lütfen arıza açıklamasını giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // arıza açıklamasını ekle bunu göndermek istediğinize emin misiniz diye sor
            DialogResult dialogResult = MessageBox.Show(("Arıza açıklamasını eklemek ve cihazın teslime hazır olduğuna emin misiniz?\n" + textBox1.Text), "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.No)
                return;

            if (!fonksiyonlar.TeslimeHazirEt(serviceID, textBox1.Text, con))
            {
                MessageBox.Show("Cihaz teslime hazır edilirken bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.Close();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        string ArizaPlaceholder2 = "Arızanın sebebini ve cihazda nelerin tamir veya değişim edildiğini yazınız.";
        private void textBox1_Click(object sender, EventArgs e)
        {
            if (isArizaCleared) return;
            if (string.Equals(textBox1.Text, ArizaPlaceholder2, StringComparison.Ordinal))
            {
                textBox1.Text = string.Empty;
                isArizaCleared = true;
            }


        }
    }
}
