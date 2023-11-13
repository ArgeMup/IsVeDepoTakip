using ArgeMup.HazirKod;
using ArgeMup.HazirKod.Ekİşlemler;
using ArgeMup.HazirKod.Ekranlar;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip.Ekranlar
{
    public partial class Ayarlar_Malzemeler : Form
    {
        public Ayarlar_Malzemeler()
        {
            InitializeComponent();

            var ayrl = Banka.ListeKutusu_Ayarlar(false, false);
            ayrl.ElemanKonumu = ListeKutusu.Ayarlar_.ElemanKonumu_.AdanZyeSıralanmış;
            Liste_Malzemeler.Başlat(null, Banka.Malzeme_Listele(), "Malzemeler", ayrl);
            Liste_Malzemeler.GeriBildirim_İşlemi += Liste_Malzemeler_GeriBildirim_İşlemi;
        }
        private bool Liste_Malzemeler_GeriBildirim_İşlemi(string Adı, ListeKutusu.İşlemTürü Türü, string YeniAdı = null)
        {
            if (Adı.BoşMu()) return true;

            switch (Türü)
            {
                case ListeKutusu.İşlemTürü.ElemanSeçildi:
                    Banka.Malzeme_TablodaGöster(Tablo, Adı, out double Miktar, out string Birim, out double UyarıVermeMiktar, out bool Detaylı, out string Notları);
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

                    ÖnYüzler_Kaydet.Enabled = false;
                    break;

                case ListeKutusu.İşlemTürü.YeniEklendi:
                    Banka.Malzeme_Ekle(Adı);
                    Banka.Değişiklikleri_Kaydet(Liste_Malzemeler);
                    break;
                case ListeKutusu.İşlemTürü.AdıDeğiştirildi:
                case ListeKutusu.İşlemTürü.Gizlendi:
                case ListeKutusu.İşlemTürü.GörünürDurumaGetirildi:
                    Ortak.Gösterge.Başlat("Bekleyiniz", false, Liste_Malzemeler, System.IO.Directory.GetFiles(Ortak.Klasör_Banka, "*.mup", System.IO.SearchOption.AllDirectories).Length);
                    Banka.Malzeme_YenidenAdlandır(Adı, YeniAdı);
                    Banka.Değişiklikleri_Kaydet(Liste_Malzemeler);
                    Banka.Değişiklikler_TamponuSıfırla();
                    Ortak.Gösterge.Bitir();
                    break;
                case ListeKutusu.İşlemTürü.Silindi:
                    Banka.Malzeme_Sil(Adı);
                    Banka.Değişiklikleri_Kaydet(Liste_Malzemeler);
                    break;
            }

            return true;
        }

        private void Ayar_Değişti(object sender, EventArgs e)
        {
            ÖnYüzler_Kaydet.Enabled = true;
        }
        private void ÖnYüzler_Kaydet_Click(object sender, EventArgs e)
        {
            if (Liste_Malzemeler.SeçilenEleman_Adı.BoşMu()) return;

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

            Banka.Malzeme_DetaylarıKaydet(Liste_Malzemeler.SeçilenEleman_Adı, mevcut, Birimi.Text, uyarımiktarı, DetaylıKullanım.Checked, Notlar.Text.Trim());
            Banka.Değişiklikleri_Kaydet(ÖnYüzler_Kaydet);

            Liste_Malzemeler_GeriBildirim_İşlemi(Liste_Malzemeler.SeçilenEleman_Adı, ListeKutusu.İşlemTürü.ElemanSeçildi);
        }
    }
}
