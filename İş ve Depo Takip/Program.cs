using ArgeMup.HazirKod;
using ArgeMup.HazirKod.Ekİşlemler;
using System;
using System.Threading;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip
{
    internal static class Program
    {
        static UygulamaOncedenCalistirildiMi_ UyÖnÇa;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Günlük.Başlat(Kendi.Klasörü + "\\Günlük", TümDosyaların_KapladığıAlan_bayt:5 * 1024 * 1024/*5Mib*/);
#if DEBUG
            Günlük.GenelSeviye = Günlük.Seviye.HazirKod;
#endif

            UyÖnÇa = new UygulamaOncedenCalistirildiMi_();
            if (UyÖnÇa.KontrolEt())
            {
                Günlük.Ekle("DiğerUygulamayıÖneGetir", Hemen:true);
                UyÖnÇa.DiğerUygulamayıÖneGetir();
            }
            else
            {
                Application.ThreadException += new ThreadExceptionEventHandler(BeklenmeyenDurum_KullanıcıArayüzü);
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(BeklenmeyenDurum_Uygulama);

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Banka.K_lar.Başlat();
                Banka.K_lar.GirişYap(false);
                Application.Run(new Form() { Opacity = 0, ShowInTaskbar = false, Visible = false });
                Ortak.Kapan("Normal");
            }
        }

        static void BeklenmeyenDurum_Uygulama(object sender, UnhandledExceptionEventArgs e)
        {
            AppDomain.CurrentDomain.UnhandledException -= BeklenmeyenDurum_Uygulama;
            BeklenmeyenDurum((Exception)e.ExceptionObject);
        }
        static void BeklenmeyenDurum_KullanıcıArayüzü(object sender, ThreadExceptionEventArgs t)
        {
            Application.ThreadException -= BeklenmeyenDurum_KullanıcıArayüzü;
            BeklenmeyenDurum(t.Exception);
        }
        static void BeklenmeyenDurum(Exception ex)
        {
            ex.Günlük(Hemen:true);

            try
            {
                bool işlem_yapıldı = Banka.Yedekle_SürümYükseltmeÖncesiYedeği_Kurtar();
                if (!işlem_yapıldı) Banka.Yedekle_Banka_Kurtar();

                MessageBox.Show("Bir sorun oluştu." + Environment.NewLine + Environment.NewLine +
                        "Uygulama kapatılıp yeniden başlatılacak." + Environment.NewLine + Environment.NewLine +
                        "Lütfen son işleminizi kontrol ediniz." + Environment.NewLine + Environment.NewLine +
                        ex.Message, "İş Ve Depo Takip");

                //Ekranlar.Eposta.EpostaGönder_İstisna(ex);

                Ortak.Kapan("BeklenmeyenDurum");
            }
            catch(Exception exxx)
            {
                exxx.Günlük(Hemen: true);

                MessageBox.Show("BÜYÜK bir SORUN oluştu, dosyalarınız KULLANILAMAZ durumda olabilir." + Environment.NewLine + Environment.NewLine +
                    "Uygulama kapatılıp yeniden başlatılacak. Lütfen son işleminizi kontrol ediniz." + Environment.NewLine + Environment.NewLine +
                    ex.Message + Environment.NewLine + Environment.NewLine +
                    "Üstteki hata mesajını üst üste 3. kez görüyorsanız şunları deneyebilirsiniz." + Environment.NewLine +
                    "1. Uygulamayı kapatıp BANKA klasörü içeriğini tümüyle silin." + Environment.NewLine +
                    "2. YEDEK klasöründeki en yeni yedeği (zip dosyası) BANKA klasörü içerisine çıkartın.", "İş Ve Depo Takip");
            }
            finally
            {
                #if DEBUG
                    Application.Exit();
                #else
                    Application.Restart();
                #endif
            }
        }
    }
}
