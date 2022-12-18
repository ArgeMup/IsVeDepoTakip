namespace İş_ve_Depo_Takip.Ekranlar
{
    partial class Ayarlar_Diğer
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
            this.Kaydet = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Klasör_Pdf = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Klasör_Yedekleme = new System.Windows.Forms.TextBox();
            this.AçılışEkranıİçinParaloİste = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Kaydet
            // 
            this.Kaydet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Kaydet.Enabled = false;
            this.Kaydet.Image = global::İş_ve_Depo_Takip.Properties.Resources.sag;
            this.Kaydet.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Kaydet.Location = new System.Drawing.Point(15, 300);
            this.Kaydet.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Kaydet.Name = "Kaydet";
            this.Kaydet.Size = new System.Drawing.Size(1005, 63);
            this.Kaydet.TabIndex = 20;
            this.Kaydet.Text = "Kaydet";
            this.Kaydet.UseVisualStyleBackColor = true;
            this.Kaydet.Click += new System.EventHandler(this.Kaydet_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 34);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 29);
            this.label1.TabIndex = 21;
            this.label1.Text = "Yedekleme";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.Klasör_Pdf);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.Klasör_Yedekleme);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(15, 14);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.groupBox1.Size = new System.Drawing.Size(1005, 211);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Klasörler";
            // 
            // Klasör_Pdf
            // 
            this.Klasör_Pdf.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Klasör_Pdf.Location = new System.Drawing.Point(17, 155);
            this.Klasör_Pdf.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.Klasör_Pdf.Name = "Klasör_Pdf";
            this.Klasör_Pdf.Size = new System.Drawing.Size(979, 36);
            this.Klasör_Pdf.TabIndex = 24;
            this.Klasör_Pdf.TextChanged += new System.EventHandler(this.Ayar_Değişti);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 122);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(420, 29);
            this.label2.TabIndex = 23;
            this.label2.Text = "Pdf Yazdırma (...\\Müşteri\\Dosya.pdf)";
            // 
            // Klasör_Yedekleme
            // 
            this.Klasör_Yedekleme.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Klasör_Yedekleme.Location = new System.Drawing.Point(17, 67);
            this.Klasör_Yedekleme.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.Klasör_Yedekleme.Name = "Klasör_Yedekleme";
            this.Klasör_Yedekleme.Size = new System.Drawing.Size(979, 36);
            this.Klasör_Yedekleme.TabIndex = 22;
            this.Klasör_Yedekleme.TextChanged += new System.EventHandler(this.Ayar_Değişti);
            // 
            // AçılışEkranıİçinParaloİste
            // 
            this.AçılışEkranıİçinParaloİste.AutoSize = true;
            this.AçılışEkranıİçinParaloİste.Checked = true;
            this.AçılışEkranıİçinParaloİste.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AçılışEkranıİçinParaloİste.Location = new System.Drawing.Point(15, 246);
            this.AçılışEkranıİçinParaloİste.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.AçılışEkranıİçinParaloİste.Name = "AçılışEkranıİçinParaloİste";
            this.AçılışEkranıİçinParaloİste.Size = new System.Drawing.Size(488, 33);
            this.AçılışEkranıİçinParaloİste.TabIndex = 23;
            this.AçılışEkranıİçinParaloİste.Text = "Açılış ekranı küçültüldüğünde parola iste";
            this.AçılışEkranıİçinParaloİste.UseVisualStyleBackColor = true;
            this.AçılışEkranıİçinParaloİste.CheckedChanged += new System.EventHandler(this.Ayar_Değişti);
            // 
            // Ayarlar_Diğer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(15F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1035, 374);
            this.Controls.Add(this.AçılışEkranıİçinParaloİste);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Kaydet);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "Ayarlar_Diğer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ayarlar Diğer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Ayarlar_Diğer_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ayarlar_Diğer_FormClosed);
            this.Load += new System.EventHandler(this.Ayarlar_Diğer_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Kaydet;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox Klasör_Pdf;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Klasör_Yedekleme;
        private System.Windows.Forms.CheckBox AçılışEkranıİçinParaloİste;
    }
}