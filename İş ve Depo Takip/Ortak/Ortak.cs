using ArgeMup.HazirKod;
using ArgeMup.HazirKod.Ekİşlemler;
using System;
using System.Collections.Generic;
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
        public static string Klasör_KullanıcıDosyaları_Ayarlar = Klasör_KullanıcıDosyaları + "Ay\\";
        public static string Klasör_KullanıcıDosyaları_ArkaPlanResimleri = Klasör_KullanıcıDosyaları + "Arka Plan Resimleri\\";
        public static string Klasör_Gecici = Klasör.Depolama(Klasör.Kapsamı.Geçici) + "\\";

        public static Ekranlar.Açılış_Ekranı AnaEkran;
        public static object[] YeniSayfaAçmaTalebi = null; //[0] Sayfanın tuşunun adı [1 ... ] varsa girdileri
        //"Yeni İş Girişi", Müşteri, SeriNo, SeriNoTürü, EkTanım
        //"Tüm İşler", null veya "Arama"
        //"Ücretler"

        public static string[] Kullanıcı_Klasör_Yedek = null;
        public static string Kullanıcı_Klasör_Pdf = null;
        public static bool Kullanıcı_KüçültüldüğündeParolaSor = true;
        public static int Kullanıcı_KüçültüldüğündeParolaSor_sn = 60;
        public static bool Kullanıcı_Eposta_hesabı_mevcut = false;

        public static bool YazıyıSayıyaDönüştür(ref string YazıŞeklindeSayı, string Konum, string YardımcıAçıklama = null, double EnDüşük = double.MinValue, double EnYüksek = double.MaxValue)
        {
            if (YazıŞeklindeSayı == null) YazıŞeklindeSayı = "";
            int sayac_virgül = YazıŞeklindeSayı.Count(x => x == ',');
            int sayac_nokta = YazıŞeklindeSayı.Count(x => x == '.');
            if (sayac_virgül == 1 && sayac_nokta == 0) YazıŞeklindeSayı = YazıŞeklindeSayı.Replace(',', '.');

            double üretilen_s = 0;
            try
            {
                üretilen_s = YazıŞeklindeSayı.NoktalıSayıya();
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
        public static void Gösterge_Açılışİşlemi(Label Gösterge, string Açıklama, ref int Tik)
        {
            int geçensüre = Environment.TickCount - Tik;
            Açıklama = Açıklama + " (" + geçensüre + " msn)";
            Gösterge.Text += Environment.NewLine + Açıklama.Günlük();
            Gösterge.Refresh();
            Tik = Environment.TickCount;
        }
        public static Ekranlar.Bekleyiniz Gösterge = new Ekranlar.Bekleyiniz();

        public static int EşZamanlıİşlemSayısı = 3;
        static Random rnd = new Random();
        public static int RasgeleSayı(int Asgari, int Azami)
        {
            return rnd.Next(Asgari, Azami);
        }

        static Bitmap _Yazdırma_Logo_ = null;
        public static Bitmap Yazdırma_Logo
        {
            get
            {
                if (_Yazdırma_Logo_ == null)
                {
                    var l = Directory.EnumerateFiles(Klasör_KullanıcıDosyaları, "*.*", SearchOption.TopDirectoryOnly)
                    .Where(s => s.EndsWith(".bmp") || s.EndsWith(".jpg") || s.EndsWith(".png"));
                    if (l != null && l.Count() > 0) _Yazdırma_Logo_ = new Bitmap(l.ElementAt(0));

                    if (_Yazdırma_Logo_ == null) _Yazdırma_Logo_ = Properties.Resources.logo_512_seffaf;
                }

                return _Yazdırma_Logo_;
            }
        }

        #region Pencere Konumları vb. için Geçici Depolama
        static Depo_ GeçiciDepolama = new Depo_();
        public static IDepo_Eleman GeçiciDepolama_PencereKonumları_Oku(Form Sayfa)
        {
            IDepo_Eleman p = GeçiciDepolama.Bul(Sayfa.Name, true);
            Sayfa.WindowState = (FormWindowState)p.Oku_Sayı("konum", (double)Sayfa.WindowState);
            if (Sayfa.WindowState == FormWindowState.Normal && p.Bul("x") != null)
            {
                Sayfa.Font = new Font(Sayfa.Font.FontFamily, (float)p.Oku_Sayı("yak"));
                Sayfa.Location = new System.Drawing.Point((int)p.Oku_Sayı("x"), (int)p.Oku_Sayı("y"));
                Sayfa.Size = new System.Drawing.Size((int)p.Oku_Sayı("gen"), (int)p.Oku_Sayı("uzu"));
            }

            if (!Sayfa.Text.StartsWith("ArGeMuP "))
            {
                Sayfa.Text = "ArGeMuP " + Kendi.Adı + " V" + Kendi.Sürümü_Dosya + " " + Sayfa.Text;
                Sayfa.Icon = Properties.Resources.kendi;
            }

            return p;
        }
        public static void GeçiciDepolama_PencereKonumları_Yaz(Form Sayfa)
        {
            if (Sayfa == null || Sayfa.Disposing || Sayfa.IsDisposed) return;

            GeçiciDepolama.Yaz(Sayfa.Name + "/konum", (double)Sayfa.WindowState);
            GeçiciDepolama.Yaz(Sayfa.Name + "/yak", Sayfa.Font.Size);
            if (Sayfa.WindowState == FormWindowState.Normal)
            {
                GeçiciDepolama.Yaz(Sayfa.Name + "/x", Sayfa.Location.X);
                GeçiciDepolama.Yaz(Sayfa.Name + "/y", Sayfa.Location.Y);
                GeçiciDepolama.Yaz(Sayfa.Name + "/gen", Sayfa.Size.Width);
                GeçiciDepolama.Yaz(Sayfa.Name + "/uzu", Sayfa.Size.Height);
            }
        }
        #endregion

        public static bool Dosya_TutmayaÇalış(string DosyaYolu, int ZamanAşımı_msn = 5000)
        {
            FileStream KilitDosyası;
            int za = Environment.TickCount + ZamanAşımı_msn;
            while (za > Environment.TickCount)
            {
                System.Threading.Thread.Sleep(100);

                try
                {
                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                    KilitDosyası = new FileStream(DosyaYolu, FileMode.Open, FileAccess.Read, FileShare.None);
                    KilitDosyası.Close();

                    return true;
                }
                catch (Exception) { }
            }

            return false;
        }

        public static bool Klasör_TamKopya(string Kaynak, string Hedef)
        {
            int ZamanAşımı_msn = Environment.TickCount + 15000;
            while (ZamanAşımı_msn > Environment.TickCount)
            {
                if (Klasör.AslınaUygunHaleGetir(Kaynak, Hedef, true, EşZamanlıİşlemSayısı)) return true;

                System.Threading.Thread.Sleep(100);
            }

            return false;
        }

        public static string[] GrupArayıcı(System.Collections.Generic.List<string> Liste, string Aranan)
        {
            string[] arananlar;

            if (string.IsNullOrEmpty(Aranan)) return Liste.ToArray();
            else
            {
                arananlar = Aranan.ToLower().Split(' ');
                return Liste.FindAll(x => KontrolEt(x)).ToArray();
            }

            bool KontrolEt(string Girdi)
            {
                Girdi = Girdi.ToLower();

                foreach (string arn in arananlar)
                {
                    if (!Girdi.Contains(arn)) return false;
                }

                return true;
            }
        }

        #region Yardımcı Sınıflar
        public class İşTakip_TeslimEdildi_İşSeç_Seç
        {
            public double AlınanÖdeme = 0, MevcutÖnÖdeme = 0, KDV_Oranı = 0, ToplamHarcama = 0, ToplamKDV = 0, İşlemSonrasıÖnÖdeme = 0;

            public string Yazdır()
            {
                //Alt Toplam (1.00 ₺) + KDV % 10 (0.10 ₺) = Genel Toplam  1.10 ₺
                //Mevcut Ön Ödeme (2.00 ₺) + Alınan Ödeme (2.00 ₺) = Ödemeler  4.00 ₺
                //Ödemeler - Genel Toplam = İşlem Sonrası / Müşterinin Borcu / Kalan Ön Ödeme 2.90 ₺

                return "Alt Toplam (" + Banka.Yazdır_Ücret(ToplamHarcama) + ") + KDV % " + KDV_Oranı + " (" + Banka.Yazdır_Ücret(ToplamKDV) + ") = Genel Toplam " + Banka.Yazdır_Ücret(ToplamHarcama + ToplamKDV) + Environment.NewLine +
                    "Mevcut Ön Ödeme (" + Banka.Yazdır_Ücret(MevcutÖnÖdeme) + ") + Alınan Ödeme (" + Banka.Yazdır_Ücret(AlınanÖdeme) + ") = Ödemeler " + Banka.Yazdır_Ücret(MevcutÖnÖdeme + AlınanÖdeme) + Environment.NewLine +
                    "Ödemeler - Genel Toplam = İşlem Sonrası " + (İşlemSonrasıÖnÖdeme < 0 ? "Müşterinin Borcu " : "Kalan Ön Ödeme ") + Banka.Yazdır_Ücret(Math.Abs(İşlemSonrasıÖnÖdeme));
            }
            public string Yazdır_Kısa()
            {
                //Alt Toplam + KDV = 1.10 ₺
                //Mevcut Ön Ödeme + Alınan Ödeme = 4.00 ₺
                //Müşterinin Borcu / Kalan Ön Ödeme = 2.90 ₺

                return "Alt Toplam" + (KDV_Oranı > 0 ? " + KDV" : null) + " = " + Banka.Yazdır_Ücret(ToplamHarcama + ToplamKDV) + Environment.NewLine +
                    "Mevcut Ön Ödeme + Alınan Ödeme : " + Banka.Yazdır_Ücret(MevcutÖnÖdeme + AlınanÖdeme) + Environment.NewLine +
                    (İşlemSonrasıÖnÖdeme < 0 ? "Müşterinin Borcu = " : "Kalan Ön Ödeme = ") + Banka.Yazdır_Ücret(Math.Abs(İşlemSonrasıÖnÖdeme));
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
                List<Hatırlatıcı_> Liste = new List<Hatırlatıcı_>();
                DateTime şimdi = DateTime.Now;
                List<string> Müşteriler = Banka.Müşteri_Listele();

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

                        if (yeni.UyarıTarihi < şimdi) EnAz1GecikmişVar = true;
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
                        if (yeni.UyarıTarihi < şimdi) EnAz1GecikmişVar = true;
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
                        Banka.Talep_Ayıkla_SeriNoDalı(sn, out string seri_no_yazısı, out _, out _, out _, out string TeslimEdilmeTarihi);

                        //Teslim edildi ise atla
                        if (TeslimEdilmeTarihi.DoluMu()) continue;

                        //son iş giriş tarihini kontrol et
                        Banka.Talep_Ayıkla_İşTürüDalı(sn.Elemanları[sn.Elemanları.Length - 1], out _, out string GirişTarihi, out string ÇıkışTarihi, out _, out _);

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
                        if (yeni.UyarıTarihi < şimdi) EnAz1GecikmişVar = true;
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
                YenidenKontrolEdilmeli = false;
            }

            class _Sıralayıcı_UyarıTarihi_EskidenYeniye : IComparer<Hatırlatıcı_>
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
