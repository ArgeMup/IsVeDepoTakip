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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.Seç = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.İlerlemeÇubuğu = new System.Windows.Forms.ProgressBar();
            this._1_AltToplam = new System.Windows.Forms.TextBox();
            this._1_Gelir.SuspendLayout();
            this._1_Gider.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._1_Tablo)).BeginInit();
            this.Sekmeler.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
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
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._1_Tablo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this._1_Tablo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._1_Tablo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._1_Tablo_Seç,
            this._1_Tablo_Müşteri,
            this._1_Tablo_Gelir,
            this._1_Tablo_Gider,
            this._1_Tablo_Fark});
            this._1_Tablo.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this._1_Tablo.Location = new System.Drawing.Point(6, 108);
            this._1_Tablo.MultiSelect = false;
            this._1_Tablo.Name = "_1_Tablo";
            this._1_Tablo.ReadOnly = true;
            this._1_Tablo.RowHeadersVisible = false;
            this._1_Tablo.RowHeadersWidth = 51;
            this._1_Tablo.RowTemplate.Height = 24;
            this._1_Tablo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._1_Tablo.ShowCellErrors = false;
            this._1_Tablo.ShowEditingIcon = false;
            this._1_Tablo.ShowRowErrors = false;
            this._1_Tablo.Size = new System.Drawing.Size(831, 313);
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
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this._1_Tablo_Gelir.DefaultCellStyle = dataGridViewCellStyle10;
            this._1_Tablo_Gelir.FillWeight = 20F;
            this._1_Tablo_Gelir.HeaderText = "Gelir ₺";
            this._1_Tablo_Gelir.MinimumWidth = 6;
            this._1_Tablo_Gelir.Name = "_1_Tablo_Gelir";
            this._1_Tablo_Gelir.ReadOnly = true;
            // 
            // _1_Tablo_Gider
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this._1_Tablo_Gider.DefaultCellStyle = dataGridViewCellStyle11;
            this._1_Tablo_Gider.FillWeight = 20F;
            this._1_Tablo_Gider.HeaderText = "Gider ₺";
            this._1_Tablo_Gider.MinimumWidth = 6;
            this._1_Tablo_Gider.Name = "_1_Tablo_Gider";
            this._1_Tablo_Gider.ReadOnly = true;
            // 
            // _1_Tablo_Fark
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this._1_Tablo_Fark.DefaultCellStyle = dataGridViewCellStyle12;
            this._1_Tablo_Fark.FillWeight = 20F;
            this._1_Tablo_Fark.HeaderText = "Fark ₺";
            this._1_Tablo_Fark.MinimumWidth = 6;
            this._1_Tablo_Fark.Name = "_1_Tablo_Fark";
            this._1_Tablo_Fark.ReadOnly = true;
            // 
            // Sekmeler
            // 
            this.Sekmeler.Controls.Add(this.tabPage1);
            this.Sekmeler.Controls.Add(this.tabPage2);
            this.Sekmeler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Sekmeler.Location = new System.Drawing.Point(0, 0);
            this.Sekmeler.Name = "Sekmeler";
            this.Sekmeler.SelectedIndex = 0;
            this.Sekmeler.Size = new System.Drawing.Size(853, 498);
            this.Sekmeler.TabIndex = 16;
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.Controls.Add(this._1_AltToplam);
            this.tabPage1.Controls.Add(this._1_Tablo);
            this.tabPage1.Controls.Add(this._1_Gelir);
            this.tabPage1.Controls.Add(this._1_Gider);
            this.tabPage1.Location = new System.Drawing.Point(4, 34);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(845, 460);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Müşteriler kapsamında";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridView2);
            this.tabPage2.Location = new System.Drawing.Point(4, 34);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(976, 460);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Genel anlamda";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AllowUserToResizeColumns = false;
            this.dataGridView2.AllowUserToResizeRows = false;
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView2.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Seç,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6});
            this.dataGridView2.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dataGridView2.Location = new System.Drawing.Point(6, 6);
            this.dataGridView2.MultiSelect = false;
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.RowTemplate.Height = 24;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.ShowCellErrors = false;
            this.dataGridView2.ShowCellToolTips = false;
            this.dataGridView2.ShowEditingIcon = false;
            this.dataGridView2.ShowRowErrors = false;
            this.dataGridView2.Size = new System.Drawing.Size(883, 352);
            this.dataGridView2.TabIndex = 11;
            // 
            // Seç
            // 
            this.Seç.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Seç.HeaderText = "Seç";
            this.Seç.MinimumWidth = 6;
            this.Seç.Name = "Seç";
            this.Seç.ReadOnly = true;
            this.Seç.Width = 53;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.FillWeight = 40F;
            this.dataGridViewTextBoxColumn3.HeaderText = "Açıklama";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle14;
            this.dataGridViewTextBoxColumn4.FillWeight = 20F;
            this.dataGridViewTextBoxColumn4.HeaderText = "Gelir";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle15;
            this.dataGridViewTextBoxColumn5.FillWeight = 20F;
            this.dataGridViewTextBoxColumn5.HeaderText = "Gider";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle16;
            this.dataGridViewTextBoxColumn6.FillWeight = 20F;
            this.dataGridViewTextBoxColumn6.HeaderText = "Fark";
            this.dataGridViewTextBoxColumn6.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // İlerlemeÇubuğu
            // 
            this.İlerlemeÇubuğu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.İlerlemeÇubuğu.Location = new System.Drawing.Point(400, 4);
            this.İlerlemeÇubuğu.Name = "İlerlemeÇubuğu";
            this.İlerlemeÇubuğu.Size = new System.Drawing.Size(450, 23);
            this.İlerlemeÇubuğu.TabIndex = 14;
            this.İlerlemeÇubuğu.Visible = false;
            // 
            // _1_AltToplam
            // 
            this._1_AltToplam.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._1_AltToplam.Location = new System.Drawing.Point(6, 427);
            this._1_AltToplam.Name = "_1_AltToplam";
            this._1_AltToplam.ReadOnly = true;
            this._1_AltToplam.Size = new System.Drawing.Size(831, 30);
            this._1_AltToplam.TabIndex = 15;
            this._1_AltToplam.Text = "Alt Toplam";
            this._1_AltToplam.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Bütçe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 498);
            this.Controls.Add(this.İlerlemeÇubuğu);
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
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
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
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.CheckBox _1_Gider_ÖdemeTalepEdildi;
        private System.Windows.Forms.CheckBox _1_Gider_DevamEden;
        private System.Windows.Forms.CheckBox _1_Gider_TeslimEdildi;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Seç;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.ProgressBar İlerlemeÇubuğu;
        private System.Windows.Forms.DataGridViewCheckBoxColumn _1_Tablo_Seç;
        private System.Windows.Forms.DataGridViewTextBoxColumn _1_Tablo_Müşteri;
        private System.Windows.Forms.DataGridViewTextBoxColumn _1_Tablo_Gelir;
        private System.Windows.Forms.DataGridViewTextBoxColumn _1_Tablo_Gider;
        private System.Windows.Forms.DataGridViewTextBoxColumn _1_Tablo_Fark;
        private System.Windows.Forms.TextBox _1_AltToplam;
    }
}