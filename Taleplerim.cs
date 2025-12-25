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
        bool isWorksOn = false;


        public Taleplerim(int UserID, bool isWorksOnPage = false)
        {
            InitializeComponent();
            con = connect.connectToSQL();
            CurrentUserID = UserID;
            isWorksOn = isWorksOnPage;
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

            bool isTechnician = fonksiyonlar.GetUserRole(CurrentUserID, con) == "Staff" ? true : false;

            // Populate services
            PopulateServices(isTechnician, isWorksOn);
        }

        private void PopulateServices(bool isTechnician = false, bool isWorksOn = false)
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
                Text = isTechnician ? "Talepler" : "Taleplerim",
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

                /*
                List<UserServices> services = isTechnician 
                    ? fonksiyonlar.GetGotServices(con)
                    : fonksiyonlar.GetUserServices(CurrentUserID, con);*/

                List<UserServices> services = new List<UserServices>();
                if (isTechnician)
                {
                    if (isWorksOn)
                        services = fonksiyonlar.GetTechniciansServices(CurrentUserID ,con);
                    else
                        services = fonksiyonlar.GetGotServices(con);
                }
                else
                {
                    services = fonksiyonlar.GetUserServices(CurrentUserID, con);
                }

                if (services.Count == 0)
                {
                    Label lblEmpty = new Label
                    {
                        Text = isTechnician ? "Henüz oluşturulmuş bir servis talebi yok." : "Henüz oluşturulmuş bir servis talebiniz yok.",
                        AutoSize = true,
                        Font = new Font("Segoe UI", 10, FontStyle.Italic),
                        ForeColor = Color.Gray
                    };

                    requestsFLPanel.Controls.Add(lblEmpty);
                    return;
                }

                foreach (UserServices service in services)
                {
                    Form detay;
                    // create a single Detaylar instance per service so we can listen for its close event
                    if (!isWorksOn)
                    {
                        detay = new Detaylar(service.ServiceID, CurrentUserID);
                        detay.FormClosed += (s, e) =>
                        {
                            // Ensure refresh runs on UI thread
                            if (this.IsHandleCreated && !this.IsDisposed)
                            {
                                this.BeginInvoke((Action)(() => PopulateServices(isTechnician, false)));
                            }
                        };
                    }
                    else
                    {
                        detay = new DetaylarTeknik(service.ServiceID, CurrentUserID);
                        detay.FormClosed += (s, e) =>
                        {
                            // Ensure refresh runs on UI thread and keep the isWorksOn context
                            if (this.IsHandleCreated && !this.IsDisposed)
                            {
                                this.BeginInvoke((Action)(() => PopulateServices(isTechnician, true)));
                            }
                        };
                    }

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
