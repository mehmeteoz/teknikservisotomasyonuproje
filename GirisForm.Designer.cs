namespace TeknikServisOtomasyonuProje
{
    partial class GirisForm
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
            this.KullaniciGirisBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.YetkiliGirisBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // KullaniciGirisBtn
            // 
            this.KullaniciGirisBtn.AutoSize = true;
            this.KullaniciGirisBtn.Location = new System.Drawing.Point(12, 70);
            this.KullaniciGirisBtn.Name = "KullaniciGirisBtn";
            this.KullaniciGirisBtn.Size = new System.Drawing.Size(136, 63);
            this.KullaniciGirisBtn.TabIndex = 0;
            this.KullaniciGirisBtn.Text = "Kullanıcı Giriş";
            this.KullaniciGirisBtn.UseVisualStyleBackColor = true;
            this.KullaniciGirisBtn.Click += new System.EventHandler(this.KullaniciGirisBtn_Click);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(319, 44);
            this.label1.TabIndex = 1;
            this.label1.Text = "Teknik Servis";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // YetkiliGirisBtn
            // 
            this.YetkiliGirisBtn.AutoSize = true;
            this.YetkiliGirisBtn.Location = new System.Drawing.Point(164, 70);
            this.YetkiliGirisBtn.Name = "YetkiliGirisBtn";
            this.YetkiliGirisBtn.Size = new System.Drawing.Size(136, 63);
            this.YetkiliGirisBtn.TabIndex = 2;
            this.YetkiliGirisBtn.Text = "Yetkili Giriş";
            this.YetkiliGirisBtn.UseVisualStyleBackColor = true;
            // 
            // GirisForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 153);
            this.Controls.Add(this.YetkiliGirisBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.KullaniciGirisBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "GirisForm";
            this.Text = "GirisForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button KullaniciGirisBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button YetkiliGirisBtn;
    }
}