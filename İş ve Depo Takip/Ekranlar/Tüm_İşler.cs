using ArgeMup.HazirKod;
using ArgeMup.HazirKod.Dönüştürme;
using ArgeMup.HazirKod.Ekİşlemler;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip.Ekranlar
{
    public partial class Tüm_İşler : Form
    {
        public Tüm_İşler()
        {
            InitializeComponent();

            Ortak.GeçiciDepolama_PencereKonumları_Oku(this);
        
            Ortak.Gösterge_UzunİşlemİçinBekleyiniz = TabloİçeriğiArama;

            İşTakip_Müşteriler_AramaÇubuğu_Liste = Banka.Müşteri_Listele();
            string[] l_dizi = İşTakip_Müşteriler_AramaÇubuğu_Liste.ToArray();
            İşTakip_Müşteriler.Items.AddRange(l_dizi);
            Arama_Müşteriler.Items.AddRange(l_dizi);

            Arama_İş_Türleri.Items.AddRange(Banka.İşTürü_Listele().ToArray());

            DateTime t = DateTime.Now;
            Arama_GirişTarihi_Bitiş.Value = new DateTime(t.Year, t.Month, t.Day, 23, 59, 59);
            Arama_GirişTarihi_Başlangıç.Value = new DateTime(t.Year - 1, 5, 19);

            P_Üst_Alt.Panel1.Controls.Add(P_SolOrta_Sağ); P_SolOrta_Sağ.Dock = DockStyle.Fill; P_SolOrta_Sağ.Visible = true;
            P_Sol_Orta.Panel2.Controls.Add(P_İşTakip_DevamEden); P_İşTakip_DevamEden.Dock = DockStyle.Fill; P_İşTakip_DevamEden.Visible = false;
            P_Sol_Orta.Panel2.Controls.Add(P_İşTakip_TeslimEdildi); P_İşTakip_TeslimEdildi.Dock = DockStyle.Fill; P_İşTakip_TeslimEdildi.Visible = false;
            P_Sol_Orta.Panel2.Controls.Add(P_İşTakip_ÖdemeBekleyen); P_İşTakip_ÖdemeBekleyen.Dock = DockStyle.Fill; P_İşTakip_ÖdemeBekleyen.Visible = false;
            P_Sol_Orta.Panel2.Controls.Add(P_İşTakip_Ödendi); P_İşTakip_Ödendi.Dock = DockStyle.Fill; P_İşTakip_Ödendi.Visible = false;
            P_Üst_Alt.Panel1.Controls.Add(P_Arama); P_Arama.Dock = DockStyle.Fill; P_Arama.Visible = false;

            P_SolOrta_Sağ.SplitterDistance = P_SolOrta_Sağ.Width * 3 / 4; //müşteriler, tuşlar + yazdırma
            P_Sol_Orta.SplitterDistance = P_Sol_Orta.Width / 2; //müşteriler + tuşları
            P_Üst_Alt.SplitterDistance = Height / 3; //tuşlar + tablo

            Seviye1_işTakip.Tag = 1;
            Seviye1_Arama.Tag = 2;
            Seviye2_DevamEden.Tag = 10;
            Seviye2_TeslimEdildi.Tag = 11;
            Seviye2_ÖdemeBekleyen.Tag = 12;
            Seviye2_Ödendi.Tag = 13;
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
            Tablo.Rows.Clear();
            İpUcu.SetToolTip(Tablo, "Tümünü seçmek / kaldırmak için çift tıkla");

            switch (no)
            {
                case 1:
                    //iş takip
                    Seviye1_işTakip.Checked = true;
                    Seviye1_Arama.Checked = false;

                    CheckBox c = null;
                    if (Seviye2_DevamEden.Checked) c = Seviye2_DevamEden;
                    else if (Seviye2_TeslimEdildi.Checked) c = Seviye2_TeslimEdildi;
                    else if (Seviye2_ÖdemeBekleyen.Checked) c = Seviye2_ÖdemeBekleyen;
                    else if (Seviye2_Ödendi.Checked) c = Seviye2_Ödendi;

                    if (c != null) Seviye_Değişti(c, null);
                    break;

                case 2:
                    //arama
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
                    break;
            }

            P_SolOrta_Sağ.Visible = Seviye1_işTakip.Checked;
            if (Seviye1_işTakip.Checked)
            {
                P_İşTakip_DevamEden.Visible = Seviye2_DevamEden.Checked;
                P_İşTakip_TeslimEdildi.Visible = Seviye2_TeslimEdildi.Checked;
                P_İşTakip_ÖdemeBekleyen.Visible = Seviye2_ÖdemeBekleyen.Checked;
                P_İşTakip_Ödendi.Visible = Seviye2_Ödendi.Checked;
            }
            P_Arama.Visible = Seviye1_Arama.Checked;
            return;

        AramaİçinSeçenekleriBelirle:
            (sender as CheckBox).Checked = !(sender as CheckBox).Checked;
        }

        List<string> İşTakip_Müşteriler_AramaÇubuğu_Liste = null;
        private void İşTakip_Müşteriler_AramaÇubuğu_TextChanged(object sender, EventArgs e)
        {
            İşTakip_Müşteriler.Items.Clear();

            if (string.IsNullOrEmpty(İşTakip_Müşteriler_AramaÇubuğu.Text))
            {
                İşTakip_Müşteriler.Items.AddRange(İşTakip_Müşteriler_AramaÇubuğu_Liste.ToArray());
            }
            else
            {
                string gecici = İşTakip_Müşteriler_AramaÇubuğu.Text.ToLower();
                İşTakip_Müşteriler.Items.AddRange(İşTakip_Müşteriler_AramaÇubuğu_Liste.FindAll(x => x.ToLower().Contains(gecici)).ToArray());
            }
        }
        private void İşTakip_Müşteriler_SelectedIndexChanged(object sender, EventArgs e)
        {
            Seviye_Değişti(Seviye1_işTakip, null);

            //eposta gönderimi için iş adetlerinin enüde gösterilemsi
            İşTakip_Eposta_DevamEden.Checked = false;
            İşTakip_Eposta_TeslimEdildi.Checked = false;
            İşTakip_Eposta_ÖdemeBekleyen.Checked = false;
            Banka_Tablo_ bt = Banka.Talep_Listele(İşTakip_Müşteriler.Text, Banka.TabloTürü.DevamEden);
            İşTakip_Eposta_DevamEden.Text = "Devam eden : " + bt.Talepler.Count + " iş"; ;
            bt = Banka.Talep_Listele(İşTakip_Müşteriler.Text, Banka.TabloTürü.TeslimEdildi);
            İşTakip_Eposta_TeslimEdildi.Text = "Teslim edildi : " + bt.Talepler.Count + " iş";
            İşTakip_Eposta_ÖdemeBekleyen.Text = "Ödeme talep edildi : " + Banka.Dosya_Listele(İşTakip_Müşteriler.Text, false).Length + " dönem";
        }
        private void İşTakip_DevamEden_Sil_Click(object sender, EventArgs e)
        {
            if (!Banka.Müşteri_MevcutMu(İşTakip_Müşteriler.Text))
            {
                MessageBox.Show("Lütfen geçerli bir müşteri seçiniz", Text);
                İşTakip_Müşteriler.Focus();
                return;
            }

            List<string> l = new List<string>();
            for (int i = 0; i < Tablo.RowCount; i++)
            {
                if ((bool)Tablo[0, i].Value) l.Add((string)Tablo[1, i].Value);
            }

            if (l.Count < 1)
            {
                MessageBox.Show("Lütfen tablodan seçim yapınız", Text);
                return;
            }

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
            if (!Banka.Müşteri_MevcutMu(İşTakip_Müşteriler.Text))
            {
                MessageBox.Show("Lütfen geçerli bir müşteri seçiniz", Text);
                İşTakip_Müşteriler.Focus();
                return;
            }

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

            Ortak.YeniSayfaAçmaTalebi = new object[] { "Yeni İş Girişi", İşTakip_Müşteriler.Text, l[0] };
            Close();
        }
        private void İşTakip_DevamEden_MüşteriyeGönder_Click(object sender, EventArgs e)
        {
            if (!Banka.Müşteri_MevcutMu(İşTakip_Müşteriler.Text))
            {
                MessageBox.Show("Lütfen geçerli bir müşteri seçiniz", Text);
                İşTakip_Müşteriler.Focus();
                return;
            }

            List<string> l = new List<string>();

            for (int i = 0; i < Tablo.RowCount; i++)
            {
                if ((bool)Tablo[0, i].Value)
                {
                    l.Add((string)Tablo[1, i].Value);
                }
            }

            if (l.Count < 1)
            {
                MessageBox.Show("Düzenleme yapılabilmesi için en az 1 adet talebin seçili olduğundan emin olunuz", Text);
                return;
            }

            Banka.Talep_İşaretle_DevamEden_MüşteriyeGönderildi(İşTakip_Müşteriler.Text, l);
            Banka.Değişiklikleri_Kaydet();

            Seviye_Değişti(null, null);
        }
        private void İşTakip_DevamEden_İsaretle_Bitti_Click(object sender, EventArgs e)
        {
            if (!Banka.Müşteri_MevcutMu(İşTakip_Müşteriler.Text))
            {
                MessageBox.Show("Lütfen geçerli bir müşteri seçiniz", Text);
                İşTakip_Müşteriler.Focus();
                return;
            }

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

            if (l.Count < 1)
            {
                MessageBox.Show("Lütfen tablodan seçim yapınız", Text);
                return;
            }

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
                Banka.Değişiklikler_TamponuSıfırla();

                DialogResult Dr = MessageBox.Show(snç + Environment.NewLine + Environment.NewLine +
                    "Ücretler sayfasını açmak ister misiniz?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (Dr == DialogResult.No) return;

                Ortak.YeniSayfaAçmaTalebi = new object[] { "Ücretler" };
                Close();
            }
        }
        private void İşTakip_TeslimEdildi_İşaretle_Etkin_Click(object sender, EventArgs e)
        {
            if (!Banka.Müşteri_MevcutMu(İşTakip_Müşteriler.Text))
            {
                MessageBox.Show("Lütfen geçerli bir müşteri seçiniz", Text);
                İşTakip_Müşteriler.Focus();
                return;
            }

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

            if (l.Count < 1)
            {
                MessageBox.Show("Lütfen tablodan seçim yapınız", Text);
                return;
            }

            Banka.Talep_İşaretle_DevamEden_TeslimEdilen(İşTakip_Müşteriler.Text, l, false);
            Banka.Değişiklikleri_Kaydet();
        }
        private void İşTakip_TeslimEdildi_ÖdemeTalebiOluştur_Click(object sender, EventArgs e)
        {
            if (!Banka.Müşteri_MevcutMu(İşTakip_Müşteriler.Text))
            {
                MessageBox.Show("Lütfen geçerli bir müşteri seçiniz", Text);
                İşTakip_Müşteriler.Focus();
                return;
            }

            İşTakip_Bitti_İlaveÖdeme_Açıklama.Text = İşTakip_Bitti_İlaveÖdeme_Açıklama.Text.Trim();
            if (!string.IsNullOrEmpty(İşTakip_Bitti_İlaveÖdeme_Açıklama.Text))
            {
                string ilave_ödeme_miktar = İşTakip_Bitti_İlaveÖdeme_Miktar.Text;
                if (!Ortak.YazıyıSayıyaDönüştür(ref ilave_ödeme_miktar, "İlave ödeme Miktar kutucuğu"))
                {
                    İşTakip_Bitti_İlaveÖdeme_Miktar.Focus();
                    return;
                }

                İşTakip_Bitti_İlaveÖdeme_Miktar.Text = ilave_ödeme_miktar;
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

            if (l.Count < 1)
            {
                MessageBox.Show("Lütfen tablodan seçim yapınız", Text);
                return;
            }

            Banka.Talep_İşaretle_ÖdemeTalepEdildi(İşTakip_Müşteriler.Text, l, İşTakip_Bitti_İlaveÖdeme_Açıklama.Text, İşTakip_Bitti_İlaveÖdeme_Miktar.Text, İşTakip_TeslimEdildi_KDV.Checked);
            Banka.Değişiklikleri_Kaydet();

            İşTakip_Bitti_İlaveÖdeme_Açıklama.Text = "";
            İşTakip_Bitti_İlaveÖdeme_Miktar.Text = "";
        }
        private void İşTakip_TeslimEdildi_İlaveÖdeme_Açıklama_TextChanged(object sender, EventArgs e)
        {
            İşTakip_Bitti_İlaveÖdeme_Miktar.Enabled = !string.IsNullOrWhiteSpace(İşTakip_Bitti_İlaveÖdeme_Açıklama.Text);
        }
        private void İşTakip_ÖdemeBekleyen_Dönem_TextChanged(object sender, EventArgs e)
        {
            if (!Banka.Müşteri_MevcutMu(İşTakip_Müşteriler.Text))
            {
                MessageBox.Show("Lütfen geçerli bir müşteri seçiniz", Text);
                İşTakip_Müşteriler.Focus();
                return;
            }

            if (!İşTakip_ÖdemeBekleyen_Dönem.Items.Contains(İşTakip_ÖdemeBekleyen_Dönem.Text))
            {
                İpUcu.SetToolTip(Tablo, "Tümünü seçmek / kaldırmak için çift tıkla");
                return;
            }

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
        }
        private void İşTakip_ÖdemeBekleyen_İptalEt_Click(object sender, EventArgs e)
        {
            if (!Banka.Müşteri_MevcutMu(İşTakip_Müşteriler.Text))
            {
                MessageBox.Show("Lütfen geçerli bir müşteri seçiniz", Text);
                İşTakip_Müşteriler.Focus();
                return;
            }

            if (!İşTakip_ÖdemeBekleyen_Dönem.Items.Contains(İşTakip_ÖdemeBekleyen_Dönem.Text))
            {
                MessageBox.Show("Lütfen geçerli bir eleman seçiniz", Text);
                İşTakip_ÖdemeBekleyen_Dönem.Focus();
                return;
            }

            DialogResult Dr = MessageBox.Show("Seçilen döneme ait altta listelenmiş işlerin ödeme talebini iptal etmek istediğinize emin misiniz?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (Dr == DialogResult.No) return;

            Banka.Talep_İşaretle_ÖdemeTalepEdildi_TeslimEdildi(İşTakip_Müşteriler.Text, İşTakip_ÖdemeBekleyen_Dönem.Text);
            Banka.Değişiklikleri_Kaydet();

            Banka.Değişiklikler_TamponuSıfırla();
            Seviye_Değişti(Seviye2_ÖdemeBekleyen, null);
        }
        private void İşTakip_ÖdemeBekleyen_ÖdendiOlarakİşaretle_Click(object sender, EventArgs e)
        {
            if (!Banka.Müşteri_MevcutMu(İşTakip_Müşteriler.Text))
            {
                MessageBox.Show("Lütfen geçerli bir müşteri seçiniz", Text);
                İşTakip_Müşteriler.Focus();
                return;
            }

            if (!İşTakip_ÖdemeBekleyen_Dönem.Items.Contains(İşTakip_ÖdemeBekleyen_Dönem.Text))
            {
                MessageBox.Show("Lütfen geçerli bir eleman seçiniz", Text);
                İşTakip_ÖdemeBekleyen_Dönem.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(İşTakip_ÖdemeBekleyen_Notlar.Text)) İşTakip_ÖdemeBekleyen_Notlar.Text = null;

            DialogResult Dr = MessageBox.Show("Seçilen döneme ait işleri KALICI olarak ÖDENDİ olarak işaretlemek istediğinize emin misiniz?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (Dr == DialogResult.No) return;

            Banka.Talep_İşaretle_ÖdemeTalepEdildi_Ödendi(İşTakip_Müşteriler.Text, İşTakip_ÖdemeBekleyen_Dönem.Text, İşTakip_ÖdemeBekleyen_Notlar.Text);
            Banka.Değişiklikleri_Kaydet();
            
            İşTakip_ÖdemeBekleyen_Notlar.Text = "";

            Banka.Değişiklikler_TamponuSıfırla();
            Seviye_Değişti(Seviye2_ÖdemeBekleyen, null);
        }
        private void İşTakip_Ödendi_Dönem_TextChanged(object sender, EventArgs e)
        {
            if (!Banka.Müşteri_MevcutMu(İşTakip_Müşteriler.Text))
            {
                MessageBox.Show("Lütfen geçerli bir müşteri seçiniz", Text);
                İşTakip_Müşteriler.Focus();
                return;
            }

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
        }
        private void İşTakip_Yazdırma_Yazdır_Click(object sender, EventArgs e)
        {
            if (!Banka.Müşteri_MevcutMu(İşTakip_Müşteriler.Text))
            {
                MessageBox.Show("Lütfen geçerli bir müşteri seçiniz", Text);
                İşTakip_Müşteriler.Focus();
                return;
            }

            ArgeMup.HazirKod.Depo_ depo;
            string gerçekdosyadı;

            if (Seviye2_DevamEden.Checked)
            {
                depo = new ArgeMup.HazirKod.Depo_();
                Banka_Tablo_ bt = Banka.Talep_Listele(İşTakip_Müşteriler.Text, Banka.TabloTürü.DevamEden);
                IDepo_Eleman talepler = depo.Bul("Talepler", true);
                depo.Yaz("Müşteri", İşTakip_Müşteriler.Text);

                foreach (IDepo_Eleman elm in bt.Talepler)
                {
                    talepler.Ekle(null, elm.YazıyaDönüştür(null));
                }

                gerçekdosyadı = "Devam_Eden_" + DateTime.Now.Yazıya(ArgeMup.HazirKod.Dönüştürme.D_TarihSaat.Şablon_DosyaAdı2) + ".pdf";
            }
            else if (Seviye2_TeslimEdildi.Checked)
            {
                depo = new ArgeMup.HazirKod.Depo_();
                Banka_Tablo_ bt = Banka.Talep_Listele(İşTakip_Müşteriler.Text, Banka.TabloTürü.TeslimEdildi);
                IDepo_Eleman talepler = depo.Bul("Talepler", true);
                depo.Yaz("Müşteri", İşTakip_Müşteriler.Text);

                foreach (IDepo_Eleman elm in bt.Talepler)
                {
                    talepler.Ekle(null, elm.YazıyaDönüştür(null));
                }

                gerçekdosyadı = "Teslim_Edildi_" + DateTime.Now.Yazıya(ArgeMup.HazirKod.Dönüştürme.D_TarihSaat.Şablon_DosyaAdı2) + ".pdf";
            }
            else if (Seviye2_ÖdemeBekleyen.Checked)
            {
                depo = Banka.Tablo(İşTakip_Müşteriler.Text, Banka.TabloTürü.ÖdemeTalepEdildi, false, İşTakip_ÖdemeBekleyen_Dönem.Text);
                gerçekdosyadı = "Ödeme_Talebi_" + İşTakip_ÖdemeBekleyen_Dönem.Text + ".pdf";
            }
            else if (Seviye2_Ödendi.Checked)
            {
                depo = Banka.Tablo(İşTakip_Müşteriler.Text, Banka.TabloTürü.Ödendi, false, İşTakip_Ödendi_Dönem.Text);
                gerçekdosyadı = "Ödendi_" + İşTakip_Ödendi_Dönem.Text + ".pdf";
            }
            else return;

            if (depo == null)
            {
                MessageBox.Show("Hiç kayıt bulunamadı", Text);
                return;
            }
            IDepo_Eleman tlp = depo.Bul("Talepler");
            if (tlp == null || tlp.Elemanları.Length < 1)
            {
                MessageBox.Show("Hiç kayıt bulunamadı", Text);
                return;
            }

            string dosyayolu = Ortak.Klasör_Gecici + Path.GetRandomFileName() + ".pdf";

            Yazdırma y = new Yazdırma();
            y.Yazdır_Depo(depo, dosyayolu);

            if (!string.IsNullOrEmpty(Ortak.Kullanıcı_Klasör_Pdf))
            {
                string hedef = Ortak.Kullanıcı_Klasör_Pdf + İşTakip_Müşteriler.Text + "\\" + gerçekdosyadı;
                if (!Dosya.Kopyala(dosyayolu, hedef))
                {
                    MessageBox.Show("Üretilen pdf kullanıcı klasörüne kopyalanamadı", Text);
                }
                else if (İşTakip_Yazdırma_VeAç.Checked) System.Diagnostics.Process.Start(hedef);
            }
            else if (İşTakip_Yazdırma_VeAç.Checked) System.Diagnostics.Process.Start(dosyayolu);
        }
        private void İşTakip_Eposta_CheckedChanged(object sender, EventArgs e)
        {
            İşTakip_Eposta_Gönder.Enabled = İşTakip_Eposta_DevamEden.Checked || İşTakip_Eposta_TeslimEdildi.Checked || İşTakip_Eposta_ÖdemeBekleyen.Checked;
        }
        private void İşTakip_Eposta_Gönder_Click(object sender, EventArgs e)
        {
            if (!Banka.Müşteri_MevcutMu(İşTakip_Müşteriler.Text))
            {
                MessageBox.Show("Lütfen geçerli bir müşteri seçiniz", Text);
                İşTakip_Müşteriler.Focus();
                return;
            }

            if (!Ortak.Kullanıcı_Eposta_hesabı_mevcut)
            {
                MessageBox.Show("Geçerli bir eposta hesabı oluşturulmadı" + Environment.NewLine + "Ana Ekran - Ayarlar - E-posta sayfasını kullanabilirsiniz", Text);
                return;
            }

            IDepo_Eleman m = Banka.Müşteri_Ayarlar(İşTakip_Müşteriler.Text);
            if (m == null || string.IsNullOrEmpty(m.Oku("Eposta/Kime") + m.Oku("Eposta/Bilgi") + m.Oku("Eposta/Gizli")))
            {
                MessageBox.Show("Müşteriye tanımlı e-posta adresi bulunamadı" + Environment.NewLine + "Ana Ekran - Ayarlar - Müşteriler sayfasını kullanabilirsiniz", Text);
                return;
            }

            ArgeMup.HazirKod.Depo_ depo;
            string gecici_dosyadı;
            string gecici_klasör = Ortak.Klasör_Gecici + Path.GetRandomFileName() + "\\";
            Directory.CreateDirectory(gecici_klasör);

            Yazdırma y = new Yazdırma();

            if (İşTakip_Eposta_DevamEden.Checked)
            {
                depo = new ArgeMup.HazirKod.Depo_();
                Banka_Tablo_ bt = Banka.Talep_Listele(İşTakip_Müşteriler.Text, Banka.TabloTürü.DevamEden);
                IDepo_Eleman talepler = depo.Bul("Talepler", true);
                depo.Yaz("Müşteri", İşTakip_Müşteriler.Text);

                if (bt.Talepler.Count > 0)
                {
                    foreach (IDepo_Eleman elm in bt.Talepler)
                    {
                        talepler.Ekle(null, elm.YazıyaDönüştür(null));
                    }

                    gecici_dosyadı = gecici_klasör + "Devam_Eden_" + DateTime.Now.Yazıya(ArgeMup.HazirKod.Dönüştürme.D_TarihSaat.Şablon_DosyaAdı2) + ".pdf";

                    y.Yazdır_Depo(depo, gecici_dosyadı);
                }
            }

            if (İşTakip_Eposta_TeslimEdildi.Checked)
            {
                depo = new ArgeMup.HazirKod.Depo_();
                Banka_Tablo_ bt = Banka.Talep_Listele(İşTakip_Müşteriler.Text, Banka.TabloTürü.TeslimEdildi);
                IDepo_Eleman talepler = depo.Bul("Talepler", true);
                depo.Yaz("Müşteri", İşTakip_Müşteriler.Text);

                if (bt.Talepler.Count > 0)
                {
                    foreach (IDepo_Eleman elm in bt.Talepler)
                    {
                        talepler.Ekle(null, elm.YazıyaDönüştür(null));
                    }

                    gecici_dosyadı = gecici_klasör + "Teslim_Edildi_" + DateTime.Now.Yazıya(ArgeMup.HazirKod.Dönüştürme.D_TarihSaat.Şablon_DosyaAdı2) + ".pdf";

                    y.Yazdır_Depo(depo, gecici_dosyadı);
                }
            }

            if (İşTakip_Eposta_ÖdemeBekleyen.Checked)
            {
                foreach (string ö in Banka.Dosya_Listele(İşTakip_Müşteriler.Text, false))
                {
                    depo = Banka.Tablo(İşTakip_Müşteriler.Text, Banka.TabloTürü.ÖdemeTalepEdildi, false, ö);
                    if (depo != null)
                    {
                        IDepo_Eleman de = depo.Bul("Talepler");
                        if (de != null && de.Elemanları.Length > 0)
                        {
                            gecici_dosyadı = gecici_klasör + "Ödeme_Talebi_" + ö + ".pdf";

                            y.Yazdır_Depo(depo, gecici_dosyadı);
                        }
                    }
                }
            }

            string[] dsy_lar = Directory.GetFiles(gecici_klasör);
            if (dsy_lar.Length > 0)
            {
                DialogResult Dr = MessageBox.Show("Oluşturulan toplam " + dsy_lar.Length + " adet belge müşterinize e-posta yoluyla gönderilecek" +
                Environment.NewLine + Environment.NewLine +
                İşTakip_Müşteriler.Text +
                Environment.NewLine + Environment.NewLine +
                "Devam etmek için Evet tuşuna basınız", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (Dr == DialogResult.No) return;

                Ayarlar_Eposta epst = new Ayarlar_Eposta();
                string snç = epst.EpostaGönder(m, dsy_lar);
                if (!string.IsNullOrEmpty(snç)) MessageBox.Show(snç, Text);
            }
            else MessageBox.Show("Hiç kayıt bulunamadı", Text);
        }

        private void Tablo_DoubleClick(object sender, EventArgs e)
        {
            if (Tablo.Tag != null || Tablo.RowCount < 1) return;
            bool b = !(bool)Tablo[0, 0].Value;
            Tablo.Tag = 0;

            for (int i = 0; i < Tablo.RowCount - 1; i++)
            {
                Tablo[0, i].Value = b;
            }

            Tablo.Tag = null;
            Tablo[0, Tablo.RowCount - 1].Value = b;
        }
        private void Tablo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Tablo.Tag != null || e.RowIndex < 0 || e.ColumnIndex < 0 || e.ColumnIndex > 0) return;

            Tablo[0, e.RowIndex].Value = !(bool)Tablo[0, e.RowIndex].Value;
        }
        private void Tablo_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (Tablo.Tag != null || e.RowIndex < 0) return;

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
                            //DateTime t = l[s].TarihSaate(ArgeMup.HazirKod.Dönüştürme.D_TarihSaat.Şablon_DosyaAdı2);
                            //if (Arama_GirişTarihi_Başlangıç.Value > t || t > Arama_GirişTarihi_Bitiş.Value) continue;

                            Arama_Sorgula_Click_2(Banka.Talep_Listele(Arama_Müşteriler.Items[i].ToString(), Banka.TabloTürü.ÖdemeTalepEdildi, l[s]));
                        }
                    }

                    if (Seviye2_Ödendi.Checked && !Arama_Sorgula_KapatmaTalebi)
                    {
                        string[] l = Banka.Dosya_Listele(Arama_Müşteriler.Items[i].ToString(), true);

                        for (int s = 0; s < l.Length && !Arama_Sorgula_KapatmaTalebi; s++)
                        {
                            Arama_İlerlemeÇubuğu.Value++;
                            //DateTime t = l[s].TarihSaate(ArgeMup.HazirKod.Dönüştürme.D_TarihSaat.Şablon_DosyaAdı2);
                            //if (Arama_GirişTarihi_Başlangıç.Value > t || t > Arama_GirişTarihi_Bitiş.Value) continue;

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

            string sn_ler = "";
            List<IDepo_Eleman> uyuşanlar = new List<IDepo_Eleman>();
            foreach (IDepo_Eleman serino in bt.Talepler)
            {
                bool evet = false;

                foreach (IDepo_Eleman iş in serino.Elemanları)
                {
                    if (!Arama_Sorgula_Aranan_İşTürleri.Contains(iş[0])) continue;

                    DateTime t = iş[1].TarihSaate(); //iş kabul tarihi
                    if (Arama_GirişTarihi_Başlangıç.Value > t || t > Arama_GirişTarihi_Bitiş.Value) continue;

                    evet = true;
                    break;
                }

                if (evet)
                {
                    sn_ler += serino.Adı + " ";
                    uyuşanlar.Add(serino);
                }
            }

            if (uyuşanlar.Count == 0) return;

            if (Arama_Sorgula_Detaylı.Checked && bt.Ödeme != null)
            {
                //boş IDepo_Eleman oluşturulması
                IDepo_Eleman y = uyuşanlar[0].Bul(null, false, true);
                y.Sil(null, true, true);
                y.Adı = "";

                sn_ler = sn_ler.TrimEnd() + Environment.NewLine + Environment.NewLine;
                sn_ler += "Talep : " + D_TarihSaat.Yazıya(bt.Ödeme.Oku_TarihSaat(null), D_TarihSaat.Şablon_DosyaAdı2);
                if (bt.Türü == Banka.TabloTürü.Ödendi) sn_ler += Environment.NewLine + "Ödeme : " + D_TarihSaat.Yazıya(bt.Ödeme.Oku_TarihSaat(null, default, 1), D_TarihSaat.Şablon_DosyaAdı2);

                //sn ler + tarihler
                y.Yaz("1", sn_ler);

                sn_ler = "";
                Banka.Talep_Ayıkla_Ödeme(bt.Ödeme, out List<string> Açıklamalar, out List<string> Ücretler, out _, out _, out string Notlar);
                for (int i = 0; i < Açıklamalar.Count; i++) sn_ler += Açıklamalar[i] + " : " + Ücretler[i] + "\n";
                sn_ler = sn_ler.TrimEnd('\n');

                if (Notlar.DoluMu()) sn_ler += "\n\n" + Notlar;
                y[2] = sn_ler;

                uyuşanlar.Add(y);
            }

            bt.Talepler = uyuşanlar;
            Banka.Talep_TablodaGöster(Tablo, bt, false);
        }
        private void Arama_Müşteriler_İşler_SelectedIndexChanged(object sender, EventArgs e)
        {
            (sender as CheckedListBox).SelectedIndex = -1;
        }
        private void Arama_Müşteriler_Seç_Click(object sender, EventArgs e)
        {
            bool şimdi = !Arama_Müşteriler.GetItemChecked(0);
            for (int i = 0; i < Arama_Müşteriler.Items.Count; i++)
            {
                Arama_Müşteriler.SetItemChecked(i, şimdi);
            }
        }
        private void Arama_İşTürleri_Seç_Click(object sender, EventArgs e)
        {
            bool şimdi = !Arama_İş_Türleri.GetItemChecked(0);
            for (int i = 0; i < Arama_İş_Türleri.Items.Count; i++)
            {
                Arama_İş_Türleri.SetItemChecked(i, şimdi);
            }
        }
    }
}
