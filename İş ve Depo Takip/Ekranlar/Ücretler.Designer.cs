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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Müşterıler = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Zam_Miktarı = new System.Windows.Forms.TextBox();
            this.Tablo = new System.Windows.Forms.DataGridView();
            this.Tablo_İş_Türleri = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tablo_Ücret = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tablo_Maliyet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SağTuşMenü_Değişkenler = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.dahiliDeğişkenlerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ücretiToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.Zam_Yap = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.AramaÇubuğu_Müşteri = new System.Windows.Forms.TextBox();
            this.AramaÇubuğu_İşTürü = new System.Windows.Forms.TextBox();
            this.Kaydet = new System.Windows.Forms.Button();
            this.KDV = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.DeğişkenlerEkranı = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Tablo)).BeginInit();
            this.SağTuşMenü_Değişkenler.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Müşterıler
            // 
            this.Müşterıler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Müşterıler.FormattingEnabled = true;
            this.Müşterıler.ItemHeight = 29;
            this.Müşterıler.Location = new System.Drawing.Point(0, 34);
            this.Müşterıler.Margin = new System.Windows.Forms.Padding(4);
            this.Müşterıler.Name = "Müşterıler";
            this.Müşterıler.Size = new System.Drawing.Size(233, 459);
            this.Müşterıler.TabIndex = 0;
            this.Müşterıler.SelectedIndexChanged += new System.EventHandler(this.Müşterıler_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(301, 29);
            this.label1.TabIndex = 2;
            this.label1.Text = "Görüntülenen ücretlerde %";
            // 
            // Zam_Miktarı
            // 
            this.Zam_Miktarı.Dock = System.Windows.Forms.DockStyle.Left;
            this.Zam_Miktarı.Location = new System.Drawing.Point(301, 0);
            this.Zam_Miktarı.Margin = new System.Windows.Forms.Padding(4);
            this.Zam_Miktarı.Name = "Zam_Miktarı";
            this.Zam_Miktarı.Size = new System.Drawing.Size(73, 34);
            this.Zam_Miktarı.TabIndex = 4;
            this.Zam_Miktarı.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Tablo
            // 
            this.Tablo.AllowUserToAddRows = false;
            this.Tablo.AllowUserToDeleteRows = false;
            this.Tablo.AllowUserToResizeColumns = false;
            this.Tablo.AllowUserToResizeRows = false;
            this.Tablo.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
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
            this.Tablo.ContextMenuStrip = this.SağTuşMenü_Değişkenler;
            this.Tablo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tablo.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.Tablo.Location = new System.Drawing.Point(0, 34);
            this.Tablo.Margin = new System.Windows.Forms.Padding(4);
            this.Tablo.MultiSelect = false;
            this.Tablo.Name = "Tablo";
            this.Tablo.RowHeadersVisible = false;
            this.Tablo.RowHeadersWidth = 51;
            this.Tablo.RowTemplate.Height = 24;
            this.Tablo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.Tablo.ShowCellErrors = false;
            this.Tablo.ShowEditingIcon = false;
            this.Tablo.ShowRowErrors = false;
            this.Tablo.Size = new System.Drawing.Size(777, 459);
            this.Tablo.TabIndex = 1;
            this.Tablo.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tablo_CellValueChanged);
            // 
            // Tablo_İş_Türleri
            // 
            this.Tablo_İş_Türleri.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Tablo_İş_Türleri.FillWeight = 50F;
            this.Tablo_İş_Türleri.HeaderText = "İş Türleri";
            this.Tablo_İş_Türleri.MinimumWidth = 6;
            this.Tablo_İş_Türleri.Name = "Tablo_İş_Türleri";
            this.Tablo_İş_Türleri.ReadOnly = true;
            this.Tablo_İş_Türleri.Width = 137;
            // 
            // Tablo_Ücret
            // 
            this.Tablo_Ücret.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Tablo_Ücret.DefaultCellStyle = dataGridViewCellStyle2;
            this.Tablo_Ücret.FillWeight = 25F;
            this.Tablo_Ücret.HeaderText = "Birim Ücret ₺";
            this.Tablo_Ücret.MinimumWidth = 6;
            this.Tablo_Ücret.Name = "Tablo_Ücret";
            // 
            // Tablo_Maliyet
            // 
            this.Tablo_Maliyet.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Tablo_Maliyet.DefaultCellStyle = dataGridViewCellStyle3;
            this.Tablo_Maliyet.FillWeight = 25F;
            this.Tablo_Maliyet.HeaderText = "Birim Maliyet ₺";
            this.Tablo_Maliyet.MinimumWidth = 6;
            this.Tablo_Maliyet.Name = "Tablo_Maliyet";
            // 
            // SağTuşMenü_Değişkenler
            // 
            this.SağTuşMenü_Değişkenler.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.SağTuşMenü_Değişkenler.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dahiliDeğişkenlerToolStripMenuItem});
            this.SağTuşMenü_Değişkenler.Name = "SağTuşMenü_Değişkenler";
            this.SağTuşMenü_Değişkenler.ShowImageMargin = false;
            this.SağTuşMenü_Değişkenler.Size = new System.Drawing.Size(175, 28);
            // 
            // dahiliDeğişkenlerToolStripMenuItem
            // 
            this.dahiliDeğişkenlerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ücretiToolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripSeparator2});
            this.dahiliDeğişkenlerToolStripMenuItem.Name = "dahiliDeğişkenlerToolStripMenuItem";
            this.dahiliDeğişkenlerToolStripMenuItem.Size = new System.Drawing.Size(174, 24);
            this.dahiliDeğişkenlerToolStripMenuItem.Text = "Dahili Değişkenler";
            // 
            // ücretiToolStripMenuItem1
            // 
            this.ücretiToolStripMenuItem1.Name = "ücretiToolStripMenuItem1";
            this.ücretiToolStripMenuItem1.Size = new System.Drawing.Size(171, 26);
            this.ücretiToolStripMenuItem1.Text = "Ortak Ücreti";
            this.ücretiToolStripMenuItem1.Click += new System.EventHandler(this.SağTuşMenü_Değişkenler_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(171, 26);
            this.toolStripMenuItem2.Text = "Maliyeti";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.SağTuşMenü_Değişkenler_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(168, 6);
            // 
            // Zam_Yap
            // 
            this.Zam_Yap.AutoSize = true;
            this.Zam_Yap.Dock = System.Windows.Forms.DockStyle.Left;
            this.Zam_Yap.Location = new System.Drawing.Point(374, 0);
            this.Zam_Yap.Margin = new System.Windows.Forms.Padding(4);
            this.Zam_Yap.Name = "Zam_Yap";
            this.Zam_Yap.Size = new System.Drawing.Size(169, 36);
            this.Zam_Yap.TabIndex = 5;
            this.Zam_Yap.Text = "değişiklik yap";
            this.Zam_Yap.UseVisualStyleBackColor = true;
            this.Zam_Yap.Click += new System.EventHandler(this.Zam_Yap_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(5, 41);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.Müşterıler);
            this.splitContainer1.Panel1.Controls.Add(this.AramaÇubuğu_Müşteri);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.Tablo);
            this.splitContainer1.Panel2.Controls.Add(this.AramaÇubuğu_İşTürü);
            this.splitContainer1.Panel2.Enabled = false;
            this.splitContainer1.Size = new System.Drawing.Size(1022, 497);
            this.splitContainer1.SplitterDistance = 237;
            this.splitContainer1.TabIndex = 8;
            // 
            // AramaÇubuğu_Müşteri
            // 
            this.AramaÇubuğu_Müşteri.Dock = System.Windows.Forms.DockStyle.Top;
            this.AramaÇubuğu_Müşteri.Location = new System.Drawing.Point(0, 0);
            this.AramaÇubuğu_Müşteri.Name = "AramaÇubuğu_Müşteri";
            this.AramaÇubuğu_Müşteri.Size = new System.Drawing.Size(233, 34);
            this.AramaÇubuğu_Müşteri.TabIndex = 4;
            this.AramaÇubuğu_Müşteri.TextChanged += new System.EventHandler(this.AramaÇubuğu_Müşteri_TextChanged);
            // 
            // AramaÇubuğu_İşTürü
            // 
            this.AramaÇubuğu_İşTürü.Dock = System.Windows.Forms.DockStyle.Top;
            this.AramaÇubuğu_İşTürü.Location = new System.Drawing.Point(0, 0);
            this.AramaÇubuğu_İşTürü.Name = "AramaÇubuğu_İşTürü";
            this.AramaÇubuğu_İşTürü.Size = new System.Drawing.Size(777, 34);
            this.AramaÇubuğu_İşTürü.TabIndex = 4;
            this.AramaÇubuğu_İşTürü.TextChanged += new System.EventHandler(this.AramaÇubuğu_İşTürü_TextChanged);
            // 
            // Kaydet
            // 
            this.Kaydet.AutoSize = true;
            this.Kaydet.Dock = System.Windows.Forms.DockStyle.Right;
            this.Kaydet.Enabled = false;
            this.Kaydet.Location = new System.Drawing.Point(925, 0);
            this.Kaydet.Margin = new System.Windows.Forms.Padding(4);
            this.Kaydet.Name = "Kaydet";
            this.Kaydet.Size = new System.Drawing.Size(97, 36);
            this.Kaydet.TabIndex = 2;
            this.Kaydet.Text = "Kaydet";
            this.Kaydet.UseVisualStyleBackColor = true;
            this.Kaydet.Click += new System.EventHandler(this.Kaydet_Click);
            // 
            // KDV
            // 
            this.KDV.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.KDV.Location = new System.Drawing.Point(668, 1);
            this.KDV.Margin = new System.Windows.Forms.Padding(4);
            this.KDV.Name = "KDV";
            this.KDV.Size = new System.Drawing.Size(73, 34);
            this.KDV.TabIndex = 9;
            this.KDV.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.KDV.TextChanged += new System.EventHandler(this.Ayar_Değişti);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(573, 4);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 29);
            this.label2.TabIndex = 10;
            this.label2.Text = "KDV %";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.DeğişkenlerEkranı);
            this.panel1.Controls.Add(this.Kaydet);
            this.panel1.Controls.Add(this.Zam_Yap);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.Zam_Miktarı);
            this.panel1.Controls.Add(this.KDV);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(5, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1022, 36);
            this.panel1.TabIndex = 11;
            // 
            // DeğişkenlerEkranı
            // 
            this.DeğişkenlerEkranı.AutoSize = true;
            this.DeğişkenlerEkranı.Dock = System.Windows.Forms.DockStyle.Right;
            this.DeğişkenlerEkranı.Location = new System.Drawing.Point(772, 0);
            this.DeğişkenlerEkranı.Margin = new System.Windows.Forms.Padding(4);
            this.DeğişkenlerEkranı.Name = "DeğişkenlerEkranı";
            this.DeğişkenlerEkranı.Size = new System.Drawing.Size(153, 36);
            this.DeğişkenlerEkranı.TabIndex = 11;
            this.DeğişkenlerEkranı.Text = "Değişkenler";
            this.DeğişkenlerEkranı.UseVisualStyleBackColor = true;
            this.DeğişkenlerEkranı.Click += new System.EventHandler(this.Değişkenler_Click);
            // 
            // Ücretler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1032, 543);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "Ücretler";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ücretler";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.Ücretler_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.Tablo)).EndInit();
            this.SağTuşMenü_Değişkenler.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.TextBox KDV;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tablo_İş_Türleri;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tablo_Ücret;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tablo_Maliyet;
        private System.Windows.Forms.ContextMenuStrip SağTuşMenü_Değişkenler;
        private System.Windows.Forms.ToolStripMenuItem dahiliDeğişkenlerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ücretiToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.Button DeğişkenlerEkranı;
    }
}