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
    public partial class KullanıcıArayuzForm : Form
    {
        int UserId;
        List<User> currentUser = new List<User>();
        SQLConnect sqlConnect = new SQLConnect();
        SqlConnection con;
        Fonksiyonlar fonksiyonlar = new Fonksiyonlar();
        public KullanıcıArayuzForm(int currentUserId)
        {
            InitializeComponent();
            WindowDragHelper.EnableDrag(panel1, this);
            UserId = currentUserId;
            con = sqlConnect.connectToSQL();
            
            currentUser = fonksiyonlar.GetUserInfo(UserId, con);
        }

        private void talepOlusturLabel_Click(object sender, EventArgs e)
        {

        }

        private void KullanıcıArayuzForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //new GirisForm().Show();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
            new KullaniciGirisForm().Show();
        }

        private void KullanıcıArayuzForm_Load(object sender, EventArgs e)
        {
            nameLabel.Text = currentUser[0].FirstName + " " + currentUser[0].LastName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fonksiyonlar.formGetir(new TalepOlustur(UserId, con), panel3);
        }
    }
}
