namespace İş_ve_Depo_Takip.Ekranlar
{
    partial class Ayarlar_Kullanıcılar
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
            this.Ekran = new ArgeMup.HazirKod.Ekranlar.Kullanıcılar();
            this.SuspendLayout();
            // 
            // Ekran
            // 
            this.Ekran.AutoScroll = true;
            this.Ekran.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Ekran.Location = new System.Drawing.Point(3, 3);
            this.Ekran.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Ekran.Name = "Ekran";
            this.Ekran.Size = new System.Drawing.Size(633, 392);
            this.Ekran.TabIndex = 0;
            // 
            // Ayarlar_Kullanıcılar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 398);
            this.Controls.Add(this.Ekran);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Name = "Ayarlar_Kullanıcılar";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kullanıcılar";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ayarlar_Kullanıcılar_FormClosed);
            this.Shown += new System.EventHandler(this.Ayarlar_Kullanıcılar_Shown);
            this.ResumeLayout(false);

        }

        #endregion
        private global::ArgeMup.HazirKod.Ekranlar.Kullanıcılar Ekran;
    }
}