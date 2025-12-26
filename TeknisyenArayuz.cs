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
        string userRole;
        List<User> userCredentials = new List<User>();
        int userID;
        bool isAcc = false;
        bool isWareh = false;

        public TeknisyenArayuz(int CurrentUserId, string UserRole = "Staff")
        {
            InitializeComponent();
            con = connect.connectToSQL();
            userID = CurrentUserId;
            userCredentials = fonksiyonlar.GetUserInfo(userID, con);
            WindowDragHelper.EnableDrag(panel1, this, formTitle);
            this.userRole = UserRole;
            isAcc = UserRole == "Accountant" ? true : false;
            isWareh = UserRole == "Warehouse" ? true : false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fonksiyonlar.formGetir(new Taleplerim(userID, isAccountant:isAcc, isWarehouse:isWareh), panel3);
        }

        private void TeknisyenArayuz_Load(object sender, EventArgs e)
        {
            userRole = userCredentials[0].Role;
            nameLabel.Text = userCredentials[0].FirstName + " " + userCredentials[0].LastName;
            roleLbl.Text = userRole;
            if (userRole == "Accountant" || userRole == "Warehouse")
            {
                button2.Visible = false;
                button2.Enabled = false;
            }
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

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
