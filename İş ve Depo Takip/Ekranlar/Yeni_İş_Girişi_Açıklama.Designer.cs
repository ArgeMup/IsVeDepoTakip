using System;

namespace İş_ve_Depo_Takip.Ekranlar
{
    partial class Yeni_İş_Girişi_Açıklama
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Yeni_İş_Girişi_Açıklama));
            this.Ayraç_SolSağ = new System.Windows.Forms.SplitContainer();
            this.Açıklamalar = new ArgeMup.HazirKod.Ekranlar.ListeKutusu();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.SeçiliOlanAçıklama = new System.Windows.Forms.TextBox();
            this.SağaAkar = new System.Windows.Forms.Button();
            this.Çıktı = new System.Windows.Forms.TextBox();
            this.Geri = new System.Windows.Forms.Button();
            this.İpUcu_Genel = new System.Windows.Forms.ToolTip(this.components);
            this.ListeyeEkle = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.Ayraç_SolSağ)).BeginInit();
            this.Ayraç_SolSağ.Panel1.SuspendLayout();
            this.Ayraç_SolSağ.Panel2.SuspendLayout();
            this.Ayraç_SolSağ.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Ayraç_SolSağ
            // 
            this.Ayraç_SolSağ.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Ayraç_SolSağ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Ayraç_SolSağ.Location = new System.Drawing.Point(0, 0);
            this.Ayraç_SolSağ.Name = "Ayraç_SolSağ";
            // 
            // Ayraç_SolSağ.Panel1
            // 
            this.Ayraç_SolSağ.Panel1.Controls.Add(this.Açıklamalar);
            // 
            // Ayraç_SolSağ.Panel2
            // 
            this.Ayraç_SolSağ.Panel2.AutoScroll = true;
            this.Ayraç_SolSağ.Panel2.Controls.Add(this.splitContainer1);
            this.Ayraç_SolSağ.Size = new System.Drawing.Size(633, 226);
            this.Ayraç_SolSağ.SplitterDistance = 160;
            this.Ayraç_SolSağ.TabIndex = 1;
            // 
            // Açıklamalar
            // 
            this.Açıklamalar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Açıklamalar.Location = new System.Drawing.Point(0, 0);
            this.Açıklamalar.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.Açıklamalar.Name = "Açıklamalar";
            this.Açıklamalar.SeçilenEleman_Adı = null;
            this.Açıklamalar.SeçilenEleman_Adları = ((System.Collections.Generic.List<string>)(resources.GetObject("Açıklamalar.SeçilenEleman_Adları")));
            this.Açıklamalar.Size = new System.Drawing.Size(156, 222);
            this.Açıklamalar.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.SeçiliOlanAçıklama);
            this.splitContainer1.Panel1.Controls.Add(this.SağaAkar);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.Çıktı);
            this.splitContainer1.Size = new System.Drawing.Size(469, 226);
            this.splitContainer1.SplitterDistance = 179;
            this.splitContainer1.TabIndex = 2;
            // 
            // SeçiliOlanAçıklama
            // 
            this.SeçiliOlanAçıklama.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SeçiliOlanAçıklama.Location = new System.Drawing.Point(0, 0);
            this.SeçiliOlanAçıklama.Multiline = true;
            this.SeçiliOlanAçıklama.Name = "SeçiliOlanAçıklama";
            this.SeçiliOlanAçıklama.ReadOnly = true;
            this.SeçiliOlanAçıklama.Size = new System.Drawing.Size(175, 193);
            this.SeçiliOlanAçıklama.TabIndex = 1;
            // 
            // SağaAkar
            // 
            this.SağaAkar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.SağaAkar.Image = global::İş_ve_Depo_Takip.Properties.Resources.sag;
            this.SağaAkar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SağaAkar.Location = new System.Drawing.Point(0, 193);
            this.SağaAkar.Margin = new System.Windows.Forms.Padding(2);
            this.SağaAkar.Name = "SağaAkar";
            this.SağaAkar.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.SağaAkar.Size = new System.Drawing.Size(175, 29);
            this.SağaAkar.TabIndex = 12;
            this.SağaAkar.Text = "Sağa aktar";
            this.SağaAkar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.İpUcu_Genel.SetToolTip(this.SağaAkar, "Esc tuşu");
            this.SağaAkar.UseVisualStyleBackColor = true;
            this.SağaAkar.Click += new System.EventHandler(this.SağaAkar_Click);
            // 
            // Çıktı
            // 
            this.Çıktı.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Çıktı.Location = new System.Drawing.Point(0, 0);
            this.Çıktı.Multiline = true;
            this.Çıktı.Name = "Çıktı";
            this.Çıktı.Size = new System.Drawing.Size(282, 222);
            this.Çıktı.TabIndex = 2;
            // 
            // Geri
            // 
            this.Geri.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Geri.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Geri.Image = global::İş_ve_Depo_Takip.Properties.Resources.sol;
            this.Geri.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Geri.Location = new System.Drawing.Point(0, 0);
            this.Geri.Margin = new System.Windows.Forms.Padding(2);
            this.Geri.Name = "Geri";
            this.Geri.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.Geri.Size = new System.Drawing.Size(503, 38);
            this.Geri.TabIndex = 11;
            this.Geri.Text = "Geri";
            this.Geri.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.İpUcu_Genel.SetToolTip(this.Geri, "Esc tuşu");
            this.Geri.UseVisualStyleBackColor = true;
            this.Geri.Click += new System.EventHandler(this.Geri_Click);
            // 
            // İpUcu_Genel
            // 
            this.İpUcu_Genel.AutomaticDelay = 100;
            this.İpUcu_Genel.AutoPopDelay = 10000;
            this.İpUcu_Genel.InitialDelay = 100;
            this.İpUcu_Genel.IsBalloon = true;
            this.İpUcu_Genel.ReshowDelay = 20;
            this.İpUcu_Genel.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.İpUcu_Genel.ToolTipTitle = "Yeni İş Girişi / Düzenleme";
            this.İpUcu_Genel.UseAnimation = false;
            this.İpUcu_Genel.UseFading = false;
            // 
            // ListeyeEkle
            // 
            this.ListeyeEkle.AutoSize = true;
            this.ListeyeEkle.Checked = true;
            this.ListeyeEkle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ListeyeEkle.Dock = System.Windows.Forms.DockStyle.Right;
            this.ListeyeEkle.Location = new System.Drawing.Point(503, 0);
            this.ListeyeEkle.Name = "ListeyeEkle";
            this.ListeyeEkle.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.ListeyeEkle.Size = new System.Drawing.Size(130, 38);
            this.ListeyeEkle.TabIndex = 0;
            this.ListeyeEkle.Text = "Listeye ekle";
            this.ListeyeEkle.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Geri);
            this.panel1.Controls.Add(this.ListeyeEkle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 226);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(633, 38);
            this.panel1.TabIndex = 2;
            // 
            // Yeni_İş_Girişi_Açıklama
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 264);
            this.Controls.Add(this.Ayraç_SolSağ);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Yeni_İş_Girişi_Açıklama";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Açıklama";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Yeni_İş_Girişi_Açıklama_FormClosing);
            this.Ayraç_SolSağ.Panel1.ResumeLayout(false);
            this.Ayraç_SolSağ.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Ayraç_SolSağ)).EndInit();
            this.Ayraç_SolSağ.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip İpUcu_Genel;
        private System.Windows.Forms.SplitContainer Ayraç_SolSağ;
        public System.Windows.Forms.Button Geri;
        private ArgeMup.HazirKod.Ekranlar.ListeKutusu Açıklamalar;
        private System.Windows.Forms.TextBox SeçiliOlanAçıklama;
        private System.Windows.Forms.CheckBox ListeyeEkle;
        private System.Windows.Forms.SplitContainer splitContainer1;
        public System.Windows.Forms.Button SağaAkar;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.TextBox Çıktı;
    }
}