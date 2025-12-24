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
        SQLConnect connect = new SQLConnect();


        public Taleplerim(int UserID, SqlConnection conn)
        {
            InitializeComponent();
            con = connect.connectToSQL();
            CurrentUserID = UserID;
        }

        private void requestsFLPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Taleplerim_Load(object sender, EventArgs e)
        {
            // Configure panel once
            requestsFLPanel.Dock = DockStyle.Fill;
            requestsFLPanel.AutoScroll = true;
            requestsFLPanel.WrapContents = true;
            requestsFLPanel.FlowDirection = FlowDirection.LeftToRight;
            requestsFLPanel.Padding = new Padding(10, 10, 10, 50);

            // Populate services
            PopulateServices();
        }

        private void PopulateServices()
        {
            // Clear existing controls
            requestsFLPanel.Controls.Clear();

            // Header
            Panel headerPanel = new Panel
            {
                Height = 60,
                Width = requestsFLPanel.ClientSize.Width - 25, // scrollbar payı
                BackColor = Color.FromArgb(40, 40, 40),
                Margin = new Padding(5, 5, 5, 15)
            };

            Label taleplerimLabel = new Label
            {
                Text = "Taleplerim",
                Font = new Font("Arial", 20, FontStyle.Bold),
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Padding = new Padding(10, 0, 0, 0),
                BackColor = Color.FromArgb(17, 17, 17),
            };

            headerPanel.Controls.Add(taleplerimLabel);
            requestsFLPanel.Controls.Add(headerPanel);
            requestsFLPanel.SetFlowBreak(headerPanel, true); // tek satır kaplasın

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
                    // create a single Detaylar instance per service so we can listen for its close event
                    Detaylar detay = new Detaylar(service.ServiceID, CurrentUserID);

                    // when details form closes, refresh the list (e.g., after a delete)
                    detay.FormClosed += (s, e) =>
                    {
                        // Ensure refresh runs on UI thread
                        if (this.IsHandleCreated && !this.IsDisposed)
                        {
                            this.BeginInvoke((Action)(() => PopulateServices()));
                        }
                    };

                    Panel card = fonksiyonlar.CreateServiceCard(service, detay);    
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

            headerPanel.Width = requestsFLPanel.ClientSize.Width - 25;  // scrollbar payı

            Panel spacer = new Panel();
            spacer.Height = 20;
            spacer.Width = requestsFLPanel.ClientSize.Width;
            spacer.Margin = new Padding(0);

            requestsFLPanel.Controls.Add(spacer);
        }


    }
}
