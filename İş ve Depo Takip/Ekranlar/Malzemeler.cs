using ArgeMup.HazirKod;
using ArgeMup.HazirKod.Ekİşlemler;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip.Ekranlar
{
    public partial class Malzemeler : Form
    {
        public Malzemeler()
        {
            InitializeComponent();

            Ortak.GeçiciDepolama_PencereKonumları_Oku(this);
        
            Liste.Items.Clear();
            AramaÇubuğu_Liste = Banka.Malzeme_Listele();
            Liste.Items.AddRange(AramaÇubuğu_Liste.ToArray());
        }

        List<string> AramaÇubuğu_Liste = null;
        private void AramaÇubuğu_TextChanged(object sender, EventArgs e)
        {
            splitContainer1.Panel2.Enabled = false;
            Liste.Items.Clear();

            Liste.Items.AddRange(Ortak.GrupArayıcı(AramaÇubuğu_Liste, AramaÇubuğu.Text));
        }

        private void Liste_SelectedValueChanged(object sender, System.EventArgs e)
        {
            if (Liste.SelectedIndex < 0) { splitContainer1.Panel2.Enabled = false; return; }
            Yeni.Text = Liste.Text;

            Banka.Malzeme_TablodaGöster(Tablo, Liste.Text, out double Miktar, out string Birim, out double UyarıVermeMiktar, out bool Detaylı, out string Notları);
            Miktarı.Text = Miktar.Yazıya();
            Birimi.Text = Birim;
            UyarıMiktarı.Text = UyarıVermeMiktar.Yazıya();
            DetaylıKullanım.Checked = Detaylı;
            Notlar.Text = Notları;

            if (UyarıVermeMiktar > 0 && Miktar <= UyarıVermeMiktar)
            {
                Miktarı.BackColor = Color.Salmon;
            }
            else Miktarı.BackColor = Color.White;

            splitContainer1.Panel1.Enabled = true;
            splitContainer1.Panel2.Enabled = true;
            Kaydet.Enabled = false;
            Liste.Focus();
        }
        private void Yeni_TextChanged(object sender, System.EventArgs e)
        {
            Ekle.Enabled = !string.IsNullOrWhiteSpace(Yeni.Text);
        }
        private void SağTuşMenü_YenidenAdlandır_Click(object sender, EventArgs e)
        {
            if (!Liste.Enabled || Liste.SelectedIndex < 0 || string.IsNullOrWhiteSpace(Yeni.Text)) return;

            if (Liste.Text == Yeni.Text)
            {
                MessageBox.Show("Mevcut ile yeni isimler aynı", Text);
                return;
            }

            if (Banka.Malzeme_MevcutMu(Yeni.Text))
            {
                MessageBox.Show("Kullanılmayan bir isim seçiniz", Text);
                return;
            }

            DialogResult Dr = MessageBox.Show("İsim değişikliği işlemine devam etmek istiyor musunuz?" + Environment.NewLine + Environment.NewLine +
                Liste.Text + " -> " + Yeni.Text, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (Dr == DialogResult.No) return;

            Ortak.Gösterge.Başlat("Bekleyiniz", false, Liste, System.IO.Directory.GetFiles(Ortak.Klasör_Banka, "*.mup", System.IO.SearchOption.AllDirectories).Length);

            Banka.Malzeme_YenidenAdlandır(Liste.Text, Yeni.Text);
            Banka.Değişiklikleri_Kaydet(Liste);
            Banka.Değişiklikler_TamponuSıfırla();
            Ortak.Gösterge.Bitir();

            AramaÇubuğu_Liste.Remove(Liste.Text);
            Liste.Items.RemoveAt(Liste.SelectedIndex);
            AramaÇubuğu_Liste.Add(Yeni.Text);
            Liste.Items.Add(Yeni.Text);
        }
        private void SağTuşMenü_Sil_Click(object sender, EventArgs e)
        {
            if (!Liste.Enabled || Liste.SelectedIndex < 0 || Liste.SelectedIndex >= Liste.Items.Count) return;

            int Sayac_Kullanım = 0;
            for (int i = 0; i < Tablo.RowCount; i++)
            {
                if (Tablo[0, i].Tag != null) Sayac_Kullanım++;
            }

            string soru = Liste.Text + " öğesi görünmez yapılacak. Devamında malzemeye ait mevcut kayıtları SİLMEK istiyor musunuz?" + Environment.NewLine + Environment.NewLine +
                "Evet : Malzemeye ait TÜM KAYITLARI SİL." + Environment.NewLine +
                "Hayır : Sadece görünmez yap." + Environment.NewLine +
                "İptal : İşlemi iptal et" +
                (Sayac_Kullanım > 0 ? Environment.NewLine + Environment.NewLine + Sayac_Kullanım + " adet iş türü içerisinde KULLANILIYOR." : null);
            DialogResult Dr = MessageBox.Show(soru, Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3);
            if (Dr == DialogResult.Cancel) return;
            else if (Dr == DialogResult.Yes)
            {
                if (!Klasör.Sil(Ortak.Klasör_Banka_MalzemeKullanımDetayları + Liste.Text))
                {
                    throw new Exception("Klasör silinemedi." + Environment.NewLine + Ortak.Klasör_Banka_MalzemeKullanımDetayları + Liste.Text);
                }
            }

            Banka.Malzeme_Sil(Liste.Text);
            Banka.Değişiklikleri_Kaydet(Liste);

            AramaÇubuğu_Liste.Remove(Liste.Text);
            Liste.Items.RemoveAt(Liste.SelectedIndex);
        }
        private void Ekle_Click(object sender, System.EventArgs e)
        {
            if (Banka.Malzeme_MevcutMu(Yeni.Text))
            {
                MessageBox.Show("Önceden eklenmiş", Text);
                return;
            }

            Banka.Malzeme_Ekle(Yeni.Text);
            Banka.Değişiklikleri_Kaydet(Ekle);
            
            Liste.Items.Add(Yeni.Text);
            AramaÇubuğu_Liste.Add(Yeni.Text);
        }

        private void Ayar_Değişti(object sender, EventArgs e)
        {
            splitContainer1.Panel1.Enabled = false;
            Kaydet.Enabled = true;
        }
        private void Kaydet_Click(object sender, EventArgs e)
        {
            string mevcut = Miktarı.Text;
            if (!Ortak.YazıyıSayıyaDönüştür(ref mevcut, "Mevcut kutucuğu")) return;

            string uyarımiktarı = UyarıMiktarı.Text;
            if (!Ortak.YazıyıSayıyaDönüştür(ref uyarımiktarı, "Uyarı Miktarı kutucuğu", null, 0)) return;

            if (Birimi.Text.BoşMu(true))
            {
                MessageBox.Show("Lütfen birimi alanını doldurunuz.");
                Birimi.Focus();
                return;
            }

            Banka.Malzeme_DetaylarıKaydet(Liste.Text, mevcut, Birimi.Text, uyarımiktarı, DetaylıKullanım.Checked, Notlar.Text.Trim());
            Banka.Değişiklikleri_Kaydet(Kaydet);

            splitContainer1.Panel1.Enabled = true;
            Liste_SelectedValueChanged(null, null);
        }
    }
}
