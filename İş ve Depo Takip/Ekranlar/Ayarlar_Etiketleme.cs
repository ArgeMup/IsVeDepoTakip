using ArgeMup.HazirKod;
using System.IO;
using System;
using System.Windows.Forms;
using ArgeMup.HazirKod.Ekİşlemler;
using System.Threading;

namespace İş_ve_Depo_Takip.Ekranlar
{
    public partial class Ayarlar_Etiketleme : Form
    {
        #region STATİC Yeni İş Girişi
        static string YeniİşGirişi_Barkodİçeriği_ = null;
        public enum YeniİşGirişi_Etiketi { Kayıt, Acilİş, Açıklama }
        public static string YeniİşGirişi_Barkodİçeriği
        {
            get
            {
                if (YeniİşGirişi_Barkodİçeriği_ == null)
                {
                    YeniİşGirişi_Barkodİçeriği_ = Banka.Ayarlar_Genel("Etiketler", true).Oku("Yeni İş Girişi", "http://%BilgisayarınYerelAdresi%:%HttpSunucuErişimNoktası%/?%SeriNo%");
                }

                return YeniİşGirişi_Barkodİçeriği_;
            }
        }
        public static string YeniİşGirişi_Barkodİçeriği_Çözümlenmiş(string Müşteri, string Hasta, string SeriNo)
        {
            string cvp = YeniİşGirişi_Barkodİçeriği;
            cvp = cvp.Replace("%Müşteri%", Müşteri);
            cvp = cvp.Replace("%Hasta%", Hasta);
            cvp = cvp.Replace("%SeriNo%", "serino=" + SeriNo);

            if (cvp.Contains("%BilgisayarınYerelAdresi%"))
            {
                cvp = cvp.Replace("%BilgisayarınYerelAdresi%", AğAraçları.Yerel_ip);
            }
                
            if (cvp.Contains("%HttpSunucuErişimNoktası%"))
            {
                int er_no = Banka.Ayarlar_BilgisayarVeKullanıcı("Http Sunucu", true).Oku_TamSayı(null);
                if (er_no <= 0) throw new Exception("Http sunucu kapalı olduğu için erişim noktası bilgisine ulaşılamadı, öncelikle sunucuyu etkinleştiriniz." + Environment.NewLine + "Ayarlar -> Diğer -> Http Sunucu");
                cvp = cvp.Replace("%HttpSunucuErişimNoktası%", er_no.ToString());
            }

            return cvp;
        }
        public static string SeriNoyuBulmayaÇalış(string Barkod)
        {
            int knm_sn = Barkod.IndexOf("serino=");
            if (knm_sn >= 0)
            {
                knm_sn += 7 /*serino=*/;
                Barkod = Barkod.Substring(knm_sn);
            }
            else
            {
                knm_sn = Barkod.LastIndexOf("/");
                if (knm_sn >= 0)
                {
                    Barkod = Barkod.Substring(knm_sn + 1);
                }
            }

            for (int i = 1; i < Barkod.Length; i++)
            {
                char sıradaki = Barkod[i];
                if (sıradaki >= '0' && sıradaki <= '9') continue;

                Barkod = Barkod.Substring(0, i);
                break;
            }

            return Barkod;
        }
        public static void Durdur()
        {
            BarkodÜret_Şebeke?.Dispose();
            BarkodÜret_Şebeke = null;

            EtiketÜret_Şebeke?.Dispose();
            EtiketÜret_Şebeke = null;
        }
        #endregion

        #region Barkod Üret
        static YanUygulama.Şebeke_ BarkodÜret_Şebeke;
        static string BarkodÜret_Cevap;
        public static string YeniİşGirişi_Barkod_Üret(string Müşteri, string Hasta, string SeriNo, bool SadeceAyarla)
        {
            int ZamanAşımıAnı = Environment.TickCount + 15000;

            if (BarkodÜret_Şebeke == null)
            {
                Ortak.Gösterge.Başlat("BarkodÜret ile ilk bağlantı kuruluyor", true, null, 500);

                string EnDüşükSürüm = "0.5";
                string DosyaYolu = Klasör.Depolama(Klasör.Kapsamı.Geçici, null, "Barkod_Uret", "") + "\\Barkod_Uret.exe";
                string AğAdresi_Uygulama = "https://github.com/ArgeMup/Barkod_Uret/raw/main/Barkod_Uret/bin/Release/Barkod_Uret.exe";
                string AğAdresi_DoğrulamaKodu = "https://github.com/ArgeMup/Barkod_Uret/raw/main/Barkod_Uret/bin/Release/Barkod_Uret.exe.DogrulamaKoduUreteci";

#if DEBUG
                //AğAdresi_Uygulama = null;
                //AğAdresi_DoğrulamaKodu = null;
                AğAdresi_Uygulama = "https://github.com/ArgeMup/a/raw/main/Barkod_Uret/Barkod_Uret.exe";
                AğAdresi_DoğrulamaKodu = "https://github.com/ArgeMup/a/raw/main/Barkod_Uret/Barkod_Uret.exe.DogrulamaKoduUreteci";
#endif

                BarkodÜret_Şebeke = new YanUygulama.Şebeke_(DosyaYolu, BarkodÜret_GeriBildirim_İşlemi_Uygulama, Ortak.Çalıştır, Banka.Ayarlar_Genel("YanUygulama/Şube", true), AğAdresi_Uygulama, EnDüşükSürüm, AğAdresi_DoğrulamaKodu);

                while (!BarkodÜret_Şebeke.BağlantıKuruldu && Ortak.Gösterge.Çalışsın && ZamanAşımıAnı > Environment.TickCount && ArgeMup.HazirKod.ArkaPlan.Ortak.Çalışsın)
                {
                    Ortak.Gösterge.İlerleme = 1;
                    Thread.Sleep(30);
                }

                Ortak.Gösterge.Açıklama = "BarkodÜret bekleniyor";
            }
            else Ortak.Gösterge.Başlat("BarkodÜret bekleniyor", true, null, 500);

            if (!BarkodÜret_Şebeke.BağlantıKuruldu)
            {
                Ortak.Gösterge.Bitir();
                return "BarkodÜret ile bağlantı kurulamadı";
            }
            else if (!Dosya.Sil(Ortak.Klasör_Gecici + "Et\\Barkod.png"))
            {
                Ortak.Gösterge.Bitir();
                return "Dosya silinemedi. " + Ortak.Klasör_Gecici + "Et\\Barkod.png";
            }

            Depo_ Depo_Komut = new Depo_();
            Depo_Komut["Komut"].İçeriği = SadeceAyarla ? new string[] { "Ayarla" } : new string[] { "Dosyaya Kaydet", Ortak.Klasör_Gecici + "Et\\Barkod.png" };
            Depo_Komut["Ayarlar", 0] = Ortak.Klasör_KullanıcıDosyaları_Etiketleme + "YeniİşGirişi_Barkod.mup";
            Depo_Komut["Güncel İçerik", 0] = YeniİşGirişi_Barkodİçeriği_Çözümlenmiş(Müşteri, Hasta, SeriNo);
            Depo_Komut.Yaz("Benzersiz_Tanımlayıcı", DateTime.Now.Yazıya());

            BarkodÜret_Cevap = null;
            BarkodÜret_Şebeke.Gönder(Depo_Komut.YazıyaDönüştür().BaytDizisine());
            while (BarkodÜret_Cevap == null && Ortak.Gösterge.Çalışsın && ZamanAşımıAnı > Environment.TickCount && ArgeMup.HazirKod.ArkaPlan.Ortak.Çalışsın)
            {
                Ortak.Gösterge.İlerleme = 1;
                Thread.Sleep(30);
            }
            Ortak.Gösterge.Bitir();

            if (BarkodÜret_Cevap == null)
            {
                BarkodÜret_Şebeke.Dispose();
                BarkodÜret_Şebeke = null;

                return "BarkodÜret cevap vermesi çok uzun sürdü, tekrar deneyiniz";
            }

            if (Depo_Komut["Benzersiz_Tanımlayıcı", 0] != BarkodÜret_Cevap)
            {
                return "İşlem beklendiği şekilde tamamlanmadı, tekrar deneyiniz." + Environment.NewLine + BarkodÜret_Cevap;
            }
            else BarkodÜret_Cevap = null; //Başarılı

            if (!SadeceAyarla)
            {
                if (!File.Exists(Ortak.Klasör_Gecici + "Et\\Barkod.png"))
                {
                    return "Barkod dosyası üretilemedi.";
                }
            }

            return null;
        }
        static void BarkodÜret_GeriBildirim_İşlemi_Uygulama(bool BağlantıKuruldu, byte[] Bilgi, string Açıklama)
        {
            string içerik = Bilgi.Yazıya();
            if (!BağlantıKuruldu || içerik.BoşMu())
            {
                if (Açıklama.DoluMu()) Açıklama.Günlük("BarkodÜret ");
                return;
            }

            BarkodÜret_Cevap = içerik;
        }
        #endregion

        #region Etiket Üret
        static YanUygulama.Şebeke_ EtiketÜret_Şebeke;
        static string EtiketÜret_Cevap;
        public static string YeniİşGirişi_Etiket_Üret(YeniİşGirişi_Etiketi Tür, int KopyaSayısı, string Müşteri, string Hasta, string SeriNo, string SonİşKabulTarihi, string SonİşTürü, bool SadeceAyarla, string Açıklama = null)
        {
            string sonuç = YeniİşGirişi_Barkod_Üret(Müşteri, Hasta, SeriNo, false);
            if (sonuç.DoluMu()) return "Barkod Üretimi Hatalı -> " + sonuç;

            int ZamanAşımıAnı = Environment.TickCount + 15000;

            if (EtiketÜret_Şebeke == null)
            {
                Ortak.Gösterge.Başlat("EtiketÜret ile ilk bağlantı kuruluyor", true, null, 500);

                string EnDüşükSürüm = "0.5";
                string DosyaYolu = Klasör.Depolama(Klasör.Kapsamı.Geçici, null, "Etiket", "") + "\\Etiket.exe";
                string AğAdresi_Uygulama = "https://github.com/ArgeMup/Etiket/raw/main/Etiket/bin/Release/Etiket.exe";
                string AğAdresi_DoğrulamaKodu = "https://github.com/ArgeMup/Etiket/raw/main/Etiket/bin/Release/Etiket.exe.DogrulamaKoduUreteci";

#if DEBUG
                //AğAdresi_Uygulama = null;
                //AğAdresi_DoğrulamaKodu = null;
                AğAdresi_Uygulama = "https://github.com/ArgeMup/a/raw/main/Etiket/Etiket.exe";
                AğAdresi_DoğrulamaKodu = "https://github.com/ArgeMup/a/raw/main/Etiket/Etiket.exe.DogrulamaKoduUreteci";
#endif

                EtiketÜret_Şebeke = new YanUygulama.Şebeke_(DosyaYolu, EtiketÜret_GeriBildirim_İşlemi_Uygulama, Ortak.Çalıştır, Banka.Ayarlar_Genel("YanUygulama/Şube", true), AğAdresi_Uygulama, EnDüşükSürüm, AğAdresi_DoğrulamaKodu);

                while (!EtiketÜret_Şebeke.BağlantıKuruldu && Ortak.Gösterge.Çalışsın && ZamanAşımıAnı > Environment.TickCount && ArgeMup.HazirKod.ArkaPlan.Ortak.Çalışsın)
                {
                    Ortak.Gösterge.İlerleme = 1;
                    Thread.Sleep(30);
                }

                Ortak.Gösterge.Açıklama = "EtiketÜret bekleniyor";
            }
            else Ortak.Gösterge.Başlat("EtiketÜret bekleniyor", true, null, 500);

            if (!EtiketÜret_Şebeke.BağlantıKuruldu)
            {
                Ortak.Gösterge.Bitir();
                return "EtiketÜret ile bağlantı kurulamadı";
            }

            Depo_ Depo_Komut = new Depo_();
            Depo_Komut["Komut"].İçeriği = new string[] { SadeceAyarla ? "Ayarla" : "Yazdır", KopyaSayısı.Yazıya() };
            Depo_Komut["Ayarlar", 0] = Ortak.Klasör_KullanıcıDosyaları_Etiketleme + (Tür == YeniİşGirişi_Etiketi.Kayıt ? "YeniİşGirişi_Etiket.mup" : Tür == YeniİşGirişi_Etiketi.Acilİş ? "YeniİşGirişi_Etiket_Acil.mup" : "YeniİşGirişi_Etiket_Açıklama.mup");
            Depo_Komut.Yaz("Benzersiz_Tanımlayıcı", DateTime.Now.Yazıya());

            IDepo_Eleman d = Depo_Komut["Değişkenler"];
            d["Firma Adı"].İçeriği = new string[] { Banka.İşyeri_Adı };
            d["Firma Logo"].İçeriği = new string[] { Ortak.Firma_Logo_DosyaYolu };
            d["Müşteri"].İçeriği = new string[] { Müşteri };
            d["Hasta"].İçeriği = new string[] { Hasta };
            d["Seri No"].İçeriği = new string[] { SeriNo };
            d["Barkod"].İçeriği = new string[] { Ortak.Klasör_Gecici + "Et\\Barkod.png" };
            d["Son İş - Kabul Tarihi"].İçeriği = new string[] { SonİşKabulTarihi };
            d["Son İş - Türü"].İçeriği = new string[] { SonİşTürü };
            d["Tarih Saat Şimdi"].İçeriği = new string[] { DateTime.Now.Yazıya() };
            d["Açıklama"].İçeriği = new string[] { Açıklama };

            EtiketÜret_Cevap = null;
            EtiketÜret_Şebeke.Gönder(Depo_Komut.YazıyaDönüştür().BaytDizisine());
            while (EtiketÜret_Cevap == null && Ortak.Gösterge.Çalışsın && ZamanAşımıAnı > Environment.TickCount && ArgeMup.HazirKod.ArkaPlan.Ortak.Çalışsın)
            {
                Ortak.Gösterge.İlerleme = 1;
                Thread.Sleep(30);
            }
            Ortak.Gösterge.Bitir();

            if (EtiketÜret_Cevap == null)
            {
                EtiketÜret_Şebeke.Dispose();
                EtiketÜret_Şebeke = null;

                return "EtiketÜret cevap vermesi çok uzun sürdü, tekrar deneyiniz";
            }

            if (Depo_Komut["Benzersiz_Tanımlayıcı", 0] != EtiketÜret_Cevap)
            {
                return "İşlem beklendiği şekilde tamamlanmadı, tekrar deneyiniz." + Environment.NewLine + EtiketÜret_Cevap;
            }
            else EtiketÜret_Cevap = null; //Başarılı

            return null;
        }
        static void EtiketÜret_GeriBildirim_İşlemi_Uygulama(bool BağlantıKuruldu, byte[] Bilgi, string Açıklama)
        {
            string içerik = Bilgi.Yazıya();
            if (!BağlantıKuruldu || içerik.BoşMu())
            {
                if (Açıklama.DoluMu()) Açıklama.Günlük("EtiketÜret ");
                return;
            }

            EtiketÜret_Cevap = içerik;
        }
        #endregion

        public Ayarlar_Etiketleme()
        {
            InitializeComponent();

            YeniİşGirişi_Barkod_İçeriği.Text = YeniİşGirişi_Barkodİçeriği;
            YeniİşGirişi_Barkod_OkuyucuSeriPort.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());
            YeniİşGirişi_Barkod_OkuyucuSeriPort.Text = Banka.Ayarlar_BilgisayarVeKullanıcı("Barkod Okuyucu", true).Oku(null, "Kapalı");
            İpUcu.SetToolTip(YeniİşGirişi_Barkod_OkuyucuSeriPort, "Barkod okuyucu seri port durumu :" + Environment.NewLine + BarkodSorgulama.SonMesaj);
            ÖnYüzler_Kaydet.Enabled = false;
        }

        #region Yeni İş Gİrişi
        private void SağTuşMenü_Barkodİçeriği_ÖnTanımlıAlan_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
            string içerik = tsmi.Tag as string;

            YeniİşGirişi_Barkod_İçeriği.Text += içerik;
        }
        private void YeniİşGirişi_Barkod_AyarDeğişti(object sender, EventArgs e)
        {
            ÖnYüzler_Kaydet.Enabled = true;
        }
        private void ÖnYüzler_Kaydet_YeniİşGirişi_Barkod_İçeriği_Click(object sender, EventArgs e)
        {
            Banka.Ayarlar_Genel("Etiketler", true).Yaz("Yeni İş Girişi", YeniİşGirişi_Barkod_İçeriği.Text);
            Banka.Ayarlar_BilgisayarVeKullanıcı("Barkod Okuyucu", true).Yaz(null, YeniİşGirişi_Barkod_OkuyucuSeriPort.Text);
            Banka.Değişiklikleri_Kaydet(ÖnYüzler_Kaydet);
            
            YeniİşGirişi_Barkodİçeriği_ = null;
            BarkodSorgulama.Durdur();
            BarkodSorgulama.Başlat();

            ÖnYüzler_Kaydet.Enabled = false;
        }

        private void YeniİşGirişi_BarkodAyarları_Click(object sender, System.EventArgs e)
        {
            YeniİşGirişi_BarkodAyarları.Enabled = false;
            string sonuç = YeniİşGirişi_Barkod_Üret("Örnek Müşteri Adı", "Örnek Hasta Adı", "Örnek Seri No", true);
            if (sonuç.DoluMu()) MessageBox.Show(sonuç, Text);
            YeniİşGirişi_BarkodAyarları.Enabled = true;
        }
        private void YeniİşGirişi_KayıtEtiketiAyarları_Click(object sender, System.EventArgs e)
        {
            YeniİşGirişi_KayıtEtiketiAyarları.Enabled = false;
            string sonuç = YeniİşGirişi_Etiket_Üret(YeniİşGirişi_Etiketi.Kayıt, 1, "Örnek Müşteri Adı", "Örnek Hasta Adı", "Örnek Seri No", "GG.AA.YYYY", "Örnek İş Türü", true);
            if (sonuç.DoluMu()) MessageBox.Show(sonuç, Text);
            YeniİşGirişi_KayıtEtiketiAyarları.Enabled = true;
        }
        private void YeniİşGirişi_AcilİşEtiketiAyarları_Click(object sender, EventArgs e)
        {
            YeniİşGirişi_AcilİşEtiketiAyarları.Enabled = false;
            string sonuç = YeniİşGirişi_Etiket_Üret(YeniİşGirişi_Etiketi.Acilİş, 1, "Örnek Müşteri Adı", "Örnek Hasta Adı", "Örnek Seri No", "GG.AA.YYYY", "Örnek İş Türü", true);
            if (sonuç.DoluMu()) MessageBox.Show(sonuç, Text);
            YeniİşGirişi_AcilİşEtiketiAyarları.Enabled = true;
        }
        private void YeniİşGirişi_AçıklamaEtiketiAyarları_Click(object sender, EventArgs e)
        {
            YeniİşGirişi_AçıklamaEtiketiAyarları.Enabled = false;
            string sonuç = YeniİşGirişi_Etiket_Üret(YeniİşGirişi_Etiketi.Açıklama, 1, "Örnek Müşteri Adı", "Örnek Hasta Adı", "Örnek Seri No", "GG.AA.YYYY", "Örnek İş Türü", true, "Örnek Açıklama");
            if (sonuç.DoluMu()) MessageBox.Show(sonuç, Text);
            YeniİşGirişi_AçıklamaEtiketiAyarları.Enabled = true;
        }
        #endregion
    }
}
