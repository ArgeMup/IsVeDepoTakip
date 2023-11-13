namespace İş_ve_Depo_Takip.Ekranlar
{
    partial class Ayarlar_Malzemeler
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ayarlar_Malzemeler));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.UyarıMiktarı = new System.Windows.Forms.TextBox();
            this.Birimi = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Miktarı = new System.Windows.Forms.TextBox();
            this.ÖnYüzler_Kaydet = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.Liste_Malzemeler = new ArgeMup.HazirKod.Ekranlar.ListeKutusu();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.Tablo = new System.Windows.Forms.DataGridView();
            this.Tablo_Kullanım = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tablo_Miktarı = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.DetaylıKullanım = new System.Windows.Forms.CheckBox();
            this.Notlar = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.İpUcu = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tablo)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // UyarıMiktarı
            // 
            this.UyarıMiktarı.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UyarıMiktarı.Location = new System.Drawing.Point(170, 87);
            this.UyarıMiktarı.Name = "UyarıMiktarı";
            this.UyarıMiktarı.Size = new System.Drawing.Size(301, 36);
            this.UyarıMiktarı.TabIndex = 7;
            this.UyarıMiktarı.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.İpUcu.SetToolTip(this.UyarıMiktarı, "Malzemenin mevcut miktarı burada yazan değerin altında ise açılış ekranında uyarı" +
        " gösterilir.\r\nİçeriği 0 dan büyük olmalıdır");
            this.UyarıMiktarı.TextChanged += new System.EventHandler(this.Ayar_Değişti);
            // 
            // Birimi
            // 
            this.Birimi.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Birimi.Location = new System.Drawing.Point(170, 45);
            this.Birimi.Name = "Birimi";
            this.Birimi.Size = new System.Drawing.Size(301, 36);
            this.Birimi.TabIndex = 6;
            this.Birimi.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.İpUcu.SetToolTip(this.Birimi, "Malzemenin birimi (kg, adet veya litre gibi)");
            this.Birimi.TextChanged += new System.EventHandler(this.Ayar_Değişti);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 29);
            this.label3.TabIndex = 3;
            this.label3.Text = "Birimi";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(154, 29);
            this.label2.TabIndex = 2;
            this.label2.Text = "Uyarı Miktarı";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 29);
            this.label1.TabIndex = 1;
            this.label1.Text = "Mevcut";
            // 
            // Miktarı
            // 
            this.Miktarı.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Miktarı.Location = new System.Drawing.Point(170, 3);
            this.Miktarı.Name = "Miktarı";
            this.Miktarı.Size = new System.Drawing.Size(301, 36);
            this.Miktarı.TabIndex = 5;
            this.Miktarı.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.İpUcu.SetToolTip(this.Miktarı, "Elinizdeki mevcut miktarı gösterir");
            this.Miktarı.TextChanged += new System.EventHandler(this.Ayar_Değişti);
            // 
            // ÖnYüzler_Kaydet
            // 
            this.ÖnYüzler_Kaydet.AutoSize = true;
            this.ÖnYüzler_Kaydet.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ÖnYüzler_Kaydet.Enabled = false;
            this.ÖnYüzler_Kaydet.Image = global::İş_ve_Depo_Takip.Properties.Resources.sag;
            this.ÖnYüzler_Kaydet.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ÖnYüzler_Kaydet.Location = new System.Drawing.Point(0, 494);
            this.ÖnYüzler_Kaydet.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.ÖnYüzler_Kaydet.Name = "ÖnYüzler_Kaydet";
            this.ÖnYüzler_Kaydet.Size = new System.Drawing.Size(470, 52);
            this.ÖnYüzler_Kaydet.TabIndex = 10;
            this.ÖnYüzler_Kaydet.Text = "Kaydet";
            this.ÖnYüzler_Kaydet.UseVisualStyleBackColor = true;
            this.ÖnYüzler_Kaydet.Click += new System.EventHandler(this.ÖnYüzler_Kaydet_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(5, 5);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.Liste_Malzemeler);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2.Controls.Add(this.ÖnYüzler_Kaydet);
            this.splitContainer1.Size = new System.Drawing.Size(831, 546);
            this.splitContainer1.SplitterDistance = 357;
            this.splitContainer1.TabIndex = 5;
            // 
            // Liste_Malzemeler
            // 
            this.Liste_Malzemeler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Liste_Malzemeler.Location = new System.Drawing.Point(0, 0);
            this.Liste_Malzemeler.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.Liste_Malzemeler.Name = "Liste_Malzemeler";
            this.Liste_Malzemeler.SeçilenEleman_Adı = null;
            this.Liste_Malzemeler.SeçilenEleman_Adları = ((System.Collections.Generic.List<string>)(resources.GetObject("Liste_Malzemeler.SeçilenEleman_Adları")));
            this.Liste_Malzemeler.Size = new System.Drawing.Size(353, 542);
            this.Liste_Malzemeler.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 127);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.Tablo);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer2.Size = new System.Drawing.Size(470, 367);
            this.splitContainer2.SplitterDistance = 177;
            this.splitContainer2.TabIndex = 8;
            // 
            // Tablo
            // 
            this.Tablo.AllowUserToAddRows = false;
            this.Tablo.AllowUserToDeleteRows = false;
            this.Tablo.AllowUserToResizeColumns = false;
            this.Tablo.AllowUserToResizeRows = false;
            this.Tablo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
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
            this.Tablo_Kullanım,
            this.Tablo_Miktarı});
            this.Tablo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tablo.Location = new System.Drawing.Point(0, 0);
            this.Tablo.Name = "Tablo";
            this.Tablo.ReadOnly = true;
            this.Tablo.RowHeadersVisible = false;
            this.Tablo.RowHeadersWidth = 51;
            this.Tablo.RowTemplate.Height = 24;
            this.Tablo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.Tablo.ShowCellErrors = false;
            this.Tablo.ShowCellToolTips = false;
            this.Tablo.ShowEditingIcon = false;
            this.Tablo.ShowRowErrors = false;
            this.Tablo.Size = new System.Drawing.Size(466, 173);
            this.Tablo.TabIndex = 8;
            // 
            // Tablo_Kullanım
            // 
            this.Tablo_Kullanım.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Tablo_Kullanım.FillWeight = 80F;
            this.Tablo_Kullanım.HeaderText = "Kullanım";
            this.Tablo_Kullanım.MinimumWidth = 6;
            this.Tablo_Kullanım.Name = "Tablo_Kullanım";
            this.Tablo_Kullanım.ReadOnly = true;
            // 
            // Tablo_Miktarı
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Tablo_Miktarı.DefaultCellStyle = dataGridViewCellStyle2;
            this.Tablo_Miktarı.FillWeight = 20F;
            this.Tablo_Miktarı.HeaderText = "Miktar";
            this.Tablo_Miktarı.MinimumWidth = 6;
            this.Tablo_Miktarı.Name = "Tablo_Miktarı";
            this.Tablo_Miktarı.ReadOnly = true;
            this.Tablo_Miktarı.Width = 111;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.DetaylıKullanım);
            this.groupBox2.Controls.Add(this.Notlar);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(466, 182);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Notlar";
            // 
            // DetaylıKullanım
            // 
            this.DetaylıKullanım.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DetaylıKullanım.AutoSize = true;
            this.DetaylıKullanım.Location = new System.Drawing.Point(347, 3);
            this.DetaylıKullanım.Name = "DetaylıKullanım";
            this.DetaylıKullanım.Size = new System.Drawing.Size(113, 33);
            this.DetaylıKullanım.TabIndex = 10;
            this.DetaylıKullanım.Text = "Detaylı";
            this.İpUcu.SetToolTip(this.DetaylıKullanım, "Kullanım miktarlarını detaylı bir şekilde kaydeder.\r\nZaman / Malzeme / Miktar / H" +
        "asta vb.");
            this.DetaylıKullanım.UseVisualStyleBackColor = true;
            this.DetaylıKullanım.CheckedChanged += new System.EventHandler(this.Ayar_Değişti);
            // 
            // Notlar
            // 
            this.Notlar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Notlar.Location = new System.Drawing.Point(3, 32);
            this.Notlar.Multiline = true;
            this.Notlar.Name = "Notlar";
            this.Notlar.Size = new System.Drawing.Size(460, 147);
            this.Notlar.TabIndex = 9;
            this.Notlar.TextChanged += new System.EventHandler(this.Ayar_Değişti);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Miktarı);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.UyarıMiktarı);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.Birimi);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(470, 127);
            this.panel1.TabIndex = 11;
            // 
            // İpUcu
            // 
            this.İpUcu.AutomaticDelay = 0;
            this.İpUcu.UseAnimation = false;
            this.İpUcu.UseFading = false;
            // 
            // Ayarlar_Malzemeler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(15F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(841, 556);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "Ayarlar_Malzemeler";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Malzemeler";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Tablo)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox UyarıMiktarı;
        private System.Windows.Forms.TextBox Birimi;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Miktarı;
        private System.Windows.Forms.Button ÖnYüzler_Kaydet;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox Notlar;
        private System.Windows.Forms.ToolTip İpUcu;
        private System.Windows.Forms.DataGridView Tablo;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tablo_Kullanım;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tablo_Miktarı;
        private System.Windows.Forms.CheckBox DetaylıKullanım;
        private System.Windows.Forms.Panel panel1;
        private ArgeMup.HazirKod.Ekranlar.ListeKutusu Liste_Malzemeler;
    }
}