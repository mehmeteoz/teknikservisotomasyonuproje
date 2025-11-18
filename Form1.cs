using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TeknikServisOtomasyonuProje
{
    public partial class Form1 : Form
    {
        SQLConnect sqlConnect = new SQLConnect();
        SqlConnection con;
        public Form1()
        {
            InitializeComponent();
            con = sqlConnect.connectToSQL();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string id = textBox1.Text;

            if (id.Trim() == "")
            {
                MessageBox.Show("Lütfen ID giriniz.");
                return;
            }

            try
            {
                con.Open();

                string query = "SELECT name, password FROM TestUser WHERE id = @id";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    nameLabel.Text = dr["name"].ToString();
                    passLabel.Text = dr["password"].ToString();
                }
                else
                {
                    MessageBox.Show("Bu ID'ye ait kayıt bulunamadı.");
                    nameLabel.Text = "İsim";
                    passLabel.Text = "Şifre";
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }
    }
}
