using ArgeMup.HazirKod.Ekİşlemler;
using ArgeMup.HazirKod;
using System.IO;
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
        public enum İlkAçılışAyarları_Komut_ { Boşta, Sayfa_GelirGiderEkle, Sayfa_CariDöküm, Sayfa_Ayarlar, Ekle_GelirGider, Yazdır };
        public class İlkAçılışAyarları_Ekle_GelirGider_Talep_
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
        public class İlkAçılışAyarları_
        {
            [Değişken_.Niteliği.Adını_Değiştir("Bt")] public string Benzersiz_Tanımlayıcı;
            [Değişken_.Niteliği.Adını_Değiştir("İy", 0)] public string İşyeri_Adı;
            [Değişken_.Niteliği.Adını_Değiştir("İy", 1)] public string İşyeri_Parolası;
            [Değişken_.Niteliği.Adını_Değiştir("İy", 2)] public string İşyeri_LogoDosyaYolu;
            [Değişken_.Niteliği.Adını_Değiştir("Kk")] public string KayıtKlasörü;
            [Değişken_.Niteliği.Adını_Değiştir("Mu")] public Dictionary<string, List<string>> SabitMuhataplar; // Grup ve elemanları, adı Çalışan olan grup uygulamada çalışanlar olarak değerlendirilir

            [Değişken_.Niteliği.Adını_Değiştir("KuR")] public bool[] Kullanıcı_Rolİzinleri;
            [Değişken_.Niteliği.Adını_Değiştir("Ku", 0)] public string Kullanıcı_Adı;
            [Değişken_.Niteliği.Adını_Değiştir("Ku", 1)] public İlkAçılışAyarları_Komut_ Kullanıcı_Komut;
            [Değişken_.Niteliği.Adını_Değiştir("KuEt")] public string[] Kullanıcı_Komut_EkTanım; //Yazdırma : pdf dosya yolu + Şablon Adı

            [Değişken_.Niteliği.Adını_Değiştir("E GeGi")] public List<İlkAçılışAyarları_Ekle_GelirGider_Talep_> Ekle_GelirGider_Talepler;

            public İlkAçılışAyarları_()
            {
                İşyeri_Adı = Banka.İşyeri_Adı;
                İşyeri_Parolası = Banka.Ayarlar_Genel("Uygulama Kimliği", true).Oku(null);
                if (İşyeri_Parolası.BoşMu(true)) throw new Exception("if (İlkAçılışAyarları.İşyeri_Parolası.BoşMu(true))");

                İşyeri_LogoDosyaYolu = Ortak.Firma_Logo_DosyaYolu;
                KayıtKlasörü = Ortak.Klasör_KullanıcıDosyaları_GelirGiderTakip;

                SabitMuhataplar = new Dictionary<string, List<string>>();
                SabitMuhataplar.Add("Müşteri", Banka.Müşteri_Listele());

                List<string> Çalışanlar = new List<string>();
                Banka.Kullanıcı_İzinleri_Tutucusu.Kişiler.ForEach(x => Çalışanlar.Add(x.Adı));
                SabitMuhataplar.Add("Çalışan", Çalışanlar);

                Kullanıcı_Rolİzinleri = new bool[(int)Banka.Ayarlar_Kullanıcılar_İzin.DiziElemanSayısı_Gelir_gider_ + 1 /*boşta*/];
            }
        }

        static string Uygulama_DosyaYolu = Klasör.Depolama(Klasör.Kapsamı.Geçici, null, "Gelir_Gider_Takip", "") + "\\Gelir Gider Takip.exe";
        static string Uygulama_Sonuç_DosyaYolu = Klasör.Depolama(Klasör.Kapsamı.Geçici, null, "Gelir_Gider_Takip", "") + "\\Sonuç.mup";
        static string[] Komut_DosyasıYolu = new string[] { Path.GetDirectoryName(Uygulama_DosyaYolu) + "\\Komut.mup" };
        static İlkAçılışAyarları_ İlkAçılışAyarları = null;

        public static string Komut_SayfaAç(İlkAçılışAyarları_Komut_ Komut, bool Sayfa_GelirGiderEkle_GelirOlarakAçılsın = false)
        {
            if (Komut < İlkAçılışAyarları_Komut_.Sayfa_GelirGiderEkle || Komut > İlkAçılışAyarları_Komut_.Sayfa_Ayarlar) return "Hatalı Komut " + Komut;

            if (İlkAçılışAyarları == null) İlkAçılışAyarları = new İlkAçılışAyarları_();

            İlkAçılışAyarları.Kullanıcı_Komut = Komut;
            İlkAçılışAyarları.Kullanıcı_Komut_EkTanım = Sayfa_GelirGiderEkle_GelirOlarakAçılsın ? new string[] { "Gelir" } : null;

            return Çalıştır(true, 0);
        }
        public static İlkAçılışAyarları_Ekle_GelirGider_Talep_ Komut_Ekle_GelirGider(string MuhatapGrubuAdı, string MuhatapAdı,
                    İşyeri_Ödeme_İşlem_Tipi_ Tipi, İşyeri_Ödeme_İşlem_Durum_ Durumu, double Miktar, İşyeri_Ödeme_ParaBirimi_ ParaBirimi,
                    DateTime İlkÖdemeTarihi, string Notlar,
                    int Taksit_Sayısı, Muhatap_Üyelik_Dönem_ Taksit_Dönem, int Taksit_Dönem_Adet,
                    DateTime? KayıtTarihi = null)
        {
            return new İlkAçılışAyarları_Ekle_GelirGider_Talep_()
            {
                Ekle_MuhatapGrubuAdı = MuhatapGrubuAdı, Ekle_MuhatapAdı = MuhatapAdı,
                Ekle_Tipi = Tipi, Ekle_Durumu = Durumu, Ekle_Miktar = Miktar, Ekle_ParaBirimi = ParaBirimi,
                Ekle_İlkÖdemeTarihi = İlkÖdemeTarihi, Ekle_Notlar = Notlar,
                Ekle_Taksit_Sayısı = Taksit_Sayısı, Ekle_Taksit_Dönem = Taksit_Dönem, Ekle_Taksit_Dönem_Adet = Taksit_Dönem_Adet,
                Ekle_KayıtTarihi = KayıtTarihi
            };
        }
        public static string Komut_Ekle_GelirGider(List<İlkAçılışAyarları_Ekle_GelirGider_Talep_> Talepler)
        {
            if (Talepler == null || Talepler.Count == 0) return "Hiç ödeme bulunamadı";

            if (İlkAçılışAyarları == null) İlkAçılışAyarları = new İlkAçılışAyarları_();

            İlkAçılışAyarları.Kullanıcı_Komut = İlkAçılışAyarları_Komut_.Ekle_GelirGider;
            İlkAçılışAyarları.Ekle_GelirGider_Talepler = Talepler;

            return Çalıştır(false, 15000);
        }
        public static string Komut_Yazdır(string PdfDosyaYolu, string Şablon = null)
        {
            if (İlkAçılışAyarları == null) İlkAçılışAyarları = new İlkAçılışAyarları_();

            İlkAçılışAyarları.Kullanıcı_Komut = İlkAçılışAyarları_Komut_.Yazdır;
            İlkAçılışAyarları.Kullanıcı_Komut_EkTanım = new string[] { PdfDosyaYolu, Şablon };

            return Çalıştır(false, 15000);
        }
        static string Çalıştır(bool HatayıGöster, int ZamanAşımı_msn)
        {
            İlkAçılışAyarları.Benzersiz_Tanımlayıcı = DateTime.Now.Yazıya();
            İlkAçılışAyarları.Kullanıcı_Adı = Banka.KullancıAdı;
            int izinler_başlangıç = (int)Banka.Ayarlar_Kullanıcılar_İzin.Gelir_gider_Boşta_;
            for (int i = 1; i < İlkAçılışAyarları.Kullanıcı_Rolİzinleri.Length; i++)
            {
                bool değeri = Banka.Kullanıcı_İzinleri_Tutucusu.GeçerliKullanıcı == null ? true : Banka.Kullanıcı_İzinleri_Tutucusu.GeçerliKullanıcı.Rol_İzinleri[izinler_başlangıç + i - 1];
                İlkAçılışAyarları.Kullanıcı_Rolİzinleri[i] = değeri;
            }

            Depo_ depo = null;
            Banka.Sınıf_Kaydet(GelirGiderTakip.İlkAçılışAyarları, ref depo);
            depo.YazıyaDönüştür().BaytDizisine().Karıştır(Parola.Dizi_GelirGiderTakip).Dosyaİçeriği_Yaz(Komut_DosyasıYolu[0]);

            YeniYazılımKontrolü_ yyk = new YeniYazılımKontrolü_();
            if (Ortak.DosyaGüncelMi(Uygulama_DosyaYolu, 0, 9)) yyk.KontrolTamamlandı = true;
            else yyk.Başlat(new Uri("https://github.com/ArgeMup/GelirGiderTakip/blob/main/bin/Yay%C4%B1nla/Gelir%20Gider%20Takip.exe?raw=true"), null, Uygulama_DosyaYolu);

            if (!yyk.KontrolTamamlandı)
            {
                Ortak.Gösterge.Başlat("Gelir Gider Takip indiriliyor", true, null, 15);
                int tümü_sayac = Environment.TickCount + 15000;
                while (!yyk.KontrolTamamlandı && Ortak.Gösterge.Çalışsın && tümü_sayac > Environment.TickCount && ArgeMup.HazirKod.ArkaPlan.Ortak.Çalışsın)
                {
                    Ortak.Gösterge.İlerleme = 1;
                    System.Threading.Thread.Sleep(1000);
                }
                Ortak.Gösterge.Bitir();
            }
            if (!yyk.KontrolTamamlandı || !File.Exists(Uygulama_DosyaYolu)) { yyk.Durdur(); return "Gelir Gider Takip indirilemedi"; }
            
            if (!Dosya.Sil(Uygulama_Sonuç_DosyaYolu)) return "Sonuç dosyası silinemedi";
            var işlem = Ortak.Çalıştır.UygulamayıDoğrudanÇalıştır(Uygulama_DosyaYolu, Komut_DosyasıYolu);

            bool Bitti = false;
            string Sonuç = null;
            int zamanaşımı = ZamanAşımı_msn > 0 ? Environment.TickCount + ZamanAşımı_msn : 0;
            System.Threading.Tasks.Task.Run(() =>
            {
                while (!işlem.HasExited && ArgeMup.HazirKod.ArkaPlan.Ortak.Çalışsın && İlkAçılışAyarları != null)
                {
                    System.Threading.Thread.Sleep(35);

                    if (ZamanAşımı_msn > 0 && zamanaşımı < Environment.TickCount) break;
                }

                Bitti = true;

                if (!işlem.HasExited)
                {
                    try { işlem.Kill(); } catch (Exception) { }
                    Sonuç = "Gelir Gider Takip kapanamadı";
                }
                else
                {
                    Sonuç = Uygulama_Sonuç_DosyaYolu.DosyaYolu_Oku_Yazı();
                    if (Sonuç.BoşMu(true) || İlkAçılışAyarları.Benzersiz_Tanımlayıcı.BoşMu(true)) Sonuç = "İşlem sonucu belirsiz";
                    else if (Sonuç == "Tamam " + İlkAçılışAyarları.Benzersiz_Tanımlayıcı) Sonuç = null;
                    
                    if (Sonuç.DoluMu())
                    {
                        Sonuç = İlkAçılışAyarları.Kullanıcı_Komut + " " + Sonuç;
                        Sonuç.Günlük("Gelir Gider Takip ");

                        if (HatayıGöster) MessageBox.Show(Sonuç, "Gelir Gider Takip");
                    }
                }
            });

            if (ZamanAşımı_msn > 0) //Bitmesini bekle
            {
                Ortak.Gösterge.Başlat("Gelir Gider Takip bekleniyor", false, null, ZamanAşımı_msn / 35);

                while (!Bitti && Ortak.Gösterge.Çalışsın)
                {
                    Ortak.Gösterge.İlerleme = 1;
                    System.Threading.Thread.Sleep(35);
                }

                Ortak.Gösterge.Bitir();
            }

            return Sonuç;
        }
        public static void Durdur()
        {
            İlkAçılışAyarları = null;

            System.Diagnostics.Process[] l = System.Diagnostics.Process.GetProcessesByName("Gelir Gider Takip");
            if (l != null && l.Length > 0)
            {
                foreach (var p in l)
                {
                    if (p.HasExited) continue;
                    if (p.MainModule.FileName == Uygulama_DosyaYolu) p.Kill();
                }
            }
        }
    }
}
