namespace İş_ve_Depo_Takip.Ekranlar
{
    partial class Müşteriler
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Müşteriler));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Eposta_Gizli = new System.Windows.Forms.TextBox();
            this.Eposta_Bilgi = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Eposta_Kime = new System.Windows.Forms.TextBox();
            this.ÖnYüzler_Kaydet = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.Liste_Müşteriler = new ArgeMup.HazirKod.Ekranlar.ListeKutusu();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Notlar = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.Liste_Müşteriler_AltGrup = new ArgeMup.HazirKod.Ekranlar.ListeKutusu();
            this.Liste_Müşteriler_AltGrup_SıralaVeYazdır = new System.Windows.Forms.CheckBox();
            this.İpUcu = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Eposta_Gizli);
            this.groupBox1.Controls.Add(this.Eposta_Bilgi);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.Eposta_Kime);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 400);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox1.Size = new System.Drawing.Size(346, 144);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "E-posta";
            // 
            // Eposta_Gizli
            // 
            this.Eposta_Gizli.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Eposta_Gizli.Location = new System.Drawing.Point(86, 103);
            this.Eposta_Gizli.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Eposta_Gizli.Name = "Eposta_Gizli";
            this.Eposta_Gizli.Size = new System.Drawing.Size(320, 30);
            this.Eposta_Gizli.TabIndex = 5;
            this.İpUcu.SetToolTip(this.Eposta_Gizli, "noktalı virgül ile ayrılmış birden fazla adres girilebilir");
            this.Eposta_Gizli.TextChanged += new System.EventHandler(this.Ayar_Değişti);
            // 
            // Eposta_Bilgi
            // 
            this.Eposta_Bilgi.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Eposta_Bilgi.Location = new System.Drawing.Point(86, 66);
            this.Eposta_Bilgi.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Eposta_Bilgi.Name = "Eposta_Bilgi";
            this.Eposta_Bilgi.Size = new System.Drawing.Size(320, 30);
            this.Eposta_Bilgi.TabIndex = 4;
            this.İpUcu.SetToolTip(this.Eposta_Bilgi, "noktalı virgül ile ayrılmış birden fazla adres girilebilir");
            this.Eposta_Bilgi.TextChanged += new System.EventHandler(this.Ayar_Değişti);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 69);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 25);
            this.label3.TabIndex = 3;
            this.label3.Text = "Bilgi";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 105);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Gizli";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 33);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Kime";
            // 
            // Eposta_Kime
            // 
            this.Eposta_Kime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Eposta_Kime.Location = new System.Drawing.Point(86, 30);
            this.Eposta_Kime.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Eposta_Kime.Name = "Eposta_Kime";
            this.Eposta_Kime.Size = new System.Drawing.Size(320, 30);
            this.Eposta_Kime.TabIndex = 0;
            this.İpUcu.SetToolTip(this.Eposta_Kime, "noktalı virgül ile ayrılmış birden fazla adres girilebilir");
            this.Eposta_Kime.TextChanged += new System.EventHandler(this.Ayar_Değişti);
            // 
            // ÖnYüzler_Kaydet
            // 
            this.ÖnYüzler_Kaydet.AutoSize = true;
            this.ÖnYüzler_Kaydet.Dock = System.Windows.Forms.DockStyle.Top;
            this.ÖnYüzler_Kaydet.Enabled = false;
            this.ÖnYüzler_Kaydet.Image = global::İş_ve_Depo_Takip.Properties.Resources.sag;
            this.ÖnYüzler_Kaydet.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ÖnYüzler_Kaydet.Location = new System.Drawing.Point(0, 544);
            this.ÖnYüzler_Kaydet.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.ÖnYüzler_Kaydet.Name = "ÖnYüzler_Kaydet";
            this.ÖnYüzler_Kaydet.Size = new System.Drawing.Size(346, 45);
            this.ÖnYüzler_Kaydet.TabIndex = 4;
            this.ÖnYüzler_Kaydet.Text = "Kaydet";
            this.ÖnYüzler_Kaydet.UseVisualStyleBackColor = true;
            this.ÖnYüzler_Kaydet.Click += new System.EventHandler(this.ÖnYüzler_Kaydet_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(4, 4);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.Liste_Müşteriler);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.ÖnYüzler_Kaydet);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer1.Size = new System.Drawing.Size(774, 445);
            this.splitContainer1.SplitterDistance = 400;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 5;
            // 
            // Liste_Müşteriler
            // 
            this.Liste_Müşteriler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Liste_Müşteriler.Location = new System.Drawing.Point(0, 0);
            this.Liste_Müşteriler.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.Liste_Müşteriler.Name = "Liste_Müşteriler";
            this.Liste_Müşteriler.SeçilenEleman_Adı = null;
            this.Liste_Müşteriler.SeçilenEleman_Adları = ((System.Collections.Generic.List<string>)(resources.GetObject("Liste_Müşteriler.SeçilenEleman_Adları")));
            this.Liste_Müşteriler.Size = new System.Drawing.Size(396, 441);
            this.Liste_Müşteriler.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Notlar);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 250);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox2.Size = new System.Drawing.Size(346, 150);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Notlar";
            // 
            // Notlar
            // 
            this.Notlar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Notlar.Location = new System.Drawing.Point(2, 26);
            this.Notlar.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Notlar.Multiline = true;
            this.Notlar.Name = "Notlar";
            this.Notlar.Size = new System.Drawing.Size(342, 121);
            this.Notlar.TabIndex = 6;
            this.Notlar.TextChanged += new System.EventHandler(this.Ayar_Değişti);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.Liste_Müşteriler_AltGrup);
            this.groupBox3.Controls.Add(this.Liste_Müşteriler_AltGrup_SıralaVeYazdır);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox3.Size = new System.Drawing.Size(346, 250);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Alt Grup";
            // 
            // Liste_Müşteriler_AltGrup
            // 
            this.Liste_Müşteriler_AltGrup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Liste_Müşteriler_AltGrup.Location = new System.Drawing.Point(5, 28);
            this.Liste_Müşteriler_AltGrup.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Liste_Müşteriler_AltGrup.Name = "Liste_Müşteriler_AltGrup";
            this.Liste_Müşteriler_AltGrup.SeçilenEleman_Adı = null;
            this.Liste_Müşteriler_AltGrup.SeçilenEleman_Adları = ((System.Collections.Generic.List<string>)(resources.GetObject("Liste_Müşteriler_AltGrup.SeçilenEleman_Adları")));
            this.Liste_Müşteriler_AltGrup.Size = new System.Drawing.Size(336, 188);
            this.Liste_Müşteriler_AltGrup.TabIndex = 7;
            // 
            // Liste_Müşteriler_AltGrup_SıralaVeYazdır
            // 
            this.Liste_Müşteriler_AltGrup_SıralaVeYazdır.AutoSize = true;
            this.Liste_Müşteriler_AltGrup_SıralaVeYazdır.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Liste_Müşteriler_AltGrup_SıralaVeYazdır.Location = new System.Drawing.Point(5, 216);
            this.Liste_Müşteriler_AltGrup_SıralaVeYazdır.Name = "Liste_Müşteriler_AltGrup_SıralaVeYazdır";
            this.Liste_Müşteriler_AltGrup_SıralaVeYazdır.Size = new System.Drawing.Size(336, 29);
            this.Liste_Müşteriler_AltGrup_SıralaVeYazdır.TabIndex = 8;
            this.Liste_Müşteriler_AltGrup_SıralaVeYazdır.Text = "Yazdırırken alt grubu da göster";
            this.Liste_Müşteriler_AltGrup_SıralaVeYazdır.UseVisualStyleBackColor = true;
            this.Liste_Müşteriler_AltGrup_SıralaVeYazdır.CheckedChanged += new System.EventHandler(this.Ayar_Değişti);
            // 
            // İpUcu
            // 
            this.İpUcu.AutomaticDelay = 0;
            this.İpUcu.UseAnimation = false;
            this.İpUcu.UseFading = false;
            // 
            // Müşteriler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 453);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "Müşteriler";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Müşteriler";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox Eposta_Gizli;
        private System.Windows.Forms.TextBox Eposta_Bilgi;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Eposta_Kime;
        private System.Windows.Forms.Button ÖnYüzler_Kaydet;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox Notlar;
        private System.Windows.Forms.ToolTip İpUcu;
        private ArgeMup.HazirKod.Ekranlar.ListeKutusu Liste_Müşteriler;
        private ArgeMup.HazirKod.Ekranlar.ListeKutusu Liste_Müşteriler_AltGrup;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox Liste_Müşteriler_AltGrup_SıralaVeYazdır;
    }
}