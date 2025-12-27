using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace TeknikServisOtomasyonuProje
{
    public partial class Taleplerim : Form
    {
        int CurrentUserID;
        SqlConnection con;
        Fonksiyonlar fonksiyonlar = new Fonksiyonlar();
        SQLConnect connect = new SQLConnect();
        bool isWorksOn = false;
        bool isAcc = false;
        bool isWareh = false;
        bool isAdm = false;


        public Taleplerim(int UserID, bool isWorksOnPage = false, bool isAccountant = false, bool isWarehouse = false, bool isAdmin = false)
        {
            InitializeComponent();
            con = connect.connectToSQL();
            CurrentUserID = UserID;
            isWorksOn = isWorksOnPage;
            isAcc = isAccountant;
            isWareh = isWarehouse;
            isAdm = isAdmin;
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
            PopulateServices(isTechnician, isWorksOn, isAcc, isWareh, isAdm);
        }

        
        string selectedCihazTipi = "Tümü";
        string selectedMarka = "Tümü";
        string selectedTarih = "En Yeni";
        string selectedMusteriTel = "";
        private void PopulateServices(bool isTechnician = false, bool isWorksOn = false, bool isAccountant = false, bool isWarehouse = false, bool isAdmin = false)
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

            string textTalepler;
            if (isTechnician)
            {
                textTalepler = "Talepler";
                if (isWorksOn)
                {
                    textTalepler = "Taleplerim";
                }
            }
            else if (isAccountant)
            {
                textTalepler = "Muhasebe Talepleri";
            }
            else if (isWarehouse)
            {
                textTalepler = "Depo Talepleri";
            }
            else if (isAdmin)
            {
                textTalepler = "Raporlanan Talepler";
            }
            else
            {
                textTalepler = "Taleplerim";
            }

            Label taleplerimLabel = new Label
            {
                Text = textTalepler,
                //Text = isTechnician ? "Talepler" : "Taleplerim",
                Font = new Font("Arial", 20, FontStyle.Bold),
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Padding = new Padding(10, 0, 0, 0),
                BackColor = Color.FromArgb(17, 17, 17),
            };

            headerPanel.Controls.Add(taleplerimLabel);

            FlowLayoutPanel filterPanel = new FlowLayoutPanel
            {
                Height = 100,
                Width = requestsFLPanel.ClientSize.Width - 30, // scrollbar payı
                BackColor = Color.FromArgb(25, 25, 25),
                Margin = new Padding(5, 0, 5, 15)
            };

            Label CreateFilterLabel(string text)
            {
                return new Label
                {
                    Text = text,
                    ForeColor = Color.White,
                    Font = new Font("Arial", 10, FontStyle.Bold),
                    AutoSize = true,
                    Margin = new Padding(10, 20, 5, 0)
                };
            }

            ComboBox CreateFilterComboBox()
            {
                return new ComboBox
                {
                    Width = 140,
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Margin = new Padding(0, 15, 15, 0),
                };
            }

            Label cihazTipiLbl = CreateFilterLabel("Cihaz Tipi");
            ComboBox cihazTipiCBx = CreateFilterComboBox();
            
            
            cihazTipiCBx.Items.AddRange(new string[]
            {
            "Tümü",
            "Telefon",
            "Masaüstü",
            "Laptop"
            });
            cihazTipiCBx.SelectedItem = selectedCihazTipi;

            cihazTipiCBx.SelectedIndexChanged += (s, e) =>
            {
                selectedCihazTipi = cihazTipiCBx.SelectedItem.ToString();
                //ApplyFilters();
            };
            

            Label markaLbl = CreateFilterLabel("Marka");
            ComboBox markaCBx = CreateFilterComboBox();

            
            markaCBx.Items.AddRange(new string[]
            {
            "Tümü",
            "Apple",
            "Samsung",
            "Lenovo",
            "Asus",
            "Acer"
            });
            markaCBx.SelectedItem = selectedMarka;

            markaCBx.SelectedIndexChanged += (s, e) =>
            {
                selectedMarka = markaCBx.SelectedItem.ToString();
                //ApplyFilters();
            };
            

            Label tarihLbl = CreateFilterLabel("Tarih");
            ComboBox tarihCBx = CreateFilterComboBox();

            
            tarihCBx.Items.AddRange(new string[]
            {
            "En Yeni",
            "En Eski"
            });
            tarihCBx.SelectedItem = selectedTarih;


            tarihCBx.SelectedIndexChanged += (s, e) =>
            {
                selectedTarih = tarihCBx.Text.ToString(); // sıralama olduğu için nullable bırakıyoruz
                //ApplyFilters();
            };
            /*
            Label durumLbl = CreateFilterLabel("Durum");
            ComboBox durumCBx = CreateFilterComboBox();


            durumCBx.Items.AddRange(new string[]
            {
                "Tümü",
                "Beklemede",
                "Devam Ediyor",
                "Tamamlandı"
            });
            durumCBx.Text = "Tümü";

            durumCBx.SelectedIndexChanged += (s, e) =>
            {
                selectedDurum = durumCBx.SelectedItem.ToString();
                //ApplyFilters();
            };
            */

            Label musteriTelLbl = CreateFilterLabel("Müşteri Telefon");
            TextBox musteriTelTBx = new TextBox
            {
                Width = 140,
                Margin = new Padding(0, 15, 15, 0),
            };
            musteriTelTBx.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(musteriTelTBx.Text)) return;
                selectedMusteriTel = musteriTelTBx.Text.Trim();
                selectedMusteriTel = selectedMusteriTel[0] == '0' ? selectedMusteriTel.Substring(1) : selectedMusteriTel; // başında sıfır olmasın
            };



            Button filtreUygulaBtn = new Button
            {
                Text = "Uygula",
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold),
                AutoSize = true,
                Margin = new Padding(10, 20, 5, 0)
            };
            filtreUygulaBtn.Click += (s, e) =>
            {
                PopulateServices(
                    isTechnician: isTechnician,
                    isWorksOn: isWorksOn,
                    isAccountant: isAccountant,
                    isWarehouse: isWarehouse,
                    isAdmin: isAdmin
                );
            };


            filterPanel.Controls.Add(cihazTipiLbl);
            filterPanel.Controls.Add(cihazTipiCBx);

            filterPanel.Controls.Add(markaLbl);
            filterPanel.Controls.Add(markaCBx);

            filterPanel.Controls.Add(tarihLbl);
            filterPanel.Controls.Add(tarihCBx);

            filterPanel.Controls.Add(musteriTelLbl);
            filterPanel.Controls.Add(musteriTelTBx);

            /*filterPanel.Controls.Add(durumLbl);
            filterPanel.Controls.Add(durumCBx);*/

            filterPanel.Controls.Add(filtreUygulaBtn);


            if ((isTechnician || isAccountant || isWarehouse || isAdmin) && !isWorksOn)
            {
                requestsFLPanel.Controls.Add(filterPanel);
                requestsFLPanel.SetFlowBreak(filterPanel, true);
            }

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
                        services = fonksiyonlar.GetGotServices(con, "Staff", selectedCihazTipi, selectedMarka, selectedTarih, selectedMusteriTel);
                }
                else if (isAccountant)
                {
                    services = fonksiyonlar.GetGotServices(con, "Accountant", selectedCihazTipi, selectedMarka, selectedTarih, selectedMusteriTel);
                }
                else if (isWarehouse)
                {
                    services = fonksiyonlar.GetGotServices(con, "Warehouse", selectedCihazTipi, selectedMarka, selectedTarih, selectedMusteriTel);
                }
                else if (isAdmin)
                {
                    services = fonksiyonlar.GetGotServices(con, "Admin", selectedCihazTipi, selectedMarka, selectedTarih, selectedMusteriTel);
                }
                else
                {
                    services = fonksiyonlar.GetUserServices(CurrentUserID, con);
                }

                if (services.Count == 0)
                {
                    string textTalep;
                    if (isTechnician)
                    {
                        textTalep = "Henüz oluşturulmuş bir servis talebi yok.";
                        if (isWorksOn)
                        {
                            textTalepler = "Henüz üzerinize kayıt edilmiş bir servis talebi yok.";
                        }
                    }
                    else if (isAccountant)
                    {
                        textTalep = "Henüz muhasebeye gönderilmiş bir servis talebi yok.";
                    }
                    else if (isWarehouse)
                    {
                        textTalep = "Henüz depoya gönderilmiş bir servis talebi yok.";
                    }
                    else if (isAdmin)
                    {
                        textTalep = "Henüz raporlanmış bir servis talebi yok.";
                    }
                    else
                    {
                        textTalep = "Henüz oluşturulmuş bir servis talebiniz yok.";
                    }

                    Label lblEmpty = new Label
                    {
                        Text = textTalep,
                        //Text = isTechnician ? "Henüz oluşturulmuş bir servis talebi yok." : "Henüz oluşturulmuş bir servis talebiniz yok.",
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
                    if (isAccountant)
                    {
                        detay = new DetaylarTeknik(service.ServiceID, CurrentUserID);
                        detay.FormClosed += (s, e) =>
                        {
                            // Ensure refresh runs on UI thread and keep the isWorksOn context
                            if (this.IsHandleCreated && !this.IsDisposed)
                            {
                                this.BeginInvoke((Action)(() => PopulateServices(isTechnician, false, true)));
                            }
                        };
                    }
                    else if (isWarehouse)
                    {
                        detay = new DetaylarTeknik(service.ServiceID, CurrentUserID);
                        detay.FormClosed += (s, e) =>
                        {
                            // Ensure refresh runs on UI thread and keep the isWorksOn context
                            if (this.IsHandleCreated && !this.IsDisposed)
                            {
                                this.BeginInvoke((Action)(() => PopulateServices(isTechnician, false, false, true)));
                            }
                        };
                    }
                    else if (isAdmin)
                    {
                        detay = new DetaylarTeknik(service.ServiceID, CurrentUserID);
                        detay.FormClosed += (s, e) =>
                        {
                            // Ensure refresh runs on UI thread and keep the isWorksOn context
                            if (this.IsHandleCreated && !this.IsDisposed)
                            {
                                this.BeginInvoke((Action)(() => PopulateServices(isTechnician, false, false, false, true)));
                            }
                        };
                    }
                    else if (!isWorksOn)
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
                    detay.StartPosition = FormStartPosition.CenterScreen;

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
