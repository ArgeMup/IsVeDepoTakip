using ArgeMup.HazirKod;
using ArgeMup.HazirKod.Ekİşlemler;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip
{
    public partial class Malzemeler : Form
    {
        public Malzemeler()
        {
            InitializeComponent();
        }
        private void Malzemeler_Load(object sender, System.EventArgs e)
        {
            Liste.Items.Clear();
            AramaÇubuğu_Liste = Banka.Malzeme_Listele();
            Liste.Items.AddRange(AramaÇubuğu_Liste.ToArray());
            if (Liste.Items.Count > 0) Sil.Enabled = true;

            Ortak.GeçiciDepolama_PencereKonumları_Oku(this);

            KeyDown += Malzemeler_Tuş;
            KeyUp += Malzemeler_Tuş;
            MouseWheel += Malzemeler_MouseWheel;
            KeyPreview = true;
        }
        bool ctrl_tuşuna_basıldı = false;
        private void Malzemeler_Tuş(object sender, KeyEventArgs e)
        {
            ctrl_tuşuna_basıldı = e.Control;
        }
        private void Malzemeler_MouseWheel(object sender, MouseEventArgs e)
        {
            if (ctrl_tuşuna_basıldı)
            {
                WindowState = FormWindowState.Normal;
                if (e.Delta > 0) Font = new Font(Font.FontFamily, Font.Size + 0.2f);
                else Font = new Font(Font.FontFamily, Font.Size - 0.2f);
            }
        }
        private void Malzemeler_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Kaydet.Enabled)
            {
                DialogResult Dr = MessageBox.Show("Değişiklikleri kaydetmeden çıkmak istediğinize emin misiniz?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                e.Cancel = Dr == DialogResult.No;
            }
        }
        private void Malzemeler_FormClosed(object sender, FormClosedEventArgs e)
        {
            Ortak.GeçiciDepolama_PencereKonumları_Yaz(this);
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

            Banka.Malzeme_TablodaGöster(Tablo, Liste.Text, out double Miktar, out string Birim, out double UyarıVermeMiktar, out string Notları);
            Miktarı.Text = Miktar.Yazıya();
            Birimi.Text = Birim;
            UyarıMiktarı.Text = UyarıVermeMiktar.Yazıya();
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
        private void Sil_Click(object sender, System.EventArgs e)
        {
            if (Liste.SelectedIndex < 0 || Liste.SelectedIndex >= Liste.Items.Count) return;

            DialogResult Dr = MessageBox.Show(Liste .Text + " öğesini silmek istediğinize emin misiniz?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (Dr == DialogResult.No) return;

            Banka.Malzeme_Sil(Liste.Text);
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
            if (Banka.Malzeme_MevcutMu(Yeni.Text))
            {
                MessageBox.Show("Önceden eklenmiş", Text);
                return;
            }

            Banka.Malzeme_Ekle(Yeni.Text);
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
            double mevcut, uyarımiktarı;

            try
            {
                mevcut = Miktarı.Text.NoktalıSayıya(true, false);
            }
            catch (Exception)
            {
                MessageBox.Show("Mevcut kutucuğu içeriği sayıya dönüştürülemedi", Text);
                Miktarı.Focus();
                return;
            }

            try
            {
                uyarımiktarı = UyarıMiktarı.Text.NoktalıSayıya(true, false);
            }
            catch (Exception)
            {
                MessageBox.Show("Uyarı Miktarı kutucuğu içeriği sayıya dönüştürülemedi", Text);
                UyarıMiktarı.Focus();
                return;
            }

            Banka.Malzeme_DetaylarıKaydet(Liste.Text, mevcut, Birimi.Text, uyarımiktarı, Notlar.Text.Trim());
            Banka.Değişiklikleri_Kaydet();

            splitContainer1.Panel1.Enabled = true;
            Liste_SelectedValueChanged(null, null);
        }
    }
}
