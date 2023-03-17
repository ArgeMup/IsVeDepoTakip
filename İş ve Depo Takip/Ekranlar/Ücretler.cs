using ArgeMup.HazirKod;
using ArgeMup.HazirKod.Ekİşlemler;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip.Ekranlar
{
    public partial class Ücretler : Form
    {
        public Ücretler()
        {
            InitializeComponent();

            Ortak.GeçiciDepolama_PencereKonumları_Oku(this);

            Ortak.YeniSayfaAçmaTalebi = null; //tüm işler içinden çağırıldığından dolayı
        
            AramaÇubuğu_Müşteri_Liste = Banka.Müşteri_Listele();
            AramaÇubuğu_Müşteri_Liste.Insert(0, "Tüm müşteriler için ortak");
            Müşterıler.Items.AddRange(AramaÇubuğu_Müşteri_Liste.ToArray());

            Tablo.Rows.Clear();
            AramaÇubuğu_İşTürü_Liste = Banka.İşTürü_Listele();
            foreach (string it in AramaÇubuğu_İşTürü_Liste)
            {
                int y = Tablo.RowCount;
                Tablo.RowCount++;

                Tablo[0, y].Value = it;
            }

            Zam_Miktarı.Text = "0";
            KDV.Text = Banka.Ayarlar_Genel("Bütçe/KDV", true).Oku_Sayı(null, 8).Yazıya();

            Müşterıler.Text = "Tüm müşteriler için ortak";
            Kaydet.Enabled = false;
            splitContainer1.Panel1.Enabled = true;
            Müşterıler.Focus();
        }

        List<string> AramaÇubuğu_Müşteri_Liste = null;
        private void AramaÇubuğu_Müşteri_TextChanged(object sender, EventArgs e)
        {
            splitContainer1.Panel2.Enabled = false;
            Zam_Yap.Enabled = false;

            Ortak.GrupArayıcı(Müşterıler, AramaÇubuğu_Müşteri_Liste, AramaÇubuğu_Müşteri.Text);
        }

        List<string> AramaÇubuğu_İşTürü_Liste = null;
        private void AramaÇubuğu_İşTürü_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(AramaÇubuğu_İşTürü.Text))
            {
                for (int i = 0; i < Tablo.RowCount; i++)
                {
                    Tablo.Rows[i].Visible = true;
                }
            }
            else
            {
                string[] arananlar = AramaÇubuğu_İşTürü.Text.ToLower().Split(' ');
                for (int i = 0; i < Tablo.RowCount; i++)
                {
                    string içerik = ((string)Tablo[0, i].Value).ToLower();
                    int bulundu_adet = 0;
                    foreach (string arn in arananlar)
                    {
                        if (!içerik.Contains(arn)) break;

                        bulundu_adet++;
                    }

                    Tablo.Rows[i].Visible = bulundu_adet == arananlar.Length;
                }
            }
        }

        private void Müşterıler_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Müşterıler.Text == "Tüm müşteriler için ortak")
            {
                Banka.Ücretler_TablodaGöster(Tablo, null);
            }
            else Banka.Ücretler_TablodaGöster(Tablo, Müşterıler.Text);

            splitContainer1.Panel1.Enabled = true;
            splitContainer1.Panel2.Enabled = true;
            Zam_Yap.Enabled = true;
            Kaydet.Enabled = false;
        }
        private void Tablo_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            Ayar_Değişti(null, null);
        }
        private void Ayar_Değişti(object sender, EventArgs e)
        {
            splitContainer1.Panel1.Enabled = false;
            Kaydet.Enabled = true;
        }

        private void Zam_Yap_Click(object sender, EventArgs e)
        {
            string yüzde_y = Zam_Miktarı.Text;
            if (!Ortak.YazıyıSayıyaDönüştür(ref yüzde_y, "Artış miktarı kutucuğu"))
            {
                Zam_Miktarı.Focus();
                return;
            }
            Zam_Miktarı.Text = yüzde_y;
            double yüzde_s = yüzde_y.NoktalıSayıya();

            if (!string.IsNullOrWhiteSpace(AramaÇubuğu_İşTürü.Text))
            {
                string soru = "Tablo içeriği filtrelendiğinden sadece GÖRÜNEN üyeler ücretlendiririlecek." + Environment.NewLine +
                    "Devam etmek için Evet tuşuna basınız";

                DialogResult Dr = MessageBox.Show(soru, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (Dr == DialogResult.No) return;
            }

            for (int i = 0; i < Tablo.RowCount; i++)
            {
                if (Tablo[1, i].Visible && Tablo[1, i].Value != null)
                {
                    try
                    {
                        double s = ((string)Tablo[1, i].Value).NoktalıSayıya();
                        s = (s / 100.0 * yüzde_s) + s;
                        Tablo[1, i].Value = s.Yazıya();
                    }
                    catch (Exception) { }
                }
            }
        }

        private void Kaydet_Click(object sender, EventArgs e)
        {
            string kdv = KDV.Text;
            if (!Ortak.YazıyıSayıyaDönüştür(ref kdv, "KDV kutucuğu", null, 0, 100))
            {
                KDV.Focus();
                return;
            }
            KDV.Text = kdv;

            bool TümMüşterilerİçinOrtak = Müşterıler.Text == "Tüm müşteriler için ortak";

            for (int i = 0; i < Tablo.RowCount; i++)
            {
                if (!string.IsNullOrEmpty((string)Tablo[1, i].Value))
                {
                    string miktar = (string)Tablo[1, i].Value;
                    if (!Ortak.YazıyıSayıyaDönüştür(ref miktar,
                        (string)Tablo[0, i].Value + " (ücret sutununun " + (i + 1).ToString() + ". satırı)",
                        "Ücretlendirmek istemiyorsanız boş olarak bırakınız." + Environment.NewLine +
                        "Eğer " + (i + 1) + ". satır görünmüyor ise arama filtresini kaldırınız",
                        0)) return;

                    Tablo[1, i].Value = miktar;
                }

                if (TümMüşterilerİçinOrtak && !string.IsNullOrEmpty((string)Tablo[2, i].Value))
                {
                    string miktar = (string)Tablo[2, i].Value;
                    if (!Ortak.YazıyıSayıyaDönüştür(ref miktar,
                        (string)Tablo[0, i].Value + " (maliyet sutununun " + (i + 1).ToString() + ". satırı)",
                        "Ücretlendirmek istemiyorsanız boş olarak bırakınız." + Environment.NewLine +
                        "Eğer " + (i + 1) + ". satır görünmüyor ise arama filtresini kaldırınız",
                        0)) return;

                    Tablo[2, i].Value = miktar;
                }
            }

            DialogResult Dr = MessageBox.Show("Değişiklikleri kaydetmek istediğinize emin misiniz?" +
                (string.IsNullOrWhiteSpace(AramaÇubuğu_İşTürü.Text) ? null : Environment.NewLine + Environment.NewLine + "Tablo içeriği filtrelendiğinden görünmeseler bile TÜM girdiler kaydedilecektir."),
                Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (Dr == DialogResult.No) return;

            Banka.Ayarlar_Genel("Bütçe/KDV", true).Yaz(null, kdv);
            if (TümMüşterilerİçinOrtak)
            {
                Banka.Ücretler_TablodakileriKaydet(Tablo, null);
            }
            else Banka.Ücretler_TablodakileriKaydet(Tablo, Müşterıler.Text);
            Banka.Değişiklikleri_Kaydet(Kaydet);

            Kaydet.Enabled = false;
            splitContainer1.Panel1.Enabled = true;
        }  
    }
}
