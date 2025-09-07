using ArgeMup.HazirKod;
using ArgeMup.HazirKod.Ekİşlemler;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip
{
    public static class Ortak
    {
        public static string Klasör_Banka = Kendi.Klasörü + "\\Banka\\";
        public static string Klasör_Banka_Müşteriler = Klasör_Banka + "Mü\\";
        public static string Klasör_Banka_MalzemeKullanımDetayları = Klasör_Banka + "MaKD\\";
        public static string Klasör_Banka2 = Kendi.Klasörü + "\\Banka2\\";
        public static string Klasör_İçYedek = Kendi.Klasörü + "\\Yedek\\";
        public static string Klasör_KullanıcıDosyaları = Kendi.Klasörü + "\\Kullanıcı Dosyaları\\";
        public static string Klasör_KullanıcıDosyaları_Gecmis = Klasör_KullanıcıDosyaları + "Gec\\";
        public static string Klasör_KullanıcıDosyaları_Ayarlar = Klasör_KullanıcıDosyaları + "Ay\\";
        public static string Klasör_KullanıcıDosyaları_Etiketleme = Klasör_KullanıcıDosyaları + "Et\\";
        public static string Klasör_KullanıcıDosyaları_DosyaEkleri = Klasör_KullanıcıDosyaları + "DoEk\\";
        public static string Klasör_KullanıcıDosyaları_KorumalıAlan = Klasör_KullanıcıDosyaları + "KoAl\\";
        public static string Klasör_KullanıcıDosyaları_GelirGiderTakip = Klasör_KullanıcıDosyaları + "GeGiTa\\";
        public static string Klasör_KullanıcıDosyaları_ArkaPlanResimleri = Klasör_KullanıcıDosyaları + "Arka Plan Resimleri\\";
        public static string Klasör_Gecici = Klasör.Depolama(Klasör.Kapsamı.Geçici, Sürüm:"") + "\\";

        public static YeniYazılımKontrolü_ YeniYazılımKontrolü = new YeniYazılımKontrolü_();
        public static string YeniYazılımKontrolü_Mesajı = "Yeni sürüm kontrol ediliyor";
        public static Çalıştır_ Çalıştır = new Çalıştır_();
        public static ArgeMup.HazirKod.ArkaPlan.Hatırlatıcı_ Hatırlatıcı;

        public static void Kapan(string Bilgi)
        {
            Günlük.Ekle("Kapatıldı " + Bilgi, Hemen: true);

            HttpSunucu.Bitir();
            Ortak.YeniYazılımKontrolü.Durdur();
            Ekranlar.ÖnYüzler.Durdur();
            Ekranlar.BarkodSorgulama.Durdur();
            Ekranlar.Eposta.Durdur();
            Ekranlar.GelirGiderTakip.Durdur();
            Ekranlar.Ayarlar_Etiketleme.Durdur();
            Hatırlatıcı?.AyarlarıOku(true);
            Çalıştır.Dispose();
            Klasör.Sil(Ortak.Klasör_Gecici);

            ArgeMup.HazirKod.ArkaPlan.Ortak.Çalışsın = false;
        }

        public static string[] Kullanıcı_Klasör_Yedek = null;
        public static string Kullanıcı_Klasör_Pdf = null;
        public static int Kullanıcı_KüçültüldüğündeParolaSor_sn = 60;

        public static bool YazıyıSayıyaDönüştür(ref string YazıŞeklindeSayı, string Konum, string YardımcıAçıklama = null, double EnDüşük = double.MinValue, double EnYüksek = double.MaxValue, bool Tamsayı = false)
        {
            if (YazıŞeklindeSayı == null) YazıŞeklindeSayı = "";
            int sayac_virgül = YazıŞeklindeSayı.Count(x => x == ',');
            int sayac_nokta = YazıŞeklindeSayı.Count(x => x == '.');
            if (sayac_virgül == 1 && sayac_nokta == 0) YazıŞeklindeSayı = YazıŞeklindeSayı.Replace(',', '.');

            double üretilen_s = 0;
            try
            {
                if (YazıŞeklindeSayı.Length > 1 && (sayac_virgül > 0 || sayac_nokta > 0)) YazıŞeklindeSayı = YazıŞeklindeSayı.TrimEnd('0').TrimEnd(',', '.');

                if (Tamsayı)
                {
                    string gecici_tamsayı = YazıŞeklindeSayı;

                    int knm = gecici_tamsayı.IndexOf(',');
                    if (knm >= 0) gecici_tamsayı = gecici_tamsayı.Remove(knm);

                    knm = gecici_tamsayı.IndexOf('.');
                    if (knm >= 0) gecici_tamsayı = gecici_tamsayı.Remove(knm);

                    üretilen_s = gecici_tamsayı.TamSayıya();
                }
                else üretilen_s = YazıŞeklindeSayı.NoktalıSayıya();
            }
            catch (Exception)
            {
                MessageBox.Show("Girdiğiniz ifade sayıya dönüştürülemedi." + Environment.NewLine +
                    "Lütfen tekrar giriniz" + Environment.NewLine + Environment.NewLine +
                    "Konum : " + Konum + Environment.NewLine +
                    "İçerik : " + YazıŞeklindeSayı +
                    (YardımcıAçıklama == null ? null : Environment.NewLine + Environment.NewLine + YardımcıAçıklama), "Yazıdan sayıya dönüştürme işlemi");

                return false;
            }

            string üretilen_y = üretilen_s.Yazıya();
            if (üretilen_y != YazıŞeklindeSayı)
            {
                string soru = "İçerik sayıya dönüştürülürken yeniden düzenlenmesi gerekti." + Environment.NewLine + Environment.NewLine +
                    "Konum : " + Konum + Environment.NewLine +
                    "İlk içerik : " + YazıŞeklindeSayı + Environment.NewLine +
                    "Düzenlenen : " + üretilen_y + Environment.NewLine + Environment.NewLine +
                    "Düzenlenen değer uygun ise Evet tuşuna basınız" +
                    (YardımcıAçıklama == null ? null : Environment.NewLine + Environment.NewLine + YardımcıAçıklama);

                DialogResult Dr = MessageBox.Show(soru, "Yazıdan sayıya dönüştürme işlemi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (Dr == DialogResult.No) return false;

                YazıŞeklindeSayı = üretilen_y;
            }

            if (üretilen_s < EnDüşük || üretilen_s > EnYüksek)
            {
                MessageBox.Show(üretilen_y + " sayısı belirlenen aralığın dışında," + Environment.NewLine +
                    "Lütfen tekrar giriniz" + Environment.NewLine + Environment.NewLine +
                    "Konum : " + Konum + Environment.NewLine +
                    "En düşük : " + EnDüşük.Yazıya() + Environment.NewLine +
                    "En yüksek : " + EnYüksek.Yazıya() +
                    (YardımcıAçıklama == null ? null : Environment.NewLine + Environment.NewLine + YardımcıAçıklama), "Yazıdan sayıya dönüştürme işlemi");

                return false;
            }

            return true;
        }

        public static System.Collections.Generic.Dictionary<string, string> Gösterge_UyarıVerenMalzemeler = new System.Collections.Generic.Dictionary<string, string>();
        public static Ekranlar.Bekleyiniz Gösterge = new Ekranlar.Bekleyiniz();

        static Random rnd = new Random();
        public static int RasgeleSayı(int Asgari, int Azami)
        {
            return rnd.Next(Asgari, Azami);
        }

        static Bitmap _Firma_Logo_ = null;
        static string _Firma_Logo_DosyaYolu_ = null;
        public static string Firma_Logo_DosyaYolu
        {
            get
            {
                if (_Firma_Logo_DosyaYolu_ == null)
                {
                    Bitmap l = Firma_Logo;
                }

                return _Firma_Logo_DosyaYolu_;
            }
        }
        public static Bitmap Firma_Logo
        {
            get
            {
                if (_Firma_Logo_ == null)
                {
                    var l = Directory.EnumerateFiles(Klasör_KullanıcıDosyaları, "LOGO.*", SearchOption.TopDirectoryOnly).Where(s => s.EndsWith(".bmp") || s.EndsWith(".jpg") || s.EndsWith(".png"));
                    if (l != null && l.Count() > 0)
                    {
                        _Firma_Logo_DosyaYolu_ = l.ElementAt(0);
                        _Firma_Logo_ = new Bitmap(_Firma_Logo_DosyaYolu_);
                    }

                    if (_Firma_Logo_ == null)
                    {
                        _Firma_Logo_DosyaYolu_ = Klasör_Gecici + "LOGO.png";
                        Properties.Resources.logo_512_seffaf.Save(_Firma_Logo_DosyaYolu_);
                        _Firma_Logo_ = Properties.Resources.logo_512_seffaf;
                    }
                }

                return _Firma_Logo_;
            }
        }

        public static bool Dosya_TamKopya(string Kaynak, string Hedef)
        {
            int ZamanAşımı_msn = Environment.TickCount + 15000;
            while (ZamanAşımı_msn > Environment.TickCount)
            {
                try
                {
                    Dosya.Kopyala(Kaynak, Hedef);
                    return true;
                }
                catch (Exception ex) { ex.Günlük(); }
                
                System.Threading.Thread.Sleep(100);
            }

            return false;
        }
        public static bool Klasör_TamKopya(string Kaynak, string Hedef, bool DoğrulamaKodunuKontrolEt_Yavaşlatır = true, bool AynıDoğrulamaKodunaSahipİse_DiğerFarklılıklarıGörmezdenGel = false)
        {
            int ZamanAşımı_msn = Environment.TickCount + 15000;
            while (ZamanAşımı_msn > Environment.TickCount)
            {
                try
                {
                    if (Directory.Exists(Kaynak))
                    {
                        if (Klasör.Kopyala(Kaynak, Hedef, true, DoğrulamaKodunuKontrolEt_Yavaşlatır, AynıDoğrulamaKodunaSahipİse_DiğerFarklılıklarıGörmezdenGel: AynıDoğrulamaKodunaSahipİse_DiğerFarklılıklarıGörmezdenGel)) return true;
                    }
                    else
                    {
                        Klasör.Sil(Hedef);
                        return true;
                    }
                }
                catch (Exception ex) { ex.Günlük(); }

                System.Threading.Thread.Sleep(100);
            }

            return false;
        }
        public static bool Klasör_KendiKlasörleriİçindeMi(string Klasör)
        {
            Klasör = new DirectoryInfo(Klasör).FullName; //kısa -> tam yol
            return Klasör.StartsWith(Kendi.Klasörü);
        }
        public static bool DosyaGüncelMi(string DosyaYolu, int Sürüm_Y, int Sürüm_D)
        {
            if (!File.Exists(DosyaYolu)) return false;

            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(DosyaYolu);

            if (fvi.FileMajorPart < Sürüm_Y) return false;

            if (fvi.FileMajorPart == Sürüm_Y)
            {
                if (fvi.FileMinorPart < Sürüm_D) return false;
            }

            return true;
        }
        public static void BatDosyasıCalistir(string DosyaAdı)
        {
            if (!File.Exists(Klasör_KullanıcıDosyaları + DosyaAdı)) return;

            System.Diagnostics.Process İşlem = Çalıştır.UygulamayıDoğrudanÇalıştır(Klasör_KullanıcıDosyaları + DosyaAdı, null, true);
            if (DosyaAdı.EndsWith("_Bekle.bat")) İşlem.WaitForExit();
        }
        public static void AltSayfayıYükle(Panel ÜzerineYerleştirilecekYüzey, Form AltSayfa)
        {
            AltSayfa.TopLevel = false;
            AltSayfa.FormBorderStyle = FormBorderStyle.None;
            ÜzerineYerleştirilecekYüzey.Controls.Add(AltSayfa);
            AltSayfa.Dock = DockStyle.Fill;
            AltSayfa.BringToFront();
            AltSayfa.Show();

            ÜzerineYerleştirilecekYüzey.Dock = DockStyle.Fill;
        }

        #region Yardımcı Sınıflar
        public class İşTakip_TeslimEdildi_İşSeç_Seç
        {
            public double MevcutÖnÖdeme = 0, İskonto_Oranı = 0, KDV_Oranı = 0;
            public double AlınanÖdeme = 0, AltToplam = 0;
            public double Hesaplanan_İskonto = 0, Hesaplanan_KDV = 0, Hesaplanan_GenelToplam, Hesaplanan_İşlemSonrasıÖnÖdeme = 0;

            public void Güncelle()
            {
                Hesaplanan_İskonto = 0;
                if (İskonto_Oranı > 0) Hesaplanan_İskonto = AltToplam / 100 * İskonto_Oranı;

                Hesaplanan_KDV = 0;
                if (KDV_Oranı > 0) Hesaplanan_KDV = (AltToplam - Hesaplanan_İskonto) / 100 * KDV_Oranı;

                Hesaplanan_GenelToplam = AltToplam - Hesaplanan_İskonto + Hesaplanan_KDV;
                Hesaplanan_İşlemSonrasıÖnÖdeme = MevcutÖnÖdeme + AlınanÖdeme - Hesaplanan_GenelToplam;
            }

            public string Yazdır()
            {
                //Alt Toplam (1.00 ₺) - İskonto % 10 (0.10 ₺) + KDV % 10 (0.10 ₺) = Genel Toplam  1.10 ₺
                //Mevcut Ön Ödeme (2.00 ₺) + Alınan Ödeme (2.00 ₺) = Ödemeler  4.00 ₺
                //Ödemeler - Genel Toplam = İşlem Sonrası / Müşterinin Borcu / Kalan Ön Ödeme 2.90 ₺

                return "Alt Toplam (" + Banka.Yazdır_Ücret(AltToplam) + ")" + (İskonto_Oranı > 0 ? " - İskonto % " + İskonto_Oranı + " (" + Banka.Yazdır_Ücret(Hesaplanan_İskonto) + ")" : null) + (KDV_Oranı > 0 ? " + KDV % " + KDV_Oranı + " (" + Banka.Yazdır_Ücret(Hesaplanan_KDV) + ")" : null) + " = Genel Toplam " + Banka.Yazdır_Ücret(Hesaplanan_GenelToplam) + Environment.NewLine +
                    "Mevcut Ön Ödeme (" + Banka.Yazdır_Ücret(MevcutÖnÖdeme) + ") + Alınan Ödeme (" + Banka.Yazdır_Ücret(AlınanÖdeme) + ") = Ödemeler " + Banka.Yazdır_Ücret(MevcutÖnÖdeme + AlınanÖdeme) + Environment.NewLine +
                    "Ödemeler - Genel Toplam = İşlem Sonrası " + (Hesaplanan_İşlemSonrasıÖnÖdeme < 0 ? "Müşterinin Borcu " : "Kalan Ön Ödeme ") + Banka.Yazdır_Ücret(Math.Abs(Hesaplanan_İşlemSonrasıÖnÖdeme));
            }
            public string Yazdır_Kısa()
            {
                //Alt Toplam - İskonto + KDV = 1.10 ₺
                //Mevcut Ön Ödeme + Alınan Ödeme = 4.00 ₺
                //Müşterinin Borcu / Kalan Ön Ödeme = 2.90 ₺

                return "Alt Toplam" + (İskonto_Oranı > 0 ? " - İskonto" : null) + (KDV_Oranı > 0 ? " + KDV" : null) + " = " + Banka.Yazdır_Ücret(Hesaplanan_GenelToplam) + Environment.NewLine +
                    "Mevcut Ön Ödeme + Alınan Ödeme : " + Banka.Yazdır_Ücret(MevcutÖnÖdeme + AlınanÖdeme) + Environment.NewLine +
                    (Hesaplanan_İşlemSonrasıÖnÖdeme < 0 ? "Müşterinin Borcu = " : "Kalan Ön Ödeme = ") + Banka.Yazdır_Ücret(Math.Abs(Hesaplanan_İşlemSonrasıÖnÖdeme));
            }
        }

        public static class Hatırlatıcılar
        {
            public enum Tip_ {  Boşta, KullanıcıNotu,
                                SeriNoluİş_DevamEdenTablosundan, SeriNoluİş_TakvimTablosundan, 
                                ÖdemeTalebi_KendiTablosundan, ÖdemeTalebi_TakvimTablosundan };
            public class Hatırlatıcı_
            {
                public Tip_ Tip;                    //SeriNoluİş            ÖdemeTalebi         KullanıcıNotu
                public DateTime BaşlangışTarihi;    //İş Giriş Tarihi       Dönem               Giriş Tarihi  
                public DateTime UyarıTarihi;        //Ertelenmiş Tarih      Ertelenmiş Tarih    Ertelenmiş Tarih
                public string Müşteri;              //Müşteri               Müşteri                     
                public string İçerik;               //Seri No                                   Kullanıcı Notu
            }

            public static Hatırlatıcı_[] Tümü;
            public static bool EnAz1GecikmişVar, YenidenKontrolEdilmeli = true;
            
            public static void KontrolEt()
            {
                EnAz1GecikmişVar = false;
                System.Collections.Generic.List<Hatırlatıcı_> Liste = new System.Collections.Generic.List<Hatırlatıcı_>();
                System.Collections.Generic.List<string> Müşteriler = Banka.Müşteri_Listele();

                //Kullanıcı tanımlı hatırlatıcıların kontrolü
                IDepo_Eleman Hatırlatıcılar = Banka.Tablo_Dal(null, Banka.TabloTürü.Takvim, "Hatırlatıcılar KullanıcıNotu");
                if (Hatırlatıcılar != null && Hatırlatıcılar.Elemanları.Length > 0)
                {
                    foreach (IDepo_Eleman h in Hatırlatıcılar.Elemanları)
                    {
                        Hatırlatıcı_ yeni = new Hatırlatıcı_();
                        yeni.Tip = Tip_.KullanıcıNotu;
                        yeni.BaşlangışTarihi = h.Adı.TarihSaate();
                        yeni.UyarıTarihi = h.Oku_TarihSaat(null, default, 0);
                        yeni.İçerik = h.Oku(null, null, 1);
                        Liste.Add(yeni);
                    }
                }

                //müşterilere ait ödeme taleplerinin kontrolü
                double ZamanAşımı_gün = Banka.Tablo_Dal(null, Banka.TabloTürü.Takvim, "Erteleme Süresi", true).Oku_Sayı(null, 7, 1);
                Hatırlatıcılar = Banka.Tablo_Dal(null, Banka.TabloTürü.Takvim, "Hatırlatıcılar ÖdemeTalebi", true);
                foreach (string müşteri in Müşteriler)
                {
                    string[] ÖdemeTalepleri = Banka.Dosya_Listele_Müşteri(müşteri, false);
                    foreach (string ödemetalebi in ÖdemeTalepleri)
                    {
                        Hatırlatıcı_ yeni = new Hatırlatıcı_();
                        yeni.Müşteri = müşteri;
                        yeni.BaşlangışTarihi = ödemetalebi.TarihSaate(ArgeMup.HazirKod.Dönüştürme.D_TarihSaat.Şablon_DosyaAdı2);

                        IDepo_Eleman h = Hatırlatıcılar.Bul(ödemetalebi);
                        if (h == null)
                        {
                            yeni.Tip = Tip_.ÖdemeTalebi_KendiTablosundan;
                            yeni.UyarıTarihi = yeni.BaşlangışTarihi.AddDays(ZamanAşımı_gün);
                        }
                        else
                        {
                            yeni.Tip = Tip_.ÖdemeTalebi_TakvimTablosundan;
                            yeni.UyarıTarihi = h.Oku_TarihSaat(null, default, 0);
                        }
                        
                        Liste.Add(yeni);
                    }
                }

                //takvim tablosunda bulunan fakat ödendiği için artık geçersiz olan girdilerin kontrolü
                foreach (IDepo_Eleman h in Hatırlatıcılar.Elemanları)
                {
                    Hatırlatıcı_ h_listedeki = Liste.Find(x => x.BaşlangışTarihi.Yazıya(ArgeMup.HazirKod.Dönüştürme.D_TarihSaat.Şablon_DosyaAdı2) == h.Adı);
                    if (h_listedeki == null) h.Sil(null);
                }

                //işlere ait serinoların kontrolü
                ZamanAşımı_gün = Banka.Tablo_Dal(null, Banka.TabloTürü.Takvim, "Erteleme Süresi", true).Oku_Sayı(null, 2, 0);
                Hatırlatıcılar = Banka.Tablo_Dal(null, Banka.TabloTürü.Takvim, "Hatırlatıcılar SeriNoluİş", true);
                foreach (string müşteri in Müşteriler)
                {
                    IDepo_Eleman Talepler = Banka.Tablo_Dal(müşteri, Banka.TabloTürü.DevamEden, "Talepler");
                    if (Talepler == null || Talepler.Elemanları.Length == 0) continue;

                    foreach (IDepo_Eleman sn in Talepler.Elemanları)
                    {
                        Banka.Talep_Ayıkla_SeriNoDalı(sn, out string seri_no_yazısı, out _, out _, out _, out string TeslimEdilmeTarihi, out _);

                        //Teslim edildi ise atla
                        if (TeslimEdilmeTarihi.DoluMu()) continue;

                        //son iş giriş tarihini kontrol et
                        Banka.Talep_Ayıkla_İşTürüDalı(sn.Elemanları[sn.Elemanları.Length - 1], out _, out _, out string GirişTarihi, out string ÇıkışTarihi, out _, out _, out _, out _);

                        //tamamlanmış bir iş ise atla
                        if (ÇıkışTarihi.DoluMu()) continue;

                        //devam eden bir iş
                        Hatırlatıcı_ yeni = new Hatırlatıcı_();
                        yeni.Müşteri = müşteri;
                        yeni.İçerik = seri_no_yazısı;
                        yeni.BaşlangışTarihi = GirişTarihi.TarihSaate();

                        IDepo_Eleman h = Hatırlatıcılar.Bul(seri_no_yazısı);
                        if (h == null)
                        {
                            yeni.Tip = Tip_.SeriNoluİş_DevamEdenTablosundan;
                            yeni.UyarıTarihi = yeni.BaşlangışTarihi.AddDays(ZamanAşımı_gün);
                        }
                        else
                        {
                            yeni.Tip = Tip_.SeriNoluİş_TakvimTablosundan;
                            yeni.UyarıTarihi = h.Oku_TarihSaat(null, default, 0);
                        }

                        Liste.Add(yeni);
                    }
                }

                //takvim tablosunda bulunan fakat teslim edildiği için artık geçersiz olan girdilerin kontrolü
                foreach (IDepo_Eleman h in Hatırlatıcılar.Elemanları)
                {
                    Hatırlatıcı_ h_listedeki = Liste.Find(x => x.İçerik == h.Adı);
                    if (h_listedeki == null) h.Sil(null);
                }

                Liste.Sort(new _Sıralayıcı_UyarıTarihi_EskidenYeniye());
                Tümü = Liste.ToArray();

                DateTime şimdi = DateTime.Now;
                bool Süreler_GecikmeleriGünBazındaHesapla = Banka.Tablo_Dal(null, Banka.TabloTürü.Takvim, "Erteleme Süresi", true).Oku_Bit("Gecikmeleri gün bazında hesapla", true);
                foreach (Hatırlatıcı_ h in Liste)
                {
                    if (Süreler_GecikmeleriGünBazındaHesapla) h.UyarıTarihi = new DateTime(h.UyarıTarihi.Year, h.UyarıTarihi.Month, h.UyarıTarihi.Day, 0, 0, 0);
                    
                    if (!EnAz1GecikmişVar && h.UyarıTarihi < şimdi) EnAz1GecikmişVar = true;
                }

                YenidenKontrolEdilmeli = false;
            }

            class _Sıralayıcı_UyarıTarihi_EskidenYeniye : System.Collections.Generic.IComparer<Hatırlatıcı_>
            {
                public int Compare(Hatırlatıcı_ x, Hatırlatıcı_ y)
                {
                    if (x.UyarıTarihi > y.UyarıTarihi) return 1;
                    else if (x.UyarıTarihi == y.UyarıTarihi) return 0;
                    else return -1;
                }
            }
        }
        #endregion
    }
}
