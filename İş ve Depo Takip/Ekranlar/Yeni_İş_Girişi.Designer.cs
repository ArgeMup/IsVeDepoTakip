using System;

namespace İş_ve_Depo_Takip.Ekranlar
{
    partial class Yeni_İş_Girişi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Yeni_İş_Girişi));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Notlar = new System.Windows.Forms.TextBox();
            this.Tablo = new System.Windows.Forms.DataGridView();
            this.Tablo_İş_Türü = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tablo_Ücret = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tablo_Giriş_Tarihi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tablo_Çıkış_Tarihi = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Kaydet = new System.Windows.Forms.Button();
            this.Seçili_Satırı_Sil = new System.Windows.Forms.Button();
            this.İskonto = new System.Windows.Forms.TextBox();
            this.İpUcu_Ücretlendirme = new System.Windows.Forms.ToolTip(this.components);
            this.Müşteriler_AramaÇubuğu = new System.Windows.Forms.TextBox();
            this.Hastalar_AramaÇubuğu = new System.Windows.Forms.TextBox();
            this.İşTürleri_AramaÇubuğu = new System.Windows.Forms.TextBox();
            this.Hastalar_AdVeSoyadıDüzelt = new System.Windows.Forms.CheckBox();
            this.Müşteriler_Grup = new System.Windows.Forms.GroupBox();
            this.Müşteriler_SeçimKutusu = new System.Windows.Forms.ListBox();
            this.Hastalar_Grup = new System.Windows.Forms.GroupBox();
            this.Hastalar_SeçimKutusu = new System.Windows.Forms.ListBox();
            this.İşTürleri = new System.Windows.Forms.GroupBox();
            this.İştürü_SeçiliSatıraKopyala = new System.Windows.Forms.Button();
            this.İşTürleri_SeçimKutusu = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.Ayraç_Kat_3_SolSağ = new System.Windows.Forms.SplitContainer();
            this.P_DosyaEkleri = new System.Windows.Forms.Panel();
            this.P_DosyaEkleri_Ayraç_SolSağ = new System.Windows.Forms.SplitContainer();
            this.P_DosyaEkleri_Liste = new System.Windows.Forms.ListBox();
            this.P_DosyaEkleri_Sil = new System.Windows.Forms.Button();
            this.P_DosyaEkleri_Geri = new System.Windows.Forms.Button();
            this.DosyaEkleri = new System.Windows.Forms.Button();
            this.Ayraç_Kat_2_3 = new System.Windows.Forms.SplitContainer();
            this.SeriNoGörseli = new System.Windows.Forms.Label();
            this.Ayraç_Kat_1_2 = new System.Windows.Forms.SplitContainer();
            this.İpUcu_Genel = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.Tablo)).BeginInit();
            this.Müşteriler_Grup.SuspendLayout();
            this.Hastalar_Grup.SuspendLayout();
            this.İşTürleri.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Ayraç_Kat_3_SolSağ)).BeginInit();
            this.Ayraç_Kat_3_SolSağ.Panel1.SuspendLayout();
            this.Ayraç_Kat_3_SolSağ.Panel2.SuspendLayout();
            this.Ayraç_Kat_3_SolSağ.SuspendLayout();
            this.P_DosyaEkleri.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.P_DosyaEkleri_Ayraç_SolSağ)).BeginInit();
            this.P_DosyaEkleri_Ayraç_SolSağ.Panel1.SuspendLayout();
            this.P_DosyaEkleri_Ayraç_SolSağ.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Ayraç_Kat_2_3)).BeginInit();
            this.Ayraç_Kat_2_3.Panel1.SuspendLayout();
            this.Ayraç_Kat_2_3.Panel2.SuspendLayout();
            this.Ayraç_Kat_2_3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Ayraç_Kat_1_2)).BeginInit();
            this.Ayraç_Kat_1_2.Panel1.SuspendLayout();
            this.Ayraç_Kat_1_2.Panel2.SuspendLayout();
            this.Ayraç_Kat_1_2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Notlar
            // 
            this.Notlar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Notlar.Location = new System.Drawing.Point(3, 35);
            this.Notlar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Notlar.Multiline = true;
            this.Notlar.Name = "Notlar";
            this.Notlar.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.Notlar.Size = new System.Drawing.Size(901, 39);
            this.Notlar.TabIndex = 5;
            this.İpUcu_Ücretlendirme.SetToolTip(this.Notlar, resources.GetString("Notlar.ToolTip"));
            this.Notlar.TextChanged += new System.EventHandler(this.Değişiklik_Yapılıyor);
            this.Notlar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Notlar_KeyDown);
            // 
            // Tablo
            // 
            this.Tablo.AllowUserToResizeColumns = false;
            this.Tablo.AllowUserToResizeRows = false;
            this.Tablo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tablo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.Tablo.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.Tablo.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tablo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Tablo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tablo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Tablo_İş_Türü,
            this.Tablo_Ücret,
            this.Tablo_Giriş_Tarihi,
            this.Tablo_Çıkış_Tarihi});
            this.Tablo.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.Tablo.Location = new System.Drawing.Point(3, 3);
            this.Tablo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Tablo.MultiSelect = false;
            this.Tablo.Name = "Tablo";
            this.Tablo.RowHeadersVisible = false;
            this.Tablo.RowHeadersWidth = 51;
            this.Tablo.RowTemplate.Height = 24;
            this.Tablo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Tablo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Tablo.ShowCellErrors = false;
            this.Tablo.ShowEditingIcon = false;
            this.Tablo.ShowRowErrors = false;
            this.Tablo.Size = new System.Drawing.Size(616, 206);
            this.Tablo.TabIndex = 9;
            this.Tablo.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tablo_CellContentClick);
            this.Tablo.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tablo_CellValueChanged);
            this.Tablo.SelectionChanged += new System.EventHandler(this.Tablo_SelectionChanged);
            // 
            // Tablo_İş_Türü
            // 
            this.Tablo_İş_Türü.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Tablo_İş_Türü.FillWeight = 70F;
            this.Tablo_İş_Türü.HeaderText = "İş Türü";
            this.Tablo_İş_Türü.MinimumWidth = 6;
            this.Tablo_İş_Türü.Name = "Tablo_İş_Türü";
            this.Tablo_İş_Türü.ReadOnly = true;
            this.Tablo_İş_Türü.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Tablo_İş_Türü.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Tablo_Ücret
            // 
            this.Tablo_Ücret.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Tablo_Ücret.DefaultCellStyle = dataGridViewCellStyle2;
            this.Tablo_Ücret.FillWeight = 30F;
            this.Tablo_Ücret.HeaderText = "Ücret ₺";
            this.Tablo_Ücret.MinimumWidth = 6;
            this.Tablo_Ücret.Name = "Tablo_Ücret";
            this.Tablo_Ücret.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Tablo_Giriş_Tarihi
            // 
            this.Tablo_Giriş_Tarihi.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Tablo_Giriş_Tarihi.DefaultCellStyle = dataGridViewCellStyle3;
            this.Tablo_Giriş_Tarihi.HeaderText = "Kabul Tarihi";
            this.Tablo_Giriş_Tarihi.MinimumWidth = 6;
            this.Tablo_Giriş_Tarihi.Name = "Tablo_Giriş_Tarihi";
            this.Tablo_Giriş_Tarihi.ReadOnly = true;
            this.Tablo_Giriş_Tarihi.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Tablo_Giriş_Tarihi.Width = 104;
            // 
            // Tablo_Çıkış_Tarihi
            // 
            this.Tablo_Çıkış_Tarihi.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Tablo_Çıkış_Tarihi.HeaderText = "Çıkış Tarihi";
            this.Tablo_Çıkış_Tarihi.MinimumWidth = 6;
            this.Tablo_Çıkış_Tarihi.Name = "Tablo_Çıkış_Tarihi";
            this.Tablo_Çıkış_Tarihi.Width = 99;
            // 
            // Kaydet
            // 
            this.Kaydet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Kaydet.Enabled = false;
            this.Kaydet.Image = global::İş_ve_Depo_Takip.Properties.Resources.sag;
            this.Kaydet.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Kaydet.Location = new System.Drawing.Point(429, 214);
            this.Kaydet.Margin = new System.Windows.Forms.Padding(2);
            this.Kaydet.Name = "Kaydet";
            this.Kaydet.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.Kaydet.Size = new System.Drawing.Size(190, 29);
            this.Kaydet.TabIndex = 11;
            this.Kaydet.Text = "Kaydet";
            this.Kaydet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Kaydet.UseVisualStyleBackColor = true;
            this.Kaydet.Click += new System.EventHandler(this.Kaydet_Click);
            // 
            // Seçili_Satırı_Sil
            // 
            this.Seçili_Satırı_Sil.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Seçili_Satırı_Sil.Image = global::İş_ve_Depo_Takip.Properties.Resources.sil;
            this.Seçili_Satırı_Sil.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Seçili_Satırı_Sil.Location = new System.Drawing.Point(3, 214);
            this.Seçili_Satırı_Sil.Margin = new System.Windows.Forms.Padding(2);
            this.Seçili_Satırı_Sil.Name = "Seçili_Satırı_Sil";
            this.Seçili_Satırı_Sil.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.Seçili_Satırı_Sil.Size = new System.Drawing.Size(190, 29);
            this.Seçili_Satırı_Sil.TabIndex = 10;
            this.Seçili_Satırı_Sil.Text = "Seçili Satırı Sil";
            this.Seçili_Satırı_Sil.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Seçili_Satırı_Sil.UseVisualStyleBackColor = true;
            this.Seçili_Satırı_Sil.Click += new System.EventHandler(this.Seçili_Satırı_Sil_Click);
            // 
            // İskonto
            // 
            this.İskonto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.İskonto.Location = new System.Drawing.Point(790, 5);
            this.İskonto.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.İskonto.Name = "İskonto";
            this.İskonto.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.İskonto.Size = new System.Drawing.Size(98, 26);
            this.İskonto.TabIndex = 4;
            this.İskonto.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.İskonto.TextChanged += new System.EventHandler(this.Değişiklik_Yapılıyor);
            // 
            // İpUcu_Ücretlendirme
            // 
            this.İpUcu_Ücretlendirme.AutomaticDelay = 100;
            this.İpUcu_Ücretlendirme.AutoPopDelay = 20000;
            this.İpUcu_Ücretlendirme.InitialDelay = 100;
            this.İpUcu_Ücretlendirme.IsBalloon = true;
            this.İpUcu_Ücretlendirme.ReshowDelay = 20;
            this.İpUcu_Ücretlendirme.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.İpUcu_Ücretlendirme.ToolTipTitle = "Ücretlendirme";
            this.İpUcu_Ücretlendirme.UseAnimation = false;
            this.İpUcu_Ücretlendirme.UseFading = false;
            // 
            // Müşteriler_AramaÇubuğu
            // 
            this.Müşteriler_AramaÇubuğu.Dock = System.Windows.Forms.DockStyle.Top;
            this.Müşteriler_AramaÇubuğu.Location = new System.Drawing.Point(3, 22);
            this.Müşteriler_AramaÇubuğu.Name = "Müşteriler_AramaÇubuğu";
            this.Müşteriler_AramaÇubuğu.Size = new System.Drawing.Size(447, 26);
            this.Müşteriler_AramaÇubuğu.TabIndex = 0;
            this.İpUcu_Genel.SetToolTip(this.Müşteriler_AramaÇubuğu, "Arama çubuğu");
            this.Müşteriler_AramaÇubuğu.TextChanged += new System.EventHandler(this.Müşteriler_AramaÇubuğu_TextChanged);
            this.Müşteriler_AramaÇubuğu.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Müşteriler_AramaÇubuğu_KeyPress);
            // 
            // Hastalar_AramaÇubuğu
            // 
            this.Hastalar_AramaÇubuğu.Dock = System.Windows.Forms.DockStyle.Top;
            this.Hastalar_AramaÇubuğu.Location = new System.Drawing.Point(3, 22);
            this.Hastalar_AramaÇubuğu.Name = "Hastalar_AramaÇubuğu";
            this.Hastalar_AramaÇubuğu.Size = new System.Drawing.Size(444, 26);
            this.Hastalar_AramaÇubuğu.TabIndex = 2;
            this.İpUcu_Genel.SetToolTip(this.Hastalar_AramaÇubuğu, "Arama çubuğu");
            this.Hastalar_AramaÇubuğu.TextChanged += new System.EventHandler(this.Hastalar_AramaÇubuğu_TextChanged);
            this.Hastalar_AramaÇubuğu.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Hastalar_AramaÇubuğu_KeyPress);
            this.Hastalar_AramaÇubuğu.Leave += new System.EventHandler(this.Hastalar_AramaÇubuğu_Leave);
            // 
            // İşTürleri_AramaÇubuğu
            // 
            this.İşTürleri_AramaÇubuğu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.İşTürleri_AramaÇubuğu.Location = new System.Drawing.Point(9, 25);
            this.İşTürleri_AramaÇubuğu.Name = "İşTürleri_AramaÇubuğu";
            this.İşTürleri_AramaÇubuğu.Size = new System.Drawing.Size(250, 26);
            this.İşTürleri_AramaÇubuğu.TabIndex = 6;
            this.İşTürleri_AramaÇubuğu.TextChanged += new System.EventHandler(this.İşTürleri_AramaÇubuğu_TextChanged);
            this.İşTürleri_AramaÇubuğu.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.İşTürleri_AramaÇubuğu_KeyPress);
            // 
            // Hastalar_AdVeSoyadıDüzelt
            // 
            this.Hastalar_AdVeSoyadıDüzelt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Hastalar_AdVeSoyadıDüzelt.AutoSize = true;
            this.Hastalar_AdVeSoyadıDüzelt.Checked = true;
            this.Hastalar_AdVeSoyadıDüzelt.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Hastalar_AdVeSoyadıDüzelt.Location = new System.Drawing.Point(425, 24);
            this.Hastalar_AdVeSoyadıDüzelt.Name = "Hastalar_AdVeSoyadıDüzelt";
            this.Hastalar_AdVeSoyadıDüzelt.Size = new System.Drawing.Size(18, 17);
            this.Hastalar_AdVeSoyadıDüzelt.TabIndex = 4;
            this.İpUcu_Genel.SetToolTip(this.Hastalar_AdVeSoyadıDüzelt, "Ad SOYAD olarak düzenle");
            this.Hastalar_AdVeSoyadıDüzelt.UseVisualStyleBackColor = true;
            // 
            // Müşteriler_Grup
            // 
            this.Müşteriler_Grup.Controls.Add(this.Müşteriler_SeçimKutusu);
            this.Müşteriler_Grup.Controls.Add(this.Müşteriler_AramaÇubuğu);
            this.Müşteriler_Grup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Müşteriler_Grup.Location = new System.Drawing.Point(0, 0);
            this.Müşteriler_Grup.Name = "Müşteriler_Grup";
            this.Müşteriler_Grup.Size = new System.Drawing.Size(453, 106);
            this.Müşteriler_Grup.TabIndex = 13;
            this.Müşteriler_Grup.TabStop = false;
            this.Müşteriler_Grup.Text = "Müşteri";
            // 
            // Müşteriler_SeçimKutusu
            // 
            this.Müşteriler_SeçimKutusu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Müşteriler_SeçimKutusu.FormattingEnabled = true;
            this.Müşteriler_SeçimKutusu.HorizontalScrollbar = true;
            this.Müşteriler_SeçimKutusu.ItemHeight = 20;
            this.Müşteriler_SeçimKutusu.Location = new System.Drawing.Point(3, 51);
            this.Müşteriler_SeçimKutusu.Margin = new System.Windows.Forms.Padding(2);
            this.Müşteriler_SeçimKutusu.Name = "Müşteriler_SeçimKutusu";
            this.Müşteriler_SeçimKutusu.Size = new System.Drawing.Size(447, 44);
            this.Müşteriler_SeçimKutusu.TabIndex = 1;
            this.Müşteriler_SeçimKutusu.SelectedIndexChanged += new System.EventHandler(this.Müşteriler_SeçimKutusu_SelectedIndexChanged);
            this.Müşteriler_SeçimKutusu.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Müşteriler_SeçimKutusu_KeyPress);
            // 
            // Hastalar_Grup
            // 
            this.Hastalar_Grup.Controls.Add(this.Hastalar_AdVeSoyadıDüzelt);
            this.Hastalar_Grup.Controls.Add(this.Hastalar_SeçimKutusu);
            this.Hastalar_Grup.Controls.Add(this.Hastalar_AramaÇubuğu);
            this.Hastalar_Grup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Hastalar_Grup.Location = new System.Drawing.Point(0, 0);
            this.Hastalar_Grup.Name = "Hastalar_Grup";
            this.Hastalar_Grup.Size = new System.Drawing.Size(450, 106);
            this.Hastalar_Grup.TabIndex = 14;
            this.Hastalar_Grup.TabStop = false;
            this.Hastalar_Grup.Text = "Hasta";
            // 
            // Hastalar_SeçimKutusu
            // 
            this.Hastalar_SeçimKutusu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Hastalar_SeçimKutusu.FormattingEnabled = true;
            this.Hastalar_SeçimKutusu.HorizontalScrollbar = true;
            this.Hastalar_SeçimKutusu.ItemHeight = 20;
            this.Hastalar_SeçimKutusu.Location = new System.Drawing.Point(3, 52);
            this.Hastalar_SeçimKutusu.Margin = new System.Windows.Forms.Padding(2);
            this.Hastalar_SeçimKutusu.Name = "Hastalar_SeçimKutusu";
            this.Hastalar_SeçimKutusu.Size = new System.Drawing.Size(444, 44);
            this.Hastalar_SeçimKutusu.TabIndex = 3;
            this.Hastalar_SeçimKutusu.SelectedIndexChanged += new System.EventHandler(this.Hastalar_SeçimKutusu_SelectedIndexChanged);
            // 
            // İşTürleri
            // 
            this.İşTürleri.Controls.Add(this.İştürü_SeçiliSatıraKopyala);
            this.İşTürleri.Controls.Add(this.İşTürleri_AramaÇubuğu);
            this.İşTürleri.Controls.Add(this.İşTürleri_SeçimKutusu);
            this.İşTürleri.Dock = System.Windows.Forms.DockStyle.Fill;
            this.İşTürleri.Location = new System.Drawing.Point(0, 0);
            this.İşTürleri.Name = "İşTürleri";
            this.İşTürleri.Size = new System.Drawing.Size(271, 247);
            this.İşTürleri.TabIndex = 14;
            this.İşTürleri.TabStop = false;
            this.İşTürleri.Text = "İş Türleri";
            // 
            // İştürü_SeçiliSatıraKopyala
            // 
            this.İştürü_SeçiliSatıraKopyala.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.İştürü_SeçiliSatıraKopyala.Enabled = false;
            this.İştürü_SeçiliSatıraKopyala.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.İştürü_SeçiliSatıraKopyala.Location = new System.Drawing.Point(9, 214);
            this.İştürü_SeçiliSatıraKopyala.Margin = new System.Windows.Forms.Padding(2);
            this.İştürü_SeçiliSatıraKopyala.Name = "İştürü_SeçiliSatıraKopyala";
            this.İştürü_SeçiliSatıraKopyala.Size = new System.Drawing.Size(249, 29);
            this.İştürü_SeçiliSatıraKopyala.TabIndex = 8;
            this.İştürü_SeçiliSatıraKopyala.Text = "Seçili Satıra Kopyala";
            this.İştürü_SeçiliSatıraKopyala.UseVisualStyleBackColor = true;
            this.İştürü_SeçiliSatıraKopyala.Click += new System.EventHandler(this.İştürü_SeçiliSatıraKopyala_Click);
            // 
            // İşTürleri_SeçimKutusu
            // 
            this.İşTürleri_SeçimKutusu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.İşTürleri_SeçimKutusu.FormattingEnabled = true;
            this.İşTürleri_SeçimKutusu.ItemHeight = 20;
            this.İşTürleri_SeçimKutusu.Location = new System.Drawing.Point(9, 57);
            this.İşTürleri_SeçimKutusu.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.İşTürleri_SeçimKutusu.Name = "İşTürleri_SeçimKutusu";
            this.İşTürleri_SeçimKutusu.Size = new System.Drawing.Size(250, 144);
            this.İşTürleri_SeçimKutusu.Sorted = true;
            this.İşTürleri_SeçimKutusu.TabIndex = 7;
            this.İşTürleri_SeçimKutusu.SelectedIndexChanged += new System.EventHandler(this.İşTürleri_SeçimKutusu_SelectedIndexChanged);
            this.İşTürleri_SeçimKutusu.DoubleClick += new System.EventHandler(this.İştürü_SeçiliSatıraKopyala_Click);
            this.İşTürleri_SeçimKutusu.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.İşTürleri_SeçimKutusu_KeyPress);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(700, 7);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 20);
            this.label6.TabIndex = 18;
            this.label6.Text = "İskonto %";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 7);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 20);
            this.label7.TabIndex = 20;
            this.label7.Text = "Notlar";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.Müşteriler_Grup);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.Hastalar_Grup);
            this.splitContainer1.Size = new System.Drawing.Size(907, 106);
            this.splitContainer1.SplitterDistance = 453;
            this.splitContainer1.TabIndex = 23;
            // 
            // Ayraç_Kat_3_SolSağ
            // 
            this.Ayraç_Kat_3_SolSağ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Ayraç_Kat_3_SolSağ.Location = new System.Drawing.Point(0, 0);
            this.Ayraç_Kat_3_SolSağ.Name = "Ayraç_Kat_3_SolSağ";
            // 
            // Ayraç_Kat_3_SolSağ.Panel1
            // 
            this.Ayraç_Kat_3_SolSağ.Panel1.Controls.Add(this.İşTürleri);
            // 
            // Ayraç_Kat_3_SolSağ.Panel2
            // 
            this.Ayraç_Kat_3_SolSağ.Panel2.Controls.Add(this.P_DosyaEkleri);
            this.Ayraç_Kat_3_SolSağ.Panel2.Controls.Add(this.DosyaEkleri);
            this.Ayraç_Kat_3_SolSağ.Panel2.Controls.Add(this.Tablo);
            this.Ayraç_Kat_3_SolSağ.Panel2.Controls.Add(this.Kaydet);
            this.Ayraç_Kat_3_SolSağ.Panel2.Controls.Add(this.Seçili_Satırı_Sil);
            this.Ayraç_Kat_3_SolSağ.Size = new System.Drawing.Size(907, 247);
            this.Ayraç_Kat_3_SolSağ.SplitterDistance = 271;
            this.Ayraç_Kat_3_SolSağ.TabIndex = 24;
            // 
            // P_DosyaEkleri
            // 
            this.P_DosyaEkleri.Controls.Add(this.P_DosyaEkleri_Ayraç_SolSağ);
            this.P_DosyaEkleri.Location = new System.Drawing.Point(165, 106);
            this.P_DosyaEkleri.Name = "P_DosyaEkleri";
            this.P_DosyaEkleri.Size = new System.Drawing.Size(307, 103);
            this.P_DosyaEkleri.TabIndex = 13;
            this.P_DosyaEkleri.Visible = false;
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
            this.P_DosyaEkleri_Ayraç_SolSağ.Panel1.Controls.Add(this.P_DosyaEkleri_Sil);
            this.P_DosyaEkleri_Ayraç_SolSağ.Panel1.Controls.Add(this.P_DosyaEkleri_Geri);
            this.P_DosyaEkleri_Ayraç_SolSağ.Size = new System.Drawing.Size(307, 103);
            this.P_DosyaEkleri_Ayraç_SolSağ.SplitterDistance = 64;
            this.P_DosyaEkleri_Ayraç_SolSağ.TabIndex = 1;
            // 
            // P_DosyaEkleri_Liste
            // 
            this.P_DosyaEkleri_Liste.Dock = System.Windows.Forms.DockStyle.Fill;
            this.P_DosyaEkleri_Liste.FormattingEnabled = true;
            this.P_DosyaEkleri_Liste.ItemHeight = 20;
            this.P_DosyaEkleri_Liste.Location = new System.Drawing.Point(0, 0);
            this.P_DosyaEkleri_Liste.Name = "P_DosyaEkleri_Liste";
            this.P_DosyaEkleri_Liste.Size = new System.Drawing.Size(64, 45);
            this.P_DosyaEkleri_Liste.TabIndex = 0;
            this.P_DosyaEkleri_Liste.SelectedIndexChanged += new System.EventHandler(this.P_DosyaEkleri_Liste_SelectedIndexChanged);
            // 
            // P_DosyaEkleri_Sil
            // 
            this.P_DosyaEkleri_Sil.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.P_DosyaEkleri_Sil.Enabled = false;
            this.P_DosyaEkleri_Sil.Image = global::İş_ve_Depo_Takip.Properties.Resources.sil;
            this.P_DosyaEkleri_Sil.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.P_DosyaEkleri_Sil.Location = new System.Drawing.Point(0, 45);
            this.P_DosyaEkleri_Sil.Margin = new System.Windows.Forms.Padding(0);
            this.P_DosyaEkleri_Sil.Name = "P_DosyaEkleri_Sil";
            this.P_DosyaEkleri_Sil.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.P_DosyaEkleri_Sil.Size = new System.Drawing.Size(64, 29);
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
            this.P_DosyaEkleri_Geri.Location = new System.Drawing.Point(0, 74);
            this.P_DosyaEkleri_Geri.Margin = new System.Windows.Forms.Padding(2);
            this.P_DosyaEkleri_Geri.Name = "P_DosyaEkleri_Geri";
            this.P_DosyaEkleri_Geri.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.P_DosyaEkleri_Geri.Size = new System.Drawing.Size(64, 29);
            this.P_DosyaEkleri_Geri.TabIndex = 11;
            this.P_DosyaEkleri_Geri.Text = "Geri";
            this.P_DosyaEkleri_Geri.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.İpUcu_Genel.SetToolTip(this.P_DosyaEkleri_Geri, "Esc tuşu");
            this.P_DosyaEkleri_Geri.UseVisualStyleBackColor = true;
            this.P_DosyaEkleri_Geri.Click += new System.EventHandler(this.P_DosyaEkleri_Geri_Click);
            // 
            // DosyaEkleri
            // 
            this.DosyaEkleri.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.DosyaEkleri.Enabled = false;
            this.DosyaEkleri.Image = global::İş_ve_Depo_Takip.Properties.Resources.sol_mavi;
            this.DosyaEkleri.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.DosyaEkleri.Location = new System.Drawing.Point(217, 214);
            this.DosyaEkleri.Margin = new System.Windows.Forms.Padding(2);
            this.DosyaEkleri.Name = "DosyaEkleri";
            this.DosyaEkleri.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.DosyaEkleri.Size = new System.Drawing.Size(190, 29);
            this.DosyaEkleri.TabIndex = 12;
            this.DosyaEkleri.Text = "Dosya Ekleri (0)";
            this.DosyaEkleri.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.DosyaEkleri.UseVisualStyleBackColor = true;
            this.DosyaEkleri.Click += new System.EventHandler(this.DosyaEkleri_Click);
            // 
            // Ayraç_Kat_2_3
            // 
            this.Ayraç_Kat_2_3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Ayraç_Kat_2_3.Location = new System.Drawing.Point(0, 0);
            this.Ayraç_Kat_2_3.Name = "Ayraç_Kat_2_3";
            this.Ayraç_Kat_2_3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // Ayraç_Kat_2_3.Panel1
            // 
            this.Ayraç_Kat_2_3.Panel1.Controls.Add(this.SeriNoGörseli);
            this.Ayraç_Kat_2_3.Panel1.Controls.Add(this.Notlar);
            this.Ayraç_Kat_2_3.Panel1.Controls.Add(this.label7);
            this.Ayraç_Kat_2_3.Panel1.Controls.Add(this.İskonto);
            this.Ayraç_Kat_2_3.Panel1.Controls.Add(this.label6);
            // 
            // Ayraç_Kat_2_3.Panel2
            // 
            this.Ayraç_Kat_2_3.Panel2.Controls.Add(this.Ayraç_Kat_3_SolSağ);
            this.Ayraç_Kat_2_3.Size = new System.Drawing.Size(907, 328);
            this.Ayraç_Kat_2_3.SplitterDistance = 77;
            this.Ayraç_Kat_2_3.TabIndex = 25;
            // 
            // SeriNoGörseli
            // 
            this.SeriNoGörseli.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.SeriNoGörseli.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.SeriNoGörseli.Location = new System.Drawing.Point(387, 5);
            this.SeriNoGörseli.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.SeriNoGörseli.Name = "SeriNoGörseli";
            this.SeriNoGörseli.Size = new System.Drawing.Size(129, 24);
            this.SeriNoGörseli.TabIndex = 21;
            this.SeriNoGörseli.Text = "A889999";
            this.SeriNoGörseli.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.SeriNoGörseli.Visible = false;
            // 
            // Ayraç_Kat_1_2
            // 
            this.Ayraç_Kat_1_2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Ayraç_Kat_1_2.Location = new System.Drawing.Point(0, 0);
            this.Ayraç_Kat_1_2.Name = "Ayraç_Kat_1_2";
            this.Ayraç_Kat_1_2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // Ayraç_Kat_1_2.Panel1
            // 
            this.Ayraç_Kat_1_2.Panel1.Controls.Add(this.splitContainer1);
            // 
            // Ayraç_Kat_1_2.Panel2
            // 
            this.Ayraç_Kat_1_2.Panel2.Controls.Add(this.Ayraç_Kat_2_3);
            this.Ayraç_Kat_1_2.Size = new System.Drawing.Size(907, 438);
            this.Ayraç_Kat_1_2.SplitterDistance = 106;
            this.Ayraç_Kat_1_2.TabIndex = 26;
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
            // Yeni_İş_Girişi
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(907, 438);
            this.Controls.Add(this.Ayraç_Kat_1_2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Yeni_İş_Girişi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Yeni İş Girişi / Düzenleme";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Yeni_İş_Girişi_FormClosed);
            this.Shown += new System.EventHandler(this.Yeni_İş_Girişi_Shown);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Yeni_İş_Girişi_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Yeni_İş_Girişi_DragEnter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Yeni_İş_Girişi_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.Tablo)).EndInit();
            this.Müşteriler_Grup.ResumeLayout(false);
            this.Müşteriler_Grup.PerformLayout();
            this.Hastalar_Grup.ResumeLayout(false);
            this.Hastalar_Grup.PerformLayout();
            this.İşTürleri.ResumeLayout(false);
            this.İşTürleri.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.Ayraç_Kat_3_SolSağ.Panel1.ResumeLayout(false);
            this.Ayraç_Kat_3_SolSağ.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Ayraç_Kat_3_SolSağ)).EndInit();
            this.Ayraç_Kat_3_SolSağ.ResumeLayout(false);
            this.P_DosyaEkleri.ResumeLayout(false);
            this.P_DosyaEkleri_Ayraç_SolSağ.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.P_DosyaEkleri_Ayraç_SolSağ)).EndInit();
            this.P_DosyaEkleri_Ayraç_SolSağ.ResumeLayout(false);
            this.Ayraç_Kat_2_3.Panel1.ResumeLayout(false);
            this.Ayraç_Kat_2_3.Panel1.PerformLayout();
            this.Ayraç_Kat_2_3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Ayraç_Kat_2_3)).EndInit();
            this.Ayraç_Kat_2_3.ResumeLayout(false);
            this.Ayraç_Kat_1_2.Panel1.ResumeLayout(false);
            this.Ayraç_Kat_1_2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Ayraç_Kat_1_2)).EndInit();
            this.Ayraç_Kat_1_2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox Notlar;
        private System.Windows.Forms.DataGridView Tablo;
        private System.Windows.Forms.Button Kaydet;
        private System.Windows.Forms.Button Seçili_Satırı_Sil;
        private System.Windows.Forms.TextBox İskonto;
        private System.Windows.Forms.ToolTip İpUcu_Ücretlendirme;
        private System.Windows.Forms.GroupBox Müşteriler_Grup;
        private System.Windows.Forms.TextBox Müşteriler_AramaÇubuğu;
        private System.Windows.Forms.GroupBox Hastalar_Grup;
        private System.Windows.Forms.TextBox Hastalar_AramaÇubuğu;
        private System.Windows.Forms.GroupBox İşTürleri;
        private System.Windows.Forms.TextBox İşTürleri_AramaÇubuğu;
        private System.Windows.Forms.ListBox İşTürleri_SeçimKutusu;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button İştürü_SeçiliSatıraKopyala;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer Ayraç_Kat_3_SolSağ;
        private System.Windows.Forms.SplitContainer Ayraç_Kat_2_3;
        private System.Windows.Forms.SplitContainer Ayraç_Kat_1_2;
        private System.Windows.Forms.ListBox Müşteriler_SeçimKutusu;
        private System.Windows.Forms.ListBox Hastalar_SeçimKutusu;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tablo_İş_Türü;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tablo_Ücret;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tablo_Giriş_Tarihi;
        private System.Windows.Forms.DataGridViewButtonColumn Tablo_Çıkış_Tarihi;
        private System.Windows.Forms.CheckBox Hastalar_AdVeSoyadıDüzelt;
        private System.Windows.Forms.ToolTip İpUcu_Genel;
        private System.Windows.Forms.Button DosyaEkleri;
        private System.Windows.Forms.Panel P_DosyaEkleri;
        private System.Windows.Forms.SplitContainer P_DosyaEkleri_Ayraç_SolSağ;
        private System.Windows.Forms.ListBox P_DosyaEkleri_Liste;
        private System.Windows.Forms.Button P_DosyaEkleri_Geri;
        private System.Windows.Forms.Button P_DosyaEkleri_Sil;
        private System.Windows.Forms.Label SeriNoGörseli;
    }
}