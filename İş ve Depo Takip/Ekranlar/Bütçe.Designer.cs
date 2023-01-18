namespace İş_ve_Depo_Takip.Ekranlar
{
    partial class Bütçe
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this._1_Gelir = new System.Windows.Forms.GroupBox();
            this._1_Gelir_ÖdemeTalepEdildi = new System.Windows.Forms.CheckBox();
            this._1_Gelir_TeslimEdildi = new System.Windows.Forms.CheckBox();
            this._1_Gelir_DevamEden = new System.Windows.Forms.CheckBox();
            this._1_Gider = new System.Windows.Forms.GroupBox();
            this._1_Gider_ÖdemeTalepEdildi = new System.Windows.Forms.CheckBox();
            this._1_Gider_DevamEden = new System.Windows.Forms.CheckBox();
            this._1_Gider_TeslimEdildi = new System.Windows.Forms.CheckBox();
            this._1_Tablo = new System.Windows.Forms.DataGridView();
            this._1_Tablo_Seç = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this._1_Tablo_Müşteri = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._1_Tablo_Gelir = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._1_Tablo_Gider = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._1_Tablo_Fark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sekmeler = new System.Windows.Forms.TabControl();
            this.Sekmeler_1 = new System.Windows.Forms.TabPage();
            this._1_AltToplam = new System.Windows.Forms.TextBox();
            this.Sekmeler_2 = new System.Windows.Forms.TabPage();
            this._2_Tablo = new System.Windows.Forms.DataGridView();
            this._2_Tablo_Seç = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this._2_Tablo_Açıklama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._2_Tablo_Gelir = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._2_Tablo_Gider = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._2_Tablo_Fark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._2_AltToplam = new System.Windows.Forms.TextBox();
            this.Kaydet = new System.Windows.Forms.Button();
            this._1_Gelir.SuspendLayout();
            this._1_Gider.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._1_Tablo)).BeginInit();
            this.Sekmeler.SuspendLayout();
            this.Sekmeler_1.SuspendLayout();
            this.Sekmeler_2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._2_Tablo)).BeginInit();
            this.SuspendLayout();
            // 
            // _1_Gelir
            // 
            this._1_Gelir.Controls.Add(this._1_Gelir_ÖdemeTalepEdildi);
            this._1_Gelir.Controls.Add(this._1_Gelir_TeslimEdildi);
            this._1_Gelir.Controls.Add(this._1_Gelir_DevamEden);
            this._1_Gelir.Location = new System.Drawing.Point(6, 6);
            this._1_Gelir.Name = "_1_Gelir";
            this._1_Gelir.Size = new System.Drawing.Size(405, 96);
            this._1_Gelir.TabIndex = 11;
            this._1_Gelir.TabStop = false;
            this._1_Gelir.Text = "İşlerin ücretlerini GELİR olarak hesaba kat";
            // 
            // _1_Gelir_ÖdemeTalepEdildi
            // 
            this._1_Gelir_ÖdemeTalepEdildi.AutoSize = true;
            this._1_Gelir_ÖdemeTalepEdildi.Checked = true;
            this._1_Gelir_ÖdemeTalepEdildi.CheckState = System.Windows.Forms.CheckState.Checked;
            this._1_Gelir_ÖdemeTalepEdildi.Location = new System.Drawing.Point(6, 63);
            this._1_Gelir_ÖdemeTalepEdildi.Name = "_1_Gelir_ÖdemeTalepEdildi";
            this._1_Gelir_ÖdemeTalepEdildi.Size = new System.Drawing.Size(196, 29);
            this._1_Gelir_ÖdemeTalepEdildi.TabIndex = 14;
            this._1_Gelir_ÖdemeTalepEdildi.Text = "Ödeme talep edildi";
            this._1_Gelir_ÖdemeTalepEdildi.UseVisualStyleBackColor = true;
            this._1_Gelir_ÖdemeTalepEdildi.CheckedChanged += new System.EventHandler(this._1_Hesapla);
            // 
            // _1_Gelir_TeslimEdildi
            // 
            this._1_Gelir_TeslimEdildi.AutoSize = true;
            this._1_Gelir_TeslimEdildi.Location = new System.Drawing.Point(216, 29);
            this._1_Gelir_TeslimEdildi.Name = "_1_Gelir_TeslimEdildi";
            this._1_Gelir_TeslimEdildi.Size = new System.Drawing.Size(142, 29);
            this._1_Gelir_TeslimEdildi.TabIndex = 13;
            this._1_Gelir_TeslimEdildi.Text = "Teslim edildi";
            this._1_Gelir_TeslimEdildi.UseVisualStyleBackColor = true;
            this._1_Gelir_TeslimEdildi.CheckedChanged += new System.EventHandler(this._1_Hesapla);
            // 
            // _1_Gelir_DevamEden
            // 
            this._1_Gelir_DevamEden.AutoSize = true;
            this._1_Gelir_DevamEden.Location = new System.Drawing.Point(6, 29);
            this._1_Gelir_DevamEden.Name = "_1_Gelir_DevamEden";
            this._1_Gelir_DevamEden.Size = new System.Drawing.Size(145, 29);
            this._1_Gelir_DevamEden.TabIndex = 12;
            this._1_Gelir_DevamEden.Text = "Devam eden";
            this._1_Gelir_DevamEden.UseVisualStyleBackColor = true;
            this._1_Gelir_DevamEden.CheckedChanged += new System.EventHandler(this._1_Hesapla);
            // 
            // _1_Gider
            // 
            this._1_Gider.Controls.Add(this._1_Gider_ÖdemeTalepEdildi);
            this._1_Gider.Controls.Add(this._1_Gider_DevamEden);
            this._1_Gider.Controls.Add(this._1_Gider_TeslimEdildi);
            this._1_Gider.Location = new System.Drawing.Point(417, 6);
            this._1_Gider.Name = "_1_Gider";
            this._1_Gider.Size = new System.Drawing.Size(420, 96);
            this._1_Gider.TabIndex = 12;
            this._1_Gider.TabStop = false;
            this._1_Gider.Text = "İşlerin maliyetlerini GİDER olarak hesaba kat";
            // 
            // _1_Gider_ÖdemeTalepEdildi
            // 
            this._1_Gider_ÖdemeTalepEdildi.AutoSize = true;
            this._1_Gider_ÖdemeTalepEdildi.Checked = true;
            this._1_Gider_ÖdemeTalepEdildi.CheckState = System.Windows.Forms.CheckState.Checked;
            this._1_Gider_ÖdemeTalepEdildi.Location = new System.Drawing.Point(6, 63);
            this._1_Gider_ÖdemeTalepEdildi.Name = "_1_Gider_ÖdemeTalepEdildi";
            this._1_Gider_ÖdemeTalepEdildi.Size = new System.Drawing.Size(196, 29);
            this._1_Gider_ÖdemeTalepEdildi.TabIndex = 17;
            this._1_Gider_ÖdemeTalepEdildi.Text = "Ödeme talep edildi";
            this._1_Gider_ÖdemeTalepEdildi.UseVisualStyleBackColor = true;
            this._1_Gider_ÖdemeTalepEdildi.CheckedChanged += new System.EventHandler(this._1_Hesapla);
            // 
            // _1_Gider_DevamEden
            // 
            this._1_Gider_DevamEden.AutoSize = true;
            this._1_Gider_DevamEden.Checked = true;
            this._1_Gider_DevamEden.CheckState = System.Windows.Forms.CheckState.Checked;
            this._1_Gider_DevamEden.Location = new System.Drawing.Point(6, 29);
            this._1_Gider_DevamEden.Name = "_1_Gider_DevamEden";
            this._1_Gider_DevamEden.Size = new System.Drawing.Size(145, 29);
            this._1_Gider_DevamEden.TabIndex = 15;
            this._1_Gider_DevamEden.Text = "Devam eden";
            this._1_Gider_DevamEden.UseVisualStyleBackColor = true;
            this._1_Gider_DevamEden.CheckedChanged += new System.EventHandler(this._1_Hesapla);
            // 
            // _1_Gider_TeslimEdildi
            // 
            this._1_Gider_TeslimEdildi.AutoSize = true;
            this._1_Gider_TeslimEdildi.Checked = true;
            this._1_Gider_TeslimEdildi.CheckState = System.Windows.Forms.CheckState.Checked;
            this._1_Gider_TeslimEdildi.Location = new System.Drawing.Point(216, 29);
            this._1_Gider_TeslimEdildi.Name = "_1_Gider_TeslimEdildi";
            this._1_Gider_TeslimEdildi.Size = new System.Drawing.Size(142, 29);
            this._1_Gider_TeslimEdildi.TabIndex = 16;
            this._1_Gider_TeslimEdildi.Text = "Teslim edildi";
            this._1_Gider_TeslimEdildi.UseVisualStyleBackColor = true;
            this._1_Gider_TeslimEdildi.CheckedChanged += new System.EventHandler(this._1_Hesapla);
            // 
            // _1_Tablo
            // 
            this._1_Tablo.AllowUserToAddRows = false;
            this._1_Tablo.AllowUserToDeleteRows = false;
            this._1_Tablo.AllowUserToResizeColumns = false;
            this._1_Tablo.AllowUserToResizeRows = false;
            this._1_Tablo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._1_Tablo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._1_Tablo.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._1_Tablo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this._1_Tablo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._1_Tablo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._1_Tablo_Seç,
            this._1_Tablo_Müşteri,
            this._1_Tablo_Gelir,
            this._1_Tablo_Gider,
            this._1_Tablo_Fark});
            this._1_Tablo.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this._1_Tablo.Location = new System.Drawing.Point(6, 108);
            this._1_Tablo.Name = "_1_Tablo";
            this._1_Tablo.ReadOnly = true;
            this._1_Tablo.RowHeadersVisible = false;
            this._1_Tablo.RowHeadersWidth = 51;
            this._1_Tablo.RowTemplate.Height = 24;
            this._1_Tablo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this._1_Tablo.ShowCellErrors = false;
            this._1_Tablo.ShowEditingIcon = false;
            this._1_Tablo.ShowRowErrors = false;
            this._1_Tablo.Size = new System.Drawing.Size(831, 264);
            this._1_Tablo.TabIndex = 10;
            this._1_Tablo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this._1_Tablo_CellClick);
            this._1_Tablo.DoubleClick += new System.EventHandler(this._1_Tablo_DoubleClick);
            // 
            // _1_Tablo_Seç
            // 
            this._1_Tablo_Seç.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this._1_Tablo_Seç.HeaderText = "Seç";
            this._1_Tablo_Seç.MinimumWidth = 6;
            this._1_Tablo_Seç.Name = "_1_Tablo_Seç";
            this._1_Tablo_Seç.ReadOnly = true;
            this._1_Tablo_Seç.Width = 53;
            // 
            // _1_Tablo_Müşteri
            // 
            this._1_Tablo_Müşteri.FillWeight = 40F;
            this._1_Tablo_Müşteri.HeaderText = "Müşteri";
            this._1_Tablo_Müşteri.MinimumWidth = 6;
            this._1_Tablo_Müşteri.Name = "_1_Tablo_Müşteri";
            this._1_Tablo_Müşteri.ReadOnly = true;
            // 
            // _1_Tablo_Gelir
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this._1_Tablo_Gelir.DefaultCellStyle = dataGridViewCellStyle2;
            this._1_Tablo_Gelir.FillWeight = 20F;
            this._1_Tablo_Gelir.HeaderText = "Gelir";
            this._1_Tablo_Gelir.MinimumWidth = 6;
            this._1_Tablo_Gelir.Name = "_1_Tablo_Gelir";
            this._1_Tablo_Gelir.ReadOnly = true;
            // 
            // _1_Tablo_Gider
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this._1_Tablo_Gider.DefaultCellStyle = dataGridViewCellStyle3;
            this._1_Tablo_Gider.FillWeight = 20F;
            this._1_Tablo_Gider.HeaderText = "Gider";
            this._1_Tablo_Gider.MinimumWidth = 6;
            this._1_Tablo_Gider.Name = "_1_Tablo_Gider";
            this._1_Tablo_Gider.ReadOnly = true;
            // 
            // _1_Tablo_Fark
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this._1_Tablo_Fark.DefaultCellStyle = dataGridViewCellStyle4;
            this._1_Tablo_Fark.FillWeight = 20F;
            this._1_Tablo_Fark.HeaderText = "Fark";
            this._1_Tablo_Fark.MinimumWidth = 6;
            this._1_Tablo_Fark.Name = "_1_Tablo_Fark";
            this._1_Tablo_Fark.ReadOnly = true;
            // 
            // Sekmeler
            // 
            this.Sekmeler.Controls.Add(this.Sekmeler_1);
            this.Sekmeler.Controls.Add(this.Sekmeler_2);
            this.Sekmeler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Sekmeler.Location = new System.Drawing.Point(0, 0);
            this.Sekmeler.Name = "Sekmeler";
            this.Sekmeler.SelectedIndex = 0;
            this.Sekmeler.Size = new System.Drawing.Size(853, 449);
            this.Sekmeler.TabIndex = 16;
            this.Sekmeler.SelectedIndexChanged += new System.EventHandler(this.Sekmeler_SelectedIndexChanged);
            // 
            // Sekmeler_1
            // 
            this.Sekmeler_1.AutoScroll = true;
            this.Sekmeler_1.Controls.Add(this._1_AltToplam);
            this.Sekmeler_1.Controls.Add(this._1_Tablo);
            this.Sekmeler_1.Controls.Add(this._1_Gelir);
            this.Sekmeler_1.Controls.Add(this._1_Gider);
            this.Sekmeler_1.Location = new System.Drawing.Point(4, 34);
            this.Sekmeler_1.Name = "Sekmeler_1";
            this.Sekmeler_1.Padding = new System.Windows.Forms.Padding(3);
            this.Sekmeler_1.Size = new System.Drawing.Size(845, 411);
            this.Sekmeler_1.TabIndex = 0;
            this.Sekmeler_1.Text = "Müşteriler kapsamında";
            this.Sekmeler_1.UseVisualStyleBackColor = true;
            // 
            // _1_AltToplam
            // 
            this._1_AltToplam.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._1_AltToplam.Location = new System.Drawing.Point(3, 378);
            this._1_AltToplam.Name = "_1_AltToplam";
            this._1_AltToplam.ReadOnly = true;
            this._1_AltToplam.Size = new System.Drawing.Size(839, 30);
            this._1_AltToplam.TabIndex = 15;
            this._1_AltToplam.Text = "Alt Toplam";
            this._1_AltToplam.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Sekmeler_2
            // 
            this.Sekmeler_2.Controls.Add(this._2_Tablo);
            this.Sekmeler_2.Controls.Add(this._2_AltToplam);
            this.Sekmeler_2.Controls.Add(this.Kaydet);
            this.Sekmeler_2.Location = new System.Drawing.Point(4, 34);
            this.Sekmeler_2.Name = "Sekmeler_2";
            this.Sekmeler_2.Padding = new System.Windows.Forms.Padding(3);
            this.Sekmeler_2.Size = new System.Drawing.Size(845, 411);
            this.Sekmeler_2.TabIndex = 1;
            this.Sekmeler_2.Text = "Genel anlamda";
            this.Sekmeler_2.UseVisualStyleBackColor = true;
            // 
            // _2_Tablo
            // 
            this._2_Tablo.AllowUserToResizeColumns = false;
            this._2_Tablo.AllowUserToResizeRows = false;
            this._2_Tablo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._2_Tablo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._2_Tablo.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._2_Tablo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this._2_Tablo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._2_Tablo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._2_Tablo_Seç,
            this._2_Tablo_Açıklama,
            this._2_Tablo_Gelir,
            this._2_Tablo_Gider,
            this._2_Tablo_Fark});
            this._2_Tablo.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this._2_Tablo.Location = new System.Drawing.Point(3, 6);
            this._2_Tablo.Name = "_2_Tablo";
            this._2_Tablo.RowHeadersVisible = false;
            this._2_Tablo.RowHeadersWidth = 51;
            this._2_Tablo.RowTemplate.Height = 24;
            this._2_Tablo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this._2_Tablo.ShowCellErrors = false;
            this._2_Tablo.ShowEditingIcon = false;
            this._2_Tablo.ShowRowErrors = false;
            this._2_Tablo.Size = new System.Drawing.Size(836, 357);
            this._2_Tablo.TabIndex = 16;
            this._2_Tablo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this._2_Tablo_CellClick);
            this._2_Tablo.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this._2_Tablo_CellValueChanged);
            this._2_Tablo.DoubleClick += new System.EventHandler(this._2_Tablo_DoubleClick);
            // 
            // _2_Tablo_Seç
            // 
            this._2_Tablo_Seç.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this._2_Tablo_Seç.HeaderText = "Seç";
            this._2_Tablo_Seç.MinimumWidth = 6;
            this._2_Tablo_Seç.Name = "_2_Tablo_Seç";
            this._2_Tablo_Seç.ReadOnly = true;
            this._2_Tablo_Seç.Width = 53;
            // 
            // _2_Tablo_Açıklama
            // 
            this._2_Tablo_Açıklama.FillWeight = 40F;
            this._2_Tablo_Açıklama.HeaderText = "Açıklama";
            this._2_Tablo_Açıklama.MinimumWidth = 6;
            this._2_Tablo_Açıklama.Name = "_2_Tablo_Açıklama";
            // 
            // _2_Tablo_Gelir
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this._2_Tablo_Gelir.DefaultCellStyle = dataGridViewCellStyle6;
            this._2_Tablo_Gelir.FillWeight = 20F;
            this._2_Tablo_Gelir.HeaderText = "Gelir ₺";
            this._2_Tablo_Gelir.MinimumWidth = 6;
            this._2_Tablo_Gelir.Name = "_2_Tablo_Gelir";
            // 
            // _2_Tablo_Gider
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this._2_Tablo_Gider.DefaultCellStyle = dataGridViewCellStyle7;
            this._2_Tablo_Gider.FillWeight = 20F;
            this._2_Tablo_Gider.HeaderText = "Gider ₺";
            this._2_Tablo_Gider.MinimumWidth = 6;
            this._2_Tablo_Gider.Name = "_2_Tablo_Gider";
            // 
            // _2_Tablo_Fark
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this._2_Tablo_Fark.DefaultCellStyle = dataGridViewCellStyle8;
            this._2_Tablo_Fark.FillWeight = 20F;
            this._2_Tablo_Fark.HeaderText = "Fark ₺";
            this._2_Tablo_Fark.MinimumWidth = 6;
            this._2_Tablo_Fark.Name = "_2_Tablo_Fark";
            this._2_Tablo_Fark.ReadOnly = true;
            // 
            // _2_AltToplam
            // 
            this._2_AltToplam.Location = new System.Drawing.Point(140, 375);
            this._2_AltToplam.Name = "_2_AltToplam";
            this._2_AltToplam.ReadOnly = true;
            this._2_AltToplam.Size = new System.Drawing.Size(699, 30);
            this._2_AltToplam.TabIndex = 17;
            this._2_AltToplam.Text = "Alt Toplam";
            this._2_AltToplam.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Kaydet
            // 
            this.Kaydet.Enabled = false;
            this.Kaydet.Location = new System.Drawing.Point(3, 369);
            this.Kaydet.Name = "Kaydet";
            this.Kaydet.Size = new System.Drawing.Size(131, 39);
            this.Kaydet.TabIndex = 18;
            this.Kaydet.Text = "Kaydet";
            this.Kaydet.UseVisualStyleBackColor = true;
            this.Kaydet.Click += new System.EventHandler(this.Kaydet_Click);
            // 
            // Bütçe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 449);
            this.Controls.Add(this.Sekmeler);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "Bütçe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bütçe";
            this.Shown += new System.EventHandler(this.Bütçe_Shown);
            this._1_Gelir.ResumeLayout(false);
            this._1_Gelir.PerformLayout();
            this._1_Gider.ResumeLayout(false);
            this._1_Gider.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._1_Tablo)).EndInit();
            this.Sekmeler.ResumeLayout(false);
            this.Sekmeler_1.ResumeLayout(false);
            this.Sekmeler_1.PerformLayout();
            this.Sekmeler_2.ResumeLayout(false);
            this.Sekmeler_2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._2_Tablo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox _1_Gelir;
        private System.Windows.Forms.CheckBox _1_Gelir_ÖdemeTalepEdildi;
        private System.Windows.Forms.CheckBox _1_Gelir_TeslimEdildi;
        private System.Windows.Forms.CheckBox _1_Gelir_DevamEden;
        private System.Windows.Forms.GroupBox _1_Gider;
        private System.Windows.Forms.DataGridView _1_Tablo;
        private System.Windows.Forms.TabControl Sekmeler;
        private System.Windows.Forms.TabPage Sekmeler_1;
        private System.Windows.Forms.TabPage Sekmeler_2;
        private System.Windows.Forms.CheckBox _1_Gider_ÖdemeTalepEdildi;
        private System.Windows.Forms.CheckBox _1_Gider_DevamEden;
        private System.Windows.Forms.CheckBox _1_Gider_TeslimEdildi;
        private System.Windows.Forms.TextBox _1_AltToplam;
        private System.Windows.Forms.TextBox _2_AltToplam;
        private System.Windows.Forms.DataGridView _2_Tablo;
        private System.Windows.Forms.DataGridViewCheckBoxColumn _2_Tablo_Seç;
        private System.Windows.Forms.DataGridViewTextBoxColumn _2_Tablo_Açıklama;
        private System.Windows.Forms.DataGridViewTextBoxColumn _2_Tablo_Gelir;
        private System.Windows.Forms.DataGridViewTextBoxColumn _2_Tablo_Gider;
        private System.Windows.Forms.DataGridViewTextBoxColumn _2_Tablo_Fark;
        private System.Windows.Forms.Button Kaydet;
        private System.Windows.Forms.DataGridViewCheckBoxColumn _1_Tablo_Seç;
        private System.Windows.Forms.DataGridViewTextBoxColumn _1_Tablo_Müşteri;
        private System.Windows.Forms.DataGridViewTextBoxColumn _1_Tablo_Gelir;
        private System.Windows.Forms.DataGridViewTextBoxColumn _1_Tablo_Gider;
        private System.Windows.Forms.DataGridViewTextBoxColumn _1_Tablo_Fark;
    }
}