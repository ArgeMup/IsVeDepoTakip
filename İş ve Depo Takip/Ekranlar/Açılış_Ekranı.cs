using ArgeMup.HazirKod;
using ArgeMup.HazirKod.Dönüştürme;
using İş_ve_Depo_Takip.Ekranlar;
using System;
using System.Linq;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip
{
    public partial class Açılış_Ekranı : Form
    {
        UygulamaOncedenCalistirildiMi_ UyÖnÇa;

        public Açılış_Ekranı()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UyÖnÇa = new UygulamaOncedenCalistirildiMi_();
            if (UyÖnÇa.KontrolEt())
            {
                UyÖnÇa.DiğerUygulamayıÖneGetir();
                Application.Exit();
                return;
            }

            Banka.Giriş_İşlemleri();

            Text = Kendi.Adı + " " + Kendi.Sürümü_Dosya;

#if DEBUG
            //Tuş_Click(Tüm_Talepler, null);
#endif
        }
        private void Açılış_Ekranı_FormClosing(object sender, FormClosingEventArgs e)
        {
#if DEBUG
            e = new FormClosingEventArgs(CloseReason.None, false);
#endif

            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Ortak.Parola_Sor = true;
                WindowState = FormWindowState.Minimized;
            }
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Banka.Çıkış_İşlemleri();

            #region yedekleme
            Klasör_ ydk_ler = new Klasör_(Ortak.Klasör_Yedek, Filtre_Dosya: "*.zip");
            ydk_ler.Dosya_Sil_SayısınaVeBoyutunaGöre(100, 1024 * 1024 * 1024 /*1GB*/);
            ydk_ler.Güncelle(Ortak.Klasör_Yedek, Filtre_Dosya: "*.zip");

            bool yedekle = false;
            if (ydk_ler.Dosyalar.Count == 0) yedekle = true;
            else
            {
                ydk_ler.Sırala_EskidenYeniye();

                Klasör_ son_ydk = SıkıştırılmışDosya.Listele(ydk_ler.Kök + "\\" + ydk_ler.Dosyalar.Last().Yolu);
                Klasör_ güncel = new Klasör_(Ortak.Klasör_Banka);
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
                string h = Ortak.Klasör_Yedek + D_TarihSaat.Yazıya(DateTime.Now, D_TarihSaat.Şablon_DosyaAdı) + ".zip";

                SıkıştırılmışDosya.Klasörden(k, h);
            }
            #endregion
        }

        private void Tuş_Click(object sender, EventArgs e)
        {
            string a = (sender as Button).Text;
            Hide();

            switch (a)
            {
                case "Yeni İş Girişi": new Yeni_Talep_Girişi().ShowDialog(); break;
                case "Tüm İşler": new Tüm_Talepler().ShowDialog(); break;
                //case "Malzeme Girişi": new xxx().ShowDialog(); break;
                case "Müşteriler": new Müşteriler().ShowDialog(); break;
                case "İş Türleri": new İş_Türleri().ShowDialog(); break;
                case "Yazdırma": new Yazdırma().ShowDialog(); break;
                case "Ücretler": new Ücretler().ShowDialog(); break;
                //case "Malzemeler": new xxx().ShowDialog(); break;
            }

            Show();
        }
    }
}
