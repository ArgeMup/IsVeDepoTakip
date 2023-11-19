namespace İş_ve_Depo_Takip.Ekranlar
{
    partial class Açılış_Ekranı
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
            this.Yeni_Talep_Girişi = new System.Windows.Forms.Button();
            this.Tüm_Talepler = new System.Windows.Forms.Button();
            this.Müşteriler = new System.Windows.Forms.Button();
            this.İş_Türleri = new System.Windows.Forms.Button();
            this.Ücretler = new System.Windows.Forms.Button();
            this.Yazdırma = new System.Windows.Forms.Button();
            this.P_AnaMenü = new System.Windows.Forms.Panel();
            this.GelirGider_CariDöküm = new System.Windows.Forms.Button();
            this.GelirGider_Ekle = new System.Windows.Forms.Button();
            this.ParolayıDeğiştir = new System.Windows.Forms.Button();
            this.ÜcretHesaplama = new System.Windows.Forms.Button();
            this.BarkodGirişi = new System.Windows.Forms.TextBox();
            this.KorumalıAlan = new System.Windows.Forms.Button();
            this.Ayarlar = new System.Windows.Forms.Button();
            this.Takvim = new System.Windows.Forms.Button();
            this.YedekleKapat = new System.Windows.Forms.Button();
            this.Ayarlar_Geri = new System.Windows.Forms.Button();
            this.tab_sayfası = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.P_Ayarlar = new System.Windows.Forms.Panel();
            this.Kullanıcılar = new System.Windows.Forms.Button();
            this.Değişkenler = new System.Windows.Forms.Button();
            this.Etiketleme = new System.Windows.Forms.Button();
            this.Malzemeler = new System.Windows.Forms.Button();
            this.Bütçe = new System.Windows.Forms.Button();
            this.Eposta = new System.Windows.Forms.Button();
            this.Diğer = new System.Windows.Forms.Button();
            this.Hata = new System.Windows.Forms.ErrorProvider(this.components);
            this.İpUcu = new System.Windows.Forms.ToolTip(this.components);
            this.P_AnaMenü.SuspendLayout();
            this.tab_sayfası.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.P_Ayarlar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Hata)).BeginInit();
            this.SuspendLayout();
            // 
            // Yeni_Talep_Girişi
            // 
            this.Yeni_Talep_Girişi.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Yeni_Talep_Girişi.AutoSize = true;
            this.Yeni_Talep_Girişi.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Yeni_Talep_Girişi.Location = new System.Drawing.Point(108, 6);
            this.Yeni_Talep_Girişi.Margin = new System.Windows.Forms.Padding(6);
            this.Yeni_Talep_Girişi.Name = "Yeni_Talep_Girişi";
            this.Yeni_Talep_Girişi.Size = new System.Drawing.Size(130, 40);
            this.Yeni_Talep_Girişi.TabIndex = 0;
            this.Yeni_Talep_Girişi.Text = "Yeni İş Girişi";
            this.İpUcu.SetToolTip(this.Yeni_Talep_Girişi, "F1");
            this.Yeni_Talep_Girişi.UseVisualStyleBackColor = true;
            this.Yeni_Talep_Girişi.Click += new System.EventHandler(this.Tuş_Click);
            // 
            // Tüm_Talepler
            // 
            this.Tüm_Talepler.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Tüm_Talepler.AutoSize = true;
            this.Tüm_Talepler.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Tüm_Talepler.Location = new System.Drawing.Point(250, 6);
            this.Tüm_Talepler.Margin = new System.Windows.Forms.Padding(6);
            this.Tüm_Talepler.Name = "Tüm_Talepler";
            this.Tüm_Talepler.Size = new System.Drawing.Size(130, 40);
            this.Tüm_Talepler.TabIndex = 1;
            this.Tüm_Talepler.Text = "Tüm İşler";
            this.İpUcu.SetToolTip(this.Tüm_Talepler, "F2 İş Takip\r\nF3 Arama");
            this.Tüm_Talepler.UseVisualStyleBackColor = true;
            this.Tüm_Talepler.Click += new System.EventHandler(this.Tuş_Click);
            // 
            // Müşteriler
            // 
            this.Müşteriler.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Müşteriler.Location = new System.Drawing.Point(20, 7);
            this.Müşteriler.Margin = new System.Windows.Forms.Padding(6);
            this.Müşteriler.Name = "Müşteriler";
            this.Müşteriler.Size = new System.Drawing.Size(169, 68);
            this.Müşteriler.TabIndex = 0;
            this.Müşteriler.Text = "Müşteriler";
            this.Müşteriler.UseVisualStyleBackColor = true;
            this.Müşteriler.Click += new System.EventHandler(this.Tuş_Click);
            // 
            // İş_Türleri
            // 
            this.İş_Türleri.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.İş_Türleri.Location = new System.Drawing.Point(20, 87);
            this.İş_Türleri.Margin = new System.Windows.Forms.Padding(6);
            this.İş_Türleri.Name = "İş_Türleri";
            this.İş_Türleri.Size = new System.Drawing.Size(169, 68);
            this.İş_Türleri.TabIndex = 1;
            this.İş_Türleri.Text = "İş Türleri";
            this.İş_Türleri.UseVisualStyleBackColor = true;
            this.İş_Türleri.Click += new System.EventHandler(this.Tuş_Click);
            // 
            // Ücretler
            // 
            this.Ücretler.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Ücretler.Location = new System.Drawing.Point(20, 167);
            this.Ücretler.Margin = new System.Windows.Forms.Padding(6);
            this.Ücretler.Name = "Ücretler";
            this.Ücretler.Size = new System.Drawing.Size(169, 68);
            this.Ücretler.TabIndex = 2;
            this.Ücretler.Text = "Ücretler";
            this.Ücretler.UseVisualStyleBackColor = true;
            this.Ücretler.Click += new System.EventHandler(this.Tuş_Click);
            // 
            // Yazdırma
            // 
            this.Yazdırma.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Yazdırma.Location = new System.Drawing.Point(360, 7);
            this.Yazdırma.Margin = new System.Windows.Forms.Padding(6);
            this.Yazdırma.Name = "Yazdırma";
            this.Yazdırma.Size = new System.Drawing.Size(169, 68);
            this.Yazdırma.TabIndex = 5;
            this.Yazdırma.Text = "Yazdırma";
            this.Yazdırma.UseVisualStyleBackColor = true;
            this.Yazdırma.Click += new System.EventHandler(this.Tuş_Click);
            // 
            // P_AnaMenü
            // 
            this.P_AnaMenü.BackgroundImage = global::İş_ve_Depo_Takip.Properties.Resources.logo_512_seffaf;
            this.P_AnaMenü.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.P_AnaMenü.Controls.Add(this.GelirGider_CariDöküm);
            this.P_AnaMenü.Controls.Add(this.GelirGider_Ekle);
            this.P_AnaMenü.Controls.Add(this.ParolayıDeğiştir);
            this.P_AnaMenü.Controls.Add(this.ÜcretHesaplama);
            this.P_AnaMenü.Controls.Add(this.BarkodGirişi);
            this.P_AnaMenü.Controls.Add(this.KorumalıAlan);
            this.P_AnaMenü.Controls.Add(this.Ayarlar);
            this.P_AnaMenü.Controls.Add(this.Takvim);
            this.P_AnaMenü.Controls.Add(this.YedekleKapat);
            this.P_AnaMenü.Controls.Add(this.Yeni_Talep_Girişi);
            this.P_AnaMenü.Controls.Add(this.Tüm_Talepler);
            this.P_AnaMenü.Location = new System.Drawing.Point(6, 6);
            this.P_AnaMenü.Name = "P_AnaMenü";
            this.P_AnaMenü.Size = new System.Drawing.Size(487, 391);
            this.P_AnaMenü.TabIndex = 10;
            this.P_AnaMenü.Visible = false;
            this.P_AnaMenü.VisibleChanged += new System.EventHandler(this.P_AnaMenü_VisibleChanged);
            // 
            // GelirGider_CariDöküm
            // 
            this.GelirGider_CariDöküm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.GelirGider_CariDöküm.AutoSize = true;
            this.GelirGider_CariDöküm.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.GelirGider_CariDöküm.Location = new System.Drawing.Point(130, 324);
            this.GelirGider_CariDöküm.Name = "GelirGider_CariDöküm";
            this.GelirGider_CariDöküm.Size = new System.Drawing.Size(91, 29);
            this.GelirGider_CariDöküm.TabIndex = 10;
            this.GelirGider_CariDöküm.Text = "Cari Döküm";
            this.İpUcu.SetToolTip(this.GelirGider_CariDöküm, "F4");
            this.GelirGider_CariDöküm.UseVisualStyleBackColor = true;
            this.GelirGider_CariDöküm.Visible = false;
            // 
            // GelirGider_Ekle
            // 
            this.GelirGider_Ekle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.GelirGider_Ekle.AutoSize = true;
            this.GelirGider_Ekle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.GelirGider_Ekle.Location = new System.Drawing.Point(6, 324);
            this.GelirGider_Ekle.Name = "GelirGider_Ekle";
            this.GelirGider_Ekle.Size = new System.Drawing.Size(118, 29);
            this.GelirGider_Ekle.TabIndex = 9;
            this.GelirGider_Ekle.Text = "Gelir Gider Ekle";
            this.İpUcu.SetToolTip(this.GelirGider_Ekle, "F4");
            this.GelirGider_Ekle.UseVisualStyleBackColor = true;
            this.GelirGider_Ekle.Visible = false;
            // 
            // ParolayıDeğiştir
            // 
            this.ParolayıDeğiştir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ParolayıDeğiştir.AutoSize = true;
            this.ParolayıDeğiştir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.ParolayıDeğiştir.Location = new System.Drawing.Point(351, 324);
            this.ParolayıDeğiştir.Margin = new System.Windows.Forms.Padding(6);
            this.ParolayıDeğiştir.Name = "ParolayıDeğiştir";
            this.ParolayıDeğiştir.Size = new System.Drawing.Size(130, 29);
            this.ParolayıDeğiştir.TabIndex = 8;
            this.ParolayıDeğiştir.Text = "Parolayı Değiştir";
            this.ParolayıDeğiştir.UseVisualStyleBackColor = true;
            this.ParolayıDeğiştir.Click += new System.EventHandler(this.Tuş_Click);
            // 
            // ÜcretHesaplama
            // 
            this.ÜcretHesaplama.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ÜcretHesaplama.AutoSize = true;
            this.ÜcretHesaplama.BackColor = System.Drawing.Color.Transparent;
            this.ÜcretHesaplama.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.ÜcretHesaplama.Location = new System.Drawing.Point(351, 260);
            this.ÜcretHesaplama.Margin = new System.Windows.Forms.Padding(6);
            this.ÜcretHesaplama.Name = "ÜcretHesaplama";
            this.ÜcretHesaplama.Size = new System.Drawing.Size(130, 29);
            this.ÜcretHesaplama.TabIndex = 7;
            this.ÜcretHesaplama.Text = "Ücret Hesaplama";
            this.ÜcretHesaplama.UseVisualStyleBackColor = true;
            this.ÜcretHesaplama.Click += new System.EventHandler(this.Tuş_Click);
            // 
            // BarkodGirişi
            // 
            this.BarkodGirişi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BarkodGirişi.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.BarkodGirişi.Location = new System.Drawing.Point(351, 295);
            this.BarkodGirişi.Margin = new System.Windows.Forms.Padding(0);
            this.BarkodGirişi.Name = "BarkodGirişi";
            this.BarkodGirişi.Size = new System.Drawing.Size(130, 23);
            this.BarkodGirişi.TabIndex = 6;
            this.İpUcu.SetToolTip(this.BarkodGirişi, "Barkod veya seri no girişi");
            this.BarkodGirişi.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BarkodGirişi_KeyDown);
            // 
            // KorumalıAlan
            // 
            this.KorumalıAlan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.KorumalıAlan.AutoSize = true;
            this.KorumalıAlan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.KorumalıAlan.Location = new System.Drawing.Point(104, 356);
            this.KorumalıAlan.Margin = new System.Windows.Forms.Padding(6);
            this.KorumalıAlan.Name = "KorumalıAlan";
            this.KorumalıAlan.Size = new System.Drawing.Size(116, 29);
            this.KorumalıAlan.TabIndex = 5;
            this.KorumalıAlan.Text = "Korumalı Alan";
            this.KorumalıAlan.UseVisualStyleBackColor = true;
            this.KorumalıAlan.Click += new System.EventHandler(this.Tuş_Click);
            // 
            // Ayarlar
            // 
            this.Ayarlar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Ayarlar.AutoSize = true;
            this.Ayarlar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Ayarlar.Location = new System.Drawing.Point(232, 356);
            this.Ayarlar.Margin = new System.Windows.Forms.Padding(6);
            this.Ayarlar.Name = "Ayarlar";
            this.Ayarlar.Size = new System.Drawing.Size(81, 29);
            this.Ayarlar.TabIndex = 2;
            this.Ayarlar.Text = "Ayarlar";
            this.Ayarlar.UseVisualStyleBackColor = true;
            this.Ayarlar.Click += new System.EventHandler(this.Ayarlar_Click);
            // 
            // Takvim
            // 
            this.Takvim.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Takvim.AutoSize = true;
            this.Takvim.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Takvim.Location = new System.Drawing.Point(6, 356);
            this.Takvim.Margin = new System.Windows.Forms.Padding(6);
            this.Takvim.Name = "Takvim";
            this.Takvim.Size = new System.Drawing.Size(81, 29);
            this.Takvim.TabIndex = 4;
            this.Takvim.Text = "Takvim";
            this.İpUcu.SetToolTip(this.Takvim, "F4");
            this.Takvim.UseVisualStyleBackColor = true;
            this.Takvim.Click += new System.EventHandler(this.Tuş_Click);
            // 
            // YedekleKapat
            // 
            this.YedekleKapat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.YedekleKapat.AutoSize = true;
            this.YedekleKapat.BackColor = System.Drawing.Color.Transparent;
            this.YedekleKapat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.YedekleKapat.Location = new System.Drawing.Point(351, 356);
            this.YedekleKapat.Margin = new System.Windows.Forms.Padding(6);
            this.YedekleKapat.Name = "YedekleKapat";
            this.YedekleKapat.Size = new System.Drawing.Size(130, 29);
            this.YedekleKapat.TabIndex = 3;
            this.YedekleKapat.Text = "Yedekle ve kapat";
            this.YedekleKapat.UseVisualStyleBackColor = true;
            this.YedekleKapat.Click += new System.EventHandler(this.YedekleKapat_Click);
            // 
            // Ayarlar_Geri
            // 
            this.Ayarlar_Geri.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Ayarlar_Geri.Image = global::İş_ve_Depo_Takip.Properties.Resources.sol;
            this.Ayarlar_Geri.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Ayarlar_Geri.Location = new System.Drawing.Point(20, 327);
            this.Ayarlar_Geri.Margin = new System.Windows.Forms.Padding(6);
            this.Ayarlar_Geri.Name = "Ayarlar_Geri";
            this.Ayarlar_Geri.Size = new System.Drawing.Size(510, 68);
            this.Ayarlar_Geri.TabIndex = 9;
            this.Ayarlar_Geri.Text = "Geri";
            this.Ayarlar_Geri.UseVisualStyleBackColor = true;
            this.Ayarlar_Geri.Click += new System.EventHandler(this.Ayarlar_Geri_Click);
            // 
            // tab_sayfası
            // 
            this.tab_sayfası.Controls.Add(this.tabPage1);
            this.tab_sayfası.Controls.Add(this.tabPage2);
            this.tab_sayfası.Location = new System.Drawing.Point(12, 12);
            this.tab_sayfası.Name = "tab_sayfası";
            this.tab_sayfası.SelectedIndex = 0;
            this.tab_sayfası.Size = new System.Drawing.Size(554, 462);
            this.tab_sayfası.TabIndex = 11;
            this.tab_sayfası.Visible = false;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.P_AnaMenü);
            this.tabPage1.Location = new System.Drawing.Point(4, 38);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(546, 420);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.P_Ayarlar);
            this.tabPage2.Location = new System.Drawing.Point(4, 38);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(546, 420);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // P_Ayarlar
            // 
            this.P_Ayarlar.Controls.Add(this.Kullanıcılar);
            this.P_Ayarlar.Controls.Add(this.Değişkenler);
            this.P_Ayarlar.Controls.Add(this.Etiketleme);
            this.P_Ayarlar.Controls.Add(this.Malzemeler);
            this.P_Ayarlar.Controls.Add(this.Bütçe);
            this.P_Ayarlar.Controls.Add(this.Eposta);
            this.P_Ayarlar.Controls.Add(this.Diğer);
            this.P_Ayarlar.Controls.Add(this.Müşteriler);
            this.P_Ayarlar.Controls.Add(this.Ayarlar_Geri);
            this.P_Ayarlar.Controls.Add(this.Yazdırma);
            this.P_Ayarlar.Controls.Add(this.Ücretler);
            this.P_Ayarlar.Controls.Add(this.İş_Türleri);
            this.P_Ayarlar.Location = new System.Drawing.Point(6, 6);
            this.P_Ayarlar.Name = "P_Ayarlar";
            this.P_Ayarlar.Size = new System.Drawing.Size(552, 401);
            this.P_Ayarlar.TabIndex = 12;
            this.P_Ayarlar.Visible = false;
            this.P_Ayarlar.VisibleChanged += new System.EventHandler(this.P_AnaMenü_VisibleChanged);
            // 
            // Kullanıcılar
            // 
            this.Kullanıcılar.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Kullanıcılar.Location = new System.Drawing.Point(361, 167);
            this.Kullanıcılar.Margin = new System.Windows.Forms.Padding(6);
            this.Kullanıcılar.Name = "Kullanıcılar";
            this.Kullanıcılar.Size = new System.Drawing.Size(169, 68);
            this.Kullanıcılar.TabIndex = 11;
            this.Kullanıcılar.Text = "Kullanıcılar";
            this.Kullanıcılar.UseVisualStyleBackColor = true;
            this.Kullanıcılar.Click += new System.EventHandler(this.Tuş_Click);
            // 
            // Değişkenler
            // 
            this.Değişkenler.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Değişkenler.Location = new System.Drawing.Point(190, 167);
            this.Değişkenler.Margin = new System.Windows.Forms.Padding(6);
            this.Değişkenler.Name = "Değişkenler";
            this.Değişkenler.Size = new System.Drawing.Size(169, 68);
            this.Değişkenler.TabIndex = 11;
            this.Değişkenler.Text = "Değişkenler";
            this.Değişkenler.UseVisualStyleBackColor = true;
            this.Değişkenler.Click += new System.EventHandler(this.Tuş_Click);
            // 
            // Etiketleme
            // 
            this.Etiketleme.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Etiketleme.Location = new System.Drawing.Point(190, 87);
            this.Etiketleme.Margin = new System.Windows.Forms.Padding(6);
            this.Etiketleme.Name = "Etiketleme";
            this.Etiketleme.Size = new System.Drawing.Size(169, 68);
            this.Etiketleme.TabIndex = 10;
            this.Etiketleme.Text = "Etiketleme";
            this.Etiketleme.UseVisualStyleBackColor = true;
            this.Etiketleme.Click += new System.EventHandler(this.Tuş_Click);
            // 
            // Malzemeler
            // 
            this.Malzemeler.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Malzemeler.Location = new System.Drawing.Point(190, 7);
            this.Malzemeler.Margin = new System.Windows.Forms.Padding(6);
            this.Malzemeler.Name = "Malzemeler";
            this.Malzemeler.Size = new System.Drawing.Size(169, 68);
            this.Malzemeler.TabIndex = 4;
            this.Malzemeler.Text = "Malzemeler";
            this.Malzemeler.UseVisualStyleBackColor = true;
            this.Malzemeler.Click += new System.EventHandler(this.Tuş_Click);
            // 
            // Bütçe
            // 
            this.Bütçe.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Bütçe.Location = new System.Drawing.Point(20, 247);
            this.Bütçe.Margin = new System.Windows.Forms.Padding(6);
            this.Bütçe.Name = "Bütçe";
            this.Bütçe.Size = new System.Drawing.Size(169, 68);
            this.Bütçe.TabIndex = 3;
            this.Bütçe.Text = "Bütçe";
            this.Bütçe.UseVisualStyleBackColor = true;
            this.Bütçe.Click += new System.EventHandler(this.Tuş_Click);
            // 
            // Eposta
            // 
            this.Eposta.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Eposta.Location = new System.Drawing.Point(361, 87);
            this.Eposta.Margin = new System.Windows.Forms.Padding(6);
            this.Eposta.Name = "Eposta";
            this.Eposta.Size = new System.Drawing.Size(169, 68);
            this.Eposta.TabIndex = 6;
            this.Eposta.Text = "E-posta";
            this.Eposta.UseVisualStyleBackColor = true;
            this.Eposta.Click += new System.EventHandler(this.Tuş_Click);
            // 
            // Diğer
            // 
            this.Diğer.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Diğer.Location = new System.Drawing.Point(361, 247);
            this.Diğer.Margin = new System.Windows.Forms.Padding(6);
            this.Diğer.Name = "Diğer";
            this.Diğer.Size = new System.Drawing.Size(169, 68);
            this.Diğer.TabIndex = 8;
            this.Diğer.Text = "Diğer";
            this.Diğer.UseVisualStyleBackColor = true;
            this.Diğer.Click += new System.EventHandler(this.Tuş_Click);
            // 
            // Hata
            // 
            this.Hata.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.AlwaysBlink;
            this.Hata.ContainerControl = this;
            // 
            // İpUcu
            // 
            this.İpUcu.AutomaticDelay = 100;
            this.İpUcu.AutoPopDelay = 10000;
            this.İpUcu.InitialDelay = 100;
            this.İpUcu.ReshowDelay = 20;
            // 
            // Açılış_Ekranı
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(15F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 499);
            this.Controls.Add(this.tab_sayfası);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.Name = "Açılış_Ekranı";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Shown += new System.EventHandler(this.Açılış_Ekranı_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Açılış_Ekranı_KeyDown);
            this.P_AnaMenü.ResumeLayout(false);
            this.P_AnaMenü.PerformLayout();
            this.tab_sayfası.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.P_Ayarlar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Hata)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Yeni_Talep_Girişi;
        private System.Windows.Forms.Button Tüm_Talepler;
        private System.Windows.Forms.Button Müşteriler;
        private System.Windows.Forms.Button İş_Türleri;
        private System.Windows.Forms.Button Ücretler;
        private System.Windows.Forms.Button Yazdırma;
        private System.Windows.Forms.Button Ayarlar;
        private System.Windows.Forms.Button Ayarlar_Geri;
        private System.Windows.Forms.TabControl tab_sayfası;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel P_Ayarlar;
        private System.Windows.Forms.ErrorProvider Hata;
        private System.Windows.Forms.Button Diğer;
        private System.Windows.Forms.Button Eposta;
        private System.Windows.Forms.Button Bütçe;
        public System.Windows.Forms.Button YedekleKapat;
        private System.Windows.Forms.Button Malzemeler;
        private System.Windows.Forms.Button Takvim;
        private System.Windows.Forms.Button KorumalıAlan;
        private System.Windows.Forms.ToolTip İpUcu;
        private System.Windows.Forms.Button Etiketleme;
        private System.Windows.Forms.Panel P_AnaMenü;
        private System.Windows.Forms.TextBox BarkodGirişi;
        private System.Windows.Forms.Button Değişkenler;
        public System.Windows.Forms.Button ÜcretHesaplama;
        private System.Windows.Forms.Button Kullanıcılar;
        private System.Windows.Forms.Button ParolayıDeğiştir;
        private System.Windows.Forms.Button GelirGider_CariDöküm;
        private System.Windows.Forms.Button GelirGider_Ekle;
    }
}

