namespace TeknikServisOtomasyonuProje
{
    partial class KullaniciGirisForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.passwordTB1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.emailTB1 = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.giris = new System.Windows.Forms.TabPage();
            this.kayit = new System.Windows.Forms.TabPage();
            this.phoneNumMTB = new System.Windows.Forms.MaskedTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.EmailTB2 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.confirmPasswordTB = new System.Windows.Forms.TextBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.kayitBtn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.passwordTB2 = new System.Windows.Forms.TextBox();
            this.firstNameTB = new System.Windows.Forms.TextBox();
            this.Ad = new System.Windows.Forms.Label();
            this.lastNameTB = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.giris.SuspendLayout();
            this.kayit.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(453, 50);
            this.label1.TabIndex = 0;
            this.label1.Text = "Kullanıcı Giriş";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(65, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 25);
            this.label2.TabIndex = 5;
            this.label2.Text = "Email";
            // 
            // passwordTB1
            // 
            this.passwordTB1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.passwordTB1.Location = new System.Drawing.Point(131, 137);
            this.passwordTB1.Name = "passwordTB1";
            this.passwordTB1.Size = new System.Drawing.Size(210, 30);
            this.passwordTB1.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(73, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 25);
            this.label3.TabIndex = 6;
            this.label3.Text = "Şifre";
            // 
            // emailTB1
            // 
            this.emailTB1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.emailTB1.Location = new System.Drawing.Point(131, 68);
            this.emailTB1.Name = "emailTB1";
            this.emailTB1.Size = new System.Drawing.Size(210, 30);
            this.emailTB1.TabIndex = 7;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(347, 148);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(57, 17);
            this.checkBox1.TabIndex = 9;
            this.checkBox1.Text = "Göster";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button1.Location = new System.Drawing.Point(119, 204);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(173, 56);
            this.button1.TabIndex = 10;
            this.button1.Text = "Giriş Yap";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.giris);
            this.tabControl1.Controls.Add(this.kayit);
            this.tabControl1.Location = new System.Drawing.Point(6, 53);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(431, 399);
            this.tabControl1.TabIndex = 11;
            this.tabControl1.TabStop = false;
            // 
            // giris
            // 
            this.giris.Controls.Add(this.checkBox1);
            this.giris.Controls.Add(this.button1);
            this.giris.Controls.Add(this.emailTB1);
            this.giris.Controls.Add(this.label3);
            this.giris.Controls.Add(this.label2);
            this.giris.Controls.Add(this.passwordTB1);
            this.giris.Location = new System.Drawing.Point(4, 22);
            this.giris.Name = "giris";
            this.giris.Padding = new System.Windows.Forms.Padding(3);
            this.giris.Size = new System.Drawing.Size(423, 373);
            this.giris.TabIndex = 0;
            this.giris.Text = "Giriş Yap";
            this.giris.UseVisualStyleBackColor = true;
            // 
            // kayit
            // 
            this.kayit.Controls.Add(this.lastNameTB);
            this.kayit.Controls.Add(this.label10);
            this.kayit.Controls.Add(this.firstNameTB);
            this.kayit.Controls.Add(this.Ad);
            this.kayit.Controls.Add(this.phoneNumMTB);
            this.kayit.Controls.Add(this.label8);
            this.kayit.Controls.Add(this.EmailTB2);
            this.kayit.Controls.Add(this.label7);
            this.kayit.Controls.Add(this.label6);
            this.kayit.Controls.Add(this.confirmPasswordTB);
            this.kayit.Controls.Add(this.checkBox2);
            this.kayit.Controls.Add(this.kayitBtn);
            this.kayit.Controls.Add(this.label4);
            this.kayit.Controls.Add(this.passwordTB2);
            this.kayit.Location = new System.Drawing.Point(4, 22);
            this.kayit.Name = "kayit";
            this.kayit.Padding = new System.Windows.Forms.Padding(3);
            this.kayit.Size = new System.Drawing.Size(423, 373);
            this.kayit.TabIndex = 1;
            this.kayit.Text = "Kayıt Ol";
            this.kayit.UseVisualStyleBackColor = true;
            // 
            // phoneNumMTB
            // 
            this.phoneNumMTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.phoneNumMTB.Location = new System.Drawing.Point(131, 105);
            this.phoneNumMTB.Mask = "(999) 000-0000";
            this.phoneNumMTB.Name = "phoneNumMTB";
            this.phoneNumMTB.Size = new System.Drawing.Size(148, 30);
            this.phoneNumMTB.TabIndex = 22;
            this.phoneNumMTB.Click += new System.EventHandler(this.phoneNumMTB_Click);
            this.phoneNumMTB.Enter += new System.EventHandler(this.phoneNumMTB_Enter);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label8.Location = new System.Drawing.Point(30, 94);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(95, 50);
            this.label8.TabIndex = 21;
            this.label8.Text = "Telefon \r\nNumarası";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // EmailTB2
            // 
            this.EmailTB2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.EmailTB2.Location = new System.Drawing.Point(131, 53);
            this.EmailTB2.Name = "EmailTB2";
            this.EmailTB2.Size = new System.Drawing.Size(210, 30);
            this.EmailTB2.TabIndex = 20;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label7.Location = new System.Drawing.Point(65, 56);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 25);
            this.label7.TabIndex = 19;
            this.label7.Text = "Email";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label6.Location = new System.Drawing.Point(11, 263);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(114, 25);
            this.label6.TabIndex = 17;
            this.label6.Text = "Şifre Tekrar";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // confirmPasswordTB
            // 
            this.confirmPasswordTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.confirmPasswordTB.Location = new System.Drawing.Point(131, 260);
            this.confirmPasswordTB.Name = "confirmPasswordTB";
            this.confirmPasswordTB.Size = new System.Drawing.Size(210, 30);
            this.confirmPasswordTB.TabIndex = 18;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(347, 249);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(57, 17);
            this.checkBox2.TabIndex = 15;
            this.checkBox2.Text = "Göster";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // kayitBtn
            // 
            this.kayitBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.kayitBtn.Location = new System.Drawing.Point(106, 296);
            this.kayitBtn.Name = "kayitBtn";
            this.kayitBtn.Size = new System.Drawing.Size(173, 56);
            this.kayitBtn.TabIndex = 16;
            this.kayitBtn.Text = "Kayıt Ol";
            this.kayitBtn.UseVisualStyleBackColor = true;
            this.kayitBtn.Click += new System.EventHandler(this.kayitBtn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(73, 226);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 25);
            this.label4.TabIndex = 12;
            this.label4.Text = "Şifre";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // passwordTB2
            // 
            this.passwordTB2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.passwordTB2.Location = new System.Drawing.Point(131, 224);
            this.passwordTB2.Name = "passwordTB2";
            this.passwordTB2.Size = new System.Drawing.Size(210, 30);
            this.passwordTB2.TabIndex = 14;
            // 
            // firstNameTB
            // 
            this.firstNameTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.firstNameTB.Location = new System.Drawing.Point(131, 152);
            this.firstNameTB.Name = "firstNameTB";
            this.firstNameTB.Size = new System.Drawing.Size(210, 30);
            this.firstNameTB.TabIndex = 24;
            // 
            // Ad
            // 
            this.Ad.AutoSize = true;
            this.Ad.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Ad.Location = new System.Drawing.Point(73, 155);
            this.Ad.Name = "Ad";
            this.Ad.Size = new System.Drawing.Size(47, 25);
            this.Ad.TabIndex = 23;
            this.Ad.Text = "İsim";
            this.Ad.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lastNameTB
            // 
            this.lastNameTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lastNameTB.Location = new System.Drawing.Point(131, 188);
            this.lastNameTB.Name = "lastNameTB";
            this.lastNameTB.Size = new System.Drawing.Size(210, 30);
            this.lastNameTB.TabIndex = 26;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label10.Location = new System.Drawing.Point(44, 191);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(81, 25);
            this.label10.TabIndex = 25;
            this.label10.Text = "Soyisim";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // KullaniciGirisForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(453, 469);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "KullaniciGirisForm";
            this.Text = "KullaniciGirisForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.KullaniciGirisForm_FormClosed);
            this.tabControl1.ResumeLayout(false);
            this.giris.ResumeLayout(false);
            this.giris.PerformLayout();
            this.kayit.ResumeLayout(false);
            this.kayit.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox passwordTB1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox emailTB1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage giris;
        private System.Windows.Forms.TabPage kayit;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Button kayitBtn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox passwordTB2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox confirmPasswordTB;
        private System.Windows.Forms.MaskedTextBox phoneNumMTB;
        private System.Windows.Forms.TextBox EmailTB2;
        private System.Windows.Forms.TextBox lastNameTB;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox firstNameTB;
        private System.Windows.Forms.Label Ad;
    }
}