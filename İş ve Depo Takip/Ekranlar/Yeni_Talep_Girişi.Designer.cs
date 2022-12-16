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
            this.Tablo_İş_Türü = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Tablo_Ücret = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tablo_Giriş_Tarihi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Müşteriler = new System.Windows.Forms.ComboBox();
            this.Kaydet = new System.Windows.Forms.Button();
            this.Seçili_Satırı_Sil = new System.Windows.Forms.Button();
            this.Hasta = new System.Windows.Forms.TextBox();
            this.İskonto = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.İpUcu = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.Tablo)).BeginInit();
            this.SuspendLayout();
            // 
            // Notlar
            // 
            this.Notlar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Notlar.Location = new System.Drawing.Point(20, 149);
            this.Notlar.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Notlar.Multiline = true;
            this.Notlar.Name = "Notlar";
            this.Notlar.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.Notlar.Size = new System.Drawing.Size(806, 86);
            this.Notlar.TabIndex = 1;
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
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
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
            this.Tablo.Location = new System.Drawing.Point(20, 245);
            this.Tablo.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Tablo.Name = "Tablo";
            this.Tablo.RowHeadersVisible = false;
            this.Tablo.RowHeadersWidth = 51;
            this.Tablo.RowTemplate.Height = 24;
            this.Tablo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Tablo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Tablo.ShowCellErrors = false;
            this.Tablo.ShowCellToolTips = false;
            this.Tablo.ShowEditingIcon = false;
            this.Tablo.ShowRowErrors = false;
            this.Tablo.Size = new System.Drawing.Size(806, 186);
            this.Tablo.TabIndex = 2;
            this.Tablo.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tablo_CellValueChanged);
            this.Tablo.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.Tablo_EditingControlShowing);
            this.Tablo.SelectionChanged += new System.EventHandler(this.Tablo_SelectionChanged);
            // 
            // Tablo_İş_Türü
            // 
            this.Tablo_İş_Türü.AutoComplete = false;
            this.Tablo_İş_Türü.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Tablo_İş_Türü.FillWeight = 70F;
            this.Tablo_İş_Türü.HeaderText = "İş Türü";
            this.Tablo_İş_Türü.MinimumWidth = 6;
            this.Tablo_İş_Türü.Name = "Tablo_İş_Türü";
            this.Tablo_İş_Türü.Sorted = true;
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
            this.Tablo_Giriş_Tarihi.Width = 141;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 29);
            this.label1.TabIndex = 3;
            this.label1.Text = "Müşteri";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 60);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 29);
            this.label2.TabIndex = 4;
            this.label2.Text = "Hasta";
            // 
            // Müşteriler
            // 
            this.Müşteriler.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Müşteriler.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.Müşteriler.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.Müşteriler.FormattingEnabled = true;
            this.Müşteriler.Location = new System.Drawing.Point(163, 12);
            this.Müşteriler.Name = "Müşteriler";
            this.Müşteriler.Size = new System.Drawing.Size(663, 37);
            this.Müşteriler.TabIndex = 0;
            this.Müşteriler.SelectedIndexChanged += new System.EventHandler(this.Müşteriler_SelectedIndexChanged);
            // 
            // Kaydet
            // 
            this.Kaydet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Kaydet.Enabled = false;
            this.Kaydet.Location = new System.Drawing.Point(476, 439);
            this.Kaydet.Name = "Kaydet";
            this.Kaydet.Size = new System.Drawing.Size(350, 42);
            this.Kaydet.TabIndex = 3;
            this.Kaydet.Text = "Kaydet";
            this.Kaydet.UseVisualStyleBackColor = true;
            this.Kaydet.Click += new System.EventHandler(this.Kaydet_Click);
            // 
            // Seçili_Satırı_Sil
            // 
            this.Seçili_Satırı_Sil.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Seçili_Satırı_Sil.Location = new System.Drawing.Point(20, 439);
            this.Seçili_Satırı_Sil.Name = "Seçili_Satırı_Sil";
            this.Seçili_Satırı_Sil.Size = new System.Drawing.Size(350, 42);
            this.Seçili_Satırı_Sil.TabIndex = 8;
            this.Seçili_Satırı_Sil.Text = "Seçili Satırı Sil";
            this.Seçili_Satırı_Sil.UseVisualStyleBackColor = true;
            this.Seçili_Satırı_Sil.Click += new System.EventHandler(this.Seçili_Satırı_Sil_Click);
            // 
            // Hasta
            // 
            this.Hasta.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Hasta.Location = new System.Drawing.Point(163, 57);
            this.Hasta.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Hasta.Name = "Hasta";
            this.Hasta.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.Hasta.Size = new System.Drawing.Size(663, 36);
            this.Hasta.TabIndex = 9;
            this.Hasta.TextChanged += new System.EventHandler(this.Değişiklik_Yapılıyor);
            this.Hasta.Leave += new System.EventHandler(this.Hasta_Leave);
            // 
            // İskonto
            // 
            this.İskonto.Location = new System.Drawing.Point(163, 103);
            this.İskonto.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.İskonto.Name = "İskonto";
            this.İskonto.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.İskonto.Size = new System.Drawing.Size(153, 36);
            this.İskonto.TabIndex = 11;
            this.İskonto.TextChanged += new System.EventHandler(this.Değişiklik_Yapılıyor);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 106);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 29);
            this.label3.TabIndex = 10;
            this.label3.Text = "İskonto %";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(746, 106);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 29);
            this.label4.TabIndex = 12;
            this.label4.Text = "Notlar";
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
            // Yeni_Talep_Girişi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(15F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 493);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.İskonto);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Hasta);
            this.Controls.Add(this.Seçili_Satırı_Sil);
            this.Controls.Add(this.Kaydet);
            this.Controls.Add(this.Müşteriler);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Tablo);
            this.Controls.Add(this.Notlar);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "Yeni_Talep_Girişi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Yeni İş Girişi / Düzenleme";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Yeni_Talep_Girişi_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Yeni_Talep_Girişi_FormClosed);
            this.Load += new System.EventHandler(this.Yeni_Talep_Girişi_Load);
            this.Shown += new System.EventHandler(this.Yeni_Talep_Girişi_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.Tablo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox Notlar;
        private System.Windows.Forms.DataGridView Tablo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox Müşteriler;
        private System.Windows.Forms.Button Kaydet;
        private System.Windows.Forms.Button Seçili_Satırı_Sil;
        private System.Windows.Forms.TextBox Hasta;
        private System.Windows.Forms.TextBox İskonto;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewComboBoxColumn Tablo_İş_Türü;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tablo_Ücret;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tablo_Giriş_Tarihi;
        private System.Windows.Forms.ToolTip İpUcu;
    }
}