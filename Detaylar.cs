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

        string price = "0";
        string resimIsim = "Resim";
        bool isServiceReported = false, isServiceCanceled = false;
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

            acceptPriceBtn.Enabled = false; //
            acceptPriceBtn.Visible = false;
            priceLbl.Visible = false;
            label2.Visible = false;
            rejectPriceBtn.Enabled = false;
            rejectPriceBtn.Visible = false;

            List<UserServices> service = new List<UserServices>();
            service = fonksiyonlar.GetServiceById(serviceID, con);

            List<ServiceOperations> serviceOperations = new List<ServiceOperations>();

            string userRole = fonksiyonlar.GetUserRole(userID, con);
            if (userRole == "Staff")
            {
                fotoDownloadBtn.Enabled = true; //
                fotoDownloadBtn.Visible = true;
                if (service[0].Status == "Talep Alındı")
                {
                    raporBtn.Enabled = true; //
                    raporBtn.Visible = true;
                    acceptBtn.Enabled = true; //
                    acceptBtn.Visible = true;
                }
            }
            else if (userRole == "Admin")
            {
                fotoDownloadBtn.Enabled = true; //
                fotoDownloadBtn.Visible = true;
                if (service[0].Status == "Talep Alındı" || service[0].Status == "Rapor Edildi")
                {
                    talepIptalBtn.Enabled = true; //
                    talepIptalBtn.Visible = true;
                    
                }
            }
            else if (userRole == "Customer")
            {
                if (service[0].Status == "Ücret Onayı Bekleniyor")
                {
                    serviceOperations = fonksiyonlar.GetServiceOperationsByServiceId(serviceID, con);
                    priceLbl.Text = serviceOperations[0].Cost.ToString("C2") + " TL";
                    price = serviceOperations[0].Cost.ToString("C2") + " TL";
                    acceptPriceBtn.Enabled = true; //
                    acceptPriceBtn.Visible = true;
                    priceLbl.Visible = true;
                    label2.Visible = true;
                    rejectPriceBtn.Enabled = true;
                    rejectPriceBtn.Visible = true;
                }
                else if (service[0].Status == "Talep Alındı")
                {
                    silBtn.Enabled = true; //
                    silBtn.Visible = true;
                }
                else if (service[0].Status == "İptal Edildi")
                {
                    silBtn.Enabled = true; //
                    silBtn.Visible = true;
                    isServiceCanceled = true;
                }
                else if (service[0].Status == "Rapor Edildi")
                {
                    silBtn.Enabled = true; //
                    silBtn.Visible = true;
                    isServiceReported = true;
                }
                else if (service[0].Status == "Tamamlandı")
                {
                    if (!fonksiyonlar.isServiceCommented(serviceID, con))
                    {
                        commentBtn.Enabled = true; //
                        commentBtn.Visible = true;
                    }

                }

            }


                

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
            this.Close();
            MessageBox.Show("Talep başarıyla kabul edildi.");
        }

        private void statusLbl_MouseHover(object sender, EventArgs e)
        {
            // Show tooltip with full status text
            ToolTip toolTip = new ToolTip();
            string toolTipText = statusLbl.Text;
            if (isServiceReported) 
            {
                toolTipText = "Servis talebiniz raporlanmıştır. Talep raporlanma sebeplerini websitemizden talep kuralları kısmında görebilirsiniz.";
            }
            else if (isServiceCanceled) 
            {
                toolTipText = "Servis talebiniz iptal edilmiştir. Talep iptal sebeplerini websitemizden talep kuralları kısmında görebilirsiniz.";
            }

                toolTip.SetToolTip(statusLbl, toolTipText);
        }

        private void acceptPriceBtn_Click(object sender, EventArgs e)
        {
            // ücreti onayla butonu
            DialogResult dialogResult = MessageBox.Show(("Bu ücreti onaylamak istediğinize emin misiniz? \nCihaz işleme geçicektir.\n" + price), 
                "Ücret Onayını Onayla", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.No) return;

            //fonksiyonlar.AcceptPriceOfService(serviceID, con);
            if (!fonksiyonlar.AcceptPriceOfService(serviceID, con)) return;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //reddet butonu
            DialogResult dialogResult = MessageBox.Show("Bu talebi reddetmek istediğinize emin misiniz? Bu işlem geri alınamaz.\n " +
                "Reddederseniz cihazı teslim almanız gerekir.", "Reddetme İşlemini Onayla", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.No) return;

            if (!fonksiyonlar.RejectPriceOfService(serviceID, con)) return;
            this.Close();
        }

        private void raporBtn_Click(object sender, EventArgs e)
        {
            /*
            if (!fonksiyonlar.ReportServiceByTechnicianID(serviceID, userID, "desc", con))
            {
                MessageBox.Show("Rapor edilirken bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show("Servis rapor edildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
            */
            ReportForm reportForm = new ReportForm(serviceID, userID, this);
            //reportForm.Show();
            // open in middle of the screen
            reportForm.StartPosition = FormStartPosition.CenterScreen;
            reportForm.Show(); // 

        }

        private void commentBtn_Click(object sender, EventArgs e)
        {
            CommentForm commentForm = new CommentForm(serviceID, userID, this);
            commentForm.StartPosition = FormStartPosition.CenterScreen;
            commentForm.Show();
        }
    }
}
