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
            Günlük.Başlat(Kendi.Klasörü + "\\Günlük");

            UyÖnÇa = new UygulamaOncedenCalistirildiMi_();
            if (UyÖnÇa.KontrolEt()) UyÖnÇa.DiğerUygulamayıÖneGetir();
            else
            {
                Application.ThreadException += new ThreadExceptionEventHandler(BeklenmeyenDurum_KullanıcıArayüzü);
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(BeklenmeyenDurum_Uygulama);

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Ekranlar.Açılış_Ekranı());
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
            try
            {
                ex.Günlük();
                Banka.Yedekle_Banka_Kurtar();

                MessageBox.Show("Bir sorun oluştu, uygulama yedekler ile kontrol edildi ve bir sorun görülmedi" + Environment.NewLine + Environment.NewLine +
                    "Uygulama kapatılıp yeniden başlatılacak." + Environment.NewLine + Environment.NewLine +
                    "Lütfen son işleminizi tekrar deneyiniz." + Environment.NewLine + Environment.NewLine + ex.Message, "İş Ve Depo Takip");

                Ekranlar.Ayarlar_Eposta epst = new Ekranlar.Ayarlar_Eposta();
                epst.EpostaGönder_İstisna(ex);
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
