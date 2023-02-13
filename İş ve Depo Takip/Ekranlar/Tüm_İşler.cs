using ArgeMup.HazirKod;
using ArgeMup.HazirKod.Dönüştürme;
using ArgeMup.HazirKod.Ekİşlemler;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip.Ekranlar
{
    public partial class Tüm_İşler : Form
    {
        public Tüm_İşler()
        {
            InitializeComponent();

            Ortak.GeçiciDepolama_PencereKonumları_Oku(this);

            İşTakip_Müşteriler_AramaÇubuğu_Liste = Banka.Müşteri_Listele();
            İşTakip_Müşteriler.Items.AddRange(İşTakip_Müşteriler_AramaÇubuğu_Liste.ToArray());
            Arama_Müşteriler.Items.AddRange(Banka.Müşteri_Listele(true).ToArray());

            Arama_İş_Türleri.Items.AddRange(Banka.İşTürü_Listele().ToArray());

            DateTime t = DateTime.Now;
            Arama_GirişTarihi_Bitiş.Value = new DateTime(t.Year, t.Month, t.Day, 23, 59, 59);
            t = t.AddMonths(-6);
            Arama_GirişTarihi_Başlangıç.Value = new DateTime(t.Year, t.Month, t.Day, 0, 0, 0);

            P_Üst_Alt.Panel1.Controls.Add(P_SolOrta_Sağ); P_SolOrta_Sağ.Dock = DockStyle.Fill; P_SolOrta_Sağ.Visible = true;
            P_Sol_Orta.Panel2.Controls.Add(P_İşTakip_DevamEden); P_İşTakip_DevamEden.Dock = DockStyle.Fill; P_İşTakip_DevamEden.Visible = false;
            P_Sol_Orta.Panel2.Controls.Add(P_İşTakip_TeslimEdildi); P_İşTakip_TeslimEdildi.Dock = DockStyle.Fill; P_İşTakip_TeslimEdildi.Visible = false;
            P_Sol_Orta.Panel2.Controls.Add(P_İşTakip_ÖdemeBekleyen); P_İşTakip_ÖdemeBekleyen.Dock = DockStyle.Fill; P_İşTakip_ÖdemeBekleyen.Visible = false;
            P_Sol_Orta.Panel2.Controls.Add(P_İşTakip_Ödendi); P_İşTakip_Ödendi.Dock = DockStyle.Fill; P_İşTakip_Ödendi.Visible = false;
            P_Üst_Alt.Panel1.Controls.Add(P_Arama); P_Arama.Dock = DockStyle.Fill; P_Arama.Visible = false;
            P_Üst_Alt.Panel1.Controls.Add(P_Malzemeler); P_Malzemeler.Dock = DockStyle.Fill; P_Malzemeler.Visible = false;

            P_SolOrta_Sağ.SplitterDistance = P_SolOrta_Sağ.Width * 3 / 4; //müşteriler, tuşlar + yazdırma
            P_Sol_Orta.SplitterDistance = P_Sol_Orta.Width * 40 / 100; //müşteriler + tuşları
            P_Üst_Alt.SplitterDistance = Height / 3; //tuşlar + tablo

            Seviye1_işTakip.Tag = 1;
            Seviye1_Arama.Tag = 2;
            Seviye2_DevamEden.Tag = 10;
            Seviye2_TeslimEdildi.Tag = 11;
            Seviye2_ÖdemeBekleyen.Tag = 12;
            Seviye2_Ödendi.Tag = 13;

            if (Ortak.YeniSayfaAçmaTalebi != null)
            {
                //farklı sayfayı açmak için kullanılıyor
                if (Ortak.YeniSayfaAçmaTalebi.Length == 2 &&
                    (string)Ortak.YeniSayfaAçmaTalebi[0] == "Tüm İşler" &&
                    (string)Ortak.YeniSayfaAçmaTalebi[1] == "Arama")
                {
                    Seviye_Değişti(Seviye1_Arama, null);
                }

                Ortak.YeniSayfaAçmaTalebi = null;
            }
        }
        private void Tüm_İşler_Shown(object sender, EventArgs e)
        {
            if (Seviye1_işTakip.Checked)
            {
                if ((int)Seviye1_işTakip.Tag == 1) İşTakip_Müşteriler_AramaÇubuğu.Focus();
                else Malzemeler_Malzeme_AramaÇubuğu.Focus();
            }
            else Arama_Sorgula.Focus();
        }
        private void Tüm_İşler_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                    Ortak.YeniSayfaAçmaTalebi = new string[] { "Yeni İş Girişi", null, null }; 
                    Close();
                    Tüm_İşler_Shown(null, null);
                    break;

                case Keys.F2:
                    Seviye_Değişti(Seviye1_işTakip, null);
                    Tüm_İşler_Shown(null, null);
                    break;

                case Keys.F3:
                    Seviye_Değişti(Seviye1_Arama, null);
                    Tüm_İşler_Shown(null, null);
                    break;
            }
        }

        private void Seviye_Değişti(object sender, EventArgs e)
        {
            int no = 3;
            if (sender != null)
            {
                no = (int)(sender as CheckBox).Tag;
                if (no < 1) return;
            }

            Banka_Tablo_ bt;
            Tablo.Rows.Clear();

            switch (no)
            {
                case 1:
                    //iş takip
                    if (Seviye1_işTakip.Checked)
                    {
                        //malzeme kullanım detayı sayfasına geç
                        Seviye1_işTakip.Tag = 3;
                        Seviye1_işTakip.FlatAppearance.CheckedBackColor = Color.Khaki;
                        P_Malzemeler.Visible = true;
                        P_SolOrta_Sağ.Visible = false;

                        Malzemeler_Malzeme_AramaÇubuğu_Liste = Banka.Malzeme_Listele(true);
                        Malzemeler_Malzeme.Items.Clear();
                        Malzemeler_Malzeme.Items.AddRange(Malzemeler_Malzeme_AramaÇubuğu_Liste.ToArray());
                    }
                    else
                    {
                        Seviye1_işTakip.Checked = true;
                        Seviye1_Arama.Checked = false;

                        CheckBox c = null;
                        if (Seviye2_DevamEden.Checked) c = Seviye2_DevamEden;
                        else if (Seviye2_TeslimEdildi.Checked) c = Seviye2_TeslimEdildi;
                        else if (Seviye2_ÖdemeBekleyen.Checked) c = Seviye2_ÖdemeBekleyen;
                        else if (Seviye2_Ödendi.Checked) c = Seviye2_Ödendi;
                        if (c != null) Seviye_Değişti(c, null);
                    }
                    break;

                case 2:
                    //arama
                    if ((int)Seviye1_işTakip.Tag != 1) Seviye_Değişti(Seviye1_işTakip, null);

                    for (int i = 0; i < Arama_Müşteriler.Items.Count; i++)
                    {
                        Arama_Müşteriler.SetItemChecked(i, !Arama_Müşteriler.Items[i].ToString().StartsWith(".:Gizli:. "));
                    }
                    for (int i = 0; i < Arama_İş_Türleri.Items.Count; i++)
                    {
                        Arama_İş_Türleri.SetItemChecked(i, true);
                    }

                    Seviye1_işTakip.Checked = false;
                    Seviye1_Arama.Checked = true;
                    break;

                case 3:
                    Seviye1_işTakip.FlatAppearance.CheckedBackColor = Color.SkyBlue;
                    Seviye1_işTakip.Tag = 1;
                    Seviye1_işTakip.Checked = false;

                    Seviye_Değişti(Seviye1_işTakip, null);
                    break;

                case 10:
                    //devam eden
                    if (!Seviye1_işTakip.Checked) goto AramaİçinSeçenekleriBelirle;

                    Seviye2_DevamEden.Checked = true;
                    Seviye2_TeslimEdildi.Checked = false;
                    Seviye2_ÖdemeBekleyen.Checked = false;
                    Seviye2_Ödendi.Checked = false;

                    if (Banka.Müşteri_MevcutMu(İşTakip_Müşteriler.Text))
                    {
                        bt = Banka.Talep_Listele(İşTakip_Müşteriler.Text, Banka.TabloTürü.DevamEden);
                        Banka.Talep_TablodaGöster(Tablo, bt);
                    }
                    break;

                case 11:
                    //teslim edildi 
                    if (!Seviye1_işTakip.Checked) goto AramaİçinSeçenekleriBelirle;

                    Seviye2_DevamEden.Checked = false;
                    Seviye2_TeslimEdildi.Checked = true;
                    Seviye2_ÖdemeBekleyen.Checked = false;
                    Seviye2_Ödendi.Checked = false;

                    if (Banka.Müşteri_MevcutMu(İşTakip_Müşteriler.Text))
                    {
                        bt = Banka.Talep_Listele(İşTakip_Müşteriler.Text, Banka.TabloTürü.TeslimEdildi);
                        Banka.Talep_TablodaGöster(Tablo, bt);
                    }
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
                    if (Banka.Müşteri_MevcutMu(İşTakip_Müşteriler.Text))
                    {
                        İşTakip_ÖdemeBekleyen_Dönem.Items.AddRange(Banka.Dosya_Listele_Müşteri(İşTakip_Müşteriler.Text, false));
                        if (İşTakip_ÖdemeBekleyen_Dönem.Items.Count > 0)
                        {
                            if (İşTakip_ÖdemeBekleyen_Dönem.SelectedIndex != 0) İşTakip_ÖdemeBekleyen_Dönem.SelectedIndex = 0;
                        }
                    }
                    break;

                case 13:
                    //ödendi
                    if (!Seviye1_işTakip.Checked) goto AramaİçinSeçenekleriBelirle;

                    Seviye2_DevamEden.Checked = false;
                    Seviye2_TeslimEdildi.Checked = false;
                    Seviye2_ÖdemeBekleyen.Checked = false;
                    Seviye2_Ödendi.Checked = true;

                    İşTakip_Ödendi_Dönem_AramaÇubuğu.Text = null;
                    İşTakip_Ödendi_Dönem_Dönemler.Items.Clear();
                    if (Banka.Müşteri_MevcutMu(İşTakip_Müşteriler.Text))
                    {
                        string[] l_dizi = Banka.Dosya_Listele_Müşteri(İşTakip_Müşteriler.Text, true);
                        İşTakip_Ödendi_Dönem_AramaÇubuğu_Liste = l_dizi.ToList();
                        İşTakip_Ödendi_Dönem_Dönemler.Items.AddRange(l_dizi);
                        if (İşTakip_Ödendi_Dönem_Dönemler.Items.Count > 0)
                        {
                            if (İşTakip_Ödendi_Dönem_Dönemler.SelectedIndex != 0) İşTakip_Ödendi_Dönem_Dönemler.SelectedIndex = 0;
                        }
                    }
                    break;
            }

            if ((int)Seviye1_işTakip.Tag == 1)
            {
                //işler sayfası
                Seviye2_DevamEden.Enabled = true;
                Seviye2_TeslimEdildi.Enabled = true;
                Seviye2_ÖdemeBekleyen.Enabled = true;
                Seviye2_Ödendi.Enabled = true;

                P_SolOrta_Sağ.Visible = Seviye1_işTakip.Checked;
                if (Seviye1_işTakip.Checked)
                {
                    P_İşTakip_DevamEden.Visible = Seviye2_DevamEden.Checked;
                    P_İşTakip_TeslimEdildi.Visible = Seviye2_TeslimEdildi.Checked;
                    P_İşTakip_ÖdemeBekleyen.Visible = Seviye2_ÖdemeBekleyen.Checked;
                    P_İşTakip_Ödendi.Visible = Seviye2_Ödendi.Checked;
                }
                P_Arama.Visible = Seviye1_Arama.Checked;
            }
            else
            {
                //malzeme kullanım detayı sayfası
                Seviye2_DevamEden.Enabled = false;
                Seviye2_TeslimEdildi.Enabled = false;
                Seviye2_ÖdemeBekleyen.Enabled = false;
                Seviye2_Ödendi.Enabled = false;

                Seviye2_DevamEden.Checked = true;
                Seviye2_TeslimEdildi.Checked = false;
                Seviye2_ÖdemeBekleyen.Checked = false;
                Seviye2_Ödendi.Checked = false;
            }
            return;

        AramaİçinSeçenekleriBelirle:
            (sender as CheckBox).Checked = !(sender as CheckBox).Checked;
        }

        private void İşTakip_Müşteriler_KeyPress(object sender, KeyPressEventArgs e)
        {
            İşTakip_Müşteriler_AramaÇubuğu.Focus();
        }
        List<string> İşTakip_Müşteriler_AramaÇubuğu_Liste = null;
        private void İşTakip_Müşteriler_AramaÇubuğu_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (İşTakip_Müşteriler.Items.Count > 0)
                {
                    İşTakip_Müşteriler.SelectedIndex = 0;
                    İşTakip_Müşteriler.Focus();
                }
            }
        }
        private void İşTakip_Müşteriler_AramaÇubuğu_TextChanged(object sender, EventArgs e)
        {
            İşTakip_Müşteriler.Items.Clear();

            İşTakip_Müşteriler.Items.AddRange(Ortak.GrupArayıcı(İşTakip_Müşteriler_AramaÇubuğu_Liste, İşTakip_Müşteriler_AramaÇubuğu.Text));
        }
        private void İşTakip_Müşteriler_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckBox c = null;
            if (Seviye2_DevamEden.Checked) c = Seviye2_DevamEden;
            else if (Seviye2_TeslimEdildi.Checked) c = Seviye2_TeslimEdildi;
            else if (Seviye2_ÖdemeBekleyen.Checked) c = Seviye2_ÖdemeBekleyen;
            else if (Seviye2_Ödendi.Checked) c = Seviye2_Ödendi;
            if (c != null) Seviye_Değişti(c, null);

            //İlave Ödeme Detayları
            IDepo_Eleman müş = Banka.Ayarlar_Müşteri(İşTakip_Müşteriler.Text, "Sayfa/Teslim Edildi", true);
            İşTakip_TeslimEdildi_İlaveÖdeme_Açıklama.Text = müş["İlave Ödeme"][0];
            İşTakip_TeslimEdildi_İlaveÖdeme_Miktar.Text = müş["İlave Ödeme"][1];
            İşTakip_TeslimEdildi_KDV.Checked = müş["KDV"].Oku_Bit(null, true);
            İşTakip_TeslimEdildi_İlaveÖdeme_HesabaDahilEt.Checked = false;

            //eposta gönderimi için iş adetlerinin enüde gösterilemsi
            İşTakip_Eposta_DevamEden.Checked = false;
            İşTakip_Eposta_TeslimEdildi.Checked = false;
            İşTakip_Eposta_ÖdemeBekleyen.Checked = false;
            Banka_Tablo_ bt = Banka.Talep_Listele(İşTakip_Müşteriler.Text, Banka.TabloTürü.DevamEden);
            İşTakip_Eposta_DevamEden.Text = "Devam eden : " + bt.Talepler.Count + " iş"; ;
            bt = Banka.Talep_Listele(İşTakip_Müşteriler.Text, Banka.TabloTürü.TeslimEdildi);
            İşTakip_Eposta_TeslimEdildi.Text = "Teslim edildi : " + bt.Talepler.Count + " iş";
            İşTakip_Eposta_ÖdemeBekleyen.Text = "Ödeme talep edildi : " + Banka.Dosya_Listele_Müşteri(İşTakip_Müşteriler.Text, false).Length + " dönem";
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
            Banka.Değişiklikleri_Kaydet(İşTakip_DevamEden_Sil);

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
            Banka.Değişiklikleri_Kaydet(İşTakip_DevamEden_MüşteriyeGönder);

            Seviye_Değişti(null, null);
        }
        private void İşTakip_DevamEden_İsaretle_TeslimEdildi_Click(object sender, EventArgs e)
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
                Banka.Değişiklikleri_Kaydet(İşTakip_DevamEden_İsaretle_TeslimEdildi);

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
        private void İşTakip_TeslimEdildi_İşaretle_DevamEden_Click(object sender, EventArgs e)
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
            Banka.Değişiklikleri_Kaydet(İşTakip_TeslimEdildi_İşaretle_DevamEden);
        }
        private void İşTakip_TeslimEdildi_ÖdemeTalebiOluştur_Click(object sender, EventArgs e)
        {
            if (!Banka.Müşteri_MevcutMu(İşTakip_Müşteriler.Text))
            {
                MessageBox.Show("Lütfen geçerli bir müşteri seçiniz", Text);
                İşTakip_Müşteriler.Focus();
                return;
            }

            string İlaveÖdeme_Açıklama = null, İlaveÖdeme_Miktar = null;
            if (İşTakip_TeslimEdildi_İlaveÖdeme_HesabaDahilEt.Checked)
            {
                İşTakip_TeslimEdildi_İlaveÖdeme_Açıklama.Text = İşTakip_TeslimEdildi_İlaveÖdeme_Açıklama.Text.Trim();
                if (!string.IsNullOrEmpty(İşTakip_TeslimEdildi_İlaveÖdeme_Açıklama.Text))
                {
                    string ilave_ödeme_miktar = İşTakip_TeslimEdildi_İlaveÖdeme_Miktar.Text;
                    if (!Ortak.YazıyıSayıyaDönüştür(ref ilave_ödeme_miktar, "İlave ödeme Miktar kutucuğu"))
                    {
                        İşTakip_TeslimEdildi_İlaveÖdeme_Miktar.Focus();
                        return;
                    }

                    İşTakip_TeslimEdildi_İlaveÖdeme_Miktar.Text = ilave_ödeme_miktar;
                }
                else İşTakip_TeslimEdildi_İlaveÖdeme_Açıklama.Text = null;

                İlaveÖdeme_Açıklama = İşTakip_TeslimEdildi_İlaveÖdeme_Açıklama.Text;
                İlaveÖdeme_Miktar = İşTakip_TeslimEdildi_İlaveÖdeme_Miktar.Text;
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

            İşTakip_TeslimEdildi_İlaveÖdeme_HesabaDahilEt.Checked = false;

            Banka.Talep_İşaretle_TeslimEdilen_ÖdemeTalepEdildi(İşTakip_Müşteriler.Text, l, İlaveÖdeme_Açıklama, İlaveÖdeme_Miktar, İşTakip_TeslimEdildi_KDV.Checked);
            Banka.Değişiklikleri_Kaydet(İşTakip_TeslimEdildi_ÖdemeTalebiOluştur);
        }
        private void İşTakip_TeslimEdildi_İlaveÖdeme_HesabaDahilEt_CheckedChanged(object sender, EventArgs e)
        {
            İşTakip_TeslimEdildi_İlaveÖdeme_Açıklama.ReadOnly = !İşTakip_TeslimEdildi_İlaveÖdeme_HesabaDahilEt.Checked;
            İşTakip_TeslimEdildi_İlaveÖdeme_Miktar.ReadOnly = !İşTakip_TeslimEdildi_İlaveÖdeme_HesabaDahilEt.Checked;
        }
        private void İşTakip_TeslimEdildi_İlaveÖdeme_Açıklama_TextChanged(object sender, EventArgs e)
        {
            İşTakip_TeslimEdildi_İlaveÖdeme_Miktar.Enabled = !string.IsNullOrWhiteSpace(İşTakip_TeslimEdildi_İlaveÖdeme_Açıklama.Text);
        }
        private void İşTakip_TeslimEdildi_Sekmeler_ÖdemeAl_Miktar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                İşTakip_TeslimEdildi_Sekmeler_ÖdemeAl_KadarİşiSeç_Click(null, null);
            }
        }
        private void İşTakip_TeslimEdildi_Sekmeler_ÖdemeAl_KadarİşiSeç_Click(object sender, EventArgs e)
        {
            Ortak.İşTakip_TeslimEdildi_İşSeç_Seç o = new Ortak.İşTakip_TeslimEdildi_İşSeç_Seç();
            İşTakip_TeslimEdildi_Sekmeler_ÖdemeAl.Tag = o;

            if (Tablo.RowCount < 1) return;

            string gecici = İşTakip_TeslimEdildi_Sekmeler_ÖdemeAl_Miktar.Text;
            if (!Ortak.YazıyıSayıyaDönüştür(ref gecici, "İş Seçme Miktarı", null, 0))
            {
                İşTakip_TeslimEdildi_Sekmeler_ÖdemeAl_Miktar.Focus();
                return;
            }
            o.AlınanÖdeme = gecici.NoktalıSayıya();

            //Ödeme Yapılarak Ödendi Olarak İşasaretleme
            IDepo_Eleman müş = Banka.Tablo_Dal(İşTakip_Müşteriler.Text, Banka.TabloTürü.Ödemeler, "Mevcut Ön Ödeme");
            if (müş != null) o.MevcutÖnÖdeme = müş.Oku_Sayı(null);

            //talepleri sıralama
            Dictionary<DateTime, double> Liste = new Dictionary<DateTime, double>();
            for (int i = 0; i < Tablo.RowCount; i++)
            {
                Tablo[0, i].Value = false;
                Liste.Add((DateTime)Tablo[7, i].Tag, (double)Tablo[6, i].Tag);
            }

            if (İşTakip_TeslimEdildi_KDV.Checked) o.KDV_Oranı = Banka.Ayarlar_Genel("Bütçe/KDV", true).Oku_Sayı(null, 8);

            double YapılanÖdeme = o.MevcutÖnÖdeme + o.AlınanÖdeme;
            for (int i = 0; i < Liste.Count; i++)
            {
                double Hesaplanan_KDV = 0;
                if (İşTakip_TeslimEdildi_KDV.Checked) Hesaplanan_KDV = Liste.ElementAt(i).Value / 100 * o.KDV_Oranı;

                if (Liste.ElementAt(i).Value + Hesaplanan_KDV + o.ToplamHarcama + o.ToplamKDV > YapılanÖdeme) break;

                o.ToplamHarcama += Liste.ElementAt(i).Value;
                o.ToplamKDV += Hesaplanan_KDV;
                Tablo[0, i].Value = true;
            }

            o.İşlemSonrasıÖnÖdeme = o.MevcutÖnÖdeme + o.AlınanÖdeme - o.ToplamHarcama - o.ToplamKDV;
            İşTakip_TeslimEdildi_Açıklama.Text = o.Yazdır();
            İşTakip_TeslimEdildi_Açıklama.ForeColor = o.İşlemSonrasıÖnÖdeme < 0 ? Color.Red : SystemColors.ControlText;
        }
        private void İşTakip_TeslimEdildi_Sekmeler_ÖdemeAl_KadarİşiSeç_Click_2(object sender, EventArgs e)
        {
            if (!P_İşTakip_TeslimEdildi.Visible) return;

            Ortak.İşTakip_TeslimEdildi_İşSeç_Seç o = new Ortak.İşTakip_TeslimEdildi_İşSeç_Seç();
            İşTakip_TeslimEdildi_Sekmeler_ÖdemeAl.Tag = o;

            if (!double.TryParse(İşTakip_TeslimEdildi_Sekmeler_ÖdemeAl_Miktar.Text, out o.AlınanÖdeme)) o.AlınanÖdeme = 0;

            //Ödeme Yapılarak Ödendi Olarak İşasaretleme
            IDepo_Eleman müş = Banka.Tablo_Dal(İşTakip_Müşteriler.Text, Banka.TabloTürü.Ödemeler, "Mevcut Ön Ödeme");
            if (müş != null) o.MevcutÖnÖdeme = müş.Oku_Sayı(null);

            if (İşTakip_TeslimEdildi_KDV.Checked) o.KDV_Oranı = Banka.Ayarlar_Genel("Bütçe/KDV", true).Oku_Sayı(null, 8);

            for (int i = 0; i < Tablo.RowCount; i++)
            {
                if (!(bool)Tablo[0, i].Value) continue;

                double Hesaplanan_KDV = 0;
                if (İşTakip_TeslimEdildi_KDV.Checked) Hesaplanan_KDV = (double)Tablo[6, i].Tag / 100 * o.KDV_Oranı;

                o.ToplamHarcama += (double)Tablo[6, i].Tag;
                o.ToplamKDV += Hesaplanan_KDV;
                Tablo[0, i].Value = true;
            }

            o.İşlemSonrasıÖnÖdeme = o.MevcutÖnÖdeme + o.AlınanÖdeme - o.ToplamHarcama - o.ToplamKDV;
            İşTakip_TeslimEdildi_Açıklama.Text = o.Yazdır();
            İşTakip_TeslimEdildi_Açıklama.ForeColor = o.İşlemSonrasıÖnÖdeme < 0 ? Color.Red : SystemColors.ControlText;
        }
        private void İşTakip_TeslimEdildi_Sekmeler_ÖdemeAl_ÖdendiOlarakİşsaretle_Click(object sender, EventArgs e)
        {
            if (!Banka.Müşteri_MevcutMu(İşTakip_Müşteriler.Text))
            {
                MessageBox.Show("Lütfen geçerli bir müşteri seçiniz", Text);
                İşTakip_Müşteriler.Focus();
                return;
            }

            İşTakip_TeslimEdildi_Sekmeler_ÖdemeAl_KadarİşiSeç_Click_2(null, null);
            Ortak.İşTakip_TeslimEdildi_İşSeç_Seç o = İşTakip_TeslimEdildi_Sekmeler_ÖdemeAl.Tag as Ortak.İşTakip_TeslimEdildi_İşSeç_Seç;
            if (o == null || (o.ToplamHarcama == 0 && o.AlınanÖdeme == 0))
            {
                MessageBox.Show("Tabloda seçili iş bulunamadı veya aldığınız ödeme 0 ₺ olduğundan işlem ilerleyemedi", Text);
                return;
            }

            string soru = "Alttaki detayların oluşturulmasında kullanılan seçili işler KALICI olarak ÖDENDİ olarak işaretlenecek." + Environment.NewLine + Environment.NewLine +
                "İşleme devam etmek istiyor musunuz?" + Environment.NewLine + Environment.NewLine +
                o.Yazdır_Kısa();
            DialogResult Dr = MessageBox.Show(soru, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (Dr == DialogResult.No) return;

            if (string.IsNullOrWhiteSpace(İşTakip_TeslimEdildi_Sekmeler_ÖdemeAl_Notlar.Text)) İşTakip_TeslimEdildi_Sekmeler_ÖdemeAl_Notlar.Text = null;

            List<string> SeriNo_lar = new List<string>();
        //YenidenDene:
            for (int i = 0; i < Tablo.RowCount; i++)
            {
                if ((bool)Tablo[0, i].Value)
                {
                    SeriNo_lar.Add((string)Tablo[1, i].Value);
                    //Tablo.Rows.RemoveAt(i);
                    //goto YenidenDene;
                }
            }
            Banka.Müşteri_ÖdemeAl(İşTakip_Müşteriler.Text, o, SeriNo_lar, İşTakip_TeslimEdildi_Sekmeler_ÖdemeAl_Notlar.Text);
            Banka.Değişiklikleri_Kaydet(İşTakip_ÖdemeBekleyen_ÖdendiOlarakİşaretle);
            
            İşTakip_TeslimEdildi_Sekmeler_ÖdemeAl_Notlar.Text = "";
            Banka.Değişiklikler_TamponuSıfırla();
            Seviye_Değişti(Seviye2_TeslimEdildi, null);
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
                İşTakip_ÖdemeBekleyen_Açıklama.Text = "Tümünü seçmek / kaldırmak için çift tıklayın";
                return;
            }

            Banka_Tablo_ bt = Banka.Talep_Listele(İşTakip_Müşteriler.Text, Banka.TabloTürü.ÖdemeTalepEdildi, İşTakip_ÖdemeBekleyen_Dönem.Text);
            Banka.Talep_TablodaGöster(Tablo, bt);

            Banka.Talep_Ayıkla_ÖdemeDalı(bt.Ödeme, out List<string> Açıklamalar, out List<string> Ücretler, out _, out _, out _, out string Notlar);
            string ipucu = "";
            for (int i = 0; i < Açıklamalar.Count; i++)
            {
                ipucu += Açıklamalar[i] + " : " + Ücretler[i] + Environment.NewLine;
            }

            if (!string.IsNullOrEmpty(Notlar))
            {
                ipucu += "Notlar : " + Notlar;
            }

            İşTakip_ÖdemeBekleyen_Açıklama.Text = ipucu;
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
            Banka.Değişiklikleri_Kaydet(İşTakip_ÖdemeBekleyen_İptalEt);

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
            Banka.Değişiklikleri_Kaydet(İşTakip_ÖdemeBekleyen_ÖdendiOlarakİşaretle);
            
            İşTakip_ÖdemeBekleyen_Notlar.Text = "";

            Banka.Değişiklikler_TamponuSıfırla();
            Seviye_Değişti(Seviye2_ÖdemeBekleyen, null);
        }
        List<string> İşTakip_Ödendi_Dönem_AramaÇubuğu_Liste = null;
        private void İşTakip_Ödendi_Dönem_AramaÇubuğu_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (İşTakip_Ödendi_Dönem_Dönemler.Items.Count > 0)
                {
                    İşTakip_Ödendi_Dönem_Dönemler.SelectedIndex = 0;
                    İşTakip_Ödendi_Dönem_Dönemler.Focus();
                }
            }
        }
        private void İşTakip_Ödendi_Dönem_AramaÇubuğu_TextChanged(object sender, EventArgs e)
        {
            İşTakip_Ödendi_Dönem_Dönemler.Items.Clear();

            İşTakip_Ödendi_Dönem_Dönemler.Items.AddRange(Ortak.GrupArayıcı(İşTakip_Ödendi_Dönem_AramaÇubuğu_Liste, İşTakip_Ödendi_Dönem_AramaÇubuğu.Text));
        }
        private void İşTakip_Ödendi_Dönem_Dönemler_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Banka.Müşteri_MevcutMu(İşTakip_Müşteriler.Text))
            {
                MessageBox.Show("Lütfen geçerli bir müşteri seçiniz", Text);
                İşTakip_Müşteriler.Focus();
                return;
            }

            if (string.IsNullOrEmpty(İşTakip_Ödendi_Dönem_Dönemler.Text))
            {
                İşTakip_Ödendi_Dönem_Açıklama.Text = "Tümünü seçmek / kaldırmak için çift tıklayın";
                return;
            }

            Banka_Tablo_ bt = Banka.Talep_Listele(İşTakip_Müşteriler.Text, Banka.TabloTürü.Ödendi, İşTakip_Ödendi_Dönem_Dönemler.Text);
            Banka.Talep_TablodaGöster(Tablo, bt);

            Banka.Talep_Ayıkla_ÖdemeDalı(bt.Ödeme, out List<string> Açıklamalar, out List<string> Ücretler, out _, out _, out double İşlemSonrasıÖnÖdeme, out string Notlar);
            string ipucu = "";
            for (int i = 0; i < Açıklamalar.Count; i++)
            {
                ipucu += Açıklamalar[i] + " : " + Ücretler[i] + Environment.NewLine;
            }

            if (!string.IsNullOrEmpty(Notlar))
            {
                ipucu += "Notlar : " + Notlar;
            }

            İşTakip_Ödendi_Dönem_Açıklama.Text = ipucu;
            İşTakip_Ödendi_Dönem_Açıklama.ForeColor = İşlemSonrasıÖnÖdeme < 0 ? Color.Red : SystemColors.ControlText;
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
                depo["Tür", 1] = İşTakip_Müşteriler.Text;

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
                depo["Tür", 1] = İşTakip_Müşteriler.Text;

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
                depo = Banka.Tablo(İşTakip_Müşteriler.Text, Banka.TabloTürü.Ödendi, false, İşTakip_Ödendi_Dönem_Dönemler.Text);
                gerçekdosyadı = "Ödendi_" + İşTakip_Ödendi_Dönem_Dönemler.Text + ".pdf";
            }
            else return;

            if (depo == null)
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
                else
                {
                    hedef = "\"" + hedef + "\"";
                    if (İşTakip_Yazdırma_VeGörüntüle.Checked) System.Diagnostics.Process.Start(hedef);
                    if (İşTakip_Yazdırma_VeKlasörüAç.Checked) System.Diagnostics.Process.Start("explorer.exe", "/select, " + hedef);
                }
            }
            else
            {
                dosyayolu = "\"" + dosyayolu + "\"";
                if (İşTakip_Yazdırma_VeGörüntüle.Checked) System.Diagnostics.Process.Start(dosyayolu);
                if (İşTakip_Yazdırma_VeKlasörüAç.Checked) System.Diagnostics.Process.Start("explorer.exe", "/select, " + dosyayolu);
            }
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

            Depo_ m = Banka.Tablo(İşTakip_Müşteriler.Text, Banka.TabloTürü.Ayarlar);
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
                depo["Tür", 1] = İşTakip_Müşteriler.Text;

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
                depo["Tür", 1] = İşTakip_Müşteriler.Text;

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
                foreach (string ö in Banka.Dosya_Listele_Müşteri(İşTakip_Müşteriler.Text, false))
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

            for (int i = 0; i < Tablo.RowCount; i++)
            {
                Tablo[0, i].Value = b;
            }

            Tablo.Tag = null;

            İşTakip_TeslimEdildi_Sekmeler_ÖdemeAl_KadarİşiSeç_Click_2(null, null);
        }
        private void Tablo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Tablo.Tag != null || e.RowIndex < 0 || e.ColumnIndex < 0 || e.ColumnIndex > 0) return;

            Tablo[0, e.RowIndex].Value = !(bool)Tablo[0, e.RowIndex].Value;

            İşTakip_TeslimEdildi_Sekmeler_ÖdemeAl_KadarİşiSeç_Click_2(null, null);
        }
        private void Tablo_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (Tablo.Tag != null || e.RowIndex < 0) return;

            string m;
            if ((int)Seviye1_işTakip.Tag == 3 && Malzemeler_Malzeme.Text.DoluMu())
            {
                //İştakip -> Malzemeler Sayfası
                m = "Miktar (" + Banka.Tablo_Dal(null, Banka.TabloTürü.Malzemeler, "Malzemeler/" + Malzemeler_Malzeme.Text)[1] + ")";
            }
            else m = "Notlar";
            
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

            string[] arananlar = TabloİçeriğiArama.Text.ToLower().Split(' ');
            for (int satır = 0; satır < Tablo.RowCount && !TabloİçeriğiArama_KapatmaTalebi; satır++)
            {
                bool bulundu = false;
                for (int sutun = 1; sutun < Tablo.Columns.Count; sutun++)
                {
                    string içerik = (string)Tablo[sutun, satır].Value;
                    if (string.IsNullOrEmpty(içerik)) Tablo[sutun, satır].Style.BackColor = Color.White;
                    else
                    {
                        içerik = içerik.ToLower();
                        int bulundu_adet = 0;
                        foreach (string arn in arananlar)
                        {
                            if (!içerik.Contains(arn)) break;
                                
                            bulundu_adet++;
                        }

                        if (bulundu_adet == arananlar.Length)
                        {
                            Tablo[sutun, satır].Style.BackColor = Color.YellowGreen;
                            bulundu = true;
                        }
                        else Tablo[sutun, satır].Style.BackColor = Color.White;
                    }
                }

                Tablo.Rows[satır].Visible = bulundu;
                if (bulundu) TabloİçeriğiArama_Sayac_Bulundu++;

                if (TabloİçeriğiArama_Tik < Environment.TickCount) { Application.DoEvents(); TabloİçeriğiArama_Tik = Environment.TickCount + 500; }
            }

            if (TabloİçeriğiArama_Sayac_Bulundu == 0) TabloİçeriğiArama_Sayac_Bulundu = -1;
            else Tablo_Notlar.HeaderText = "Bulundu : " + TabloİçeriğiArama_Sayac_Bulundu;

            TabloİçeriğiArama.BackColor = Color.White;
            TabloİçeriğiArama_Çalışıyor = false;
            Tablo.ClearSelection();

            if (TabloİçeriğiArama_KapatmaTalebi) TabloİçeriğiArama_TextChanged(null, null);
            TabloİçeriğiArama_KapatmaTalebi = false;
        }

        string Arama_Sorgula_Aranan_İşTürleri = null;
        private void Arama_Sorgula_Click(object sender, EventArgs e)
        {
            Ortak.Gösterge.Başlat("Sayılıyor", true, Arama_Sorgula, 0);
            TabloİçeriğiArama.Text = null;

            if (Arama_GirişTarihi_Başlangıç.Value > Arama_GirişTarihi_Bitiş.Value)
            {
                DateTime gecici = Arama_GirişTarihi_Başlangıç.Value;
                Arama_GirişTarihi_Başlangıç.Value = new DateTime(Arama_GirişTarihi_Bitiş.Value.Year, Arama_GirişTarihi_Bitiş.Value.Month, Arama_GirişTarihi_Bitiş.Value.Day, 0, 0, 0);
                Arama_GirişTarihi_Bitiş.Value = new DateTime(gecici.Year, gecici.Month, gecici.Day, 23, 59, 00);
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
            Arama_Sorgula_Aranan_İşTürleri = null;
            if (Arama_İş_Türleri.CheckedItems.Count != Arama_İş_Türleri.Items.Count)
            {
                for (int i = 0; i < Arama_İş_Türleri.Items.Count; i++)
                {
                    if (Arama_İş_Türleri.GetItemChecked(i)) Arama_Sorgula_Aranan_İşTürleri += "-_" + Arama_İş_Türleri.Items[i].ToString() + "_-";
                }
            }
            if (!Seviye2_DevamEden.Checked && !Seviye2_TeslimEdildi.Checked && !Seviye2_ÖdemeBekleyen.Checked && !Seviye2_Ödendi.Checked)
            {
                Seviye2_DevamEden.Checked = true;
            }

            int kademe = 0;
            for (int i = 0; i < Arama_Müşteriler.Items.Count && Ortak.Gösterge.Çalışsın; i++)
            {
                if (Arama_Müşteriler.GetItemChecked(i) && Ortak.Gösterge.Çalışsın)
                {
                    if (Seviye2_DevamEden.Checked) kademe += 1;
                    if (Seviye2_TeslimEdildi.Checked) kademe += 1;
                    if (Seviye2_ÖdemeBekleyen.Checked) kademe += Banka.Dosya_Listele_Müşteri(Arama_Müşteriler.Items[i].ToString(), false).Length;
                    if (Seviye2_Ödendi.Checked) kademe += Banka.Dosya_Listele_Müşteri(Arama_Müşteriler.Items[i].ToString(), true).Length;
                }
            }
            Ortak.Gösterge.Başlat("Sorgu devam ediyor", true, Arama_Sorgula, kademe);

            Banka_Tablo_ bt = new Banka_Tablo_(null);
            bt.Türü = Banka.TabloTürü.DevamEden_TeslimEdildi_ÖdemeTalepEdildi_Ödendi;
            Banka.Talep_TablodaGöster(Tablo, bt);
            Tablo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            Tablo.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            for (int i = 0; i < Arama_Müşteriler.Items.Count && Ortak.Gösterge.Çalışsın; i++)
            {
                if (Arama_Müşteriler.GetItemChecked(i))
                {
                    if (Seviye2_DevamEden.Checked && Ortak.Gösterge.Çalışsın)
                    {
                        Ortak.Gösterge.İlerleme = 1;
                        Arama_Sorgula_Click_2(Banka.Talep_Listele(Arama_Müşteriler.Items[i].ToString(), Banka.TabloTürü.DevamEden));
                    }

                    if (Seviye2_TeslimEdildi.Checked && Ortak.Gösterge.Çalışsın)
                    {
                        Ortak.Gösterge.İlerleme = 1;
                        Arama_Sorgula_Click_2(Banka.Talep_Listele(Arama_Müşteriler.Items[i].ToString(), Banka.TabloTürü.TeslimEdildi));
                    }

                    if (Seviye2_ÖdemeBekleyen.Checked && Ortak.Gösterge.Çalışsın)
                    {
                        string[] l = Banka.Dosya_Listele_Müşteri(Arama_Müşteriler.Items[i].ToString(), false);

                        for (int s = 0; s < l.Length && Ortak.Gösterge.Çalışsın; s++)
                        {
                            Ortak.Gösterge.İlerleme = 1;
                            //DateTime t = l[s].TarihSaate(ArgeMup.HazirKod.Dönüştürme.D_TarihSaat.Şablon_DosyaAdı2);
                            //if (Arama_GirişTarihi_Başlangıç.Value > t || t > Arama_GirişTarihi_Bitiş.Value) continue;

                            Arama_Sorgula_Click_2(Banka.Talep_Listele(Arama_Müşteriler.Items[i].ToString(), Banka.TabloTürü.ÖdemeTalepEdildi, l[s]));
                        }
                    }

                    if (Seviye2_Ödendi.Checked && Ortak.Gösterge.Çalışsın)
                    {
                        string[] l = Banka.Dosya_Listele_Müşteri(Arama_Müşteriler.Items[i].ToString(), true);

                        for (int s = 0; s < l.Length && Ortak.Gösterge.Çalışsın; s++)
                        {
                            Ortak.Gösterge.İlerleme = 1;
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
            Tablo.ClearSelection();
            if (Tablo.RowCount > 0) Tablo[0, 0].Value = true; //sayım yapması için
            Tablo.Tag = null;
            if (Tablo.RowCount > 0) Tablo[0, 0].Value = false; //sayım yapması için

            Ortak.Gösterge.Bitir();
            TabloİçeriğiArama.Focus();
        }
        private void Arama_Sorgula_Click_2(Banka_Tablo_ bt)
        {
            if (!Ortak.Gösterge.Çalışsın) return;
            
            string sn_ler = "";
            List<IDepo_Eleman> uyuşanlar = new List<IDepo_Eleman>();
            foreach (IDepo_Eleman serino in bt.Talepler)
            {
                bool evet = false;

                foreach (IDepo_Eleman iş in serino.Elemanları)
                {
                    Banka.Talep_Ayıkla_İşTürüDalı(iş, out string İşTürü, out string GirişTarihi, out string _, out string _, out string _);

                    if (Arama_Sorgula_Aranan_İşTürleri.DoluMu() && !Arama_Sorgula_Aranan_İşTürleri.Contains(İşTürü)) continue;

                    DateTime t = GirişTarihi.TarihSaate();
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
                Banka.Talep_Ayıkla_ÖdemeDalı(bt.Ödeme, out List<string> Açıklamalar, out List<string> Ücretler, out _, out _, out _, out string Notlar);
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

        List<string> Malzemeler_Malzeme_AramaÇubuğu_Liste = null;
        private void Malzemeler_Malzeme_AramaÇubuğu_TextChanged(object sender, EventArgs e)
        {
            Malzemeler_Malzeme.Items.Clear();

            if (string.IsNullOrEmpty(Malzemeler_Malzeme_AramaÇubuğu.Text))
            {
                Malzemeler_Malzeme.Items.AddRange(Malzemeler_Malzeme_AramaÇubuğu_Liste.ToArray());
            }
            else
            {
                string gecici = Malzemeler_Malzeme_AramaÇubuğu.Text.ToLower();
                Malzemeler_Malzeme.Items.AddRange(Malzemeler_Malzeme_AramaÇubuğu_Liste.FindAll(x => x.ToLower().Contains(gecici)).ToArray());
            }
        }
        private void Malzemeler_Malzeme_SelectedIndexChanged(object sender, EventArgs e)
        {
            Banka.Malzeme_KullanımDetayı_TablodaGöster(Tablo, Malzemeler_Malzeme.Text);
        }
        private void Malzemeler_SeçilenleriSil_Click(object sender, EventArgs e)
        {
            if (!Banka.Malzeme_MevcutMu(Malzemeler_Malzeme.Text))
            {
                MessageBox.Show("Lütfen geçerli bir malzeme seçiniz", Text);
                Malzemeler_Malzeme.Focus();
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

            Banka.Malzeme_KullanımDetayı_Tablodaki_SeçiliOlanlarıSil(Tablo, Malzemeler_Malzeme.Text);
            Banka.Değişiklikleri_Kaydet(İşTakip_DevamEden_Sil);

        YenidenDene:
            for (int i = 0; i < Tablo.RowCount; i++)
            {
                if ((bool)Tablo[0, i].Value)
                {
                    Tablo.Rows.RemoveAt(i);
                    goto YenidenDene;
                }
            }
            if (Tablo.RowCount > 0) Tablo_CellValueChanged(null, new DataGridViewCellEventArgs(0, 0));
        }
    }
}
