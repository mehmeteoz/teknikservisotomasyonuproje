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
    public partial class CommentForm : Form
    {
        Fonksiyonlar fonksiyonlar = new Fonksiyonlar();
        SQLConnect connect = new SQLConnect();
        SqlConnection con;
        int serviceID;
        int userID;
        Detaylar detaylar;

        public CommentForm(int ServiceID, int UserID, Detaylar detaylarForm)
        {
            InitializeComponent();
            con = connect.connectToSQL();
            WindowDragHelper.EnableDrag(label1, this);
            serviceID = ServiceID;
            userID = UserID;
            detaylar = detaylarForm;
        }

        Color changeColorTo = Color.Yellow;
        int selectedRating = 0;
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            fonksiyonlar.ChangeRadioButtonColor(radioButton1, changeColorTo, Color.White);
            if (radioButton1.Checked)
            {
                selectedRating = 1;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            fonksiyonlar.ChangeRadioButtonColor(radioButton2, changeColorTo, Color.White);
            if (radioButton2.Checked)
            {
                selectedRating = 2;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            fonksiyonlar.ChangeRadioButtonColor(radioButton3, changeColorTo, Color.White);
            if (radioButton3.Checked)
            {
                selectedRating = 3;
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            fonksiyonlar.ChangeRadioButtonColor(radioButton4, changeColorTo, Color.White);
            if (radioButton4.Checked)
            {
                selectedRating = 4;
            }
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            fonksiyonlar.ChangeRadioButtonColor(radioButton5, changeColorTo, Color.White);
            if (radioButton5.Checked)
            {
                selectedRating = 5;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(commentTBox.Text))
            {
                MessageBox.Show("Lütfen yorum metnini giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!(radioButton1.Checked || radioButton2.Checked || radioButton3.Checked || radioButton4.Checked || radioButton5.Checked))
            {
                MessageBox.Show("Lütfen yorumu değerlendiriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!fonksiyonlar.CommentServiceByID(serviceID, userID, commentTBox.Text.Trim() ,selectedRating, con))
            {
                MessageBox.Show("Yorum kaydedilirken bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show("Yorum başarıyla kaydedildi.", "Yorum Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
            detaylar.Close();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}