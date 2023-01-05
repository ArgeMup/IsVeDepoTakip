using System;

namespace İş_ve_Depo_Takip
{
    partial class Yeni_Talep_Girişi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Yeni_Talep_Girişi));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Notlar = new System.Windows.Forms.TextBox();
            this.Tablo = new System.Windows.Forms.DataGridView();
            this.Tablo_İş_Türü = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tablo_Ücret = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tablo_Giriş_Tarihi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kaydet = new System.Windows.Forms.Button();
            this.Seçili_Satırı_Sil = new System.Windows.Forms.Button();
            this.İskonto = new System.Windows.Forms.TextBox();
            this.İpUcu = new System.Windows.Forms.ToolTip(this.components);
            this.Müşteriler_AramaÇubuğu = new System.Windows.Forms.TextBox();
            this.Hastalar_AramaÇubuğu = new System.Windows.Forms.TextBox();
            this.İşTürleri_AramaÇubuğu = new System.Windows.Forms.TextBox();
            this.Müşteriler_Grup = new System.Windows.Forms.GroupBox();
            this.Müşteriler_SeçimKutusu = new System.Windows.Forms.ComboBox();
            this.Hastalar_Grup = new System.Windows.Forms.GroupBox();
            this.Hastalar_SeçimKutusu = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.İştürü_SeçiliSatıraKopyala = new System.Windows.Forms.Button();
            this.İşTürleri_SeçimKutusu = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.Ayraç_Kat_2_3 = new System.Windows.Forms.SplitContainer();
            this.Ayraç_Kat_1_2 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.Tablo)).BeginInit();
            this.Müşteriler_Grup.SuspendLayout();
            this.Hastalar_Grup.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
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
            this.Notlar.Location = new System.Drawing.Point(4, 44);
            this.Notlar.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Notlar.Multiline = true;
            this.Notlar.Name = "Notlar";
            this.Notlar.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.Notlar.Size = new System.Drawing.Size(838, 55);
            this.Notlar.TabIndex = 5;
            this.İpUcu.SetToolTip(this.Notlar, resources.GetString("Notlar.ToolTip"));
            this.Notlar.TextChanged += new System.EventHandler(this.Değişiklik_Yapılıyor);
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
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tablo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Tablo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tablo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Tablo_İş_Türü,
            this.Tablo_Ücret,
            this.Tablo_Giriş_Tarihi});
            this.Tablo.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.Tablo.Location = new System.Drawing.Point(4, 4);
            this.Tablo.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
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
            this.Tablo.Size = new System.Drawing.Size(570, 271);
            this.Tablo.TabIndex = 9;
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
            this.Tablo_Giriş_Tarihi.HeaderText = "Giriş Tarihi";
            this.Tablo_Giriş_Tarihi.MinimumWidth = 6;
            this.Tablo_Giriş_Tarihi.Name = "Tablo_Giriş_Tarihi";
            this.Tablo_Giriş_Tarihi.ReadOnly = true;
            this.Tablo_Giriş_Tarihi.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Tablo_Giriş_Tarihi.Width = 111;
            // 
            // Kaydet
            // 
            this.Kaydet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Kaydet.Enabled = false;
            this.Kaydet.Image = global::İş_ve_Depo_Takip.Properties.Resources.sag;
            this.Kaydet.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Kaydet.Location = new System.Drawing.Point(288, 281);
            this.Kaydet.Margin = new System.Windows.Forms.Padding(2);
            this.Kaydet.Name = "Kaydet";
            this.Kaydet.Size = new System.Drawing.Size(280, 36);
            this.Kaydet.TabIndex = 11;
            this.Kaydet.Text = "Kaydet";
            this.Kaydet.UseVisualStyleBackColor = true;
            this.Kaydet.Click += new System.EventHandler(this.Kaydet_Click);
            // 
            // Seçili_Satırı_Sil
            // 
            this.Seçili_Satırı_Sil.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Seçili_Satırı_Sil.Image = global::İş_ve_Depo_Takip.Properties.Resources.sil;
            this.Seçili_Satırı_Sil.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Seçili_Satırı_Sil.Location = new System.Drawing.Point(13, 281);
            this.Seçili_Satırı_Sil.Margin = new System.Windows.Forms.Padding(2);
            this.Seçili_Satırı_Sil.Name = "Seçili_Satırı_Sil";
            this.Seçili_Satırı_Sil.Size = new System.Drawing.Size(280, 36);
            this.Seçili_Satırı_Sil.TabIndex = 10;
            this.Seçili_Satırı_Sil.Text = "Seçili Satırı Sil";
            this.Seçili_Satırı_Sil.UseVisualStyleBackColor = true;
            this.Seçili_Satırı_Sil.Click += new System.EventHandler(this.Seçili_Satırı_Sil_Click);
            // 
            // İskonto
            // 
            this.İskonto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.İskonto.Location = new System.Drawing.Point(706, 6);
            this.İskonto.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.İskonto.Name = "İskonto";
            this.İskonto.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.İskonto.Size = new System.Drawing.Size(117, 30);
            this.İskonto.TabIndex = 4;
            this.İskonto.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.İskonto.TextChanged += new System.EventHandler(this.Değişiklik_Yapılıyor);
            // 
            // İpUcu
            // 
            this.İpUcu.AutomaticDelay = 0;
            this.İpUcu.AutoPopDelay = 0;
            this.İpUcu.InitialDelay = 0;
            this.İpUcu.IsBalloon = true;
            this.İpUcu.ReshowDelay = 0;
            this.İpUcu.ShowAlways = true;
            this.İpUcu.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.İpUcu.ToolTipTitle = "Ücretlendirme";
            this.İpUcu.UseAnimation = false;
            this.İpUcu.UseFading = false;
            // 
            // Müşteriler_AramaÇubuğu
            // 
            this.Müşteriler_AramaÇubuğu.Dock = System.Windows.Forms.DockStyle.Top;
            this.Müşteriler_AramaÇubuğu.Location = new System.Drawing.Point(4, 27);
            this.Müşteriler_AramaÇubuğu.Margin = new System.Windows.Forms.Padding(4);
            this.Müşteriler_AramaÇubuğu.Name = "Müşteriler_AramaÇubuğu";
            this.Müşteriler_AramaÇubuğu.Size = new System.Drawing.Size(415, 30);
            this.Müşteriler_AramaÇubuğu.TabIndex = 0;
            this.İpUcu.SetToolTip(this.Müşteriler_AramaÇubuğu, "Arama çubuğu");
            this.Müşteriler_AramaÇubuğu.TextChanged += new System.EventHandler(this.Müşteriler_AramaÇubuğu_TextChanged);
            // 
            // Hastalar_AramaÇubuğu
            // 
            this.Hastalar_AramaÇubuğu.Dock = System.Windows.Forms.DockStyle.Top;
            this.Hastalar_AramaÇubuğu.Location = new System.Drawing.Point(4, 27);
            this.Hastalar_AramaÇubuğu.Margin = new System.Windows.Forms.Padding(4);
            this.Hastalar_AramaÇubuğu.Name = "Hastalar_AramaÇubuğu";
            this.Hastalar_AramaÇubuğu.Size = new System.Drawing.Size(410, 30);
            this.Hastalar_AramaÇubuğu.TabIndex = 2;
            this.İpUcu.SetToolTip(this.Hastalar_AramaÇubuğu, "Arama çubuğu");
            this.Hastalar_AramaÇubuğu.TextChanged += new System.EventHandler(this.Hastalar_AramaÇubuğu_TextChanged);
            // 
            // İşTürleri_AramaÇubuğu
            // 
            this.İşTürleri_AramaÇubuğu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.İşTürleri_AramaÇubuğu.Location = new System.Drawing.Point(11, 31);
            this.İşTürleri_AramaÇubuğu.Margin = new System.Windows.Forms.Padding(4);
            this.İşTürleri_AramaÇubuğu.Name = "İşTürleri_AramaÇubuğu";
            this.İşTürleri_AramaÇubuğu.Size = new System.Drawing.Size(228, 30);
            this.İşTürleri_AramaÇubuğu.TabIndex = 6;
            this.İpUcu.SetToolTip(this.İşTürleri_AramaÇubuğu, "Arama çubuğu");
            this.İşTürleri_AramaÇubuğu.TextChanged += new System.EventHandler(this.İşTürleri_AramaÇubuğu_TextChanged);
            // 
            // Müşteriler_Grup
            // 
            this.Müşteriler_Grup.Controls.Add(this.Müşteriler_SeçimKutusu);
            this.Müşteriler_Grup.Controls.Add(this.Müşteriler_AramaÇubuğu);
            this.Müşteriler_Grup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Müşteriler_Grup.Location = new System.Drawing.Point(0, 0);
            this.Müşteriler_Grup.Margin = new System.Windows.Forms.Padding(4);
            this.Müşteriler_Grup.Name = "Müşteriler_Grup";
            this.Müşteriler_Grup.Padding = new System.Windows.Forms.Padding(4);
            this.Müşteriler_Grup.Size = new System.Drawing.Size(423, 112);
            this.Müşteriler_Grup.TabIndex = 13;
            this.Müşteriler_Grup.TabStop = false;
            this.Müşteriler_Grup.Text = "Müşteri";
            // 
            // Müşteriler_SeçimKutusu
            // 
            this.Müşteriler_SeçimKutusu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Müşteriler_SeçimKutusu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Müşteriler_SeçimKutusu.FormattingEnabled = true;
            this.Müşteriler_SeçimKutusu.Location = new System.Drawing.Point(4, 65);
            this.Müşteriler_SeçimKutusu.Margin = new System.Windows.Forms.Padding(4);
            this.Müşteriler_SeçimKutusu.Name = "Müşteriler_SeçimKutusu";
            this.Müşteriler_SeçimKutusu.Size = new System.Drawing.Size(415, 33);
            this.Müşteriler_SeçimKutusu.Sorted = true;
            this.Müşteriler_SeçimKutusu.TabIndex = 1;
            this.Müşteriler_SeçimKutusu.SelectedIndexChanged += new System.EventHandler(this.Müşteriler_SeçimKutusu_SelectedIndexChanged);
            // 
            // Hastalar_Grup
            // 
            this.Hastalar_Grup.Controls.Add(this.Hastalar_SeçimKutusu);
            this.Hastalar_Grup.Controls.Add(this.Hastalar_AramaÇubuğu);
            this.Hastalar_Grup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Hastalar_Grup.Location = new System.Drawing.Point(0, 0);
            this.Hastalar_Grup.Margin = new System.Windows.Forms.Padding(4);
            this.Hastalar_Grup.Name = "Hastalar_Grup";
            this.Hastalar_Grup.Padding = new System.Windows.Forms.Padding(4);
            this.Hastalar_Grup.Size = new System.Drawing.Size(418, 112);
            this.Hastalar_Grup.TabIndex = 14;
            this.Hastalar_Grup.TabStop = false;
            this.Hastalar_Grup.Text = "Hasta";
            // 
            // Hastalar_SeçimKutusu
            // 
            this.Hastalar_SeçimKutusu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Hastalar_SeçimKutusu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Hastalar_SeçimKutusu.FormattingEnabled = true;
            this.Hastalar_SeçimKutusu.Location = new System.Drawing.Point(4, 65);
            this.Hastalar_SeçimKutusu.Margin = new System.Windows.Forms.Padding(4);
            this.Hastalar_SeçimKutusu.Name = "Hastalar_SeçimKutusu";
            this.Hastalar_SeçimKutusu.Size = new System.Drawing.Size(410, 33);
            this.Hastalar_SeçimKutusu.TabIndex = 3;
            this.Hastalar_SeçimKutusu.SelectedIndexChanged += new System.EventHandler(this.Hastalar_SeçimKutusu_SelectedIndexChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.İştürü_SeçiliSatıraKopyala);
            this.groupBox4.Controls.Add(this.İşTürleri_AramaÇubuğu);
            this.groupBox4.Controls.Add(this.İşTürleri_SeçimKutusu);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox4.Size = new System.Drawing.Size(254, 323);
            this.groupBox4.TabIndex = 14;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "İş Türleri";
            // 
            // İştürü_SeçiliSatıraKopyala
            // 
            this.İştürü_SeçiliSatıraKopyala.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.İştürü_SeçiliSatıraKopyala.Enabled = false;
            this.İştürü_SeçiliSatıraKopyala.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.İştürü_SeçiliSatıraKopyala.Location = new System.Drawing.Point(11, 281);
            this.İştürü_SeçiliSatıraKopyala.Margin = new System.Windows.Forms.Padding(2);
            this.İştürü_SeçiliSatıraKopyala.Name = "İştürü_SeçiliSatıraKopyala";
            this.İştürü_SeçiliSatıraKopyala.Size = new System.Drawing.Size(228, 36);
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
            this.İşTürleri_SeçimKutusu.ItemHeight = 25;
            this.İşTürleri_SeçimKutusu.Location = new System.Drawing.Point(11, 71);
            this.İşTürleri_SeçimKutusu.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.İşTürleri_SeçimKutusu.Name = "İşTürleri_SeçimKutusu";
            this.İşTürleri_SeçimKutusu.Size = new System.Drawing.Size(228, 204);
            this.İşTürleri_SeçimKutusu.Sorted = true;
            this.İşTürleri_SeçimKutusu.TabIndex = 7;
            this.İşTürleri_SeçimKutusu.SelectedIndexChanged += new System.EventHandler(this.İşTürleri_SeçimKutusu_SelectedIndexChanged);
            this.İşTürleri_SeçimKutusu.DoubleClick += new System.EventHandler(this.İştürü_SeçiliSatıraKopyala_Click);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(598, 9);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 25);
            this.label6.TabIndex = 18;
            this.label6.Text = "İskonto %";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 9);
            this.label7.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 25);
            this.label7.TabIndex = 20;
            this.label7.Text = "Notlar";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.Müşteriler_Grup);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.Hastalar_Grup);
            this.splitContainer1.Size = new System.Drawing.Size(846, 112);
            this.splitContainer1.SplitterDistance = 423;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 23;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox4);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.Tablo);
            this.splitContainer2.Panel2.Controls.Add(this.Kaydet);
            this.splitContainer2.Panel2.Controls.Add(this.Seçili_Satırı_Sil);
            this.splitContainer2.Size = new System.Drawing.Size(846, 323);
            this.splitContainer2.SplitterDistance = 254;
            this.splitContainer2.SplitterWidth = 5;
            this.splitContainer2.TabIndex = 24;
            // 
            // Ayraç_Kat_2_3
            // 
            this.Ayraç_Kat_2_3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Ayraç_Kat_2_3.Location = new System.Drawing.Point(0, 0);
            this.Ayraç_Kat_2_3.Margin = new System.Windows.Forms.Padding(4);
            this.Ayraç_Kat_2_3.Name = "Ayraç_Kat_2_3";
            this.Ayraç_Kat_2_3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // Ayraç_Kat_2_3.Panel1
            // 
            this.Ayraç_Kat_2_3.Panel1.Controls.Add(this.Notlar);
            this.Ayraç_Kat_2_3.Panel1.Controls.Add(this.label7);
            this.Ayraç_Kat_2_3.Panel1.Controls.Add(this.İskonto);
            this.Ayraç_Kat_2_3.Panel1.Controls.Add(this.label6);
            // 
            // Ayraç_Kat_2_3.Panel2
            // 
            this.Ayraç_Kat_2_3.Panel2.Controls.Add(this.splitContainer2);
            this.Ayraç_Kat_2_3.Size = new System.Drawing.Size(846, 431);
            this.Ayraç_Kat_2_3.SplitterDistance = 103;
            this.Ayraç_Kat_2_3.SplitterWidth = 5;
            this.Ayraç_Kat_2_3.TabIndex = 25;
            // 
            // Ayraç_Kat_1_2
            // 
            this.Ayraç_Kat_1_2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Ayraç_Kat_1_2.Location = new System.Drawing.Point(0, 0);
            this.Ayraç_Kat_1_2.Margin = new System.Windows.Forms.Padding(4);
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
            this.Ayraç_Kat_1_2.Size = new System.Drawing.Size(846, 548);
            this.Ayraç_Kat_1_2.SplitterDistance = 112;
            this.Ayraç_Kat_1_2.SplitterWidth = 5;
            this.Ayraç_Kat_1_2.TabIndex = 26;
            // 
            // Yeni_Talep_Girişi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(846, 548);
            this.Controls.Add(this.Ayraç_Kat_1_2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "Yeni_Talep_Girişi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Yeni İş Girişi / Düzenleme";
            ((System.ComponentModel.ISupportInitialize)(this.Tablo)).EndInit();
            this.Müşteriler_Grup.ResumeLayout(false);
            this.Müşteriler_Grup.PerformLayout();
            this.Hastalar_Grup.ResumeLayout(false);
            this.Hastalar_Grup.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
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
        private System.Windows.Forms.ToolTip İpUcu;
        private System.Windows.Forms.GroupBox Müşteriler_Grup;
        private System.Windows.Forms.TextBox Müşteriler_AramaÇubuğu;
        private System.Windows.Forms.GroupBox Hastalar_Grup;
        private System.Windows.Forms.TextBox Hastalar_AramaÇubuğu;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox İşTürleri_AramaÇubuğu;
        private System.Windows.Forms.ListBox İşTürleri_SeçimKutusu;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button İştürü_SeçiliSatıraKopyala;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer Ayraç_Kat_2_3;
        private System.Windows.Forms.SplitContainer Ayraç_Kat_1_2;
        private System.Windows.Forms.ComboBox Müşteriler_SeçimKutusu;
        private System.Windows.Forms.ComboBox Hastalar_SeçimKutusu;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tablo_İş_Türü;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tablo_Ücret;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tablo_Giriş_Tarihi;
    }
}