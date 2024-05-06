using ArgeMup.HazirKod.Dönüştürme;
using ArgeMup.HazirKod.Ekİşlemler;
using ArgeMup.HazirKod;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace İş_ve_Depo_Takip.Ekranlar
{
    public static class Eposta
    {
        static YanUygulama.Şebeke_ Şebeke;
        static Depo_ Cevap;
        static string EpostaAltyapısı_BeyazListe = "";
        static bool EpostaAltyapısı_İlkAçılışAyarlarıYapıldı;

        static string EpostaAltyapısı_Gönder(string Kime, string Bilgi, string Gizli, string Konu, string Mesaj_Html, string Mesaj, string[] DosyaEkleri)
        {
            Depo_ Depo_Komut = new Depo_();

            Depo_Komut.Yaz("Komutlar/Gönder", Kime, 0); //Kime
            Depo_Komut.Yaz("Komutlar/Gönder", Bilgi, 1);
            Depo_Komut.Yaz("Komutlar/Gönder", Gizli, 2);
            Depo_Komut.Yaz("Komutlar/Gönder", Konu, 3);
            Depo_Komut.Yaz("Komutlar/Gönder", Mesaj_Html, 4);
            Depo_Komut.Yaz("Komutlar/Gönder", Mesaj, 5);

            if (DosyaEkleri != null)
            {
                for (int i = 0; i < DosyaEkleri.Length; i++)
                {
                    DosyaEkleri[i] = DosyaEkleri[i];
                }
            }
            Depo_Komut["Komutlar/Gönder/Dosya Ekleri"].İçeriği = DosyaEkleri;

            string _cevap_ = EpostaAltyapısı_Çalıştır(Depo_Komut);
            if (_cevap_.DoluMu()) return _cevap_;

            if (Cevap["Cevaplar/Gönder", 0] != "Başarılı") return "Gönder Başarısız " + Cevap["Cevaplar/Gönder", 1] + Environment.NewLine;

            return null;
        }
        static string EpostaAltyapısı_YenileİşaretleSil(string KlasörAdı, int Yenile_GündenEskiOlanlar, bool Yenile_SadeceOkunmamışlar, string[] OkunduOlarakİşaretle, string[] Taşı, string Taşı_HedefKlasör)
        {
            Depo_ Depo_Komut = new Depo_();

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

            string _cevap_ = EpostaAltyapısı_Çalıştır(Depo_Komut);
            if (_cevap_.DoluMu()) return _cevap_;

            string ht_lar = null;

            if (OkunduOlarakİşaretle != null)
            {
                for (int i = 0; i < OkunduOlarakİşaretle.Length; i++)
                {
                    if (Cevap["Cevaplar/Okundu Olarak İşaretle " + i, 0] != "Başarılı") ht_lar += "Okundu Olarak İşaretle " + i + " " + Cevap["Cevaplar/Okundu Olarak İşaretle " + i, 1];
                }
            }

            if (Taşı != null)
            {
                for (int i = 0; i < Taşı.Length; i++)
                {
                    if (Cevap["Cevaplar/Taşı " + i, 0] != "Başarılı") ht_lar += "Taşı " + i + " " + Cevap["Cevaplar/Taşı " + i, 1];
                }
            }

            if (Yenile_GündenEskiOlanlar > 0)
            {
                if (Cevap["Cevaplar/Epostaları Yenile", 0] != "Başarılı") ht_lar += "Epostaları Yenile " + Cevap["Cevaplar/Epostaları Yenile", 1];
            }

            if (Klasörler == null)
            {
                if (Cevap["Cevaplar/Klasörleri Listele", 0] == "Başarılı")
                {
                    Klasörler = new string[Cevap["Cevaplar/Klasörleri Listele"].Elemanları.Length];
                    for (int i = 0; i < Klasörler.Length; i++)
                    {
                        Klasörler[i] = Cevap["Cevaplar/Klasörleri Listele"].Elemanları[i].Adı;
                    }
                }
            }

            return ht_lar;
        }
        static string _KlasörAdıBoşİseDüzelt_(string KlasörAdı)
        {
            return KlasörAdı.BoşMu(true) ? "Gelen Kutusu" : KlasörAdı;
        }
        static string EpostaAltyapısı_Çalıştır(Depo_ Talep)
        {
            if (Talep == null) throw new Exception("if (Talep == null)");

            IDepo_Eleman ayarlar_eposta = Banka.Ayarlar_Genel("Eposta");
            if (ayarlar_eposta == null || ayarlar_eposta.Oku("Gönderici/Şifresi").BoşMu()) return "Eposta ayarlarını kontrol ediniz";

            int ZamanAşımıAnı = Environment.TickCount + 60000;

            if (Şebeke == null)
            {
                string EnDüşükSürüm = "0.7";
                string DosyaYolu = Klasör.Depolama(Klasör.Kapsamı.Geçici, null, "Eposta", "") + "\\Eposta.exe";
                string AğAdresi_Uygulama = "https://github.com/ArgeMup/Eposta/raw/main/Eposta/bin/Release/Eposta.exe";
                string AğAdresi_DoğrulamaKodu = "https://github.com/ArgeMup/Eposta/raw/main/Eposta/bin/Release/Eposta.exe.DogrulamaKoduUreteci";

#if DEBUG
                //AğAdresi_Uygulama = null;
                //AğAdresi_DoğrulamaKodu = null;
                AğAdresi_Uygulama = "https://github.com/ArgeMup/a/raw/main/Eposta/Eposta.exe";
                AğAdresi_DoğrulamaKodu = "https://github.com/ArgeMup/a/raw/main/Eposta/Eposta.exe.DogrulamaKoduUreteci";
#endif

                Şebeke = new YanUygulama.Şebeke_(DosyaYolu, EpostaAltyapısı_GeriBildirim_İşlemi_Uygulama, Ortak.Çalıştır, Banka.Ayarlar_Genel("YanUygulama/Şube", true), AğAdresi_Uygulama, EnDüşükSürüm, AğAdresi_DoğrulamaKodu);

                while (!Şebeke.BağlantıKuruldu && ZamanAşımıAnı > Environment.TickCount && ArgeMup.HazirKod.ArkaPlan.Ortak.Çalışsın)
                {
                    Thread.Sleep(30);
                }

                EpostaAltyapısı_İlkAçılışAyarlarıYapıldı = false;
                EpostaAltyapısı_BeyazListe = "";
            }

            if (!Şebeke.BağlantıKuruldu)
            {
                Şebeke.Dispose();
                Şebeke = null;

                return "Eposta ile bağlantı kurulamadı";
            }

            if (!EpostaAltyapısı_İlkAçılışAyarlarıYapıldı)
            {
                Depo_ depo = new Depo_();
                depo.Yaz("Komutlar/Başlat Gönderici", ayarlar_eposta.Oku("Gönderici/Adı"), 0);
                depo.Yaz("Komutlar/Başlat Gönderici", ayarlar_eposta.Oku("Gönderici/Adresi"), 1);
                depo.Yaz("Komutlar/Başlat Gönderici", ayarlar_eposta.Oku("Gönderici/Şifresi"), 2);
                depo.Yaz("Komutlar/Başlat Gönderici", ayarlar_eposta.Oku("Sunucu Smtp/Adresi"), 3);
                depo.Yaz("Komutlar/Başlat Gönderici", ayarlar_eposta.Oku_TamSayı("Sunucu Smtp/Erişim Noktası"), 4);
                depo.Yaz("Komutlar/Başlat Gönderici", ayarlar_eposta.Oku_Bit("Sunucu Smtp/SSL"), 5);

                depo.Yaz("Komutlar/Başlat Alıcı", ayarlar_eposta.Oku("Gönderici/Adresi"), 0);
                depo.Yaz("Komutlar/Başlat Alıcı", ayarlar_eposta.Oku("Gönderici/Şifresi"), 1);
                depo.Yaz("Komutlar/Başlat Alıcı", ayarlar_eposta.Oku("Sunucu Imap/Adresi"), 2);
                depo.Yaz("Komutlar/Başlat Alıcı", ayarlar_eposta.Oku_TamSayı("Sunucu Imap/Erişim Noktası"), 3);
                depo.Yaz("Komutlar/Başlat Alıcı", ayarlar_eposta.Oku_Bit("Sunucu Imap/SSL"), 4);
                depo.Yaz("Komutlar/Başlat Alıcı", EpostaAltyapısı_EpostalarınYolu, 5);

                string _cevap_ = _Çalıştır_(depo);
                if (_cevap_.DoluMu()) return _cevap_;

                if (Cevap["Cevaplar/Başlat Gönderici", 0] != "Başarılı" ||
                    Cevap["Cevaplar/Başlat Alıcı", 0] != "Başarılı")
                {
                    return "Ayarlarınızı kontrol ediniz." + Cevap["Cevaplar/Başlat Gönderici", 1] + " " + Cevap["Cevaplar/Başlat Alıcı", 1];
                }

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

                EpostaAltyapısı_İlkAçılışAyarlarıYapıldı = true;
            }

            return _Çalıştır_(Talep);

            string _Çalıştır_(Depo_ _Depo_)
            {
                _Depo_.Yaz("Benzersiz_Tanımlayıcı", DateTime.Now.Yazıya());

                Cevap = null;
                Şebeke.Gönder(_Depo_.YazıyaDönüştür().BaytDizisine());
                while (Cevap == null && ZamanAşımıAnı > Environment.TickCount && ArgeMup.HazirKod.ArkaPlan.Ortak.Çalışsın)
                {
                    Thread.Sleep(30);
                }

                if (Cevap == null)
                {
                    Durdur();
                    return "Epostanın cevap vermesi çok uzun sürdü, tekrar deneyiniz";
                }

                if (_Depo_["Benzersiz_Tanımlayıcı", 0] != Cevap["Benzersiz_Tanımlayıcı", 0])
                {
                    return "İşlem beklendiği şekilde tamamlanmadı, tekrar deneyiniz";
                }

                return null;
            }
        }
        static void EpostaAltyapısı_GeriBildirim_İşlemi_Uygulama(bool BağlantıKuruldu, byte[] Bilgi, string Açıklama)
        {
            string içerik = Bilgi.Yazıya();
            if (!BağlantıKuruldu || içerik.BoşMu())
            {
                EpostaAltyapısı_İlkAçılışAyarlarıYapıldı = false;
                if (Açıklama.DoluMu()) Açıklama.Günlük("Eposta ");
                return;
            }

            Cevap = new Depo_(içerik);
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
            Şebeke?.Dispose();
            Şebeke = null;
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
            Gönder(müşt?.Oku("Kime"), müşt?.Oku("Bilgi"), müşt?.Oku("Gizli"), ayar?.Oku("Konu"), ayar?.Oku("İçerik").Replace("%Müşteri%", Müşteri), null, DosyaEkleri, GeriBildirimİşlemi_Tamamlandı);
        }
        public static void Gönder_Kişiye(string Kişi, string Müşteri, string[] DosyaEkleri, Action<string> GeriBildirimİşlemi_Tamamlandı = null)
        {
            IDepo_Eleman ayar = Banka.Ayarlar_Genel("Eposta/Mesaj");
            Gönder(Kişi, null, null, Müşteri + " " + ayar?.Oku("Konu"), ayar?.Oku("İçerik").Replace("%Müşteri%", Müşteri), null, DosyaEkleri, GeriBildirimİşlemi_Tamamlandı);
        }
        public static void Gönder_Kişiye(string Kişi, string Konu, string Mesaj_Html, string[] DosyaEkleri, Action<string> GeriBildirimİşlemi_Tamamlandı = null)
        {
            Gönder(Kişi, null, null, Konu, Mesaj_Html, null, DosyaEkleri, GeriBildirimİşlemi_Tamamlandı);
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
            return D_DosyaKlasörAdı.Düzelt_Tam(EpostaAltyapısı_EpostalarınYolu + "\\Epostaları Yenile\\" + _KlasörAdıBoşİseDüzelt_(KlasörAdı) + "\\Depo.mup", true);
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
