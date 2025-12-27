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
    public partial class ReportForm : Form
    {
        Fonksiyonlar fonksiyonlar = new Fonksiyonlar();
        SQLConnect connect = new SQLConnect();
        SqlConnection con;

        int serviceID;
        int userID;

        Detaylar detaylar;

        public ReportForm(int ServiceId, int UserId, Detaylar detaylarForm)
        {
            InitializeComponent();
            con = connect.connectToSQL();
            serviceID = ServiceId;
            userID = UserId;
            WindowDragHelper.EnableDrag(label1, this);
            detaylar = detaylarForm;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(raporTBox.Text))
            {
                MessageBox.Show("Lütfen rapor metnini giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!fonksiyonlar.ReportServiceByTechnicianID(serviceID, userID, raporTBox.Text.Trim(), con))
            {
                MessageBox.Show("Rapor kaydedilirken bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show("Rapor başarıyla kaydedildi.", "Rapor Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
            detaylar.Close();
            // close the detaylarteknik form if it's open

        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
