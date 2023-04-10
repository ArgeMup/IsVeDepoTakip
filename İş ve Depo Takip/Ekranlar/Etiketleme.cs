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
                    YeniİşGirişi_Barkodİçeriği_ = Banka.Ayarlar_Genel("Etiketler", true).Oku("Yeni İş Girişi", "http://%BilgisayarınYerelAdresi%:%HttpSunucuErişimNoktası%/%SeriNo%");
                }

                return YeniİşGirişi_Barkodİçeriği_;
            }
        }
        public static string YeniİşGirişi_Barkodİçeriği_Çözümlenmiş(string Müşteri, string Hasta, string SeriNo)
        {
            string cvp = YeniİşGirişi_Barkodİçeriği;
            cvp = cvp.Replace("%Müşteri%", Müşteri);
            cvp = cvp.Replace("%Hasta%", Hasta);
            cvp = cvp.Replace("%SeriNo%", SeriNo);
            cvp = cvp.Replace("%BilgisayarınYerelAdresi%", AğAraçları.Yerel_ip);

            int er_no = Banka.Ayarlar_BilgisayarVeKullanıcı("Http Sunucu").Oku_TamSayı(null);
            if (er_no <= 0) throw new Exception("Http sunucu kapalı olduğu için erişim noktası bilgisine ulaşılamadı, öncelikle sunucuyu etkinleştiriniz.");
            cvp = cvp.Replace("%HttpSunucuErişimNoktası%", er_no.ToString());

            return cvp;
        }
        public static string YeniİşGirişi_Barkod_Üret(string Müşteri, string Hasta, string SeriNo, bool SadeceAyarla)
        {
            string sonuç = null;
            Depo_ Depo_Komut = new Depo_();
            Depo_Komut["Komut"].İçeriği = SadeceAyarla ? new string[] { "Ayarla" } : new string[] { "Dosyaya Kaydet", Ortak.Klasör_Gecici + "Et\\Barkod.png" };
            Depo_Komut["Ayarlar", 0] = Ortak.Klasör_KullanıcıDosyaları_Etiketleme + "YeniİşGirişi_Barkod.mup";
            Depo_Komut["Güncel İçerik", 0] = YeniİşGirişi_Barkodİçeriği_Çözümlenmiş(Müşteri, Hasta, SeriNo);

            string Barkod_Uret_dosyayolu = Klasör.Depolama(Klasör.Kapsamı.Geçici, null, "Barkod_Uret", "") + "\\Barkod_Uret.exe";
            bool Barkod_Uret_tamamlandı = false;
            if (File.Exists(Barkod_Uret_dosyayolu)) Barkod_Uret_tamamlandı = true;
            else
            {
                Klasör.Oluştur(Path.GetDirectoryName(Barkod_Uret_dosyayolu));

                System.Net.WebClient İstemci = new System.Net.WebClient();
                İstemci.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(İstemci_DownloadFileCompleted);
                İstemci.DownloadFileAsync(new Uri("https://github.com/ArgeMup/Barkod_Uret/blob/main/Barkod_Uret/bin/Release/Barkod_Uret.exe?raw=true"), Barkod_Uret_dosyayolu);

                void İstemci_DownloadFileCompleted(object senderrr, System.ComponentModel.AsyncCompletedEventArgs eee)
                {
                    if (eee.Error != null) sonuç += eee.Error.Message + Environment.NewLine;

                    İstemci.Dispose();
                    Barkod_Uret_tamamlandı = true;
                }
            }

            if (!Barkod_Uret_tamamlandı)
            {
                Ortak.Gösterge.Başlat("Barkod_Uret indiriliyor", true, null, 15);
                int tümü_sayac = Environment.TickCount + 15000;
                while (!Barkod_Uret_tamamlandı && Ortak.Gösterge.Çalışsın && tümü_sayac > Environment.TickCount)
                {
                    Ortak.Gösterge.İlerleme = 1;
                    System.Threading.Thread.Sleep(1000);
                }
            }

            if (sonuç.BoşMu())
            {
                Dosya.Sil(Ortak.Klasör_Gecici + "Et\\Barkod.png");

                System.Diagnostics.Process uyg = System.Diagnostics.Process.Start(Barkod_Uret_dosyayolu, ArgeMup.HazirKod.Dönüştürme.D_Yazı.Taban64e(Depo_Komut.YazıyaDönüştür()));

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
                }
            }

            Ortak.Gösterge.Bitir();
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
            bool Etiket_tamamlandı = false;
            if (File.Exists(Etiket_dosyayolu)) Etiket_tamamlandı = true;
            else
            {
                Klasör.Oluştur(Path.GetDirectoryName(Etiket_dosyayolu));

                System.Net.WebClient İstemci = new System.Net.WebClient();
                İstemci.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(İstemci_DownloadFileCompleted);
                İstemci.DownloadFileAsync(new Uri("https://github.com/ArgeMup/Etiket/blob/main/Etiket/bin/Release/Etiket.exe?raw=true"), Etiket_dosyayolu);

                void İstemci_DownloadFileCompleted(object senderrr, System.ComponentModel.AsyncCompletedEventArgs eee)
                {
                    if (eee.Error != null) sonuç += eee.Error.Message + Environment.NewLine;

                    İstemci.Dispose();
                    Etiket_tamamlandı = true;
                }
            }

            if (!Etiket_tamamlandı)
            {
                Ortak.Gösterge.Başlat("Etiket indiriliyor", true, null, 15);
                int tümü_sayac = Environment.TickCount + 15000;
                while (!Etiket_tamamlandı && Ortak.Gösterge.Çalışsın && tümü_sayac > Environment.TickCount)
                {
                    Ortak.Gösterge.İlerleme = 1;
                    System.Threading.Thread.Sleep(1000);
                }
            }

            if (sonuç.BoşMu())
            {
                System.Diagnostics.Process uyg = System.Diagnostics.Process.Start(Etiket_dosyayolu, ArgeMup.HazirKod.Dönüştürme.D_Yazı.Taban64e(Depo_Komut.YazıyaDönüştür()));

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
                }
            }

            Ortak.Gösterge.Bitir();
            return sonuç;
        }
        #endregion

        public Etiketleme()
        {
            InitializeComponent();

            Ortak.GeçiciDepolama_PencereKonumları_Oku(this);

            YeniİşGirişi_Barkod_İçeriği.Text = YeniİşGirişi_Barkodİçeriği;
            YeniİşGirişi_Barkod_İçeriğiKaydet.Enabled = false;
        }

        #region Yeni İş Gİrişi
        private void SağTuşMenü_Barkodİçeriği_ÖnTanımlıAlan_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
            string içerik = tsmi.Tag as string;

            YeniİşGirişi_Barkod_İçeriği.Text += içerik;
        }
        private void YeniİşGirişi_Barkod_İçeriği_TextChanged(object sender, EventArgs e)
        {
            YeniİşGirişi_Barkod_İçeriğiKaydet.Enabled = true;
        }
        private void YeniİşGirişi_Barkod_İçeriğiKaydet_Click(object sender, EventArgs e)
        {
            Banka.Ayarlar_Genel("Etiketler", true).Yaz("Yeni İş Girişi", YeniİşGirişi_Barkod_İçeriği.Text);
            Banka.Değişiklikleri_Kaydet(YeniİşGirişi_Barkod_İçeriğiKaydet);
            YeniİşGirişi_Barkodİçeriği_ = null;

            YeniİşGirişi_Barkod_İçeriğiKaydet.Enabled = false;
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
