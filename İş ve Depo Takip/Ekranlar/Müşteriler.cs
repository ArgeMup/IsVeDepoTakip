using ArgeMup.HazirKod;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip.Ekranlar
{
    public partial class Müşteriler : Form
    {
        public Müşteriler()
        {
            InitializeComponent();

            AramaÇubuğu_Liste = Banka.Müşteri_Listele(true);
            Ortak.GrupArayıcı(Liste, AramaÇubuğu_Liste);
        }

        List<string> AramaÇubuğu_Liste = null;
        private void AramaÇubuğu_TextChanged(object sender, EventArgs e)
        {
            splitContainer1.Panel2.Enabled = false;

            Ortak.GrupArayıcı(Liste, AramaÇubuğu_Liste, AramaÇubuğu.Text);
        }

        private void Liste_SelectedValueChanged(object sender, System.EventArgs e)
        {
            if (Liste.SelectedIndex < 0) { splitContainer1.Panel2.Enabled = false; return; }
            Yeni.Text = Liste.Text;

            IDepo_Eleman m = Banka.Ayarlar_Müşteri(Liste.Text, "Eposta", true);
            Eposta_Kime.Text = m.Oku("Kime");
            Eposta_Bilgi.Text = m.Oku("Bilgi");
            Eposta_Gizli.Text = m.Oku("Gizli");
            Notlar.Text = Banka.Ayarlar_Müşteri(Liste.Text, "Notlar", true)[0];

            splitContainer1.Panel1.Enabled = true;
            splitContainer1.Panel2.Enabled = true;
            Kaydet.Enabled = false;
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

            if (Banka.Müşteri_MevcutMu(Yeni.Text))
            {
                MessageBox.Show("Kullanılmayan bir isim seçiniz", Text);
                return;
            }

            DialogResult Dr = MessageBox.Show("İsim değişikliği işlemine devam etmek istiyor musunuz?" + Environment.NewLine + Environment.NewLine +
                Liste.Text + " -> " + Yeni.Text, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (Dr == DialogResult.No) return;

            Banka.Müşteri_YenidenAdlandır(Liste.Text, Yeni.Text);
            Banka.Değişiklikleri_Kaydet(Liste);
            Banka.Değişiklikler_TamponuSıfırla();

            AramaÇubuğu_Liste.Remove(Liste.Text);
            AramaÇubuğu_Liste.Add(Yeni.Text);
            Ortak.GrupArayıcı(Liste, AramaÇubuğu_Liste, AramaÇubuğu.Text);
        }
        private void SağTuşMenü_Sil_Click(object sender, EventArgs e)
        {
            if (!Liste.Enabled || Liste.SelectedIndex < 0 || Liste.SelectedIndex >= Liste.Items.Count) return;

            if (Liste.Text.StartsWith(".:Gizli:. "))
            {
                string soru = Liste.Text + " öğesi tüm kayıtlarıyla birlikte KALICI olarak SİLİNECEK." + Environment.NewLine + Environment.NewLine +
                    "İşleme devam etmek istiyor musunuz?";
                DialogResult Dr = MessageBox.Show(soru, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (Dr == DialogResult.No) return;

                Banka.Müşteri_Sil(Liste.Text);
            }
            else
            {
                string soru = Liste.Text + " öğesi görünmez yapılacak. Devamında müşteriye ait mevcut kayıtları SİLMEK istiyor musunuz?" + Environment.NewLine + Environment.NewLine +
                "Evet : Müşteriye ait TÜM KAYITLARI SİL." + Environment.NewLine +
                "Hayır : Sadece görünmez yap." + Environment.NewLine +
                "İptal : İşlemi iptal et";
                
                DialogResult Dr = MessageBox.Show(soru, Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3);
                if (Dr == DialogResult.Cancel) return;
                else if (Dr == DialogResult.Yes) Banka.Müşteri_Sil(Liste.Text);
                else
                {
                    Banka.Müşteri_YenidenAdlandır(Liste.Text, ".:Gizli:. " + Liste.Text);
                    AramaÇubuğu_Liste.Add(".:Gizli:. " + Liste.Text);
                }
            }

            Banka.Değişiklikleri_Kaydet(Liste);
            Banka.Değişiklikler_TamponuSıfırla();

            AramaÇubuğu_Liste.Remove(Liste.Text);
            Ortak.GrupArayıcı(Liste, AramaÇubuğu_Liste, AramaÇubuğu.Text);
        }
        private void Ekle_Click(object sender, System.EventArgs e)
        {
            if (Banka.Müşteri_MevcutMu(Yeni.Text))
            {
                MessageBox.Show("Önceden eklenmiş", Text);
                return;
            }

            Banka.Müşteri_Ekle(Yeni.Text);
            Banka.Değişiklikleri_Kaydet(Ekle);

            AramaÇubuğu_Liste.Add(Yeni.Text);
            Ortak.GrupArayıcı(Liste, AramaÇubuğu_Liste, AramaÇubuğu.Text);
        }

        private void Ayar_Değişti(object sender, EventArgs e)
        {
            splitContainer1.Panel1.Enabled = false;
            Kaydet.Enabled = true;
        }
        private void Kaydet_Click(object sender, EventArgs e)
        {
            Eposta_Kime.Text = Eposta_Kime.Text.Trim().ToLower();
            Eposta_Bilgi.Text = Eposta_Bilgi.Text.Trim().ToLower();
            Eposta_Gizli.Text = Eposta_Gizli.Text.Trim().ToLower();

            IDepo_Eleman m = Banka.Ayarlar_Müşteri(Liste.Text, "Eposta", true);
            m.Yaz("Kime", Eposta_Kime.Text);
            m.Yaz("Bilgi", Eposta_Bilgi.Text);
            m.Yaz("Gizli", Eposta_Gizli.Text);
            Banka.Ayarlar_Müşteri(Liste.Text, "Notlar", true)[0] = Notlar.Text.Trim();
            Banka.Değişiklikleri_Kaydet(Kaydet);

            splitContainer1.Panel1.Enabled = true;
            Liste_SelectedValueChanged(null, null);

            Ekranlar.Eposta.Durdur();
        }
    }
}