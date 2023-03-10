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
            this.P_YeniParola = new System.Windows.Forms.Panel();
            this.YeniParola_Etiket = new System.Windows.Forms.Label();
            this.YeniParola_1 = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.YeniParola_Kaydet = new System.Windows.Forms.Button();
            this.YeniParola_2 = new System.Windows.Forms.MaskedTextBox();
            this.P_Parola = new System.Windows.Forms.Panel();
            this.Parola_Kontrol = new System.Windows.Forms.Button();
            this.Parola_Giriş = new System.Windows.Forms.MaskedTextBox();
            this.P_AnaMenü = new System.Windows.Forms.Panel();
            this.KorumalıAlan = new System.Windows.Forms.Button();
            this.Ayarlar = new System.Windows.Forms.Button();
            this.Takvim = new System.Windows.Forms.Button();
            this.YedekleKapat = new System.Windows.Forms.Button();
            this.Ayarlar_Geri = new System.Windows.Forms.Button();
            this.tab_sayfası = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.P_Ayarlar = new System.Windows.Forms.Panel();
            this.Malzemeler = new System.Windows.Forms.Button();
            this.Bütçe = new System.Windows.Forms.Button();
            this.ParolayıDeğiştir = new System.Windows.Forms.Button();
            this.Eposta = new System.Windows.Forms.Button();
            this.Diğer = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.Hata = new System.Windows.Forms.ErrorProvider(this.components);
            this.AçılışYazısı = new System.Windows.Forms.Label();
            this.İpUcu = new System.Windows.Forms.ToolTip(this.components);
            this.P_YeniParola.SuspendLayout();
            this.P_Parola.SuspendLayout();
            this.P_AnaMenü.SuspendLayout();
            this.tab_sayfası.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.P_Ayarlar.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Hata)).BeginInit();
            this.SuspendLayout();
            // 
            // Yeni_Talep_Girişi
            // 
            this.Yeni_Talep_Girişi.Anchor = System.Windows.Forms.AnchorStyles.Top;
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
            this.Yazdırma.Location = new System.Drawing.Point(363, 7);
            this.Yazdırma.Margin = new System.Windows.Forms.Padding(6);
            this.Yazdırma.Name = "Yazdırma";
            this.Yazdırma.Size = new System.Drawing.Size(169, 68);
            this.Yazdırma.TabIndex = 5;
            this.Yazdırma.Text = "Yazdırma";
            this.Yazdırma.UseVisualStyleBackColor = true;
            this.Yazdırma.Click += new System.EventHandler(this.Tuş_Click);
            // 
            // P_YeniParola
            // 
            this.P_YeniParola.Controls.Add(this.YeniParola_Etiket);
            this.P_YeniParola.Controls.Add(this.YeniParola_1);
            this.P_YeniParola.Controls.Add(this.label1);
            this.P_YeniParola.Controls.Add(this.YeniParola_Kaydet);
            this.P_YeniParola.Controls.Add(this.YeniParola_2);
            this.P_YeniParola.Location = new System.Drawing.Point(25, 19);
            this.P_YeniParola.Name = "P_YeniParola";
            this.P_YeniParola.Size = new System.Drawing.Size(347, 246);
            this.P_YeniParola.TabIndex = 9;
            this.P_YeniParola.Tag = "";
            this.P_YeniParola.Visible = false;
            // 
            // YeniParola_Etiket
            // 
            this.YeniParola_Etiket.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.YeniParola_Etiket.AutoSize = true;
            this.YeniParola_Etiket.Location = new System.Drawing.Point(27, 14);
            this.YeniParola_Etiket.Name = "YeniParola_Etiket";
            this.YeniParola_Etiket.Size = new System.Drawing.Size(207, 29);
            this.YeniParola_Etiket.TabIndex = 9;
            this.YeniParola_Etiket.Text = "Bir parola seçiniz";
            // 
            // YeniParola_1
            // 
            this.YeniParola_1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.YeniParola_1.Location = new System.Drawing.Point(32, 46);
            this.YeniParola_1.Name = "YeniParola_1";
            this.YeniParola_1.Size = new System.Drawing.Size(283, 36);
            this.YeniParola_1.TabIndex = 0;
            this.YeniParola_1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.YeniParola_1.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(250, 29);
            this.label1.TabIndex = 7;
            this.label1.Text = "Parolayı tekrar giriniz";
            // 
            // YeniParola_Kaydet
            // 
            this.YeniParola_Kaydet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.YeniParola_Kaydet.Location = new System.Drawing.Point(32, 163);
            this.YeniParola_Kaydet.Margin = new System.Windows.Forms.Padding(6);
            this.YeniParola_Kaydet.Name = "YeniParola_Kaydet";
            this.YeniParola_Kaydet.Size = new System.Drawing.Size(283, 68);
            this.YeniParola_Kaydet.TabIndex = 2;
            this.YeniParola_Kaydet.Text = "Yeni Parolayı Kaydet";
            this.YeniParola_Kaydet.UseVisualStyleBackColor = true;
            this.YeniParola_Kaydet.Click += new System.EventHandler(this.YeniParola_Kaydet_Click);
            // 
            // YeniParola_2
            // 
            this.YeniParola_2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.YeniParola_2.Location = new System.Drawing.Point(32, 118);
            this.YeniParola_2.Name = "YeniParola_2";
            this.YeniParola_2.Size = new System.Drawing.Size(283, 36);
            this.YeniParola_2.TabIndex = 1;
            this.YeniParola_2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.YeniParola_2.UseSystemPasswordChar = true;
            // 
            // P_Parola
            // 
            this.P_Parola.BackgroundImage = global::İş_ve_Depo_Takip.Properties.Resources.logo_512_seffaf;
            this.P_Parola.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.P_Parola.Controls.Add(this.Parola_Kontrol);
            this.P_Parola.Controls.Add(this.Parola_Giriş);
            this.P_Parola.Location = new System.Drawing.Point(16, 16);
            this.P_Parola.Name = "P_Parola";
            this.P_Parola.Size = new System.Drawing.Size(441, 364);
            this.P_Parola.TabIndex = 10;
            this.P_Parola.Visible = false;
            // 
            // Parola_Kontrol
            // 
            this.Parola_Kontrol.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Parola_Kontrol.Location = new System.Drawing.Point(226, 318);
            this.Parola_Kontrol.Margin = new System.Windows.Forms.Padding(6);
            this.Parola_Kontrol.Name = "Parola_Kontrol";
            this.Parola_Kontrol.Size = new System.Drawing.Size(100, 40);
            this.Parola_Kontrol.TabIndex = 1;
            this.Parola_Kontrol.Text = "Giriş";
            this.Parola_Kontrol.UseVisualStyleBackColor = true;
            this.Parola_Kontrol.Click += new System.EventHandler(this.Parola_Kontrol_Click);
            // 
            // Parola_Giriş
            // 
            this.Parola_Giriş.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Parola_Giriş.Location = new System.Drawing.Point(117, 321);
            this.Parola_Giriş.Name = "Parola_Giriş";
            this.Parola_Giriş.Size = new System.Drawing.Size(100, 36);
            this.Parola_Giriş.TabIndex = 0;
            this.Parola_Giriş.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Parola_Giriş.UseSystemPasswordChar = true;
            this.Parola_Giriş.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Parola_Giriş_KeyUp);
            // 
            // P_AnaMenü
            // 
            this.P_AnaMenü.BackgroundImage = global::İş_ve_Depo_Takip.Properties.Resources.logo_512_seffaf;
            this.P_AnaMenü.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
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
            // KorumalıAlan
            // 
            this.KorumalıAlan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
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
            this.Ayarlar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Ayarlar.Location = new System.Drawing.Point(237, 356);
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
            this.Ayarlar_Geri.Size = new System.Drawing.Size(513, 68);
            this.Ayarlar_Geri.TabIndex = 9;
            this.Ayarlar_Geri.Text = "Geri";
            this.Ayarlar_Geri.UseVisualStyleBackColor = true;
            this.Ayarlar_Geri.Click += new System.EventHandler(this.Ayarlar_Geri_Click);
            // 
            // tab_sayfası
            // 
            this.tab_sayfası.Controls.Add(this.tabPage1);
            this.tab_sayfası.Controls.Add(this.tabPage2);
            this.tab_sayfası.Controls.Add(this.tabPage3);
            this.tab_sayfası.Controls.Add(this.tabPage4);
            this.tab_sayfası.Location = new System.Drawing.Point(12, 12);
            this.tab_sayfası.Name = "tab_sayfası";
            this.tab_sayfası.SelectedIndex = 0;
            this.tab_sayfası.Size = new System.Drawing.Size(584, 496);
            this.tab_sayfası.TabIndex = 11;
            this.tab_sayfası.Visible = false;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.P_AnaMenü);
            this.tabPage1.Location = new System.Drawing.Point(4, 38);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(576, 454);
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
            this.tabPage2.Size = new System.Drawing.Size(576, 454);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // P_Ayarlar
            // 
            this.P_Ayarlar.BackgroundImage = global::İş_ve_Depo_Takip.Properties.Resources.logo_512_seffaf;
            this.P_Ayarlar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.P_Ayarlar.Controls.Add(this.Malzemeler);
            this.P_Ayarlar.Controls.Add(this.Bütçe);
            this.P_Ayarlar.Controls.Add(this.ParolayıDeğiştir);
            this.P_Ayarlar.Controls.Add(this.Eposta);
            this.P_Ayarlar.Controls.Add(this.Diğer);
            this.P_Ayarlar.Controls.Add(this.Müşteriler);
            this.P_Ayarlar.Controls.Add(this.Ayarlar_Geri);
            this.P_Ayarlar.Controls.Add(this.Yazdırma);
            this.P_Ayarlar.Controls.Add(this.Ücretler);
            this.P_Ayarlar.Controls.Add(this.İş_Türleri);
            this.P_Ayarlar.Location = new System.Drawing.Point(6, 6);
            this.P_Ayarlar.Name = "P_Ayarlar";
            this.P_Ayarlar.Size = new System.Drawing.Size(555, 401);
            this.P_Ayarlar.TabIndex = 12;
            this.P_Ayarlar.Visible = false;
            this.P_Ayarlar.VisibleChanged += new System.EventHandler(this.P_AnaMenü_VisibleChanged);
            // 
            // Malzemeler
            // 
            this.Malzemeler.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Malzemeler.Location = new System.Drawing.Point(191, 7);
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
            // ParolayıDeğiştir
            // 
            this.ParolayıDeğiştir.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ParolayıDeğiştir.Location = new System.Drawing.Point(364, 167);
            this.ParolayıDeğiştir.Margin = new System.Windows.Forms.Padding(6);
            this.ParolayıDeğiştir.Name = "ParolayıDeğiştir";
            this.ParolayıDeğiştir.Size = new System.Drawing.Size(169, 68);
            this.ParolayıDeğiştir.TabIndex = 7;
            this.ParolayıDeğiştir.Text = "Parola";
            this.ParolayıDeğiştir.UseVisualStyleBackColor = true;
            this.ParolayıDeğiştir.Click += new System.EventHandler(this.ParolayıDeğiştir_Click);
            // 
            // Eposta
            // 
            this.Eposta.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Eposta.Location = new System.Drawing.Point(364, 87);
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
            this.Diğer.Location = new System.Drawing.Point(364, 247);
            this.Diğer.Margin = new System.Windows.Forms.Padding(6);
            this.Diğer.Name = "Diğer";
            this.Diğer.Size = new System.Drawing.Size(169, 68);
            this.Diğer.TabIndex = 8;
            this.Diğer.Text = "Diğer";
            this.Diğer.UseVisualStyleBackColor = true;
            this.Diğer.Click += new System.EventHandler(this.Tuş_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.P_Parola);
            this.tabPage3.Location = new System.Drawing.Point(4, 38);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(576, 454);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.P_YeniParola);
            this.tabPage4.Location = new System.Drawing.Point(4, 38);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(576, 454);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "tabPage4";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // Hata
            // 
            this.Hata.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.AlwaysBlink;
            this.Hata.ContainerControl = this;
            // 
            // AçılışYazısı
            // 
            this.AçılışYazısı.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AçılışYazısı.Image = global::İş_ve_Depo_Takip.Properties.Resources.sag;
            this.AçılışYazısı.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.AçılışYazısı.Location = new System.Drawing.Point(0, 0);
            this.AçılışYazısı.Name = "AçılışYazısı";
            this.AçılışYazısı.Size = new System.Drawing.Size(578, 499);
            this.AçılışYazısı.TabIndex = 12;
            this.AçılışYazısı.Text = "Lütfen bekleyiniz";
            this.AçılışYazısı.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.Controls.Add(this.AçılışYazısı);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.Name = "Açılış_Ekranı";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Açılış_Ekranı_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Açılış_Ekranı_FormClosed);
            this.Shown += new System.EventHandler(this.Açılış_Ekranı_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Açılış_Ekranı_KeyDown);
            this.Resize += new System.EventHandler(this.Açılış_Ekranı_Resize);
            this.P_YeniParola.ResumeLayout(false);
            this.P_YeniParola.PerformLayout();
            this.P_Parola.ResumeLayout(false);
            this.P_Parola.PerformLayout();
            this.P_AnaMenü.ResumeLayout(false);
            this.tab_sayfası.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.P_Ayarlar.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
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
        private System.Windows.Forms.Panel P_YeniParola;
        private System.Windows.Forms.MaskedTextBox YeniParola_2;
        private System.Windows.Forms.Panel P_Parola;
        private System.Windows.Forms.Panel P_AnaMenü;
        private System.Windows.Forms.Label YeniParola_Etiket;
        private System.Windows.Forms.MaskedTextBox YeniParola_1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button YeniParola_Kaydet;
        private System.Windows.Forms.Button Parola_Kontrol;
        private System.Windows.Forms.MaskedTextBox Parola_Giriş;
        private System.Windows.Forms.Button Ayarlar;
        private System.Windows.Forms.Button Ayarlar_Geri;
        private System.Windows.Forms.TabControl tab_sayfası;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel P_Ayarlar;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ErrorProvider Hata;
        private System.Windows.Forms.Button Diğer;
        private System.Windows.Forms.Button Eposta;
        private System.Windows.Forms.Label AçılışYazısı;
        private System.Windows.Forms.Button ParolayıDeğiştir;
        private System.Windows.Forms.Button Bütçe;
        public System.Windows.Forms.Button YedekleKapat;
        private System.Windows.Forms.Button Malzemeler;
        private System.Windows.Forms.Button Takvim;
        private System.Windows.Forms.Button KorumalıAlan;
        private System.Windows.Forms.ToolTip İpUcu;
    }
}

