using ArgeMup.HazirKod;
using ArgeMup.HazirKod.Dönüştürme;
using ArgeMup.HazirKod.Ekİşlemler;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        public static void Giriş_İşlemleri()
        {
            Klasör.Oluştur(Ortak.Klasör_Banka);
            Klasör.Oluştur(Ortak.Klasör_İçYedek);
            Klasör.Oluştur(Ortak.Klasör_Diğer);
            Klasör.Oluştur(Ortak.Klasör_Gecici);

            Depo_ d = Tablo(null, TabloTürü.Ayarlar);
            if (d != null)
            {
                Ortak.Klasör_KullanıcıYedeği = d.Oku("Klasör/Yedek");
                Ortak.Klasör_Pdf = d.Oku("Klasör/Pdf", Ortak.Klasör_Pdf);
                Ortak.AçılışEkranıİçinParaloİste = d.Oku_Bit("AçılışEkranıİçinParaloİste", true);
            }

            Klasör.Oluştur(Ortak.Klasör_KullanıcıYedeği);
            Klasör.Oluştur(Ortak.Klasör_Pdf);
			
            DoğrulamaKodu.KontrolEt.Durum_ snç = DoğrulamaKodu.KontrolEt.Klasör(Ortak.Klasör_Banka);
            switch (snç)
            {
                case DoğrulamaKodu.KontrolEt.Durum_.Aynı:
                    #region yedekleme
                    Klasör_ ydk_ler = new Klasör_(Ortak.Klasör_İçYedek, Filtre_Dosya: "*.zip");
                    ydk_ler.Dosya_Sil_SayısınaVeBoyutunaGöre(15, 1024 * 1024 * 1024 /*1GB*/);
                    ydk_ler.Güncelle(Ortak.Klasör_İçYedek, Filtre_Dosya: "*.zip");

                    bool yedekle = false;
                    if (ydk_ler.Dosyalar.Count == 0) yedekle = true;
                    else
                    {
                        ydk_ler.Sırala_EskidenYeniye();

                        Klasör_ son_ydk = SıkıştırılmışDosya.Listele(ydk_ler.Kök + "\\" + ydk_ler.Dosyalar.Last().Yolu);
                        Klasör_ güncel = new Klasör_(Ortak.Klasör_Banka);
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
                        string h = Ortak.Klasör_İçYedek + D_TarihSaat.Yazıya(DateTime.Now, D_TarihSaat.Şablon_DosyaAdı) + ".zip";

                        SıkıştırılmışDosya.Klasörden(k, h);
                    }

                    if (!string.IsNullOrEmpty(Ortak.Klasör_KullanıcıYedeği))
                    {
                        Klasör.Kopyala(Ortak.Klasör_Banka, Ortak.Klasör_KullanıcıYedeği + "Banka");
                        Klasör.Kopyala(Ortak.Klasör_Diğer, Ortak.Klasör_KullanıcıYedeği + "Diğer");
                        Klasör.Kopyala(Ortak.Klasör_İçYedek, Ortak.Klasör_KullanıcıYedeği + "Yedek");
                    }

                    Klasör.AslınaUygunHaleGetir(Ortak.Klasör_Banka, Ortak.Klasör_Banka2, true);
                    #endregion
                    break;

                case DoğrulamaKodu.KontrolEt.Durum_.DoğrulamaDosyasıYok:
#if !DEBUG
                    Klasör_ kls = new Klasör_(Ortak.Klasör_Banka);
                    if (kls.Dosyalar.Count > 0) throw new Exception("Büyük Hata A");
#endif
                    break;

                case DoğrulamaKodu.KontrolEt.Durum_.DoğrulamaDosyasıİçeriğiHatalı:
                case DoğrulamaKodu.KontrolEt.Durum_.Farklı:
                case DoğrulamaKodu.KontrolEt.Durum_.FazlaKlasörVeyaDosyaVar:
                    throw new Exception("Banka klasörünün içeriği hatalı" + Environment.NewLine +
                        "Son yaptığınız işlem yarım kalmış olabilir" + Environment.NewLine + 
                        "Yedeği kullanmak için" + Environment.NewLine +
                        "Banka klasörünün içeriğini tamamen silebilir ve" + Environment.NewLine +
                        "Yedek klasöründen en son tarihli yedeğini Banka klasörü içerisine çıkarabilirsiniz");
            }
        }
        public static void Çıkış_İşlemleri()
        {
            Klasör.Sil(Ortak.Klasör_Gecici);
            Klasör.Sil_İçiBoşOlanları(Ortak.Klasör_Banka);
        }
        public static Depo_ ÖrnekMüşteriTablosuOluştur()
        {
            Depo_ Depo = new Depo_();
            Depo.Yaz("Müşteri", "Örnek Müşteri Adı");

            int örnek_iş_sayısı = 0;
            DateTime t = DateTime.Now.AddYears(-1);
            for (int i = 1; i <= 35; i++) //talep sayısı
            {
                if (++örnek_iş_sayısı > 6) örnek_iş_sayısı = 1;

                Depo.Yaz("Talepler/SeriNo" + i, "Örnek Hasta Adı " + i, 0);
                if (örnek_iş_sayısı == 1 || örnek_iş_sayısı == 5) Depo.Yaz("Talepler/SeriNo" + i, i, 1);

                for (int ii = 1; ii <= örnek_iş_sayısı; ii++)
                {
                    Depo.Yaz("Talepler/SeriNo" + i + "/" + ii, "Örnek İş Türü " + ii, 0);
                    Depo.Yaz("Talepler/SeriNo" + i + "/" + ii, t, 1);
                    Depo.Yaz("Talepler/SeriNo" + i + "/" + ii, i * ii * 10, 2);

                    t = t.AddDays(1);
                }
            }
            Depo.Yaz("Ödeme", t, 1);
            Depo.Yaz("Ödeme", "Fatura No : ASDF123466", 2);
            Depo.Yaz("Ödeme/Alt Toplam", 123456789);
            Depo.Yaz("Ödeme/İlave Ödeme", "Örnek İlave ödeme açıklaması", 0);
            Depo.Yaz("Ödeme/İlave Ödeme", 35.79, 1);

            return Depo;
        }

        public static Depo_ Tablo(string Müşteri, TabloTürü Tür, bool YoksaOluştur = false, string EkTanım = null)
        {
            Depo_ depo = null;
            if (Müşteri == null)
            {
                switch (Tür)
                {
                    case TabloTürü.Ayarlar:
                        if (Ayarlar == null) 
                        {
                            if (!Depo_DosyaVarMı("Ayarlar"))
                            {
                                if (!YoksaOluştur) return null;
                            }

                            Ayarlar = Depo_Aç("Ayarlar");
                        }
                        depo = Ayarlar;
                        break;

                    case TabloTürü.Ücretler:
                        if (Ücretler == null)
                        {
                            if (!Depo_DosyaVarMı("Ücretler"))
                            {
                                if (!YoksaOluştur) return null;
                            }

                            Ücretler = Depo_Aç("Ücretler");
                        }
                        depo = Ücretler;
                        break;

                    case TabloTürü.İşTürleri:
                        if (İşTürleri == null)
                        {
                            if (!Depo_DosyaVarMı("İş Türleri"))
                            {
                                if (!YoksaOluştur) return null;
                            }

                            İşTürleri = Depo_Aç("İş Türleri");
                        }
                        depo = İşTürleri;
                        break;

                    case TabloTürü.Malzemeler:
                        if (Malzemeler == null)
                        {
                            if (!Depo_DosyaVarMı("Malzemeler"))
                            {
                                if (!YoksaOluştur) return null;
                            }

                            Malzemeler = Depo_Aç("Malzemeler");
                        }
                        depo = Malzemeler;
                        break;

                    default: 
                        return null;
                }
            }
            else
            {
                Müşteri_ m = null;
                if (Müşteriler == null) Müşteriler = new Dictionary<string, Müşteri_>();

                if (!Müşteriler.TryGetValue(Müşteri, out m))
                {
                    m = new Müşteri_(Müşteri);
                    Müşteriler.Add(Müşteri, m);
                }

                switch (Tür)
                {
                    case TabloTürü.DevamEden:
                        if (m.DevamEden == null)
                        {
                            if (!Depo_DosyaVarMı(Müşteri + "\\Devam Eden"))
                            {
                                if (!YoksaOluştur) return null;
                            }

                            m.DevamEden = Depo_Aç(Müşteri + "\\Devam Eden");
                        }
                        depo = m.DevamEden;
                        break;

                    case TabloTürü.Ücretler:
                        if (m.Ücretler == null)
                        {
                            if (!Depo_DosyaVarMı(Müşteri + "\\Ücretler"))
                            {
                                if (!YoksaOluştur) return null;
                            }

                            m.Ücretler = Depo_Aç(Müşteri + "\\Ücretler");
                        }
                        depo = m.Ücretler;
                        break;

                    case TabloTürü.ÖdemeTalepEdildi:
                        if (m.ÖdemeTalepEdildi == null)
                        {
                            m.ÖdemeTalepEdildi = new Dictionary<string, Depo_>();
                        }

                        EkTanım = EkTanım.Replace(' ', '_');

                        if (!m.ÖdemeTalepEdildi.TryGetValue(EkTanım, out depo))
                        {
                            if (!Depo_DosyaVarMı(Müşteri + "\\Ödeme Talep Edildi_" + EkTanım))
                            {
                                if (!YoksaOluştur) return null;
                            }

                            depo = Depo_Aç(Müşteri + "\\Ödeme Talep Edildi_" + EkTanım);
                            m.ÖdemeTalepEdildi.Add(EkTanım, depo);
                        }
                        break;

                    case TabloTürü.Ödendi:
                        if (m.Ödendi == null)
                        {
                            m.Ödendi = new Dictionary<string, Depo_>();
                        }

                        EkTanım = EkTanım.Replace(' ', '_');

                        if (!m.Ödendi.TryGetValue(EkTanım, out depo))
                        {
                            if (!Depo_DosyaVarMı(Müşteri + "\\Ödendi_" + EkTanım))
                            {
                                if (!YoksaOluştur) return null;
                            }

                            depo = Depo_Aç(Müşteri + "\\Ödendi_" + EkTanım);
                            m.Ödendi.Add(EkTanım, depo);
                        }
                        break;

                    default:
                        return null;
                }

                //dosya adı ve başlık uyumluluğu kontrolü
                string müş = depo.Oku("Müşteri");
                if (string.IsNullOrEmpty(müş)) depo.Yaz("Müşteri", Müşteri);
                else if (müş != Müşteri) throw new Exception("Banka/" + Müşteri + "/" + Tür.ToString() + "/" + EkTanım + " anahtarının başlığı (" + müş + ") hatalı");
            }

            return depo;
        }
        public static IDepo_Eleman Tablo_Dal(string Müşteri, TabloTürü Tür, string Dal, bool YoksaOluştur = false, string EkTanım = null)
        {
            Depo_ d = Tablo(Müşteri, Tür, YoksaOluştur, EkTanım);
            if (d == null) return null;

            return d.Bul(Dal, YoksaOluştur);
        }
        public static string[] Dosya_Listele(string Müşteri, bool Ödendi_1_ÖdemeBekleyen_0)
        {
            if (Müşteriler == null) Müşteriler = new Dictionary<string, Müşteri_>();

            Müşteri_ m;
            if (!Müşteriler.TryGetValue(Müşteri, out m))
            {
                m = new Müşteri_(Müşteri);
                Müşteriler.Add(Müşteri, m);
            }

            if (Ödendi_1_ÖdemeBekleyen_0)
            {
                if (m.Liste_Ödendi == null)
                {
                    string[] l = Directory.GetFiles(Ortak.Klasör_Banka + Müşteri, "Ödendi_*.mup", SearchOption.TopDirectoryOnly);
                    m.Liste_Ödendi = new string[l.Length];

                    for (int i = 0; i < l.Length; i++)
                    {
                        m.Liste_Ödendi[i] = Path.GetFileNameWithoutExtension(l[i]).Substring(7 /*Ödendi_*/).Replace('_', ' ');
                    }
                }

                return m.Liste_Ödendi;
            }
            else
            {
                if (m.Liste_ÖdemeTalepEdildi == null)
                {
                    string[] l = Directory.GetFiles(Ortak.Klasör_Banka + Müşteri, "Ödeme Talep Edildi_*.mup", SearchOption.TopDirectoryOnly);
                    m.Liste_ÖdemeTalepEdildi = new string[l.Length];

                    for (int i = 0; i < l.Length; i++)
                    {
                        m.Liste_ÖdemeTalepEdildi[i] = Path.GetFileNameWithoutExtension(l[i]).Substring(19 /*Ödeme Talep Edildi_*/).Replace('_', ' ');
                    }
                }

                return m.Liste_ÖdemeTalepEdildi;
            }
        }

        public static List<string> Müşteri_Listele()
        {
            IDepo_Eleman d = Tablo_Dal(null, TabloTürü.Ayarlar, "Müşteriler");
            List<string> l = new List<string>();
            if (d == null) return l;

            foreach (IDepo_Eleman e in d.Elemanları)
            {
                if (e.İçiBoşOlduğuİçinSilinecek) continue;

                l.Add(e.Adı);
            }

            return l;
        }
        public static void Müşteri_Ekle(string Adı)
        {
            Tablo_Dal(null, TabloTürü.Ayarlar, "Müşteriler", true).Yaz(Adı, ".");
        }
        public static void Müşteri_Sil(string Adı)
        {
            IDepo_Eleman d = Tablo_Dal(null, TabloTürü.Ayarlar, "Müşteriler");
            if (d != null) d.Sil(Adı);
        }
        public static bool Müşteri_MevcutMu(string Adı)
        {
            return !string.IsNullOrWhiteSpace(Adı) && Tablo_Dal(null, TabloTürü.Ayarlar, "Müşteriler/" + Adı) != null;
        }
        
        public static List<string> İşTürü_Listele()
        {
            Depo_ d = Tablo(null, TabloTürü.İşTürleri);
            List<string> l = new List<string>();
            if (d == null) return l;

            foreach (IDepo_Eleman e in d.Elemanları)
            {
                if (e.İçiBoşOlduğuİçinSilinecek) continue;

                l.Add(e.Adı);
            }

            return l;
        }
        public static void İşTürü_Ekle(string Adı)
        {
            Tablo_Dal(null, TabloTürü.İşTürleri, Adı, true)[0] = ".";
        }
        public static void İşTürü_Sil(string Adı)
        {
            IDepo_Eleman d = Tablo_Dal(null, TabloTürü.İşTürleri, Adı);
            if (d != null) d.Sil(null);
        }
        public static bool İşTürü_MevcutMu(string Adı)
        {
            return !string.IsNullOrWhiteSpace(Adı) && Tablo_Dal(null, TabloTürü.İşTürleri, Adı) != null;
        }

        public static string SeriNo_Üret()
        {
            //<ay><yıl><no>
            //A:Ocak, B:Şubat, 2023:23, 2035:35, sn
            //Mart 2071 no:1 -> C711
            //Mart 3071 no:6789 -> C716789

            IDepo_Eleman o = Tablo_Dal(null, TabloTürü.Ayarlar, "Seri No", true);
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

        static List<IDepo_Eleman> Ücretler_Listele(string Müşteri)
        {
            IDepo_Eleman d = Tablo_Dal(Müşteri, TabloTürü.Ücretler, "Ücretler");
            List<IDepo_Eleman> l = new List<IDepo_Eleman>();
            if (d == null || d.Elemanları.Length == 0) return l;

            foreach (IDepo_Eleman e in d.Elemanları)
            {
                if (e.İçiBoşOlduğuİçinSilinecek) continue;

                l.Add(e);
            }

            return l;
        }
        public static void Ücretler_TablodaGöster(DataGridView Tablo, string Müşteri)
        {
            if (Tablo.SortedColumn != null)
            {
                DataGridViewColumn col = Tablo.SortedColumn;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                col.SortMode = DataGridViewColumnSortMode.Automatic;
            }

            List<IDepo_Eleman> Ücretler = Ücretler_Listele(Müşteri);

            for (int i = 0; i < Tablo.RowCount; i++)
            {
                string aranan = (string)Tablo[0, i].Value;
                IDepo_Eleman bulunan = Ücretler.Find(x => x.Adı == aranan);

                if (bulunan != null)
                {
                    Tablo[1, i].Value = bulunan[0];
                    Ücretler.Remove(bulunan);
                }
                else Tablo[1, i].Value = null;
            }

            if (Ücretler.Count > 0)
            {
                //dosya içinde önceden kalma girdi var

                IDepo_Eleman d = Tablo_Dal(Müşteri, TabloTürü.Ücretler, "Ücretler");
                foreach (IDepo_Eleman ü in Ücretler)
                {
                    d.Sil(ü.Adı);
                }
            }
        }
        public static void Ücretler_TablodakileriKaydet(DataGridView Tablo, string Müşteri)
        {
            IDepo_Eleman d = Tablo_Dal(Müşteri, TabloTürü.Ücretler, "Ücretler", true);

            for (int i = 0; i < Tablo.RowCount; i++)
            {
                string ü = (string)Tablo[1, i].Value;
                if (!string.IsNullOrEmpty(ü)) ü = ü.NoktalıSayıya().Yazıya();
                string iştürü = (string)Tablo[0, i].Value;

                d.Yaz(iştürü, ü);
            }
        }
        public static double Ücretler_BirimÜcret(string Müşteri, string İşTürü)
        {
            string ücret = null;

            //müşteriye özel ücret varmı diye bak
            IDepo_Eleman d = Tablo_Dal(Müşteri, TabloTürü.Ücretler, "Ücretler/" + İşTürü);
            if (d != null) ücret = d[0];
            
            if (string.IsNullOrEmpty(ücret))
            {
                d = Tablo_Dal(null, TabloTürü.Ücretler, "Ücretler/" + İşTürü);
                if (d != null) ücret = d[0];
            }

            if (string.IsNullOrEmpty(ücret)) return -1;
            return ücret.NoktalıSayıya();
        }

        public static void Talep_Ekle(string Müşteri, string Hasta, string İskonto, string Notlar, List<string> İşTürleri, List<string> Ücretler, List<string> GirişTarihleri, string SeriNo = null)
        {
            if (string.IsNullOrEmpty(SeriNo)) SeriNo = SeriNo_Üret();

            IDepo_Eleman d = Tablo_Dal(Müşteri, TabloTürü.DevamEden, "Talepler/" + SeriNo, true);
            d[0] = Hasta;
            d[1] = İskonto;
            d[2] = Notlar;
            d[3] = null; //teslim edilme tarihi

            for (int i = 0; i < İşTürleri.Count; i++)
            {
                string ad = (i + 1).ToString();
                d.Yaz(ad, İşTürleri[i], 0);
                d.Yaz(ad, GirişTarihleri[i], 1);
                d.Yaz(ad, Ücretler[i], 2);
            }
        }
        public static void Talep_Sil(string Müşteri, List<string> Seri_No_lar)
        {
            IDepo_Eleman d = Tablo_Dal(Müşteri, TabloTürü.DevamEden, "Talepler");
            if (d == null) return;

            foreach (string sn in Seri_No_lar)
            {
                d.Sil(sn);
            }
        }
        public static Banka_Tablo_ Talep_Listele(string Müşteri, TabloTürü Tür, string EkTanım = null)
        {
            IDepo_Eleman d;
            Banka_Tablo_ bt = new Banka_Tablo_(Müşteri);
            bt.Türü = Tür;

            switch (Tür)
            {
                case TabloTürü.DevamEden:
                    d = Tablo_Dal(Müşteri, TabloTürü.DevamEden, "Talepler");
                    if (d != null)
                    {
                        foreach (IDepo_Eleman e in d.Elemanları)
                        {
                            if (string.IsNullOrEmpty(e[3])) bt.Talepler.Add(e);
                        }
                    }
                    break;

                case TabloTürü.TeslimEdildi:
                    d = Tablo_Dal(Müşteri, TabloTürü.DevamEden, "Talepler");
                    if (d != null)
                    {
                        foreach (IDepo_Eleman e in d.Elemanları)
                        {
                            if (!string.IsNullOrEmpty(e[3])) bt.Talepler.Add(e);
                        }
                    }
                    break;

                case TabloTürü.ÖdemeTalepEdildi:
                case TabloTürü.Ödendi:
                    Depo_ depo = Tablo(Müşteri, Tür, false, EkTanım);
                    d = depo.Bul("Talepler");
                    if (d != null)
                    {
                        bt.Talepler = d.Elemanları.ToList();

                        bt.Ödeme = depo.Bul("Ödeme");
                    }
                    break;

                default:
                    break;
            }

            return bt;
        }
        public static string Talep_İşaretle_DevamEden_TeslimEdilen(string Müşteri, List<string> Üyeler, bool TeslimEdildi_1_DevamEden_0)
        {
            IDepo_Eleman d = Tablo_Dal(Müşteri, TabloTürü.DevamEden, "Talepler", true);

            foreach (string ü in Üyeler)
            {
                IDepo_Eleman sn = d.Bul(ü);
                if (sn == null) throw new Exception(Müşteri + " / Devam Eden / Talepler / " + ü + " bulunamadı");

                if (TeslimEdildi_1_DevamEden_0)
                {
                    //Teslim edildi olarak işaretle
                    sn.Yaz(null, DateTime.Now, 3); //tamamlanma tarihi bugün

                    foreach (IDepo_Eleman e in sn.Elemanları)
                    {
                        double ücret = e.Oku_Sayı(null, -1, 2);
                        if (ücret < 0)
                        {
                            //kullanıcı ücret girmemiş
                            e.Yaz(null, null, 3); //girilmediği için eğer önceden kalma varsa sil

                            //hesaplatılacak
                            ücret = Ücretler_BirimÜcret(Müşteri, e[0]);
                            if (ücret < 0)
                            {
                                return Müşteri + " / " + sn.Adı + " / " + e[0] + " için ücret hesaplanamadı" + Environment.NewLine + Environment.NewLine +
                            "Bilgi için \"Ana Ekran -> Yeni İş Girişi -> Notlar\" elemanı üzerine" + Environment.NewLine +
                            "fareyi götürüp 1 sn kadar bekleyiniz";
                            }
                        }
                        else
                        {
                            //kullanıcının girdiğini ayrıca not al, ilerde tersine işlem yaparken lazım olacak
                            e.Yaz(null, ücret, 3); //girilmediği için eğer önceden kalma varsa sil
                        }

                        e.Yaz(null, ücret, 2);
                    }
                }
                else
                {
                    //devam eden olarak işaretle
                    sn.Yaz(null, null, 3); //tamamlanma tarihi iptal

                    foreach (IDepo_Eleman e in sn.Elemanları)
                    {
                        e[2] = e[3]; //eğer var ise kullanıcının girdiği değeri geri yükle
                    }
                }
            }

            return null;
        }
        public static string Talep_İşaretle_ÖdemeTalepEdildi(string Müşteri, List<string> Seri_No_lar, string İlaveÖdeme_Açıklama, double İlaveÖdeme_Miktar)
        {
            DateTime t = DateTime.Now;
            Depo_ yeni_tablo = Tablo(Müşteri, TabloTürü.ÖdemeTalepEdildi, true, t.Yazıya(D_TarihSaat.Şablon_DosyaAdı));
            IDepo_Eleman yeni_tablodaki_işler = yeni_tablo.Bul("Talepler", true);
            IDepo_Eleman eski_tablodaki_işler = Tablo_Dal(Müşteri, TabloTürü.DevamEden, "Talepler");
            double Alt_Toplam = 0;

            foreach (string sn in Seri_No_lar)
            {
                IDepo_Eleman seri_noya_ait_detaylar = eski_tablodaki_işler.Bul(sn); //bir talep
                double iş_toplam = 0;

                foreach (IDepo_Eleman i in seri_noya_ait_detaylar.Elemanları)
                {
                    double birim_ücret = i.Oku_Sayı(null, 0, 2); //bir iş
                    if (birim_ücret < 0)
                    {
                        throw new Exception(Müşteri + " / Devam Eden / " + seri_noya_ait_detaylar.Adı + " / " + i[0] + " ücreti (" + birim_ücret + ") hatalı");
                    }

                    iş_toplam += birim_ücret;
                }

                double iskonto = seri_noya_ait_detaylar.Oku_Sayı(null, 0, 1);
                if (iskonto > 0) iş_toplam -= iş_toplam / 100 * iskonto;
                Alt_Toplam += iş_toplam;

                yeni_tablodaki_işler.Ekle(null, seri_noya_ait_detaylar.YazıyaDönüştür(null));
                seri_noya_ait_detaylar.Sil(null);
            }

            yeni_tablo.Yaz("Ödeme", t);
            if (!string.IsNullOrEmpty(İlaveÖdeme_Açıklama))
            {
                yeni_tablo.Yaz("Ödeme/İlave Ödeme", İlaveÖdeme_Açıklama, 0);
                yeni_tablo.Yaz("Ödeme/İlave Ödeme", İlaveÖdeme_Miktar, 1);
            }

            yeni_tablo.Yaz("Ödeme/Alt Toplam", Alt_Toplam);

            return null;
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
        public static void Talep_İşaretle_ÖdemeTalepEdildi_Ödendi(string Müşteri, string EkTanım, string Notlar)
        {
            DateTime t = DateTime.Now;
            Depo_ depo = Tablo(Müşteri, TabloTürü.ÖdemeTalepEdildi, false, EkTanım);
            
            Depo_ yeni_tablo = new Depo_();
            yeni_tablo.Ekle(depo.YazıyaDönüştür());
            yeni_tablo.Yaz("Ödeme", t.Yazıya(), 1);
            yeni_tablo.Yaz("Ödeme", Notlar, 2);
            Depo_Kaydet(Müşteri + "\\Ödendi_" + t.Yazıya(D_TarihSaat.Şablon_DosyaAdı), yeni_tablo);

            depo.Yaz("Silinecek", "Evet");
        }
        public static void Talep_TablodaGöster(DataGridView Tablo, Banka_Tablo_ İçerik, bool ÖnceTemizle = true)
        {
            Ortak.BeklemeGöstergesi.BackColor = Color.Salmon;
            Ortak.BeklemeGöstergesi.Refresh();
            
            if (ÖnceTemizle)
            {
                Tablo.Rows.Clear();

                if (Tablo.SortedColumn != null)
                {
                    DataGridViewColumn col = Tablo.SortedColumn;
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
                    col.SortMode = DataGridViewColumnSortMode.Automatic;
                }
            }

            string tar_ödeme_talep = null;
            string tar_ödendi = null;
            if (İçerik.Ödeme != null)
            {
                tar_ödeme_talep = Yazdır_Tarih(İçerik.Ödeme.Oku(null, null, 0));
                tar_ödendi = Yazdır_Tarih(İçerik.Ödeme.Oku(null, null, 1));
            }
            
            foreach (IDepo_Eleman t in İçerik.Talepler)
            {
                int y = Tablo.RowCount;
                Tablo.RowCount++;

                double ü = 0;
                Talep_Ayıkla_İş(t, out string Hasta, out string İşler, ref ü);

                Tablo[0, y].Value = false; //seçim kutucuğu
                Tablo[1, y].Value = t.Adı; //seri no
                Tablo[2, y].Value = İçerik.Müşteri;
                Tablo[3, y].Value = Hasta;
                Tablo[4, y].Value = İşler;
                Tablo[5, y].Value = Yazdır_Tarih(t[3]); //teslim edilme tarihi
                Tablo[6, y].Value = tar_ödeme_talep;
                Tablo[7, y].Value = tar_ödendi;
                Tablo[8, y].Value = t[2]; //notlar
            }
          
            if (ÖnceTemizle)
            {
                switch (İçerik.Türü)
                {
                    case TabloTürü.DevamEden:
                        Tablo.Columns[2].Visible = false; //müşteri
                        Tablo.Columns[5].Visible = false; //tarih teslim
                        Tablo.Columns[6].Visible = false; //tarih ödeme talebi
                        Tablo.Columns[7].Visible = false; //tarih ödendi
                        break;
                    case TabloTürü.TeslimEdildi:
                        Tablo.Columns[2].Visible = false; //müşteri
                        Tablo.Columns[5].Visible = true; //tarih teslim
                        Tablo.Columns[6].Visible = false; //tarih ödeme talebi
                        Tablo.Columns[7].Visible = false; //tarih ödendi
                        break;

                    case TabloTürü.ÖdemeTalepEdildi:
                        Tablo.Columns[2].Visible = false; //müşteri
                        Tablo.Columns[5].Visible = true; //tarih teslim
                        Tablo.Columns[6].Visible = true; //tarih ödeme talebi
                        Tablo.Columns[7].Visible = false; //tarih ödendi
                        break;

                    case TabloTürü.Ödendi:
                        Tablo.Columns[2].Visible = false; //müşteri
                        Tablo.Columns[5].Visible = true; //tarih teslim
                        Tablo.Columns[6].Visible = true; //tarih ödeme talebi
                        Tablo.Columns[7].Visible = true; //tarih ödendi
                        break;

                    case TabloTürü.DevamEden_TeslimEdildi_ÖdemeTalepEdildi_Ödendi:
                        Tablo.Columns[2].Visible = true; //müşteri
                        Tablo.Columns[5].Visible = true; //tarih teslim
                        Tablo.Columns[6].Visible = true; //tarih ödeme talebi
                        Tablo.Columns[7].Visible = true; //tarih ödendi
                        break;
                    default:
                        break;
                }
            }
            
            Tablo.ClearSelection();
            Ortak.BeklemeGöstergesi.BackColor = Color.White;
        }
        public static void Talep_Ayıkla_İş(IDepo_Eleman Talep, out string Hasta, out string İşler, ref double Toplam)
        {
            Hasta = Talep[0];
            double iskonto = Talep.Oku_Sayı(null, 0, 1);
            if (iskonto > 0) Hasta += "\n% " + iskonto + " iskonto";

            İşler = "";
            foreach (IDepo_Eleman iş in Talep.Elemanları)
            {
                //tarih - iş türü - ücret sadece 0 dan büyük ise
                İşler += Yazdır_Tarih(iş[1]) + " " + iş[0];

                double ücret = iş.Oku_Sayı(null, 0, 2);
                if (ücret > 0)
                {
                    İşler += " " + iş[2] + " ₺";
                    Toplam += ücret;
                }

                İşler += "\n";
            }
            İşler = İşler.TrimEnd('\n');

            if (iskonto > 0) Toplam -= Toplam / 100 * iskonto;
        }
        public static void Talep_Ayıkla_Ödeme(IDepo_Eleman Talep, out List<string> Açıklamalar, out List<string> Ücretler, out string ÖdemeTalepEdildi, out string Ödendi, out string Notlar)
        {
            double AltToplam = Talep.Oku_Sayı("Alt Toplam");
            double İlaveÖdeme = Talep.Oku_Sayı("İlave Ödeme", 0, 1);
            string İlaveÖdemeAçıklaması = Talep.Oku("İlave Ödeme");

            ÖdemeTalepEdildi = Yazdır_Tarih(Talep[0]);
            Ödendi = Yazdır_Tarih(Talep[1]);
            Notlar = Talep[2];

            Açıklamalar = new List<string>();
            Ücretler = new List<string>();

            Açıklamalar.Add("Alt Toplam"); Ücretler.Add(string.Format("{0:,0.00}", AltToplam) + " ₺");
            
            if (İlaveÖdeme > 0)
            {
                Açıklamalar.Add(İlaveÖdemeAçıklaması); Ücretler.Add(string.Format("{0:,0.00}", İlaveÖdeme) + " ₺");
            }

            Açıklamalar.Add("Genel Toplam"); Ücretler.Add(string.Format("{0:,0.00}", AltToplam + İlaveÖdeme) + " ₺");
        }

        public static void Değişiklikleri_Kaydet()
        {
            bool EnAzBirDeğişiklikYapıldı = false;

            if (Ayarlar != null && Ayarlar.EnAzBir_ElemanAdıVeyaİçeriği_Değişti) { Depo_Kaydet("Ayarlar", Ayarlar); EnAzBirDeğişiklikYapıldı = true; }
            if (İşTürleri != null && İşTürleri.EnAzBir_ElemanAdıVeyaİçeriği_Değişti) { Depo_Kaydet("İş Türleri", İşTürleri); EnAzBirDeğişiklikYapıldı = true; }
            if (Malzemeler != null && Malzemeler.EnAzBir_ElemanAdıVeyaİçeriği_Değişti) { Depo_Kaydet("Malzemeler", Malzemeler); EnAzBirDeğişiklikYapıldı = true; }
            if (Ücretler != null && Ücretler.EnAzBir_ElemanAdıVeyaİçeriği_Değişti) { Depo_Kaydet("Ücretler", Ücretler); EnAzBirDeğişiklikYapıldı = true; }

            if (Müşteriler != null && Müşteriler.Count > 0)
            {
                foreach (Müşteri_ m in Müşteriler.Values)
                {
                    if (m.DevamEden != null && m.DevamEden.EnAzBir_ElemanAdıVeyaİçeriği_Değişti) { Depo_Kaydet(m.Adı + "\\Devam Eden", m.DevamEden); EnAzBirDeğişiklikYapıldı = true; }
                    if (m.Ücretler != null && m.Ücretler.EnAzBir_ElemanAdıVeyaİçeriği_Değişti) { Depo_Kaydet(m.Adı + "\\Ücretler", m.Ücretler); EnAzBirDeğişiklikYapıldı = true; }

                    if (m.ÖdemeTalepEdildi != null)
                    {
                        foreach (KeyValuePair<string, Depo_> a in m.ÖdemeTalepEdildi)
                        {
                            if (a.Value == null) continue;
                            if (!string.IsNullOrEmpty(a.Value.Oku("Silinecek")))
                            {
                                Dosya.Sil(Ortak.Klasör_Banka + m.Adı + "\\Ödeme Talep Edildi_" + a.Key + ".mup");
                                EnAzBirDeğişiklikYapıldı = true;
                            }
                            else if (a.Value.EnAzBir_ElemanAdıVeyaİçeriği_Değişti) { Depo_Kaydet(m.Adı + "\\Ödeme Talep Edildi_" + a.Key, a.Value); EnAzBirDeğişiklikYapıldı = true; }
                        }
                    }
                    m.ÖdemeTalepEdildi = null;
                    
                    m.Ödendi = null;
                    m.Liste_ÖdemeTalepEdildi = null;
                    m.Liste_Ödendi = null;
                }
            }

            if (EnAzBirDeğişiklikYapıldı)
            {
                DoğrulamaKodu.Üret.Klasörden(Ortak.Klasör_Banka, true);
                Klasör.AslınaUygunHaleGetir(Ortak.Klasör_Banka, Ortak.Klasör_Banka2, true);
            }
        }
        public static void Değişiklikleri_GeriAl()
        {
            Ayarlar = null;
            İşTürleri = null;
            Malzemeler = null;
            Ücretler = null;
            Müşteriler = null;
        }

        public static string Yazdır_Tarih(string Girdi)
        {
            if (string.IsNullOrEmpty(Girdi) || Girdi.Length < 10) return Girdi;

            return Girdi.Substring(0, 10); // dd.MM.yyyy
        }

#region Demirbaşlar
        public enum TabloTürü { Ayarlar, İşTürleri, Ücretler, Malzemeler, DevamEden, TeslimEdildi, ÖdemeTalepEdildi, Ödendi,
                                                                             DevamEden_TeslimEdildi_ÖdemeTalepEdildi_Ödendi
        }
        static Depo_ Ayarlar = null;
        static Depo_ İşTürleri = null;
        static Depo_ Ücretler = null;
        static Depo_ Malzemeler = null;

        class Müşteri_
        {
            public string Adı = null;

            public Depo_ DevamEden = null;
            public Depo_ Ücretler = null;
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
#endregion

#region Depo + Sıkıştırma + Şifreleme
        static DahaCokKarmasiklastirma_ DaÇoKa = new DahaCokKarmasiklastirma_();

        static bool Depo_DosyaVarMı(string DosyaYolu)
        {
            DosyaYolu = Ortak.Klasör_Banka + DosyaYolu + ".mup";
            return File.Exists(DosyaYolu);
        }
        static void Depo_Kaydet(string DosyaYolu, Depo_ Depo)
        {
            //Depo
            string içerik = Depo.YazıyaDönüştür();
            if (string.IsNullOrEmpty(içerik)) içerik = " ";

#if DEBUG
            byte[] çıktı = içerik.BaytDizisine();
#else
string tarihsaat = D_TarihSaat.Yazıya(DateTime.Now, D_TarihSaat.Şablon_DosyaAdı);
            byte[] dizi_içerik = içerik.BaytDizisine();
            byte[] dizi_doko = D_Yazı.BaytDizisine(tarihsaat + ";" + DoğrulamaKodu.Üret.BaytDizisinden(dizi_içerik).HexYazıya());

            //Sıkıştırma
            string Gecici_zip_dosyası = Ortak.Klasör_Gecici + Path.GetRandomFileName();
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
                    H.Write(dizi_içerik, 0, dizi_içerik.Length);
                }
            }

            //Şifreleme
            byte[] çıktı = DaÇoKa.Karıştır(File.ReadAllBytes(Gecici_zip_dosyası), Parola.Dizi);
            Dosya.Sil(Gecici_zip_dosyası);
#endif
            DosyaYolu = Ortak.Klasör_Banka + DosyaYolu + ".mup";
            string yedek_dosya_yolu = DosyaYolu + ".yedek";
            Klasör.Oluştur(Path.GetDirectoryName(DosyaYolu));

            if (!File.Exists(yedek_dosya_yolu) && File.Exists(DosyaYolu)) File.Move(DosyaYolu, yedek_dosya_yolu);
            
            File.WriteAllBytes(DosyaYolu, çıktı);

            Dosya.Sil(yedek_dosya_yolu);

            Depo.EnAzBir_ElemanAdıVeyaİçeriği_Değişti = false;
        }
        static Depo_ Depo_Aç(string DosyaYolu)
        {
            DosyaYolu = Ortak.Klasör_Banka + DosyaYolu + ".mup";
            if (!File.Exists(DosyaYolu)) return new Depo_();

#if DEBUG
            return new Depo_(File.ReadAllBytes(DosyaYolu).Yazıya());
#else
            Depo_ Depo = null;

            //Şifreleme
            byte[] çıktı = DaÇoKa.Düzelt(File.ReadAllBytes(DosyaYolu), Parola.Dizi);

            string Gecici_zip_dosyası = Ortak.Klasör_Gecici + Path.GetRandomFileName();
            File.WriteAllBytes(Gecici_zip_dosyası, çıktı);

            //Sıkıştırma
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
                            if (dizi_içerik != null && dizi_içerik.Length > 0 && dizi_içerik.Length == adt)
                            {
                                if (doko == DoğrulamaKodu.Üret.BaytDizisinden(dizi_içerik).HexYazıya())
                                {
                                    //Depo
                                    string okunan = dizi_içerik.Yazıya();
                                    if (!string.IsNullOrEmpty(okunan)) Depo = new Depo_(okunan);
                                }
                            }
                        }
                    }
                }
            }
            Dosya.Sil(Gecici_zip_dosyası);

            if (Depo == null) throw new Exception(DosyaYolu + " dosyası arızalı");

            return Depo;
#endif
        }
#endregion
    }
}
