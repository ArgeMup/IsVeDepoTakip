namespace İş_ve_Depo_Takip.Ekranlar
{
    partial class Ayarlar_Eposta
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
            this.Kaydet = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Mesaj_İçerik = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.Mesaj_Konu = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.Sunucu_SSL = new System.Windows.Forms.CheckBox();
            this.Sunucu_ErişimNoktası = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.Sunucu_Adres = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Gönderici_Ad = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Gönderici_Şifre = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Gönderici_Adres = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.İpUcu = new System.Windows.Forms.ToolTip(this.components);
            this.GöndermeyiDene = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // Kaydet
            // 
            this.Kaydet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Kaydet.Enabled = false;
            this.Kaydet.Image = global::İş_ve_Depo_Takip.Properties.Resources.sag;
            this.Kaydet.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Kaydet.Location = new System.Drawing.Point(24, 528);
            this.Kaydet.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Kaydet.Name = "Kaydet";
            this.Kaydet.Size = new System.Drawing.Size(681, 55);
            this.Kaydet.TabIndex = 9;
            this.Kaydet.Text = "Kaydet";
            this.Kaydet.UseVisualStyleBackColor = true;
            this.Kaydet.Click += new System.EventHandler(this.Kaydet_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.Mesaj_İçerik);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.Mesaj_Konu);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Location = new System.Drawing.Point(12, 241);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox2.Size = new System.Drawing.Size(706, 279);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Mesaj";
            // 
            // Mesaj_İçerik
            // 
            this.Mesaj_İçerik.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Mesaj_İçerik.Location = new System.Drawing.Point(12, 115);
            this.Mesaj_İçerik.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Mesaj_İçerik.Multiline = true;
            this.Mesaj_İçerik.Name = "Mesaj_İçerik";
            this.Mesaj_İçerik.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.Mesaj_İçerik.Size = new System.Drawing.Size(681, 152);
            this.Mesaj_İçerik.TabIndex = 8;
            this.Mesaj_İçerik.Text = "<h1>Sayın %Müşteri%</h1>\r\n<br>\r\nGüncel işlere ait detaylar ekte sunulmuştur.\r\n<br" +
    "><br>\r\nİyi çalışmalar dileriz.";
            this.İpUcu.SetToolTip(this.Mesaj_İçerik, "HTML olarak mesaj içeriği\r\n\r\n%Müşteri% -> Müşteri adı");
            this.Mesaj_İçerik.TextChanged += new System.EventHandler(this.Ayar_Değişti);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 87);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(132, 25);
            this.label9.TabIndex = 38;
            this.label9.Text = "İçerik (HTML)";
            // 
            // Mesaj_Konu
            // 
            this.Mesaj_Konu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Mesaj_Konu.Location = new System.Drawing.Point(12, 54);
            this.Mesaj_Konu.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Mesaj_Konu.Name = "Mesaj_Konu";
            this.Mesaj_Konu.Size = new System.Drawing.Size(681, 30);
            this.Mesaj_Konu.TabIndex = 7;
            this.Mesaj_Konu.Text = "Güncel çalışmalar hk.";
            this.Mesaj_Konu.TextChanged += new System.EventHandler(this.Ayar_Değişti);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 26);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 25);
            this.label8.TabIndex = 36;
            this.label8.Text = "Konu";
            // 
            // Sunucu_SSL
            // 
            this.Sunucu_SSL.AutoSize = true;
            this.Sunucu_SSL.Checked = true;
            this.Sunucu_SSL.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Sunucu_SSL.Location = new System.Drawing.Point(12, 167);
            this.Sunucu_SSL.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Sunucu_SSL.Name = "Sunucu_SSL";
            this.Sunucu_SSL.Size = new System.Drawing.Size(73, 29);
            this.Sunucu_SSL.TabIndex = 2;
            this.Sunucu_SSL.Text = "SSL";
            this.Sunucu_SSL.UseVisualStyleBackColor = true;
            // 
            // Sunucu_ErişimNoktası
            // 
            this.Sunucu_ErişimNoktası.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Sunucu_ErişimNoktası.Location = new System.Drawing.Point(12, 115);
            this.Sunucu_ErişimNoktası.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Sunucu_ErişimNoktası.Name = "Sunucu_ErişimNoktası";
            this.Sunucu_ErişimNoktası.Size = new System.Drawing.Size(321, 30);
            this.Sunucu_ErişimNoktası.TabIndex = 1;
            this.Sunucu_ErişimNoktası.Text = "587";
            this.İpUcu.SetToolTip(this.Sunucu_ErişimNoktası, "SSL için 587");
            this.Sunucu_ErişimNoktası.TextChanged += new System.EventHandler(this.Ayar_Değişti);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 87);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(135, 25);
            this.label7.TabIndex = 33;
            this.label7.Text = "Erişim Noktası";
            // 
            // Sunucu_Adres
            // 
            this.Sunucu_Adres.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Sunucu_Adres.Location = new System.Drawing.Point(12, 54);
            this.Sunucu_Adres.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Sunucu_Adres.Name = "Sunucu_Adres";
            this.Sunucu_Adres.Size = new System.Drawing.Size(321, 30);
            this.Sunucu_Adres.TabIndex = 0;
            this.Sunucu_Adres.Text = "smtp.firmaadi.com";
            this.İpUcu.SetToolTip(this.Sunucu_Adres, "smtp.firmaadi.com");
            this.Sunucu_Adres.TextChanged += new System.EventHandler(this.Ayar_Değişti);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 26);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 25);
            this.label6.TabIndex = 31;
            this.label6.Text = "Adresi";
            // 
            // Gönderici_Ad
            // 
            this.Gönderici_Ad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Gönderici_Ad.Location = new System.Drawing.Point(12, 54);
            this.Gönderici_Ad.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Gönderici_Ad.Name = "Gönderici_Ad";
            this.Gönderici_Ad.Size = new System.Drawing.Size(325, 30);
            this.Gönderici_Ad.TabIndex = 4;
            this.Gönderici_Ad.Text = "Firma Adı";
            this.İpUcu.SetToolTip(this.Gönderici_Ad, "Firma Adı");
            this.Gönderici_Ad.TextChanged += new System.EventHandler(this.Ayar_Değişti);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 26);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 25);
            this.label5.TabIndex = 29;
            this.label5.Text = "Adı";
            // 
            // Gönderici_Şifre
            // 
            this.Gönderici_Şifre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Gönderici_Şifre.Location = new System.Drawing.Point(12, 176);
            this.Gönderici_Şifre.Name = "Gönderici_Şifre";
            this.Gönderici_Şifre.Size = new System.Drawing.Size(325, 30);
            this.Gönderici_Şifre.TabIndex = 6;
            this.Gönderici_Şifre.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Gönderici_Şifre.UseSystemPasswordChar = true;
            this.Gönderici_Şifre.TextChanged += new System.EventHandler(this.Ayar_Değişti);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 148);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 25);
            this.label4.TabIndex = 27;
            this.label4.Text = "Şifresi";
            // 
            // Gönderici_Adres
            // 
            this.Gönderici_Adres.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Gönderici_Adres.Location = new System.Drawing.Point(12, 115);
            this.Gönderici_Adres.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Gönderici_Adres.Name = "Gönderici_Adres";
            this.Gönderici_Adres.Size = new System.Drawing.Size(325, 30);
            this.Gönderici_Adres.TabIndex = 5;
            this.Gönderici_Adres.Text = "bilgi@firmaadi.com";
            this.İpUcu.SetToolTip(this.Gönderici_Adres, "bilgi@firmaadi.com");
            this.Gönderici_Adres.TextChanged += new System.EventHandler(this.Ayar_Değişti);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 87);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 25);
            this.label3.TabIndex = 25;
            this.label3.Text = "Adresi";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.GöndermeyiDene);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.Sunucu_Adres);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.Sunucu_SSL);
            this.groupBox1.Controls.Add(this.Sunucu_ErişimNoktası);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(350, 223);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Smtp Sunucu";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.Gönderici_Ad);
            this.groupBox3.Controls.Add(this.Gönderici_Adres);
            this.groupBox3.Controls.Add(this.Gönderici_Şifre);
            this.groupBox3.Location = new System.Drawing.Point(368, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(350, 223);
            this.groupBox3.TabIndex = 26;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Gönderici";
            // 
            // İpUcu
            // 
            this.İpUcu.AutomaticDelay = 0;
            this.İpUcu.IsBalloon = true;
            this.İpUcu.ShowAlways = true;
            this.İpUcu.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.İpUcu.UseAnimation = false;
            this.İpUcu.UseFading = false;
            // 
            // GöndermeyiDene
            // 
            this.GöndermeyiDene.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.GöndermeyiDene.Location = new System.Drawing.Point(93, 153);
            this.GöndermeyiDene.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GöndermeyiDene.Name = "GöndermeyiDene";
            this.GöndermeyiDene.Size = new System.Drawing.Size(240, 55);
            this.GöndermeyiDene.TabIndex = 3;
            this.GöndermeyiDene.Text = "Göndermeyi Dene";
            this.GöndermeyiDene.UseVisualStyleBackColor = true;
            this.GöndermeyiDene.Click += new System.EventHandler(this.GöndermeyiDene_Click);
            // 
            // Ayarlar_Eposta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 594);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.Kaydet);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ayarlar_Eposta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ayarlar Eposta";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Ayarlar_Eposta_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ayarlar_Eposta_FormClosed);
            this.Load += new System.EventHandler(this.Ayarlar_Eposta_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Kaydet;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox Gönderici_Adres;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MaskedTextBox Gönderici_Şifre;
        private System.Windows.Forms.TextBox Gönderici_Ad;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox Sunucu_ErişimNoktası;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox Sunucu_Adres;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox Sunucu_SSL;
        private System.Windows.Forms.TextBox Mesaj_İçerik;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox Mesaj_Konu;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ToolTip İpUcu;
        private System.Windows.Forms.Button GöndermeyiDene;
    }
}