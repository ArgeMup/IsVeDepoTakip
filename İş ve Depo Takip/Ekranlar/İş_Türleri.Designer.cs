namespace İş_ve_Depo_Takip
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
            this.Liste = new System.Windows.Forms.ListBox();
            this.Sil = new System.Windows.Forms.Button();
            this.Yeni = new System.Windows.Forms.TextBox();
            this.Ekle = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Liste
            // 
            this.Liste.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Liste.FormattingEnabled = true;
            this.Liste.ItemHeight = 29;
            this.Liste.Location = new System.Drawing.Point(15, 14);
            this.Liste.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Liste.Name = "Liste";
            this.Liste.Size = new System.Drawing.Size(652, 265);
            this.Liste.Sorted = true;
            this.Liste.TabIndex = 0;
            this.Liste.SelectedValueChanged += new System.EventHandler(this.Liste_SelectedValueChanged);
            // 
            // Sil
            // 
            this.Sil.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Sil.Enabled = false;
            this.Sil.Image = global::İş_ve_Depo_Takip.Properties.Resources.sil;
            this.Sil.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Sil.Location = new System.Drawing.Point(459, 292);
            this.Sil.Name = "Sil";
            this.Sil.Size = new System.Drawing.Size(208, 52);
            this.Sil.TabIndex = 1;
            this.Sil.Text = "Sil";
            this.Sil.UseVisualStyleBackColor = true;
            this.Sil.Click += new System.EventHandler(this.Sil_Click);
            // 
            // Yeni
            // 
            this.Yeni.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Yeni.Location = new System.Drawing.Point(12, 350);
            this.Yeni.Name = "Yeni";
            this.Yeni.Size = new System.Drawing.Size(652, 36);
            this.Yeni.TabIndex = 2;
            this.Yeni.TextChanged += new System.EventHandler(this.Yeni_TextChanged);
            // 
            // Ekle
            // 
            this.Ekle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Ekle.Enabled = false;
            this.Ekle.Image = global::İş_ve_Depo_Takip.Properties.Resources.sag;
            this.Ekle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Ekle.Location = new System.Drawing.Point(459, 392);
            this.Ekle.Name = "Ekle";
            this.Ekle.Size = new System.Drawing.Size(208, 52);
            this.Ekle.TabIndex = 1;
            this.Ekle.Text = "Ekle";
            this.Ekle.UseVisualStyleBackColor = true;
            this.Ekle.Click += new System.EventHandler(this.Ekle_Click);
            // 
            // İş_Türleri
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(15F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 454);
            this.Controls.Add(this.Yeni);
            this.Controls.Add(this.Ekle);
            this.Controls.Add(this.Sil);
            this.Controls.Add(this.Liste);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "İş_Türleri";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "İş Türleri";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.İş_Türleri_FormClosed);
            this.Load += new System.EventHandler(this.İş_Türleri_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox Liste;
        private System.Windows.Forms.Button Sil;
        private System.Windows.Forms.TextBox Yeni;
        private System.Windows.Forms.Button Ekle;
    }
}