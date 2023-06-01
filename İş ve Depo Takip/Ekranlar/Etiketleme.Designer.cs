namespace İş_ve_Depo_Takip.Ekranlar
{
    partial class Etiketleme
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
            this.İpUcu = new System.Windows.Forms.ToolTip(this.components);
            this.YeniİşGirişi_Barkod_İçeriği = new System.Windows.Forms.TextBox();
            this.SağTuşMenü_Barkodİçeriği = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SağTuşMenü_Barkodİçeriği_MüşteriAdı = new System.Windows.Forms.ToolStripMenuItem();
            this.SağTuşMenü_Barkodİçeriği_HastaAdı = new System.Windows.Forms.ToolStripMenuItem();
            this.SağTuşMenü_Barkodİçeriği_SeriNo = new System.Windows.Forms.ToolStripMenuItem();
            this.SağTuşMenü_Barkodİçeriği_BilgisayarınYerelAdresi = new System.Windows.Forms.ToolStripMenuItem();
            this.SağTuşMenü_Barkodİçeriği_HttpSunucuErişimNoktası = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.YeniİşGirişi_Barkod_İçeriğiKaydet = new System.Windows.Forms.Button();
            this.YeniİşGirişi_EtiketAyarları = new System.Windows.Forms.Button();
            this.YeniİşGirişi_BarkodAyarları = new System.Windows.Forms.Button();
            this.SağTuşMenü_Barkodİçeriği.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // İpUcu
            // 
            this.İpUcu.AutomaticDelay = 100;
            this.İpUcu.AutoPopDelay = 10000;
            this.İpUcu.InitialDelay = 100;
            this.İpUcu.ReshowDelay = 20;
            this.İpUcu.UseAnimation = false;
            this.İpUcu.UseFading = false;
            // 
            // YeniİşGirişi_Barkod_İçeriği
            // 
            this.YeniİşGirişi_Barkod_İçeriği.ContextMenuStrip = this.SağTuşMenü_Barkodİçeriği;
            this.YeniİşGirişi_Barkod_İçeriği.Dock = System.Windows.Forms.DockStyle.Fill;
            this.YeniİşGirişi_Barkod_İçeriği.Location = new System.Drawing.Point(10, 33);
            this.YeniİşGirişi_Barkod_İçeriği.Multiline = true;
            this.YeniİşGirişi_Barkod_İçeriği.Name = "YeniİşGirişi_Barkod_İçeriği";
            this.YeniİşGirişi_Barkod_İçeriği.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.YeniİşGirişi_Barkod_İçeriği.Size = new System.Drawing.Size(442, 115);
            this.YeniİşGirişi_Barkod_İçeriği.TabIndex = 3;
            this.İpUcu.SetToolTip(this.YeniİşGirişi_Barkod_İçeriği, "Barkod oluşturulurken kullanılacak detaylar\r\n\r\nKullanılmayacak ise KAPALI yazınız" +
        ".");
            this.YeniİşGirişi_Barkod_İçeriği.TextChanged += new System.EventHandler(this.YeniİşGirişi_Barkod_İçeriği_TextChanged);
            // 
            // SağTuşMenü_Barkodİçeriği
            // 
            this.SağTuşMenü_Barkodİçeriği.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.SağTuşMenü_Barkodİçeriği.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SağTuşMenü_Barkodİçeriği_MüşteriAdı,
            this.SağTuşMenü_Barkodİçeriği_HastaAdı,
            this.SağTuşMenü_Barkodİçeriği_SeriNo,
            this.SağTuşMenü_Barkodİçeriği_BilgisayarınYerelAdresi,
            this.SağTuşMenü_Barkodİçeriği_HttpSunucuErişimNoktası});
            this.SağTuşMenü_Barkodİçeriği.Name = "SağTuşMenü_Barkodİçeriği";
            this.SağTuşMenü_Barkodİçeriği.ShowImageMargin = false;
            this.SağTuşMenü_Barkodİçeriği.Size = new System.Drawing.Size(233, 124);
            // 
            // SağTuşMenü_Barkodİçeriği_MüşteriAdı
            // 
            this.SağTuşMenü_Barkodİçeriği_MüşteriAdı.Name = "SağTuşMenü_Barkodİçeriği_MüşteriAdı";
            this.SağTuşMenü_Barkodİçeriği_MüşteriAdı.Size = new System.Drawing.Size(232, 24);
            this.SağTuşMenü_Barkodİçeriği_MüşteriAdı.Tag = "%Müşteri%";
            this.SağTuşMenü_Barkodİçeriği_MüşteriAdı.Text = "Müşteri";
            this.SağTuşMenü_Barkodİçeriği_MüşteriAdı.Click += new System.EventHandler(this.SağTuşMenü_Barkodİçeriği_ÖnTanımlıAlan_Click);
            // 
            // SağTuşMenü_Barkodİçeriği_HastaAdı
            // 
            this.SağTuşMenü_Barkodİçeriği_HastaAdı.Name = "SağTuşMenü_Barkodİçeriği_HastaAdı";
            this.SağTuşMenü_Barkodİçeriği_HastaAdı.Size = new System.Drawing.Size(232, 24);
            this.SağTuşMenü_Barkodİçeriği_HastaAdı.Tag = "%Hasta%";
            this.SağTuşMenü_Barkodİçeriği_HastaAdı.Text = "Hasta";
            this.SağTuşMenü_Barkodİçeriği_HastaAdı.Click += new System.EventHandler(this.SağTuşMenü_Barkodİçeriği_ÖnTanımlıAlan_Click);
            // 
            // SağTuşMenü_Barkodİçeriği_SeriNo
            // 
            this.SağTuşMenü_Barkodİçeriği_SeriNo.Name = "SağTuşMenü_Barkodİçeriği_SeriNo";
            this.SağTuşMenü_Barkodİçeriği_SeriNo.Size = new System.Drawing.Size(232, 24);
            this.SağTuşMenü_Barkodİçeriği_SeriNo.Tag = "%SeriNo%";
            this.SağTuşMenü_Barkodİçeriği_SeriNo.Text = "Seri No";
            this.SağTuşMenü_Barkodİçeriği_SeriNo.Click += new System.EventHandler(this.SağTuşMenü_Barkodİçeriği_ÖnTanımlıAlan_Click);
            // 
            // SağTuşMenü_Barkodİçeriği_BilgisayarınYerelAdresi
            // 
            this.SağTuşMenü_Barkodİçeriği_BilgisayarınYerelAdresi.Name = "SağTuşMenü_Barkodİçeriği_BilgisayarınYerelAdresi";
            this.SağTuşMenü_Barkodİçeriği_BilgisayarınYerelAdresi.Size = new System.Drawing.Size(232, 24);
            this.SağTuşMenü_Barkodİçeriği_BilgisayarınYerelAdresi.Tag = "%BilgisayarınYerelAdresi%";
            this.SağTuşMenü_Barkodİçeriği_BilgisayarınYerelAdresi.Text = "Bilgisayarın Yerel Adresi";
            this.SağTuşMenü_Barkodİçeriği_BilgisayarınYerelAdresi.Click += new System.EventHandler(this.SağTuşMenü_Barkodİçeriği_ÖnTanımlıAlan_Click);
            // 
            // SağTuşMenü_Barkodİçeriği_HttpSunucuErişimNoktası
            // 
            this.SağTuşMenü_Barkodİçeriği_HttpSunucuErişimNoktası.Name = "SağTuşMenü_Barkodİçeriği_HttpSunucuErişimNoktası";
            this.SağTuşMenü_Barkodİçeriği_HttpSunucuErişimNoktası.Size = new System.Drawing.Size(232, 24);
            this.SağTuşMenü_Barkodİçeriği_HttpSunucuErişimNoktası.Tag = "%HttpSunucuErişimNoktası%";
            this.SağTuşMenü_Barkodİçeriği_HttpSunucuErişimNoktası.Text = "Http Sunucu Erişim Noktası";
            this.SağTuşMenü_Barkodİçeriği_HttpSunucuErişimNoktası.Click += new System.EventHandler(this.SağTuşMenü_Barkodİçeriği_ÖnTanımlıAlan_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.YeniİşGirişi_BarkodAyarları);
            this.groupBox1.Controls.Add(this.YeniİşGirişi_EtiketAyarları);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(10);
            this.groupBox1.Size = new System.Drawing.Size(482, 353);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Yeni İş Girişi";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.YeniİşGirişi_Barkod_İçeriği);
            this.groupBox2.Controls.Add(this.YeniİşGirişi_Barkod_İçeriğiKaydet);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(10, 33);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(10);
            this.groupBox2.Size = new System.Drawing.Size(462, 202);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Barkod İçeriği Ayarları";
            // 
            // YeniİşGirişi_Barkod_İçeriğiKaydet
            // 
            this.YeniİşGirişi_Barkod_İçeriğiKaydet.AutoSize = true;
            this.YeniİşGirişi_Barkod_İçeriğiKaydet.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.YeniİşGirişi_Barkod_İçeriğiKaydet.Location = new System.Drawing.Point(10, 148);
            this.YeniİşGirişi_Barkod_İçeriğiKaydet.Name = "YeniİşGirişi_Barkod_İçeriğiKaydet";
            this.YeniİşGirişi_Barkod_İçeriğiKaydet.Size = new System.Drawing.Size(442, 44);
            this.YeniİşGirişi_Barkod_İçeriğiKaydet.TabIndex = 3;
            this.YeniİşGirişi_Barkod_İçeriğiKaydet.Text = "Kaydet";
            this.YeniİşGirişi_Barkod_İçeriğiKaydet.UseVisualStyleBackColor = true;
            this.YeniİşGirişi_Barkod_İçeriğiKaydet.Click += new System.EventHandler(this.YeniİşGirişi_Barkod_İçeriğiKaydet_Click);
            // 
            // YeniİşGirişi_EtiketAyarları
            // 
            this.YeniİşGirişi_EtiketAyarları.AutoSize = true;
            this.YeniİşGirişi_EtiketAyarları.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.YeniİşGirişi_EtiketAyarları.Location = new System.Drawing.Point(10, 289);
            this.YeniİşGirişi_EtiketAyarları.Name = "YeniİşGirişi_EtiketAyarları";
            this.YeniİşGirişi_EtiketAyarları.Size = new System.Drawing.Size(462, 54);
            this.YeniİşGirişi_EtiketAyarları.TabIndex = 2;
            this.YeniİşGirişi_EtiketAyarları.Text = "Etiket Görseli Ayarları";
            this.YeniİşGirişi_EtiketAyarları.UseVisualStyleBackColor = true;
            this.YeniİşGirişi_EtiketAyarları.Click += new System.EventHandler(this.YeniİşGirişi_EtiketAyarları_Click);
            // 
            // YeniİşGirişi_BarkodAyarları
            // 
            this.YeniİşGirişi_BarkodAyarları.AutoSize = true;
            this.YeniİşGirişi_BarkodAyarları.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.YeniİşGirişi_BarkodAyarları.Location = new System.Drawing.Point(10, 235);
            this.YeniİşGirişi_BarkodAyarları.Name = "YeniİşGirişi_BarkodAyarları";
            this.YeniİşGirişi_BarkodAyarları.Size = new System.Drawing.Size(462, 54);
            this.YeniİşGirişi_BarkodAyarları.TabIndex = 1;
            this.YeniİşGirişi_BarkodAyarları.Text = "Barkod Türü Ayarları";
            this.YeniİşGirişi_BarkodAyarları.UseVisualStyleBackColor = true;
            this.YeniİşGirişi_BarkodAyarları.Click += new System.EventHandler(this.YeniİşGirişi_BarkodAyarları_Click);
            // 
            // Etiketleme
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 353);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.MaximizeBox = false;
            this.Name = "Etiketleme";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Etiketleme";
            this.SağTuşMenü_Barkodİçeriği.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip İpUcu;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button YeniİşGirişi_EtiketAyarları;
        private System.Windows.Forms.Button YeniİşGirişi_BarkodAyarları;
        private System.Windows.Forms.TextBox YeniİşGirişi_Barkod_İçeriği;
        private System.Windows.Forms.ContextMenuStrip SağTuşMenü_Barkodİçeriği;
        private System.Windows.Forms.ToolStripMenuItem SağTuşMenü_Barkodİçeriği_MüşteriAdı;
        private System.Windows.Forms.ToolStripMenuItem SağTuşMenü_Barkodİçeriği_HastaAdı;
        private System.Windows.Forms.ToolStripMenuItem SağTuşMenü_Barkodİçeriği_SeriNo;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button YeniİşGirişi_Barkod_İçeriğiKaydet;
        private System.Windows.Forms.ToolStripMenuItem SağTuşMenü_Barkodİçeriği_BilgisayarınYerelAdresi;
        private System.Windows.Forms.ToolStripMenuItem SağTuşMenü_Barkodİçeriği_HttpSunucuErişimNoktası;
    }
}