namespace İş_ve_Depo_Takip.Ekranlar
{
    partial class Parola_Kontrol
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
            this.tab_sayfası = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.P_Parola = new System.Windows.Forms.Panel();
            this.Tuş_ParolaKontrol = new System.Windows.Forms.Button();
            this.Parola_Giriş = new System.Windows.Forms.MaskedTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.P_YeniParola = new System.Windows.Forms.Panel();
            this.YeniParola_Etiket = new System.Windows.Forms.Label();
            this.YeniParola_1 = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Tuş_YeniParola_Kaydet = new System.Windows.Forms.Button();
            this.YeniParola_2 = new System.Windows.Forms.MaskedTextBox();
            this.Hata = new System.Windows.Forms.ErrorProvider(this.components);
            this.İpUcu = new System.Windows.Forms.ToolTip(this.components);
            this.tab_sayfası.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.P_Parola.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.P_YeniParola.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Hata)).BeginInit();
            this.SuspendLayout();
            // 
            // tab_sayfası
            // 
            this.tab_sayfası.Controls.Add(this.tabPage1);
            this.tab_sayfası.Controls.Add(this.tabPage2);
            this.tab_sayfası.Location = new System.Drawing.Point(12, 12);
            this.tab_sayfası.Name = "tab_sayfası";
            this.tab_sayfası.SelectedIndex = 0;
            this.tab_sayfası.Size = new System.Drawing.Size(366, 319);
            this.tab_sayfası.TabIndex = 11;
            this.tab_sayfası.Visible = false;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.P_Parola);
            this.tabPage1.Location = new System.Drawing.Point(4, 38);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(358, 277);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // P_Parola
            // 
            this.P_Parola.BackgroundImage = global::İş_ve_Depo_Takip.Properties.Resources.logo_512_seffaf;
            this.P_Parola.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.P_Parola.Controls.Add(this.Tuş_ParolaKontrol);
            this.P_Parola.Controls.Add(this.Parola_Giriş);
            this.P_Parola.Location = new System.Drawing.Point(6, 6);
            this.P_Parola.Name = "P_Parola";
            this.P_Parola.Size = new System.Drawing.Size(338, 236);
            this.P_Parola.TabIndex = 11;
            this.P_Parola.Visible = false;
            // 
            // Tuş_ParolaKontrol
            // 
            this.Tuş_ParolaKontrol.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Tuş_ParolaKontrol.Location = new System.Drawing.Point(175, 190);
            this.Tuş_ParolaKontrol.Margin = new System.Windows.Forms.Padding(6);
            this.Tuş_ParolaKontrol.Name = "Tuş_ParolaKontrol";
            this.Tuş_ParolaKontrol.Size = new System.Drawing.Size(100, 40);
            this.Tuş_ParolaKontrol.TabIndex = 1;
            this.Tuş_ParolaKontrol.Text = "Giriş";
            this.Tuş_ParolaKontrol.UseVisualStyleBackColor = true;
            this.Tuş_ParolaKontrol.Click += new System.EventHandler(this.Parola_Kontrol_Click);
            // 
            // Parola_Giriş
            // 
            this.Parola_Giriş.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Parola_Giriş.Location = new System.Drawing.Point(66, 193);
            this.Parola_Giriş.Name = "Parola_Giriş";
            this.Parola_Giriş.Size = new System.Drawing.Size(100, 36);
            this.Parola_Giriş.TabIndex = 0;
            this.Parola_Giriş.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Parola_Giriş.UseSystemPasswordChar = true;
            this.Parola_Giriş.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Parola_Giriş_KeyUp);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.P_YeniParola);
            this.tabPage2.Location = new System.Drawing.Point(4, 38);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(358, 277);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // P_YeniParola
            // 
            this.P_YeniParola.Controls.Add(this.YeniParola_Etiket);
            this.P_YeniParola.Controls.Add(this.YeniParola_1);
            this.P_YeniParola.Controls.Add(this.label1);
            this.P_YeniParola.Controls.Add(this.Tuş_YeniParola_Kaydet);
            this.P_YeniParola.Controls.Add(this.YeniParola_2);
            this.P_YeniParola.Location = new System.Drawing.Point(6, 6);
            this.P_YeniParola.Name = "P_YeniParola";
            this.P_YeniParola.Size = new System.Drawing.Size(347, 246);
            this.P_YeniParola.TabIndex = 10;
            this.P_YeniParola.Tag = "";
            this.P_YeniParola.Visible = false;
            // 
            // YeniParola_Etiket
            // 
            this.YeniParola_Etiket.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.YeniParola_Etiket.AutoSize = true;
            this.YeniParola_Etiket.Location = new System.Drawing.Point(27, 14);
            this.YeniParola_Etiket.Name = "YeniParola_Etiket";
            this.YeniParola_Etiket.Size = new System.Drawing.Size(207, 29);
            this.YeniParola_Etiket.TabIndex = 9;
            this.YeniParola_Etiket.Text = "Bir parola seçiniz";
            // 
            // YeniParola_1
            // 
            this.YeniParola_1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.YeniParola_1.Location = new System.Drawing.Point(32, 46);
            this.YeniParola_1.Name = "YeniParola_1";
            this.YeniParola_1.Size = new System.Drawing.Size(283, 36);
            this.YeniParola_1.TabIndex = 0;
            this.YeniParola_1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.YeniParola_1.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(250, 29);
            this.label1.TabIndex = 7;
            this.label1.Text = "Parolayı tekrar giriniz";
            // 
            // Tuş_YeniParola_Kaydet
            // 
            this.Tuş_YeniParola_Kaydet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Tuş_YeniParola_Kaydet.Location = new System.Drawing.Point(32, 163);
            this.Tuş_YeniParola_Kaydet.Margin = new System.Windows.Forms.Padding(6);
            this.Tuş_YeniParola_Kaydet.Name = "Tuş_YeniParola_Kaydet";
            this.Tuş_YeniParola_Kaydet.Size = new System.Drawing.Size(283, 68);
            this.Tuş_YeniParola_Kaydet.TabIndex = 2;
            this.Tuş_YeniParola_Kaydet.Text = "Yeni Parolayı Kaydet";
            this.Tuş_YeniParola_Kaydet.UseVisualStyleBackColor = true;
            this.Tuş_YeniParola_Kaydet.Click += new System.EventHandler(this.YeniParola_Kaydet_Click);
            // 
            // YeniParola_2
            // 
            this.YeniParola_2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.YeniParola_2.Location = new System.Drawing.Point(32, 118);
            this.YeniParola_2.Name = "YeniParola_2";
            this.YeniParola_2.Size = new System.Drawing.Size(283, 36);
            this.YeniParola_2.TabIndex = 1;
            this.YeniParola_2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.YeniParola_2.UseSystemPasswordChar = true;
            // 
            // Hata
            // 
            this.Hata.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.AlwaysBlink;
            this.Hata.ContainerControl = this;
            // 
            // İpUcu
            // 
            this.İpUcu.AutomaticDelay = 100;
            this.İpUcu.AutoPopDelay = 10000;
            this.İpUcu.InitialDelay = 100;
            this.İpUcu.ReshowDelay = 20;
            // 
            // Parola_Kontrol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(15F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 349);
            this.Controls.Add(this.tab_sayfası);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.Name = "Parola_Kontrol";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Parola_Kontrol_FormClosing);
            this.Shown += new System.EventHandler(this.Parola_Kontrol_Shown);
            this.Resize += new System.EventHandler(this.Parola_Kontrol_Resize);
            this.tab_sayfası.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.P_Parola.ResumeLayout(false);
            this.P_Parola.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.P_YeniParola.ResumeLayout(false);
            this.P_YeniParola.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Hata)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tab_sayfası;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ErrorProvider Hata;
        private System.Windows.Forms.ToolTip İpUcu;
        private System.Windows.Forms.Panel P_Parola;
        private System.Windows.Forms.Button Tuş_ParolaKontrol;
        private System.Windows.Forms.MaskedTextBox Parola_Giriş;
        private System.Windows.Forms.Panel P_YeniParola;
        private System.Windows.Forms.Label YeniParola_Etiket;
        private System.Windows.Forms.MaskedTextBox YeniParola_1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Tuş_YeniParola_Kaydet;
        private System.Windows.Forms.MaskedTextBox YeniParola_2;
    }
}

