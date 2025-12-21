using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace TeknikServisOtomasyonuProje
{
    public partial class GirisForm : Form
    {

        public GirisForm()
        {
            InitializeComponent();
            WindowDragHelper.EnableDrag(label1, this);
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

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void GirisForm_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {

        }
    }
}
