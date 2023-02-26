namespace İş_ve_Depo_Takip.Ekranlar
{
    partial class İş_Türleri
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Liste = new System.Windows.Forms.ListBox();
            this.Yeni = new System.Windows.Forms.TextBox();
            this.Ekle = new System.Windows.Forms.Button();
            this.Kaydet = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.AramaÇubuğu = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.MüşteriyeGösterilecekOlanAdı = new System.Windows.Forms.TextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.Malzeme_SeçiliSatıraKopyala = new System.Windows.Forms.Button();
            this.Malzeme_AramaÇubuğu = new System.Windows.Forms.TextBox();
            this.Malzeme_SeçimKutusu = new System.Windows.Forms.ListBox();
            this.Tablo = new System.Windows.Forms.DataGridView();
            this.Tablo_Malzeme = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tablo_Miktar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tablo_Biim = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Notlar = new System.Windows.Forms.TextBox();
            this.İpUcu = new System.Windows.Forms.ToolTip(this.components);
            this.SağTuşMenü = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SağTuşMenü_YenidenAdlandır = new System.Windows.Forms.ToolStripMenuItem();
            this.SağTuşMenü_Sil = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tablo)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SağTuşMenü.SuspendLayout();
            this.SuspendLayout();
            // 
            // Liste
            // 
            this.Liste.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Liste.ContextMenuStrip = this.SağTuşMenü;
            this.Liste.FormattingEnabled = true;
            this.Liste.ItemHeight = 25;
            this.Liste.Location = new System.Drawing.Point(12, 50);
            this.Liste.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Liste.Name = "Liste";
            this.Liste.Size = new System.Drawing.Size(300, 379);
            this.Liste.Sorted = true;
            this.Liste.TabIndex = 0;
            this.Liste.SelectedValueChanged += new System.EventHandler(this.Liste_SelectedValueChanged);
            // 
            // Yeni
            // 
            this.Yeni.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Yeni.Location = new System.Drawing.Point(12, 441);
            this.Yeni.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Yeni.Name = "Yeni";
            this.Yeni.Size = new System.Drawing.Size(300, 30);
            this.Yeni.TabIndex = 2;
            this.Yeni.TextChanged += new System.EventHandler(this.Yeni_TextChanged);
            // 
            // Ekle
            // 
            this.Ekle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Ekle.Enabled = false;
            this.Ekle.Image = global::İş_ve_Depo_Takip.Properties.Resources.sag;
            this.Ekle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Ekle.Location = new System.Drawing.Point(12, 478);
            this.Ekle.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Ekle.Name = "Ekle";
            this.Ekle.Size = new System.Drawing.Size(299, 45);
            this.Ekle.TabIndex = 1;
            this.Ekle.Text = "Ekle";
            this.Ekle.UseVisualStyleBackColor = true;
            this.Ekle.Click += new System.EventHandler(this.Ekle_Click);
            // 
            // Kaydet
            // 
            this.Kaydet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Kaydet.Enabled = false;
            this.Kaydet.Image = global::İş_ve_Depo_Takip.Properties.Resources.sag;
            this.Kaydet.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Kaydet.Location = new System.Drawing.Point(12, 478);
            this.Kaydet.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Kaydet.Name = "Kaydet";
            this.Kaydet.Size = new System.Drawing.Size(796, 45);
            this.Kaydet.TabIndex = 4;
            this.Kaydet.Text = "Kaydet";
            this.İpUcu.SetToolTip(this.Kaydet, "Silinmek istenen malzemenin miktarı 0 yazılmalıdır");
            this.Kaydet.UseVisualStyleBackColor = true;
            this.Kaydet.Click += new System.EventHandler(this.Kaydet_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.AramaÇubuğu);
            this.splitContainer1.Panel1.Controls.Add(this.Liste);
            this.splitContainer1.Panel1.Controls.Add(this.Yeni);
            this.splitContainer1.Panel1.Controls.Add(this.Ekle);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.MüşteriyeGösterilecekOlanAdı);
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel2.Controls.Add(this.Kaydet);
            this.splitContainer1.Panel2.Enabled = false;
            this.splitContainer1.Size = new System.Drawing.Size(1136, 533);
            this.splitContainer1.SplitterDistance = 320;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 5;
            // 
            // AramaÇubuğu
            // 
            this.AramaÇubuğu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AramaÇubuğu.Location = new System.Drawing.Point(12, 12);
            this.AramaÇubuğu.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.AramaÇubuğu.Name = "AramaÇubuğu";
            this.AramaÇubuğu.Size = new System.Drawing.Size(300, 30);
            this.AramaÇubuğu.TabIndex = 3;
            this.İpUcu.SetToolTip(this.AramaÇubuğu, "Arama çubuğu");
            this.AramaÇubuğu.TextChanged += new System.EventHandler(this.AramaÇubuğu_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(279, 25);
            this.label1.TabIndex = 9;
            this.label1.Text = "Müşteriye gösterilecek olan adı";
            // 
            // MüşteriyeGösterilecekOlanAdı
            // 
            this.MüşteriyeGösterilecekOlanAdı.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MüşteriyeGösterilecekOlanAdı.Location = new System.Drawing.Point(311, 12);
            this.MüşteriyeGösterilecekOlanAdı.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MüşteriyeGösterilecekOlanAdı.Name = "MüşteriyeGösterilecekOlanAdı";
            this.MüşteriyeGösterilecekOlanAdı.Size = new System.Drawing.Size(498, 30);
            this.MüşteriyeGösterilecekOlanAdı.TabIndex = 8;
            this.İpUcu.SetToolTip(this.MüşteriyeGösterilecekOlanAdı, "Arama çubuğu");
            this.MüşteriyeGösterilecekOlanAdı.TextChanged += new System.EventHandler(this.Ayar_Değişti);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.Location = new System.Drawing.Point(12, 50);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer2.Size = new System.Drawing.Size(796, 422);
            this.splitContainer2.SplitterDistance = 318;
            this.splitContainer2.SplitterWidth = 3;
            this.splitContainer2.TabIndex = 7;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.groupBox4);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.Tablo);
            this.splitContainer3.Size = new System.Drawing.Size(796, 318);
            this.splitContainer3.SplitterDistance = 325;
            this.splitContainer3.SplitterWidth = 3;
            this.splitContainer3.TabIndex = 1;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.Malzeme_SeçiliSatıraKopyala);
            this.groupBox4.Controls.Add(this.Malzeme_AramaÇubuğu);
            this.groupBox4.Controls.Add(this.Malzeme_SeçimKutusu);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(325, 318);
            this.groupBox4.TabIndex = 15;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Malzemeler";
            // 
            // Malzeme_SeçiliSatıraKopyala
            // 
            this.Malzeme_SeçiliSatıraKopyala.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Malzeme_SeçiliSatıraKopyala.Enabled = false;
            this.Malzeme_SeçiliSatıraKopyala.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Malzeme_SeçiliSatıraKopyala.Location = new System.Drawing.Point(9, 282);
            this.Malzeme_SeçiliSatıraKopyala.Margin = new System.Windows.Forms.Padding(2);
            this.Malzeme_SeçiliSatıraKopyala.Name = "Malzeme_SeçiliSatıraKopyala";
            this.Malzeme_SeçiliSatıraKopyala.Size = new System.Drawing.Size(304, 31);
            this.Malzeme_SeçiliSatıraKopyala.TabIndex = 22;
            this.Malzeme_SeçiliSatıraKopyala.Text = "Seçili Satıra Kopyala";
            this.Malzeme_SeçiliSatıraKopyala.UseVisualStyleBackColor = true;
            this.Malzeme_SeçiliSatıraKopyala.Click += new System.EventHandler(this.Malzeme_SeçiliSatıraKopyala_Click);
            // 
            // Malzeme_AramaÇubuğu
            // 
            this.Malzeme_AramaÇubuğu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Malzeme_AramaÇubuğu.Location = new System.Drawing.Point(9, 27);
            this.Malzeme_AramaÇubuğu.Name = "Malzeme_AramaÇubuğu";
            this.Malzeme_AramaÇubuğu.Size = new System.Drawing.Size(305, 30);
            this.Malzeme_AramaÇubuğu.TabIndex = 5;
            this.İpUcu.SetToolTip(this.Malzeme_AramaÇubuğu, "Arama çubuğu");
            this.Malzeme_AramaÇubuğu.TextChanged += new System.EventHandler(this.Malzeme_TextChanged);
            // 
            // Malzeme_SeçimKutusu
            // 
            this.Malzeme_SeçimKutusu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Malzeme_SeçimKutusu.FormattingEnabled = true;
            this.Malzeme_SeçimKutusu.ItemHeight = 25;
            this.Malzeme_SeçimKutusu.Location = new System.Drawing.Point(9, 64);
            this.Malzeme_SeçimKutusu.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Malzeme_SeçimKutusu.Name = "Malzeme_SeçimKutusu";
            this.Malzeme_SeçimKutusu.Size = new System.Drawing.Size(305, 204);
            this.Malzeme_SeçimKutusu.Sorted = true;
            this.Malzeme_SeçimKutusu.TabIndex = 4;
            this.Malzeme_SeçimKutusu.SelectedIndexChanged += new System.EventHandler(this.Malzeme_SeçimKutusu_SelectedValueChanged);
            this.Malzeme_SeçimKutusu.DoubleClick += new System.EventHandler(this.Malzeme_SeçiliSatıraKopyala_Click);
            // 
            // Tablo
            // 
            this.Tablo.AllowUserToResizeColumns = false;
            this.Tablo.AllowUserToResizeRows = false;
            this.Tablo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.Tablo.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tablo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle16;
            this.Tablo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tablo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Tablo_Malzeme,
            this.Tablo_Miktar,
            this.Tablo_Biim});
            this.Tablo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tablo.Location = new System.Drawing.Point(0, 0);
            this.Tablo.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Tablo.MultiSelect = false;
            this.Tablo.Name = "Tablo";
            this.Tablo.RowHeadersVisible = false;
            this.Tablo.RowHeadersWidth = 51;
            this.Tablo.RowTemplate.Height = 24;
            this.Tablo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Tablo.Size = new System.Drawing.Size(468, 318);
            this.Tablo.TabIndex = 0;
            this.İpUcu.SetToolTip(this.Tablo, "Silinmek istenen malzemenin miktarı 0 yazılmalıdır");
            this.Tablo.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tablo_CellValueChanged);
            // 
            // Tablo_Malzeme
            // 
            this.Tablo_Malzeme.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Tablo_Malzeme.HeaderText = "Malzeme";
            this.Tablo_Malzeme.MinimumWidth = 6;
            this.Tablo_Malzeme.Name = "Tablo_Malzeme";
            this.Tablo_Malzeme.ReadOnly = true;
            this.Tablo_Malzeme.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Tablo_Miktar
            // 
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Tablo_Miktar.DefaultCellStyle = dataGridViewCellStyle17;
            this.Tablo_Miktar.HeaderText = "Miktarı";
            this.Tablo_Miktar.MinimumWidth = 6;
            this.Tablo_Miktar.Name = "Tablo_Miktar";
            this.Tablo_Miktar.Width = 98;
            // 
            // Tablo_Biim
            // 
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Tablo_Biim.DefaultCellStyle = dataGridViewCellStyle18;
            this.Tablo_Biim.HeaderText = "Birimi";
            this.Tablo_Biim.MinimumWidth = 6;
            this.Tablo_Biim.Name = "Tablo_Biim";
            this.Tablo_Biim.ReadOnly = true;
            this.Tablo_Biim.Width = 88;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Notlar);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox2.Size = new System.Drawing.Size(796, 101);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Notlar";
            // 
            // Notlar
            // 
            this.Notlar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Notlar.Location = new System.Drawing.Point(2, 26);
            this.Notlar.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Notlar.Multiline = true;
            this.Notlar.Name = "Notlar";
            this.Notlar.Size = new System.Drawing.Size(792, 72);
            this.Notlar.TabIndex = 6;
            this.İpUcu.SetToolTip(this.Notlar, "Silinmek istenen malzemenin miktarı 0 yazılmalıdır");
            this.Notlar.TextChanged += new System.EventHandler(this.Ayar_Değişti);
            // 
            // İpUcu
            // 
            this.İpUcu.AutomaticDelay = 0;
            this.İpUcu.AutoPopDelay = 0;
            this.İpUcu.InitialDelay = 0;
            this.İpUcu.ReshowDelay = 0;
            this.İpUcu.ShowAlways = true;
            this.İpUcu.UseAnimation = false;
            this.İpUcu.UseFading = false;
            // 
            // SağTuşMenü
            // 
            this.SağTuşMenü.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.SağTuşMenü.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SağTuşMenü_YenidenAdlandır,
            this.SağTuşMenü_Sil});
            this.SağTuşMenü.Name = "SağTuşMenü";
            this.SağTuşMenü.ShowImageMargin = false;
            this.SağTuşMenü.Size = new System.Drawing.Size(186, 80);
            // 
            // SağTuşMenü_YenidenAdlandır
            // 
            this.SağTuşMenü_YenidenAdlandır.Name = "SağTuşMenü_YenidenAdlandır";
            this.SağTuşMenü_YenidenAdlandır.Size = new System.Drawing.Size(185, 24);
            this.SağTuşMenü_YenidenAdlandır.Text = "Yeniden Adlandır";
            this.SağTuşMenü_YenidenAdlandır.ToolTipText = "Ödendi olarak işaretlenen işler HARİÇ";
            this.SağTuşMenü_YenidenAdlandır.Click += new System.EventHandler(this.SağTuşMenü_YenidenAdlandır_Click);
            // 
            // SağTuşMenü_Sil
            // 
            this.SağTuşMenü_Sil.Name = "SağTuşMenü_Sil";
            this.SağTuşMenü_Sil.Size = new System.Drawing.Size(185, 24);
            this.SağTuşMenü_Sil.Text = "Sil";
            this.SağTuşMenü_Sil.Click += new System.EventHandler(this.SağTuşMenü_Sil_Click);
            // 
            // İş_Türleri
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1136, 533);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "İş_Türleri";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "İş Türleri";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tablo)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.SağTuşMenü.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox Liste;
        private System.Windows.Forms.TextBox Yeni;
        private System.Windows.Forms.Button Ekle;
        private System.Windows.Forms.Button Kaydet;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox Notlar;
        private System.Windows.Forms.ToolTip İpUcu;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView Tablo;
        private System.Windows.Forms.TextBox AramaÇubuğu;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button Malzeme_SeçiliSatıraKopyala;
        private System.Windows.Forms.TextBox Malzeme_AramaÇubuğu;
        private System.Windows.Forms.ListBox Malzeme_SeçimKutusu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox MüşteriyeGösterilecekOlanAdı;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tablo_Malzeme;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tablo_Miktar;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tablo_Biim;
        private System.Windows.Forms.ContextMenuStrip SağTuşMenü;
        private System.Windows.Forms.ToolStripMenuItem SağTuşMenü_YenidenAdlandır;
        private System.Windows.Forms.ToolStripMenuItem SağTuşMenü_Sil;
    }
}