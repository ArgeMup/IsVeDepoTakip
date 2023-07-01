using ArgeMup.HazirKod;
using System.IO;
using System;
using System.Windows.Forms;
using ArgeMup.HazirKod.Ekİşlemler;

namespace İş_ve_Depo_Takip.Ekranlar
{
    public partial class Etiketleme : Form
    {
        #region STATİC Yeni İş Girişi
        static string YeniİşGirişi_Barkodİçeriği_ = null;
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
        public static string YeniİşGirişi_Barkod_Üret(string Müşteri, string Hasta, string SeriNo, bool SadeceAyarla)
        {
            string sonuç = null;
            Depo_ Depo_Komut = new Depo_();
            Depo_Komut["Komut"].İçeriği = SadeceAyarla ? new string[] { "Ayarla" } : new string[] { "Dosyaya Kaydet", Ortak.Klasör_Gecici + "Et\\Barkod.png" };
            Depo_Komut["Ayarlar", 0] = Ortak.Klasör_KullanıcıDosyaları_Etiketleme + "YeniİşGirişi_Barkod.mup";
            Depo_Komut["Güncel İçerik", 0] = YeniİşGirişi_Barkodİçeriği_Çözümlenmiş(Müşteri, Hasta, SeriNo);

            string Barkod_Uret_dosyayolu = Klasör.Depolama(Klasör.Kapsamı.Geçici, null, "Barkod_Uret", "") + "\\Barkod_Uret.exe";
            YeniYazılımKontrolü_ yyk = new YeniYazılımKontrolü_();
            if (File.Exists(Barkod_Uret_dosyayolu)) yyk.KontrolTamamlandı = true;
            else yyk.Başlat(new Uri("https://github.com/ArgeMup/Barkod_Uret/blob/main/Barkod_Uret/bin/Release/Barkod_Uret.exe?raw=true"), null, Barkod_Uret_dosyayolu);

            if (!yyk.KontrolTamamlandı)
            {
                Ortak.Gösterge.Başlat("Barkod_Uret indiriliyor", true, null, 15);
                int tümü_sayac = Environment.TickCount + 15000;
                while (!yyk.KontrolTamamlandı && Ortak.Gösterge.Çalışsın && tümü_sayac > Environment.TickCount)
                {
                    Ortak.Gösterge.İlerleme = 1;
                    System.Threading.Thread.Sleep(1000);
                }
                Ortak.Gösterge.Bitir();
            }

            if (!yyk.KontrolTamamlandı || !File.Exists(Barkod_Uret_dosyayolu)) sonuç += "Barkod_Uret indirilemedi" + Environment.NewLine;
            else
            {
                Dosya.Sil(Ortak.Klasör_Gecici + "Et\\Barkod.png");

                System.Diagnostics.Process uyg = Ortak.Çalıştır_Uygulama(Barkod_Uret_dosyayolu, Depo_Komut.YazıyaDönüştür().BaytDizisine().Taban64e());

                if (!SadeceAyarla)
                {
                    Ortak.Gösterge.Başlat("Barkod_Uret bekleniyor", true, null, 15000 / 35);
                    int tümü_sayac = Environment.TickCount + 15000;
                    while (!uyg.HasExited && Ortak.Gösterge.Çalışsın && tümü_sayac > Environment.TickCount)
                    {
                        Ortak.Gösterge.İlerleme = 1;
                        System.Threading.Thread.Sleep(35);
                    }

                    if (!uyg.HasExited) sonuç += "Barkod_Uret.exe kapanamadı" + Environment.NewLine;
                    else
                    {
                        string hatalar = Path.GetDirectoryName(Barkod_Uret_dosyayolu) + "\\Hatalar.txt";
                        if (File.Exists(hatalar)) sonuç += File.ReadAllText(hatalar) + Environment.NewLine;
                        else if (!File.Exists(Ortak.Klasör_Gecici + "Et\\Barkod.png")) sonuç += "Barkod dosyası üretilemedi" + Environment.NewLine;
                    }
                    Ortak.Gösterge.Bitir();
                }
            }
            yyk.Durdur();
            
            return sonuç;
        }
        public static string YeniİşGirişi_Etiket_Üret(string Müşteri, string Hasta, string SeriNo, string SonİşKabulTarihi, string SonİşTürü, bool SadeceAyarla)
        {
            string sonuç = YeniİşGirişi_Barkod_Üret(Müşteri, Hasta, SeriNo, false);
            if (sonuç.DoluMu()) return "Barkod Üretimi Hatalı -> " + sonuç;

            Depo_ Depo_Komut = new Depo_();
            Depo_Komut["Komut"].İçeriği = SadeceAyarla ? new string[] { "Ayarla" } : new string[] { "Yazdır" };
            Depo_Komut["Ayarlar", 0] = Ortak.Klasör_KullanıcıDosyaları_Etiketleme + "YeniİşGirişi_Etiket.mup";

            IDepo_Eleman d = Depo_Komut["Değişkenler"];
            d["Firma Adı"].İçeriği = new string[] { Banka.Ayarlar_Genel("Eposta", true).Oku("Gönderici/Adı") };
            d["Firma Logo"].İçeriği = new string[] { Ortak.Firma_Logo_DosyaYolu };
            d["Müşteri"].İçeriği = new string[] { Müşteri };
            d["Hasta"].İçeriği = new string[] { Hasta };
            d["Seri No"].İçeriği = new string[] { SeriNo };
            d["Barkod"].İçeriği = new string[] { Ortak.Klasör_Gecici + "Et\\Barkod.png" };
            d["Son İş - Kabul Tarihi"].İçeriği = new string[] { SonİşKabulTarihi };
            d["Son İş - Türü"].İçeriği = new string[] { SonİşTürü };
            d["Tarih Saat Şimdi"].İçeriği = new string[] { DateTime.Now.Yazıya() };

            string Etiket_dosyayolu = Klasör.Depolama(Klasör.Kapsamı.Geçici, null, "Etiket", "") + "\\Etiket.exe";
            YeniYazılımKontrolü_ yyk = new YeniYazılımKontrolü_();
            if (File.Exists(Etiket_dosyayolu)) yyk.KontrolTamamlandı = true;
            else yyk.Başlat(new Uri("https://github.com/ArgeMup/Etiket/blob/main/Etiket/bin/Release/Etiket.exe?raw=true"), null, Etiket_dosyayolu);

            if (!yyk.KontrolTamamlandı)
            {
                Ortak.Gösterge.Başlat("Etiket indiriliyor", true, null, 15);
                int tümü_sayac = Environment.TickCount + 15000;
                while (!yyk.KontrolTamamlandı && Ortak.Gösterge.Çalışsın && tümü_sayac > Environment.TickCount)
                {
                    Ortak.Gösterge.İlerleme = 1;
                    System.Threading.Thread.Sleep(1000);
                }
                Ortak.Gösterge.Bitir();
            }

            if (!yyk.KontrolTamamlandı || !File.Exists(Etiket_dosyayolu)) sonuç += "Etiket indirilemedi" + Environment.NewLine;
            else
            {
                System.Diagnostics.Process uyg = Ortak.Çalıştır_Uygulama(Etiket_dosyayolu, Depo_Komut.YazıyaDönüştür().BaytDizisine().Taban64e());

                if (!SadeceAyarla)
                {
                    Ortak.Gösterge.Başlat("Etiket bekleniyor", true, null, 15000 / 35);
                    int tümü_sayac = Environment.TickCount + 15000;
                    while (!uyg.HasExited && Ortak.Gösterge.Çalışsın && tümü_sayac > Environment.TickCount)
                    {
                        Ortak.Gösterge.İlerleme = 1;
                        System.Threading.Thread.Sleep(35);
                    }

                    if (!uyg.HasExited) sonuç += "Etiket.exe kapanamadı" + Environment.NewLine;
                    else
                    {
                        string hatalar = Path.GetDirectoryName(Etiket_dosyayolu) + "\\Hatalar.txt";
                        if (File.Exists(hatalar)) sonuç += File.ReadAllText(hatalar) + Environment.NewLine;
                    }
                    Ortak.Gösterge.Bitir();
                }
            }

            yyk.Durdur();
            return sonuç;
        }
        #endregion

        public Etiketleme()
        {
            InitializeComponent();

            YeniİşGirişi_Barkod_İçeriği.Text = YeniİşGirişi_Barkodİçeriği;
            YeniİşGirişi_Barkod_OkuyucuSeriPort.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());
            YeniİşGirişi_Barkod_OkuyucuSeriPort.Text = Banka.Ayarlar_BilgisayarVeKullanıcı("Barkod Okuyucu", true).Oku(null, "Kapalı");
            İpUcu.SetToolTip(YeniİşGirişi_Barkod_OkuyucuSeriPort, "Barkod okuyucu seri port durumu :" + Environment.NewLine + BarkodSorgulama.SonMesaj);
            Kaydet.Enabled = false;
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
            Kaydet.Enabled = true;
        }
        private void YeniİşGirişi_Barkod_İçeriğiKaydet_Click(object sender, EventArgs e)
        {
            Banka.Ayarlar_Genel("Etiketler", true).Yaz("Yeni İş Girişi", YeniİşGirişi_Barkod_İçeriği.Text);
            Banka.Ayarlar_BilgisayarVeKullanıcı("Barkod Okuyucu", true).Yaz(null, YeniİşGirişi_Barkod_OkuyucuSeriPort.Text);
            Banka.Değişiklikleri_Kaydet(Kaydet);
            
            YeniİşGirişi_Barkodİçeriği_ = null;
            BarkodSorgulama.Durdur();
            BarkodSorgulama.Başlat();

            Kaydet.Enabled = false;
        }

        private void YeniİşGirişi_BarkodAyarları_Click(object sender, System.EventArgs e)
        {
            YeniİşGirişi_BarkodAyarları.Enabled = false;
            string sonuç = YeniİşGirişi_Barkod_Üret("Örnek Müşteri Adı", "Örnek Hasta Adı", "Örnek Seri No", true);
            if (sonuç.DoluMu()) MessageBox.Show(sonuç, Text);
            YeniİşGirişi_BarkodAyarları.Enabled = true;
        }
        private void YeniİşGirişi_EtiketAyarları_Click(object sender, System.EventArgs e)
        {
            YeniİşGirişi_EtiketAyarları.Enabled = false;
            string sonuç = YeniİşGirişi_Etiket_Üret("Örnek Müşteri Adı", "Örnek Hasta Adı", "Örnek Seri No", "GG.AA.YYYY", "Örnek İş Türü", true);
            if (sonuç.DoluMu()) MessageBox.Show(sonuç, Text);
            YeniİşGirişi_EtiketAyarları.Enabled = true;
        }
        #endregion
    }
}
