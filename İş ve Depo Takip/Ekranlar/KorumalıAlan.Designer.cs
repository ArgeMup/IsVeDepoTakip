namespace İş_ve_Depo_Takip.Ekranlar
{
    partial class KorumalıAlan
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
            this.Liste = new System.Windows.Forms.ListBox();
            this.Sil = new System.Windows.Forms.Button();
            this.MasaüstüneKopyala = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.AramaÇubuğu = new System.Windows.Forms.TextBox();
            this.Sürümler = new System.Windows.Forms.ListBox();
            this.İçeriAl = new System.Windows.Forms.Button();
            this.İpUcu = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Liste
            // 
            this.Liste.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Liste.FormattingEnabled = true;
            this.Liste.ItemHeight = 25;
            this.Liste.Location = new System.Drawing.Point(0, 30);
            this.Liste.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Liste.Name = "Liste";
            this.Liste.Size = new System.Drawing.Size(374, 335);
            this.Liste.Sorted = true;
            this.Liste.TabIndex = 0;
            this.İpUcu.SetToolTip(this.Liste, "Korunan içerik listesi\r\n\r\nKorumak istediğiniz dosya veya klasörleri\r\nburaya sürük" +
        "le - bırak yapabilirsiniz");
            this.Liste.SelectedValueChanged += new System.EventHandler(this.Liste_SelectedValueChanged);
            // 
            // Sil
            // 
            this.Sil.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Sil.Enabled = false;
            this.Sil.Image = global::İş_ve_Depo_Takip.Properties.Resources.sil;
            this.Sil.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Sil.Location = new System.Drawing.Point(0, 365);
            this.Sil.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Sil.Name = "Sil";
            this.Sil.Size = new System.Drawing.Size(374, 45);
            this.Sil.TabIndex = 1;
            this.Sil.Text = "Sil";
            this.İpUcu.SetToolTip(this.Sil, "Dosyanızı tüm sürümleriyle birlikte KALICI olarak siler.");
            this.Sil.UseVisualStyleBackColor = true;
            this.Sil.Click += new System.EventHandler(this.Sil_Click);
            // 
            // MasaüstüneKopyala
            // 
            this.MasaüstüneKopyala.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.MasaüstüneKopyala.Enabled = false;
            this.MasaüstüneKopyala.Image = global::İş_ve_Depo_Takip.Properties.Resources.sag;
            this.MasaüstüneKopyala.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.MasaüstüneKopyala.Location = new System.Drawing.Point(0, 365);
            this.MasaüstüneKopyala.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MasaüstüneKopyala.Name = "MasaüstüneKopyala";
            this.MasaüstüneKopyala.Size = new System.Drawing.Size(357, 45);
            this.MasaüstüneKopyala.TabIndex = 4;
            this.MasaüstüneKopyala.Text = "Masa Üstüne Kopyala";
            this.İpUcu.SetToolTip(this.MasaüstüneKopyala, "Seçtiğiniz sürümü masaüstünüze kopyalar.");
            this.MasaüstüneKopyala.UseVisualStyleBackColor = true;
            this.MasaüstüneKopyala.Click += new System.EventHandler(this.MasaüstüneKopyala_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(5, 77);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.Liste);
            this.splitContainer1.Panel1.Controls.Add(this.AramaÇubuğu);
            this.splitContainer1.Panel1.Controls.Add(this.Sil);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.Sürümler);
            this.splitContainer1.Panel2.Controls.Add(this.MasaüstüneKopyala);
            this.splitContainer1.Panel2.Enabled = false;
            this.splitContainer1.Size = new System.Drawing.Size(742, 414);
            this.splitContainer1.SplitterDistance = 378;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 5;
            // 
            // AramaÇubuğu
            // 
            this.AramaÇubuğu.Dock = System.Windows.Forms.DockStyle.Top;
            this.AramaÇubuğu.Location = new System.Drawing.Point(0, 0);
            this.AramaÇubuğu.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.AramaÇubuğu.Name = "AramaÇubuğu";
            this.AramaÇubuğu.Size = new System.Drawing.Size(374, 30);
            this.AramaÇubuğu.TabIndex = 4;
            this.İpUcu.SetToolTip(this.AramaÇubuğu, "Arama çubuğu");
            this.AramaÇubuğu.TextChanged += new System.EventHandler(this.AramaÇubuğu_TextChanged);
            // 
            // Sürümler
            // 
            this.Sürümler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Sürümler.FormattingEnabled = true;
            this.Sürümler.ItemHeight = 25;
            this.Sürümler.Location = new System.Drawing.Point(0, 0);
            this.Sürümler.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Sürümler.Name = "Sürümler";
            this.Sürümler.Size = new System.Drawing.Size(357, 365);
            this.Sürümler.TabIndex = 7;
            this.İpUcu.SetToolTip(this.Sürümler, "Seçtiğiniz dosyaya ait tüm eski sürümler listelenmektedir");
            this.Sürümler.SelectedIndexChanged += new System.EventHandler(this.Sürümler_SelectedIndexChanged);
            this.Sürümler.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Sürümler_MouseDoubleClick);
            // 
            // İçeriAl
            // 
            this.İçeriAl.Dock = System.Windows.Forms.DockStyle.Top;
            this.İçeriAl.Image = global::İş_ve_Depo_Takip.Properties.Resources.sol_mavi;
            this.İçeriAl.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.İçeriAl.Location = new System.Drawing.Point(5, 5);
            this.İçeriAl.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.İçeriAl.Name = "İçeriAl";
            this.İçeriAl.Size = new System.Drawing.Size(742, 72);
            this.İçeriAl.TabIndex = 8;
            this.İçeriAl.Text = "Masa üstünde bulunan korunan içeriği yeni bir sürüm olarak içeri al\r\nve masa üstü" +
    "ndeki kopyalarını KALICI olarak SİL";
            this.İçeriAl.UseVisualStyleBackColor = true;
            this.İçeriAl.Click += new System.EventHandler(this.İçeriAl_Click);
            // 
            // İpUcu
            // 
            this.İpUcu.AutomaticDelay = 0;
            this.İpUcu.UseAnimation = false;
            this.İpUcu.UseFading = false;
            // 
            // KorumalıAlan
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 496);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.İçeriAl);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "KorumalıAlan";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Korumalı Alan";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.KorumalıAlan_FormClosing);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.KorumalıAlan_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.KorumalıAlan_DragEnter);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox Liste;
        private System.Windows.Forms.Button Sil;
        private System.Windows.Forms.Button MasaüstüneKopyala;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolTip İpUcu;
        private System.Windows.Forms.TextBox AramaÇubuğu;
        private System.Windows.Forms.ListBox Sürümler;
        private System.Windows.Forms.Button İçeriAl;
    }
}