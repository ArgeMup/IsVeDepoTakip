﻿using ArgeMup.HazirKod;
using ArgeMup.HazirKod.Ekİşlemler;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip.Ekranlar
{
    public partial class Tüm_Talepler : Form
    {
        public Tüm_Talepler()
        {
            InitializeComponent();

            Ortak.GeçiciDepolama_PencereKonumları_Oku(this);
        }
        private void Tüm_Talepler_Load(object sender, EventArgs e)
        {
            List<string> l = Banka.Müşteri_Listele();
            string[] l_dizi = l.ToArray();
            İşTakip_Müşteriler.Items.AddRange(l_dizi);
            Seçenekli_Müşteriler.Items.AddRange(l_dizi);
            for (int i = 0; i < l.Count; i++)
            {
                Seçenekli_Müşteriler.SetItemChecked(i, true);
            }

            l = Banka.İşTürü_Listele();
            l_dizi = l.ToArray();
            Seçenekli_İş_Türleri.Items.AddRange(l_dizi);
            for (int i = 0; i < l.Count; i++)
            {
                Seçenekli_İş_Türleri.SetItemChecked(i, true);
            }

            Seçenekli_GirişTarihi_Bitiş.Value = DateTime.Now;
            Seçenekli_GirişTarihi_Başlangıç.Value = Seçenekli_GirişTarihi_Bitiş.Value.AddMonths(-6);

            splitContainer1.Panel1.Controls.Add(P_İşTakip); P_İşTakip.Dock = DockStyle.Fill; P_İşTakip.Visible = true;
            P_İşTakip.Controls.Add(P_İşTakip_DevamEden); P_İşTakip_DevamEden.Dock = DockStyle.Fill; P_İşTakip_DevamEden.Visible = false;
            P_İşTakip.Controls.Add(P_İşTakip_TeslimEdildi); P_İşTakip_TeslimEdildi.Dock = DockStyle.Fill; P_İşTakip_TeslimEdildi.Visible = false;
            P_İşTakip.Controls.Add(P_İşTakip_ÖdemeBekleyen); P_İşTakip_ÖdemeBekleyen.Dock = DockStyle.Fill; P_İşTakip_ÖdemeBekleyen.Visible = false;
            P_İşTakip.Controls.Add(P_İşTakip_Ödendi); P_İşTakip_Ödendi.Dock = DockStyle.Fill; P_İşTakip_Ödendi.Visible = false;
            splitContainer1.Panel1.Controls.Add(P_Arama); P_Arama.Dock = DockStyle.Fill; P_Arama.Visible = false;

            Seviye1_işTakip.Tag = 1;
            Seviye1_Arama.Tag = 2;
            Seviye2_DevamEden.Tag = 10;
            Seviye2_TeslimEdildi.Tag = 11;
            Seviye2_ÖdemeBekleyen.Tag = 12;
            Seviye2_Ödendi.Tag = 13;

            KeyDown += Yeni_Talep_Girişi_Tuş;
            KeyUp += Yeni_Talep_Girişi_Tuş;
            MouseWheel += Yeni_Talep_Girişi_MouseWheel;
            KeyPreview = true;
        }
        bool ctrl_tuşuna_basıldı = false;
        private void Yeni_Talep_Girişi_Tuş(object sender, KeyEventArgs e)
        {
            ctrl_tuşuna_basıldı = e.Control;
        }
        private void Yeni_Talep_Girişi_MouseWheel(object sender, MouseEventArgs e)
        {
            if (ctrl_tuşuna_basıldı)
            {
                WindowState = FormWindowState.Normal;
                if (e.Delta > 0) Font = new Font(Font.FontFamily, Font.Size + 0.2f);
                else Font = new Font(Font.FontFamily, Font.Size - 0.2f);
            }
        }
        private void Tüm_Talepler_Shown(object sender, EventArgs e)
        {
            İşTakip_Müşteriler.DroppedDown = true;
        }
        private void Tüm_Talepler_FormClosed(object sender, FormClosedEventArgs e)
        {
            Ortak.GeçiciDepolama_PencereKonumları_Yaz(this);
        }

        private void Seviye_Değişti(object sender, EventArgs e)
        {
            int no = 1;
            if (sender != null)
            {
                no = (int)(sender as CheckBox).Tag;
                if (no < 1) return;
            }

            Banka_Tablo_ bt;
            Yazdır.Enabled = false;
            Tablo.SuspendLayout();
            Tablo.Rows.Clear();
            İpUcu.SetToolTip(Tablo, "Tümünü seçmek / kaldırmak için çift tıkla");

            switch (no)
            {
                case 1:
                    //iş takip
                    Seviye1_işTakip.Checked = true;
                    Seviye1_Arama.Checked = false;

                    if (Banka.Müşteri_MevcutMu(İşTakip_Müşteriler.Text))
                    {
                        CheckBox c = new CheckBox();
                        for (int i = 10; i <= 13; i++)
                        {
                            c.Tag = i;
                            Seviye_Değişti(c, null);

                            if (Tablo.RowCount > 0) break;
                        }
                    }
                    else
                    {
                        Seviye2_DevamEden.Checked = false;
                        Seviye2_TeslimEdildi.Checked = false;
                        Seviye2_ÖdemeBekleyen.Checked = false;
                        Seviye2_Ödendi.Checked = false;
                    }
                    break;

                case 2:
                    //arama
                    Seviye2_DevamEden.Checked = true;
                    Seviye2_TeslimEdildi.Checked = true;
                    Seviye2_ÖdemeBekleyen.Checked = true;
                    Seviye2_Ödendi.Checked = true;

                    Seviye1_işTakip.Checked = false;
                    Seviye1_Arama.Checked = true;
                    splitContainer1.SplitterDistance = Height / 4;
                    break;

                case 10:
                    //devam eden
                    if (!Seviye1_işTakip.Checked) goto AramayıBaşlat;

                    Seviye2_DevamEden.Checked = true;
                    Seviye2_TeslimEdildi.Checked = false;
                    Seviye2_ÖdemeBekleyen.Checked = false;
                    Seviye2_Ödendi.Checked = false;

                    bt = Banka.Talep_Listele(İşTakip_Müşteriler.Text, Banka.TabloTürü.DevamEden);
                    Banka.Talep_TablodaGöster(Tablo, bt);
                    splitContainer1.SplitterDistance = İşTakip_Etkin_İsaretle_Bitti.PointToScreen(new Point()).Y + İşTakip_Etkin_İsaretle_Bitti.Size.Height;

                    if (Tablo.RowCount < 1) Seviye2_DevamEden.Checked = false;
                    break;

                case 11:
                    //teslim edildi 
                    if (!Seviye1_işTakip.Checked) goto AramayıBaşlat;

                    Seviye2_DevamEden.Checked = false;
                    Seviye2_TeslimEdildi.Checked = true;
                    Seviye2_ÖdemeBekleyen.Checked = false;
                    Seviye2_Ödendi.Checked = false;

                    bt = Banka.Talep_Listele(İşTakip_Müşteriler.Text, Banka.TabloTürü.TeslimEdildi);
                    Banka.Talep_TablodaGöster(Tablo, bt);
                    splitContainer1.SplitterDistance = İşTakip_Bitti_ÖdemeTalebiOluştur.PointToScreen(new Point()).Y + İşTakip_Bitti_ÖdemeTalebiOluştur.Size.Height;

                    if (Tablo.RowCount < 1) Seviye2_TeslimEdildi.Checked = false;
                    break;

                case 12:
                    //ödeme bekliyor
                    if (!Seviye1_işTakip.Checked) goto AramayıBaşlat;

                    Seviye2_DevamEden.Checked = false;
                    Seviye2_TeslimEdildi.Checked = false;
                    Seviye2_ÖdemeBekleyen.Checked = true;
                    Seviye2_Ödendi.Checked = false;

                    İşTakip_ÖdemeBekleyen_Dönem.Text = null;
                    İşTakip_ÖdemeBekleyen_Dönem.Items.Clear();
                    İşTakip_ÖdemeBekleyen_Dönem.Items.AddRange(Banka.Dosya_Listele(İşTakip_Müşteriler.Text, false));
                    if (İşTakip_ÖdemeBekleyen_Dönem.Items.Count > 0)
                    {
                        if (İşTakip_ÖdemeBekleyen_Dönem.SelectedIndex != 0) İşTakip_ÖdemeBekleyen_Dönem.SelectedIndex = 0;
                    }
                    else Seviye2_ÖdemeBekleyen.Checked = false;
                    
                    splitContainer1.SplitterDistance = İşTakip_ÖdemeBekleyen_ÖdendiOlarakİşaretle.PointToScreen(new Point()).Y + İşTakip_ÖdemeBekleyen_ÖdendiOlarakİşaretle.Size.Height;
                    break;

                case 13:
                    //ödendi
                    if (!Seviye1_işTakip.Checked) goto AramayıBaşlat;

                    Seviye2_DevamEden.Checked = false;
                    Seviye2_TeslimEdildi.Checked = false;
                    Seviye2_ÖdemeBekleyen.Checked = false;
                    Seviye2_Ödendi.Checked = true;

                    İşTakip_Ödendi_Dönem.Text = null;
                    İşTakip_Ödendi_Dönem.Items.Clear();
                    İşTakip_Ödendi_Dönem.Items.AddRange(Banka.Dosya_Listele(İşTakip_Müşteriler.Text, true));
                    if (İşTakip_Ödendi_Dönem.Items.Count > 0)
                    {
                        if (İşTakip_Ödendi_Dönem.SelectedIndex != 0) İşTakip_Ödendi_Dönem.SelectedIndex = 0;
                    }
                    else Seviye2_Ödendi.Checked = false;

                    splitContainer1.SplitterDistance = İşTakip_Ödendi_Dönem.PointToScreen(new Point()).Y + İşTakip_Ödendi_Dönem.Size.Height;
                    break;
            }

            P_İşTakip.Visible = Seviye1_işTakip.Checked;
            P_İşTakip_DevamEden.Visible = Seviye2_DevamEden.Checked;
            P_İşTakip_TeslimEdildi.Visible = Seviye2_TeslimEdildi.Checked;
            P_İşTakip_ÖdemeBekleyen.Visible = Seviye2_ÖdemeBekleyen.Checked;
            P_İşTakip_Ödendi.Visible = Seviye2_Ödendi.Checked;
            P_Arama.Visible = Seviye1_Arama.Checked;

            Tablo.ResumeLayout();
            return;

        AramayıBaşlat:
            Tablo.ResumeLayout();
            (sender as CheckBox).Checked = !(sender as CheckBox).Checked;
            Arama_Başlat();
        }
        void Arama_Başlat()
        {

        }

        private void İşTakip_Müşteriler_TextChanged(object sender, EventArgs e)
        {
            CheckBox c = new CheckBox();
            c.Tag = 1;
            Seviye_Değişti(c, null);
        }
        private void İşTakip_DevamEden_Sil_Click(object sender, EventArgs e)
        {
            List<string> l = new List<string>();
            for (int i = 0; i < Tablo.RowCount; i++)
            {
                if ((bool)Tablo[0, i].Value) l.Add((string)Tablo[1, i].Value);
            }
            if (l.Count == 0) return;

            DialogResult Dr = MessageBox.Show("Seçili " + l.Count + " adet öğeyi KALICI OLARAK SİLMEK istediğinize emin misiniz?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (Dr == DialogResult.No) return;

            Banka.Talep_Sil(İşTakip_Müşteriler.Text, l);
            Banka.Değişiklikleri_Kaydet();

        YenidenDene:
            for (int i = 0; i < Tablo.RowCount; i++)
            {
                if ((bool)Tablo[0, i].Value)
                {
                    Tablo.Rows.RemoveAt(i);
                    goto YenidenDene;
                }
            }
        }
        private void İşTakip_DevamEden_Düzenle_Click(object sender, EventArgs e)
        {
            List<string> l = new List<string>();

            for (int i = 0; i < Tablo.RowCount; i++)
            {
                if ((bool)Tablo[0, i].Value)
                {
                    l.Add((string)Tablo[1, i].Value);
                }
            }

            if (l.Count != 1)
            {
                MessageBox.Show("Düzenleme yapılabilmesi için sadece 1 adet talebin seçili olduğundan emin olunuz", Text);
                return;
            }

            new Yeni_Talep_Girişi(İşTakip_Müşteriler.Text, l[0]).ShowDialog();
            Banka.Değişiklikleri_GeriAl();
            Seviye_Değişti(null, null);
        }
        private void İşTakip_DevamEden_İsaretle_Bitti_Click(object sender, EventArgs e)
        {
            List<string> l = new List<string>();
            List<DataGridViewRow> silinecek_satırlar = new List<DataGridViewRow>();

            for (int i = 0; i < Tablo.RowCount; i++)
            {
                if ((bool)Tablo[0, i].Value)
                {
                    l.Add((string)Tablo[1, i].Value);
                    silinecek_satırlar.Add(Tablo.Rows[i]);
                }
            }

            if (l.Count > 0)
            {
                string snç = Banka.Talep_İşaretle_DevamEden_TeslimEdilen(İşTakip_Müşteriler.Text, l, true);
                if (string.IsNullOrEmpty(snç))
                {
                    //başarılı
                    Banka.Değişiklikleri_Kaydet();

                    foreach (DataGridViewRow s in silinecek_satırlar)
                    {
                        Tablo.Rows.Remove(s);
                    }
                }
                else
                {
                    Banka.Değişiklikleri_GeriAl();

                    DialogResult Dr = MessageBox.Show(snç + Environment.NewLine + Environment.NewLine +
                        "Ücretler sayfasını açmak ister misiniz?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (Dr == DialogResult.No) return;

                    new Ücretler().ShowDialog();
                }
            }
        }
        private void İşTakip_TeslimEdildi_İşaretle_Etkin_Click(object sender, EventArgs e)
        {
            List<string> l = new List<string>();

            YenidenDene:
            for (int i = 0; i < Tablo.RowCount; i++)
            {
                if ((bool)Tablo[0, i].Value)
                {
                    l.Add((string)Tablo[1, i].Value);
                    Tablo.Rows.RemoveAt(i);
                    goto YenidenDene;
                }
            }

            if (l.Count > 0)
            {
                Banka.Talep_İşaretle_DevamEden_TeslimEdilen(İşTakip_Müşteriler.Text, l, false);
                Banka.Değişiklikleri_Kaydet();
            }
        }
        private void İşTakip_TeslimEdildi_ÖdemeTalebiOluştur_Click(object sender, EventArgs e)
        {
            double ilave_ödeme_miktar = 0;
            İşTakip_Bitti_İlaveÖdeme_Açıklama.Text = İşTakip_Bitti_İlaveÖdeme_Açıklama.Text.Trim();
            if (!string.IsNullOrEmpty(İşTakip_Bitti_İlaveÖdeme_Açıklama.Text))
            {
                İşTakip_Bitti_İlaveÖdeme_Miktar.Text = İşTakip_Bitti_İlaveÖdeme_Miktar.Text.Trim();

                try
                {
                    if (!string.IsNullOrEmpty(İşTakip_Bitti_İlaveÖdeme_Miktar.Text))
                    {
                        ilave_ödeme_miktar = İşTakip_Bitti_İlaveÖdeme_Miktar.Text.NoktalıSayıya();
                    }
                    else İşTakip_Bitti_İlaveÖdeme_Miktar.Text = null;
                }
                catch (Exception)
                {
                    MessageBox.Show("İlave ödeme Miktar kutucuğu içeriği sayıya dönüştürülemedi", Text);
                    İşTakip_Bitti_İlaveÖdeme_Miktar.Focus();
                    return;
                }
            }
            else İşTakip_Bitti_İlaveÖdeme_Açıklama.Text = null;

            List<string> l = new List<string>();

            YenidenDene:
            for (int i = 0; i < Tablo.RowCount; i++)
            {
                if ((bool)Tablo[0, i].Value)
                {
                    l.Add((string)Tablo[1, i].Value);
                    Tablo.Rows.RemoveAt(i);
                    goto YenidenDene;
                }
            }

            if (l.Count > 0)
            {
                Banka.Talep_İşaretle_ÖdemeTalepEdildi(İşTakip_Müşteriler.Text, l, İşTakip_Bitti_İlaveÖdeme_Açıklama.Text, ilave_ödeme_miktar);
                Banka.Değişiklikleri_Kaydet();

                İşTakip_Bitti_İlaveÖdeme_Açıklama.Text = "";
                İşTakip_Bitti_İlaveÖdeme_Miktar.Text = "";
            }
        }
        private void İşTakip_TeslimEdildi_İlaveÖdeme_Açıklama_TextChanged(object sender, EventArgs e)
        {
            İşTakip_Bitti_İlaveÖdeme_Miktar.Enabled = !string.IsNullOrWhiteSpace(İşTakip_Bitti_İlaveÖdeme_Açıklama.Text);
        }
        private void İşTakip_ÖdemeBekleyen_Dönem_TextChanged(object sender, EventArgs e)
        {
            if (!İşTakip_ÖdemeBekleyen_Dönem.Items.Contains(İşTakip_ÖdemeBekleyen_Dönem.Text))
            {
                İşTakip_ÖdemeBekleyen_İptalEt.Enabled = false;
                İşTakip_ÖdemeBekleyen_ÖdemeAlındı.Enabled = false;
                İpUcu.SetToolTip(Tablo, "Tümünü seçmek / kaldırmak için çift tıkla");
                return;
            }

            İşTakip_ÖdemeBekleyen_İptalEt.Enabled = true;
            İşTakip_ÖdemeBekleyen_ÖdemeAlındı.Enabled = true;

            Banka_Tablo_ bt = Banka.Talep_Listele(İşTakip_Müşteriler.Text, Banka.TabloTürü.ÖdemeTalepEdildi, İşTakip_ÖdemeBekleyen_Dönem.Text);
            Banka.Talep_TablodaGöster(Tablo, bt);

            Banka.Talep_Ayıkla_Ödeme(bt.Ödeme, out List<string> Açıklamalar, out List<string> Ücretler, out string ÖdemeTalepEdildi, out string Ödendi, out string Notlar);
            string ipucu = "";
            for (int i = 0; i < Açıklamalar.Count; i++)
            {
                ipucu += Environment.NewLine + Environment.NewLine + Açıklamalar[i] + " : " + Ücretler[i];
            }
            
            if (!string.IsNullOrEmpty(Notlar))
            {
                ipucu += Environment.NewLine + Environment.NewLine + "Notlar : " + Notlar;
            }
            
            İpUcu.SetToolTip(Tablo, ipucu);

            Yazdır.Enabled = true;
        }
        private void İşTakip_ÖdemeBekleyen_İptalEt_Click(object sender, EventArgs e)
        {
            if (!İşTakip_ÖdemeBekleyen_Dönem.Items.Contains(İşTakip_ÖdemeBekleyen_Dönem.Text)) return;

            DialogResult Dr = MessageBox.Show("Seçilen döneme ait altta listelenmiş işlerin ödeme talebini iptal etmek istediğinize emin misiniz?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (Dr == DialogResult.No) return;

            Banka.Talep_İşaretle_ÖdemeTalepEdildi_TeslimEdildi(İşTakip_Müşteriler.Text, İşTakip_ÖdemeBekleyen_Dönem.Text);
            Banka.Değişiklikleri_Kaydet();
            Tablo.Rows.Clear();
        }
        private void İşTakip_ÖdemeBekleyen_ÖdendiOlarakİşaretle_Click(object sender, EventArgs e)
        {
            if (!İşTakip_ÖdemeBekleyen_Dönem.Items.Contains(İşTakip_ÖdemeBekleyen_Dönem.Text)) return;

            if (string.IsNullOrWhiteSpace(İşTakip_ÖdemeBekleyen_Notlar.Text)) İşTakip_ÖdemeBekleyen_Notlar.Text = null;

            DialogResult Dr = MessageBox.Show("Seçilen döneme ait işleri KALICI olarak ÖDENDİ olarak işaretlemek istediğinize emin misiniz?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (Dr == DialogResult.No) return;

            Banka.Talep_İşaretle_ÖdemeTalepEdildi_Ödendi(İşTakip_Müşteriler.Text, İşTakip_ÖdemeBekleyen_Dönem.Text, İşTakip_ÖdemeBekleyen_Notlar.Text);
            Banka.Değişiklikleri_Kaydet();
            Tablo.Rows.Clear();

            İşTakip_ÖdemeBekleyen_Notlar.Text = "";
        }
        private void İşTakip_Ödendi_Dönem_TextChanged(object sender, EventArgs e)
        {
            if (!İşTakip_Ödendi_Dönem.Items.Contains(İşTakip_Ödendi_Dönem.Text))
            {
                İpUcu.SetToolTip(Tablo, "Tümünü seçmek / kaldırmak için çift tıkla");
                return;
            }

            Banka_Tablo_ bt = Banka.Talep_Listele(İşTakip_Müşteriler.Text, Banka.TabloTürü.Ödendi, İşTakip_Ödendi_Dönem.Text);
            Banka.Talep_TablodaGöster(Tablo, bt);

            Banka.Talep_Ayıkla_Ödeme(bt.Ödeme, out List<string> Açıklamalar, out List<string> Ücretler, out string ÖdemeTalepEdildi, out string Ödendi, out string Notlar);
            string ipucu = "";
            for (int i = 0; i < Açıklamalar.Count; i++)
            {
                ipucu += Environment.NewLine + Environment.NewLine + Açıklamalar[i] + " : " + Ücretler[i];
            }

            if (!string.IsNullOrEmpty(Notlar))
            {
                ipucu += Environment.NewLine + Environment.NewLine + "Notlar : " + Notlar;
            }

            İpUcu.SetToolTip(Tablo, ipucu);

            Yazdır.Enabled = true;
        }

        private void Tablo_DoubleClick(object sender, EventArgs e)
        {
            if (Tablo.RowCount < 1) return;
            bool b = !(bool)Tablo[0, 0].Value;

            for (int i = 0; i < Tablo.RowCount; i++)
            {
                Tablo[0, i].Value = b;
            }
        }
        private void Tablo_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string m = "Notlar";

            if (Tablo.RowCount > 0)
            {
                int seçili = 0;
                for (int i = 0; i < Tablo.RowCount; i++)
                {
                    if ((bool)Tablo[0, i].Value) seçili++;
                }

                m += " ( " + seçili + " / " + Tablo.RowCount + " )";
            }

            Tablo_Notlar.HeaderText = m;
        }

        private void Yazdır_Click(object sender, EventArgs e)
        {
            ArgeMup.HazirKod.Depo_ depo;
            string DosyaAdı;

            if (Seviye2_ÖdemeBekleyen.Checked)
            {
                depo = Banka.Tablo(İşTakip_Müşteriler.Text, Banka.TabloTürü.ÖdemeTalepEdildi, false, İşTakip_ÖdemeBekleyen_Dönem.Text);
                DosyaAdı = Ortak.Klasör_Pdf + İşTakip_Müşteriler.Text + "\\Ödeme Talep Edildi_" + İşTakip_ÖdemeBekleyen_Dönem.Text.Replace(' ', '_') + ".pdf";
            }
            else if (Seviye2_Ödendi.Checked)
            {
                depo = Banka.Tablo(İşTakip_Müşteriler.Text, Banka.TabloTürü.Ödendi, false, İşTakip_Ödendi_Dönem.Text);
                DosyaAdı = Ortak.Klasör_Pdf + İşTakip_Müşteriler.Text + "\\Ödendi_" + İşTakip_Ödendi_Dönem.Text.Replace(' ', '_') + ".pdf";
            }
            else return;

            Klasör.Oluştur(Path.GetDirectoryName(DosyaAdı));

            Yazdırma y = new Yazdırma();
            y.Yazdırma_Load(null, null);
            y.Yazdır_Depo(depo, DosyaAdı);

            try
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                Thread.Sleep(100);
                System.Diagnostics.Process.Start(DosyaAdı);
            }
            catch (Exception) { }
        }
    }
}
