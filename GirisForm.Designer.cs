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
            this.exit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // KullaniciGirisBtn
            // 
            this.KullaniciGirisBtn.AutoSize = true;
            this.KullaniciGirisBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.KullaniciGirisBtn.FlatAppearance.BorderSize = 0;
            this.KullaniciGirisBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.KullaniciGirisBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.KullaniciGirisBtn.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.KullaniciGirisBtn.Location = new System.Drawing.Point(170, 275);
            this.KullaniciGirisBtn.Name = "KullaniciGirisBtn";
            this.KullaniciGirisBtn.Size = new System.Drawing.Size(253, 152);
            this.KullaniciGirisBtn.TabIndex = 0;
            this.KullaniciGirisBtn.Text = "Kullanıcı Giriş";
            this.KullaniciGirisBtn.UseVisualStyleBackColor = false;
            this.KullaniciGirisBtn.Click += new System.EventHandler(this.KullaniciGirisBtn_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1280, 95);
            this.label1.TabIndex = 1;
            this.label1.Text = "Teknik Servis";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // YetkiliGirisBtn
            // 
            this.YetkiliGirisBtn.AutoSize = true;
            this.YetkiliGirisBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.YetkiliGirisBtn.FlatAppearance.BorderSize = 0;
            this.YetkiliGirisBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.YetkiliGirisBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.YetkiliGirisBtn.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.YetkiliGirisBtn.Location = new System.Drawing.Point(883, 275);
            this.YetkiliGirisBtn.Name = "YetkiliGirisBtn";
            this.YetkiliGirisBtn.Size = new System.Drawing.Size(253, 152);
            this.YetkiliGirisBtn.TabIndex = 2;
            this.YetkiliGirisBtn.Text = "Yetkili Giriş";
            this.YetkiliGirisBtn.UseVisualStyleBackColor = false;
            // 
            // exit
            // 
            this.exit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.exit.FlatAppearance.BorderSize = 0;
            this.exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exit.ForeColor = System.Drawing.SystemColors.ControlText;
            this.exit.Image = global::TeknikServisOtomasyonuProje.Properties.Resources.exit;
            this.exit.Location = new System.Drawing.Point(1204, 12);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(64, 64);
            this.exit.TabIndex = 3;
            this.exit.UseVisualStyleBackColor = false;
            this.exit.Click += new System.EventHandler(this.exit_Click);
            // 
            // GirisForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.exit);
            this.Controls.Add(this.YetkiliGirisBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.KullaniciGirisBtn);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "GirisForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GirisForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button KullaniciGirisBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button YetkiliGirisBtn;
        private System.Windows.Forms.Button exit;
    }
}