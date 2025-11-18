using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeknikServisOtomasyonuProje
{
    public partial class GirisForm : Form
    {
        public GirisForm()
        {
            InitializeComponent();
        }

        private void KullaniciGirisBtn_Click(object sender, EventArgs e)
        {
            var kullaniciForm = new KullaniciGirisForm();
            // Hide the main (startup) form instead of closing it — closing the startup form
            // will terminate the application and close any other open forms.
            this.Hide();
            // When the user form closes, show this form again.
            kullaniciForm.FormClosed += (s, args) => this.Show();
            kullaniciForm.Show();
        }
        private void KullaniciGirisForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // No action here. The GirisForm that opened this form will handle showing itself again.
        }
    }
}
