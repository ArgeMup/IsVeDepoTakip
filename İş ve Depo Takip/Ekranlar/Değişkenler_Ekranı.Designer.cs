namespace İş_ve_Depo_Takip.Ekranlar
{
    partial class Değişkenler_Ekranı
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Değişkenler_Ekranı));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Kaydet = new System.Windows.Forms.Button();
            this.TabloİçeriğiArama = new System.Windows.Forms.TextBox();
            this.Tablo = new System.Windows.Forms.DataGridView();
            this.Tablo_Adı = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tablo_İçeriği = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tablo_Değeri = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.İpUcu_Genel = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.Tablo)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Kaydet
            // 
            this.Kaydet.AutoSize = true;
            this.Kaydet.Dock = System.Windows.Forms.DockStyle.Left;
            this.Kaydet.Enabled = false;
            this.Kaydet.Location = new System.Drawing.Point(0, 0);
            this.Kaydet.Name = "Kaydet";
            this.Kaydet.Size = new System.Drawing.Size(267, 46);
            this.Kaydet.TabIndex = 18;
            this.Kaydet.Text = "Kaydet";
            this.Kaydet.UseVisualStyleBackColor = true;
            this.Kaydet.Click += new System.EventHandler(this.Kaydet_Click);
            // 
            // TabloİçeriğiArama
            // 
            this.TabloİçeriğiArama.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabloİçeriğiArama.Location = new System.Drawing.Point(267, 0);
            this.TabloİçeriğiArama.Name = "TabloİçeriğiArama";
            this.TabloİçeriğiArama.Size = new System.Drawing.Size(701, 34);
            this.TabloİçeriğiArama.TabIndex = 16;
            this.TabloİçeriğiArama.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.İpUcu_Genel.SetToolTip(this.TabloİçeriğiArama, resources.GetString("TabloİçeriğiArama.ToolTip"));
            this.TabloİçeriğiArama.TextChanged += new System.EventHandler(this.TabloİçeriğiArama_TextChanged);
            this.TabloİçeriğiArama.DoubleClick += new System.EventHandler(this.TabloİçeriğiArama_DoubleClick);
            // 
            // Tablo
            // 
            this.Tablo.AllowUserToResizeColumns = false;
            this.Tablo.AllowUserToResizeRows = false;
            this.Tablo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
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
            this.Tablo_Adı,
            this.Tablo_İçeriği,
            this.Tablo_Değeri});
            this.Tablo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tablo.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.Tablo.Location = new System.Drawing.Point(7, 53);
            this.Tablo.Name = "Tablo";
            this.Tablo.RowHeadersVisible = false;
            this.Tablo.RowHeadersWidth = 51;
            this.Tablo.RowTemplate.Height = 24;
            this.Tablo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.Tablo.ShowCellErrors = false;
            this.Tablo.ShowEditingIcon = false;
            this.Tablo.ShowRowErrors = false;
            this.Tablo.Size = new System.Drawing.Size(968, 493);
            this.Tablo.TabIndex = 17;
            this.Tablo.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tablo_CellValueChanged);
            // 
            // Tablo_Adı
            // 
            this.Tablo_Adı.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Tablo_Adı.FillWeight = 20F;
            this.Tablo_Adı.HeaderText = "Adı";
            this.Tablo_Adı.MinimumWidth = 6;
            this.Tablo_Adı.Name = "Tablo_Adı";
            this.Tablo_Adı.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Tablo_Adı.Width = 54;
            // 
            // Tablo_İçeriği
            // 
            this.Tablo_İçeriği.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Tablo_İçeriği.FillWeight = 20F;
            this.Tablo_İçeriği.HeaderText = "İçeriği";
            this.Tablo_İçeriği.MinimumWidth = 6;
            this.Tablo_İçeriği.Name = "Tablo_İçeriği";
            this.Tablo_İçeriği.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Tablo_Değeri
            // 
            this.Tablo_Değeri.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tablo_Değeri.DefaultCellStyle = dataGridViewCellStyle2;
            this.Tablo_Değeri.FillWeight = 20F;
            this.Tablo_Değeri.HeaderText = "Değeri";
            this.Tablo_Değeri.MinimumWidth = 6;
            this.Tablo_Değeri.Name = "Tablo_Değeri";
            this.Tablo_Değeri.ReadOnly = true;
            this.Tablo_Değeri.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Tablo_Değeri.Width = 92;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.TabloİçeriğiArama);
            this.panel1.Controls.Add(this.Kaydet);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(7, 7);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(968, 46);
            this.panel1.TabIndex = 19;
            // 
            // İpUcu_Genel
            // 
            this.İpUcu_Genel.AutomaticDelay = 100;
            this.İpUcu_Genel.AutoPopDelay = 10000;
            this.İpUcu_Genel.InitialDelay = 100;
            this.İpUcu_Genel.IsBalloon = true;
            this.İpUcu_Genel.ReshowDelay = 20;
            this.İpUcu_Genel.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.İpUcu_Genel.ToolTipTitle = "Değişkenler";
            this.İpUcu_Genel.UseAnimation = false;
            this.İpUcu_Genel.UseFading = false;
            // 
            // Değişkenler_Ekranı
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 553);
            this.Controls.Add(this.Tablo);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "Değişkenler_Ekranı";
            this.Padding = new System.Windows.Forms.Padding(7);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Değişkenler";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Değişkenler_Ekranı_FormClosed);
            this.Shown += new System.EventHandler(this.Değişkenler_Ekranı_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.Tablo)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button Kaydet;
        private System.Windows.Forms.TextBox TabloİçeriğiArama;
        private System.Windows.Forms.DataGridView Tablo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolTip İpUcu_Genel;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tablo_Adı;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tablo_İçeriği;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tablo_Değeri;
    }
}