using ArgeMup.HazirKod;
using ArgeMup.HazirKod.Ekİşlemler;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
            Ortak.BeklemeGöstergesi = TabloİçeriğiArama;

            List<string> l = Banka.Müşteri_Listele();
            string[] l_dizi = l.ToArray();
            İşTakip_Müşteriler.Items.AddRange(l_dizi);
            Arama_Müşteriler.Items.AddRange(l_dizi);
            for (int i = 0; i < l.Count; i++)
            {
                Arama_Müşteriler.SetItemChecked(i, true);
            }

            l = Banka.İşTürü_Listele();
            l_dizi = l.ToArray();
            Arama_İş_Türleri.Items.AddRange(l_dizi);
            for (int i = 0; i < l.Count; i++)
            {
                Arama_İş_Türleri.SetItemChecked(i, true);
            }

            DateTime t = DateTime.Now;
            Arama_GirişTarihi_Bitiş.Value = new DateTime(t.Year, t.Month, t.Day, 23, 59, 59);
            Arama_GirişTarihi_Başlangıç.Value = new DateTime(t.Year - 1, 5, 19);

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

                    for (int i = 0; i < Arama_Müşteriler.Items.Count; i++)
                    {
                        Arama_Müşteriler.SetItemChecked(i, true);
                    }
                    for (int i = 0; i < Arama_İş_Türleri.Items.Count; i++)
                    {
                        Arama_İş_Türleri.SetItemChecked(i, true);
                    }

                    Seviye1_işTakip.Checked = false;
                    Seviye1_Arama.Checked = true;
                    splitContainer1.SplitterDistance = Height / 4;
                    break;

                case 10:
                    //devam eden
                    if (!Seviye1_işTakip.Checked) goto AramaİçinSeçenekleriBelirle;

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
                    if (!Seviye1_işTakip.Checked) goto AramaİçinSeçenekleriBelirle;

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
                    if (!Seviye1_işTakip.Checked) goto AramaİçinSeçenekleriBelirle;

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
                    if (!Seviye1_işTakip.Checked) goto AramaİçinSeçenekleriBelirle;

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

            return;

        AramaİçinSeçenekleriBelirle:
            (sender as CheckBox).Checked = !(sender as CheckBox).Checked;
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
        private void Tablo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || e.ColumnIndex > 0) return;

            Tablo[0, e.RowIndex].Value = !(bool)Tablo[0, e.RowIndex].Value;
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
            string gerçekdosyadı;
            
            if (Seviye2_ÖdemeBekleyen.Checked)
            {
                depo = Banka.Tablo(İşTakip_Müşteriler.Text, Banka.TabloTürü.ÖdemeTalepEdildi, false, İşTakip_ÖdemeBekleyen_Dönem.Text);
                gerçekdosyadı = "Ödeme Talep Edildi_" + İşTakip_ÖdemeBekleyen_Dönem.Text.Replace(' ', '_') + ".pdf";
            }
            else if (Seviye2_Ödendi.Checked)
            {
                depo = Banka.Tablo(İşTakip_Müşteriler.Text, Banka.TabloTürü.Ödendi, false, İşTakip_Ödendi_Dönem.Text);
                gerçekdosyadı = "Ödendi_" + İşTakip_Ödendi_Dönem.Text.Replace(' ', '_') + ".pdf";
            }
            else return;

            string dosyayolu = Ortak.Klasör_Gecici + Path.GetRandomFileName() + ".pdf";

            Yazdırma y = new Yazdırma();
            y.Yazdırma_Load(null, null);
            y.Yazdır_Depo(depo, dosyayolu);

            if (!string.IsNullOrEmpty(Ortak.Klasör_Pdf)) Dosya.Kopyala(dosyayolu, Ortak.Klasör_Pdf + İşTakip_Müşteriler.Text + "\\" + gerçekdosyadı);
            
            Ortak.Pdf_AçmayaÇalış(dosyayolu);

            if (!Ortak.Eposta_hesabı_mevcut) return;
            IDepo_Eleman m = Banka.Tablo_Dal(null, Banka.TabloTürü.Ayarlar, "Müşteriler/" + İşTakip_Müşteriler.Text);
            if (m == null || string.IsNullOrEmpty(m.Oku("Eposta/Kime") + m.Oku("Eposta/Bilgi") + m.Oku("Eposta/Gizli"))) return;

            DialogResult Dr = MessageBox.Show("Belgeyi müşterinize eposta ile göndermek ister misiniz?" + 
                Environment.NewLine + Environment.NewLine + 
                İşTakip_Müşteriler.Text, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (Dr == DialogResult.No) return;

            string ek_dosyası = Ortak.Klasör_Gecici + DateTime.Now.Yazıya(ArgeMup.HazirKod.Dönüştürme.D_TarihSaat.Şablon_DosyaAdı) + ".pdf";
            File.Move(dosyayolu, ek_dosyası);

            Ayarlar_Eposta epst = new Ayarlar_Eposta();
            epst.Ayarlar_Eposta_Load(null, null);
            string snç = epst.EpostaGönder(m, ek_dosyası);
            if (!string.IsNullOrEmpty(snç)) MessageBox.Show(snç, Text);
        }

        bool TabloİçeriğiArama_Çalışıyor = false;
        bool TabloİçeriğiArama_KapatmaTalebi = false;
        int TabloİçeriğiArama_Tik = 0;
        int TabloİçeriğiArama_Sayac_Bulundu = 0;
        private void TabloİçeriğiArama_TextChanged(object sender, EventArgs e)
        {
            TabloİçeriğiArama_Tik = Environment.TickCount + 100;
            if (TabloİçeriğiArama.Text.Length < 2)
            {
                if (TabloİçeriğiArama_Sayac_Bulundu != 0)
                {
                    TabloİçeriğiArama.BackColor = Color.Salmon;

                    for (int satır = 0; satır < Tablo.RowCount; satır++)
                    {
                        Tablo.Rows[satır].Visible = true;
                        if (TabloİçeriğiArama_Tik < Environment.TickCount) { Application.DoEvents(); TabloİçeriğiArama_Tik = Environment.TickCount + 100; }
                    }

                    TabloİçeriğiArama.BackColor = Color.White;
                    TabloİçeriğiArama_Sayac_Bulundu = 0;
                    Tablo_CellValueChanged(null, new DataGridViewCellEventArgs(0, 0));
                }

                return;
            }

            if (TabloİçeriğiArama_Çalışıyor) { TabloİçeriğiArama_KapatmaTalebi = true; return; }

            TabloİçeriğiArama_Çalışıyor = true;
            TabloİçeriğiArama_KapatmaTalebi = false;
            TabloİçeriğiArama_Sayac_Bulundu = 0;
            TabloİçeriğiArama_Tik = Environment.TickCount + 500;
            TabloİçeriğiArama.BackColor = Color.Salmon;

            string aranan = TabloİçeriğiArama.Text.ToLower();
            for (int satır = 0; satır < Tablo.RowCount && !TabloİçeriğiArama_KapatmaTalebi; satır++)
            {
                bool bulundu = false;
                for (int sutun = 1; sutun < Tablo.Columns.Count; sutun++)
                {
                    string içerik = (string)Tablo[sutun, satır].Value;
                    if (string.IsNullOrEmpty(içerik)) Tablo[sutun, satır].Style.BackColor = Color.White;
                    else if (içerik.ToLower().Contains(aranan))
                    {
                        Tablo[sutun, satır].Style.BackColor = Color.YellowGreen;
                        bulundu = true;
                    }
                    else Tablo[sutun, satır].Style.BackColor = Color.White;
                }

                Tablo.Rows[satır].Visible = bulundu;
                if (bulundu) TabloİçeriğiArama_Sayac_Bulundu++;

                if (TabloİçeriğiArama_Tik < Environment.TickCount) { Application.DoEvents(); TabloİçeriğiArama_Tik = Environment.TickCount + 500; }
            }

            if (TabloİçeriğiArama_Sayac_Bulundu == 0) TabloİçeriğiArama_Sayac_Bulundu = -1;
            else Tablo_Notlar.HeaderText = "Notlar ( 0 / " + TabloİçeriğiArama_Sayac_Bulundu + " )";

            TabloİçeriğiArama.BackColor = Color.White;
            TabloİçeriğiArama_Çalışıyor = false;
            Tablo.ClearSelection();

            if (TabloİçeriğiArama_KapatmaTalebi) TabloİçeriğiArama_TextChanged(null, null);
            TabloİçeriğiArama_KapatmaTalebi = false;
        }

        bool Arama_Sorgula_Çalışıyor = false;
        bool Arama_Sorgula_KapatmaTalebi = false;
        int Arama_Sorgula_Tik = 0;
        string Arama_Sorgula_Aranan_İşTürleri = null;
        private void Arama_Sorgula_Click(object sender, EventArgs e)
        {
            if (Arama_Sorgula_Çalışıyor) { Arama_Sorgula_KapatmaTalebi = true; return; }
            Arama_Sorgula_Çalışıyor = true;
            Arama_Sorgula_KapatmaTalebi = false;
            Arama_Sorgula_Tik = Environment.TickCount + 500;
            TabloİçeriğiArama.BackColor = Color.Salmon;

            if (Arama_GirişTarihi_Başlangıç.Value > Arama_GirişTarihi_Bitiş.Value)
            {
                DateTime gecici = Arama_GirişTarihi_Başlangıç.Value;
                Arama_GirişTarihi_Başlangıç.Value = Arama_GirişTarihi_Bitiş.Value;
                Arama_GirişTarihi_Bitiş.Value = gecici;
            }
            if (Arama_Müşteriler.CheckedItems.Count == 0)
            {
                for (int i = 0; i < Arama_Müşteriler.Items.Count; i++)
                {
                    Arama_Müşteriler.SetItemChecked(i, true);
                }
            }
            if (Arama_İş_Türleri.CheckedItems.Count == 0)
            {
                for (int i = 0; i < Arama_İş_Türleri.Items.Count; i++)
                {
                    Arama_İş_Türleri.SetItemChecked(i, true);
                }
            }
            Arama_Sorgula_Aranan_İşTürleri = "";
            for (int i = 0; i < Arama_İş_Türleri.Items.Count; i++)
            {
                if (Arama_İş_Türleri.GetItemChecked(i)) Arama_Sorgula_Aranan_İşTürleri += "-_" + Arama_İş_Türleri.Items[i].ToString() + "_-";
            }
            if (!Seviye2_DevamEden.Checked && !Seviye2_TeslimEdildi.Checked && !Seviye2_ÖdemeBekleyen.Checked && !Seviye2_Ödendi.Checked)
            {
                Seviye2_DevamEden.Checked = true;
            }

            Arama_İlerlemeÇubuğu.Minimum = 0;
            Arama_İlerlemeÇubuğu.Value = 0;
            Arama_İlerlemeÇubuğu.Maximum = 0;
            Arama_İlerlemeÇubuğu.Visible = true;
            for (int i = 0; i < Arama_Müşteriler.Items.Count && !Arama_Sorgula_KapatmaTalebi; i++)
            {
                if (Arama_Müşteriler.GetItemChecked(i) && !Arama_Sorgula_KapatmaTalebi)
                {
                    if (Seviye2_DevamEden.Checked) Arama_İlerlemeÇubuğu.Maximum += 1;
                    if (Seviye2_TeslimEdildi.Checked) Arama_İlerlemeÇubuğu.Maximum += 1;
                    if (Seviye2_ÖdemeBekleyen.Checked) Arama_İlerlemeÇubuğu.Maximum += Banka.Dosya_Listele(Arama_Müşteriler.Items[i].ToString(), false).Length;
                    if (Seviye2_Ödendi.Checked) Arama_İlerlemeÇubuğu.Maximum += Banka.Dosya_Listele(Arama_Müşteriler.Items[i].ToString(), true).Length;
                }

                if (Arama_Sorgula_Tik < Environment.TickCount) { Application.DoEvents(); Arama_Sorgula_Tik = Environment.TickCount + 500; }
            }

            Banka_Tablo_ bt = new Banka_Tablo_(null);
            bt.Türü = Banka.TabloTürü.DevamEden_TeslimEdildi_ÖdemeTalepEdildi_Ödendi;
            Banka.Talep_TablodaGöster(Tablo, bt);
            Tablo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            Tablo.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            for (int i = 0; i < Arama_Müşteriler.Items.Count && !Arama_Sorgula_KapatmaTalebi; i++)
            {
                if (Arama_Müşteriler.GetItemChecked(i))
                {
                    if (Seviye2_DevamEden.Checked && !Arama_Sorgula_KapatmaTalebi)
                    {
                        Arama_İlerlemeÇubuğu.Value++;
                        Arama_Sorgula_Click_2(Banka.Talep_Listele(Arama_Müşteriler.Items[i].ToString(), Banka.TabloTürü.DevamEden));
                    }

                    if (Seviye2_TeslimEdildi.Checked && !Arama_Sorgula_KapatmaTalebi)
                    {
                        Arama_İlerlemeÇubuğu.Value++;
                        Arama_Sorgula_Click_2(Banka.Talep_Listele(Arama_Müşteriler.Items[i].ToString(), Banka.TabloTürü.TeslimEdildi));
                    }

                    if (Seviye2_ÖdemeBekleyen.Checked && !Arama_Sorgula_KapatmaTalebi)
                    {
                        string[] l = Banka.Dosya_Listele(Arama_Müşteriler.Items[i].ToString(), false);

                        for (int s = 0; s < l.Length && !Arama_Sorgula_KapatmaTalebi; s++)
                        {
                            Arama_İlerlemeÇubuğu.Value++;
                            DateTime t = l[s].TarihSaate("dd MM yyyy HH mm ss");
                            if (Arama_GirişTarihi_Başlangıç.Value > t || t > Arama_GirişTarihi_Bitiş.Value) continue;

                            Arama_Sorgula_Click_2(Banka.Talep_Listele(Arama_Müşteriler.Items[i].ToString(), Banka.TabloTürü.ÖdemeTalepEdildi, l[s]));
                        }
                    }

                    if (Seviye2_Ödendi.Checked && !Arama_Sorgula_KapatmaTalebi)
                    {
                        string[] l = Banka.Dosya_Listele(Arama_Müşteriler.Items[i].ToString(), true);

                        for (int s = 0; s < l.Length && !Arama_Sorgula_KapatmaTalebi; s++)
                        {
                            Arama_İlerlemeÇubuğu.Value++;
                            DateTime t = l[s].TarihSaate("dd MM yyyy HH mm ss");
                            if (Arama_GirişTarihi_Başlangıç.Value > t || t > Arama_GirişTarihi_Bitiş.Value) continue;

                            Arama_Sorgula_Click_2(Banka.Talep_Listele(Arama_Müşteriler.Items[i].ToString(), Banka.TabloTürü.Ödendi, l[s]));
                        }
                    }
                }
            }

            Tablo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            Tablo.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            Tablo.AutoResizeColumns();

            Arama_İlerlemeÇubuğu.Visible = false;
            Arama_Sorgula_Çalışıyor = false;
            TabloİçeriğiArama.BackColor = Color.White;
        }
        private void Arama_Sorgula_Click_2(Banka_Tablo_ bt)
        {
            if (Arama_Sorgula_KapatmaTalebi) return;
            if (Arama_Sorgula_Tik < Environment.TickCount) { Application.DoEvents(); Arama_Sorgula_Tik = Environment.TickCount + 500; }

            List<IDepo_Eleman> uyuşanlar = new List<IDepo_Eleman>();
            foreach (IDepo_Eleman serino in bt.Talepler)
            {
                bool evet = false;

                foreach (IDepo_Eleman iş in serino.Elemanları)
                {
                    if (!Arama_Sorgula_Aranan_İşTürleri.Contains(iş[0])) continue;

                    DateTime t = iş[1].TarihSaate();
                    if (Arama_GirişTarihi_Başlangıç.Value > t || t > Arama_GirişTarihi_Bitiş.Value) continue;

                    evet = true;
                    break;
                }

                if (evet) uyuşanlar.Add(serino);
            }

            if (uyuşanlar.Count == 0) return;
            bt.Talepler = uyuşanlar;
            Banka.Talep_TablodaGöster(Tablo, bt, false);
        }

        private void Arama_Müşteriler_İşler_SelectedIndexChanged(object sender, EventArgs e)
        {
            (sender as CheckedListBox).SelectedIndex = -1;
        }
    }
}
