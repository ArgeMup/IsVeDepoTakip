using ArgeMup.HazirKod.Ekİşlemler;
using ArgeMup.HazirKod.Ekranlar;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip.Ekranlar
{
    public partial class Ayarlar_İş_Türleri : Form
    {
        public Ayarlar_İş_Türleri()
        {
            InitializeComponent();

            ListeKutusu.Ayarlar_ ayrlr = Banka.ListeKutusu_Ayarlar(false, false);
            ayrlr.Yasakİçerik = new string[] { "{", "}" };
            Liste_işTürleri.Başlat(null, Banka.İşTürü_Listele(), "İş Türleri", ayrlr);
            Liste_işTürleri.GeriBildirim_İşlemi += Liste_işTürleri_GeriBildirim_İşlemi;

            Liste_Malzemeler.Başlat(null, Banka.Malzeme_Listele(), "Malzemeler", Banka.ListeKutusu_Ayarlar(true, false));

            Banka.İştürü_Tamamlayıcıİş_Sıfırla();
        }

        private bool Liste_işTürleri_GeriBildirim_İşlemi(string Adı, ListeKutusu.İşlemTürü Türü, string YeniAdı = null)
        {
            if (Adı.BoşMu()) return false;

            switch (Türü)
            {
                case ListeKutusu.İşlemTürü.ElemanSeçildi:
                    Banka.İşTürü_Malzemeler_TablodaGöster(Tablo, Adı, out string MüşteriyeGösterilecekAdı, out string Notları, out bool Tamamlayıcıİştir);
                    MüşteriyeGösterilecekOlanAdı.Text = MüşteriyeGösterilecekAdı;
                    Notlar.Text = Notları;
                    Tamamlayıcıİş.Checked = Tamamlayıcıİştir;

                    ÖnYüzler_Kaydet.Enabled = false;
                    break;

                case ListeKutusu.İşlemTürü.YeniEklendi:
                    Banka.İşTürü_Ekle(Adı);
                    Banka.Değişiklikleri_Kaydet(Liste_işTürleri);
                    break;
                case ListeKutusu.İşlemTürü.AdıDeğiştirildi:
                case ListeKutusu.İşlemTürü.Gizlendi:
                case ListeKutusu.İşlemTürü.GörünürDurumaGetirildi:
                    Ortak.Gösterge.Başlat("Bekleyiniz", false, Liste_işTürleri, System.IO.Directory.GetFiles(Ortak.Klasör_Banka, "*.mup", System.IO.SearchOption.AllDirectories).Length);
                    Banka.İşTürü_YenidenAdlandır(Adı, YeniAdı);
                    Banka.Değişiklikleri_Kaydet(Liste_işTürleri);
                    Banka.Değişiklikler_TamponuSıfırla();
                    Ortak.Gösterge.Bitir();
                    Banka.İştürü_Tamamlayıcıİş_Sıfırla();
                    break;
                case ListeKutusu.İşlemTürü.KonumDeğişikliğiKaydedildi:
                    Banka.İşTürü_Sırala(Liste_işTürleri.Tüm_Elemanlar);
                    Banka.Değişiklikleri_Kaydet(Liste_işTürleri);
                    break;
                case ListeKutusu.İşlemTürü.Silindi:
                    Banka.İşTürü_Sil(Liste_işTürleri.SeçilenEleman_Adı);
                    Banka.Değişiklikleri_Kaydet(Liste_işTürleri);
                    Banka.İştürü_Tamamlayıcıİş_Sıfırla();
                    break;
            }

            return true;
        }
        private void Liste_Malzemeler_DoubleClick(object sender, EventArgs e)
        {
            Malzeme_SeçiliSatıraKopyala_Click(null, null);
        }
        private void Malzeme_SeçiliSatıraKopyala_Click(object sender, EventArgs e)
        {
            if (Liste_Malzemeler.SeçilenEleman_Adı.BoşMu()) return;

            var l = Tablo.SelectedRows;
            if (l == null || l.Count != 1) return;

            int konum = l[0].Index;
            if (konum == Tablo.RowCount - 1) Tablo.RowCount++;

            Tablo[0, konum].Value = Liste_Malzemeler.SeçilenEleman_Adı;
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
            ÖnYüzler_Kaydet.Enabled = true;
        }     
        private void ÖnYüzler_Kaydet_Click(object sender, EventArgs e)
        {
            if (Liste_işTürleri.SeçilenEleman_Adı.BoşMu()) return;

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

            Banka.İşTürü_Malzemeler_Kaydet(Liste_işTürleri.SeçilenEleman_Adı, Malzemeler, Miktarlar, MüşteriyeGösterilecekOlanAdı.Text.Trim(), Notlar.Text.Trim(), Tamamlayıcıİş.Checked);
            Banka.Değişiklikleri_Kaydet(ÖnYüzler_Kaydet);
            Banka.İştürü_Tamamlayıcıİş_Sıfırla();

            Liste_işTürleri_GeriBildirim_İşlemi(Liste_işTürleri.SeçilenEleman_Adı, ListeKutusu.İşlemTürü.ElemanSeçildi);
        }
    }
}
