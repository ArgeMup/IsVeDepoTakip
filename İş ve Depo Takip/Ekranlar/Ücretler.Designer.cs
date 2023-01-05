namespace İş_ve_Depo_Takip.Ekranlar
{
    partial class Ücretler
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
            this.Müşterıler = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Zam_Miktarı = new System.Windows.Forms.TextBox();
            this.Tablo = new System.Windows.Forms.DataGridView();
            this.Zam_Yap = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.AramaÇubuğu_Müşteri = new System.Windows.Forms.TextBox();
            this.AramaÇubuğu_İşTürü = new System.Windows.Forms.TextBox();
            this.Kaydet = new System.Windows.Forms.Button();
            this.Tablo_İş_Türleri = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tablo_Ücret = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tablo_Maliyet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.Tablo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Müşterıler
            // 
            this.Müşterıler.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Müşterıler.FormattingEnabled = true;
            this.Müşterıler.ItemHeight = 29;
            this.Müşterıler.Location = new System.Drawing.Point(0, 43);
            this.Müşterıler.Margin = new System.Windows.Forms.Padding(4);
            this.Müşterıler.Name = "Müşterıler";
            this.Müşterıler.Size = new System.Drawing.Size(325, 410);
            this.Müşterıler.Sorted = true;
            this.Müşterıler.TabIndex = 0;
            this.Müşterıler.SelectedIndexChanged += new System.EventHandler(this.Müşterıler_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(390, 29);
            this.label1.TabIndex = 2;
            this.label1.Text = "Seçilen müşterinin ücretlerinde %";
            // 
            // Zam_Miktarı
            // 
            this.Zam_Miktarı.Location = new System.Drawing.Point(412, 18);
            this.Zam_Miktarı.Margin = new System.Windows.Forms.Padding(4);
            this.Zam_Miktarı.Name = "Zam_Miktarı";
            this.Zam_Miktarı.Size = new System.Drawing.Size(148, 36);
            this.Zam_Miktarı.TabIndex = 4;
            this.Zam_Miktarı.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Tablo
            // 
            this.Tablo.AllowUserToAddRows = false;
            this.Tablo.AllowUserToDeleteRows = false;
            this.Tablo.AllowUserToResizeColumns = false;
            this.Tablo.AllowUserToResizeRows = false;
            this.Tablo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tablo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.Tablo.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tablo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Tablo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tablo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Tablo_İş_Türleri,
            this.Tablo_Ücret,
            this.Tablo_Maliyet});
            this.Tablo.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.Tablo.Location = new System.Drawing.Point(0, 43);
            this.Tablo.Margin = new System.Windows.Forms.Padding(4);
            this.Tablo.MultiSelect = false;
            this.Tablo.Name = "Tablo";
            this.Tablo.RowHeadersVisible = false;
            this.Tablo.RowHeadersWidth = 51;
            this.Tablo.RowTemplate.Height = 24;
            this.Tablo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Tablo.ShowCellErrors = false;
            this.Tablo.ShowCellToolTips = false;
            this.Tablo.ShowEditingIcon = false;
            this.Tablo.ShowRowErrors = false;
            this.Tablo.Size = new System.Drawing.Size(729, 415);
            this.Tablo.TabIndex = 1;
            this.Tablo.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tablo_CellValueChanged);
            // 
            // Zam_Yap
            // 
            this.Zam_Yap.Location = new System.Drawing.Point(568, 7);
            this.Zam_Yap.Margin = new System.Windows.Forms.Padding(4);
            this.Zam_Yap.Name = "Zam_Yap";
            this.Zam_Yap.Size = new System.Drawing.Size(220, 57);
            this.Zam_Yap.TabIndex = 5;
            this.Zam_Yap.Text = "değişiklik yap";
            this.Zam_Yap.UseVisualStyleBackColor = true;
            this.Zam_Yap.Click += new System.EventHandler(this.Zam_Yap_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 69);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.AramaÇubuğu_Müşteri);
            this.splitContainer1.Panel1.Controls.Add(this.Müşterıler);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.AramaÇubuğu_İşTürü);
            this.splitContainer1.Panel2.Controls.Add(this.Tablo);
            this.splitContainer1.Panel2.Enabled = false;
            this.splitContainer1.Size = new System.Drawing.Size(1058, 462);
            this.splitContainer1.SplitterDistance = 325;
            this.splitContainer1.TabIndex = 8;
            // 
            // AramaÇubuğu_Müşteri
            // 
            this.AramaÇubuğu_Müşteri.Dock = System.Windows.Forms.DockStyle.Top;
            this.AramaÇubuğu_Müşteri.Location = new System.Drawing.Point(0, 0);
            this.AramaÇubuğu_Müşteri.Name = "AramaÇubuğu_Müşteri";
            this.AramaÇubuğu_Müşteri.Size = new System.Drawing.Size(325, 36);
            this.AramaÇubuğu_Müşteri.TabIndex = 4;
            this.AramaÇubuğu_Müşteri.TextChanged += new System.EventHandler(this.AramaÇubuğu_Müşteri_TextChanged);
            // 
            // AramaÇubuğu_İşTürü
            // 
            this.AramaÇubuğu_İşTürü.Dock = System.Windows.Forms.DockStyle.Top;
            this.AramaÇubuğu_İşTürü.Location = new System.Drawing.Point(0, 0);
            this.AramaÇubuğu_İşTürü.Name = "AramaÇubuğu_İşTürü";
            this.AramaÇubuğu_İşTürü.Size = new System.Drawing.Size(729, 36);
            this.AramaÇubuğu_İşTürü.TabIndex = 4;
            this.AramaÇubuğu_İşTürü.TextChanged += new System.EventHandler(this.AramaÇubuğu_İşTürü_TextChanged);
            // 
            // Kaydet
            // 
            this.Kaydet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Kaydet.Enabled = false;
            this.Kaydet.Location = new System.Drawing.Point(912, 7);
            this.Kaydet.Margin = new System.Windows.Forms.Padding(4);
            this.Kaydet.Name = "Kaydet";
            this.Kaydet.Size = new System.Drawing.Size(158, 57);
            this.Kaydet.TabIndex = 2;
            this.Kaydet.Text = "Kaydet";
            this.Kaydet.UseVisualStyleBackColor = true;
            this.Kaydet.Click += new System.EventHandler(this.Kaydet_Click);
            // 
            // Tablo_İş_Türleri
            // 
            this.Tablo_İş_Türleri.FillWeight = 60F;
            this.Tablo_İş_Türleri.HeaderText = "İş Türleri";
            this.Tablo_İş_Türleri.MinimumWidth = 6;
            this.Tablo_İş_Türleri.Name = "Tablo_İş_Türleri";
            this.Tablo_İş_Türleri.ReadOnly = true;
            // 
            // Tablo_Ücret
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Tablo_Ücret.DefaultCellStyle = dataGridViewCellStyle2;
            this.Tablo_Ücret.FillWeight = 20F;
            this.Tablo_Ücret.HeaderText = "Ücret ₺";
            this.Tablo_Ücret.MinimumWidth = 6;
            this.Tablo_Ücret.Name = "Tablo_Ücret";
            // 
            // Tablo_Maliyet
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Tablo_Maliyet.DefaultCellStyle = dataGridViewCellStyle3;
            this.Tablo_Maliyet.FillWeight = 20F;
            this.Tablo_Maliyet.HeaderText = "Maliyet ₺";
            this.Tablo_Maliyet.MinimumWidth = 6;
            this.Tablo_Maliyet.Name = "Tablo_Maliyet";
            // 
            // Ücretler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(15F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 543);
            this.Controls.Add(this.Kaydet);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.Zam_Yap);
            this.Controls.Add(this.Zam_Miktarı);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "Ücretler";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ücretler";
            ((System.ComponentModel.ISupportInitialize)(this.Tablo)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox Müşterıler;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Zam_Miktarı;
        private System.Windows.Forms.DataGridView Tablo;
        private System.Windows.Forms.Button Zam_Yap;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button Kaydet;
        private System.Windows.Forms.TextBox AramaÇubuğu_Müşteri;
        private System.Windows.Forms.TextBox AramaÇubuğu_İşTürü;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tablo_İş_Türleri;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tablo_Ücret;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tablo_Maliyet;
    }
}