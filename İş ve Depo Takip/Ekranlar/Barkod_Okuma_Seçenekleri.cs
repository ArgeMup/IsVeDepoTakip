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
        string SeriNo_, Hasta_;
        Banka.Talep_Bul_Detaylar_ Detaylar;

        public Barkod_Okuma_Seçenekleri(Banka.Talep_Bul_Detaylar_ Detaylar)
        {
            InitializeComponent();

            Başlat(Detaylar);
        }
        void Başlat(Banka.Talep_Bul_Detaylar_ Detaylar)
        {
            this.Detaylar = Detaylar;
            Banka.Talep_Ayıkla_SeriNoDalı(Detaylar.SeriNoDalı, out SeriNo_, out Hasta_, out _, out _, out string TeslimEdilmeTarihi, out _);
            Banka.Talep_Hesaplat_FirmaİçindekiSüreler(Detaylar.SeriNoDalı, out TimeSpan Firmaİçinde, out TimeSpan Toplam);

            Text = SeriNo_ + " " + Detaylar.Tür;

            Müşteri.Text = Detaylar.Müşteri;
            Hasta.Text = Hasta_;
            Süre.Text = "Toplam " + Banka.Yazdır_Tarih_Gün(Toplam) + ", firma içinde " + Banka.Yazdır_Tarih_Gün(Firmaİçinde);
          
            İşler.Items.Clear();
            string Sonİş_ÇıkışTarihi = null;
            foreach (IDepo_Eleman İşTürüDalı in Detaylar.SeriNoDalı.Elemanları)
            {
                Banka.Talep_Ayıkla_İşTürüDalı(İşTürüDalı, out string İşTürü, out _, out Sonİş_ÇıkışTarihi, out _, out _, out byte[] Kullanım_AdetVeKonum);
                İşler.Items.Add(İşTürü + " " + Banka.Ücretler_AdetÇarpanı(Kullanım_AdetVeKonum));
            }

            if (Detaylar.Tür > Banka.TabloTürü.TeslimEdildi)
            {
                //sadece olunabilir
            }
            else
            {
                bool Kıstas_teslimedildi = TeslimEdilmeTarihi.BoşMu();
                bool Kıstas_müşteriyegönder = Detaylar.Tür == Banka.TabloTürü.DevamEden && Sonİş_ÇıkışTarihi.BoşMu();
                İsaretle_TeslimEdildi.Enabled = Kıstas_teslimedildi;
                MüşteriyeGönder.Enabled = Kıstas_müşteriyegönder;
            }
        }

        private void Düzenle_Click(object sender, EventArgs e)
        {
            Close();
            Ekranlar.ÖnYüzler.Ekle(new Yeni_İş_Girişi(SeriNo_, Detaylar.Müşteri, Detaylar.Tür, Detaylar.EkTanım));
        }
        private void MüşteriyeGönder_Click(object sender, EventArgs e)
        {
            Banka.Talep_İşaretle_DevamEden_MüşteriyeGönderildi(Detaylar.Müşteri, new List<string>() { SeriNo_ });
            Banka.Değişiklikleri_Kaydet(MüşteriyeGönder);
            Ekranlar.ÖnYüzler.GüncellenenSeriNoyuİşaretle(SeriNo_);
            Close();
        }
        private void İsaretle_TeslimEdildi_Click(object sender, EventArgs e)
        {
            Banka.Talep_İşaretle_DevamEden_TeslimEdilen(Detaylar.Müşteri, new List<string>() { SeriNo_ }, true);
            Banka.Değişiklikleri_Kaydet(İsaretle_TeslimEdildi);
            Ekranlar.ÖnYüzler.GüncellenenSeriNoyuİşaretle(SeriNo_);
            Close();
        }
        
        private void AçıklamaEtiketi_Click(object sender, EventArgs e)
        {
            Yeni_İş_Girişi_Açıklama açklm = new Yeni_İş_Girişi_Açıklama();
            açklm.FormClosed += Açklm_FormClosed;
            ÖnYüzler.Ekle(açklm);

            void Açklm_FormClosed(object _sender_, FormClosedEventArgs _e_)
            {
                string Açıklama = açklm.YazdırVeKaydet.Tag as string;
                if (Açıklama.BoşMu(true)) return;

                if (Detaylar.Tür <= Banka.TabloTürü.TeslimEdildi)
                {
                    //sadece okunabilir değil
                    string Notlar = Detaylar.SeriNoDalı[2];
                    if (Notlar.DoluMu(true)) Notlar += Environment.NewLine;
                    Notlar += DateTime.Now.ToString("dd MMM ddd") + " " + Açıklama;
                   
                    Detaylar.SeriNoDalı[2] = Notlar;
                    Banka.Değişiklikleri_Kaydet(AçıklamaEtiketi);
                    Ekranlar.ÖnYüzler.GüncellenenSeriNoyuİşaretle(SeriNo_);
                }

                Banka.Talep_Ayıkla_İşTürüDalı(Detaylar.SeriNoDalı.Elemanları[Detaylar.SeriNoDalı.Elemanları.Length - 1], out string SonİşTürü_, out string SonGirişTarihi_, out _, out _, out _, out _);
                string sonuç = Ayarlar_Etiketleme.YeniİşGirişi_Etiket_Üret(Ayarlar_Etiketleme.YeniİşGirişi_Etiketi.Açıklama, 1, Detaylar.Müşteri, Hasta_, SeriNo_, SonGirişTarihi_, SonİşTürü_, false, Açıklama, null, null);
                if (sonuç.DoluMu()) MessageBox.Show("Açıklama etiketinin yazdırılması aşamasında bir sorun ile karşılaşıldı" + Environment.NewLine + Environment.NewLine + sonuç, "Açıklama Etiketi");
            }
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
        static IDonanımHaberleşmesi SeriPort = null;
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
            Form AnaEkran = Ekranlar.ÖnYüzler.AnaEkran;
            bool İzinliMi = Banka.K_lar.İzinliMi(new Banka.K_lar.İzin[] { Banka.K_lar.İzin.Devam_eden_işler_içinde_işlem_yapabilir, Banka.K_lar.İzin.Tamamlanmış_işler_içinde_işlem_yapabilir });
            if (AnaEkran == null || !İzinliMi) return;

            string sn = Ayarlar_Etiketleme.SeriNoyuBulmayaÇalış(Barkod).ToUpper();
            Banka.Talep_Bul_Detaylar_ detaylar = Banka.Talep_Bul(sn);
            if (detaylar == null) return;

            if (!AnaEkran.InvokeRequired) Ekranlar.ÖnYüzler.Ekle(new Barkod_Okuma_Seçenekleri(detaylar));
            else
            {
                AnaEkran.Invoke(new Action(() =>
                {
                    Ekranlar.ÖnYüzler.Ekle(new Barkod_Okuma_Seçenekleri(detaylar));
                }));
            }
        }
    }
}
