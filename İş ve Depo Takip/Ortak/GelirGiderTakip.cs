using ArgeMup.HazirKod.Ekİşlemler;
using ArgeMup.HazirKod;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip.Ekranlar
{
    public static class GelirGiderTakip
    {
        public enum İşyeri_Ödeme_İşlem_Tipi_ { Boşta, Gider, MaaşÖdemesi, AvansVerilmesi, Gelir, AvansÖdemesi, KontrolNoktası } //Gelir vaya gider olarak sınıflandırılabilen alt sınıflar
        public enum İşyeri_Ödeme_İşlem_Durum_ { Boşta, Ödenmedi, KısmenÖdendi, TamÖdendi, İptalEdildi, KısmiÖdemeYapıldı, PeşinatÖdendi }
        public enum İşyeri_Ödeme_ParaBirimi_ { Boşta, TürkLirası, Avro, Dolar, ElemanSayısı = 4 };
        public enum Muhatap_Üyelik_Dönem_ { Boşta, Günlük, Haftalık, Aylık, Yıllık };
        public enum Şube_Talep_Komut_ { Boşta, Sayfa_GelirGiderEkle, Sayfa_CariDöküm, Sayfa_Ayarlar, Ekle_GelirGider, Yazdır, Kontrol, İşyeriParolasınıDeğiştir };
        public class Şube_Talep_Ekle_GelirGider_
        {
            [Değişken_.Niteliği.Adını_Değiştir("T", 0)] public string Ekle_MuhatapGrubuAdı;
            [Değişken_.Niteliği.Adını_Değiştir("T", 1)] public string Ekle_MuhatapAdı;
            [Değişken_.Niteliği.Adını_Değiştir("T", 2)] public İşyeri_Ödeme_İşlem_Tipi_ Ekle_Tipi;
            [Değişken_.Niteliği.Adını_Değiştir("T", 3)] public İşyeri_Ödeme_İşlem_Durum_ Ekle_Durumu;
            [Değişken_.Niteliği.Adını_Değiştir("T", 4)] public double Ekle_Miktar;
            [Değişken_.Niteliği.Adını_Değiştir("T", 5)] public İşyeri_Ödeme_ParaBirimi_ Ekle_ParaBirimi;
            [Değişken_.Niteliği.Adını_Değiştir("T", 6)] public DateTime Ekle_İlkÖdemeTarihi;
            [Değişken_.Niteliği.Adını_Değiştir("T", 7)] public string Ekle_Notlar;
            [Değişken_.Niteliği.Adını_Değiştir("T", 8)] public int Ekle_Taksit_Sayısı;
            [Değişken_.Niteliği.Adını_Değiştir("T", 9)] public Muhatap_Üyelik_Dönem_ Ekle_Taksit_Dönem;
            [Değişken_.Niteliği.Adını_Değiştir("T", 10)] public int Ekle_Taksit_Dönem_Adet;
            [Değişken_.Niteliği.Adını_Değiştir("T", 11)] public DateTime? Ekle_KayıtTarihi = null;
        }
        public class Şube_Talep_
        {
            [Değişken_.Niteliği.Adını_Değiştir("Bt")] public string Benzersiz_Tanımlayıcı;
            [Değişken_.Niteliği.Adını_Değiştir("İy", 0)] public string İşyeri_Adı;
            [Değişken_.Niteliği.Adını_Değiştir("İy", 1)] public string İşyeri_Parolası;
            [Değişken_.Niteliği.Adını_Değiştir("İy", 2)] public string İşyeri_LogoDosyaYolu;
            [Değişken_.Niteliği.Adını_Değiştir("Kk")] public string KayıtKlasörü;
            [Değişken_.Niteliği.Adını_Değiştir("Mu")] public Dictionary<string, List<string>> SabitMuhataplar; // Grup ve elemanları, adı Çalışan olan grup uygulamada çalışanlar olarak değerlendirilir

            [Değişken_.Niteliği.Adını_Değiştir("KuR")] public bool[] Kullanıcı_Rolİzinleri;
            [Değişken_.Niteliği.Adını_Değiştir("Ku", 0)] public string Kullanıcı_Adı;
            [Değişken_.Niteliği.Adını_Değiştir("Ku", 1)] public Şube_Talep_Komut_ Kullanıcı_Komut;
            [Değişken_.Niteliği.Adını_Değiştir("KuEt")] public string[] Kullanıcı_Komut_EkTanım;    //Yazdır                    : pdf dosya yolu + Şablon adı + Ek açıklama
                                                                                                    //Sayfa_GelirGiderEkle      : Gelir veya Gider veya Boş
                                                                                                    //İşyeriParolasınıDeğiştir  : Mevcut_Parola + Yeni_Parola + Mevcut_Parola + Yeni_Parola
                                                                                                    //                              Parola kullanılmayacak ise içeriği _YOK_ olmalı,
                                                                                                    //                              yada ArgeMup.HazirKod.Dönüştürme.D_HexYazı.BaytDizisinden( Rastgele.BaytDizisi(32) ) ile üretilmeli

            [Değişken_.Niteliği.Adını_Değiştir("E GeGi")] public List<Şube_Talep_Ekle_GelirGider_> Ekle_GelirGider_Talepler;

            public Şube_Talep_()
            {
                İşyeri_Adı = Banka.İşyeri_Adı;
                İşyeri_Parolası = Banka.Ayarlar_Genel("Gelir Gider Takip", true).Oku(null, "_YOK_");
                if (İşyeri_Parolası.BoşMu(true)) throw new Exception("if (İlkAçılışAyarları.İşyeri_Parolası.BoşMu(true))");

                İşyeri_LogoDosyaYolu = Ortak.Firma_Logo_DosyaYolu;
                KayıtKlasörü = Ortak.Klasör_KullanıcıDosyaları_GelirGiderTakip;

                SabitMuhataplar = new Dictionary<string, List<string>>
                {
                    { "Müşteri", Banka.Müşteri_Listele() },
                    { "Çalışan", Banka.K_lar.KullancıAdları(true, false) }
                };

                Kullanıcı_Rolİzinleri = new bool[(int)Banka.K_lar.İzin.DiziElemanSayısı_Gelir_gider_ + 1 /*boşta*/];
            }
        }
        class Şube_Talep_Cevap_
        {
            [Değişken_.Niteliği.Adını_Değiştir("C", 0)] public string Benzersiz_Tanımlayıcı;
            [Değişken_.Niteliği.Adını_Değiştir("C", 1)] public bool Başarılı;

            [Değişken_.Niteliği.Adını_Değiştir("D")] public string[] Detaylar;
        }
        static YanUygulama.Şebeke_ Şebeke;
        static Şube_Talep_Cevap_ Cevap;
        static Şube_Talep_ Şube_Talep = null;

        public static string Komut_SayfaAç(Şube_Talep_Komut_ Komut, bool Sayfa_GelirGiderEkle_GelirOlarakAçılsın = false)
        {
            if (Komut < Şube_Talep_Komut_.Sayfa_GelirGiderEkle || Komut > Şube_Talep_Komut_.Sayfa_Ayarlar) return "Hatalı Komut " + Komut;

            if (Şube_Talep == null) Şube_Talep = new Şube_Talep_();

            Şube_Talep.Kullanıcı_Komut = Komut;
            Şube_Talep.Kullanıcı_Komut_EkTanım = Sayfa_GelirGiderEkle_GelirOlarakAçılsın ? new string[] { "Gelir" } : null;

            return Çalıştır(true, out _);
        }
        public static Şube_Talep_Ekle_GelirGider_ Komut_Ekle_GelirGider(string MuhatapGrubuAdı, string MuhatapAdı,
                    İşyeri_Ödeme_İşlem_Tipi_ Tipi, İşyeri_Ödeme_İşlem_Durum_ Durumu, double Miktar, İşyeri_Ödeme_ParaBirimi_ ParaBirimi,
                    DateTime İlkÖdemeTarihi, string Notlar,
                    int Taksit_Sayısı, Muhatap_Üyelik_Dönem_ Taksit_Dönem, int Taksit_Dönem_Adet,
                    DateTime? KayıtTarihi = null)
        {
            return new Şube_Talep_Ekle_GelirGider_()
            {
                Ekle_MuhatapGrubuAdı = MuhatapGrubuAdı, Ekle_MuhatapAdı = MuhatapAdı,
                Ekle_Tipi = Tipi, Ekle_Durumu = Durumu, Ekle_Miktar = Miktar, Ekle_ParaBirimi = ParaBirimi,
                Ekle_İlkÖdemeTarihi = İlkÖdemeTarihi, Ekle_Notlar = Notlar,
                Ekle_Taksit_Sayısı = Taksit_Sayısı, Ekle_Taksit_Dönem = Taksit_Dönem, Ekle_Taksit_Dönem_Adet = Taksit_Dönem_Adet,
                Ekle_KayıtTarihi = KayıtTarihi
            };
        }
        public static string Komut_Ekle_GelirGider(List<Şube_Talep_Ekle_GelirGider_> Talepler)
        {
            if (Talepler == null || Talepler.Count == 0) return "Hiç ödeme bulunamadı";

            if (Şube_Talep == null) Şube_Talep = new Şube_Talep_();

            Şube_Talep.Kullanıcı_Komut = Şube_Talep_Komut_.Ekle_GelirGider;
            Şube_Talep.Ekle_GelirGider_Talepler = Talepler;

            return Çalıştır(false, out _);
        }
        public static string Komut_Yazdır(string PdfDosyaYolu, string Şablon)
        {
            string EkAçıklama = Banka.Müşteriler_Ayıkla_GelirGider(out double Gelir_DevamEden, out double Gider_DevamEden, out double Gelir_TeslimEdildi, out double Gider_TeslimEdildi, out double Gelir_ÖdemeTalepEdildi, out double Gider_ÖdemeTalepEdildi);
            EkAçıklama = 
                "Devam eden : " + Banka.Yazdır_Ücret(Gelir_DevamEden - Gider_DevamEden) +
                ", teslim edilen : " + Banka.Yazdır_Ücret(Gelir_TeslimEdildi - Gider_TeslimEdildi) +
                ", ödeme talep edilen : " + Banka.Yazdır_Ücret(Gelir_ÖdemeTalepEdildi - Gider_ÖdemeTalepEdildi) +
                (EkAçıklama.DoluMu() ? ", ayrıca " + (EkAçıklama.Split('\n').Length - 1) + " hata bulundu, iş ve depo takip içindeki bütçe sayfasını inceleyiniz" : null);

            if (Şube_Talep == null) Şube_Talep = new Şube_Talep_();

            Şube_Talep.Kullanıcı_Komut = Şube_Talep_Komut_.Yazdır;
            Şube_Talep.Kullanıcı_Komut_EkTanım = new string[] { PdfDosyaYolu, Şablon, EkAçıklama };

            return Çalıştır(false, out _);
        }
        public static string Komut_Kontrol(bool PencereleriKapat, bool Yedekle, bool Durdur, out string[] Detaylar)
        {
            if (Şube_Talep == null) { Detaylar = null; return null; }

            Şube_Talep.Kullanıcı_Komut = Şube_Talep_Komut_.Kontrol;
            Şube_Talep.Kullanıcı_Komut_EkTanım = new string[] { PencereleriKapat ? "1" : "0", Yedekle ? "1" : "0", Durdur ? "1" : "0" };

            return Çalıştır(false, out Detaylar);
        }
        public static string Komut_ParolayıDeğiştir(string Mevcut_Parola, string Yeni_Parola)
        {
        	Durdur();
            Şube_Talep = new Şube_Talep_();

            Şube_Talep.Kullanıcı_Komut = Şube_Talep_Komut_.İşyeriParolasınıDeğiştir;
            Şube_Talep.Kullanıcı_Komut_EkTanım = new string[] { Mevcut_Parola, Yeni_Parola, Mevcut_Parola, Yeni_Parola };

            string cevap = Çalıştır(false, out _);
            Durdur();
            return cevap;
        }
        static string Çalıştır(bool HatayıGöster, out string[] Detaylar)
        {
            Detaylar = null;
            int ZamanAşımıAnı = Environment.TickCount + 15000;
            
            if (Şebeke == null)
            {
                Ortak.Gösterge.Başlat("Gelir Gider Takip ile ilk bağlantı kuruluyor", true, null, 500);

                string EnDüşükSürüm = "0.14";
                string DosyaYolu = Klasör.Depolama(Klasör.Kapsamı.Geçici, null, "Gelir_Gider_Takip", "") + "\\Gelir Gider Takip.exe";
                string AğAdresi_Uygulama = "https://github.com/ArgeMup/GelirGiderTakip/raw/main/bin/Yay%C4%B1nla/Gelir%20Gider%20Takip.exe";
                string AğAdresi_DoğrulamaKodu = "https://github.com/ArgeMup/GelirGiderTakip/raw/main/bin/Yay%C4%B1nla/Gelir%20Gider%20Takip.exe.DogrulamaKoduUreteci";

#if DEBUG
                //AğAdresi_Uygulama = null;
                //AğAdresi_DoğrulamaKodu = null;
                AğAdresi_Uygulama = "https://github.com/ArgeMup/a/raw/main/Gegita/Gelir%20Gider%20Takip.exe";
                AğAdresi_DoğrulamaKodu = "https://github.com/ArgeMup/a/raw/main/Gegita/Gelir%20Gider%20Takip.exe.DogrulamaKoduUreteci";
#endif

                Şebeke = new YanUygulama.Şebeke_(DosyaYolu, GeriBildirim_İşlemi_Uygulama, Ortak.Çalıştır, Banka.Ayarlar_Genel("YanUygulama/Şube", true), AğAdresi_Uygulama, EnDüşükSürüm, AğAdresi_DoğrulamaKodu);

                while (!Şebeke.BağlantıKuruldu && Ortak.Gösterge.Çalışsın && ZamanAşımıAnı > Environment.TickCount && ArgeMup.HazirKod.ArkaPlan.Ortak.Çalışsın)
                {
                    Ortak.Gösterge.İlerleme = 1;
                    System.Threading.Thread.Sleep(30);
                }

                Ortak.Gösterge.Açıklama = "Gelir Gider Takip bekleniyor";
            }
            else Ortak.Gösterge.Başlat("Gelir Gider Takip bekleniyor", true, null, 500);

            if (!Şebeke.BağlantıKuruldu)
            {
                Durdur();

                Ortak.Gösterge.Bitir();
                string açklm = "Gelir Gider Takip ile bağlantı kurulamadı";
                _Açıkla_(açklm);
                return açklm;
            }

            Şube_Talep.Benzersiz_Tanımlayıcı = DateTime.Now.Yazıya();
            Şube_Talep.Kullanıcı_Adı = Banka.K_lar.KullancıAdı;
            int izinler_başlangıç = (int)Banka.K_lar.İzin.Gelir_gider_Boşta_;
            for (int i = 1; i < Şube_Talep.Kullanıcı_Rolİzinleri.Length; i++)
            {
                bool değeri = Banka.K_lar.GeçerliKullanıcı == null ? true : Banka.K_lar.GeçerliKullanıcı.Rol_İzinleri[izinler_başlangıç + i - 1];
                Şube_Talep.Kullanıcı_Rolİzinleri[i] = değeri;
            }

            Cevap = null;
            Depo_ depo = null;
            Banka.Sınıf_Kaydet(Şube_Talep, ref depo);
            Şebeke.Gönder(depo.YazıyaDönüştür().BaytDizisine());
            while (Cevap == null && Ortak.Gösterge.Çalışsın && ZamanAşımıAnı > Environment.TickCount && ArgeMup.HazirKod.ArkaPlan.Ortak.Çalışsın)
            {
                Ortak.Gösterge.İlerleme = 1;
                System.Threading.Thread.Sleep(30);
            }
            Ortak.Gösterge.Bitir();

            string açıklama = null;
            if (Cevap == null) açıklama = Şube_Talep.Kullanıcı_Komut + " İşlem sonucu belirsiz";
            else
            {
                if (Cevap.Başarılı) Detaylar = Cevap.Detaylar;
                else açıklama = Şube_Talep.Kullanıcı_Komut + " Hatalı " + Cevap.Detaylar?[0];
            }

            _Açıkla_(açıklama);
            return açıklama;

            void _Açıkla_(string _açıklama_)
            {
                if (_açıklama_.DoluMu())
                {
                    _açıklama_.Günlük("Gelir Gider Takip ");

                    if (HatayıGöster) MessageBox.Show(_açıklama_, "Gelir Gider Takip");
                }
            }
        }
        static void GeriBildirim_İşlemi_Uygulama(bool BağlantıKuruldu, byte[] Bilgi, string Açıklama)
        {
            string içerik = Bilgi.Yazıya();
            if (!BağlantıKuruldu || içerik.BoşMu()) 
            {
                if (Açıklama.DoluMu()) Açıklama.Günlük("Gelir Gider Takip ");
                return; 
            }

            Depo_ Depo = new Depo_(içerik);
            Şube_Talep_Cevap_ Cevap = Banka.Sınıf_Oluştur(typeof(Şube_Talep_Cevap_), Depo) as Şube_Talep_Cevap_;
            if (Cevap.Benzersiz_Tanımlayıcı != Şube_Talep.Benzersiz_Tanımlayıcı) return;

            GelirGiderTakip.Cevap = Cevap;
        }
        public static void Durdur()
        {
            Şebeke?.Dispose();
            Şebeke = null;
            Şube_Talep = null;
        }
    }
}
