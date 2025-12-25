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
    public partial class TeknisyenArayuz : Form
    {
        SQLConnect connect = new SQLConnect();
        Fonksiyonlar fonksiyonlar = new Fonksiyonlar();
        SqlConnection con;
        List<User> userCredentials = new List<User>();
        int userID;

        public TeknisyenArayuz(int CurrentUserId)
        {
            InitializeComponent();
            con = connect.connectToSQL();
            userID = CurrentUserId;
            userCredentials = fonksiyonlar.GetUserInfo(userID, con);
            WindowDragHelper.EnableDrag(panel1, this, formTitle);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fonksiyonlar.formGetir(new Taleplerim(userID), panel3);
        }

        private void TeknisyenArayuz_Load(object sender, EventArgs e)
        {
            nameLabel.Text = userCredentials[0].FirstName + " " + userCredentials[0].LastName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fonksiyonlar.formGetir(new Taleplerim(userID, true), panel3);
        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
            new KullaniciGirisForm().Show();
        }
    }
}
