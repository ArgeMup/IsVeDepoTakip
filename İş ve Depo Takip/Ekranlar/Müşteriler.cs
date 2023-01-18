using ArgeMup.HazirKod;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip
{
    public partial class Müşteriler : Form
    {
        public Müşteriler()
        {
            InitializeComponent();

            Ortak.GeçiciDepolama_PencereKonumları_Oku(this);
        
            Liste.Items.Clear();
            AramaÇubuğu_Liste = Banka.Müşteri_Listele();
            Liste.Items.AddRange(AramaÇubuğu_Liste.ToArray());
        }

        List<string> AramaÇubuğu_Liste = null;
        private void AramaÇubuğu_TextChanged(object sender, EventArgs e)
        {
            splitContainer1.Panel2.Enabled = false;
            Liste.Items.Clear();

            if (string.IsNullOrEmpty(AramaÇubuğu.Text))
            {
                Liste.Items.AddRange(AramaÇubuğu_Liste.ToArray());
            }
            else
            {
                AramaÇubuğu.Text = AramaÇubuğu.Text.ToLower();
                Liste.Items.AddRange(AramaÇubuğu_Liste.FindAll(x => x.ToLower().Contains(AramaÇubuğu.Text)).ToArray());
            }
        }

        private void Liste_SelectedValueChanged(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(Liste.Text)) { splitContainer1.Panel2.Enabled = false; return; }
            Yeni.Text = Liste.Text;

            IDepo_Eleman m = Banka.Müşteri_Ayarlar(Liste.Text);
            if (m != null)
            {
                Eposta_Kime.Text = m.Oku("Eposta/Kime");
                Eposta_Bilgi.Text = m.Oku("Eposta/Bilgi");
                Eposta_Gizli.Text = m.Oku("Eposta/Gizli");
                Notlar.Text = m.Oku("Notlar");
            }

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
            if (!Liste.Visible || string.IsNullOrEmpty(Liste.Text) || string.IsNullOrWhiteSpace(Yeni.Text)) return;
            
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

            if (System.IO.Directory.Exists(Ortak.Klasör_Banka + Yeni.Text))
            {
                string soru = "Yeni isime ait önceden kalma dosyalar bulundu." + Environment.NewLine +
                    "İşleme devam ederseniz alttaki klasördeki dosyalar SİLİNECEK." + Environment.NewLine +
                    Ortak.Klasör_Banka + Yeni.Text + Environment.NewLine + Environment.NewLine +
                    "İşleme devam etmek istiyor musunuz?";

                Dr = MessageBox.Show(soru, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (Dr == DialogResult.No) return;

                if (!Klasör.Sil(Ortak.Klasör_Banka + Yeni.Text))
                {
                    throw new Exception("Klasör silinemedi." + Environment.NewLine + Ortak.Klasör_Banka + Yeni.Text);
                }
            }

            Banka.Değişiklikler_TamponuSıfırla();
            Banka.Müşteri_İsminiDeğiştir(Liste.Text, Yeni.Text);
            Banka.Değişiklikleri_Kaydet(Liste);
            Banka.Değişiklikler_TamponuSıfırla();

            AramaÇubuğu_Liste.Remove(Liste.Text);
            Liste.Items.RemoveAt(Liste.SelectedIndex);
            AramaÇubuğu_Liste.Add(Yeni.Text);
            Liste.Items.Add(Yeni.Text);
        }
        private void SağTuşMenü_Sil_Click(object sender, EventArgs e)
        {
            if (!Liste.Visible || Liste.SelectedIndex < 0 || Liste.SelectedIndex >= Liste.Items.Count) return;

            string soru = Liste.Text + " öğesi görünmez yapılacak. Devamında müşteriye ait mevcut kayıtları SİLMEK istiyor musunuz?" + Environment.NewLine + Environment.NewLine +
                "Evet : Müşteriye ait TÜM KAYITLARI SİL." + Environment.NewLine +
                "Hayır : Sadece görünmez yap." + Environment.NewLine +
                "İptal : İşlemi iptal et";
            DialogResult Dr = MessageBox.Show(soru, Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3);
            if (Dr == DialogResult.Cancel) return;
		    else if (Dr == DialogResult.Yes)
            {
                if (!Klasör.Sil(Ortak.Klasör_Banka + Liste.Text))
                {
                    throw new Exception("Klasör silinemedi." + Environment.NewLine + Ortak.Klasör_Banka + Liste.Text);
                }
            }	
			
            Banka.Müşteri_Sil(Liste.Text);
            Banka.Değişiklikleri_Kaydet(Liste);

            AramaÇubuğu_Liste.Remove(Liste.Text);
            Liste.Items.RemoveAt(Liste.SelectedIndex);
        }
        private void Ekle_Click(object sender, System.EventArgs e)
        {
            if (Banka.Müşteri_MevcutMu(Yeni.Text))
            {
                MessageBox.Show("Önceden eklenmiş", Text);
                return;
            }

            if (!System.IO.Directory.Exists(Ortak.Klasör_Banka + Yeni.Text))
            {
                if (!Klasör.Oluştur(Ortak.Klasör_Banka + Yeni.Text))
                {
                    MessageBox.Show(Yeni.Text + " girdisi ile klasör oluşturulamıyor, uygun olmayan karakterleri değiştiriniz", Text);
                    return;
                }
                else Klasör.Sil(Ortak.Klasör_Banka + Yeni.Text);
            }
            
            Banka.Müşteri_Ekle(Yeni.Text);
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
            IDepo_Eleman m = Banka.Müşteri_Ayarlar(Liste.Text, true);
            m.Yaz("Eposta/Kime", Eposta_Kime.Text);
            m.Yaz("Eposta/Bilgi", Eposta_Bilgi.Text);
            m.Yaz("Eposta/Gizli", Eposta_Gizli.Text);
            m.Yaz("Notlar", Notlar.Text.Trim());
            Banka.Değişiklikleri_Kaydet(Kaydet);

            splitContainer1.Panel1.Enabled = true;
            Liste_SelectedValueChanged(null, null);
        }
    }
}
