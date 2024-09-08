using System;

namespace İş_ve_Depo_Takip.Ekranlar
{
    partial class Yeni_İş_Girişi_Sürümler
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
            this.Sürümler = new System.Windows.Forms.RichTextBox();
            this.Çıkış_Geri = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Sürümler
            // 
            this.Sürümler.BackColor = System.Drawing.SystemColors.Window;
            this.Sürümler.DetectUrls = false;
            this.Sürümler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Sürümler.Font = new System.Drawing.Font("Consolas", 10F);
            this.Sürümler.Location = new System.Drawing.Point(0, 0);
            this.Sürümler.Name = "Sürümler";
            this.Sürümler.ReadOnly = true;
            this.Sürümler.Size = new System.Drawing.Size(818, 422);
            this.Sürümler.TabIndex = 0;
            this.Sürümler.Text = "";
            this.Sürümler.WordWrap = false;
            // 
            // Çıkış_Geri
            // 
            this.Çıkış_Geri.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Çıkış_Geri.Image = global::İş_ve_Depo_Takip.Properties.Resources.sol;
            this.Çıkış_Geri.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Çıkış_Geri.Location = new System.Drawing.Point(0, 422);
            this.Çıkış_Geri.Margin = new System.Windows.Forms.Padding(2);
            this.Çıkış_Geri.Name = "Çıkış_Geri";
            this.Çıkış_Geri.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Çıkış_Geri.Size = new System.Drawing.Size(818, 29);
            this.Çıkış_Geri.TabIndex = 13;
            this.Çıkış_Geri.Text = "Geri";
            this.Çıkış_Geri.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Çıkış_Geri.UseVisualStyleBackColor = true;
            this.Çıkış_Geri.Click += new System.EventHandler(this.Çıkış_Geri_Click);
            // 
            // Yeni_İş_Girişi_Sürümler
            // 
            this.ClientSize = new System.Drawing.Size(818, 451);
            this.Controls.Add(this.Sürümler);
            this.Controls.Add(this.Çıkış_Geri);
            this.Name = "Yeni_İş_Girişi_Sürümler";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Shown += new System.EventHandler(this.Yeni_İş_Girişi_Sürümler_Shown);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip İpUcu_Genel;
        private System.Windows.Forms.SplitContainer Ayraç_SolSağ;
        private System.Windows.Forms.Button Geri;
        private ArgeMup.HazirKod.Ekranlar.ListeKutusu Açıklamalar;
        private System.Windows.Forms.TextBox SeçiliOlanAçıklama;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button SağaAktar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox Çıktı;
        public System.Windows.Forms.Button YazdırVeKaydet;
        private System.Windows.Forms.Button ListeyeEkle;
        private System.Windows.Forms.RichTextBox Sürümler;
        public System.Windows.Forms.Button Çıkış_Geri;
    }
}