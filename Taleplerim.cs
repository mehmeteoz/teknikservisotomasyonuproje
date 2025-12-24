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
    public partial class Taleplerim : Form
    {
        int CurrentUserID;
        SqlConnection con;
        Fonksiyonlar fonksiyonlar = new Fonksiyonlar();
        public Taleplerim(int UserID, SqlConnection conn)
        {
            InitializeComponent();
            con = conn;
            CurrentUserID = UserID;
        }

        private void requestsFLPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Taleplerim_Load(object sender, EventArgs e)
        {
            requestsFLPanel.Dock = DockStyle.Fill;
            requestsFLPanel.AutoScroll = true;
            requestsFLPanel.WrapContents = true;
            requestsFLPanel.FlowDirection = FlowDirection.LeftToRight;
            requestsFLPanel.Padding = new Padding(10, 10, 10, 50);


            try
            {
                con.Open();

                // Giriş yapan kullanıcının servisleri
                List<UserServices> services =
                    fonksiyonlar.GetUserServices(CurrentUserID, con);

                if (services.Count == 0)
                {
                    Label lblEmpty = new Label
                    {
                        Text = "Henüz oluşturulmuş bir servis talebiniz yok.",
                        AutoSize = true,
                        Font = new Font("Segoe UI", 10, FontStyle.Italic),
                        ForeColor = Color.Gray
                    };

                    requestsFLPanel.Controls.Add(lblEmpty);
                    return;
                }

                foreach (UserServices service in services)
                {
                    Panel card = fonksiyonlar.CreateServiceCard(service);
                    requestsFLPanel.Controls.Add(card);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Servis talepleri yüklenirken hata oluştu.\n\n" + ex.Message,
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }

            Panel spacer = new Panel();
            spacer.Height = 20;
            spacer.Width = requestsFLPanel.ClientSize.Width; // 🔴 KRİTİK
            spacer.Margin = new Padding(0);

            requestsFLPanel.Controls.Add(spacer);

        }


    }
}
