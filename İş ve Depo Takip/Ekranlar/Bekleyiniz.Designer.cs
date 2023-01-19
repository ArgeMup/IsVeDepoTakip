namespace İş_ve_Depo_Takip.Ekranlar
{
    partial class Bekleyiniz
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
            this.İptalEt = new System.Windows.Forms.Button();
            this.Mesaj = new System.Windows.Forms.Label();
            this.İlerlemeÇubuğu = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // İptalEt
            // 
            this.İptalEt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.İptalEt.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.İptalEt.Enabled = false;
            this.İptalEt.Image = global::İş_ve_Depo_Takip.Properties.Resources.sil;
            this.İptalEt.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.İptalEt.Location = new System.Drawing.Point(6, 87);
            this.İptalEt.Margin = new System.Windows.Forms.Padding(0);
            this.İptalEt.Name = "İptalEt";
            this.İptalEt.Size = new System.Drawing.Size(387, 43);
            this.İptalEt.TabIndex = 0;
            this.İptalEt.Text = "İptal Et";
            this.İptalEt.UseVisualStyleBackColor = true;
            this.İptalEt.Click += new System.EventHandler(this.İptalEt_Click);
            // 
            // Mesaj
            // 
            this.Mesaj.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Mesaj.Location = new System.Drawing.Point(6, 6);
            this.Mesaj.Margin = new System.Windows.Forms.Padding(0);
            this.Mesaj.Name = "Mesaj";
            this.Mesaj.Size = new System.Drawing.Size(387, 61);
            this.Mesaj.TabIndex = 0;
            this.Mesaj.Text = "mesaj";
            this.Mesaj.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // İlerlemeÇubuğu
            // 
            this.İlerlemeÇubuğu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.İlerlemeÇubuğu.Location = new System.Drawing.Point(6, 69);
            this.İlerlemeÇubuğu.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.İlerlemeÇubuğu.Name = "İlerlemeÇubuğu";
            this.İlerlemeÇubuğu.Size = new System.Drawing.Size(387, 17);
            this.İlerlemeÇubuğu.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.İlerlemeÇubuğu.TabIndex = 0;
            // 
            // Bekleyiniz
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.İptalEt;
            this.ClientSize = new System.Drawing.Size(398, 134);
            this.ControlBox = false;
            this.Controls.Add(this.İlerlemeÇubuğu);
            this.Controls.Add(this.İptalEt);
            this.Controls.Add(this.Mesaj);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Bekleyiniz";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bekleyiniz";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button İptalEt;
        private System.Windows.Forms.Label Mesaj;
        private System.Windows.Forms.ProgressBar İlerlemeÇubuğu;
    }
}