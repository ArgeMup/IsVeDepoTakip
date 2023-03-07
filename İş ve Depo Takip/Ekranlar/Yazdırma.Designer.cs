namespace İş_ve_Depo_Takip.Ekranlar
{
    partial class Yazdırma
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Yazdırma));
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.Önizleme = new System.Windows.Forms.PrintPreviewControl();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.Yazcılar = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.KenarBoşluğu = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.KarakterBüyüklüğü_Başlıklar = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.KarakterKümeleri = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.KarakterBüyüklüğü_Diğerleri = new System.Windows.Forms.NumericUpDown();
            this.KarakterBüyüklüğü_Müşteri = new System.Windows.Forms.NumericUpDown();
            this.Kaydet = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.FirmaLogo_Yükseklik = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.FirmaLogo_Genişlik = new System.Windows.Forms.NumericUpDown();
            this.İpucu = new System.Windows.Forms.ToolTip(this.components);
            this.DosyayaYazdır = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.KenarBoşluğu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KarakterBüyüklüğü_Başlıklar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KarakterBüyüklüğü_Diğerleri)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KarakterBüyüklüğü_Müşteri)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FirmaLogo_Yükseklik)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FirmaLogo_Genişlik)).BeginInit();
            this.SuspendLayout();
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // Önizleme
            // 
            this.Önizleme.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Önizleme.AutoZoom = false;
            this.Önizleme.Location = new System.Drawing.Point(359, 9);
            this.Önizleme.Margin = new System.Windows.Forms.Padding(4);
            this.Önizleme.Name = "Önizleme";
            this.Önizleme.Size = new System.Drawing.Size(595, 406);
            this.Önizleme.TabIndex = 0;
            this.Önizleme.Zoom = 1D;
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // Yazcılar
            // 
            this.Yazcılar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Yazcılar.FormattingEnabled = true;
            this.Yazcılar.Location = new System.Drawing.Point(17, 33);
            this.Yazcılar.Margin = new System.Windows.Forms.Padding(4);
            this.Yazcılar.Name = "Yazcılar";
            this.Yazcılar.Size = new System.Drawing.Size(334, 28);
            this.Yazcılar.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Yazıcı";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 65);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Karakter Kümesi";
            // 
            // KenarBoşluğu
            // 
            this.KenarBoşluğu.Location = new System.Drawing.Point(17, 145);
            this.KenarBoşluğu.Margin = new System.Windows.Forms.Padding(4);
            this.KenarBoşluğu.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.KenarBoşluğu.Name = "KenarBoşluğu";
            this.KenarBoşluğu.Size = new System.Drawing.Size(334, 26);
            this.KenarBoşluğu.TabIndex = 7;
            this.KenarBoşluğu.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 121);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(233, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Sayfanın Kenar Boşluğu (mm)";
            // 
            // KarakterBüyüklüğü_Başlıklar
            // 
            this.KarakterBüyüklüğü_Başlıklar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.KarakterBüyüklüğü_Başlıklar.Location = new System.Drawing.Point(11, 100);
            this.KarakterBüyüklüğü_Başlıklar.Margin = new System.Windows.Forms.Padding(4);
            this.KarakterBüyüklüğü_Başlıklar.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.KarakterBüyüklüğü_Başlıklar.Name = "KarakterBüyüklüğü_Başlıklar";
            this.KarakterBüyüklüğü_Başlıklar.Size = new System.Drawing.Size(165, 26);
            this.KarakterBüyüklüğü_Başlıklar.TabIndex = 9;
            this.KarakterBüyüklüğü_Başlıklar.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 22);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "Müşteri";
            // 
            // KarakterKümeleri
            // 
            this.KarakterKümeleri.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.KarakterKümeleri.FormattingEnabled = true;
            this.KarakterKümeleri.Location = new System.Drawing.Point(17, 89);
            this.KarakterKümeleri.Margin = new System.Windows.Forms.Padding(4);
            this.KarakterKümeleri.Name = "KarakterKümeleri";
            this.KarakterKümeleri.Size = new System.Drawing.Size(334, 28);
            this.KarakterKümeleri.TabIndex = 14;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 76);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(74, 20);
            this.label10.TabIndex = 15;
            this.label10.Text = "Başlıklar";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 130);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(73, 20);
            this.label11.TabIndex = 16;
            this.label11.Text = "Diğerleri";
            // 
            // KarakterBüyüklüğü_Diğerleri
            // 
            this.KarakterBüyüklüğü_Diğerleri.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.KarakterBüyüklüğü_Diğerleri.Location = new System.Drawing.Point(11, 154);
            this.KarakterBüyüklüğü_Diğerleri.Margin = new System.Windows.Forms.Padding(4);
            this.KarakterBüyüklüğü_Diğerleri.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.KarakterBüyüklüğü_Diğerleri.Name = "KarakterBüyüklüğü_Diğerleri";
            this.KarakterBüyüklüğü_Diğerleri.Size = new System.Drawing.Size(165, 26);
            this.KarakterBüyüklüğü_Diğerleri.TabIndex = 17;
            this.KarakterBüyüklüğü_Diğerleri.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // KarakterBüyüklüğü_Müşteri
            // 
            this.KarakterBüyüklüğü_Müşteri.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.KarakterBüyüklüğü_Müşteri.Location = new System.Drawing.Point(11, 46);
            this.KarakterBüyüklüğü_Müşteri.Margin = new System.Windows.Forms.Padding(4);
            this.KarakterBüyüklüğü_Müşteri.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.KarakterBüyüklüğü_Müşteri.Name = "KarakterBüyüklüğü_Müşteri";
            this.KarakterBüyüklüğü_Müşteri.Size = new System.Drawing.Size(165, 26);
            this.KarakterBüyüklüğü_Müşteri.TabIndex = 18;
            this.KarakterBüyüklüğü_Müşteri.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Kaydet
            // 
            this.Kaydet.Enabled = false;
            this.Kaydet.Location = new System.Drawing.Point(17, 375);
            this.Kaydet.Name = "Kaydet";
            this.Kaydet.Size = new System.Drawing.Size(334, 44);
            this.Kaydet.TabIndex = 19;
            this.Kaydet.Text = "Kaydet";
            this.Kaydet.UseVisualStyleBackColor = true;
            this.Kaydet.Click += new System.EventHandler(this.Kaydet_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.KarakterBüyüklüğü_Başlıklar);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.KarakterBüyüklüğü_Müşteri);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.KarakterBüyüklüğü_Diğerleri);
            this.groupBox1.Location = new System.Drawing.Point(17, 178);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(183, 191);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Karakter Büyüklüğü";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.FirmaLogo_Yükseklik);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.FirmaLogo_Genişlik);
            this.groupBox2.Location = new System.Drawing.Point(206, 178);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(145, 191);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Firma Logosu";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 22);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 20);
            this.label5.TabIndex = 8;
            this.label5.Text = "Genişlik (mm)";
            // 
            // FirmaLogo_Yükseklik
            // 
            this.FirmaLogo_Yükseklik.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FirmaLogo_Yükseklik.Location = new System.Drawing.Point(11, 100);
            this.FirmaLogo_Yükseklik.Margin = new System.Windows.Forms.Padding(4);
            this.FirmaLogo_Yükseklik.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.FirmaLogo_Yükseklik.Name = "FirmaLogo_Yükseklik";
            this.FirmaLogo_Yükseklik.Size = new System.Drawing.Size(127, 26);
            this.FirmaLogo_Yükseklik.TabIndex = 9;
            this.İpucu.SetToolTip(this.FirmaLogo_Yükseklik, "Logo dosyası DİĞER klasörünün içinde\r\nLOGO.jpg,  LOGO.png veya LOGO.bmp \r\nolarak " +
        "bulunmalıdır");
            this.FirmaLogo_Yükseklik.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 76);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(123, 20);
            this.label6.TabIndex = 15;
            this.label6.Text = "Yükseklik (mm)";
            // 
            // FirmaLogo_Genişlik
            // 
            this.FirmaLogo_Genişlik.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FirmaLogo_Genişlik.Location = new System.Drawing.Point(11, 46);
            this.FirmaLogo_Genişlik.Margin = new System.Windows.Forms.Padding(4);
            this.FirmaLogo_Genişlik.Name = "FirmaLogo_Genişlik";
            this.FirmaLogo_Genişlik.Size = new System.Drawing.Size(127, 26);
            this.FirmaLogo_Genişlik.TabIndex = 18;
            this.İpucu.SetToolTip(this.FirmaLogo_Genişlik, "Logo dosyası DİĞER klasörünün içinde\r\nLOGO.jpg,  LOGO.png veya LOGO.bmp \r\nolarak " +
        "bulunmalıdır");
            this.FirmaLogo_Genişlik.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // İpucu
            // 
            this.İpucu.AutomaticDelay = 0;
            this.İpucu.IsBalloon = true;
            this.İpucu.ShowAlways = true;
            this.İpucu.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.İpucu.ToolTipTitle = "Yazdırma";
            this.İpucu.UseAnimation = false;
            this.İpucu.UseFading = false;
            // 
            // DosyayaYazdır
            // 
            this.DosyayaYazdır.AutoSize = true;
            this.DosyayaYazdır.Location = new System.Drawing.Point(203, 64);
            this.DosyayaYazdır.Name = "DosyayaYazdır";
            this.DosyayaYazdır.Size = new System.Drawing.Size(148, 24);
            this.DosyayaYazdır.TabIndex = 24;
            this.DosyayaYazdır.Text = "Dosyaya Yazdır";
            this.DosyayaYazdır.UseVisualStyleBackColor = true;
            // 
            // Yazdırma
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 428);
            this.Controls.Add(this.DosyayaYazdır);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Kaydet);
            this.Controls.Add(this.KarakterKümeleri);
            this.Controls.Add(this.KenarBoşluğu);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Yazcılar);
            this.Controls.Add(this.Önizleme);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Yazdırma";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Yazdırma";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.KenarBoşluğu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.KarakterBüyüklüğü_Başlıklar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.KarakterBüyüklüğü_Diğerleri)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.KarakterBüyüklüğü_Müşteri)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FirmaLogo_Yükseklik)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FirmaLogo_Genişlik)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.PrintPreviewControl Önizleme;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.ComboBox Yazcılar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown KenarBoşluğu;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown KarakterBüyüklüğü_Başlıklar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox KarakterKümeleri;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown KarakterBüyüklüğü_Diğerleri;
        private System.Windows.Forms.NumericUpDown KarakterBüyüklüğü_Müşteri;
        private System.Windows.Forms.Button Kaydet;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown FirmaLogo_Yükseklik;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown FirmaLogo_Genişlik;
        private System.Windows.Forms.ToolTip İpucu;
        private System.Windows.Forms.CheckBox DosyayaYazdır;
    }
}