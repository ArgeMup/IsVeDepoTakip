﻿using ArgeMup.HazirKod;
using ArgeMup.HazirKod.Dönüştürme;
using ArgeMup.HazirKod.Ekİşlemler;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Windows.Forms;
using static ArgeMup.HazirKod.Vt100;

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
        static int Malzemeler_GeriyeDönükİstatistik_Ay = 36;
        public static void Giriş_İşlemleri(Label AçılışYazısı)
        {
            int Açılışİşlemi_Tik = Environment.TickCount;
            Klasör.Oluştur(Ortak.Klasör_Banka);
            Klasör.Oluştur(Ortak.Klasör_İçYedek);
            Klasör.Oluştur(Ortak.Klasör_Diğer);
            Klasör.Oluştur(Ortak.Klasör_Diğer_ArkaPlanResimleri);
            Klasör.Oluştur(Ortak.Klasör_Gecici);
            Ortak.Gösterge_Açılışİşlemi(AçılışYazısı, "Klasörler", ref Açılışİşlemi_Tik);

            Depo_ d = Tablo(null, TabloTürü.Ayarlar);
            if (d != null)
            {
                IDepo_Eleman k_y = d.Bul("Klasör/Yedek");
                Ortak.Kullanıcı_Klasör_Yedek = k_y == null ? new string[0] : k_y.İçeriği;

                Ortak.Kullanıcı_Klasör_Pdf = d.Oku("Klasör/Pdf");
                Ortak.Kullanıcı_KüçültüldüğündeParolaSor = d.Oku_Bit("Küçültüldüğünde Parola Sor", true, 0);
                Ortak.Kullanıcı_KüçültüldüğündeParolaSor_sn = d.Oku_TamSayı("Küçültüldüğünde Parola Sor", 60, 1);
                Ortak.Kullanıcı_Eposta_hesabı_mevcut = !string.IsNullOrEmpty(d.Oku("Eposta/Gönderici/Şifresi"));

                while (d.Oku_TarihSaat("Son Banka Kayıt", default, 1) > DateTime.Now)
                {
                    MessageBox.Show(
                        "Son kayıt saati : " + d.Oku("Son Banka Kayıt", default, 1) + Environment.NewLine +
                        "Bilgisayarınızın saati : " + DateTime.Now.Yazıya() + Environment.NewLine + Environment.NewLine +
                        "Muhtemelen bilgisayarınızın saati geri kaldı, lütfen düzeltip devam ediniz", "Bütünlük Kontrolü");
                }
            }
            Ortak.Gösterge_Açılışİşlemi(AçılışYazısı, "Ayarlar", ref Açılışİşlemi_Tik);

            DoğrulamaKodu.KontrolEt.Durum_ snç = DoğrulamaKodu.KontrolEt.Klasör(Ortak.Klasör_Banka, SearchOption.AllDirectories, Parola.Yazı, Ortak.EşZamanlıİşlemSayısı);
            Ortak.Gösterge_Açılışİşlemi(AçılışYazısı, "Bütünlük Kontrolü", ref Açılışİşlemi_Tik);
            switch (snç)
            {
                case DoğrulamaKodu.KontrolEt.Durum_.Aynı:
                    #region yedekleme
                    Yedekle();
                    Ortak.Gösterge_Açılışİşlemi(AçılışYazısı, "Yedekleme", ref Açılışİşlemi_Tik);

                    Klasör.AslınaUygunHaleGetir(Ortak.Klasör_Banka, Ortak.Klasör_Banka2, true, Ortak.EşZamanlıİşlemSayısı);
                    Ortak.Gösterge_Açılışİşlemi(AçılışYazısı, "İlk Kullanıma Hazırlanıyor", ref Açılışİşlemi_Tik);
                    #endregion

                    Malzeme_KritikMiktarKontrolü();
                    Ortak.Gösterge_Açılışİşlemi(AçılışYazısı, "Malzeme Durumu", ref Açılışİşlemi_Tik);
                    break;

                case DoğrulamaKodu.KontrolEt.Durum_.DoğrulamaDosyasıYok:
#if !DEBUG
                    Klasör_ kls = new Klasör_(Ortak.Klasör_Banka, EşZamanlıİşlemSayısı: Ortak.EşZamanlıİşlemSayısı);
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

            if (!Directory.Exists(Ortak.Klasör_Banka + Müşteri)) return new string[0];

            if (Ödendi_1_ÖdemeBekleyen_0)
            {
                if (m.Liste_Ödendi == null)
                {
                    string[] l = new DirectoryInfo(Ortak.Klasör_Banka + Müşteri).GetFiles("Ödendi_*.mup", SearchOption.TopDirectoryOnly)
                        .OrderByDescending(f => f.LastWriteTime)
                        .Select(f => f.FullName)
                        .ToArray();

                    m.Liste_Ödendi = new string[l.Length];

                    for (int i = 0; i < l.Length; i++)
                    {
                        m.Liste_Ödendi[i] = Path.GetFileNameWithoutExtension(l[i]).Substring(7 /*Ödendi_*/);
                    }
                }

                return m.Liste_Ödendi;
            }
            else
            {
                if (m.Liste_ÖdemeTalepEdildi == null)
                {
                    string[] l = new DirectoryInfo(Ortak.Klasör_Banka + Müşteri).GetFiles("Ödeme Talep Edildi_*.mup", SearchOption.TopDirectoryOnly)
                        .OrderBy(f => f.LastWriteTime)
                        .Select(f => f.FullName)
                        .ToArray();

                    m.Liste_ÖdemeTalepEdildi = new string[l.Length];

                    for (int i = 0; i < l.Length; i++)
                    {
                        m.Liste_ÖdemeTalepEdildi[i] = Path.GetFileNameWithoutExtension(l[i]).Substring(19 /*Ödeme Talep Edildi_*/);
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
        public static void İşTürü_Malzemeler_TablodaGöster(DataGridView Tablo, string İşTürü, out string Notlar)
        {
            Tablo.Rows.Clear();
            if (Tablo.SortedColumn != null)
            {
                DataGridViewColumn col = Tablo.SortedColumn;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                col.SortMode = DataGridViewColumnSortMode.Automatic;
            }

            Notlar = null;
            IDepo_Eleman d = Tablo_Dal(null, TabloTürü.İşTürleri, İşTürü);
            if (d == null) return;

            Notlar = d.Oku("Notlar");

            d = d.Bul("Malzemeler");
            if (d == null) return;

            List<string> mlzler = Malzeme_Listele();

            foreach (IDepo_Eleman e in d.Elemanları)
            {
                if (!mlzler.Contains(e.Adı)) continue;

                int y = Tablo.RowCount - 1;
                Tablo.RowCount++;

                Tablo[0, y].Value = e.Adı;
                Tablo[1, y].Value = e[0];
                Tablo[2, y].Value = Malzeme_Birimi(e.Adı);
            }

            Tablo.ClearSelection();
        }
        public static void İşTürü_Malzemeler_Kaydet(string İşTürü, List<string> Malzemeler, List<string> Miktarlar, string Notlar)
        {
            IDepo_Eleman d = Tablo_Dal(null, TabloTürü.İşTürleri, İşTürü, true);
            d.Yaz("Notlar", Notlar);
            d.Sil("Malzemeler");

            for (int i = 0; i < Malzemeler.Count; i++)
            {
                d.Yaz("Malzemeler/" + Malzemeler[i], Miktarlar[i]);
            }
        }

        public static void Malzeme_KritikMiktarKontrolü()
        {
            Depo_ d_malzemeler = Tablo(null, TabloTürü.Malzemeler);
            if (d_malzemeler == null) return;

            DateTime t = Tablo_Dal(null, TabloTürü.Ayarlar, "Malzemeler", true).Oku_TarihSaat(null, DateTime.Now);
            int fark_tarih = (int)(DateTime.Now - t).TotalDays / 30;

            foreach (IDepo_Eleman malzeme in d_malzemeler.Elemanları)
            {
                double mevcut = malzeme.Oku_Sayı(null, 0, 0);
                double uyarıvermemiktarı = malzeme.Oku_Sayı(null, 0, 2);

                if (uyarıvermemiktarı > 0)
                {
                    if (mevcut <= uyarıvermemiktarı) Ortak.Gösterge_UyarıVerenMalzemeler[malzeme.Adı] = mevcut.Yazıya() + " " + malzeme[1];
                    else Ortak.Gösterge_UyarıVerenMalzemeler[malzeme.Adı] = null;
                }

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
                Tablo_Dal(null, TabloTürü.Ayarlar, "Malzemeler").Yaz(null, DateTime.Now);
                Değişiklikleri_Kaydet();
            }
        }
        public static List<string> Malzeme_Listele()
        {
            Depo_ d = Tablo(null, TabloTürü.Malzemeler);
            List<string> l = new List<string>();
            if (d == null) return l;

            foreach (IDepo_Eleman e in d.Elemanları)
            {
                if (e.İçiBoşOlduğuİçinSilinecek) continue;

                l.Add(e.Adı);
            }

            return l;
        }
        public static void Malzeme_Ekle(string Adı)
        {
            Tablo_Dal(null, TabloTürü.Malzemeler, Adı, true)[0] = "0";
        }
        public static void Malzeme_Sil(string Adı)
        {
            IDepo_Eleman d = Tablo_Dal(null, TabloTürü.Malzemeler, Adı);
            if (d != null) d.Sil(null);
        }
        public static bool Malzeme_MevcutMu(string Adı)
        {
            return !string.IsNullOrWhiteSpace(Adı) && Tablo_Dal(null, TabloTürü.Malzemeler, Adı) != null;
        }
        public static void Malzeme_TablodaGöster(DataGridView Tablo, string Malzeme, out double Miktarı, out string Birimi, out double UyarıVermeMiktarı, out string Notlar)
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
            Notlar = null;
            IDepo_Eleman d = Tablo_Dal(null, TabloTürü.Malzemeler, Malzeme);
            if (d == null) return;
            int y;

            Miktarı = d.Oku_Sayı(null, 0, 0);
            Birimi = d[1];
            UyarıVermeMiktarı = d.Oku_Sayı(null, 0, 2);
            Notlar = d.Oku("Notlar");

            //bu malzemeyi kullanan iş türlerinin bulunması
            Depo_ iş_türleri = Banka.Tablo(null, TabloTürü.İşTürleri);
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
                            }
                        }
                    }
                }
            }

            //istatistikleri ekleme
            d = d.Bul("Tüketim");
            if (d == null) return;

            //genel toplam
            y = Tablo.RowCount;
            Tablo.RowCount++;
            Tablo[0, y].Value = "Genel kullanım miktarı";
            Tablo[1, y].Value = d[0];

            //önceki dönemler
            DateTime t = DateTime.Now;
            for (int i = 1; i < Malzemeler_GeriyeDönükİstatistik_Ay + 1; i++)
            {
                if (string.IsNullOrEmpty(d[i])) break;
                
                y = Tablo.RowCount;
                Tablo.RowCount++;
                Tablo[0, y].Value = t.Year + " " + t.Yazıya("MMMM", System.Threading.Thread.CurrentThread.CurrentCulture) + " ayı kullanımı";
                Tablo[1, y].Value = d[i];
                
                t = t.AddMonths(-1);
            }

            Tablo.ClearSelection();
        }
        public static void Malzeme_DetaylarıKaydet(string Malzeme, string Mevcut, string Birimi, string UyarıVermeMiktarı, string Notlar)
        {
            IDepo_Eleman d = Tablo_Dal(null, TabloTürü.Malzemeler, Malzeme);
            d[0] = Mevcut;
            d[1] = Birimi;
            d[2] = UyarıVermeMiktarı;
            d.Yaz("Notlar", Notlar);

            double UyarıVermeMiktarı_s = UyarıVermeMiktarı.NoktalıSayıya();
            if (UyarıVermeMiktarı_s > 0)
            {
                if (Mevcut.NoktalıSayıya() <= UyarıVermeMiktarı_s) Ortak.Gösterge_UyarıVerenMalzemeler[Malzeme] = Mevcut + " " + Birimi;
                else Ortak.Gösterge_UyarıVerenMalzemeler[Malzeme] = null;
            }
        }
        public static string Malzeme_Birimi(string Malzeme)
        {
            IDepo_Eleman d = Tablo_Dal(null, TabloTürü.Malzemeler, Malzeme);
            if (d == null) return null;

            return d[1];
        }
        public static void Malzeme_İştürüneGöreHareket(List<string> İşTürleri, bool Eksilt)
        {
            //İşTürleri değişkeni içeriğini silerek ilerliyor

            Depo_ d_malzemeler = Tablo(null, TabloTürü.Malzemeler);
            if (d_malzemeler == null) return;

            Depo_ d_iştürleri = Tablo(null, TabloTürü.İşTürleri);
            if (d_iştürleri == null) return;

            İşTürleri.Sort();
            while (İşTürleri.Count > 0)
            {
                double adet = 1;
                while (İşTürleri.Count > 1 && İşTürleri[0] == İşTürleri[1])
                {
                    adet++;
                    İşTürleri.RemoveAt(1);
                }

                IDepo_Eleman iştürünün_malzemeleri = d_iştürleri.Bul(İşTürleri[0] + "/Malzemeler");
                İşTürleri.RemoveAt(0);
                if (iştürünün_malzemeleri == null) continue;

                foreach (IDepo_Eleman iştürünün_malzemesi in iştürünün_malzemeleri.Elemanları)
                {
                    double miktar = iştürünün_malzemesi.Oku_Sayı(null) * adet;
                    
                    IDepo_Eleman malzeme = d_malzemeler.Bul(iştürünün_malzemesi.Adı);
                    if (malzeme == null) continue;

                    double mevcut, uyarıvermemiktarı;
                    if (Eksilt)
                    {
                        malzeme.Yaz("Tüketim", malzeme.Oku_Sayı("Tüketim", 0, 0) + miktar, 0);  //toplam
                        malzeme.Yaz("Tüketim", malzeme.Oku_Sayı("Tüketim", 0, 1) + miktar, 1);  //bu ay
                        
                        mevcut = malzeme.Oku_Sayı(null, 0, 0) - miktar;
                        malzeme.Yaz(null, mevcut, 0);                                           //mevcut

                        uyarıvermemiktarı = malzeme.Oku_Sayı(null, 0, 2);
                    }
                    else
                    {
                        malzeme.Yaz("Tüketim", malzeme.Oku_Sayı("Tüketim", 0, 0) - miktar, 0);  //toplam
                        malzeme.Yaz("Tüketim", malzeme.Oku_Sayı("Tüketim", 0, 1) - miktar, 1);  //bu ay

                        mevcut = malzeme.Oku_Sayı(null, 0, 0) + miktar;
                        malzeme.Yaz(null, mevcut, 0);                                           //mevcut

                        uyarıvermemiktarı = malzeme.Oku_Sayı(null, 0, 2);
                    }

                    if (uyarıvermemiktarı > 0)
                    {
                        if (mevcut <= uyarıvermemiktarı) Ortak.Gösterge_UyarıVerenMalzemeler[malzeme.Adı] = mevcut.Yazıya() + " " + malzeme[1];
                        else Ortak.Gösterge_UyarıVerenMalzemeler[malzeme.Adı] = null;
                    }
                }
            }
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
                    if (Müşteri == null) Tablo[2, i].Value = bulunan[1];
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

            Tablo.Columns[2].Visible = Müşteri == null;
            Tablo.ClearSelection();
        }
        public static void Ücretler_TablodakileriKaydet(DataGridView Tablo, string Müşteri)
        {
            IDepo_Eleman d = Tablo_Dal(Müşteri, TabloTürü.Ücretler, "Ücretler", true);

            for (int i = 0; i < Tablo.RowCount; i++)
            {
                string iştürü = (string)Tablo[0, i].Value;

                string ücret = (string)Tablo[1, i].Value;
                if (!string.IsNullOrEmpty(ücret)) ücret = ücret.NoktalıSayıya().Yazıya();
                
                d.Yaz(iştürü, ücret, 0);

                if (Müşteri == null)
                {
                    string maliyet = (string)Tablo[2, i].Value;
                    if (!string.IsNullOrEmpty(maliyet)) maliyet = maliyet.NoktalıSayıya().Yazıya();

                    d.Yaz(iştürü, maliyet, 1);
                }
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
        public static string Ücretler_BirimÜcret_Detaylı(string Müşteri, string İşTürü)
        {
            double müşteriye_özel = -1, ortak = -1, maliyet = -1;

            //müşteriye özel ücret varmı diye bak
            IDepo_Eleman d;
            
            if (!string.IsNullOrEmpty(Müşteri))
            {
                d = Tablo_Dal(Müşteri, TabloTürü.Ücretler, "Ücretler/" + İşTürü);
                if (d != null) müşteriye_özel = d.Oku_Sayı(null, -1);
            }

            //genel ücret
            d = Tablo_Dal(null, TabloTürü.Ücretler, "Ücretler/" + İşTürü);
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
            IDepo_Eleman d = Tablo_Dal(null, TabloTürü.Ücretler, "Ücretler/" + İşTürü);
            
            if (d != null) return d.Oku_Sayı(null, 0, 1);
            else return 0;
        }

        public static void Talep_Ekle(string Müşteri, string Hasta, string İskonto, string Notlar, List<string> İşTürleri, List<string> Ücretler, List<string> GirişTarihleri, string SeriNo = null)
        {
            if (string.IsNullOrEmpty(SeriNo)) SeriNo = SeriNo_Üret();

            IDepo_Eleman d = Tablo_Dal(Müşteri, TabloTürü.DevamEden, "Talepler/" + SeriNo, true);
            d[0] = Hasta;
            d[1] = İskonto;
            d[2] = Notlar;

            for (int i = 0; i < İşTürleri.Count; i++)
            {
                string ad = (i + 1).ToString();
                d.Yaz(ad, İşTürleri[i], 0);
                d.Yaz(ad, GirişTarihleri[i], 1);
                d.Yaz(ad, Ücretler[i], 2);
            }

            if (!string.IsNullOrEmpty(d[3]))
            {
                d[3] = null; //teslim edilme tarihi
                Malzeme_İştürüneGöreHareket(İşTürleri, false);//depoya geri teslim et
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
        public static string Talep_İşaretle_DevamEden_TeslimEdilen(string Müşteri, List<string> SeriNolar, bool TeslimEdildi_1_DevamEden_0)
        {
            IDepo_Eleman d = Tablo_Dal(Müşteri, TabloTürü.DevamEden, "Talepler", true);
            List<string> İşTürleri = new List<string>();

            foreach (string SeriNo in SeriNolar)
            {
                IDepo_Eleman sn = d.Bul(SeriNo);
                if (sn == null) throw new Exception(Müşteri + " / Devam Eden / Talepler / " + SeriNo + " bulunamadı");

                if (TeslimEdildi_1_DevamEden_0)
                {
                    //Teslim edildi olarak işaretle
                    sn.Yaz(null, DateTime.Now, 3); //tamamlanma tarihi bugün

                    foreach (IDepo_Eleman iştürü in sn.Elemanları)
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
                                return Müşteri + " / " + sn.Adı + " / " + iştürü[0] + " için ücret hesaplanamadı" + Environment.NewLine + Environment.NewLine +
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

                        İşTürleri.Add(iştürü[0]); //depo hareketi için
                    }
                }
                else
                {
                    //devam eden olarak işaretle
                    sn.Yaz(null, null, 3); //tamamlanma tarihi iptal

                    foreach (IDepo_Eleman iştürü in sn.Elemanları)
                    {
                        iştürü[2] = iştürü[3]; //eğer var ise kullanıcının girdiği değeri geri yükle

                        İşTürleri.Add(iştürü[0]); //depo hareketi için
                    }
                }
            }

            Malzeme_İştürüneGöreHareket(İşTürleri, TeslimEdildi_1_DevamEden_0);

            return null;
        }
        public static string Talep_İşaretle_ÖdemeTalepEdildi(string Müşteri, List<string> Seri_No_lar, string İlaveÖdeme_Açıklama, string İlaveÖdeme_Miktar)
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
            Depo_Kaydet(Müşteri + "\\Ödendi_" + t.Yazıya(ArgeMup.HazirKod.Dönüştürme.D_TarihSaat.Şablon_DosyaAdı2), yeni_tablo);

            depo.Yaz("Silinecek", "Evet");
        }
        public static void Talep_TablodaGöster(DataGridView Tablo, Banka_Tablo_ İçerik, bool ÖnceTemizle = true)
        {
            Ortak.Gösterge_UzunİşlemİçinBekleyiniz.BackColor = Color.Salmon;
            Ortak.Gösterge_UzunİşlemİçinBekleyiniz.Refresh();
            
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

                Tablo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                Tablo.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                Tablo.AutoResizeColumns();
            }
            
            Tablo.ClearSelection();
            Ortak.Gösterge_UzunİşlemİçinBekleyiniz.BackColor = Color.White;
        }
        public static void Talep_Ayıkla_İş(IDepo_Eleman SeriNoDalı, out string Hasta, out string İşler, ref double Toplam)
        {
            Hasta = SeriNoDalı[0];
            double iskonto = SeriNoDalı.Oku_Sayı(null, 0, 1);
            if (iskonto > 0) Hasta += "\n% " + iskonto + " iskonto";

            İşler = "";
            double AltToplam = 0;
            foreach (IDepo_Eleman iş in SeriNoDalı.Elemanları)
            {
                //tarih - iş türü - ücret sadece 0 dan büyük ise
                İşler += Yazdır_Tarih(iş[1]) + " " + iş[0];

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
        public static void Talep_Ayıkla_İş(string Müşteri, IDepo_Eleman SeriNoDalı, ref double İskontaDahilÜcretler_Toplamı, ref double Maliyetler_Toplamı, ref string HataMesajı)
        {
            double iskonto = SeriNoDalı.Oku_Sayı(null, 0, 1), Toplam_Ücret = 0, Toplam_Maliyet = 0;

            foreach (IDepo_Eleman iş in SeriNoDalı.Elemanları)
            {
                double ücret = iş.Oku_Sayı(null, -1, 2);
                if (ücret < 0) ücret = Ücretler_BirimÜcret(Müşteri, iş[0]);
                if (ücret > 0) Toplam_Ücret += ücret;
                else HataMesajı += iş[0] + " için ücret hesaplanamadı" + Environment.NewLine;

                Toplam_Maliyet += Ücretler_Maliyet(iş[0]);
            }

            if (iskonto > 0 && Toplam_Ücret > 0) Toplam_Ücret -= Toplam_Ücret / 100 * iskonto;

            İskontaDahilÜcretler_Toplamı += Toplam_Ücret;
            Maliyetler_Toplamı += Toplam_Maliyet;
        }
        public static void Talep_Ayıkla_Ödeme(IDepo_Eleman ÖdemeDalı, out List<string> Açıklamalar, out List<string> Ücretler, out string ÖdemeTalepEdildi, out string Ödendi, out string Notlar)
        {
            double AltToplam = ÖdemeDalı.Oku_Sayı("Alt Toplam");
            double İlaveÖdeme = ÖdemeDalı.Oku_Sayı("İlave Ödeme", 0, 1);
            string İlaveÖdemeAçıklaması = ÖdemeDalı.Oku("İlave Ödeme");

            ÖdemeTalepEdildi = Yazdır_Tarih(ÖdemeDalı[0]);
            Ödendi = Yazdır_Tarih(ÖdemeDalı[1]);
            Notlar = ÖdemeDalı[2];

            Açıklamalar = new List<string>();
            Ücretler = new List<string>();

            Açıklamalar.Add("Alt Toplam"); Ücretler.Add(Yazdır_Ücret(AltToplam));
            if (!string.IsNullOrEmpty(İlaveÖdemeAçıklaması))
            {
                Açıklamalar.Add(İlaveÖdemeAçıklaması); Ücretler.Add(Yazdır_Ücret(İlaveÖdeme));
            }
            Açıklamalar.Add("Genel Toplam"); Ücretler.Add(Yazdır_Ücret(AltToplam + İlaveÖdeme));
        }

        public static void Değişiklikleri_Kaydet()
        {
            bool EnAzBirDeğişiklikYapıldı = false;

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

            if (Ayarlar != null && ( Ayarlar.EnAzBir_ElemanAdıVeyaİçeriği_Değişti || EnAzBirDeğişiklikYapıldı )) 
            {
                Ayarlar.Yaz("Son Banka Kayıt", Kendi.BilgisayarAdı + " / " + Kendi.KullanıcıAdı, 0);
                Ayarlar.Yaz("Son Banka Kayıt", DateTime.Now, 1);

                Depo_Kaydet("Ayarlar", Ayarlar); 
                EnAzBirDeğişiklikYapıldı = true; 
            }

            if (EnAzBirDeğişiklikYapıldı)
            {
                DoğrulamaKodu.Üret.Klasörden(Ortak.Klasör_Banka, true, SearchOption.AllDirectories, Parola.Yazı);
                Klasör.AslınaUygunHaleGetir(Ortak.Klasör_Banka, Ortak.Klasör_Banka2, true, Ortak.EşZamanlıİşlemSayısı);
            }
        }
        public static void Değişiklikler_TamponuSıfırla()
        {
            Ayarlar = null;
            İşTürleri = null;
            Malzemeler = null;
            Ücretler = null;
            Müşteriler = null;
        }
        public static void Yedekle()
        {
            Klasör_ ydk_ler = new Klasör_(Ortak.Klasör_İçYedek, Filtre_Dosya: "*.zip", EşZamanlıİşlemSayısı: Ortak.EşZamanlıİşlemSayısı);
            ydk_ler.Dosya_Sil_SayısınaVeBoyutunaGöre(15, 1024 * 1024 * 1024 /*1GB*/, Ortak.EşZamanlıİşlemSayısı);
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

            for (int i = 0; i < Ortak.Kullanıcı_Klasör_Yedek.Length; i++)
            {
                if (string.IsNullOrEmpty(Ortak.Kullanıcı_Klasör_Yedek[i])) continue;

                Klasör.Kopyala(Ortak.Klasör_Banka, Ortak.Kullanıcı_Klasör_Yedek[i] + "Banka", Ortak.EşZamanlıİşlemSayısı);
                Klasör.Kopyala(Ortak.Klasör_Diğer, Ortak.Kullanıcı_Klasör_Yedek[i] + "Diğer", Ortak.EşZamanlıİşlemSayısı);
                Klasör.Kopyala(Ortak.Klasör_İçYedek, Ortak.Kullanıcı_Klasör_Yedek[i] + "Yedek", Ortak.EşZamanlıİşlemSayısı);
            }
        }

        public static string Yazdır_Tarih(string Girdi)
        {
            if (string.IsNullOrEmpty(Girdi) || Girdi.Length < 10) return Girdi;

            return Girdi.Substring(0, 10); // dd.MM.yyyy
        }
        public static string Yazdır_Ücret(double Ücret)
        {
            return string.Format("{0:,0.00}", Ücret) + " ₺";
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
