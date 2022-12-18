using ArgeMup.HazirKod;
using System;
using System.Windows.Forms;
using static ArgeMup.HazirKod.MesajPanosu_;

namespace İş_ve_Depo_Takip
{
    public partial class Müşteriler : Form
    {
        public Müşteriler()
        {
            InitializeComponent();
        }
        private void Müşteriler_Load(object sender, System.EventArgs e)
        {
            Liste.Items.Clear();
            Liste.Items.AddRange(Banka.Müşteri_Listele().ToArray());
            if (Liste.Items.Count > 0) Sil.Enabled = true;

            Ortak.GeçiciDepolama_PencereKonumları_Oku(this);
        }
        private void Müşteriler_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Kaydet.Enabled)
            {
                DialogResult Dr = MessageBox.Show("Değişiklikleri kaydetmeden çıkmak istediğinize emin misiniz?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                e.Cancel = Dr == DialogResult.No;
            }
        }
        private void Müşteriler_FormClosed(object sender, FormClosedEventArgs e)
        {
            Ortak.GeçiciDepolama_PencereKonumları_Yaz(this);
        }

        private void Liste_SelectedValueChanged(object sender, System.EventArgs e)
        {
            Sil.Enabled = !string.IsNullOrEmpty(Liste.Text);

            if (!Sil.Enabled) { splitContainer1.Panel2.Enabled = false; return; }

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
            m.Yaz("Notlar", Notlar.Text);
            Banka.Değişiklikleri_Kaydet();

            splitContainer1.Panel1.Enabled = true;
            Kaydet.Enabled = false;
        }
    }
}
