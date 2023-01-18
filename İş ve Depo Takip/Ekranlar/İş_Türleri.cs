using ArgeMup.HazirKod.Ekİşlemler;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip
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
            if (Liste.Items.Count > 0) Sil.Enabled = true;

            Malzeme_Liste = Banka.Malzeme_Listele();
            string[] m_l = Malzeme_Liste.ToArray();
            Malzeme_SeçimKutusu.Items.AddRange(m_l);
            Tablo_Malzeme.Items.AddRange(m_l);
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

        List<string> Malzeme_Liste = null;
        private void Malzeme_TextChanged(object sender, EventArgs e)
        {
            Malzeme_SeçiliSatıraKopyala.Enabled = false;
            Malzeme_SeçimKutusu.Items.Clear();

            if (string.IsNullOrEmpty(Malzeme_AramaÇubuğu.Text))
            {
                Malzeme_SeçimKutusu.Items.AddRange(Malzeme_Liste.ToArray());
            }
            else
            {
                Malzeme_AramaÇubuğu.Text = Malzeme_AramaÇubuğu.Text.ToLower();
                Malzeme_SeçimKutusu.Items.AddRange(Malzeme_Liste.FindAll(x => x.ToLower().Contains(Malzeme_AramaÇubuğu.Text)).ToArray());
            }
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
            Sil.Enabled = !string.IsNullOrEmpty(Liste.Text);

            if (!Sil.Enabled) { splitContainer1.Panel2.Enabled = false; return; }
            Yeni.Text = Liste.Text;

            Banka.İşTürü_Malzemeler_TablodaGöster(Tablo, Liste.Text, out string Notları);
            Notlar.Text = Notları;

            splitContainer1.Panel1.Enabled = true;
            splitContainer1.Panel2.Enabled = true;
            Kaydet.Enabled = false;
        }
        private void Sil_Click(object sender, System.EventArgs e)
        {
            if (Liste.SelectedIndex < 0 || Liste.SelectedIndex >= Liste.Items.Count) return;

            DialogResult Dr = MessageBox.Show(Liste .Text + " öğesini silmek istediğinize emin misiniz?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (Dr == DialogResult.No) return;

            Banka.İşTürü_Sil(Liste.Text);
            Banka.Değişiklikleri_Kaydet(Sil);

            AramaÇubuğu_Liste.Remove(Liste.Text);
            Liste.Items.RemoveAt(Liste.SelectedIndex);
        }
        private void Yeni_TextChanged(object sender, System.EventArgs e)
        {
            Ekle.Enabled = !string.IsNullOrWhiteSpace(Yeni.Text);
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

            if (Malzemeler.Count < 1)
            {
                MessageBox.Show("Tabloda hiç geçerli girdi bulunamadı", Text);
                return;
            }

            Banka.İşTürü_Malzemeler_Kaydet(Liste.Text, Malzemeler, Miktarlar, Notlar.Text.Trim());
            Banka.Değişiklikleri_Kaydet(Kaydet);

            splitContainer1.Panel1.Enabled = true;
            Liste_SelectedValueChanged(null, null);
        }
    }
}
