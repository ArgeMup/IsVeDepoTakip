using ArgeMup.HazirKod;
using ArgeMup.HazirKod.Ekİşlemler;
using ArgeMup.HazirKod.Ekranlar;
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

            Liste_Müşteriler.Başlat(null, Banka.Müşteri_Listele(true), "Müşteriler", Banka.ListeKutusu_Ayarlar(false, false));
            Liste_Müşteriler.GeriBildirim_İşlemi += Liste_Müşteriler_GeriBildirim_İşlemi;
        }
        private bool Liste_Müşteriler_GeriBildirim_İşlemi(string Adı, ArgeMup.HazirKod.Ekranlar.ListeKutusu.İşlemTürü Türü, string YeniAdı = null)
        {
            if (Adı.BoşMu()) return true;

            switch (Türü)
            {
                case ListeKutusu.İşlemTürü.ElemanSeçildi:
                    IDepo_Eleman m = Banka.Ayarlar_Müşteri(Adı, "Eposta", true);
                    Eposta_Kime.Text = m.Oku("Kime");
                    Eposta_Bilgi.Text = m.Oku("Bilgi");
                    Eposta_Gizli.Text = m.Oku("Gizli");
                    Notlar.Text = Banka.Ayarlar_Müşteri(Adı, "Notlar", true)[0];

                    ÖnYüzler_Kaydet.Enabled = false;
                    break;

                case ListeKutusu.İşlemTürü.YeniEklendi:
                    Banka.Müşteri_Ekle(Adı);
                    Banka.Değişiklikleri_Kaydet(Liste_Müşteriler);
                    break;
                case ListeKutusu.İşlemTürü.AdıDeğiştirildi:
                case ListeKutusu.İşlemTürü.Gizlendi:
                case ListeKutusu.İşlemTürü.GörünürDurumaGetirildi:
                    Banka.Müşteri_YenidenAdlandır(Adı, YeniAdı);
                    Banka.Değişiklikleri_Kaydet(Liste_Müşteriler);
                    Banka.Değişiklikler_TamponuSıfırla();
                    break;
                case ListeKutusu.İşlemTürü.KonumDeğişikliğiKaydedildi:
                    Banka.Müşteri_Sırala(Liste_Müşteriler.Tüm_Elemanlar);
                    Banka.Değişiklikleri_Kaydet(Liste_Müşteriler);
                    break;
                case ListeKutusu.İşlemTürü.Silindi:
                    string soru = Adı + " öğesi tüm kayıtlarıyla birlikte KALICI olarak SİLİNECEK." + Environment.NewLine + Environment.NewLine +
                        "İşleme devam etmek istiyor musunuz?";
                    DialogResult Dr = MessageBox.Show(soru, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (Dr == DialogResult.No) return false;

                    Banka.Müşteri_Sil(Adı);
                    Banka.Değişiklikleri_Kaydet(Liste_Müşteriler);
                    Banka.Değişiklikler_TamponuSıfırla();
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
            if (Liste_Müşteriler.SeçilenEleman_Adı.BoşMu()) return;

            Eposta_Kime.Text = Eposta_Kime.Text.Trim().ToLower();
            Eposta_Bilgi.Text = Eposta_Bilgi.Text.Trim().ToLower();
            Eposta_Gizli.Text = Eposta_Gizli.Text.Trim().ToLower();

            IDepo_Eleman m = Banka.Ayarlar_Müşteri(Liste_Müşteriler.SeçilenEleman_Adı, "Eposta", true);
            m.Yaz("Kime", Eposta_Kime.Text);
            m.Yaz("Bilgi", Eposta_Bilgi.Text);
            m.Yaz("Gizli", Eposta_Gizli.Text);
            Banka.Ayarlar_Müşteri(Liste_Müşteriler.SeçilenEleman_Adı, "Notlar", true)[0] = Notlar.Text.Trim();
            Banka.Değişiklikleri_Kaydet(ÖnYüzler_Kaydet);

            Liste_Müşteriler_GeriBildirim_İşlemi(Liste_Müşteriler.SeçilenEleman_Adı, ListeKutusu.İşlemTürü.ElemanSeçildi);

            Ekranlar.Eposta.Durdur();
        }
    }
}