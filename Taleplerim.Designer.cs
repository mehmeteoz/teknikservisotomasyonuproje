namespace TeknikServisOtomasyonuProje
{
    partial class Taleplerim
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Taleplerim));
            this.requestsFLPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // requestsFLPanel
            // 
            this.requestsFLPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.requestsFLPanel.Location = new System.Drawing.Point(0, 0);
            this.requestsFLPanel.Name = "requestsFLPanel";
            this.requestsFLPanel.Size = new System.Drawing.Size(758, 537);
            this.requestsFLPanel.TabIndex = 0;
            this.requestsFLPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.requestsFLPanel_Paint);
            // 
            // Taleplerim
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.ClientSize = new System.Drawing.Size(758, 537);
            this.Controls.Add(this.requestsFLPanel);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Taleplerim";
            this.Text = "Taleplerim";
            this.Load += new System.EventHandler(this.Taleplerim_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel requestsFLPanel;
    }
}