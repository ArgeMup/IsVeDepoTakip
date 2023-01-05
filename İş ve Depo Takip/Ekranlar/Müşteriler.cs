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
            if (Liste.Items.Count > 0) Sil.Enabled = true;
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
            Sil.Enabled = !string.IsNullOrEmpty(Liste.Text);

            if (!Sil.Enabled) { splitContainer1.Panel2.Enabled = false; return; }
            Yeni.Text = Liste.Text;

            IDepo_Eleman m = Banka.Tablo_Dal(null, Banka.TabloTürü.Ayarlar, "Müşteriler/" + Liste.Text);
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
        private void Sil_Click(object sender, System.EventArgs e)
        {
            if (Liste.SelectedIndex < 0 || Liste.SelectedIndex >= Liste.Items.Count) return;

            DialogResult Dr = MessageBox.Show(Liste .Text + " öğesini silmek istediğinize emin misiniz?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (Dr == DialogResult.No) return;

            Banka.Müşteri_Sil(Liste.Text);
            Banka.Değişiklikleri_Kaydet();

            AramaÇubuğu_Liste.Remove(Liste.Text);
            Liste.Items.RemoveAt(Liste.SelectedIndex);
        }
        private void Yeni_TextChanged(object sender, System.EventArgs e)
        {
            Ekle.Enabled = !string.IsNullOrWhiteSpace(Yeni.Text);
        }
        private void Ekle_Click(object sender, System.EventArgs e)
        {
            if (Banka.Müşteri_MevcutMu(Yeni.Text))
            {
                MessageBox.Show("Önceden eklenmiş", Text);
                return;
            }

            if (!Klasör.Oluştur(Ortak.Klasör_Banka + Yeni.Text))
            {
                MessageBox.Show(Yeni.Text + " girdisi ile klasör oluşturulamıyor, uygun olmayan karakterleri değiştiriniz", Text);
                return;
            }
            else Klasör.Sil(Ortak.Klasör_Banka + Yeni.Text);

            Banka.Müşteri_Ekle(Yeni.Text);
            Banka.Değişiklikleri_Kaydet();

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
            IDepo_Eleman m = Banka.Tablo_Dal(null, Banka.TabloTürü.Ayarlar, "Müşteriler/" + Liste.Text, true);
            m.Yaz("Eposta/Kime", Eposta_Kime.Text);
            m.Yaz("Eposta/Bilgi", Eposta_Bilgi.Text);
            m.Yaz("Eposta/Gizli", Eposta_Gizli.Text);
            m.Yaz("Notlar", Notlar.Text.Trim());
            Banka.Değişiklikleri_Kaydet();

            splitContainer1.Panel1.Enabled = true;
            Liste_SelectedValueChanged(null, null);
        }
    }
}
