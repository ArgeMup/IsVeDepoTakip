namespace İş_ve_Depo_Takip.Ekranlar
{
    partial class Ayarlar_İş_Türleri
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
            this.ÖnYüzler_Kaydet = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.Liste_işTürleri = new ArgeMup.HazirKod.Ekranlar.ListeKutusu();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.Liste_Malzemeler = new ArgeMup.HazirKod.Ekranlar.ListeKutusu();
            this.Malzeme_SeçiliSatıraKopyala = new System.Windows.Forms.Button();
            this.Tablo = new System.Windows.Forms.DataGridView();
            this.Tablo_Malzeme = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tablo_Miktar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tablo_Biim = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Tamamlayıcıİş = new System.Windows.Forms.CheckBox();
            this.Notlar = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.MüşteriyeGösterilecekOlanAdı = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.İpUcu = new System.Windows.Forms.ToolTip(this.components);
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
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ÖnYüzler_Kaydet
            // 
            this.ÖnYüzler_Kaydet.AutoSize = true;
            this.ÖnYüzler_Kaydet.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ÖnYüzler_Kaydet.Enabled = false;
            this.ÖnYüzler_Kaydet.Image = global::İş_ve_Depo_Takip.Properties.Resources.sag;
            this.ÖnYüzler_Kaydet.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ÖnYüzler_Kaydet.Location = new System.Drawing.Point(0, 478);
            this.ÖnYüzler_Kaydet.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.ÖnYüzler_Kaydet.Name = "ÖnYüzler_Kaydet";
            this.ÖnYüzler_Kaydet.Size = new System.Drawing.Size(624, 45);
            this.ÖnYüzler_Kaydet.TabIndex = 4;
            this.ÖnYüzler_Kaydet.Text = "Kaydet";
            this.İpUcu.SetToolTip(this.ÖnYüzler_Kaydet, "Silinmek istenen malzemenin miktarı 0 yazılmalıdır");
            this.ÖnYüzler_Kaydet.UseVisualStyleBackColor = true;
            this.ÖnYüzler_Kaydet.Click += new System.EventHandler(this.ÖnYüzler_Kaydet_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(5, 5);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.Liste_işTürleri);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2.Controls.Add(this.ÖnYüzler_Kaydet);
            this.splitContainer1.Size = new System.Drawing.Size(972, 523);
            this.splitContainer1.SplitterDistance = 345;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 5;
            // 
            // Liste_işTürleri
            // 
            this.Liste_işTürleri.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Liste_işTürleri.Location = new System.Drawing.Point(0, 0);
            this.Liste_işTürleri.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Liste_işTürleri.Name = "Liste_işTürleri";
            this.Liste_işTürleri.Size = new System.Drawing.Size(341, 519);
            this.Liste_işTürleri.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 33);
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
            this.splitContainer2.Size = new System.Drawing.Size(624, 445);
            this.splitContainer2.SplitterDistance = 333;
            this.splitContainer2.SplitterWidth = 3;
            this.splitContainer2.TabIndex = 7;
            // 
            // splitContainer3
            // 
            this.splitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
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
            this.splitContainer3.Size = new System.Drawing.Size(624, 333);
            this.splitContainer3.SplitterDistance = 252;
            this.splitContainer3.SplitterWidth = 3;
            this.splitContainer3.TabIndex = 1;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.Liste_Malzemeler);
            this.groupBox4.Controls.Add(this.Malzeme_SeçiliSatıraKopyala);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(248, 329);
            this.groupBox4.TabIndex = 15;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Malzemeler";
            // 
            // Liste_Malzemeler
            // 
            this.Liste_Malzemeler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Liste_Malzemeler.Location = new System.Drawing.Point(3, 26);
            this.Liste_Malzemeler.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Liste_Malzemeler.Name = "Liste_Malzemeler";
            this.Liste_Malzemeler.Size = new System.Drawing.Size(242, 265);
            this.Liste_Malzemeler.TabIndex = 23;
            this.Liste_Malzemeler.DoubleClick += new System.EventHandler(this.Liste_Malzemeler_DoubleClick);
            // 
            // Malzeme_SeçiliSatıraKopyala
            // 
            this.Malzeme_SeçiliSatıraKopyala.AutoSize = true;
            this.Malzeme_SeçiliSatıraKopyala.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Malzeme_SeçiliSatıraKopyala.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Malzeme_SeçiliSatıraKopyala.Location = new System.Drawing.Point(3, 291);
            this.Malzeme_SeçiliSatıraKopyala.Margin = new System.Windows.Forms.Padding(2);
            this.Malzeme_SeçiliSatıraKopyala.Name = "Malzeme_SeçiliSatıraKopyala";
            this.Malzeme_SeçiliSatıraKopyala.Size = new System.Drawing.Size(242, 35);
            this.Malzeme_SeçiliSatıraKopyala.TabIndex = 22;
            this.Malzeme_SeçiliSatıraKopyala.Text = "Seçili Satıra Kopyala";
            this.Malzeme_SeçiliSatıraKopyala.UseVisualStyleBackColor = true;
            this.Malzeme_SeçiliSatıraKopyala.Click += new System.EventHandler(this.Malzeme_SeçiliSatıraKopyala_Click);
            // 
            // Tablo
            // 
            this.Tablo.AllowUserToResizeColumns = false;
            this.Tablo.AllowUserToResizeRows = false;
            this.Tablo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.Tablo.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
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
            this.Tablo.Size = new System.Drawing.Size(365, 329);
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
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Tablo_Miktar.DefaultCellStyle = dataGridViewCellStyle2;
            this.Tablo_Miktar.HeaderText = "Miktarı";
            this.Tablo_Miktar.MinimumWidth = 6;
            this.Tablo_Miktar.Name = "Tablo_Miktar";
            this.Tablo_Miktar.Width = 98;
            // 
            // Tablo_Biim
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Tablo_Biim.DefaultCellStyle = dataGridViewCellStyle3;
            this.Tablo_Biim.HeaderText = "Birimi";
            this.Tablo_Biim.MinimumWidth = 6;
            this.Tablo_Biim.Name = "Tablo_Biim";
            this.Tablo_Biim.ReadOnly = true;
            this.Tablo_Biim.Width = 88;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Tamamlayıcıİş);
            this.groupBox2.Controls.Add(this.Notlar);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox2.Size = new System.Drawing.Size(620, 105);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Notlar";
            // 
            // Tamamlayıcıİş
            // 
            this.Tamamlayıcıİş.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Tamamlayıcıİş.AutoSize = true;
            this.Tamamlayıcıİş.Location = new System.Drawing.Point(451, 3);
            this.Tamamlayıcıİş.Name = "Tamamlayıcıİş";
            this.Tamamlayıcıİş.Size = new System.Drawing.Size(164, 29);
            this.Tamamlayıcıİş.TabIndex = 7;
            this.Tamamlayıcıİş.Text = "Tamamlayıcı İş";
            this.Tamamlayıcıİş.UseVisualStyleBackColor = true;
            this.Tamamlayıcıİş.CheckedChanged += new System.EventHandler(this.Ayar_Değişti);
            // 
            // Notlar
            // 
            this.Notlar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Notlar.Location = new System.Drawing.Point(2, 26);
            this.Notlar.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Notlar.Multiline = true;
            this.Notlar.Name = "Notlar";
            this.Notlar.Size = new System.Drawing.Size(616, 76);
            this.Notlar.TabIndex = 6;
            this.İpUcu.SetToolTip(this.Notlar, "Silinmek istenen malzemenin miktarı 0 yazılmalıdır");
            this.Notlar.TextChanged += new System.EventHandler(this.Ayar_Değişti);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.MüşteriyeGösterilecekOlanAdı);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(624, 33);
            this.panel1.TabIndex = 11;
            // 
            // MüşteriyeGösterilecekOlanAdı
            // 
            this.MüşteriyeGösterilecekOlanAdı.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MüşteriyeGösterilecekOlanAdı.Location = new System.Drawing.Point(289, 0);
            this.MüşteriyeGösterilecekOlanAdı.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MüşteriyeGösterilecekOlanAdı.Name = "MüşteriyeGösterilecekOlanAdı";
            this.MüşteriyeGösterilecekOlanAdı.Size = new System.Drawing.Size(335, 30);
            this.MüşteriyeGösterilecekOlanAdı.TabIndex = 8;
            this.İpUcu.SetToolTip(this.MüşteriyeGösterilecekOlanAdı, "Arama çubuğu");
            this.MüşteriyeGösterilecekOlanAdı.TextChanged += new System.EventHandler(this.Ayar_Değişti);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(289, 25);
            this.label1.TabIndex = 9;
            this.label1.Text = "Müşteriye gösterilecek olan adı  ";
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
            // Ayarlar_İş_Türleri
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 533);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "Ayarlar_İş_Türleri";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "İş Türleri";
            this.splitContainer1.Panel1.ResumeLayout(false);
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
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button ÖnYüzler_Kaydet;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox Notlar;
        private System.Windows.Forms.ToolTip İpUcu;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView Tablo;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button Malzeme_SeçiliSatıraKopyala;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox MüşteriyeGösterilecekOlanAdı;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tablo_Malzeme;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tablo_Miktar;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tablo_Biim;
        private System.Windows.Forms.Panel panel1;
        private ArgeMup.HazirKod.Ekranlar.ListeKutusu Liste_işTürleri;
        private ArgeMup.HazirKod.Ekranlar.ListeKutusu Liste_Malzemeler;
        private System.Windows.Forms.CheckBox Tamamlayıcıİş;
    }
}