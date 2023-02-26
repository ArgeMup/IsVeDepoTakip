using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip.Ekranlar
{
    public partial class İş_Türleri : Form
    {
        public İş_Türleri()
        {
            InitializeComponent();

            Ortak.GeçiciDepolama_PencereKonumları_Oku(this);
        
            Liste.Items.Clear();
            AramaÇubuğu_Liste = Banka.İşTürü_Listele();
            Liste.Items.AddRange(AramaÇubuğu_Liste.ToArray());

            Malzeme_Liste = Banka.Malzeme_Listele();
            Malzeme_SeçimKutusu.Items.AddRange(Malzeme_Liste.ToArray());
        }

        List<string> AramaÇubuğu_Liste = null;
        private void AramaÇubuğu_TextChanged(object sender, EventArgs e)
        {
            splitContainer1.Panel2.Enabled = false;
            Liste.Items.Clear();

            Liste.Items.AddRange(Ortak.GrupArayıcı(AramaÇubuğu_Liste, AramaÇubuğu.Text));
        }

        List<string> Malzeme_Liste = null;
        private void Malzeme_TextChanged(object sender, EventArgs e)
        {
            Malzeme_SeçiliSatıraKopyala.Enabled = false;
            Malzeme_SeçimKutusu.Items.Clear();

            Malzeme_SeçimKutusu.Items.AddRange(Ortak.GrupArayıcı(Malzeme_Liste, Malzeme_AramaÇubuğu.Text));
        }
        private void Malzeme_SeçimKutusu_SelectedValueChanged(object sender, System.EventArgs e)
        {
            Malzeme_SeçiliSatıraKopyala.Enabled = !string.IsNullOrEmpty(Malzeme_SeçimKutusu.Text);
        }
        private void Malzeme_SeçiliSatıraKopyala_Click(object sender, EventArgs e)
        {
            var l = Tablo.SelectedRows;
            if (l == null || l.Count != 1) return;

            int konum = l[0].Index;
            if (konum == Tablo.RowCount - 1) Tablo.RowCount++;

            Tablo[0, konum].Value = Malzeme_SeçimKutusu.Text;
        }

        private void Liste_SelectedValueChanged(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(Liste.Text)) { splitContainer1.Panel2.Enabled = false; return; }
            Yeni.Text = Liste.Text;

            Banka.İşTürü_Malzemeler_TablodaGöster(Tablo, Liste.Text, out string MüşteriyeGösterilecekAdı, out string Notları);
            MüşteriyeGösterilecekOlanAdı.Text = MüşteriyeGösterilecekAdı;
            Notlar.Text = Notları;

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
            if (!Liste.Enabled || string.IsNullOrEmpty(Liste.Text) || string.IsNullOrWhiteSpace(Yeni.Text)) return;

            if (Liste.Text == Yeni.Text)
            {
                MessageBox.Show("Mevcut ile yeni isimler aynı", Text);
                return;
            }

            if (Banka.İşTürü_MevcutMu(Yeni.Text))
            {
                MessageBox.Show("Kullanılmayan bir isim seçiniz", Text);
                return;
            }

            DialogResult Dr = MessageBox.Show("İsim değişikliği işlemine devam etmek istiyor musunuz?" + Environment.NewLine + Environment.NewLine +
                Liste.Text + " -> " + Yeni.Text, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (Dr == DialogResult.No) return;

            Ortak.Gösterge.Başlat("Bekleyiniz", false, Liste, System.IO.Directory.GetFiles(Ortak.Klasör_Banka, "*.mup", System.IO.SearchOption.AllDirectories).Length);
           
            Banka.İşTürü_YenidenAdlandır(Liste.Text, Yeni.Text);
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

            DialogResult Dr = MessageBox.Show(Liste.Text + " öğesini silmek istediğinize emin misiniz?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (Dr == DialogResult.No) return;

            Banka.İşTürü_Sil(Liste.Text);
            Banka.Değişiklikleri_Kaydet(Liste);

            AramaÇubuğu_Liste.Remove(Liste.Text);
            Liste.Items.RemoveAt(Liste.SelectedIndex);
        }
        private void Ekle_Click(object sender, System.EventArgs e)
        {
            if (Banka.İşTürü_MevcutMu(Yeni.Text))
            {
                MessageBox.Show("Önceden eklenmiş", Text);
                return;
            }

            Banka.İşTürü_Ekle(Yeni.Text);
            Banka.Değişiklikleri_Kaydet(Ekle);

            Liste.Items.Add(Yeni.Text);
            AramaÇubuğu_Liste.Add(Yeni.Text);
        }

        private void Tablo_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;

            Ayar_Değişti(null, null);

            //malzeme değişince birimini güncelle
            if (e.ColumnIndex == 0) Tablo[2, e.RowIndex].Value = Banka.Malzeme_Birimi((string)Tablo[0, e.RowIndex].Value);
        }
        private void Ayar_Değişti(object sender, EventArgs e)
        {
            splitContainer1.Panel1.Enabled = false;
            Kaydet.Enabled = true;
        }     
        private void Kaydet_Click(object sender, EventArgs e)
        {
            List<string> Malzemeler = new List<string>();
            List<string> Miktarlar = new List<string>();
            List<string> EklenmişMalzmeler = new List<string>();

            for (int i = 0; i < Tablo.RowCount - 1; i++)
            {
                string miktar = (string)Tablo[1, i].Value;
                if (!Ortak.YazıyıSayıyaDönüştür(ref miktar, 
                    "Tablodaki miktar sutununun " + (i + 1).ToString() + ". satırı", 
                    "İlgili satırı silmek için sıfır yazınız.", 
                    0)) return;
                Tablo[1, i].Value = miktar;
                if (miktar == "0") continue; //silmek için kullanılıyor

                if (!Banka.Malzeme_MevcutMu((string)Tablo[0, i].Value))
                {
                    MessageBox.Show("Tablodaki " + (i + 1) + ". satırdaki \"Malzeme\" uygun değil", Text);
                    return;
                }

                if (EklenmişMalzmeler.Contains((string)Tablo[0, i].Value))
                {
                    MessageBox.Show("Tablodaki " + (i + 1).ToString() + ". satırdaki " + (string)Tablo[0, i].Value + " malzemesi ikinci kez seçilmiş, lütfen fazla olanı siliniz", Text);
                    return;
                }
                else EklenmişMalzmeler.Add((string)Tablo[0, i].Value); 

                Malzemeler.Add((string)Tablo[0, i].Value);
                Miktarlar.Add((string)Tablo[1, i].Value);
            }

            Banka.İşTürü_Malzemeler_Kaydet(Liste.Text, Malzemeler, Miktarlar, MüşteriyeGösterilecekOlanAdı.Text.Trim(), Notlar.Text.Trim());
            Banka.Değişiklikleri_Kaydet(Kaydet);

            splitContainer1.Panel1.Enabled = true;
            Liste_SelectedValueChanged(null, null);
        }
    }
}
