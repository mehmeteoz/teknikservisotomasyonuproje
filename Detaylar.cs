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

namespace TeknikServisOtomasyonuProje
{
    public partial class Detaylar : Form
    {
        int serviceID;
        int userID;
        SqlConnection con;
        Fonksiyonlar fonksiyonlar = new Fonksiyonlar();
        SQLConnect connect = new SQLConnect();
        public Detaylar(int ServiceId, int UserId)
        {
            InitializeComponent();
            con = connect.connectToSQL();
            serviceID = ServiceId;
            userID = UserId;
            WindowDragHelper.EnableDrag(panel1, this, formTitle);
        }

        string resimIsim = "Resim";
        private void Detaylar_Load(object sender, EventArgs e)
        {
            silBtn.Enabled = false; //
            silBtn.Visible = false;
            acceptBtn.Enabled = false; //
            acceptBtn.Visible = false;
            raporBtn.Enabled = false; //
            raporBtn.Visible = false;
            talepIptalBtn.Visible = false; // 
            talepIptalBtn.Enabled = false;
            fotoDownloadBtn.Enabled = false;
            fotoDownloadBtn.Visible = false;
            commentBtn.Enabled = false; //
            commentBtn.Visible = false;
            string userRole = fonksiyonlar.GetUserRole(userID, con);
            if (userRole == "Staff")
            {
                acceptBtn.Enabled = true; //
                acceptBtn.Visible = true;
                raporBtn.Enabled = true; //
                raporBtn.Visible = true;
                fotoDownloadBtn.Enabled = true;
                fotoDownloadBtn.Visible = true;
            }
            else if (userRole == "Admin")
            {
                talepIptalBtn.Enabled = true; //
                talepIptalBtn.Visible = true;
                fotoDownloadBtn.Enabled = true;
                fotoDownloadBtn.Visible = true;
            }
            else if (userRole == "Customer")
            {
                silBtn.Enabled = true; //
                silBtn.Visible = true; 
                commentBtn.Enabled = true; //
                commentBtn.Visible = true;
            }

            List<UserServices> service = new List<UserServices>();
            service = fonksiyonlar.GetServiceById(serviceID, con);

            resimIsim = "Servis_" + serviceID.ToString() + "_" + service[0].Brand + "_" + service[0].DeviceType;

            cihazTipiLbl.Text = service[0].DeviceType;
            markaLbl.Text = service[0].Brand;
            modelLbl.Text = service[0].Model;
            serialLbl.Text = service[0].SerialNumber;
            descTxtBx.Text = service[0].ProblemDescription;
            statusLbl.Text = service[0].Status;
            pictureBox1.Image = fonksiyonlar.Base64tenResmeCevir(service[0].Picture64);


        }

        private void silBtn_Click(object sender, EventArgs e)
        {
            // ask the user if they are sure they want to delete
            DialogResult dialogResult = MessageBox.Show("Bu talebi silmek istediğinize emin misiniz?", "Silme İşlemini Onayla", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (!fonksiyonlar.DeleteServiceRecord(serviceID, con)) return;
                MessageBox.Show("Servis talebi silindi");
                this.Close();
            }
        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fotoUploadBtn_Click(object sender, EventArgs e)
        {
            // picture box taki resmi bilgisayara indirme
            fonksiyonlar.ResimKaydet(pictureBox1.Image, resimIsim);
        }

        private void acceptBtn_Click(object sender, EventArgs e)
        {
            if(!fonksiyonlar.AccpetRequestByTechnicianID(serviceID, userID, con))   return ;
            MessageBox.Show("Talep başarıyla kabul edildi.");
        }

        private void statusLbl_MouseHover(object sender, EventArgs e)
        {
            // Show tooltip with full status text
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(statusLbl, statusLbl.Text);
        }
    }
}
