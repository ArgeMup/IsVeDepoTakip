using ArgeMup.HazirKod;
using ArgeMup.HazirKod.DonanımHaberleşmesi;
using ArgeMup.HazirKod.Ekİşlemler;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip.Ekranlar
{
    public partial class Barkod_Okuma_Seçenekleri : Form, IGüncellenenSeriNolar
    {
        string Müşteri_, SeriNo_, Hasta_, EkTanım_;
        Banka.TabloTürü Türü_;

        public Barkod_Okuma_Seçenekleri(Banka.Talep_Bul_Detaylar_ Detaylar)
        {
            InitializeComponent();

            Başlat(Detaylar);
        }
        void Başlat(Banka.Talep_Bul_Detaylar_ Detaylar)
        {
            Banka.Talep_Ayıkla_SeriNoDalı(Detaylar.SeriNoDalı, out SeriNo_, out Hasta_, out _, out _, out string TeslimEdilmeTarihi);
            Banka.Talep_Hesaplat_FirmaİçindekiSüreler(Detaylar.SeriNoDalı, out TimeSpan Firmaİçinde, out TimeSpan Toplam);

            Text = SeriNo_ + " " + Detaylar.Tür;

            Müşteri.Text = Detaylar.Müşteri;
            Hasta.Text = Hasta_;
            Süre.Text = "Toplam " + Banka.Yazdır_Tarih_Gün(Toplam) + ", firma içinde " + Banka.Yazdır_Tarih_Gün(Firmaİçinde);

            Müşteri_ = Detaylar.Müşteri;
            EkTanım_ = Detaylar.EkTanım;
            Türü_ = Detaylar.Tür;

            İşler.Items.Clear();
            string Sonİş_ÇıkışTarihi = null;
            foreach (IDepo_Eleman İşTürüDalı in Detaylar.SeriNoDalı.Elemanları)
            {
                Banka.Talep_Ayıkla_İşTürüDalı(İşTürüDalı, out string İşTürü, out _, out Sonİş_ÇıkışTarihi, out _, out _, out byte[] Kullanım_AdetVeKonum);
                İşler.Items.Add(İşTürü + " " + Banka.Ücretler_AdetÇarpanı(Kullanım_AdetVeKonum));
            }

            if (Türü_ > Banka.TabloTürü.TeslimEdildi)
            {
                //sadece olunabilir
            }
            else
            {
                İsaretle_TeslimEdildi.Enabled = TeslimEdilmeTarihi.BoşMu();
                MüşteriyeGönder.Enabled = Türü_ == Banka.TabloTürü.DevamEden && Sonİş_ÇıkışTarihi.BoşMu();
            }
        }

        private void Düzenle_Click(object sender, EventArgs e)
        {
            Close();
            Ekranlar.ÖnYüzler.Ekle(new Yeni_İş_Girişi(SeriNo_, Müşteri_, Türü_, EkTanım_));
        }
        private void MüşteriyeGönder_Click(object sender, EventArgs e)
        {
            Banka.Talep_İşaretle_DevamEden_MüşteriyeGönderildi(Müşteri_, new List<string>() { SeriNo_ });
            Banka.Değişiklikleri_Kaydet(MüşteriyeGönder);
            Ekranlar.ÖnYüzler.GüncellenenSeriNoyuİşaretle(SeriNo_);
            Close();
        }
        private void İsaretle_TeslimEdildi_Click(object sender, EventArgs e)
        {
            Banka.Talep_İşaretle_DevamEden_TeslimEdilen(Müşteri_, new List<string>() { SeriNo_ }, true);
            Banka.Değişiklikleri_Kaydet(İsaretle_TeslimEdildi);
            Ekranlar.ÖnYüzler.GüncellenenSeriNoyuİşaretle(SeriNo_);
            Close();
        }

        void IGüncellenenSeriNolar.KontrolEt(List<string> GüncellenenSeriNolar)
        {
            if (SeriNo_.DoluMu() && GüncellenenSeriNolar.Contains(SeriNo_))
            {
                Banka.Talep_Bul_Detaylar_ detaylar = Banka.Talep_Bul(SeriNo_);
                if (detaylar == null) Close();
                else Başlat(detaylar);
            }
        }
    }

    public static class BarkodSorgulama
    {
        static IDonanımHaberlleşmesi SeriPort = null;
        public static string SonMesaj = null;
        public static void Başlat()
        {
            IDepo_Eleman Ayarlar_Bilgisayar = Banka.Ayarlar_BilgisayarVeKullanıcı("Barkod Okuyucu");
            if (Ayarlar_Bilgisayar == null || Ayarlar_Bilgisayar[0].BoşMu() || Ayarlar_Bilgisayar[0] == "Kapalı") return;

            SeriPort = new SeriPort_(Ayarlar_Bilgisayar[0], 9600, GeriBildirim_Islemi_SeriPort, SatırSatırGönderVeAl:false, TekrarDeneme_ZamanAşımı_msn:-1);
        }
        public static void Durdur()
        {
            SeriPort?.Durdur();
            SeriPort = null;
        }
        static void GeriBildirim_Islemi_SeriPort(string Kaynak, GeriBildirim_Türü_ Tür, object İçerik, object Hatırlatıcı)
        {
            if (Tür == GeriBildirim_Türü_.BilgiGeldi)
            {
                SonMesaj = ((byte[])İçerik).Yazıya().TrimEnd('\r', '\n');
                SeçenekleriGöster(SonMesaj);
            }
            else SonMesaj = Kaynak + " " + Tür.ToString();
        }

        public static void SeçenekleriGöster(string Barkod)
        {
            if (Ortak.ParolaGirilmesiGerekiyor) return;

            string sn = Ayarlar_Etiketleme.SeriNoyuBulmayaÇalış(Barkod).ToUpper();
            Banka.Talep_Bul_Detaylar_ detaylar = Banka.Talep_Bul(sn);
            if (detaylar == null) return;

            if (!Ortak.AnaEkran.InvokeRequired) Ekranlar.ÖnYüzler.Ekle(new Barkod_Okuma_Seçenekleri(detaylar));
            else
            {
                Ortak.AnaEkran.Invoke(new Action(() =>
                {
                    Ekranlar.ÖnYüzler.Ekle(new Barkod_Okuma_Seçenekleri(detaylar));
                }));
            }
        }
    }
}
