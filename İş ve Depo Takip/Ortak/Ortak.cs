using ArgeMup.HazirKod;
using ArgeMup.HazirKod.Ekİşlemler;
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
        public static string Klasör_Diğer_ArkaPlanResimleri = Klasör_Diğer + "Arka Plan Resimleri\\";
        public static string Klasör_Gecici = Klasör.Depolama(Klasör.Kapsamı.Geçici) + "\\";

        public static Açılış_Ekranı AnaEkran;
        public static object[] YeniSayfaAçmaTalebi = null; //[0] Sayfanın tuşunun adı [1 ... ] varsa girdileri

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

        public static TextBox Gösterge_UzunİşlemİçinBekleyiniz = null;
        public static System.Collections.Generic.Dictionary<string, string> Gösterge_UyarıVerenMalzemeler = new System.Collections.Generic.Dictionary<string, string>();
        public static void Gösterge_Açılışİşlemi(Label Gösterge, string Açıklama, ref int Tik)
        {
            int geçensüre = Environment.TickCount - Tik;
            Açıklama = Açıklama + " (" + geçensüre + " msn)";
            Gösterge.Text += Environment.NewLine + Açıklama.Günlük();
            Gösterge.Refresh();
            Tik = Environment.TickCount;
        }

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
                    var l = Directory.EnumerateFiles(Klasör_Diğer, "*.*", SearchOption.TopDirectoryOnly)
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

            if (!Sayfa.Text.StartsWith("ArGeMuP "))
            {
                Sayfa.Text = "ArGeMuP " + Kendi.Adı + " V" + Kendi.Sürümü_Dosya + " " + Sayfa.Text;
                Sayfa.Icon = Properties.Resources.kendi;
            }
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
                Thread.Sleep(100);

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
    }
}
