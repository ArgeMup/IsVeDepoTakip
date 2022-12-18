using ArgeMup.HazirKod;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip
{
    public static class Ortak
    {
        public static string Klasör_Banka = Kendi.Klasörü + "\\Banka\\";
        public static string Klasör_Banka2 = Kendi.Klasörü + "\\Banka2\\";
        public static string Klasör_İçYedek = Kendi.Klasörü + "\\Yedek\\";
        public static string Klasör_Diğer = Kendi.Klasörü + "\\Diğer\\";
		public static string Klasör_Gecici = Klasör.Depolama(Klasör.Kapsamı.Geçici) + "\\";
		
        public static string Klasör_KullanıcıYedeği = null;
        public static string Klasör_Pdf = null;
        public static bool AçılışEkranıİçinParaloİste = true;
        public static bool Eposta_hesabı_mevcut = false;

        public static TextBox BeklemeGöstergesi = null;

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
                    var l = Directory.EnumerateFiles(Klasör_Diğer, "*.*", SearchOption.AllDirectories)
                    .Where(s => s.EndsWith(".bmp") || s.EndsWith(".jpg") || s.EndsWith(".png"));
                    if (l != null && l.Count() > 0) _Yazdırma_Logo_ = new Bitmap(l.ElementAt(0));

                    if (_Yazdırma_Logo_ == null) _Yazdırma_Logo_ = Properties.Resources.logo_512_seffaf;
                }

                return _Yazdırma_Logo_;
            }
        }

        #region Pencere Konumları vb. için Geçici Depolama
        static Depo_ GeçiciDepolama = new Depo_();
        public static void GeçiciDepolama_PencereKonumları_Oku(Form Sayfa)
        {
            IDepo_Eleman p = GeçiciDepolama.Bul(Sayfa.Name, true);
            Sayfa.WindowState = (FormWindowState)p.Oku_Sayı("konum", (double)Sayfa.WindowState);
            if (Sayfa.WindowState == FormWindowState.Normal && p.Bul("x") != null)
            {
                Sayfa.Font = new Font(Sayfa.Font.FontFamily, (float)p.Oku_Sayı("yak"));
                Sayfa.Location = new System.Drawing.Point((int)p.Oku_Sayı("x"), (int)p.Oku_Sayı("y"));
                Sayfa.Size = new System.Drawing.Size((int)p.Oku_Sayı("gen"), (int)p.Oku_Sayı("uzu"));
            }

            Sayfa.Text = "ArGeMuP " + Kendi.Adı + " V" + Kendi.Sürümü_Dosya + " " + Sayfa.Text;

            Sayfa.Icon = Properties.Resources.kendi;
        }
        public static void GeçiciDepolama_PencereKonumları_Yaz(Form Sayfa)
        {
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

        public static bool Pdf_AçmayaÇalış(string DosyaYolu, int ZamanAşımı_msn = 5000)
        {
            int za = Environment.TickCount + ZamanAşımı_msn;
            while (za > Environment.TickCount)
            {
                try
                {
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    
                    System.Diagnostics.Process.Start(DosyaYolu);
                    return true;
                }
                catch (Exception) { }

                Thread.Sleep(100);
            }

            return false;
        }
    }
}
