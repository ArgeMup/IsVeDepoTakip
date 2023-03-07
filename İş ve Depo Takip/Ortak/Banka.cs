using ArgeMup.HazirKod;
using ArgeMup.HazirKod.Dönüştürme;
using ArgeMup.HazirKod.Ekİşlemler;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip
{
    class Banka_Tablo_
    {
        public string Müşteri;
        public Banka.TabloTürü Türü;

        public List<IDepo_Eleman> Talepler;
        public IDepo_Eleman Ödeme;

        public Banka_Tablo_(string Müşteri)
        {
            this.Müşteri = Müşteri;
            Talepler = new List<IDepo_Eleman>();
        }
    }

    class Banka
    {
        public const string Sürüm = "1";
        static int Malzemeler_GeriyeDönükİstatistik_Ay = 36;
        public static void Giriş_İşlemleri(Label AçılışYazısı)
        {
            int Açılışİşlemi_Tik = Environment.TickCount;
            Klasör.Oluştur(Ortak.Klasör_Banka);
            Klasör.Oluştur(Ortak.Klasör_İçYedek);
            Klasör.Oluştur(Ortak.Klasör_KullanıcıDosyaları);
            Klasör.Oluştur(Ortak.Klasör_KullanıcıDosyaları_KorumalıAlan);
            Klasör.Oluştur(Ortak.Klasör_KullanıcıDosyaları_ArkaPlanResimleri);
            Klasör.Oluştur(Ortak.Klasör_Gecici);
            Ortak.Gösterge_Açılışİşlemi(AçılışYazısı, "Klasörler", ref Açılışİşlemi_Tik);

            DoğrulamaKodu.KontrolEt.Durum_ snç = DoğrulamaKodu.KontrolEt.Klasör(Ortak.Klasör_Banka, SearchOption.AllDirectories, Parola.Yazı, Ortak.EşZamanlıİşlemSayısı);
            Ortak.Gösterge_Açılışİşlemi(AçılışYazısı, "Bütünlük Kontrolü", ref Açılışİşlemi_Tik);
            Günlük.Ekle("Bütünlük Kontrolü " + snç.ToString());
            switch (snç)
            {
                case DoğrulamaKodu.KontrolEt.Durum_.Aynı:
                    goto Devam;

                case DoğrulamaKodu.KontrolEt.Durum_.DoğrulamaDosyasıYok:
#if !DEBUG
                    Klasör_ kls = new Klasör_(Ortak.Klasör_Banka, EşZamanlıİşlemSayısı: Ortak.EşZamanlıİşlemSayısı);
                    if (kls.Dosyalar.Count > 0) throw new Exception("Büyük Hata A");
#endif
                    goto Devam;

                default:
                case DoğrulamaKodu.KontrolEt.Durum_.DoğrulamaDosyasıİçeriğiHatalı:
                case DoğrulamaKodu.KontrolEt.Durum_.Farklı:
                case DoğrulamaKodu.KontrolEt.Durum_.FazlaKlasörVeyaDosyaVar:
                    snç = DoğrulamaKodu.KontrolEt.Klasör(Ortak.Klasör_Banka2, SearchOption.AllDirectories, Parola.Yazı, Ortak.EşZamanlıİşlemSayısı);
                    Günlük.Ekle("Bütünlük Kontrolü 2 " + snç.ToString());
                    if (snç == DoğrulamaKodu.KontrolEt.Durum_.Aynı)
                    {
                        if (Ortak.Klasör_TamKopya(Ortak.Klasör_Banka2, Ortak.Klasör_Banka))
                        {
                            Değişiklikler_TamponuSıfırla();
                            goto Devam;
                        }
                    }
                    break;
            }

            throw new Exception("Banka klasörünün içeriği hatalı" + Environment.NewLine +
                        "Son yaptığınız işlem yarım kalmış olabilir" + Environment.NewLine +
                        "Yedeği kullanmak için" + Environment.NewLine +
                        "Banka klasörünün içeriğini tamamen silebilir ve" + Environment.NewLine +
                        "Yedek klasöründen en son tarihli yedeğini Banka klasörü içerisine çıkarabilirsiniz");

        Devam:
            #region Ayarlar
            IDepo_Eleman d_ayarlar_bilgisayar_kullanıcı = Ayarlar_BilgisayarVeKullanıcı("Klasör", true);
            Ortak.Kullanıcı_Klasör_Yedek = d_ayarlar_bilgisayar_kullanıcı.Bul("Yedek", true).İçeriği;
            Ortak.Kullanıcı_Klasör_Pdf = d_ayarlar_bilgisayar_kullanıcı.Oku("Pdf");

            Depo_ d = Tablo(null, TabloTürü.Ayarlar, true);
            Ortak.Kullanıcı_KüçültüldüğündeParolaSor = d.Oku_Bit("Küçültüldüğünde Parola Sor", true, 0);
            Ortak.Kullanıcı_KüçültüldüğündeParolaSor_sn = d.Oku_TamSayı("Küçültüldüğünde Parola Sor", 60, 1);
            Ortak.Kullanıcı_Eposta_hesabı_mevcut = !string.IsNullOrEmpty(d.Oku("Eposta/Gönderici/Şifresi"));

            while (d.Oku_TarihSaat("Son Banka Kayıt", default, 1) > DateTime.Now)
            {
                string msg = "Son kayıt saati : " + d.Oku("Son Banka Kayıt", default, 1) + Environment.NewLine +
                    "Bilgisayarınızın saati : " + DateTime.Now.Yazıya() + Environment.NewLine + Environment.NewLine +
                    "Muhtemelen bilgisayarınızın saati geri kaldı, lütfen düzeltip devam ediniz";

                MessageBox.Show(msg.Günlük(), Ortak.AnaEkran.Text + " Bütünlük Kontrolü");
            }

            Ortak.Gösterge_Açılışİşlemi(AçılışYazısı, "Ayarlar", ref Açılışİşlemi_Tik);
            #endregion

            #region yedekleme
            Yedekle_DahaYeniYedekVarsa_KullanıcıyaSor();
            Yedekleme_İzleyici_Başlat();
            Ortak.Gösterge_Açılışİşlemi(AçılışYazısı, "Yedekleme", ref Açılışİşlemi_Tik);

            Yedekle_Banka();
            Ortak.Gösterge_Açılışİşlemi(AçılışYazısı, "İlk Kullanıma Hazırlanıyor", ref Açılışİşlemi_Tik);
            #endregion

            #region Yeni Sürüme Uygun Hale Getirme
            //if (Koşul)
            //{
            //    Değişiklikler_TamponuSıfırla();
            //    string ydk_zip = "SürümYükseltmeÖncesiYedeği.zip";
            //    if (!File.Exists(Kendi.Klasörü + "\\" + ydk_zip))
            //    {
            //        SıkıştırılmışDosya.Klasörden(Kendi.Klasörü, Ortak.Klasör_Gecici + ydk_zip);
            //        Dosya.Kopyala(Ortak.Klasör_Gecici + ydk_zip, Kendi.Klasörü + "\\" + ydk_zip);
            //    }

            //    Günlük.Ekle("Banka yeni sürüme geçirme aşama x tamam");

            //    //Ayarlar
            //    Ayarlar = Depo_Aç("Ay");
            //    IDepo_Eleman ayr = Ayarlar["Son Banka Kayıt"];
            //    ayr.Yaz(null, DateTime.Now, 1);
            //    ayr.Yaz(null, Sürüm, 2);

            //    Değişiklikleri_Kaydet(null);
            //    Yedekle_Banka();
            //    Değişiklikler_TamponuSıfırla();

            //    Dosya.Sil(Kendi.Klasörü + "\\" + ydk_zip);
            //}
            //Ortak.Gösterge_Açılışİşlemi(AçılışYazısı, "Yeni Sürüme Uygunlaştırma", ref Açılışİşlemi_Tik);
            #endregion

            Ortak.Hatırlatıcılar.KontrolEt();
            Ortak.Gösterge_Açılışİşlemi(AçılışYazısı, "Takvim", ref Açılışİşlemi_Tik);

            Malzeme_KritikMiktarKontrolü();
            Ortak.Gösterge_Açılışİşlemi(AçılışYazısı, "Malzeme Durumu", ref Açılışİşlemi_Tik);
        }
        public static void Çıkış_İşlemleri()
        {
            Klasör.Sil(Ortak.Klasör_Gecici);
        }
        public static Depo_ ÖrnekMüşteriTablosuOluştur()
        {
            Depo_ Depo = new Depo_();
            Depo["Tür", 1] = "Örnek Müşteri Adı";

            int örnek_iş_sayısı = 0;
            DateTime t = DateTime.Now.AddYears(-1);
            for (int i = 1; i <= 35; i++) //talep sayısı
            {
                if (++örnek_iş_sayısı > 6) örnek_iş_sayısı = 1;

                Depo.Yaz("Talepler/SeriNo" + i, "Örnek Hasta Adı " + i, 0);
                if (örnek_iş_sayısı == 1 || örnek_iş_sayısı == 5) Depo.Yaz("Talepler/SeriNo" + i, i, 1); //iskonto

                for (int ii = 1; ii <= örnek_iş_sayısı; ii++)
                {
                    Depo.Yaz("Talepler/SeriNo" + i + "/" + t.Yazıya(), "Örnek İş Türü " + ii, 0); //iş türü adı
                    Depo.Yaz("Talepler/SeriNo" + i + "/" + t.Yazıya(), i * ii * 10, 2); //ücreti

                    t = t.AddDays(1);
                }
            }
            Depo.Yaz("Ödeme", t, 1);
            Depo.Yaz("Ödeme", "Fatura No : ASDF123466", 2);
            Depo.Yaz("Ödeme/Alt Toplam", 98765.43);
            Depo.Yaz("Ödeme/Alt Toplam", 10, 1);
            Depo.Yaz("Ödeme/İlave Ödeme", "Örnek İlave ödeme açıklaması", 0);
            Depo.Yaz("Ödeme/İlave Ödeme", 2109.87, 1);

            return Depo;
        }
        public static Depo_ YazdırmayaHazırla_İşTürüAdları(Depo_ Depo)
        {
            IDepo_Eleman it_leri = Tablo_Dal(null, TabloTürü.İşTürleri, "İş Türleri");
            if (it_leri == null) return Depo;

            Depo = new Depo_(Depo.YazıyaDönüştür());

            foreach (IDepo_Eleman sn in Depo.Bul("Talepler", true).Elemanları)
            {
                foreach (IDepo_Eleman iş in sn.Elemanları)
                {
                    IDepo_Eleman it_ma = it_leri.Bul(iş[0] + "/Müşteri için adı");
                    if (it_ma == null || it_ma[0].BoşMu()) continue;

                    iş[0] = it_ma[0]; //iş türü adı
                }
            }

            return Depo;
        }

        public static Depo_ Tablo(string MüşteriVeyaMalzeme, TabloTürü Tür, bool YoksaOluştur = false, string EkTanım = null)
        {
            Depo_ depo = null;
            bool KontrolEdildi = false;

            if (Tür == TabloTürü.MalzemeKullanımDetayı)
            {
                MalzemeKullanımDetayı_ m;
                if (MalzemeKullanımDetayları == null) MalzemeKullanımDetayları = new Dictionary<string, MalzemeKullanımDetayı_>();

                if (!MalzemeKullanımDetayları.TryGetValue(MüşteriVeyaMalzeme, out m))
                {
                    m = new MalzemeKullanımDetayı_(MüşteriVeyaMalzeme);
                    m.KlasörAdı = Tablo_Dal(null, TabloTürü.Malzemeler, "Malzemeler/" + MüşteriVeyaMalzeme + "/Klasör KuDe", true)[0];
                    if (m.KlasörAdı.BoşMu())
                    {
                        m.KlasörAdı = Path.GetRandomFileName();
                        while (Directory.Exists(Ortak.Klasör_Banka_MalzemeKullanımDetayları + m.KlasörAdı)) m.KlasörAdı = Path.GetRandomFileName();

                        Tablo_Dal(null, TabloTürü.Malzemeler, "Malzemeler/" + MüşteriVeyaMalzeme + "/Klasör KuDe")[0] = m.KlasörAdı;
                    }
                    MalzemeKullanımDetayları.Add(MüşteriVeyaMalzeme, m);
                }

                string İlgili_Kls = "MaKD\\" + m.KlasörAdı + "\\"; //Ortak.Banka klasörünü başlanıç olarak kabul ediyor

                if (EkTanım == null)
                {
                    if (m.DevamEden == null)
                    {
                        İlgili_Kls += "MaKD_A";

                        if (!Depo_DosyaVarMı(İlgili_Kls))
                        {
                            if (!YoksaOluştur) return null;
                        }

                        m.DevamEden = Depo_Aç(İlgili_Kls);
                    }
                    else KontrolEdildi = true;

                    depo = m.DevamEden;
                }
            }
            else if (MüşteriVeyaMalzeme == null)
            {
                switch (Tür)
                {
                    case TabloTürü.Ayarlar:
                        if (Ayarlar == null)
                        {
                            if (!Depo_DosyaVarMı("Ay"))
                            {
                                if (!YoksaOluştur) return null;

                                Ayarlar = Depo_Aç("Ay");
                                Ayarlar["Uygulama Kimliği", 0] = DoğrulamaKodu.Üret.Yazıdan(DateTime.Now.Yazıya() + Ortak.Klasör_Banka); //yedekleme işleminde tarama aşamasında aynı uygulamanın dosyalarının kullanıldığından emin olmak için
                                Ayarlar["Son Banka Kayıt"].İçeriği = new string[] { Kendi.BilgisayarAdı + " " + Kendi.KullanıcıAdı, DateTime.Now.Yazıya(), Sürüm };
                            }
                            else Ayarlar = Depo_Aç("Ay");
                        }
                        else KontrolEdildi = true;

                        depo = Ayarlar;
                        break;

                    case TabloTürü.İşTürleri:
                        if (İşTürleri == null)
                        {
                            if (!Depo_DosyaVarMı("İt"))
                            {
                                if (!YoksaOluştur) return null;
                            }

                            İşTürleri = Depo_Aç("İt");
                        }
                        else KontrolEdildi = true;

                        depo = İşTürleri;
                        break;

                    case TabloTürü.Malzemeler:
                        if (Malzemeler == null)
                        {
                            if (!Depo_DosyaVarMı("Ma"))
                            {
                                if (!YoksaOluştur) return null;

                                Malzemeler = Depo_Aç("Ma");
                                Malzemeler.Yaz("Son Aylık Sayım", DateTime.Now);
                            }
                            else Malzemeler = Depo_Aç("Ma");
                        }
                        else KontrolEdildi = true;

                        depo = Malzemeler;
                        break;

                    case TabloTürü.Kullanıcılar:
                        if (Kullanıcılar == null)
                        {
                            if (!Depo_DosyaVarMı("Ku", Ortak.Klasör_KullanıcıDosyaları_Ayarlar))
                            {
                                if (!YoksaOluştur) return null;
                            }

                            Kullanıcılar = Depo_Aç("Ku", Ortak.Klasör_KullanıcıDosyaları_Ayarlar);
                        }
                        else KontrolEdildi = true;

                        depo = Kullanıcılar;
                        break;

                    case TabloTürü.Takvim:
                        if (Takvim == null)
                        {
                            if (!Depo_DosyaVarMı("Ta"))
                            {
                                if (!YoksaOluştur) return null;
                            }

                            Takvim = Depo_Aç("Ta");
                        }
                        else KontrolEdildi = true;

                        depo = Takvim;
                        break;

                    case TabloTürü.KorumalıAlan:
                        if (KorumalıAlan == null)
                        {
                            if (!Depo_DosyaVarMı("KoAl"))
                            {
                                if (!YoksaOluştur) return null;
                            }

                            KorumalıAlan = Depo_Aç("KoAl");
                        }
                        else KontrolEdildi = true;

                        depo = KorumalıAlan;
                        break;

                    default:
                        return null;
                }
            }
            else
            {
                Müşteri_ m;
                if (Müşteriler == null) Müşteriler = new Dictionary<string, Müşteri_>();

                if (!Müşteriler.TryGetValue(MüşteriVeyaMalzeme, out m))
                {
                    m = new Müşteri_(MüşteriVeyaMalzeme);
                    m.KlasörAdı = Tablo_Dal(null, TabloTürü.Ayarlar, "Müşteriler/" + MüşteriVeyaMalzeme, true)[0];
                    if (m.KlasörAdı.BoşMu()) throw new Exception("Banka/" + Tür.ToString() + "/" + MüşteriVeyaMalzeme + "/Klasör (" + m.KlasörAdı + ") hatalı");
                    Müşteriler.Add(MüşteriVeyaMalzeme, m);
                }

                string İlgili_Kls = "Mü\\" + m.KlasörAdı + "\\"; //Ortak.Banka klasörünü başlanıç olarak kabul ediyor

                switch (Tür)
                {
                    case TabloTürü.DevamEden:
                        if (m.DevamEden == null)
                        {
                            İlgili_Kls += "Mü_A";

                            if (!Depo_DosyaVarMı(İlgili_Kls))
                            {
                                if (!YoksaOluştur) return null;
                            }

                            m.DevamEden = Depo_Aç(İlgili_Kls);
                        }
                        else KontrolEdildi = true;

                        depo = m.DevamEden;
                        break;

                    case TabloTürü.Ayarlar:
                        if (m.Ayarlar == null)
                        {
                            İlgili_Kls += "Mü_Ay";

                            if (!Depo_DosyaVarMı(İlgili_Kls))
                            {
                                if (!YoksaOluştur) return null;
                            }

                            m.Ayarlar = Depo_Aç(İlgili_Kls);
                        }
                        else KontrolEdildi = true;

                        depo = m.Ayarlar;
                        break;

                    case TabloTürü.Ödemeler:
                        if (m.Ödemeler == null)
                        {
                            İlgili_Kls += "Mü_Öd";

                            if (!Depo_DosyaVarMı(İlgili_Kls))
                            {
                                if (!YoksaOluştur) return null;
                            }

                            m.Ödemeler = Depo_Aç(İlgili_Kls);
                        }
                        else KontrolEdildi = true;

                        depo = m.Ödemeler;
                        break;

                    case TabloTürü.ÖdemeTalepEdildi:
                        if (m.ÖdemeTalepEdildi == null)
                        {
                            m.ÖdemeTalepEdildi = new Dictionary<string, Depo_>();
                        }

                        if (!m.ÖdemeTalepEdildi.TryGetValue(EkTanım, out depo))
                        {
                            İlgili_Kls += "Mü_B\\Mü_B_" + EkTanım;

                            if (!Depo_DosyaVarMı(İlgili_Kls))
                            {
                                if (!YoksaOluştur) return null;
                            }

                            depo = Depo_Aç(İlgili_Kls);
                            m.ÖdemeTalepEdildi.Add(EkTanım, depo);
                        }
                        else KontrolEdildi = true;
                        break;

                    case TabloTürü.Ödendi:
                        if (m.Ödendi == null)
                        {
                            m.Ödendi = new Dictionary<string, Depo_>();
                        }

                        if (!m.Ödendi.TryGetValue(EkTanım, out depo))
                        {
                            İlgili_Kls += "Mü_C\\Mü_C_" + EkTanım;

                            if (!Depo_DosyaVarMı(İlgili_Kls))
                            {
                                if (!YoksaOluştur) return null;
                            }

                            depo = Depo_Aç(İlgili_Kls);
                            m.Ödendi.Add(EkTanım, depo);
                        }
                        else KontrolEdildi = true;
                        break;

                    default:
                        return null;
                }
            }

            if (!KontrolEdildi)
            {
                //dosya adı ve başlık uyumluluğu kontrolü
                string tür = depo.Oku("Tür");
                if (string.IsNullOrEmpty(tür)) depo["Tür"].İçeriği = new string[] { Tür.ToString(), MüşteriVeyaMalzeme, EkTanım };
                else
                {
                    string tür_isteneen_depo = ("Tür>" + Tür.ToString() + ">" + MüşteriVeyaMalzeme + ">" + EkTanım).TrimEnd('>');
                    tür = depo["Tür"].YazıyaDönüştür(null).TrimEnd('\n');
                    if (tür != tür_isteneen_depo) throw new Exception("Banka/" + MüşteriVeyaMalzeme + "/" + Tür.ToString() + "/" + EkTanım + " anahtarının başlığı (" + tür + ") hatalı");
                }
            }

            return depo;
        }
        public static IDepo_Eleman Tablo_Dal(string MüşteriVeyaMalzeme, TabloTürü Tür, string Dal, bool YoksaOluştur = false, string EkTanım = null)
        {
            Depo_ d = Tablo(MüşteriVeyaMalzeme, Tür, YoksaOluştur, EkTanım);
            if (d == null) return null;

            return d.Bul(Dal, YoksaOluştur);
        }
        public static string[] Dosya_Listele_Müşteri(string Müşteri, bool Ödendi_1_ÖdemeBekleyen_0)
        {
            if (Müşteriler == null) Müşteriler = new Dictionary<string, Müşteri_>();

            Müşteri_ m;
            if (!Müşteriler.TryGetValue(Müşteri, out m))
            {
                m = new Müşteri_(Müşteri);
                m.KlasörAdı = Tablo_Dal(null, TabloTürü.Ayarlar, "Müşteriler/" + Müşteri, true)[0];
                if (m.KlasörAdı.BoşMu()) throw new Exception("Banka/" + Ödendi_1_ÖdemeBekleyen_0.ToString() + "/" + Müşteri + "/Klasör (" + m.KlasörAdı + ") hatalı");
                Müşteriler.Add(Müşteri, m);
            }

            if (Ödendi_1_ÖdemeBekleyen_0)
            {
                if (m.Liste_Ödendi == null)
                {
                    if (!Directory.Exists(Ortak.Klasör_Banka_Müşteriler + m.KlasörAdı + "\\Mü_C")) return new string[0];

                    string[] l = new DirectoryInfo(Ortak.Klasör_Banka_Müşteriler + m.KlasörAdı + "\\Mü_C").GetFiles("Mü_C_*.mup", SearchOption.TopDirectoryOnly)
                        .OrderByDescending(f => D_TarihSaat.Tarihe(Path.GetFileNameWithoutExtension(f.FullName).Substring(5 /*"Mü_C_*/)))
                        .Select(f => f.FullName)
                        .ToArray();

                    m.Liste_Ödendi = new string[l.Length];

                    for (int i = 0; i < l.Length; i++)
                    {
                        m.Liste_Ödendi[i] = Path.GetFileNameWithoutExtension(l[i]).Substring(5 /*Mü_C_*/);
                    }
                }

                return m.Liste_Ödendi;
            }
            else
            {
                if (m.Liste_ÖdemeTalepEdildi == null)
                {
                    if (!Directory.Exists(Ortak.Klasör_Banka_Müşteriler + m.KlasörAdı + "\\Mü_B")) return new string[0];

                    string[] l = new DirectoryInfo(Ortak.Klasör_Banka_Müşteriler + m.KlasörAdı + "\\Mü_B").GetFiles("Mü_B_*.mup", SearchOption.TopDirectoryOnly)
                        .OrderByDescending(f => D_TarihSaat.Tarihe(Path.GetFileNameWithoutExtension(f.FullName).Substring(5 /*"Mü_B_*/)))
                        .Select(f => f.FullName)
                        .ToArray();

                    m.Liste_ÖdemeTalepEdildi = new string[l.Length];

                    for (int i = 0; i < l.Length; i++)
                    {
                        m.Liste_ÖdemeTalepEdildi[i] = Path.GetFileNameWithoutExtension(l[i]).Substring(5 /*"Mü_B_*/);
                    }
                }

                return m.Liste_ÖdemeTalepEdildi;
            }
        }

        public static IDepo_Eleman Ayarlar_Genel(string Dal, bool YoksaOluştur = false)
        {
            return Tablo_Dal(null, TabloTürü.Ayarlar, Dal, YoksaOluştur);
        }
        public static IDepo_Eleman Ayarlar_BilgisayarVeKullanıcı(string Dal, bool YoksaOluştur = false)
        {
            Dal = "Bilgisayarlar/" + Kendi.BilgisayarAdı + " " + Kendi.KullanıcıAdı + (string.IsNullOrEmpty(Dal) ? null : "/" + Dal);
            return Tablo_Dal(null, TabloTürü.Ayarlar, Dal, YoksaOluştur);
        }
        public static IDepo_Eleman Ayarlar_Müşteri(string Müşteri, string Dal, bool YoksaOluştur = false)
        {
            return Tablo_Dal(Müşteri, TabloTürü.Ayarlar, Dal, YoksaOluştur);
        }
        public static IDepo_Eleman Ayarlar_Kullanıcı(string Sayfa, string Dal)
        {
            return Tablo_Dal(null, TabloTürü.Kullanıcılar, Sayfa + (Dal.DoluMu() ? "/" + Dal : null), true);
        }

        public static List<string> Müşteri_Listele(bool GizlilerDahil = false)
        {
            List<string> l = new List<string>();
            List<string> l_gizli = new List<string>();

            IDepo_Eleman d = Ayarlar_Genel("Müşteriler");
            if (d == null || d.Elemanları.Length == 0) return l;

            foreach (IDepo_Eleman e in d.Elemanları)
            {
                if (e.İçiBoşOlduğuİçinSilinecek) continue;

                if (e.Adı.StartsWith(".:Gizli:. "))
                {
                    if (GizlilerDahil) l_gizli.Add(e.Adı);
                }
                else l.Add(e.Adı);
            }

            l.Sort();
            l_gizli.Sort();
            l.AddRange(l_gizli);

            return l;
        }
        public static void Müşteri_Ekle(string Adı)
        {
            IDepo_Eleman müş = Ayarlar_Genel("Müşteriler/" + Adı, true);

            string kls_müş = Path.GetRandomFileName();
            while (Directory.Exists(Ortak.Klasör_Banka_Müşteriler + kls_müş)) kls_müş = Path.GetRandomFileName();
            if (!Klasör.Oluştur(Ortak.Klasör_Banka_Müşteriler + kls_müş)) throw new Exception("Klasör oluşturulamadı " + Ortak.Klasör_Banka_Müşteriler + kls_müş);

            müş.Yaz(null, kls_müş);
        }
        public static void Müşteri_Sil(string Adı)
        {
            IDepo_Eleman d = Ayarlar_Genel("Müşteriler/" + Adı);
            if (d != null)
            {
                if (!Klasör.Sil(Ortak.Klasör_Banka_Müşteriler + d[0]))
                {
                    throw new Exception("Klasör silinemedi." + Environment.NewLine + Ortak.Klasör_Banka_Müşteriler + d[0]);
                }

                if (Müşteriler.ContainsKey(Adı)) Müşteriler.Remove(Adı);

                d.Sil(null);
            }
        }
        public static bool Müşteri_MevcutMu(string Adı)
        {
            return Adı.DoluMu(true) && Ayarlar_Genel("Müşteriler/" + Adı) != null;
        }
        public static void Müşteri_YenidenAdlandır(string Eski, string Yeni)
        {
            Değişiklikler_TamponuSıfırla();

            IDepo_Eleman müş = Ayarlar_Genel("Müşteriler/" + Eski);
            if (müş != null)
            {
                if (Directory.Exists(Ortak.Klasör_Banka_Müşteriler + müş[0]))
                {
                    string[] dsy_lar = Directory.GetFiles(Ortak.Klasör_Banka_Müşteriler + müş[0], "*.mup", SearchOption.AllDirectories);
                    if (dsy_lar != null)
                    {
                        foreach (string dsy in dsy_lar)
                        {
                            string d = dsy.Substring(Ortak.Klasör_Banka.Length); //Ortak.Banka klasörünü başlanıç olarak kabul ediyor
                            d = d.Remove(d.LastIndexOf('.'));

                            Depo_ gecici = Depo_Aç(d);
                            gecici["Tür", 1] = Yeni;
                            Depo_Kaydet(d, gecici);
                        }
                    }
                }

                müş.Adı = Yeni;
            }

            if (Müşteriler != null && Müşteriler.ContainsKey(Eski)) Müşteriler.Remove(Eski);
        }
        public static double Müşteri_ÖnÖdemeMiktarı(string Adı)
        {
            IDepo_Eleman müş = Tablo_Dal(Adı, TabloTürü.Ödemeler, "Mevcut Ön Ödeme");
            if (müş != null) return müş.Oku_Sayı(null);

            return 0;
        }
        public static void Müşteri_ÖdemeAl(string Adı, Ortak.İşTakip_TeslimEdildi_İşSeç_Seç Detaylar, List<string> Seri_No_lar, string Notlar)
        {
            DateTime t = DateTime.Now;

            //Ödemeler tablosuna kayıt
            Depo_ Tablo_Ödemeler = Tablo(Adı, TabloTürü.Ödemeler, true);
            Tablo_Ödemeler["Ödemeler/" + t.Yazıya()].İçeriği = new string[]
            {
                Detaylar.MevcutÖnÖdeme.Yazıya(), Detaylar.AlınanÖdeme.Yazıya(),
                (Detaylar.ToplamHarcama + Detaylar.ToplamKDV).Yazıya(),
                Notlar
            };
            Tablo_Ödemeler["Mevcut Ön Ödeme"].Yaz(null, Detaylar.İşlemSonrasıÖnÖdeme);

            //müşteri ayarları tablosuna kayıt
            IDepo_Eleman müş = Ayarlar_Müşteri(Adı, "Sayfa/Teslim Edildi", true);
            müş["KDV"].Yaz(null, Detaylar.KDV_Oranı > 0);

            //Ödendi tablosuna kayıt
            Depo_ yeni_tablo = Tablo(Adı, TabloTürü.Ödendi, true, t.Yazıya(ArgeMup.HazirKod.Dönüştürme.D_TarihSaat.Şablon_DosyaAdı2));
            IDepo_Eleman yeni_tablodaki_işler = yeni_tablo.Bul("Talepler", true);
            IDepo_Eleman eski_tablodaki_işler = Tablo_Dal(Adı, TabloTürü.DevamEden, "Talepler");

            foreach (string sn in Seri_No_lar)
            {
                IDepo_Eleman seri_noya_ait_detaylar = eski_tablodaki_işler.Bul(sn); //bir talep
                yeni_tablodaki_işler.Ekle(null, seri_noya_ait_detaylar.YazıyaDönüştür(null));
                seri_noya_ait_detaylar.Sil(null);
            }

            yeni_tablo["Ödeme"].İçeriği = new string[] { t.Yazıya(), t.Yazıya(), Notlar };
            yeni_tablo["Ödeme/Alt Toplam"].İçeriği = new string[] { Detaylar.ToplamHarcama.Yazıya(), Detaylar.KDV_Oranı > 0 ? Detaylar.KDV_Oranı.Yazıya() : null };
            yeni_tablo["Ödeme/Ön Ödeme"].İçeriği = new string[] { Detaylar.MevcutÖnÖdeme.Yazıya(), Detaylar.AlınanÖdeme.Yazıya() };

            //Ödendi dosyasının kaydı
            müş = Ayarlar_Genel("Müşteriler/" + Adı);
            Depo_Kaydet("Mü\\" + müş[0] + "\\Mü_C\\Mü_C_" + t.Yazıya(ArgeMup.HazirKod.Dönüştürme.D_TarihSaat.Şablon_DosyaAdı2), yeni_tablo);
        }
        public static void Müşteri_Ödemeler_TablodaGöster(string Adı, DataGridView Tablo)
        {
            Tablo.Rows.Clear();
            if (Tablo.SortedColumn != null)
            {
                DataGridViewColumn col = Tablo.SortedColumn;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                col.SortMode = DataGridViewColumnSortMode.Automatic;
            }

            IDepo_Eleman liste = Tablo_Dal(Adı, TabloTürü.Ödemeler, "Ödemeler");
            if (liste == null || liste.Elemanları.Length == 0) return;

            IDepo_Eleman[] Ödemeler = liste.Elemanları;
            Ortak.Gösterge.Başlat("Liste Dolduruluyor", true, null, Ödemeler.Length);
            for (int i = Ödemeler.Length - 1; i >= 0 && Ortak.Gösterge.Çalışsın; i--)
            {
                Ortak.Gösterge.İlerleme = 1;

                //sondan başa
                int y = Tablo.RowCount;
                Tablo.RowCount++;

                double MevcutÖnÖdeme = Ödemeler[i][0].NoktalıSayıya();
                double AlınanÖdeme = Ödemeler[i][1].NoktalıSayıya();
                double GenelToplam = Ödemeler[i][2].NoktalıSayıya();
                double İşlemSonrasıÖnÖdeme = MevcutÖnÖdeme + AlınanÖdeme - GenelToplam;

                Tablo[0, y].Value = Yazdır_Tarih(Ödemeler[i].Adı); //tarih
                Tablo[0, y].ToolTipText = Ödemeler[i].Adı;
                Tablo[1, y].Value = Yazdır_Ücret(MevcutÖnÖdeme);
                Tablo[2, y].Value = Yazdır_Ücret(AlınanÖdeme);
                Tablo[3, y].Value = Yazdır_Ücret(GenelToplam);
                Tablo[4, y].Value = Yazdır_Ücret(İşlemSonrasıÖnÖdeme);
                Tablo[5, y].Value = Ödemeler[i][3]; //Notlar

                if (MevcutÖnÖdeme < 0) Tablo[1, y].Style.BackColor = System.Drawing.Color.Salmon;
                if (İşlemSonrasıÖnÖdeme < 0) Tablo[4, y].Style.BackColor = System.Drawing.Color.Salmon;
            }

            Tablo.ClearSelection();
            Ortak.Gösterge.Bitir();
        }
        public static bool Müşteri_ÖdemeTalebi_GeciciDetaylarıEkle(string Müşteri, ref IDepo_Eleman ÖdemeDalı)
        {
            string[] Ödendi_ler = Dosya_Listele_Müşteri(Müşteri, true);
            if (Ödendi_ler.Length == 0) return false;

            IDepo_Eleman ÖdendiDokümanı_ÖdemeDalı = Tablo_Dal(Müşteri, TabloTürü.Ödendi, "Ödeme", false, Ödendi_ler[0]);
            if (ÖdendiDokümanı_ÖdemeDalı == null) return false;

            _Talep_Ayıkla_ÖdemeDalı o = new _Talep_Ayıkla_ÖdemeDalı(ÖdendiDokümanı_ÖdemeDalı);
            if (!o.ÖnÖdeme_İşlemiVarmı) return false;

            ÖdemeDalı = ÖdemeDalı.Bul(null, false, true); //Bağımsız kopya
            ÖdemeDalı["Müşteri_ÖdemeTalebi_GeciciDetaylarıEkle"].İçeriği = new string[] { o.ÖnÖdeme_AlınanÖdeme.Yazıya(), o.Genel_Toplam.Yazıya(), o.ÖnÖdeme_MevcutÖnÖdeme.Yazıya(), Müşteri_ÖnÖdemeMiktarı(Müşteri).Yazıya() };
            return true;
        }
        public static void Müşteri_ÖdemeTalebi_GeciciDetaylarıEkle(string Müşteri, ref Depo_ Depo)
        {
            string[] Ödendi_ler = Dosya_Listele_Müşteri(Müşteri, true);
            if (Ödendi_ler.Length == 0) return;

            IDepo_Eleman ÖdendiDokümanı_ÖdemeDalı = Tablo_Dal(Müşteri, TabloTürü.Ödendi, "Ödeme", false, Ödendi_ler[0]);
            if (ÖdendiDokümanı_ÖdemeDalı == null) return;

            _Talep_Ayıkla_ÖdemeDalı o = new _Talep_Ayıkla_ÖdemeDalı(ÖdendiDokümanı_ÖdemeDalı);
            if (!o.ÖnÖdeme_İşlemiVarmı) return;

            Depo = new Depo_(Depo.YazıyaDönüştür()); //Bağımsız kopya
            IDepo_Eleman ÖdemeDalı = Depo.Bul("Ödeme");
            ÖdemeDalı["Müşteri_ÖdemeTalebi_GeciciDetaylarıEkle"].İçeriği = new string[] { o.ÖnÖdeme_AlınanÖdeme.Yazıya(), o.Genel_Toplam.Yazıya(), o.ÖnÖdeme_MevcutÖnÖdeme.Yazıya(), Müşteri_ÖnÖdemeMiktarı(Müşteri).Yazıya() };
        }

        public static List<string> İşTürü_Listele()
        {
            IDepo_Eleman d = Tablo_Dal(null, TabloTürü.İşTürleri, "İş Türleri");
            List<string> l = new List<string>();
            if (d == null || d.Elemanları.Length == 0) return l;

            foreach (IDepo_Eleman e in d.Elemanları)
            {
                if (e.İçiBoşOlduğuİçinSilinecek) continue;

                l.Add(e.Adı);
            }

            return l;
        }
        public static void İşTürü_Ekle(string Adı)
        {
            Tablo_Dal(null, TabloTürü.İşTürleri, "İş Türleri/" + Adı, true)[0] = ".";
        }
        public static void İşTürü_Sil(string Adı)
        {
            IDepo_Eleman d = Tablo_Dal(null, TabloTürü.İşTürleri, "İş Türleri/" + Adı);
            if (d != null)
            {
                //iştürü tablosundan silinmesi
                d.Sil(null);

                //müşteri ücretlerinden silinmesi
                List<string> müş_ler = Müşteri_Listele(true);
                foreach (string müş in müş_ler)
                {
                    d = Ayarlar_Müşteri(müş, "Bütçe/" + Adı);
                    if (d != null) d.Sil(null);
                }
            }
        }
        public static bool İşTürü_MevcutMu(string Adı)
        {
            return !string.IsNullOrWhiteSpace(Adı) && Tablo_Dal(null, TabloTürü.İşTürleri, "İş Türleri/" + Adı) != null;
        }
        public static void İşTürü_YenidenAdlandır(string Eski, string Yeni)
        {
            Değişiklikler_TamponuSıfırla();

            IDepo_Eleman iş_türü = Tablo_Dal(null, TabloTürü.İşTürleri, "İş Türleri/" + Eski);
            if (iş_türü != null) iş_türü.Adı = Yeni;

            if (Directory.Exists(Ortak.Klasör_Banka_MalzemeKullanımDetayları))
            {
                string[] dsy_lar = Directory.GetFiles(Ortak.Klasör_Banka_MalzemeKullanımDetayları, "*.mup", SearchOption.AllDirectories);
                if (dsy_lar != null && dsy_lar.Length > 0)
                {
                    foreach (string dsy in dsy_lar)
                    {
                        Ortak.Gösterge.İlerleme = 1;
                        bool _ = Ortak.Gösterge.Çalışsın;

                        string d_adı = dsy.Substring(Ortak.Klasör_Banka.Length);
                        d_adı = d_adı.Remove(d_adı.Length - 4);
                        Depo_ depo = Depo_Aç(d_adı);
                        IDepo_Eleman İşlemler = depo.Bul("İşlemler");
                        if (İşlemler != null && İşlemler.Elemanları.Length > 0)
                        {
                            foreach (IDepo_Eleman sn in İşlemler.Elemanları)
                            {
                                foreach (IDepo_Eleman işlem in sn.Elemanları)
                                {
                                    if (işlem[0] == Eski) işlem[0] = Yeni;
                                }
                            }

                            Depo_Kaydet(d_adı, depo);
                        }
                    }
                }
            }

            if (Directory.Exists(Ortak.Klasör_Banka_Müşteriler))
            {
                string[] dsy_lar = Directory.GetFiles(Ortak.Klasör_Banka_Müşteriler, "Mü_Ay.mup", SearchOption.AllDirectories);
                if (dsy_lar != null && dsy_lar.Length > 0)
                {
                    foreach (string dsy in dsy_lar)
                    {
                        Ortak.Gösterge.İlerleme = 1;
                        bool _ = Ortak.Gösterge.Çalışsın;

                        string d_adı = dsy.Substring(Ortak.Klasör_Banka.Length);
                        d_adı = d_adı.Remove(d_adı.Length - 4);
                        Depo_ depo = Depo_Aç(d_adı);
                        IDepo_Eleman Bütçe = depo.Bul("Bütçe");
                        if (Bütçe != null && Bütçe.Elemanları.Length > 0)
                        {
                            foreach (IDepo_Eleman it in Bütçe.Elemanları)
                            {
                                if (it.Adı == Eski) it.Adı = Yeni;
                            }

                            Depo_Kaydet(d_adı, depo);
                        }
                    }
                }

                dsy_lar = Directory.GetFiles(Ortak.Klasör_Banka_Müşteriler, "Mü_A.mup", SearchOption.AllDirectories);
                if (dsy_lar != null && dsy_lar.Length > 0)
                {
                    foreach (string dsy in dsy_lar)
                    {
                        Ortak.Gösterge.İlerleme = 1;
                        bool _ = Ortak.Gösterge.Çalışsın;

                        string d_adı = dsy.Substring(Ortak.Klasör_Banka.Length);
                        d_adı = d_adı.Remove(d_adı.Length - 4);
                        Depo_ depo = Depo_Aç(d_adı);
                        IDepo_Eleman Talepler = depo.Bul("Talepler");
                        if (Talepler != null && Talepler.Elemanları.Length > 0)
                        {
                            foreach (IDepo_Eleman sn in Talepler.Elemanları)
                            {
                                foreach (IDepo_Eleman it in sn.Elemanları)
                                {
                                    if (it[0] == Eski) it[0] = Yeni;
                                }
                            }

                            Depo_Kaydet(d_adı, depo);
                        }
                    }
                }

                dsy_lar = Directory.GetFiles(Ortak.Klasör_Banka_Müşteriler, "Mü_B_*.mup", SearchOption.AllDirectories);
                if (dsy_lar != null && dsy_lar.Length > 0)
                {
                    foreach (string dsy in dsy_lar)
                    {
                        Ortak.Gösterge.İlerleme = 1;
                        bool _ = Ortak.Gösterge.Çalışsın;

                        string d_adı = dsy.Substring(Ortak.Klasör_Banka.Length);
                        d_adı = d_adı.Remove(d_adı.Length - 4);
                        Depo_ depo = Depo_Aç(d_adı);
                        IDepo_Eleman Talepler = depo.Bul("Talepler");
                        if (Talepler != null && Talepler.Elemanları.Length > 0)
                        {
                            foreach (IDepo_Eleman sn in Talepler.Elemanları)
                            {
                                foreach (IDepo_Eleman it in sn.Elemanları)
                                {
                                    if (it[0] == Eski) it[0] = Yeni;
                                }
                            }

                            Depo_Kaydet(d_adı, depo);
                        }
                    }
                }
            }
        }
        public static void İşTürü_Malzemeler_TablodaGöster(DataGridView Tablo, string İşTürü, out string MüşteriyeGösterilecekOlanAdı, out string Notlar)
        {
            Tablo.Rows.Clear();
            if (Tablo.SortedColumn != null)
            {
                DataGridViewColumn col = Tablo.SortedColumn;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                col.SortMode = DataGridViewColumnSortMode.Automatic;
            }

            MüşteriyeGösterilecekOlanAdı = null;
            Notlar = null;
            IDepo_Eleman d = Tablo_Dal(null, TabloTürü.İşTürleri, "İş Türleri/" + İşTürü);
            if (d == null) return;

            MüşteriyeGösterilecekOlanAdı = d.Oku("Müşteri için adı");
            Notlar = d.Oku("Notlar");

            d = d.Bul("Malzemeler");
            if (d == null) return;

            List<string> mlzler = Malzeme_Listele();

            foreach (IDepo_Eleman e in d.Elemanları)
            {
                int y = Tablo.RowCount - 1;
                Tablo.RowCount++;

                Tablo[0, y].Value = e.Adı;
                Tablo[1, y].Value = e[0];
                Tablo[2, y].Value = Malzeme_Birimi(e.Adı);

                if (!mlzler.Contains(e.Adı))
                {
                    Tablo[0, y].Style.BackColor = System.Drawing.Color.Salmon;
                    Tablo[0, y].ToolTipText = "Bu malzeme artık kullanılmıyor.";
                }
            }

            Tablo.ClearSelection();
        }
        public static void İşTürü_Malzemeler_Kaydet(string İşTürü, List<string> Malzemeler, List<string> Miktarlar, string MüşteriyeGösterilecekOlanAdı, string Notlar)
        {
            IDepo_Eleman d = Tablo_Dal(null, TabloTürü.İşTürleri, "İş Türleri/" + İşTürü, true);
            d.Yaz("Müşteri için adı", MüşteriyeGösterilecekOlanAdı);
            d.Yaz("Notlar", Notlar);
            d.Sil("Malzemeler");

            for (int i = 0; i < Malzemeler.Count; i++)
            {
                d.Yaz("Malzemeler/" + Malzemeler[i], Miktarlar[i]);
            }
        }

        public static void Malzeme_KritikMiktarKontrolü()
        {
            IDepo_Eleman d_malzemeler = Tablo_Dal(null, TabloTürü.Malzemeler, "Malzemeler");
            if (d_malzemeler == null) return;

            DateTime t_okunan = Tablo_Dal(null, TabloTürü.Malzemeler, "Son Aylık Sayım", true).Oku_TarihSaat(null);
            int fark_tarih = (int)(DateTime.Now - t_okunan).TotalDays / 30; //son kontrolden sonra geçen ay sayısı

            foreach (IDepo_Eleman malzeme in d_malzemeler.Elemanları)
            {
                double mevcut = malzeme.Oku_Sayı(null, 0, 0);
                double uyarıvermemiktarı = malzeme.Oku_Sayı(null, 0, 2);

                if (uyarıvermemiktarı > 0 && mevcut <= uyarıvermemiktarı) Ortak.Gösterge_UyarıVerenMalzemeler[malzeme.Adı] = mevcut.Yazıya() + " " + malzeme[1];
                else if (Ortak.Gösterge_UyarıVerenMalzemeler.ContainsKey(malzeme.Adı)) Ortak.Gösterge_UyarıVerenMalzemeler[malzeme.Adı] = null;

                if (fark_tarih > 0)
                {
                    IDepo_Eleman m = malzeme.Bul("Tüketim", true);

                    int ek_yapılacak_ay = fark_tarih - 1;
                    for (int i = Malzemeler_GeriyeDönükİstatistik_Ay; i - ek_yapılacak_ay > 1; i--)
                    {
                        m[i] = m[i - 1 - ek_yapılacak_ay];
                    }
                    if (ek_yapılacak_ay > (Malzemeler_GeriyeDönükİstatistik_Ay - 1)) ek_yapılacak_ay = Malzemeler_GeriyeDönükİstatistik_Ay - 1;
                    for (int i = 1; i < ek_yapılacak_ay + 2; i++)
                    {
                        m[i] = "0";
                    }
                }
            }

            if (fark_tarih > 0)
            {
                Tablo_Dal(null, TabloTürü.Malzemeler, "Son Aylık Sayım").Yaz(null, DateTime.Now);
                Değişiklikleri_Kaydet(null);
            }
        }
        public static List<string> Malzeme_Listele(bool SadeceDetayıOlanları = false)
        {
            IDepo_Eleman d_malzemeler = Tablo_Dal(null, TabloTürü.Malzemeler, "Malzemeler");
            List<string> l = new List<string>();
            if (d_malzemeler == null) return l;

            foreach (IDepo_Eleman e in d_malzemeler.Elemanları)
            {
                if (e.İçiBoşOlduğuİçinSilinecek) continue;
                if (SadeceDetayıOlanları && !e.Oku_Bit(null, false, 3)) continue;

                l.Add(e.Adı);
            }

            return l;
        }
        public static void Malzeme_Ekle(string Adı)
        {
            Tablo_Dal(null, TabloTürü.Malzemeler, "Malzemeler/" + Adı, true)[0] = "0";
        }
        public static void Malzeme_Sil(string Adı)
        {
            IDepo_Eleman d = Tablo_Dal(null, TabloTürü.Malzemeler, "Malzemeler/" + Adı);
            if (d != null) d.Sil(null);
        }
        public static bool Malzeme_MevcutMu(string Adı)
        {
            return !string.IsNullOrWhiteSpace(Adı) && Tablo_Dal(null, TabloTürü.Malzemeler, "Malzemeler/" + Adı) != null;
        }
        public static void Malzeme_YenidenAdlandır(string Eski, string Yeni)
        {
            Değişiklikler_TamponuSıfırla();

            IDepo_Eleman iş_türleri = Tablo_Dal(null, TabloTürü.İşTürleri, "İş Türleri");
            if (iş_türleri != null && iş_türleri.Elemanları.Length > 0)
            {
                foreach (IDepo_Eleman iş_türü in iş_türleri.Elemanları)
                {
                    IDepo_Eleman malzemeler = iş_türü.Bul("Malzemeler");
                    if (malzemeler != null && malzemeler.Elemanları.Length > 0)
                    {
                        foreach (IDepo_Eleman malzeme in malzemeler.Elemanları)
                        {
                            if (malzeme.Adı == Eski) malzeme.Adı = Yeni;
                        }
                    }
                }
            }

            IDepo_Eleman Malzeme = Tablo_Dal(null, TabloTürü.Malzemeler, "Malzemeler/" + Eski);
            if (Malzeme != null)
            {
                IDepo_Eleman tür = Tablo_Dal(Eski, TabloTürü.MalzemeKullanımDetayı, "Tür");
                if (tür != null)
                {
                    tür.Yaz(null, Yeni, 1);
                }

                Malzeme.Adı = Yeni;
            }
        }
        public static void Malzeme_Ayıkla_MalzemeDalı(IDepo_Eleman MalzemeDalı, out string Adı, out double Miktarı, out string Birimi, out double UyarıVermeMiktarı, out bool Detaylı, out string Notlar)
        {
            Adı = MalzemeDalı.Adı;
            Miktarı = MalzemeDalı.Oku_Sayı(null, 0, 0);
            Birimi = MalzemeDalı[1];
            UyarıVermeMiktarı = MalzemeDalı.Oku_Sayı(null, 0, 2);
            Detaylı = MalzemeDalı.Oku_Bit(null, false, 3);
            Notlar = MalzemeDalı.Oku("Notlar");
        }
        public static void Malzeme_Ayıkla_MalzemeDalı_Tüketim(IDepo_Eleman MalzemeDalı, out double Toplam, out DateTime[] Dönemler, out double[] Dönemler_Kullanım)
        {
            Toplam = 0;
            Dönemler = new DateTime[0];
            Dönemler_Kullanım = new double[0];

            IDepo_Eleman TüketimDalı = MalzemeDalı.Bul("Tüketim");
            if (TüketimDalı == null) return;

            Toplam = TüketimDalı.Oku_Sayı(null, 0, 0);
            Dönemler = new DateTime[TüketimDalı.İçeriği.Length - 1 /*toplam*/];
            Dönemler_Kullanım = new double[Dönemler.Length];

            //önceki dönemler
            DateTime t = DateTime.Now;
            for (int i = 0; i < Malzemeler_GeriyeDönükİstatistik_Ay; i++)
            {
                if (string.IsNullOrEmpty(TüketimDalı[i + 1])) break;

                Dönemler[i] = t;
                Dönemler_Kullanım[i] = TüketimDalı.Oku_Sayı(null, 0, i + 1);
               
                t = t.AddMonths(-1);
            }
        }
        public static void Malzeme_TablodaGöster(DataGridView Tablo, string Malzeme, out double Miktarı, out string Birimi, out double UyarıVermeMiktarı, out bool Detaylı, out string Notlar)
        {
            Tablo.Rows.Clear();
            if (Tablo.SortedColumn != null)
            {
                DataGridViewColumn col = Tablo.SortedColumn;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                col.SortMode = DataGridViewColumnSortMode.Automatic;
            }

            Miktarı = 0;
            Birimi = null;
            UyarıVermeMiktarı = 0;
            Detaylı = false;
            Notlar = null;
            IDepo_Eleman d = Tablo_Dal(null, TabloTürü.Malzemeler, "Malzemeler/" + Malzeme);
            if (d == null) return;
            Malzeme_Ayıkla_MalzemeDalı(d, out _, out Miktarı, out Birimi, out UyarıVermeMiktarı, out Detaylı, out Notlar);

            //bu malzemeyi kullanan iş türlerinin bulunması
            int y;
            IDepo_Eleman iş_türleri = Tablo_Dal(null, TabloTürü.İşTürleri, "İş Türleri");
            if (iş_türleri != null)
            {
                foreach (IDepo_Eleman it in iş_türleri.Elemanları)
                {
                    IDepo_Eleman it2 = it.Bul("Malzemeler");
                    if (it2 != null)
                    {
                        foreach (IDepo_Eleman mlz in it2.Elemanları)
                        {
                            if (mlz.Adı == Malzeme)
                            {
                                y = Tablo.RowCount;
                                Tablo.RowCount++;

                                Tablo[0, y].Value = it.Adı;
                                Tablo[1, y].Value = mlz[0];
                                Tablo[0, y].Tag = 0; //Bu satırın bir iş türüne ait olduğunu gösteriyor
                            }
                        }
                    }
                }
            }

            //istatistikleri ekleme
            Malzeme_Ayıkla_MalzemeDalı_Tüketim(d, out double Toplam, out DateTime[] Dönemler, out double[] Dönemler_Kullanım);
            if (Dönemler.Length == 0) return;

            //genel toplam
            y = Tablo.RowCount;
            Tablo.RowCount++;
            Tablo[0, y].Value = "Genel kullanım miktarı";
            Tablo[1, y].Value = Toplam.Yazıya();

            //önceki dönemler
            for (int i = 0; i < Dönemler.Length; i++)
            {
                y = Tablo.RowCount;
                Tablo.RowCount++;
                Tablo[0, y].Value = Dönemler[i].ToString("yyyy MMMM", System.Threading.Thread.CurrentThread.CurrentCulture) + " ayı kullanımı";
                Tablo[1, y].Value = Dönemler_Kullanım[i].Yazıya();
            }

            Tablo.ClearSelection();
        }
        public static void Malzeme_DetaylarıKaydet(string Malzeme, string Mevcut, string Birimi, string UyarıVermeMiktarı, bool Detaylı, string Notlar)
        {
            IDepo_Eleman d = Tablo_Dal(null, TabloTürü.Malzemeler, "Malzemeler/" + Malzeme);

            if (Detaylı != d.Oku_Bit(null, false, 3))
            {
                //artık detaylı depo kaydı istenmiyor
                IDepo_Eleman detay = Tablo_Dal(Malzeme, TabloTürü.MalzemeKullanımDetayı, "İşlemler/-", true);
                detay[0] = "Siz";
                detay[DateTime.Now.AddMilliseconds(-2).Yazıya()].İçeriği = new string[] { "Detay özelliği " + (Detaylı ? "açıldı" : "kapatıldı") + ", mevcut : " + Mevcut + " (önceki " + d[0] + ")" };
            }
            if (Detaylı && d[0] != Mevcut)
            {
                //depo kaydı güncellemesi
                IDepo_Eleman detay = Tablo_Dal(Malzeme, TabloTürü.MalzemeKullanımDetayı, "İşlemler/-", true);
                detay[0] = "Siz";
                detay[DateTime.Now.Yazıya()].İçeriği = new string[] { "Mevcut : " + Mevcut + " (önceki " + d[0] + ")" };
            }

            d[0] = Mevcut;
            d[1] = Birimi;
            d[2] = UyarıVermeMiktarı;
            d.Yaz(null, Detaylı, 3);
            d.Yaz("Notlar", Notlar);

            double UyarıVermeMiktarı_s = UyarıVermeMiktarı.NoktalıSayıya();
            if (UyarıVermeMiktarı_s > 0 && Mevcut.NoktalıSayıya() <= UyarıVermeMiktarı_s) Ortak.Gösterge_UyarıVerenMalzemeler[Malzeme] = Mevcut + " " + Birimi;
            else if (Ortak.Gösterge_UyarıVerenMalzemeler.ContainsKey(Malzeme)) Ortak.Gösterge_UyarıVerenMalzemeler[Malzeme] = null;
        }
        public static string Malzeme_Birimi(string Malzeme)
        {
            IDepo_Eleman d = Tablo_Dal(null, TabloTürü.Malzemeler, "Malzemeler/" + Malzeme);
            if (d == null) return null;

            return d[1];
        }
        public static void Malzeme_İştürüneGöreHareket(List<string> İşTürleri, bool Eksilt, string SeriNo, string Müşteri, string Hasta, List<string> İşGirişTarihleri = null)
        {
            IDepo_Eleman d_malzemeler = Tablo_Dal(null, TabloTürü.Malzemeler, "Malzemeler");
            if (d_malzemeler == null || d_malzemeler.Elemanları.Length == 0) return;

            IDepo_Eleman d_iştürleri = Tablo_Dal(null, TabloTürü.İşTürleri, "İş Türleri");
            if (d_iştürleri == null || d_iştürleri.Elemanları.Length == 0) return;

            DateTime t = DateTime.Now;
            for (int i = 0; i < İşTürleri.Count; i++)
            {
                IDepo_Eleman iştürünün_malzemeleri = d_iştürleri.Bul(İşTürleri[i] + "/Malzemeler");
                if (iştürünün_malzemeleri == null) continue;

                foreach (IDepo_Eleman iştürünün_malzemesi in iştürünün_malzemeleri.Elemanları)
                {
                    IDepo_Eleman malzeme = d_malzemeler.Bul(iştürünün_malzemesi.Adı);
                    if (malzeme == null) continue;

                    double Miktar = iştürünün_malzemesi.Oku_Sayı(null);
                    double Mevcut = malzeme.Oku_Sayı(null, 0, 0);
                    double Kullanım_Toplam = malzeme.Oku_Sayı("Tüketim", 0, 0);
                    double Kullanım_BuAy = malzeme.Oku_Sayı("Tüketim", 0, 1);

                    if (Eksilt)
                    {
                        //Depodaki malzemeyi işe harca
                        malzeme.Yaz("Tüketim", Kullanım_Toplam + Miktar, 0);    //Toplam
                        malzeme.Yaz("Tüketim", Kullanım_BuAy + Miktar, 1);      //BuAy

                        Mevcut -= Miktar;
                        malzeme.Yaz(null, Mevcut, 0);                           //Mevcut
                    }
                    else
                    {
                        //depoya geri iade et
                        malzeme.Yaz("Tüketim", Kullanım_Toplam - Miktar, 0);    //Toplam
                        malzeme.Yaz("Tüketim", Kullanım_BuAy - Miktar, 1);      //BuAy

                        Mevcut += Miktar;
                        malzeme.Yaz(null, Mevcut, 0);                           //Mevcut
                    }

                    double uyarıvermemiktarı = malzeme.Oku_Sayı(null, 0, 2);
                    if (uyarıvermemiktarı > 0 && Mevcut <= uyarıvermemiktarı) Ortak.Gösterge_UyarıVerenMalzemeler[malzeme.Adı] = Mevcut.Yazıya() + " " + malzeme[1];
                    else if (Ortak.Gösterge_UyarıVerenMalzemeler.ContainsKey(malzeme.Adı)) Ortak.Gösterge_UyarıVerenMalzemeler[malzeme.Adı] = null;

                    if (malzeme.Oku_Bit(null, false, 3))                        //Detaylı
                    {
                        IDepo_Eleman detay = Tablo_Dal(malzeme.Adı, TabloTürü.MalzemeKullanımDetayı, "İşlemler/" + SeriNo, true);
                        detay[0] = Müşteri;
                        detay[1] = Hasta;

                        detay[İşGirişTarihleri == null ? t.Yazıya() : İşGirişTarihleri[i]].İçeriği = new string[] { İşTürleri[i], (Eksilt ? null : "-") + iştürünün_malzemesi.Oku(null) };
                        t = t.AddMilliseconds(2);
                    }
                }
            }
        }
        public static void Malzeme_KullanımDetayı_TablodaGöster(DataGridView Tablo, string Malzeme, out string Açıklama)
        {
            Açıklama = null;
            Tablo.Tag = 0;

            Tablo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            Tablo.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            Tablo.Rows.Clear();

            if (Tablo.SortedColumn != null)
            {
                DataGridViewColumn col = Tablo.SortedColumn;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                col.SortMode = DataGridViewColumnSortMode.Automatic;
            }

            IDepo_Eleman detay_lar = Tablo_Dal(Malzeme, TabloTürü.MalzemeKullanımDetayı, "İşlemler");
            if (detay_lar == null || detay_lar.Elemanları.Length == 0) return;
            int y;
            double kullanım = 0, iptal = 0;

            foreach (IDepo_Eleman sn in detay_lar.Elemanları)
            {
                foreach (IDepo_Eleman detay in sn.Elemanları)
                {
                    y = Tablo.RowCount;
                    Tablo.RowCount++;

                    Tablo[0, y].Value = false;                  //seçim kutucuğu
                    Tablo[1, y].Value = sn.Adı;                 //seri no
                    Tablo[2, y].Value = sn[0];                  //müşteri
                    Tablo[3, y].Value = sn[1];                  //hasta
                    Tablo[4, y].Value = Yazdır_Tarih(detay.Adı);//İş giriş tarihi
                    Tablo[4, y].Tag = detay.Adı;
                    Tablo[6, y].Value = detay[0];               //iş türü

                    if (detay[1].BoşMu()) Tablo[10, y].Value = null;  //miktar
                    else
                    {
                        double miktar = detay[1].NoktalıSayıya();

                        if (miktar < 0)
                        {
                            Tablo[10, y].Value = "İptal " + detay[1];
                            iptal += miktar;
                        }
                        else
                        {
                            Tablo[10, y].Value = "Kullanım " + detay[1];
                            kullanım += miktar;
                        }
                    }
                }
            }

            Açıklama = Tablo_Dal(null, TabloTürü.Malzemeler, "Malzemeler/" + Malzeme)[1]; //birimi
            Açıklama = "Kullanım : " + kullanım + " " + Açıklama + Environment.NewLine +
                "İptal : " + iptal + " " + Açıklama + Environment.NewLine +
                "Fark : " + (kullanım + iptal) + " " + Açıklama;

            Tablo.Columns[2].Visible = true;  //iş çıkış tarihi
            Tablo.Columns[5].Visible = false; //iş çıkış tarihi
            Tablo.Columns[7].Visible = false; //tarih teslim
            Tablo.Columns[8].Visible = false; //tarih ödeme talebi
            Tablo.Columns[9].Visible = false; //tarih ödendi

            Tablo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            Tablo.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            Tablo.AutoResizeColumns();

            Tablo.ClearSelection();
            if (Tablo.RowCount > 0) Tablo[0, 0].Value = true; //sayım yapması için
            Tablo.Tag = null;
            if (Tablo.RowCount > 0) Tablo[0, 0].Value = false; //sayım yapması için
        }
        public static void Malzeme_KullanımDetayı_Tablodaki_SeçiliOlanlarıSil(DataGridView Tablo, string Malzeme)
        {
            for (int i = 0; i < Tablo.RowCount; i++)
            {
                if (!(bool)Tablo[0, i].Value) continue;

                IDepo_Eleman detay = Tablo_Dal(Malzeme, TabloTürü.MalzemeKullanımDetayı, "İşlemler/" + Tablo[1, i].Value);
                if (detay == null) throw new Exception(Malzeme + " / MalzemeKullanımDetayı / İşlemler / " + Tablo[1, i].Value + " altında iş bulunamadı");

                IDepo_Eleman detay_alt = detay.Bul((string)Tablo[4, i].Tag);
                if (detay_alt == null) throw new Exception(Malzeme + " / MalzemeKullanımDetayı / İşlemler / " + Tablo[1, i].Value + " / " + (string)Tablo[0, i].Tag + " altında iş bulunamadı");
                detay_alt.Sil(null);

                bool _ = detay.İçiBoşOlduğuİçinSilinecek;
                if (detay.Elemanları.Length == 0) detay.Sil(null);
            }
        }

        public static string SeriNo_Üret()
        {
            //<ay><yıl><no>
            //A:Ocak, B:Şubat, 2023:23, 2035:35, sn
            //Mart 2071 no:1 -> C711
            //Mart 3071 no:6789 -> C716789

            IDepo_Eleman o = Ayarlar_Genel("Seri No", true);
            string yeni_sn = "";
            DateTime t = DateTime.Now;

            if (string.IsNullOrEmpty(o[0]) || o[0] == "ArGeMuP" || string.IsNullOrEmpty(o[1]))
            {
                yeni_sn = "1";
            }
            else
            {
                try
                {
                    if (o[0] != t.Year.ToString())
                    {
                        if (t.Month != 1) throw new Exception();

                        yeni_sn = "1";
                    }
                    else yeni_sn = (int.Parse(o[1]) + 1).ToString();
                }
                catch (Exception)
                {
                    throw new Exception("Banka/Seri No içeriği (" + o.YazıyaDönüştür(null) + ") hatalı");
                }
            }

            o[0] = t.Year.ToString();
            o[1] = yeni_sn;
            yeni_sn = ((char)('A' + t.Month - 1)).ToString() + (t.Year - 2000) + yeni_sn;

            return yeni_sn;
        }

        public static void Ücretler_TablodaGöster(DataGridView Önyüz_Tablo, string Müşteri)
        {
            if (Önyüz_Tablo.SortedColumn != null)
            {
                DataGridViewColumn col = Önyüz_Tablo.SortedColumn;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                col.SortMode = DataGridViewColumnSortMode.Automatic;
            }

            if (Müşteri == null)
            {
                //tüm müşteriler için ortak ücretler
                Önyüz_Tablo.Columns[2].Visible = true; // maliyet sutunu

                IDepo_Eleman İşTürleri = Tablo_Dal(null, TabloTürü.İşTürleri, "İş Türleri", true);

                for (int i = 0; i < Önyüz_Tablo.RowCount; i++)
                {
                    IDepo_Eleman bulunan = İşTürleri.Bul((string)Önyüz_Tablo[0, i].Value + "/Bütçe");

                    if (bulunan != null)
                    {
                        Önyüz_Tablo[1, i].Value = bulunan[0]; //ücret
                        Önyüz_Tablo[2, i].Value = bulunan[1]; //maliyet
                    }
                    else
                    {
                        Önyüz_Tablo[1, i].Value = null;
                        Önyüz_Tablo[2, i].Value = null;
                    }
                }
            }
            else
            {
                //müşteriye ait ücretler
                Önyüz_Tablo.Columns[2].Visible = false; // maliyet sutunu

                IDepo_Eleman Bütçe = Ayarlar_Müşteri(Müşteri, "Bütçe", true);

                for (int i = 0; i < Önyüz_Tablo.RowCount; i++)
                {
                    IDepo_Eleman bulunan = Bütçe.Bul((string)Önyüz_Tablo[0, i].Value);

                    if (bulunan != null)
                    {
                        Önyüz_Tablo[1, i].Value = bulunan[0]; //ücret
                    }
                    else
                    {
                        Önyüz_Tablo[1, i].Value = null;
                    }
                }
            }

            Önyüz_Tablo.ClearSelection();
        }
        public static void Ücretler_TablodakileriKaydet(DataGridView Önyüz_Tablo, string Müşteri)
        {
            if (Müşteri == null)
            {
                //tüm müşteriler için ortak ücretler
                IDepo_Eleman İşTürleri = Tablo_Dal(null, TabloTürü.İşTürleri, "İş Türleri", true);

                for (int i = 0; i < Önyüz_Tablo.RowCount; i++)
                {
                    string iştürü = (string)Önyüz_Tablo[0, i].Value;

                    string ücret = (string)Önyüz_Tablo[1, i].Value;
                    if (!string.IsNullOrEmpty(ücret)) ücret = ücret.NoktalıSayıya().Yazıya();

                    string maliyet = (string)Önyüz_Tablo[2, i].Value;
                    if (!string.IsNullOrEmpty(maliyet)) maliyet = maliyet.NoktalıSayıya().Yazıya();

                    İşTürleri[iştürü + "/Bütçe"].İçeriği = new string[] { ücret, maliyet };
                }
            }
            else
            {
                //müşteriye ait ücretler
                IDepo_Eleman Bütçe = Ayarlar_Müşteri(Müşteri, "Bütçe", true);

                for (int i = 0; i < Önyüz_Tablo.RowCount; i++)
                {
                    string iştürü = (string)Önyüz_Tablo[0, i].Value;

                    string ücret = (string)Önyüz_Tablo[1, i].Value;
                    if (!string.IsNullOrEmpty(ücret)) ücret = ücret.NoktalıSayıya().Yazıya();

                    Bütçe[iştürü].İçeriği = new string[] { ücret };
                }
            }
        }
        public static double Ücretler_BirimÜcret(string Müşteri, string İşTürü)
        {
            string ücret = null;

            //müşteriye özel ücret varmı diye bak
            IDepo_Eleman d = Ayarlar_Müşteri(Müşteri, "Bütçe/" + İşTürü);
            if (d != null) ücret = d[0];

            if (string.IsNullOrEmpty(ücret))
            {
                //tüm müşteriler için ortak ücret
                d = Tablo_Dal(null, TabloTürü.İşTürleri, "İş Türleri/" + İşTürü + "/Bütçe");
                if (d != null) ücret = d[0];
            }

            if (string.IsNullOrEmpty(ücret)) return -1;
            return ücret.NoktalıSayıya();
        }
        public static string Ücretler_BirimÜcret_Detaylı(string Müşteri, string İşTürü)
        {
            double müşteriye_özel = -1, ortak = -1, maliyet = -1;
            IDepo_Eleman d;

            //müşteriye özel ücret varmı diye bak
            if (!string.IsNullOrEmpty(Müşteri))
            {
                d = Ayarlar_Müşteri(Müşteri, "Bütçe/" + İşTürü);
                if (d != null) müşteriye_özel = d.Oku_Sayı(null, -1);
            }

            //tüm müşteriler için ortak ücret
            d = Tablo_Dal(null, TabloTürü.İşTürleri, "İş Türleri/" + İşTürü + "/Bütçe");
            if (d != null) ortak = d.Oku_Sayı(null, -1, 0);

            if (müşteriye_özel < 0 && ortak < 0)
            {
                return "Ücret bilgisi girilmemiş.";
            }

            //maliyet
            if (d != null) maliyet = d.Oku_Sayı(null, -1, 1);

            return "Müşteri : " + Müşteri + Environment.NewLine +
                "İş Türü : " + İşTürü + Environment.NewLine +
                (maliyet < 0 ? null : "Maliyet : " + Yazdır_Ücret(maliyet) + Environment.NewLine) + Environment.NewLine +
                (müşteriye_özel < 0 ? null : "Özel ücret : " + Yazdır_Ücret(müşteriye_özel)) +
                (ortak < 0 ? null : (müşteriye_özel < 0 ? null : Environment.NewLine) + "Ortak ücret : " + Yazdır_Ücret(ortak));
        }
        public static double Ücretler_Maliyet(string İşTürü)
        {
            //tüm müşteriler için ortak ücret
            IDepo_Eleman d = Tablo_Dal(null, TabloTürü.İşTürleri, "İş Türleri/" + İşTürü + "/Bütçe");

            if (d != null) return d.Oku_Sayı(null, 0, 1);
            else return 0;
        }

        public static void Talep_Ekle(string Müşteri, string Hasta, string İskonto, string Notlar, List<string> İşTürleri, List<string> Ücretler, List<string> GirişTarihleri, List<string> ÇıkışTarihleri, string SeriNo = null)
        {
            bool YeniKayıt = false;
            if (string.IsNullOrEmpty(SeriNo))
            {
                YeniKayıt = true;
                SeriNo = SeriNo_Üret();
            }

            IDepo_Eleman sn_dalı = Tablo_Dal(Müşteri, TabloTürü.DevamEden, "Talepler/" + SeriNo, true);
            sn_dalı[0] = Hasta;
            sn_dalı[1] = İskonto;
            sn_dalı[2] = Notlar;
            sn_dalı[3] = null; //teslim edilme tarihi

            IDepo_Eleman silinecekler = sn_dalı.Bul(null, false, true);
            sn_dalı.Sil(null, false, true);

            for (int i = 0; i < İşTürleri.Count; i++)
            {
                sn_dalı.Yaz(GirişTarihleri[i], İşTürleri[i], 0);
                sn_dalı.Yaz(GirişTarihleri[i], ÇıkışTarihleri[i], 1);
                sn_dalı.Yaz(GirişTarihleri[i], Ücretler[i], 2);
                //3 nolu konumda ücret detayı var
            }

            if (YeniKayıt) Malzeme_İştürüneGöreHareket(İşTürleri, true, SeriNo, Müşteri, Hasta, GirişTarihleri); //Depodaki malzemeyi işlere harca
            else
            {
                //farkın bulunması
                IDepo_Eleman eklenecekler = sn_dalı.Bul(null, false, true);

                foreach (IDepo_Eleman biri_sil in silinecekler.Elemanları)
                {
                    foreach (var biri_ekle in eklenecekler.Elemanları)
                    {
                        if (biri_sil.Adı == biri_ekle.Adı && biri_sil[0] == biri_ekle[0]) //giriş tarihi && iş türü
                        {
                            biri_sil.Sil(null);
                            biri_ekle.Sil(null);
                            break;
                        }
                    }
                }

                bool _ = silinecekler.İçiBoşOlduğuİçinSilinecek;
                if (silinecekler.Elemanları.Length > 0)
                {
                    List<string> l = new List<string>();
                    foreach (IDepo_Eleman biri in silinecekler.Elemanları)
                    {
                        l.Add(biri[0]); //iş türü
                    }

                    Malzeme_İştürüneGöreHareket(l, false, SeriNo, Müşteri, Hasta);
                }

                _ = eklenecekler.İçiBoşOlduğuİçinSilinecek;
                if (eklenecekler.Elemanları.Length > 0)
                {
                    List<string> l_it = new List<string>();
                    List<string> l_gt = new List<string>();
                    foreach (IDepo_Eleman biri in eklenecekler.Elemanları)
                    {
                        l_gt.Add(biri.Adı); //iş giriş tarihi
                        l_it.Add(biri[0]);  //iş türü
                    }

                    Malzeme_İştürüneGöreHareket(l_it, true, SeriNo, Müşteri, Hasta, l_gt);
                }
            }
        }
        public static void Talep_Sil(string Müşteri, List<string> Seri_No_lar)
        {
            IDepo_Eleman Talepler = Tablo_Dal(Müşteri, TabloTürü.DevamEden, "Talepler");
            if (Talepler == null || Talepler.Elemanları.Length == 0)
            {
                if (Seri_No_lar != null && Seri_No_lar.Count > 0) throw new Exception(Müşteri + " / Devam Eden / Talepler altında iş bulunamadı");

                return;
            }

            List<string> işler_silinecek = new List<string>();

            foreach (string sn in Seri_No_lar)
            {
                IDepo_Eleman seri_no_dalı = Talepler.Bul(sn);
                if (seri_no_dalı == null) throw new Exception(Müşteri + " / Devam Eden / Talepler / " + sn + " bulunamadı");

                foreach (IDepo_Eleman iş in seri_no_dalı.Elemanları)
                {
                    işler_silinecek.Add(iş[0]); //iş türü
                }

                Malzeme_İştürüneGöreHareket(işler_silinecek, false, sn, Müşteri, seri_no_dalı[0]/*hasta*/); //depoya geri teslim et
                seri_no_dalı.Sil(null);
            }
        }
        public static Banka_Tablo_ Talep_Listele(string Müşteri, TabloTürü Tür, string EkTanım = null)
        {
            IDepo_Eleman Talepler;
            Banka_Tablo_ bt = new Banka_Tablo_(Müşteri);
            bt.Türü = Tür;

            switch (Tür)
            {
                case TabloTürü.DevamEden:
                    Talepler = Tablo_Dal(Müşteri, TabloTürü.DevamEden, "Talepler");
                    if (Talepler != null)
                    {
                        foreach (IDepo_Eleman seri_no_dalı in Talepler.Elemanları)
                        {
                            if (string.IsNullOrEmpty(seri_no_dalı[3 /*teslim tarihi*/])) bt.Talepler.Add(seri_no_dalı);
                        }
                    }
                    break;

                case TabloTürü.TeslimEdildi:
                    Talepler = Tablo_Dal(Müşteri, TabloTürü.DevamEden, "Talepler");
                    if (Talepler != null)
                    {
                        foreach (IDepo_Eleman seri_no_dalı in Talepler.Elemanları)
                        {
                            if (!string.IsNullOrEmpty(seri_no_dalı[3 /*teslim tarihi*/])) bt.Talepler.Add(seri_no_dalı);
                        }

                        if (bt.Talepler.Count > 0) bt.Talepler.Sort(new _Sıralayıcı_SeriNoDalı_TeslimEdildi_EskidenYeniye());
                    }
                    break;

                case TabloTürü.ÖdemeTalepEdildi:
                case TabloTürü.Ödendi:
                    Depo_ depo = Tablo(Müşteri, Tür, false, EkTanım);
                    Talepler = depo.Bul("Talepler");
                    if (Talepler != null) bt.Talepler = Talepler.Elemanları.ToList();
                    bt.Ödeme = depo.Bul("Ödeme");
                    break;

                default:
                    break;
            }

            return bt;
        }
        public static void Talep_İşaretle_DevamEden_MüşteriyeGönderildi(string Müşteri, List<string> SeriNolar)
        {
            IDepo_Eleman Talepler = Tablo_Dal(Müşteri, TabloTürü.DevamEden, "Talepler", true);

            foreach (string SeriNo in SeriNolar)
            {
                IDepo_Eleman seri_no_dalı = Talepler.Bul(SeriNo);
                if (seri_no_dalı == null) throw new Exception(Müşteri + " / Devam Eden / Talepler / " + SeriNo + " bulunamadı");

                IDepo_Eleman iştürü = seri_no_dalı.Elemanları.Last();
                //iş çıkış tarihi
                if (string.IsNullOrEmpty(iştürü.Oku(null, null, 1))) iştürü.Yaz(null, DateTime.Now, 1);
            }
        }
        public static string Talep_İşaretle_DevamEden_TeslimEdilen(string Müşteri, List<string> SeriNolar, bool TeslimEdildi_1_DevamEden_0)
        {
            IDepo_Eleman Talepler = Tablo_Dal(Müşteri, TabloTürü.DevamEden, "Talepler", true);

            foreach (string SeriNo in SeriNolar)
            {
                IDepo_Eleman seri_no_dalı = Talepler.Bul(SeriNo);
                if (seri_no_dalı == null) throw new Exception(Müşteri + " / Devam Eden / Talepler / " + SeriNo + " bulunamadı");

                if (TeslimEdildi_1_DevamEden_0)
                {
                    //Teslim edildi olarak işaretle
                    seri_no_dalı.Yaz(null, DateTime.Now, 3); //tamamlanma tarihi bugün

                    foreach (IDepo_Eleman iştürü in seri_no_dalı.Elemanları)
                    {
                        double ücret = iştürü.Oku_Sayı(null, -1, 2);
                        if (ücret < 0)
                        {
                            //kullanıcı ücret girmemiş
                            iştürü.Yaz(null, null, 3); //girilmediği için eğer önceden kalma varsa sil

                            //hesaplatılacak
                            ücret = Ücretler_BirimÜcret(Müşteri, iştürü[0]);
                            if (ücret < 0)
                            {
                                return Müşteri + " / " + seri_no_dalı.Adı + " / " + iştürü[0] + " için ücret hesaplanamadı" + Environment.NewLine + Environment.NewLine +
                                    "Bilgi için \"Ana Ekran -> Yeni İş Girişi -> Notlar\" elemanı üzerine" + Environment.NewLine +
                                    "fareyi götürüp 1 sn kadar bekleyiniz";
                            }
                        }
                        else
                        {
                            //kullanıcının girdiğini ayrıca not al, ilerde tersine işlem yaparken lazım olacak
                            iştürü.Yaz(null, ücret, 3); //girilmediği için eğer önceden kalma varsa sil
                        }

                        iştürü.Yaz(null, ücret, 2);
                    }
                }
                else
                {
                    //devam eden olarak işaretle
                    seri_no_dalı.Yaz(null, null, 3); //teslim edildi tarihi iptal

                    foreach (IDepo_Eleman iştürü in seri_no_dalı.Elemanları)
                    {
                        iştürü[2] = iştürü[3]; //eğer var ise kullanıcının girdiği değeri geri yükle
                    }
                }
            }

            return null;
        }
        public static void Talep_İşaretle_TeslimEdilen_ÖdemeTalepEdildi(string Müşteri, List<string> Seri_No_lar, string İlaveÖdeme_Açıklama, string İlaveÖdeme_Miktar, bool KDV)
        {
            DateTime t = DateTime.Now;
            Depo_ yeni_tablo = Tablo(Müşteri, TabloTürü.ÖdemeTalepEdildi, true, t.Yazıya(ArgeMup.HazirKod.Dönüştürme.D_TarihSaat.Şablon_DosyaAdı2));
            IDepo_Eleman yeni_tablodaki_işler = yeni_tablo.Bul("Talepler", true);
            IDepo_Eleman eski_tablodaki_işler = Tablo_Dal(Müşteri, TabloTürü.DevamEden, "Talepler");
            double Alt_Toplam = 0;

            foreach (string sn in Seri_No_lar)
            {
                IDepo_Eleman seri_noya_ait_detaylar = eski_tablodaki_işler.Bul(sn); //bir talep
                double iş_toplam = 0;

                foreach (IDepo_Eleman iş in seri_noya_ait_detaylar.Elemanları)
                {
                    double birim_ücret = iş.Oku_Sayı(null, 0, 2); //bir iş
                    if (birim_ücret < 0)
                    {
                        throw new Exception(Müşteri + " / Devam Eden / " + seri_noya_ait_detaylar.Adı + " / " + iş[0] + " ücreti (" + birim_ücret + ") hatalı");
                    }

                    iş_toplam += birim_ücret;
                }

                double iskonto = seri_noya_ait_detaylar.Oku_Sayı(null, 0, 1);
                if (iskonto > 0) iş_toplam -= iş_toplam / 100 * iskonto;
                Alt_Toplam += iş_toplam;

                yeni_tablodaki_işler.Ekle(null, seri_noya_ait_detaylar.YazıyaDönüştür(null));
                seri_noya_ait_detaylar.Sil(null);
            }

            IDepo_Eleman müş = Ayarlar_Müşteri(Müşteri, "Sayfa/Teslim Edildi", true);
            müş["KDV"].Yaz(null, KDV);

            yeni_tablo.Yaz("Ödeme", t);
            if (!string.IsNullOrEmpty(İlaveÖdeme_Açıklama))
            {
                yeni_tablo.Yaz("Ödeme/İlave Ödeme", İlaveÖdeme_Açıklama, 0);
                yeni_tablo.Yaz("Ödeme/İlave Ödeme", İlaveÖdeme_Miktar, 1);

                müş["İlave Ödeme"].İçeriği = new string[] { İlaveÖdeme_Açıklama, İlaveÖdeme_Miktar };
            }

            yeni_tablo.Yaz("Ödeme/Alt Toplam", Alt_Toplam);
            if (KDV) yeni_tablo.Yaz("Ödeme/Alt Toplam", Ayarlar_Genel("Bütçe/KDV", true).Oku_Sayı(null, 8), 1);
        }
        public static void Talep_İşaretle_ÖdemeTalepEdildi_TeslimEdildi(string Müşteri, string EkTanım)
        {
            Depo_ depo = Tablo(Müşteri, TabloTürü.ÖdemeTalepEdildi, false, EkTanım);

            IDepo_Eleman eski = depo.Bul("Talepler");
            if (eski == null) return;

            IDepo_Eleman devam_eden = Tablo_Dal(Müşteri, TabloTürü.DevamEden, "Talepler", true);
            foreach (IDepo_Eleman e in eski.Elemanları)
            {
                devam_eden.Ekle(null, e.YazıyaDönüştür(null));
            }

            depo.Yaz("Silinecek", "Evet");
        }
        public static void Talep_İşaretle_ÖdemeTalepEdildi_Ödendi(string Müşteri, string EkTanım, string AlınanÖdemeMiktarı, string Notlar)
        {
            //AlınanÖdemeMiktarı kullanılmıyor ise 0 olmalı

            DateTime t = DateTime.Now;
            Depo_ depo = Tablo(Müşteri, TabloTürü.ÖdemeTalepEdildi, false, EkTanım);
            IDepo_Eleman müş = Ayarlar_Genel("Müşteriler/" + Müşteri);

            Depo_ yeni_tablo = new Depo_();
            yeni_tablo.Ekle(depo.YazıyaDönüştür());
            yeni_tablo.Yaz("Tür", TabloTürü.Ödendi.ToString());
            yeni_tablo.Yaz("Tür", t.Yazıya(ArgeMup.HazirKod.Dönüştürme.D_TarihSaat.Şablon_DosyaAdı2), 2);
            yeni_tablo.Yaz("Ödeme", t.Yazıya(), 1);
            yeni_tablo.Yaz("Ödeme", Notlar, 2);

            //Ödeme Yapılarak Ödendi Olarak İşasaretleme
            Depo_ Ödemeler = Tablo(Müşteri, TabloTürü.Ödemeler, AlınanÖdemeMiktarı.NoktalıSayıya() != 0);
            if (Ödemeler != null)
            {
                //ödendi tablosuna kayıt
                string MevcutÖnÖdeme = Ödemeler.Oku("Mevcut Ön Ödeme", "0");
                yeni_tablo["Ödeme/Ön Ödeme"].İçeriği = new string[] { MevcutÖnÖdeme, AlınanÖdemeMiktarı };

                //ödemeler tablosuna kayıt
                IDepo_Eleman ÖdemeDalı = yeni_tablo["Ödeme"];
                double AltToplam = ÖdemeDalı.Oku_Sayı("Alt Toplam");
                double KDV = ÖdemeDalı.Oku_Sayı("Alt Toplam", 0, 1);
                double KDV_Hesaplanan = KDV == 0 ? 0 : AltToplam / 100 * KDV;
                double İlaveÖdeme = ÖdemeDalı.Oku_Sayı("İlave Ödeme", 0, 1);
                double GenelToplam = AltToplam + KDV_Hesaplanan + İlaveÖdeme;

                Ödemeler.Yaz("Mevcut Ön Ödeme", MevcutÖnÖdeme.NoktalıSayıya() + AlınanÖdemeMiktarı.NoktalıSayıya() - GenelToplam);
                Ödemeler["Ödemeler/" + t.Yazıya()].İçeriği = new string[]
                {
                    MevcutÖnÖdeme, AlınanÖdemeMiktarı, GenelToplam.Yazıya(),
                    t.Yazıya(ArgeMup.HazirKod.Dönüştürme.D_TarihSaat.Şablon_DosyaAdı2) + (Notlar.DoluMu() ? "\n" + Notlar : null)
                };
            }

            Depo_Kaydet("Mü\\" + müş[0] + "\\Mü_C\\Mü_C_" + t.Yazıya(ArgeMup.HazirKod.Dönüştürme.D_TarihSaat.Şablon_DosyaAdı2), yeni_tablo);
            
            depo.Yaz("Silinecek", "Evet");
        }
        public static void Talep_TablodaGöster(DataGridView Tablo, Banka_Tablo_ İçerik, bool ÖnceTemizle = true)
        {
            Tablo.Tag = 0;

            if (ÖnceTemizle)
            {
                Tablo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                Tablo.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                Tablo.Rows.Clear();

                if (Tablo.SortedColumn != null)
                {
                    DataGridViewColumn col = Tablo.SortedColumn;
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
                    col.SortMode = DataGridViewColumnSortMode.Automatic;
                }
            }

            string tar_ödeme_talep = null, tar_ödendi = null;
            object tar_ödeme_talep_t = null, tar_ödendi_t = null;
            if (İçerik.Ödeme != null)
            {
                tar_ödeme_talep = İçerik.Ödeme.Oku(null, null, 0);
                tar_ödendi = İçerik.Ödeme.Oku(null, null, 1);

                if (tar_ödeme_talep.DoluMu())
                {
                    tar_ödeme_talep_t = tar_ödeme_talep.TarihSaate();
                    tar_ödeme_talep = Yazdır_Tarih(tar_ödeme_talep);
                }

                if (tar_ödendi.DoluMu())
                {
                    tar_ödendi_t = tar_ödendi.TarihSaate();
                    tar_ödendi = Yazdır_Tarih(tar_ödendi);
                }
            }

            foreach (IDepo_Eleman seri_no_dalı in İçerik.Talepler)
            {
                int y = Tablo.RowCount;
                Tablo.RowCount++;

                double ücreti = 0;
                Talep_Ayıkla_SeriNoDalı(seri_no_dalı, out string Hasta, out string İşGirişTarihleri, out string İşÇıkışTarihleri, out string İşler, ref ücreti);

                Tablo[0, y].Value = false; //seçim kutucuğu
                Tablo[1, y].Value = seri_no_dalı.Adı; //seri no
                Tablo[2, y].Value = İçerik.Müşteri;
                Tablo[3, y].Value = Hasta;
                Tablo[4, y].Value = İşGirişTarihleri; //iş giriş tarihi
                Tablo[5, y].Value = İşÇıkışTarihleri; //iş çıkış tarihi
                Tablo[6, y].Value = İşler;
                Tablo[7, y].Value = Yazdır_Tarih(seri_no_dalı[3]); //teslim edilme tarihi
                Tablo[8, y].Value = tar_ödeme_talep;
                Tablo[9, y].Value = tar_ödendi;
                Tablo[10, y].Value = seri_no_dalı[2]; //notlar

                if (seri_no_dalı[3].DoluMu())
                {
                    //teslim edildi ise
                    Tablo[6, y].ToolTipText = Yazdır_Ücret(ücreti);
                    Tablo[6, y].Tag = ücreti;
                    Tablo[7, y].Tag = seri_no_dalı[3].TarihSaate(); //teslim edilme tarihi
                    Tablo[8, y].Tag = tar_ödeme_talep_t; //ödeme talep edilme tarihi
                    Tablo[9, y].Tag = tar_ödendi_t; //ödeme tarihi
                }
            }

            if (ÖnceTemizle)
            {
                Tablo.Columns[5].Visible = true; //iş çıkış tarihi

                switch (İçerik.Türü)
                {
                    case TabloTürü.DevamEden:
                        Tablo.Columns[2].Visible = false; //müşteri
                        Tablo.Columns[7].Visible = false; //tarih teslim
                        Tablo.Columns[8].Visible = false; //tarih ödeme talebi
                        Tablo.Columns[9].Visible = false; //tarih ödendi
                        break;
                    case TabloTürü.TeslimEdildi:
                        Tablo.Columns[2].Visible = false; //müşteri
                        Tablo.Columns[7].Visible = true; //tarih teslim
                        Tablo.Columns[8].Visible = false; //tarih ödeme talebi
                        Tablo.Columns[9].Visible = false; //tarih ödendi
                        break;

                    case TabloTürü.ÖdemeTalepEdildi:
                        Tablo.Columns[2].Visible = false; //müşteri
                        Tablo.Columns[7].Visible = true; //tarih teslim
                        Tablo.Columns[8].Visible = true; //tarih ödeme talebi
                        Tablo.Columns[9].Visible = false; //tarih ödendi
                        break;

                    case TabloTürü.Ödendi:
                        Tablo.Columns[2].Visible = false; //müşteri
                        Tablo.Columns[7].Visible = true; //tarih teslim
                        Tablo.Columns[8].Visible = true; //tarih ödeme talebi
                        Tablo.Columns[9].Visible = true; //tarih ödendi
                        break;

                    case TabloTürü.DevamEden_TeslimEdildi_ÖdemeTalepEdildi_Ödendi:
                        Tablo.Columns[2].Visible = true; //müşteri
                        Tablo.Columns[7].Visible = true; //tarih teslim
                        Tablo.Columns[8].Visible = true; //tarih ödeme talebi
                        Tablo.Columns[9].Visible = true; //tarih ödendi
                        break;
                    default:
                        break;
                }

                Tablo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                Tablo.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                Tablo.AutoResizeColumns();
                Tablo.ClearSelection();
            }

            Tablo.Tag = null;
        }
        public static void Talep_Ayıkla_İşTürüDalı(IDepo_Eleman İşTürüDalı, out string İşTürü, out string GirişTarihi, out string ÇıkışTarihi, out string Ücret1, out string Ücret2)
        {
            GirişTarihi = İşTürüDalı.Adı;
            İşTürü = İşTürüDalı[0];
            ÇıkışTarihi = İşTürüDalı[1];
            Ücret1 = İşTürüDalı[2];
            Ücret2 = İşTürüDalı[3];
        }
        public static void Talep_Ayıkla_SeriNoDalı(IDepo_Eleman SeriNoDalı, out string SeriNo, out string Hasta, out string İskonto, out string Notlar, out string TeslimEdilmeTarihi)
        {
            SeriNo = SeriNoDalı.Adı;
            Hasta = SeriNoDalı[0];
            İskonto = SeriNoDalı[1];
            Notlar = SeriNoDalı[2];
            TeslimEdilmeTarihi = SeriNoDalı[3];
        }
        public static void Talep_Ayıkla_SeriNoDalı(IDepo_Eleman SeriNoDalı, out string Hasta, out string İşler, ref double Toplam)
        {
            Hasta = SeriNoDalı[0];
            double iskonto = SeriNoDalı.Oku_Sayı(null, 0, 1);
            if (iskonto > 0) Hasta += "\n% " + iskonto + " iskonto";

            İşler = "";
            double AltToplam = 0;
            foreach (IDepo_Eleman iş in SeriNoDalı.Elemanları)
            {
                //giriş tarih - iş türü - ücret
                İşler += Yazdır_Tarih(iş.Adı) + " " + iş[0];

                //ücret girilmemiş ise gösterilmeyecek, AltToplamdan hariç tutulacak : -1
                //ücret girilmiş ise gösterilecek : >= 0
                double ücret = iş.Oku_Sayı(null, -1, 2);
                if (ücret >= 0)
                {
                    İşler += " " + Yazdır_Ücret(ücret);
                    AltToplam += ücret;
                }

                İşler += "\n";
            }
            İşler = İşler.TrimEnd('\n');

            if (iskonto > 0 && AltToplam > 0) AltToplam -= AltToplam / 100 * iskonto;

            Toplam += AltToplam;
        }
        public static void Talep_Ayıkla_SeriNoDalı(IDepo_Eleman SeriNoDalı, out string Hasta, out string İşGirişTarihleri, out string İşÇıkışTarihleri, out string İşler, ref double Toplam)
        {
            Hasta = SeriNoDalı[0];
            double iskonto = SeriNoDalı.Oku_Sayı(null, 0, 1);
            if (iskonto > 0) Hasta += "\n% " + iskonto + " iskonto";

            İşGirişTarihleri = "";
            İşÇıkışTarihleri = "";
            İşler = "";
            double AltToplam = 0;
            foreach (IDepo_Eleman iş in SeriNoDalı.Elemanları)
            {
                //tarihler - iş türü - ücret sadece 0 dan büyük ise

                İşGirişTarihleri += Yazdır_Tarih(iş.Adı) + "\n";
                İşÇıkışTarihleri += (iş[1].DoluMu() ? Yazdır_Tarih(iş[1]) : " ") + "\n";
                İşler += iş[0];

                //ücret girilmemiş ise gösterilmeyecek, AltToplamdan hariç tutulacak : -1
                //ücret girilmiş ise gösterilecek : >= 0
                double ücret = iş.Oku_Sayı(null, -1, 2);
                if (ücret >= 0)
                {
                    İşler += " " + Yazdır_Ücret(ücret);
                    AltToplam += ücret;
                }

                İşler += "\n";
            }
            İşGirişTarihleri = İşGirişTarihleri.TrimEnd('\n');
            İşÇıkışTarihleri = İşÇıkışTarihleri.TrimEnd('\n');
            İşler = İşler.TrimEnd('\n');

            if (iskonto > 0 && AltToplam > 0) AltToplam -= AltToplam / 100 * iskonto;

            Toplam += AltToplam;
        }
        public static void Talep_Ayıkla_SeriNoDalı(string Müşteri, IDepo_Eleman SeriNoDalı, ref double İskontaDahilÜcretler_Toplamı, ref double Maliyetler_Toplamı, ref string HataMesajı)
        {
            double iskonto = SeriNoDalı.Oku_Sayı(null, 0, 1), Toplam_Ücret = 0, Toplam_Maliyet = 0;

            foreach (IDepo_Eleman iş in SeriNoDalı.Elemanları)
            {
                double ücret = iş.Oku_Sayı(null, -1, 2);
                if (ücret < 0) ücret = Ücretler_BirimÜcret(Müşteri, iş[0]);

                if (ücret >= 0) Toplam_Ücret += ücret;
                else HataMesajı += SeriNoDalı.Adı + " " + iş[0] + " için ücret hesaplanamadı" + Environment.NewLine;

                Toplam_Maliyet += Ücretler_Maliyet(iş[0]);
            }

            if (iskonto > 0 && Toplam_Ücret > 0) Toplam_Ücret -= Toplam_Ücret / 100 * iskonto;

            İskontaDahilÜcretler_Toplamı += Toplam_Ücret;
            Maliyetler_Toplamı += Toplam_Maliyet;
        }    
        public static void Talep_Ayıkla_ÖdemeDalı(IDepo_Eleman ÖdemeDalı, out List<string> Açıklamalar, out List<string> Ücretler, out string ÖdemeTalepEdildi, out string Notlar, out bool MüşteriBorçluMu)
        {
            _Talep_Ayıkla_ÖdemeDalı o = new _Talep_Ayıkla_ÖdemeDalı(ÖdemeDalı);
            string Açıklama;

            ÖdemeTalepEdildi = o.Tarih_ÖdemeTalebi;
            Notlar = o.Notlar;
            MüşteriBorçluMu = o.MüşteriBorçluMu;

            Açıklamalar = new List<string>();
            Ücretler = new List<string>();

            if (o.KDV_Oranı == 0 && !o.İlaveÖdeme_İşlemiVarmı) Açıklama = "Alt Toplam";
            else
            {
                Açıklama = "Alt Toplam (" + Yazdır_Ücret(o.AltToplam) + ")";

                if (o.KDV_Oranı > 0) Açıklama += " + KDV % " + o.KDV_Oranı + " (" + Yazdır_Ücret(o.KDV_Hesaplanan) + ")";

                if (o.İlaveÖdeme_İşlemiVarmı)
                {
                    Açıklama += " + Diğer (" + Yazdır_Ücret(o.İlaveÖdeme) + ")";
                    Notlar = Notlar + (Notlar.DoluMu() ? Environment.NewLine : null) + "Diğer : " + o.İlaveÖdeme_Açıklaması;
                }
            }
            Açıklamalar.Add(Açıklama); Ücretler.Add(Yazdır_Ücret(o.AltToplam + o.KDV_Hesaplanan + o.İlaveÖdeme, false));

            if (o.Tarih_Ödendi.DoluMu())
            {
                //Ödendi
                if (o.ÖnÖdeme_İşlemiVarmı)
                {
                    //Ödemeye eklenen notlar 
                    //Diğer : İlave Ödeme Açıklaması (varsa)
                    //Ödendi : 31.01.2022 (varsa)

                    //Alt Toplam (1.00 ₺) + KDV % 10 (0.10 ₺) + Diğer (0.50 ₺)                          1.60 ₺
                    //Alınan Ödeme (2.00 ₺) + Mevcut Ön Ödeme (2.00 ₺) / - Mevcut Borcunuz (500,00 ₺)   4.00 ₺
                    //İşlem Sonrası / Müşterinin Borcu / Kalan Ön Ödeme                                 2.40 ₺

                    Açıklama = "Alınan Ödeme (" + Yazdır_Ücret(o.ÖnÖdeme_AlınanÖdeme) + ") ";

                    if (o.ÖnÖdeme_MevcutÖnÖdeme < 0) Açıklama += "- Mevcut Borç";
                    else Açıklama += "+ Mevcut Ön Ödeme";

                    Açıklama += " (" + Yazdır_Ücret(Math.Abs(o.ÖnÖdeme_MevcutÖnÖdeme)) + ")";
                    
                    Açıklamalar.Add(Açıklama); Ücretler.Add(Yazdır_Ücret(o.ÖnÖdeme_MevcutÖnÖdeme + o.ÖnÖdeme_AlınanÖdeme, false));

                    Açıklamalar.Add("İşlem Sonrası " + (o.MüşteriBorçluMu ? "Müşterinin Borcu" : "Kalan Ön Ödeme")); Ücretler.Add(Yazdır_Ücret(Math.Abs(o.ÖnÖdeme_İşlemSonrasıÖnÖdeme), false));
                }
                //else
                //{
                //    //Ödemeye eklenen notlar 
                //    //Diğer : İlave Ödeme Açıklaması (varsa)
                //    //Ödendi : 31.01.2022 (varsa)
                //
                //    //Alt Toplam (1.00 ₺) + KDV % 10 (0.10 ₺) + Diğer (0.50 ₺) 1.60 ₺
                //}

                Notlar = Notlar + (Notlar.DoluMu() ? Environment.NewLine : null) + "Ödendi : " + Yazdır_Tarih(o.Tarih_Ödendi);
            }
            else
            {
                //Ödeme Talep Edildi
                if (o.Gecici_İşlemiVarmı)
                {
                    //Ödemeye eklenen notlar 
                    //Diğer : İlave Ödeme Açıklaması (varsa)

                    //Alt Toplam (5.00 ₺) + KDV % 10 (0.50 ₺) + Diğer (0.50 ₺)                                           6.00 ₺
                    //Geçen Dönem : Alınan Ödeme (20,00 ₺) + Artan (5,00 ₺) - Genel Toplam (10,00 ₺) - Borç (5,00 ₺)    10.00 ₺
                    //İşlem Sonrası / Dönem Borcu / Artan                                                                4.00 ₺

                    Açıklama = "Son Dönem : Alınan Ödeme (" + Yazdır_Ücret(o.Gecici_EnSonÖdemeDokümanı_AlınanÖdeme) + ")";
                    
                    if (o.Gecici_EnSonÖdemeDokümanı_ÖnÖdeme > 0) Açıklama += " + Mevcut Ön Ödeme (" + Yazdır_Ücret(o.Gecici_EnSonÖdemeDokümanı_ÖnÖdeme) + ")";
                    
                    Açıklama += " - Genel Toplam (" + Yazdır_Ücret(o.Gecici_EnSonÖdemeDokümanı_GenelToplam) + ")";
                    
                    if (o.Gecici_EnSonÖdemeDokümanı_ÖnÖdeme < 0) Açıklama += " - Mevcut Borç (" + Yazdır_Ücret(Math.Abs(o.Gecici_EnSonÖdemeDokümanı_ÖnÖdeme)) + ")";
                    
                    Açıklamalar.Add(Açıklama); Ücretler.Add(Yazdır_Ücret(o.Gecici_Güncel_ÖnÖdeme, false));

                    Açıklamalar.Add("İşlem Sonrası " + (o.MüşteriBorçluMu ? "Dönem Borcu" : "Artan")); Ücretler.Add(Yazdır_Ücret(Math.Abs(o.Gecici_Güncel_İşlemSonrasıMüşteriBorcu), false));
                }
            }
        }
        public static void Talep_Ayıkla_ÖdemeDalı_Açıklama(IDepo_Eleman ÖdemeDalı, out string Açıklama, out bool İşlemSonucundaMüşteriBorçlu, out double GenelToplam)
        {
            Talep_Ayıkla_ÖdemeDalı(ÖdemeDalı, out List<string> Açıklamalar, out List<string> Ücretler, out _, out string Notlar, out _);
            Açıklama = "";
            for (int i = 0; i < Açıklamalar.Count; i++)
            {
                Açıklama += Açıklamalar[i] + " = " + Ücretler[i] + Environment.NewLine;
            }

            if (Notlar.DoluMu()) Açıklama += Notlar;
            else Açıklama = Açıklama.TrimEnd('\r', '\n');

            _Talep_Ayıkla_ÖdemeDalı o = new _Talep_Ayıkla_ÖdemeDalı(ÖdemeDalı);
            İşlemSonucundaMüşteriBorçlu = o.MüşteriBorçluMu;
            GenelToplam = o.Genel_Toplam;
        }
        public static IDepo_Eleman Talep_Bul_DevamEden(string Müşteri, string SeriNo)
        {
            return Tablo_Dal(Müşteri, TabloTürü.DevamEden, "Talepler/" + SeriNo);
        }

        public static List<string> KorumalıAlan_Listele_Dosyalar()
        {
            List<string> l = new List<string>();

            IDepo_Eleman dosyalar = Tablo_Dal(null, TabloTürü.KorumalıAlan, "Dosyalar");
            if (dosyalar == null || dosyalar.Elemanları.Length == 0) return l;

            foreach (IDepo_Eleman dosya in dosyalar.Elemanları)
            {
                if (dosya.İçiBoşOlduğuİçinSilinecek) continue;

                l.Add(dosya.Adı);
            }

            return l;
        }
        public static List<string> KorumalıAlan_Listele_Sürümler(string DosyaAdı)
        {
            List<string> l = new List<string>();

            IDepo_Eleman sürümler = Tablo_Dal(null, TabloTürü.KorumalıAlan, "Dosyalar/" + DosyaAdı);
            if (sürümler == null || sürümler.Elemanları.Length == 0) return l;

            //son tarihli en üstte
            for (int i = sürümler.Elemanları.Length - 1; i >= 0; i--)
            {
                if (sürümler.Elemanları[i].İçiBoşOlduğuİçinSilinecek) continue;

                l.Add(sürümler.Elemanları[i].Adı);
            }

            return l;
        }
        public static void KorumalıAlan_Ekle(string DosyaYolu)
        {
            byte[] içerik;
            string DoKo;
            string ıdepo_adı = Path.GetFileName(DosyaYolu);

            if (!File.Exists(DosyaYolu) && Directory.Exists(DosyaYolu))
            {
                //klasör
                string Gecici_zip_klasörü = Path.GetRandomFileName();
                while (Directory.Exists(Ortak.Klasör_Gecici + Gecici_zip_klasörü)) Gecici_zip_klasörü = Path.GetRandomFileName();
                Gecici_zip_klasörü = Ortak.Klasör_Gecici + Gecici_zip_klasörü + "\\" + ıdepo_adı + ".zip";

                if (!SıkıştırılmışDosya.Klasörden(DosyaYolu, Gecici_zip_klasörü)) throw new Exception("Klasör sıkıştırılamadı " + Gecici_zip_klasörü);
                içerik = File.ReadAllBytes(Gecici_zip_klasörü);
                if (!Klasör.Sil(Klasör.ÜstKlasör(Gecici_zip_klasörü))) throw new Exception("Klasör silinemedi " + Gecici_zip_klasörü);

                DoKo = DoğrulamaKodu.Üret.Klasörden(DosyaYolu, false, SearchOption.AllDirectories, Parola.Yazı);

                ıdepo_adı = ":" + Path.GetFileNameWithoutExtension(ıdepo_adı);
            }
            else
            {
                //dosya
                içerik = File.ReadAllBytes(DosyaYolu);
                DoKo = DoğrulamaKodu.Üret.BaytDizisinden(içerik).HexYazıya();
            }
           
            IDepo_Eleman dosya = Tablo_Dal(null, TabloTürü.KorumalıAlan, "Dosyalar/" + ıdepo_adı, true);
            if (dosya.Elemanları.Length > 0 && dosya.Elemanları[dosya.Elemanları.Length - 1][1] == DoKo) return;//son eklenen ile aynı

            int SürümSayısı = Tablo_Dal(null, TabloTürü.KorumalıAlan, "Sürüm Sayısı", true).Oku_TamSayı(null, 15);
            int adet_fazla = (dosya.Elemanları.Length - SürümSayısı) + 1 /*yenisi*/;
            for (int i = 0; i < adet_fazla; i++)
            {
                if (!Dosya.Sil(Ortak.Klasör_KullanıcıDosyaları_KorumalıAlan + dosya.Elemanları[i].Adı)) throw new Exception("Dosya silinemedi eski " + dosya.Elemanları[i].Adı);
                dosya.Sil(dosya.Elemanları[i].Adı);
            }

            string KapalıAdı = Path.GetRandomFileName();
            while (File.Exists(Ortak.Klasör_KullanıcıDosyaları_KorumalıAlan + KapalıAdı)) KapalıAdı = Path.GetRandomFileName();

            File.WriteAllBytes(Ortak.Klasör_KullanıcıDosyaları_KorumalıAlan + KapalıAdı, Dosya_SıkıştırKarıştır(içerik));
            dosya[DateTime.Now.Yazıya()].İçeriği = new string[] { KapalıAdı, DoKo };
        }
        public static void KorumalıAlan_MasaüstüneKopyala(string DosyaAdı, string Sürüm)
        {
            IDepo_Eleman dosya = Tablo_Dal(null, TabloTürü.KorumalıAlan, "Dosyalar/" + DosyaAdı + "/" + Sürüm);
            if (dosya == null) throw new Exception("Dosya girdisi bulunamadı " + DosyaAdı);
            if (!File.Exists(Ortak.Klasör_KullanıcıDosyaları_KorumalıAlan + dosya[0])) throw new Exception("Dosya bulunamadı " + dosya[0]);
            byte[] içerik = Dosya_DüzeltAç(File.ReadAllBytes(Ortak.Klasör_KullanıcıDosyaları_KorumalıAlan + dosya[0]));
            
            if (DosyaAdı.StartsWith(":"))
            {
                //klasör
                string Gecici_zip_dosyası = Path.GetRandomFileName();
                while (File.Exists(Ortak.Klasör_Gecici + Gecici_zip_dosyası)) Gecici_zip_dosyası = Path.GetRandomFileName();
                Gecici_zip_dosyası = Ortak.Klasör_Gecici + Gecici_zip_dosyası;
                string Gecici_zip_klasörü = Gecici_zip_dosyası + "_";

                if (!Klasör.Oluştur(Gecici_zip_klasörü)) throw new Exception("Klasör oluşturulamadı " + Gecici_zip_klasörü);
                File.WriteAllBytes(Gecici_zip_dosyası, içerik);
                SıkıştırılmışDosya.Klasöre(Gecici_zip_dosyası, Gecici_zip_klasörü);
                if (!Dosya.Sil(Gecici_zip_dosyası)) throw new Exception("DOsya silinemedi " + Gecici_zip_dosyası);

                string DoKo = DoğrulamaKodu.Üret.Klasörden(Gecici_zip_klasörü, false, SearchOption.AllDirectories, Parola.Yazı);
                if (dosya[1] != DoKo)
                {
                    Klasör.Sil(Gecici_zip_klasörü);
                    throw new Exception("Dosya doğrulama kodu hatalı " + dosya[0]);
                }

                DosyaAdı = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\" + DosyaAdı.Substring(1);
                if (!Ortak.Klasör_TamKopya(Gecici_zip_klasörü, DosyaAdı)) throw new Exception("Klasör kopyalanamadı " + DosyaAdı);
                if (!Klasör.Sil(Gecici_zip_klasörü)) throw new Exception("Klasör silinemedi " + Gecici_zip_klasörü);
            }
            else
            {
                string DoKo = DoğrulamaKodu.Üret.BaytDizisinden(içerik).HexYazıya();
                if (dosya[1] != DoKo) throw new Exception("Dosya doğrulama kodu hatalı " + dosya[0]);

                File.WriteAllBytes(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\" + DosyaAdı, içerik);
            }
        }
        public static void KorumalıAlan_Sil(string DosyaAdı)
        {
            IDepo_Eleman dosya = Tablo_Dal(null, TabloTürü.KorumalıAlan, "Dosyalar/" + DosyaAdı);
            if (dosya != null)
            {
                foreach (IDepo_Eleman dsy in dosya.Elemanları)
                {
                    if (!Dosya.Sil(Ortak.Klasör_KullanıcıDosyaları_KorumalıAlan + dsy[0])) throw new Exception("Dosya silinemedi " + Ortak.Klasör_KullanıcıDosyaları_KorumalıAlan + dsy.Adı);
                }

                dosya.Sil(null);
            }
        }

        public static void Değişiklikleri_Kaydet(Control Tetikleyen)
        {
            if (Yedekleme_Tümü_Çalışıyor)
            {
                Ortak.Gösterge.Başlat("Yedekleniyor", false, Tetikleyen, 0);

                while (Yedekleme_Tümü_Çalışıyor && Ortak.Gösterge.Çalışsın)
                {
                    System.Threading.Thread.Sleep(250);
                }
            }

            if (Yedekleme_İzleyici_DeğişiklikYapıldı != null)
            {
                string soru = "Yedeğinizin içeriği uygulama haricinde değiştirildi." + Environment.NewLine +
                "Uygulamayı yeniden başlatmak ister misiniz?" + Environment.NewLine + Environment.NewLine +
                "Evet : Tavsiye edilir fakat şuanda yaptığınız işlemden feragat edeceksiniz." + Environment.NewLine +
                "Hayır : İşlemlerinizin sonuçları değişen yedeğin üzerine kaydedilir." + Environment.NewLine + Environment.NewLine +
                Yedekleme_İzleyici_DeğişiklikYapıldı;

                DialogResult Dr = MessageBox.Show(soru, Ortak.AnaEkran.Text + " Yedekleme", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                if (Dr == DialogResult.Yes) 
                { 
                    System.Windows.Forms.Application.Restart(); 
                    return; 
                }

                Yedekleme_İzleyici_DeğişiklikYapıldı = null;
            }

            bool EnAzBirDeğişiklikYapıldı = false;
            Ortak.Gösterge.Başlat("Kaydediliyor", false, Tetikleyen, 7 + (Müşteriler == null ? 0 : Müşteriler.Count * 4) + (MalzemeKullanımDetayları == null ? 0 : MalzemeKullanımDetayları.Count * 1));

            if (Müşteriler != null && Müşteriler.Count > 0)
            {
                foreach (Müşteri_ m in Müşteriler.Values)
                {
                    Ortak.Gösterge.İlerleme = 1;
                    if (m.ÖdemeTalepEdildi != null)
                    {
                        foreach (KeyValuePair<string, Depo_> a in m.ÖdemeTalepEdildi)
                        {
                            if (a.Value == null) continue;

                            if (!string.IsNullOrEmpty(a.Value.Oku("Silinecek"))) { Dosya.Sil(Ortak.Klasör_Banka_Müşteriler + m.KlasörAdı + "\\Mü_B\\Mü_B_" + a.Key + ".mup"); EnAzBirDeğişiklikYapıldı = true; }
                            else if (a.Value.EnAzBir_ElemanAdıVeyaİçeriği_Değişti) { Depo_Kaydet("Mü\\" + m.KlasörAdı + "\\Mü_B\\Mü_B_" + a.Key, a.Value); EnAzBirDeğişiklikYapıldı = true; }
                        }
                    }
                    m.ÖdemeTalepEdildi = null;
                    
                    m.Ödendi = null;
                    m.Liste_ÖdemeTalepEdildi = null;
                    m.Liste_Ödendi = null;

                    Ortak.Gösterge.İlerleme = 1;
                    if (m.DevamEden != null && m.DevamEden.EnAzBir_ElemanAdıVeyaİçeriği_Değişti) { Depo_Kaydet("Mü\\" + m.KlasörAdı + "\\Mü_A", m.DevamEden); EnAzBirDeğişiklikYapıldı = true; }

                    Ortak.Gösterge.İlerleme = 1;
                    if (m.Ödemeler != null && m.Ödemeler.EnAzBir_ElemanAdıVeyaİçeriği_Değişti) { Depo_Kaydet("Mü\\" + m.KlasörAdı + "\\Mü_Öd", m.Ödemeler); EnAzBirDeğişiklikYapıldı = true; }

                    Ortak.Gösterge.İlerleme = 1;
                    if (m.Ayarlar != null && m.Ayarlar.EnAzBir_ElemanAdıVeyaİçeriği_Değişti) { Depo_Kaydet("Mü\\" + m.KlasörAdı + "\\Mü_Ay", m.Ayarlar); EnAzBirDeğişiklikYapıldı = true; }
                }
            }

            if (MalzemeKullanımDetayları != null && MalzemeKullanımDetayları.Count > 0)
            {
                foreach (MalzemeKullanımDetayı_ m in MalzemeKullanımDetayları.Values)
                {
                    Ortak.Gösterge.İlerleme = 1;
                    if (m.DevamEden != null && m.DevamEden.EnAzBir_ElemanAdıVeyaİçeriği_Değişti) { Depo_Kaydet("MaKD\\" + m.KlasörAdı + "\\MaKD_A", m.DevamEden); EnAzBirDeğişiklikYapıldı = true; }
                }
            }

            Ortak.Gösterge.İlerleme = 1;
            if (İşTürleri != null && İşTürleri.EnAzBir_ElemanAdıVeyaİçeriği_Değişti) { Depo_Kaydet("İt", İşTürleri); EnAzBirDeğişiklikYapıldı = true; }
           
            Ortak.Gösterge.İlerleme = 1;
            if (Malzemeler != null && Malzemeler.EnAzBir_ElemanAdıVeyaİçeriği_Değişti) { Depo_Kaydet("Ma", Malzemeler); EnAzBirDeğişiklikYapıldı = true; }

            Ortak.Gösterge.İlerleme = 1;
            if (Kullanıcılar != null && Kullanıcılar.EnAzBir_ElemanAdıVeyaİçeriği_Değişti) { Depo_Kaydet("Ku", Kullanıcılar, Ortak.Klasör_KullanıcıDosyaları_Ayarlar); }

            Ortak.Gösterge.İlerleme = 1;
            if (Takvim != null && Takvim.EnAzBir_ElemanAdıVeyaİçeriği_Değişti) { Depo_Kaydet("Ta", Takvim); EnAzBirDeğişiklikYapıldı = true; }

            Ortak.Gösterge.İlerleme = 1;
            if (KorumalıAlan != null && KorumalıAlan.EnAzBir_ElemanAdıVeyaİçeriği_Değişti) { Depo_Kaydet("KoAl", KorumalıAlan); EnAzBirDeğişiklikYapıldı = true; }

            Ortak.Gösterge.İlerleme = 1;
            if (EnAzBirDeğişiklikYapıldı || (Ayarlar != null && Ayarlar.EnAzBir_ElemanAdıVeyaİçeriği_Değişti))
            {
                IDepo_Eleman d = Tablo_Dal(null, TabloTürü.Ayarlar, "Son Banka Kayıt", true);
                if (d.Oku_TarihSaat("Son Banka Kayıt", default, 1) > DateTime.Now)
                {
                    string msg = "Son kayıt saati : " + d.Oku("Son Banka Kayıt", default, 1) + Environment.NewLine +
                        "Bilgisayarınızın saati : " + DateTime.Now.Yazıya() + Environment.NewLine + Environment.NewLine +
                        "Muhtemelen bilgisayarınızın saati geri kaldı, lütfen düzeltip devam ediniz";

                    throw new Exception(msg);
                }

                d[1] = DateTime.Now.Yazıya();
                Depo_Kaydet("Ay", Ayarlar); 

                EnAzBirDeğişiklikYapıldı = true; 
            }

            Ortak.Gösterge.İlerleme = 1;
            if (EnAzBirDeğişiklikYapıldı)
            {
                DoğrulamaKodu.Üret.Klasörden(Ortak.Klasör_Banka, true, SearchOption.AllDirectories, Parola.Yazı);
                Yedekle_Banka();
                Yedekleme_EnAz1Kez_Değişiklikler_Kaydedildi = true;
                Ortak.Hatırlatıcılar.YenidenKontrolEdilmeli = true;
            }

            Ortak.Gösterge.Bitir();
        }
        public static void Değişiklikler_TamponuSıfırla()
        {
            Ayarlar = null;
            İşTürleri = null;
            Malzemeler = null;
            Müşteriler = null;
            Kullanıcılar = null;
            KorumalıAlan = null;
            Takvim = null;
            MalzemeKullanımDetayları = null;
        }

        public static string Yazdır_Tarih(string Girdi)
        {
            if (string.IsNullOrEmpty(Girdi) || Girdi.Length < 10) return Girdi;

            return Girdi.Substring(0, 10); // dd.MM.yyyy
        }
        public static string Yazdır_Ücret(double Ücret, bool SondakiSıfırlarıSil = true)
        {
            string çıktı = string.Format("{0:,0.00}", Ücret);
            if (SondakiSıfırlarıSil && çıktı.EndsWith("00")) çıktı = çıktı.Remove(çıktı.Length - 3/*.00*/);

            return çıktı + " ₺";
        }

        #region Demirbaşlar
        public enum TabloTürü { Ayarlar, İşTürleri, Malzemeler, MalzemeKullanımDetayı, Ödemeler, Kullanıcılar, Takvim, KorumalıAlan,
                                DevamEden, TeslimEdildi, ÖdemeTalepEdildi, Ödendi,
                                DevamEden_TeslimEdildi_ÖdemeTalepEdildi_Ödendi         }
        static Depo_ Ayarlar = null;
        static Depo_ İşTürleri = null;
        static Depo_ Malzemeler = null;
        static Depo_ Kullanıcılar = null;
        static Depo_ KorumalıAlan = null;
        static Depo_ Takvim = null;

        class Müşteri_
        {
            public string Adı = null;
            public string KlasörAdı = null;

            public Depo_ DevamEden = null;
            public Depo_ Ayarlar = null;
            public Depo_ Ödemeler = null;
            public Dictionary<string, Depo_> ÖdemeTalepEdildi = null;
            public Dictionary<string, Depo_> Ödendi = null;
            
            public string[] Liste_ÖdemeTalepEdildi = null;
            public string[] Liste_Ödendi = null;

            public Müşteri_(string Adı)
            {
                this.Adı = Adı;
            }
        }
        static Dictionary<string, Müşteri_> Müşteriler = null;

        class MalzemeKullanımDetayı_
        {
            public string Adı = null;
            public string KlasörAdı = null;

            public Depo_ DevamEden = null;
            //public Dictionary<string, Depo_> Arşiv = null;
            //public string[] Liste_Arşiv = null;

            public MalzemeKullanımDetayı_(string Adı)
            {
                this.Adı = Adı;
            }
        }
        static Dictionary<string, MalzemeKullanımDetayı_> MalzemeKullanımDetayları = null;

        class _Sıralayıcı_SeriNoDalı_TeslimEdildi_EskidenYeniye : IComparer<IDepo_Eleman>
        {
            public int Compare(IDepo_Eleman x, IDepo_Eleman y)
            {
                DateTime x_t = x.Oku_TarihSaat(null, default, 3);
                DateTime y_t = y.Oku_TarihSaat(null, default, 3);

                if (x_t > y_t) return 1;
                else if (x_t == y_t) return 0;
                else return -1;
            }
        }
        class _Talep_Ayıkla_ÖdemeDalı
        {
            public double AltToplam, KDV_Oranı, KDV_Hesaplanan, Genel_Toplam;
            public string Tarih_ÖdemeTalebi, Tarih_Ödendi, Notlar;
            public bool MüşteriBorçluMu;

            public double İlaveÖdeme;
            public string İlaveÖdeme_Açıklaması;
            public bool İlaveÖdeme_İşlemiVarmı;

            public double ÖnÖdeme_MevcutÖnÖdeme, ÖnÖdeme_AlınanÖdeme, ÖnÖdeme_İşlemSonrasıÖnÖdeme;
            public bool ÖnÖdeme_İşlemiVarmı;

            //Gecici - KAYDEDİLMEYECEK - Ödeme Talebinin Yazdırılabilmesi için Detaylar  
            public double Gecici_EnSonÖdemeDokümanı_AlınanÖdeme, Gecici_EnSonÖdemeDokümanı_GenelToplam, Gecici_EnSonÖdemeDokümanı_ÖnÖdeme, Gecici_Güncel_ÖnÖdeme, Gecici_Güncel_İşlemSonrasıMüşteriBorcu;
            public bool Gecici_İşlemiVarmı;

            public _Talep_Ayıkla_ÖdemeDalı(IDepo_Eleman ÖdemeDalı)
            {
                AltToplam = ÖdemeDalı.Oku_Sayı("Alt Toplam");
                KDV_Oranı = ÖdemeDalı.Oku_Sayı("Alt Toplam", 0, 1);
                KDV_Hesaplanan = KDV_Oranı == 0 ? 0 : AltToplam / 100 * KDV_Oranı;

                İlaveÖdeme_Açıklaması = ÖdemeDalı.Oku("İlave Ödeme");
                İlaveÖdeme = ÖdemeDalı.Oku_Sayı("İlave Ödeme", 0, 1);
                İlaveÖdeme_İşlemiVarmı = İlaveÖdeme_Açıklaması.DoluMu(true);

                Genel_Toplam = İlaveÖdeme + AltToplam + KDV_Hesaplanan;

                Tarih_ÖdemeTalebi = ÖdemeDalı[0];
                Tarih_Ödendi = ÖdemeDalı[1];
                Notlar = ÖdemeDalı[2];

                if (ÖdemeDalı.Oku("Ön Ödeme").DoluMu())
                {
                    ÖnÖdeme_İşlemiVarmı = true;
                    ÖnÖdeme_MevcutÖnÖdeme = ÖdemeDalı.Oku_Sayı("Ön Ödeme", 0, 0);
                    ÖnÖdeme_AlınanÖdeme = ÖdemeDalı.Oku_Sayı("Ön Ödeme", 0, 1);

                    ÖnÖdeme_İşlemSonrasıÖnÖdeme = ÖnÖdeme_MevcutÖnÖdeme + ÖnÖdeme_AlınanÖdeme - Genel_Toplam;
                    MüşteriBorçluMu = ÖnÖdeme_İşlemSonrasıÖnÖdeme < 0;
                }
                else if (ÖdemeDalı.Oku("Müşteri_ÖdemeTalebi_GeciciDetaylarıEkle").DoluMu())
                {
                    Gecici_İşlemiVarmı = true;
                    Gecici_EnSonÖdemeDokümanı_AlınanÖdeme = ÖdemeDalı.Oku_Sayı("Müşteri_ÖdemeTalebi_GeciciDetaylarıEkle", 0, 0);
                    Gecici_EnSonÖdemeDokümanı_GenelToplam = ÖdemeDalı.Oku_Sayı("Müşteri_ÖdemeTalebi_GeciciDetaylarıEkle", 0, 1);
                    Gecici_EnSonÖdemeDokümanı_ÖnÖdeme = ÖdemeDalı.Oku_Sayı("Müşteri_ÖdemeTalebi_GeciciDetaylarıEkle", 0, 2);
                    Gecici_Güncel_ÖnÖdeme = ÖdemeDalı.Oku_Sayı("Müşteri_ÖdemeTalebi_GeciciDetaylarıEkle", 0, 3);

                    Gecici_Güncel_İşlemSonrasıMüşteriBorcu =  Genel_Toplam - Gecici_Güncel_ÖnÖdeme;
                    MüşteriBorçluMu = Gecici_Güncel_İşlemSonrasıMüşteriBorcu > 0;
                }
            }
        }
        #endregion

        #region Depo + Sıkıştırma + Şifreleme
        static DahaCokKarmasiklastirma_ DaÇoKa = new DahaCokKarmasiklastirma_();

        static bool Depo_DosyaVarMı(string DosyaYolu, string BankaYolu = null)
        {
            DosyaYolu = (BankaYolu == null ? Ortak.Klasör_Banka : BankaYolu) + DosyaYolu + ".mup";
            return File.Exists(DosyaYolu);
        }
        static void Depo_Kaydet(string DosyaYolu, Depo_ Depo, string BankaYolu = null)
        {
            //Depo
            string içerik = Depo.YazıyaDönüştür();
            if (string.IsNullOrEmpty(içerik)) içerik = " ";

#if DEBUG
            byte[] çıktı = içerik.BaytDizisine();
#else
            byte[] çıktı = Dosya_SıkıştırKarıştır(içerik.BaytDizisine());
            #endif

            DosyaYolu = (BankaYolu == null ? Ortak.Klasör_Banka : BankaYolu)  + DosyaYolu + ".mup";
            string yedek_dosya_yolu = DosyaYolu + ".yedek";
            Klasör.Oluştur(Path.GetDirectoryName(DosyaYolu));

            if (!File.Exists(yedek_dosya_yolu) && File.Exists(DosyaYolu)) File.Move(DosyaYolu, yedek_dosya_yolu);
            
            File.WriteAllBytes(DosyaYolu, çıktı);

            Dosya.Sil(yedek_dosya_yolu);

            Depo.EnAzBir_ElemanAdıVeyaİçeriği_Değişti = false;
        }
        static Depo_ Depo_Aç(string DosyaYolu, string BankaYolu = null)
        {
            DosyaYolu = (BankaYolu == null ? Ortak.Klasör_Banka : BankaYolu) + DosyaYolu + ".mup";
            if (!File.Exists(DosyaYolu)) return new Depo_();

#if DEBUG
            return new Depo_(File.ReadAllBytes(DosyaYolu).Yazıya());
#else
            Depo_ Depo = null;
            byte[] çıktı = Dosya_DüzeltAç(File.ReadAllBytes(DosyaYolu));
            string okunan = çıktı.Yazıya();
            if (!string.IsNullOrEmpty(okunan)) Depo = new Depo_(okunan);
            if (Depo == null) throw new Exception(DosyaYolu + " dosyası arızalı");

            return Depo;
#endif
        }
        public static void BankayıDışarıyaAç()
        {
            Klasör.Oluştur(Ortak.Klasör_Gecici);

            foreach (string dsy in Directory.GetFiles(Ortak.Klasör_Banka, "*.mup", SearchOption.AllDirectories))
            {
                string dsy_adı = dsy.Substring(Ortak.Klasör_Banka.Length);
                Depo_ d = Depo_Aç(dsy_adı.Remove(dsy_adı.LastIndexOf('.')));
                dsy_adı = "BankayıDışarıyaAç\\" + dsy_adı;
                Klasör.Oluştur(Path.GetDirectoryName(dsy_adı));
                File.WriteAllText(dsy_adı, d.YazıyaDönüştür());
            }
        }
        static byte[] Dosya_SıkıştırKarıştır(byte[] İçerik)
        {
            if (İçerik == null || İçerik.Length == 0) return İçerik;

            string tarihsaat = D_TarihSaat.Yazıya(DateTime.Now, D_TarihSaat.Şablon_DosyaAdı);
            byte[] dizi_doko = D_Yazı.BaytDizisine(tarihsaat + ";" + DoğrulamaKodu.Üret.BaytDizisinden(İçerik).HexYazıya());

            //Ara dosya
            string Gecici_zip_dosyası = Path.GetRandomFileName();
            while (File.Exists(Ortak.Klasör_Gecici + Gecici_zip_dosyası)) Gecici_zip_dosyası = Path.GetRandomFileName();
            Gecici_zip_dosyası = Ortak.Klasör_Gecici + Gecici_zip_dosyası;

            //Sıkıştırma
            using (ZipArchive archive = ZipFile.Open(Gecici_zip_dosyası, ZipArchiveMode.Create))
            {
                ZipArchiveEntry biri = archive.CreateEntry("doko", CompressionLevel.Optimal);
                using (Stream H = biri.Open())
                {
                    H.Write(dizi_doko, 0, dizi_doko.Length);
                }

                biri = archive.CreateEntry(tarihsaat, CompressionLevel.Optimal);
                using (Stream H = biri.Open())
                {
                    H.Write(İçerik, 0, İçerik.Length);
                }
            }

            //Şifreleme
            byte[] çıktı = DaÇoKa.Karıştır(File.ReadAllBytes(Gecici_zip_dosyası), Parola.Dizi);
            Dosya.Sil(Gecici_zip_dosyası);

            return çıktı;
        }
        static byte[] Dosya_DüzeltAç(byte[] İçerik)
        {
            if (İçerik == null || İçerik.Length == 0) return İçerik;

            //Şifre çözme
            byte[] çıktı = DaÇoKa.Düzelt(İçerik, Parola.Dizi);

            //Ara dosya
            string Gecici_zip_dosyası = Path.GetRandomFileName();
            while (File.Exists(Ortak.Klasör_Gecici + Gecici_zip_dosyası)) Gecici_zip_dosyası = Path.GetRandomFileName();
            Gecici_zip_dosyası = Ortak.Klasör_Gecici + Gecici_zip_dosyası;
            File.WriteAllBytes(Gecici_zip_dosyası, çıktı);
            çıktı = null;

            //Açma
            using (ZipArchive Arşiv = ZipFile.OpenRead(Gecici_zip_dosyası))
            {
                byte[] dizi_içerik = null, dizi_doko = null;
                int adt = 0;

                ZipArchiveEntry Biri = Arşiv.GetEntry("doko");
                if (Biri != null)
                {
                    using (Stream Akış = Biri.Open())
                    {
                        dizi_doko = new byte[Biri.Length];
                        adt = Akış.Read(dizi_doko, 0, (int)Biri.Length);
                    }
                }

                if (dizi_doko != null && dizi_doko.Length > 0 && dizi_doko.Length == adt)
                {
                    string doko = dizi_doko.Yazıya();
                    string[] bölünmüş = doko.Split(';');
                    if (bölünmüş.Length == 2)
                    {
                        string tarih_saat = bölünmüş[0];
                        doko = bölünmüş[1];
                        if (!string.IsNullOrEmpty(doko))
                        {
                            Biri = Arşiv.GetEntry(tarih_saat);
                            if (Biri != null)
                            {
                                using (Stream Akış = Biri.Open())
                                {
                                    dizi_içerik = new byte[Biri.Length];
                                    adt = Akış.Read(dizi_içerik, 0, (int)Biri.Length);
                                }
                            }

                            if (dizi_içerik != null && dizi_içerik.Length == adt)
                            {
                                if (doko == DoğrulamaKodu.Üret.BaytDizisinden(dizi_içerik).HexYazıya())
                                {
                                    çıktı = dizi_içerik;
                                }
                            }
                        }
                    }
                }
            }
            Dosya.Sil(Gecici_zip_dosyası);

            return çıktı;
        }
        #endregion

        #region Yedekleme
        public static bool Yedekleme_Tümü_Çalışıyor = false;
        public static bool Yedekleme_EnAz1Kez_Değişiklikler_Kaydedildi = false;
        public static string Yedekleme_Hatalar = null;

        static FileSystemWatcher[] Yedekleme_izleyiciler = null;
        static string _Yedekleme_İzleyici_DeğişiklikYapıldı = null;
        static string Yedekleme_İzleyici_DeğişiklikYapıldı
        {
            get
            {
                return _Yedekleme_İzleyici_DeğişiklikYapıldı;
            }
            set
            {
                _Yedekleme_İzleyici_DeğişiklikYapıldı = value;
                Ortak.AnaEkran.Invoke(new Action(() => 
                {
                    if (_Yedekleme_İzleyici_DeğişiklikYapıldı == null)
                    {
                        Ortak.AnaEkran.YedekleKapat.BackColor = System.Drawing.Color.Transparent;
                        Ortak.AnaEkran.YedekleKapat.Text = "Yedekle ve kapat";
                    }
                    else
                    {
                        Ortak.AnaEkran.YedekleKapat.BackColor = System.Drawing.Color.Khaki;
                        Ortak.AnaEkran.YedekleKapat.Text = "Yeniden başlat";
                    }
                }));
            }
        }

        static void Yedekleme_İzleyici_Başlat()
        {
            Yedekleme_İzleyici_Durdur();

            List<FileSystemWatcher> liste = new List<FileSystemWatcher>();
            for (int i = 0; i < Ortak.Kullanıcı_Klasör_Yedek.Length; i++)
            {
                Günlük.Ekle("Yedekleme_İzleyici_Başlat deneniyor " + i + " " + Ortak.Kullanıcı_Klasör_Yedek[i]);

                if (string.IsNullOrEmpty(Ortak.Kullanıcı_Klasör_Yedek[i])) continue;
                if (!Klasör.Oluştur(Ortak.Kullanıcı_Klasör_Yedek[i]))
                {
                    Günlük.Ekle("Yedekleme_İzleyici_Başlat oluşturulamadı " + Ortak.Kullanıcı_Klasör_Yedek[i]);
                    MessageBox.Show("Klasör oluşturulamadı, belirtilen konuma yedek alınmayacaktır" + Environment.NewLine + Environment.NewLine +
                        Ortak.Kullanıcı_Klasör_Yedek[i], Ortak.AnaEkran.Text + " Yedekleme");
                    continue;
                }

                FileSystemWatcher yeni = new FileSystemWatcher(Ortak.Kullanıcı_Klasör_Yedek[i], "*.mup");
                yeni.Changed += Yeni_Created_Changed_Deleted;
                yeni.Created += Yeni_Created_Changed_Deleted;
                yeni.Error += Yeni_Error;
                yeni.Deleted += Yeni_Created_Changed_Deleted;
                yeni.Renamed += Yeni_Renamed;
                yeni.IncludeSubdirectories = true;
                yeni.EnableRaisingEvents = true;
                liste.Add(yeni);
            }

            if (liste.Count > 0) Yedekleme_izleyiciler = liste.ToArray();
            else Yedekleme_izleyiciler = null;

            void Yeni_Renamed(object sender, RenamedEventArgs e)
            {
                Yedekleme_İzleyici_DeğişiklikYapıldı = e.FullPath;
                Günlük.Ekle(e.ChangeType.ToString() + " " + e.OldFullPath + " " + e.FullPath);
            }
            void Yeni_Created_Changed_Deleted(object sender, FileSystemEventArgs e)
            {
                Yedekleme_İzleyici_DeğişiklikYapıldı = e.FullPath;
                Günlük.Ekle(e.ChangeType.ToString() + " " + e.FullPath);
            }
            void Yeni_Error(object sender, ErrorEventArgs e)
            {
                Yedekleme_İzleyici_DeğişiklikYapıldı = e.GetException().Message;
                Günlük.Ekle(e.GetException().ToString());
            }
        }
        static void Yedekleme_İzleyici_Durdur()
        {
            if (Yedekleme_izleyiciler != null)
            {
                foreach (var a in Yedekleme_izleyiciler) a.Dispose();
                Yedekleme_izleyiciler = null;
            }

            Yedekleme_İzleyici_DeğişiklikYapıldı = null;
        }

        public static void Yedekle_Tümü()
        {
            if (Yedekleme_Tümü_Çalışıyor || !Yedekleme_EnAz1Kez_Değişiklikler_Kaydedildi) return;
            Yedekleme_Tümü_Çalışıyor = true;
            Yedekleme_Hatalar = null;

            if (Yedekleme_İzleyici_DeğişiklikYapıldı != null)
            {
                string soru = "Yedeğinizin içeriği uygulama haricinde değiştirildi." + Environment.NewLine +
                "Uygulamayı yeniden başlatmak ister misiniz?" + Environment.NewLine + Environment.NewLine +
                "Evet : Tavsiye edilir." + Environment.NewLine +
                "Hayır : İşlemlerinizin sonuçları değişen yedeğin üzerine kaydedilir." + Environment.NewLine + Environment.NewLine +
                Yedekleme_İzleyici_DeğişiklikYapıldı;

                DialogResult Dr = MessageBox.Show(soru, Ortak.AnaEkran.Text + " Yedekleme", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                if (Dr == DialogResult.Yes) 
                {
                    System.Windows.Forms.Application.Restart();
                    Yedekleme_Tümü_Çalışıyor = false;
                    return;
                }
                
                Yedekleme_İzleyici_DeğişiklikYapıldı = null;
            }

            System.Threading.Tasks.Task.Run(() =>
            {
                try
                {
                    Klasör_ ydk_ler = new Klasör_(Ortak.Klasör_İçYedek, Filtre_Dosya: "*.zip", EşZamanlıİşlemSayısı: Ortak.EşZamanlıİşlemSayısı);
                    ydk_ler.Dosya_Sil_SayısınaVeBoyutunaGöre(100, 500 * 1024 * 1024 /*500MB*/, Ortak.EşZamanlıİşlemSayısı);
                    ydk_ler.Güncelle(Ortak.Klasör_İçYedek, Filtre_Dosya: "*.zip", EşZamanlıİşlemSayısı: Ortak.EşZamanlıİşlemSayısı);

                    bool yedekle = false;
                    if (ydk_ler.Dosyalar.Count == 0) yedekle = true;
                    else
                    {
                        ydk_ler.Sırala_EskidenYeniye();

                        Klasör_ son_ydk = SıkıştırılmışDosya.Listele(ydk_ler.Kök + "\\" + ydk_ler.Dosyalar.Last().Yolu);
                        Klasör_ güncel = new Klasör_(Ortak.Klasör_Banka, EşZamanlıİşlemSayısı: Ortak.EşZamanlıİşlemSayısı);
                        Klasör_.Farklılık_ farklar = güncel.Karşılaştır(son_ydk);
                        if (farklar.FarklılıkSayısı > 0)
                        {
                            int içeriği_farklı_dosya_Sayısı = 0;
                            foreach (Klasör_.Fark_Dosya_ a in farklar.Dosyalar)
                            {
                                if (!a.Aynı_Doğrulama_Kodu)
                                {
                                    içeriği_farklı_dosya_Sayısı++;
                                    break;
                                }
                            }
                            if (içeriği_farklı_dosya_Sayısı > 0) yedekle = true;
                        }
                    }

                    if (yedekle)
                    {
                        string k = Ortak.Klasör_Banka;
                        string h = Ortak.Klasör_İçYedek + D_TarihSaat.Yazıya(DateTime.Now, ArgeMup.HazirKod.Dönüştürme.D_TarihSaat.Şablon_DosyaAdı2) + ".zip";

                        SıkıştırılmışDosya.Klasörden(k, h);
                    }

                    Yedekleme_İzleyici_Durdur();
                    if (Ortak.Kullanıcı_Klasör_Yedek.Length > 0)
                    {
                        for (int i = 0; i < Ortak.Kullanıcı_Klasör_Yedek.Length; i++)
                        {
                            if (string.IsNullOrEmpty(Ortak.Kullanıcı_Klasör_Yedek[i])) continue;

                            bool sonuç = true;
                            sonuç &= Ortak.Klasör_TamKopya(Ortak.Klasör_Banka, Ortak.Kullanıcı_Klasör_Yedek[i] + "Banka");
                            sonuç &= Ortak.Klasör_TamKopya(Ortak.Klasör_KullanıcıDosyaları, Ortak.Kullanıcı_Klasör_Yedek[i] + "Kullanıcı Dosyaları");
                            sonuç &= Ortak.Klasör_TamKopya(Ortak.Klasör_İçYedek, Ortak.Kullanıcı_Klasör_Yedek[i] + "Yedek");
                            sonuç &= Dosya.Kopyala(Kendi.DosyaYolu, Ortak.Kullanıcı_Klasör_Yedek[i] + Kendi.DosyaAdı);

                            if (!sonuç) Yedekleme_Hatalar += ("Yedek no : " + (i+1) + " yedekleme başarısız").Günlük() + Environment.NewLine;
                        }

                        Yedekleme_İzleyici_Başlat();
                    }

                    Yedekleme_EnAz1Kez_Değişiklikler_Kaydedildi = false;
                }
                catch (Exception ex) { Yedekleme_Hatalar += ex.Günlük().Message + Environment.NewLine; }

                Yedekleme_Tümü_Çalışıyor = false;
            });
        }
        public static void Yedekle_Banka()
        {
            if (!Ortak.Klasör_TamKopya(Ortak.Klasör_Banka, Ortak.Klasör_Banka2))
            {
                throw new Exception("Yedekle_Banka>" + Ortak.Klasör_Banka + ">" + Ortak.Klasör_Banka2);
            }
        }
        public static void Yedekle_Banka_Kurtar()
        {
            Değişiklikler_TamponuSıfırla();

            DoğrulamaKodu.KontrolEt.Durum_ snç = DoğrulamaKodu.KontrolEt.Klasör(Ortak.Klasör_Banka, SearchOption.AllDirectories, Parola.Yazı, Ortak.EşZamanlıİşlemSayısı);
            if (snç != DoğrulamaKodu.KontrolEt.Durum_.Aynı)
            {
                snç = DoğrulamaKodu.KontrolEt.Klasör(Ortak.Klasör_Banka2, SearchOption.AllDirectories, Parola.Yazı, Ortak.EşZamanlıİşlemSayısı);
                if (snç != DoğrulamaKodu.KontrolEt.Durum_.Aynı)
                {
                    throw new Exception("Yedekle_Banka_Kurtar>Banka2>" + snç.ToString());
                }

                if (!Ortak.Klasör_TamKopya(Ortak.Klasör_Banka2, Ortak.Klasör_Banka))
                {
                    throw new Exception("Yedekle_Banka_Kurtar>Banka2>Banka");
                }

                snç = DoğrulamaKodu.KontrolEt.Klasör(Ortak.Klasör_Banka, SearchOption.AllDirectories, Parola.Yazı, Ortak.EşZamanlıİşlemSayısı);
                if (snç != DoğrulamaKodu.KontrolEt.Durum_.Aynı)
                {
                    throw new Exception("Yedekle_Banka_Kurtar>Banka>" + snç.ToString());
                }
            }

            Günlük.Ekle("Yedekle_Banka_Kurtar>Başarılı");
        }
        static void Yedekle_DahaYeniYedekVarsa_KullanıcıyaSor()
        {
            string yenidenbaşlatmamesajı = null;

            //bize ait detayların okunması
            DateTime bizimki_saat = default;
            string bizimki_uygulamakimliği = "";
            Depo_ bizimki_d = Tablo(null, TabloTürü.Ayarlar);
            if (bizimki_d != null)
            {
                bizimki_saat = bizimki_d.Oku_TarihSaat("Son Banka Kayıt", default, 1);
                bizimki_uygulamakimliği = bizimki_d.Oku("Uygulama Kimliği");
            }

            Dictionary<string, DateTime> l = new Dictionary<string, DateTime>();

            for (int i = 0; i < Ortak.Kullanıcı_Klasör_Yedek.Length && yenidenbaşlatmamesajı == null; i++)
            {
                try
                {
                    string bnk_yolu = Ortak.Kullanıcı_Klasör_Yedek[i] + "Banka\\";
                    Günlük.Ekle("Deneniyor " + bnk_yolu);

                    if (string.IsNullOrEmpty(Ortak.Kullanıcı_Klasör_Yedek[i]) ||
                        DoğrulamaKodu.KontrolEt.Klasör(bnk_yolu,
                            SearchOption.AllDirectories, Parola.Yazı, Ortak.EşZamanlıİşlemSayısı) !=
                            DoğrulamaKodu.KontrolEt.Durum_.Aynı ||
                        !File.Exists(bnk_yolu + "Ay.mup")) continue;

                    Depo_ d = Depo_Aç("Ay", bnk_yolu);

                    //uygulama kimliği kontrolü
                    if (bizimki_uygulamakimliği != d.Oku("Uygulama Kimliği")) continue;

                    if (Sürüm.TamSayıya() < d.Oku_TamSayı("Son Banka Kayıt", 0, 2))
                    {
                        Ortak.Gösterge.Başlat("Banka sürümü daha yüksek bir yedek bulundu." + Environment.NewLine +
                            "Güncelleme tamamlanana kadar bekleyiniz.", false, null, 0);
                        while (!Ortak.AnaEkran.YeniYazılımKontrolü.KontrolTamamlandı && Ortak.Gösterge.Çalışsın) System.Threading.Thread.Sleep(500);

                        yenidenbaşlatmamesajı = "Banka sürümü daha yüksek bir yedek bulundu. Bizimki:" + Sürüm + ", Yedek:" + d.Oku("Son Banka Kayıt", null, 2);
                        throw new Exception();
                    }

                    //ayarlar dan son kayıt tarihini al
                    l.Add(bnk_yolu, d.Oku_TarihSaat("Son Banka Kayıt", default, 1));
                    Günlük.Ekle("Kabul edildi " + d.Oku("Son Banka Kayıt", default, 1));
                }
                catch (Exception) { }

                if (yenidenbaşlatmamesajı.DoluMu()) throw new Exception(yenidenbaşlatmamesajı);
            }
            if (l.Count == 0) return;

            l = l.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            Günlük.Ekle("İşleniyor " + l.Values.First().Yazıya());

            //karşılaştırma
            if (l.Values.First() > bizimki_saat)
            {
                Depo_ d = Depo_Aç("Ay", l.Keys.First());

                string soru = "Mevcut kayıtlarınızdan daha yeni bir yedek bulundu." + Environment.NewLine + Environment.NewLine +
                    "Mevcut kullanıcı : " + bizimki_d.Oku("Son Banka Kayıt") + Environment.NewLine +
                    "Diğer kullanıcı : " + d.Oku("Son Banka Kayıt") + Environment.NewLine + Environment.NewLine +
                    "Mevcut kayıt saati : " + bizimki_saat.Yazıya() + Environment.NewLine +
                    "Diğer kayıt saati : " + l.Values.First().Yazıya() + Environment.NewLine + Environment.NewLine +
                    "Mevcut kayıtlarınız yerine DAHA YENİ olan DİĞER kayıtları kullanarak devam etmek ister misiniz?";

                DialogResult Dr = MessageBox.Show(soru, Ortak.AnaEkran.Text + " Mevcut kayılarınız daha eski", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                Dr.Günlük("Mevcut kayıtlarınızdan daha yeni bir yedek bulundu. ");
                if (Dr == DialogResult.No) return;
                string hata;

                // banka -> banka2
                if (!Ortak.Klasör_TamKopya(Ortak.Klasör_Banka, Ortak.Klasör_Banka2))
                {
                    throw new Exception("AslınaUygunHaleGetir>" + Ortak.Klasör_Banka + ">" + Ortak.Klasör_Banka2);
                }

                // yedek -> gecici
                string kla_gecici = Ortak.Klasör_Gecici + Path.GetRandomFileName();
                if (!Ortak.Klasör_TamKopya(l.Keys.First(), kla_gecici))
                {
                    hata = "1.AslınaUygunHaleGetir>" + l.Keys.First() + ">" + kla_gecici;
                    goto HatalıCıkış;
                }

                // gecici 2. kez doko
                DoğrulamaKodu.KontrolEt.Durum_ doko = DoğrulamaKodu.KontrolEt.Klasör(kla_gecici, SearchOption.AllDirectories, Parola.Yazı, Ortak.EşZamanlıİşlemSayısı);
                if (doko != DoğrulamaKodu.KontrolEt.Durum_.Aynı)
                {
                    hata = "2.DoğrulamaKodu.KontrolEt.Klasör>" + doko.ToString() + ">" + kla_gecici;
                    goto HatalıCıkış;
                }

                // gecici -> banka
                if (!Ortak.Klasör_TamKopya(kla_gecici, Ortak.Klasör_Banka))
                {
                    hata = "3.AslınaUygunHaleGetir>" + kla_gecici + ">" + Ortak.Klasör_Banka;
                    goto HatalıCıkış;
                }

                // banka 2. kez doko
                doko = DoğrulamaKodu.KontrolEt.Klasör(Ortak.Klasör_Banka, SearchOption.AllDirectories, Parola.Yazı, Ortak.EşZamanlıİşlemSayısı);
                if (doko != DoğrulamaKodu.KontrolEt.Durum_.Aynı)
                {
                    hata = "4.DoğrulamaKodu.KontrolEt.Klasör>" + doko.ToString() + ">" + Ortak.Klasör_Banka;
                    goto HatalıCıkış;
                }

                hata = Klasör.ÜstKlasör(l.Keys.First()) + "\\";
                Ortak.Klasör_TamKopya(hata + "Kullanıcı Dosyaları", Ortak.Klasör_KullanıcıDosyaları);
                Ortak.Klasör_TamKopya(hata + "Yedek", Ortak.Klasör_İçYedek);
                Klasör.Sil(kla_gecici);
                Değişiklikler_TamponuSıfırla();
                return;

            HatalıCıkış:
                // banka2 -> banka
                if (Ortak.Klasör_TamKopya(Ortak.Klasör_Banka2, Ortak.Klasör_Banka))
                {
                    hata = "Yedekleme başarısız oldu fakat mevcut kayıtlarınız sağlam," + Environment.NewLine +
                        "uygulamayı kapatıp açmak yardımcı olabilir" + Environment.NewLine + Environment.NewLine +
                        "Hata : " + hata;
                }
                else
                {
                    hata = "Yedekleme başarısız oldu, lütfen uygulamayı kapatıp YEDEK klasörünüdeki en yeni yedeği BANKA içerisine kopyalayınız" + Environment.NewLine + Environment.NewLine +
                        "YEDEK : " + Ortak.Klasör_İçYedek + Environment.NewLine +
                        "BANKA : " + Ortak.Klasör_Banka + Environment.NewLine + Environment.NewLine +
                        "Hata : " + hata;
                }
                
                try { MessageBox.Show(hata, "Yedekleme"); } catch (Exception) { }
                throw new Exception(hata).Günlük();
            }
        }
        #endregion
    }
}
