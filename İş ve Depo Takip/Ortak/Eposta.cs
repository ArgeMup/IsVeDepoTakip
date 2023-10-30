using ArgeMup.HazirKod.Dönüştürme;
using ArgeMup.HazirKod.Ekİşlemler;
using ArgeMup.HazirKod;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace İş_ve_Depo_Takip.Ekranlar
{
    public static class Eposta
    {
        static Process EpostaAltyapısı_İşlem = null;
        static byte[] EpostaAltyapısı_ParolaAes;
        static string EpostaAltyapısı_Eposta_dosyayolu = Klasör.Depolama(Klasör.Kapsamı.Geçici, null, "Eposta", "") + "\\Eposta.exe";
        static string EpostaAltyapısı_KomutDosyasıYolu = Path.GetDirectoryName(EpostaAltyapısı_Eposta_dosyayolu) + "\\Komut.mup";
        static string EpostaAltyapısı_BeyazListe = "";

        static string EpostaAltyapısı_Başlat()
        {
            IDepo_Eleman ayarlar_eposta = Banka.Ayarlar_Genel("Eposta");
            if (ayarlar_eposta == null || ayarlar_eposta.Oku("Gönderici/Şifresi").BoşMu()) return "Eposta ayarlarını kontrol ediniz";
            if (EpostaAltyapısı_İşlem != null && !EpostaAltyapısı_İşlem.HasExited && File.Exists(EpostaAltyapısı_KomutDosyasıYolu)) return null;

            Durdur();
            YeniYazılımKontrolü_ yyk = new YeniYazılımKontrolü_();
            if (File.Exists(EpostaAltyapısı_Eposta_dosyayolu)) yyk.KontrolTamamlandı = true;
            else yyk.Başlat(new Uri("https://github.com/ArgeMup/Eposta/blob/main/Eposta/bin/Release/Eposta.exe?raw=true"), null, EpostaAltyapısı_Eposta_dosyayolu);

            int za;
            if (!yyk.KontrolTamamlandı)
            {
                za = Environment.TickCount + 15000;
                while (!yyk.KontrolTamamlandı && za > Environment.TickCount && ArgeMup.HazirKod.ArkaPlan.Ortak.Çalışsın) Thread.Sleep(35);
            }
            if (!yyk.KontrolTamamlandı || !File.Exists(EpostaAltyapısı_Eposta_dosyayolu)) { yyk.Durdur(); return "Eposta indirilemedi"; }

            if (!Dosya.Sil(EpostaAltyapısı_KomutDosyasıYolu)) return "Dosya silinemedi " + EpostaAltyapısı_KomutDosyasıYolu;

            EpostaAltyapısı_İşlem = Ortak.Çalıştır.UygulamayıDoğrudanÇalıştır(EpostaAltyapısı_Eposta_dosyayolu, null, true);

            za = Environment.TickCount + 15000;
            while (!EpostaAltyapısı_İşlem.HasExited && !File.Exists(EpostaAltyapısı_KomutDosyasıYolu) && za > Environment.TickCount && ArgeMup.HazirKod.ArkaPlan.Ortak.Çalışsın) Thread.Sleep(35);
            if (EpostaAltyapısı_İşlem.HasExited || !File.Exists(EpostaAltyapısı_KomutDosyasıYolu)) return "Eposta beklendiği gibi çalışmadı";

            Depo_ Depo_Komut = new Depo_(File.ReadAllText(EpostaAltyapısı_KomutDosyasıYolu));
            DahaCokKarmasiklastirma_Asimetrik_ Dçk_Rsa = new DahaCokKarmasiklastirma_Asimetrik_(Depo_Komut.Oku("Kimlik Kontrolü"));
            EpostaAltyapısı_ParolaAes = Dçk_Rsa.ParolaÜret();
            byte[] ParolaRsa = Dçk_Rsa.Karıştır(EpostaAltyapısı_ParolaAes);
            Dçk_Rsa.Dispose();
            Depo_Komut.Yaz("Kimlik Kontrolü/ParolaRsa", ParolaRsa.Taban64e());

            //Uygulamanın kendisinin kontrolü
            string SihirliKelime = Path.GetRandomFileName();
            Depo_Komut["Komutlar/Diğer"].İçeriği = new string[] { "Kendini Tanıt", _Karıştır_(SihirliKelime) };
            File.WriteAllText(EpostaAltyapısı_KomutDosyasıYolu, Depo_Komut.YazıyaDönüştür());
            if (!_DepoDosyasınınGüncellenmesiniBekle_()) return "Epostanın cevap vermesi çok uzun sürdü, tekrar deneyiniz";
            Depo_Komut = new Depo_(File.ReadAllText(EpostaAltyapısı_KomutDosyasıYolu));
            if (Depo_Komut["Cevaplar/Diğer", 0] != "Başarılı" || Depo_Komut["Cevaplar/Diğer", 1].Düzelt(Parola.Yazı_Eposta) != SihirliKelime)
            {
                Durdur();
                Thread.Sleep(100);
                Klasör.Sil(EpostaAltyapısı_KomutDosyasıYolu.DosyaYolu_ÜstKlasör());
                return "Eposta uygulaması doğrulanamadı, tekrar deneyiniz.";
            }

            Depo_Komut.Yaz("Kimlik Kontrolü/Gönderici", _Karıştır_(ayarlar_eposta.Oku("Gönderici/Adı")), 0);
            Depo_Komut.Yaz("Kimlik Kontrolü/Gönderici", _Karıştır_(ayarlar_eposta.Oku("Gönderici/Adresi")), 1);
            Depo_Komut.Yaz("Kimlik Kontrolü/Gönderici", _Karıştır_(ayarlar_eposta.Oku("Gönderici/Şifresi")), 2);
            Depo_Komut.Yaz("Kimlik Kontrolü/Gönderici", _Karıştır_(ayarlar_eposta.Oku("Sunucu Smtp/Adresi")), 3);
            Depo_Komut.Yaz("Kimlik Kontrolü/Gönderici", ayarlar_eposta.Oku_TamSayı("Sunucu Smtp/Erişim Noktası"), 4);
            Depo_Komut.Yaz("Kimlik Kontrolü/Gönderici", ayarlar_eposta.Oku_Bit("Sunucu Smtp/SSL"), 5);
            Depo_Komut.Yaz("Kimlik Kontrolü/Gönderici", true /*Tüm İçerik Şifreli*/, 6);

            Depo_Komut.Yaz("Kimlik Kontrolü/Alıcı", _Karıştır_(ayarlar_eposta.Oku("Gönderici/Adresi")), 0);
            Depo_Komut.Yaz("Kimlik Kontrolü/Alıcı", _Karıştır_(ayarlar_eposta.Oku("Gönderici/Şifresi")), 1);
            Depo_Komut.Yaz("Kimlik Kontrolü/Alıcı", _Karıştır_(ayarlar_eposta.Oku("Sunucu Imap/Adresi")), 2);
            Depo_Komut.Yaz("Kimlik Kontrolü/Alıcı", ayarlar_eposta.Oku_TamSayı("Sunucu Imap/Erişim Noktası"), 3);
            Depo_Komut.Yaz("Kimlik Kontrolü/Alıcı", ayarlar_eposta.Oku_Bit("Sunucu Imap/SSL"), 4);
            Depo_Komut.Yaz("Kimlik Kontrolü/Alıcı", _Karıştır_(EpostaAltyapısı_EpostalarınYolu), 5);
            Depo_Komut.Yaz("Kimlik Kontrolü/Alıcı", false /*Çıktıları ParolaAes ile şifrele*/, 6);

            File.WriteAllText(EpostaAltyapısı_KomutDosyasıYolu, Depo_Komut.YazıyaDönüştür());
            if (!_DepoDosyasınınGüncellenmesiniBekle_()) return "Epostanın cevap vermesi çok uzun sürdü, tekrar deneyiniz";

            EpostaAltyapısı_BeyazListe = ayarlar_eposta.Oku("Gönderici/Adresi") + ";" + ayarlar_eposta.Oku("Beyaz Liste"); //kendisi + beyaz liste
            foreach (string müş in Banka.Müşteri_Listele())
            {
                IDepo_Eleman epst = Banka.Ayarlar_Müşteri(müş, "Eposta");
                if (epst != null)
                {
                    string g = epst.Oku("Kime");
                    if (g.DoluMu()) EpostaAltyapısı_BeyazListe += ";" + g;

                    g = epst.Oku("Bilgi");
                    if (g.DoluMu()) EpostaAltyapısı_BeyazListe += ";" + g;

                    g = epst.Oku("Gizli");
                    if (g.DoluMu()) EpostaAltyapısı_BeyazListe += ";" + g;
                }
            }

            return null;
        }
        static string EpostaAltyapısı_Gönder(string Kime, string Bilgi, string Gizli, string Konu, string Mesaj_Html, string Mesaj, string[] DosyaEkleri)
        {
            string snç = EpostaAltyapısı_Başlat();
            if (snç.DoluMu()) return snç;

            Depo_ Depo_Komut = new Depo_(File.ReadAllText(EpostaAltyapısı_KomutDosyasıYolu));
            Depo_Komut.Sil("Komutlar");
            Depo_Komut.Sil("Cevaplar");

            Depo_Komut.Yaz("Komutlar/Gönder", _Karıştır_(Kime), 0); //Kime
            Depo_Komut.Yaz("Komutlar/Gönder", _Karıştır_(Bilgi), 1);
            Depo_Komut.Yaz("Komutlar/Gönder", _Karıştır_(Gizli), 2);
            Depo_Komut.Yaz("Komutlar/Gönder", _Karıştır_(Konu), 3);
            Depo_Komut.Yaz("Komutlar/Gönder", _Karıştır_(Mesaj_Html), 4);
            Depo_Komut.Yaz("Komutlar/Gönder", _Karıştır_(Mesaj), 5);

            if (DosyaEkleri != null)
            {
                for (int i = 0; i < DosyaEkleri.Length; i++)
                {
                    DosyaEkleri[i] = _Karıştır_(DosyaEkleri[i]);
                }
            }
            Depo_Komut["Komutlar/Gönder/Dosya Ekleri"].İçeriği = DosyaEkleri;

            File.WriteAllText(EpostaAltyapısı_KomutDosyasıYolu, Depo_Komut.YazıyaDönüştür());
            if (!_DepoDosyasınınGüncellenmesiniBekle_()) return "Epostanın cevap vermesi çok uzun sürdü, tekrar deneyiniz";

            Depo_Komut = new Depo_(File.ReadAllText(EpostaAltyapısı_KomutDosyasıYolu));
            if (Depo_Komut["Cevaplar/Gönder", 0] != "Başarılı") return "Gönder Başarısız " + Depo_Komut["Cevaplar/Gönder", 1] + Environment.NewLine;

            return null;
        }
        static string EpostaAltyapısı_YenileİşaretleSil(string KlasörAdı, int Yenile_GündenEskiOlanlar, bool Yenile_SadeceOkunmamışlar, string[] OkunduOlarakİşaretle, string[] Taşı, string Taşı_HedefKlasör)
        {
            string snç = EpostaAltyapısı_Başlat();
            if (snç.DoluMu()) return snç;

            Depo_ Depo_Komut = new Depo_(File.ReadAllText(EpostaAltyapısı_KomutDosyasıYolu));
            Depo_Komut.Sil("Komutlar");
            Depo_Komut.Sil("Cevaplar");

            if (OkunduOlarakİşaretle != null)
            {
                for (int i = 0; i < OkunduOlarakİşaretle.Length; i++)
                {
                    Depo_Komut["Komutlar/Okundu Olarak İşaretle " + i].İçeriği = new string[] { KlasörAdı, OkunduOlarakİşaretle[i] };
                }
            }

            if (Taşı != null)
            {
                for (int i = 0; i < Taşı.Length; i++)
                {
                    Depo_Komut["Komutlar/Taşı/" + Taşı[i]].İçeriği = new string[] { KlasörAdı, Taşı_HedefKlasör };
                }
            }

            if (Yenile_GündenEskiOlanlar > 0)
            {
                Depo_Komut.Yaz("Komutlar/Epostaları Yenile", KlasörAdı, 0);
                Depo_Komut.Yaz("Komutlar/Epostaları Yenile", Yenile_SadeceOkunmamışlar, 1);
                Depo_Komut.Yaz("Komutlar/Epostaları Yenile", DateTime.Now.AddDays(-Yenile_GündenEskiOlanlar), 2); //Aralık Başlangıç
                Depo_Komut.Yaz("Komutlar/Epostaları Yenile", DateTime.Now, 3); //Aralık Bitiş
                Depo_Komut.Yaz("Komutlar/Epostaları Yenile", true, 4); //Dosya eklerini al
            }

            if (Klasörler == null)
            {
                Depo_Komut.Yaz("Komutlar/Klasörleri Listele", false);
            }

            File.WriteAllText(EpostaAltyapısı_KomutDosyasıYolu, Depo_Komut.YazıyaDönüştür());
            if (!_DepoDosyasınınGüncellenmesiniBekle_()) return "Epostanın cevap vermesi çok uzun sürdü, tekrar deneyiniz";

            Depo_Komut = new Depo_(File.ReadAllText(EpostaAltyapısı_KomutDosyasıYolu));
            string ht_lar = null;

            if (OkunduOlarakİşaretle != null)
            {
                for (int i = 0; i < OkunduOlarakİşaretle.Length; i++)
                {
                    if (Depo_Komut["Cevaplar/Okundu Olarak İşaretle " + i, 0] != "Başarılı") ht_lar += "Okundu Olarak İşaretle " + i + " " + Depo_Komut["Cevaplar/Okundu Olarak İşaretle " + i, 1];
                }
            }

            if (Taşı != null)
            {
                for (int i = 0; i < Taşı.Length; i++)
                {
                    if (Depo_Komut["Cevaplar/Taşı " + i, 0] != "Başarılı") ht_lar += "Taşı " + i + " " + Depo_Komut["Cevaplar/Taşı " + i, 1];
                }
            }

            if (Yenile_GündenEskiOlanlar > 0)
            {
                if (Depo_Komut["Cevaplar/Epostaları Yenile", 0] != "Başarılı") ht_lar += "Epostaları Yenile " + Depo_Komut["Cevaplar/Epostaları Yenile", 1];
            }

            if (Klasörler == null)
            {
                if (Depo_Komut["Cevaplar/Klasörleri Listele", 0] == "Başarılı")
                {
                    Klasörler = new string[Depo_Komut["Cevaplar/Klasörleri Listele"].Elemanları.Length];
                    for (int i = 0; i < Klasörler.Length; i++)
                    {
                        Klasörler[i] = Depo_Komut["Cevaplar/Klasörleri Listele"].Elemanları[i].Adı;
                    }
                }
            }

            return ht_lar;
        }
        static string _Karıştır_(string Girdi)
        {
            if (Girdi.BoşMu(true)) return Girdi;

            return Girdi.BaytDizisine().Karıştır(EpostaAltyapısı_ParolaAes).Taban64e();
        }
        static bool _DepoDosyasınınGüncellenmesiniBekle_(int msn = 30000)
        {
            DateTime Şimdi = DateTime.Now;

            int za_aş = Environment.TickCount + msn;
            while (EpostaAltyapısı_İşlem != null &&
                !EpostaAltyapısı_İşlem.HasExited &&
                File.GetLastWriteTime(EpostaAltyapısı_KomutDosyasıYolu) <= Şimdi &&
                za_aş > Environment.TickCount &&
                ArgeMup.HazirKod.ArkaPlan.Ortak.Çalışsın) Thread.Sleep(1000);

            return File.GetLastWriteTime(EpostaAltyapısı_KomutDosyasıYolu) > Şimdi;
        }
        static string _KlasörAdıBoşİseDüzelt_(string KlasörAdı)
        {
            return KlasörAdı.BoşMu(true) ? "Gelen Kutusu" : KlasörAdı;
        }

        public static string[] Klasörler = null;
        public static string EpostaAltyapısı_EpostalarınYolu = Ortak.Klasör_Gecici + "Ep";
        public static bool BirEpostaHesabıEklenmişMi
        {
            get { return Banka.Ayarlar_Genel("Eposta", true).Oku("Gönderici/Şifresi").DoluMu(); }
        }
        public static void Girişİşlemleri()
        {
            if (!BirEpostaHesabıEklenmişMi) return;

            //epostaların okunduğu ekrandaki (Yeni_İş_Girişi_Epostalar) kullanıcının son ayarlarını almak için
            IDepo_Eleman Ayrl_Kullanıcı = Banka.Ayarlar_Kullanıcı("Yeni_İş_Girişi_Epostalar", null);
            YenileİşaretleSil(Ayrl_Kullanıcı.Oku("Seç_Klasör"), Ayrl_Kullanıcı.Oku_TamSayı("Seç_GünKadarEskiEpostalar", 7), Ayrl_Kullanıcı.Oku_Bit("Seç_SadeceOkunmamışlar", true));
        }
        public static bool GönderenBeyazListeİçindeMi(string GönderenEpostaAdresi)
        {
            return EpostaAltyapısı_BeyazListe.Contains(GönderenEpostaAdresi.ToLower());
        }
        public static void Durdur()
        {
            EpostaAltyapısı_İşlem?.Dispose();
            EpostaAltyapısı_İşlem = null;
            EpostaAltyapısı_BeyazListe = "";

            Process[] l = Process.GetProcessesByName("Eposta");
            if (l != null && l.Length > 0)
            {
                foreach (var p in l)
                {
                    if (p.HasExited) continue;
                    if (p.MainModule.FileName == EpostaAltyapısı_Eposta_dosyayolu) p.Kill();
                }
            }
        }
        public static void Gönder(string Kime, string Bilgi, string Gizli, string Konu, string Mesaj_Html, string Mesaj, string[] DosyaEkleri, Action<string> GeriBildirimİşlemi_Tamamlandı = null)
        {
            string sonuç = null;
            Task.Run(() =>
            {
                sonuç = EpostaAltyapısı_Gönder(Kime, Bilgi, Gizli, Konu, Mesaj_Html, Mesaj, DosyaEkleri);
            }).ContinueWith((t) =>
            {
                GeriBildirimİşlemi_Tamamlandı?.Invoke(sonuç);
            });
        }
        public static void Gönder_Müşteriye(string Müşteri, string[] DosyaEkleri, Action<string> GeriBildirimİşlemi_Tamamlandı = null)
        {
            IDepo_Eleman müşt = Banka.Ayarlar_Müşteri(Müşteri, "Eposta");
            IDepo_Eleman ayar = Banka.Ayarlar_Genel("Eposta/Mesaj");
            Gönder(müşt.Oku("Kime"), müşt.Oku("Bilgi"), müşt.Oku("Gizli"), ayar.Oku("Konu"), ayar.Oku("İçerik").Replace("%Müşteri%", Müşteri), null, DosyaEkleri, GeriBildirimİşlemi_Tamamlandı);
        }
        public static void Gönder_Kişiye(string Kişi, string Müşteri, string[] DosyaEkleri, Action<string> GeriBildirimİşlemi_Tamamlandı = null)
        {
            IDepo_Eleman ayar = Banka.Ayarlar_Genel("Eposta/Mesaj");
            Gönder(Kişi, null, null, Müşteri + " " + ayar.Oku("Konu"), ayar.Oku("İçerik").Replace("%Müşteri%", Müşteri), null, DosyaEkleri, GeriBildirimİşlemi_Tamamlandı);
        }
        public static void YenileİşaretleSil(string KlasörAdı, int Yenile_GündenEskiOlanlar = 7, bool Yenile_SadeceOkunmamışlar = true, string[] OkunduOlarakİşaretle = null, string[] Taşı = null, string Taşı_HedefKlasör = null, Action<string> GeriBildirimİşlemi_Tamamlandı = null)
        {
            string sonuç = null;
            Task.Run(() =>
            {
                sonuç = EpostaAltyapısı_YenileİşaretleSil(KlasörAdı, Yenile_GündenEskiOlanlar, Yenile_SadeceOkunmamışlar, OkunduOlarakİşaretle, Taşı, Taşı_HedefKlasör);
            }).ContinueWith((t) =>
            {
                GeriBildirimİşlemi_Tamamlandı?.Invoke(sonuç);
            });
        }
        public static string Yenile_DepoDosyaYolu(string KlasörAdı)
        {
            return D_DosyaKlasörAdı.Düzelt(EpostaAltyapısı_EpostalarınYolu + "\\Epostaları Yenile\\" + _KlasörAdıBoşİseDüzelt_(KlasörAdı) + "\\Depo.mup");
        }

        //public static void EpostaGönder_İstisna(Exception İstisna, int ZamanAşımı_msn = 60000)
        //{
        //    if (!BirEpostaHesabıEklenmişMi) return;

        //    Task.Run(() =>
        //    {
        //        List<string> DosyaEkleri = new List<string>();
        //        string HataMesajı = "İstisna " + DateTime.Now.Yazıya();

        //        try
        //        {
        //            while (İstisna != null)
        //            {
        //                HataMesajı += Environment.NewLine + İstisna.ToString();
        //                İstisna = İstisna.InnerException;
        //            }
        //        }
        //        catch (Exception) { }

        //        try
        //        {
        //            //Son 15 günlük dosyası
        //            System.Threading.Thread.Sleep(100); //Günlük dosyalarının oluşması için
        //            Klasör_ gnlk = new Klasör_(Kendi.Klasörü + "\\Günlük");
        //            gnlk.Sırala_EskidenYeniye();
        //            gnlk.Klasörler = new List<string>();
        //            if (gnlk.Dosyalar.Count > 15) gnlk.Dosyalar.RemoveRange(0, gnlk.Dosyalar.Count - 15);
        //            string dsy_gnlk = Ortak.Klasör_Gecici + Path.GetRandomFileName() + "_Günlük.zip";
        //            if (SıkıştırılmışDosya.Klasörden(gnlk, dsy_gnlk)) DosyaEkleri.Add(dsy_gnlk);
        //        }
        //        catch (Exception) { }

        //        try
        //        {
        //            //Banka içeriği
        //            string dsy_bnk = Ortak.Klasör_Gecici + Path.GetRandomFileName() + "_Banka.zip";
        //            if (SıkıştırılmışDosya.Klasörden(Ortak.Klasör_Banka, dsy_bnk)) DosyaEkleri.Add(dsy_bnk);
        //        }
        //        catch (Exception) { }

        //        Eposta.Gönder("ArgeMup@yandex.com", null, null, "İstisna Hk.", "Oluşan hataya dair detaylar :" + Environment.NewLine + HataMesajı.Replace("|", Environment.NewLine), null, DosyaEkleri.ToArray());
        //    });
        //}
    }
}
