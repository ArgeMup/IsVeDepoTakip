using System;

namespace İş_ve_Depo_Takip.Ekranlar
{
    partial class Yeni_İş_Girişi_DosyaEkleri
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
            this.components = new System.ComponentModel.Container();
            this.P_DosyaEkleri_Ayraç_SolSağ = new System.Windows.Forms.SplitContainer();
            this.P_DosyaEkleri_Liste = new System.Windows.Forms.ListBox();
            this.P_DosyaEkleri_YakınlaşmaOranı = new System.Windows.Forms.NumericUpDown();
            this.P_DosyaEkleri_GelenKutusunuAç = new System.Windows.Forms.Button();
            this.P_DosyaEkleri_MasaüstüneKopyala = new System.Windows.Forms.Button();
            this.P_DosyaEkleri_Sil = new System.Windows.Forms.Button();
            this.P_DosyaEkleri_Geri = new System.Windows.Forms.Button();
            this.P_DosyaEkleri_Resim = new System.Windows.Forms.PictureBox();
            this.İpUcu_Genel = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.P_DosyaEkleri_Ayraç_SolSağ)).BeginInit();
            this.P_DosyaEkleri_Ayraç_SolSağ.Panel1.SuspendLayout();
            this.P_DosyaEkleri_Ayraç_SolSağ.Panel2.SuspendLayout();
            this.P_DosyaEkleri_Ayraç_SolSağ.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.P_DosyaEkleri_YakınlaşmaOranı)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.P_DosyaEkleri_Resim)).BeginInit();
            this.SuspendLayout();
            // 
            // P_DosyaEkleri_Ayraç_SolSağ
            // 
            this.P_DosyaEkleri_Ayraç_SolSağ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.P_DosyaEkleri_Ayraç_SolSağ.Location = new System.Drawing.Point(0, 0);
            this.P_DosyaEkleri_Ayraç_SolSağ.Name = "P_DosyaEkleri_Ayraç_SolSağ";
            // 
            // P_DosyaEkleri_Ayraç_SolSağ.Panel1
            // 
            this.P_DosyaEkleri_Ayraç_SolSağ.Panel1.Controls.Add(this.P_DosyaEkleri_Liste);
            this.P_DosyaEkleri_Ayraç_SolSağ.Panel1.Controls.Add(this.P_DosyaEkleri_YakınlaşmaOranı);
            this.P_DosyaEkleri_Ayraç_SolSağ.Panel1.Controls.Add(this.P_DosyaEkleri_GelenKutusunuAç);
            this.P_DosyaEkleri_Ayraç_SolSağ.Panel1.Controls.Add(this.P_DosyaEkleri_MasaüstüneKopyala);
            this.P_DosyaEkleri_Ayraç_SolSağ.Panel1.Controls.Add(this.P_DosyaEkleri_Sil);
            this.P_DosyaEkleri_Ayraç_SolSağ.Panel1.Controls.Add(this.P_DosyaEkleri_Geri);
            // 
            // P_DosyaEkleri_Ayraç_SolSağ.Panel2
            // 
            this.P_DosyaEkleri_Ayraç_SolSağ.Panel2.AutoScroll = true;
            this.P_DosyaEkleri_Ayraç_SolSağ.Panel2.Controls.Add(this.P_DosyaEkleri_Resim);
            this.P_DosyaEkleri_Ayraç_SolSağ.Size = new System.Drawing.Size(907, 438);
            this.P_DosyaEkleri_Ayraç_SolSağ.SplitterDistance = 227;
            this.P_DosyaEkleri_Ayraç_SolSağ.TabIndex = 1;
            // 
            // P_DosyaEkleri_Liste
            // 
            this.P_DosyaEkleri_Liste.Dock = System.Windows.Forms.DockStyle.Fill;
            this.P_DosyaEkleri_Liste.Enabled = false;
            this.P_DosyaEkleri_Liste.FormattingEnabled = true;
            this.P_DosyaEkleri_Liste.ItemHeight = 20;
            this.P_DosyaEkleri_Liste.Location = new System.Drawing.Point(0, 0);
            this.P_DosyaEkleri_Liste.Name = "P_DosyaEkleri_Liste";
            this.P_DosyaEkleri_Liste.Size = new System.Drawing.Size(227, 296);
            this.P_DosyaEkleri_Liste.TabIndex = 0;
            this.P_DosyaEkleri_Liste.SelectedIndexChanged += new System.EventHandler(this.P_DosyaEkleri_Liste_SelectedIndexChanged);
            // 
            // P_DosyaEkleri_YakınlaşmaOranı
            // 
            this.P_DosyaEkleri_YakınlaşmaOranı.DecimalPlaces = 1;
            this.P_DosyaEkleri_YakınlaşmaOranı.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.P_DosyaEkleri_YakınlaşmaOranı.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.P_DosyaEkleri_YakınlaşmaOranı.Location = new System.Drawing.Point(0, 296);
            this.P_DosyaEkleri_YakınlaşmaOranı.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.P_DosyaEkleri_YakınlaşmaOranı.Name = "P_DosyaEkleri_YakınlaşmaOranı";
            this.P_DosyaEkleri_YakınlaşmaOranı.Size = new System.Drawing.Size(227, 26);
            this.P_DosyaEkleri_YakınlaşmaOranı.TabIndex = 0;
            this.P_DosyaEkleri_YakınlaşmaOranı.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.P_DosyaEkleri_YakınlaşmaOranı.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.P_DosyaEkleri_YakınlaşmaOranı.ValueChanged += new System.EventHandler(this.P_DosyaEkleri_YakınlaşmaOranı_ValueChanged);
            // 
            // P_DosyaEkleri_GelenKutusunuAç
            // 
            this.P_DosyaEkleri_GelenKutusunuAç.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.P_DosyaEkleri_GelenKutusunuAç.Enabled = false;
            this.P_DosyaEkleri_GelenKutusunuAç.Image = global::İş_ve_Depo_Takip.Properties.Resources.Eposta;
            this.P_DosyaEkleri_GelenKutusunuAç.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.P_DosyaEkleri_GelenKutusunuAç.Location = new System.Drawing.Point(0, 322);
            this.P_DosyaEkleri_GelenKutusunuAç.Margin = new System.Windows.Forms.Padding(0);
            this.P_DosyaEkleri_GelenKutusunuAç.Name = "P_DosyaEkleri_GelenKutusunuAç";
            this.P_DosyaEkleri_GelenKutusunuAç.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.P_DosyaEkleri_GelenKutusunuAç.Size = new System.Drawing.Size(227, 29);
            this.P_DosyaEkleri_GelenKutusunuAç.TabIndex = 13;
            this.P_DosyaEkleri_GelenKutusunuAç.Text = "Gelen Kutusu";
            this.P_DosyaEkleri_GelenKutusunuAç.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.P_DosyaEkleri_GelenKutusunuAç.UseVisualStyleBackColor = true;
            // 
            // P_DosyaEkleri_MasaüstüneKopyala
            // 
            this.P_DosyaEkleri_MasaüstüneKopyala.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.P_DosyaEkleri_MasaüstüneKopyala.Enabled = false;
            this.P_DosyaEkleri_MasaüstüneKopyala.Image = global::İş_ve_Depo_Takip.Properties.Resources.sol_mavi;
            this.P_DosyaEkleri_MasaüstüneKopyala.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.P_DosyaEkleri_MasaüstüneKopyala.Location = new System.Drawing.Point(0, 351);
            this.P_DosyaEkleri_MasaüstüneKopyala.Margin = new System.Windows.Forms.Padding(0);
            this.P_DosyaEkleri_MasaüstüneKopyala.Name = "P_DosyaEkleri_MasaüstüneKopyala";
            this.P_DosyaEkleri_MasaüstüneKopyala.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.P_DosyaEkleri_MasaüstüneKopyala.Size = new System.Drawing.Size(227, 29);
            this.P_DosyaEkleri_MasaüstüneKopyala.TabIndex = 14;
            this.P_DosyaEkleri_MasaüstüneKopyala.Text = "Masaüstüne Kopyala";
            this.P_DosyaEkleri_MasaüstüneKopyala.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.P_DosyaEkleri_MasaüstüneKopyala.UseVisualStyleBackColor = true;
            this.P_DosyaEkleri_MasaüstüneKopyala.Click += new System.EventHandler(this.P_DosyaEkleri_MasaüstüneKopyala_Click);
            // 
            // P_DosyaEkleri_Sil
            // 
            this.P_DosyaEkleri_Sil.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.P_DosyaEkleri_Sil.Enabled = false;
            this.P_DosyaEkleri_Sil.Image = global::İş_ve_Depo_Takip.Properties.Resources.sil;
            this.P_DosyaEkleri_Sil.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.P_DosyaEkleri_Sil.Location = new System.Drawing.Point(0, 380);
            this.P_DosyaEkleri_Sil.Margin = new System.Windows.Forms.Padding(0);
            this.P_DosyaEkleri_Sil.Name = "P_DosyaEkleri_Sil";
            this.P_DosyaEkleri_Sil.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.P_DosyaEkleri_Sil.Size = new System.Drawing.Size(227, 29);
            this.P_DosyaEkleri_Sil.TabIndex = 12;
            this.P_DosyaEkleri_Sil.Text = "Sil";
            this.P_DosyaEkleri_Sil.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.P_DosyaEkleri_Sil.UseVisualStyleBackColor = true;
            this.P_DosyaEkleri_Sil.Click += new System.EventHandler(this.P_DosyaEkleri_Sil_Click);
            // 
            // P_DosyaEkleri_Geri
            // 
            this.P_DosyaEkleri_Geri.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.P_DosyaEkleri_Geri.Image = global::İş_ve_Depo_Takip.Properties.Resources.sol;
            this.P_DosyaEkleri_Geri.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.P_DosyaEkleri_Geri.Location = new System.Drawing.Point(0, 409);
            this.P_DosyaEkleri_Geri.Margin = new System.Windows.Forms.Padding(2);
            this.P_DosyaEkleri_Geri.Name = "P_DosyaEkleri_Geri";
            this.P_DosyaEkleri_Geri.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.P_DosyaEkleri_Geri.Size = new System.Drawing.Size(227, 29);
            this.P_DosyaEkleri_Geri.TabIndex = 11;
            this.P_DosyaEkleri_Geri.Text = "Geri";
            this.P_DosyaEkleri_Geri.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.İpUcu_Genel.SetToolTip(this.P_DosyaEkleri_Geri, "Esc tuşu");
            this.P_DosyaEkleri_Geri.UseVisualStyleBackColor = true;
            // 
            // P_DosyaEkleri_Resim
            // 
            this.P_DosyaEkleri_Resim.Location = new System.Drawing.Point(0, 0);
            this.P_DosyaEkleri_Resim.Name = "P_DosyaEkleri_Resim";
            this.P_DosyaEkleri_Resim.Size = new System.Drawing.Size(60, 50);
            this.P_DosyaEkleri_Resim.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.P_DosyaEkleri_Resim.TabIndex = 1;
            this.P_DosyaEkleri_Resim.TabStop = false;
            // 
            // İpUcu_Genel
            // 
            this.İpUcu_Genel.AutomaticDelay = 100;
            this.İpUcu_Genel.AutoPopDelay = 10000;
            this.İpUcu_Genel.InitialDelay = 100;
            this.İpUcu_Genel.IsBalloon = true;
            this.İpUcu_Genel.ReshowDelay = 20;
            this.İpUcu_Genel.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.İpUcu_Genel.ToolTipTitle = "Yeni İş Girişi / Düzenleme";
            this.İpUcu_Genel.UseAnimation = false;
            this.İpUcu_Genel.UseFading = false;
            // 
            // Yeni_İş_Girişi_DosyaEkleri
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(907, 438);
            this.Controls.Add(this.P_DosyaEkleri_Ayraç_SolSağ);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Yeni_İş_Girişi_DosyaEkleri";
            this.Text = "Yeni İş Girişi / Düzenleme";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Yeni_İş_Girişi_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Yeni_İş_Girişi_DragEnter);
            this.P_DosyaEkleri_Ayraç_SolSağ.Panel1.ResumeLayout(false);
            this.P_DosyaEkleri_Ayraç_SolSağ.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.P_DosyaEkleri_Ayraç_SolSağ)).EndInit();
            this.P_DosyaEkleri_Ayraç_SolSağ.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.P_DosyaEkleri_YakınlaşmaOranı)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.P_DosyaEkleri_Resim)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip İpUcu_Genel;
        private System.Windows.Forms.SplitContainer P_DosyaEkleri_Ayraç_SolSağ;
        private System.Windows.Forms.Button P_DosyaEkleri_Sil;
        private System.Windows.Forms.Button P_DosyaEkleri_GelenKutusunuAç;
        private System.Windows.Forms.Button P_DosyaEkleri_MasaüstüneKopyala;
        private System.Windows.Forms.NumericUpDown P_DosyaEkleri_YakınlaşmaOranı;
        private System.Windows.Forms.PictureBox P_DosyaEkleri_Resim;
        public System.Windows.Forms.ListBox P_DosyaEkleri_Liste;
        public System.Windows.Forms.Button P_DosyaEkleri_Geri;
    }
}