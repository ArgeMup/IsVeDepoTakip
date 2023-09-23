using ArgeMup.HazirKod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip.Ekranlar
{
    class Önyüz_
    {
        public Form Ekran;
        public FormWindowState İlkDurumu;

        public Önyüz_(Form ekran)
        {
            Ekran = ekran;
            İlkDurumu = ekran.WindowState;
        }
    }
    public interface IEkran
    {
        void ResimDeğiştir(System.Drawing.Image Resim);
    }
    public interface IGüncellenenSeriNolar
    {
        void KontrolEt(List<string> GüncellenenSeriNolar);
    }

    public static class ÖnYüzler
    {
        static List<Önyüz_> Tümü = new List<Önyüz_>();
        static KlavyeFareGozlemcisi_ ÖndekiEkran_KlaFaGö = null;
        static System.Windows.Forms.Timer ÖndekiEkran_Zamanlayıcı = null;
        static List<string> GüncellenenSeriNolar = new List<string>();

        public static void Başlat()
        {
            Ekle(new Açılış_Ekranı()); //0. eleman

#if DEBUG
            ÖndekiEkran_KlaFaGö = new KlavyeFareGozlemcisi_(false, false, false);//denemelerde kasıyor
#else
            ÖndekiEkran_KlaFaGö = new KlavyeFareGozlemcisi_();
#endif

            #region ÖndekiEkran ve zamanlayıcı
            ÖndekiEkran_Zamanlayıcı = new System.Windows.Forms.Timer();
            ÖndekiEkran_Zamanlayıcı.Interval = 5000;
            ÖndekiEkran_Zamanlayıcı.Tick += T_Tick;
            ÖndekiEkran_Zamanlayıcı.Start();

            string[] rsm_ler = System.IO.Directory.GetFiles(Ortak.Klasör_KullanıcıDosyaları_ArkaPlanResimleri, "*.*", System.IO.SearchOption.AllDirectories);
            if (rsm_ler != null)
            {
                Depo_ d = new Depo_();
                int syc = 0;

                for (int i = 0; i < rsm_ler.Length; i++)
                {
                    if (rsm_ler[i].EndsWith(".bmp") || rsm_ler[i].EndsWith(".jpg") || rsm_ler[i].EndsWith(".png"))
                    {
                        d.Yaz("resimler", rsm_ler[i], syc);
                        syc++;
                    }
                }

                if (syc > 0)
                {
                    d.Yaz("sayac", 0);
                    d.Yaz("toplam", syc);

                    ÖndekiEkran_Zamanlayıcı.Tag = d;
                }
            }

            void T_Tick(object senderr, EventArgs ee)
            {
                Form f = Tümü.Last().Ekran;
                ÖndekiEkran_Zamanlayıcı.Stop();

                if (f.WindowState == FormWindowState.Minimized)
                {
                    ÖndekiEkran_Zamanlayıcı.Interval = 5000;
                }
                else
                {
#if DEBUG
                    ÖndekiEkran_KlaFaGö.SonKlavyeFareOlayıAnı = DateTime.Now; //sürenin dolmaması için
#endif

                    if ((DateTime.Now - ÖndekiEkran_KlaFaGö.SonKlavyeFareOlayıAnı).TotalSeconds < Ortak.Kullanıcı_KüçültüldüğündeParolaSor_sn)
                    {
                        //kullanıcı bilgisayarı kullanıyor
                        f.Opacity = 1;
                        ÖndekiEkran_Zamanlayıcı.Interval = 5000;

                        if (ÖndekiEkran_Zamanlayıcı.Tag != null)
                        {
                            //resimleri döndür
                            Depo_ d = ÖndekiEkran_Zamanlayıcı.Tag as Depo_;
                            int syc = d.Oku_TamSayı("sayac");
                            System.Drawing.Image r = System.Drawing.Image.FromFile(d.Oku("resimler", null, syc++));
                            if (syc >= d.Oku_TamSayı("toplam")) syc = 0;
                            d.Yaz("sayac", syc);

                            IEkran f_ekran = f as IEkran;
                            f_ekran?.ResimDeğiştir(r);
                        }
                    }
                    else
                    {
                        //bilgisayar boşta
                        ÖndekiEkran_Zamanlayıcı.Interval = 100;

                        f.Opacity -= 0.01;
                        if (f.Opacity <= 0)
                        {
                            f.WindowState = FormWindowState.Minimized;
                            f.Opacity = 1;
                        }
                    }
                }

                ÖndekiEkran_Zamanlayıcı.Start();
            }
            #endregion
        }
        public static void Ekle(Form Ekran)
        {
            if (Tümü.Count > 15)
            {
                MessageBox.Show("15 olan azami pencere sınırına ulaştınız, mevcut pencereleri kapatıp tekrar deneyiniz.", "Açık Pencereler Hk.");
                return;
            }

            if (Tümü.Count > 0)
            {
                Form son_açılan = Tümü.Last().Ekran;
                son_açılan.Hide();
            }

            if (Ekran is Parola_Kontrol)
            {
                Ekran.FormClosed += Ekran_FormClosed;
            }
            else if (Ekran is Açılış_Ekranı)
            {
                Ekran.KeyDown += Ekran_KeyDown;
                Ekran.Resize += Ekran_Resize;
            }
            else
            {
                Ekran.KeyDown += Ekran_KeyDown;
                Ekran.Resize += Ekran_Resize;
                Ekran.FormClosing += Ekran_FormClosing;
                Ekran.FormClosed += Ekran_FormClosed;
            }

            Ekran.Text = "ArGeMuP " + Kendi.Adı + " " + Ekran.Text + " " + Ortak.YeniYazılımKontrolü_Mesajı;
            Ekran.Icon = Properties.Resources.kendi;
            Ekran.KeyPreview = true;

            Tümü.Add(new Önyüz_(Ekran));
            Ekran.Show();

            Günlük.Ekle("Yan uygulama açıldı " + Ekran.Text);
        }
        public static void GüncellenenSeriNoyuİşaretle(string SeriNo)
        {
            GüncellenenSeriNolar.Add(SeriNo);
        }
        public static void Durdur()
        {
            GüncellenenSeriNolar.Clear();

            ÖndekiEkran_Zamanlayıcı?.Dispose();
            ÖndekiEkran_Zamanlayıcı = null;
            ÖndekiEkran_KlaFaGö?.Dispose();
            ÖndekiEkran_KlaFaGö = null;

            for (int i = Tümü.Count - 1; i > 0; i--) //açılış ekranı hariç
            {
                try
                {
                    Tümü[i].Ekran.Dispose();
                }
                catch (Exception) { }
            }
            Tümü.Clear();
        }
        public static void SürümKontrolMesajınıGüncelle()
        {
            foreach (Önyüz_ önyüz in Tümü)
            {
                önyüz.Ekran.Text = önyüz.Ekran.Text.Replace(Ortak.YeniYazılımKontrolü_Mesajı_Sabiti, Ortak.YeniYazılımKontrolü_Mesajı);
            }
        }

        private static void Ekran_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                    Ekle(new Yeni_İş_Girişi());
                    break;

                case Keys.F2:
                    Ekle(new Tüm_İşler(false));
                    break;

                case Keys.F3:
                    Ekle(new Tüm_İşler(true));
                    break;

                case Keys.F4:
                    Ekle(new Takvim());
                    break;

                case Keys.Escape:
                    if (Tümü.Count > 1) Tümü.Last().Ekran.Close();
                    break;
            }
        }
        private static void Ekran_Resize(object sender, EventArgs e)
        {
            Form şimdiki = sender as Form;

            if (şimdiki.WindowState == FormWindowState.Minimized)
            {
                if (Ortak.Kullanıcı_KüçültüldüğündeParolaSor) Ortak.ParolaGirilmesiGerekiyor = true;
                
                Banka.Yedekle_Tümü();
            }
            else
            {
                şimdiki.Opacity = 1;

                if (Ortak.ParolaGirilmesiGerekiyor)
                {
                    //şifre sayfasını aç
                    Ekranlar.ÖnYüzler.Ekle(new Parola_Kontrol(false));
                }
            }
        }
        private static void Ekran_FormClosing(object sender, FormClosingEventArgs e)
        {
            Önyüz_ öndeki;

            if (e.CloseReason == CloseReason.UserClosing)
            {
                öndeki = Tümü.First(x => x.Ekran == sender);
                if (öndeki != Tümü.Last()) throw new System.Exception("1 Listedeki son eleman olmamasına rağmen kapatma isteği geldi " + öndeki.Ekran.Text);

                foreach (Control ÜstEleman in öndeki.Ekran.Controls)
                {
                    _Bul_(ÜstEleman);
                }
            }

            bool _Bul_(Control Eleman)
            {
                if (Eleman.Name.StartsWith("ÖnYüzler_Kaydet") && Eleman.Enabled)
                {
                    DialogResult Dr = MessageBox.Show("Değişiklikleri kaydetmeden çıkmak istediğinize emin misiniz?", öndeki.Ekran.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    e.Cancel = Dr == DialogResult.No;
                    return true;
                }

                foreach (Control AltEleman in Eleman.Controls)
                {
                    if (_Bul_(AltEleman)) return true;
                }

                return false;
            }
        }
        private static void Ekran_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Önyüz_ öndeki = Tümü.First(x => x.Ekran == sender);
                if (öndeki != Tümü.Last()) throw new System.Exception("2 Listedeki son eleman olmamasına rağmen kapatma isteği geldi " + öndeki.Ekran.Text);

                if (Ortak.ParolaGirilmesiGerekiyor) throw new System.Exception("Ortak.ParolaGirilmesiGerekiyor " + öndeki.Ekran.Text);

                Tümü.Remove(öndeki);
                if (Tümü.Count == 1) GüncellenenSeriNolar.Clear();

                Önyüz_ Arkadaki = Tümü.Last();
                Arkadaki.Ekran.Show();
                if (GüncellenenSeriNolar.Count > 0 && !Arkadaki.Ekran.Disposing && !Arkadaki.Ekran.IsDisposed)
                {
                    IGüncellenenSeriNolar arakontrol = Arkadaki.Ekran as IGüncellenenSeriNolar;
                    arakontrol?.KontrolEt(GüncellenenSeriNolar);
                }

                if (öndeki.Ekran is Parola_Kontrol)
                {
                    Arkadaki.Ekran.WindowState = Arkadaki.İlkDurumu;
                }

                Günlük.Ekle("Yan uygulama kapatıldı " + öndeki.Ekran.Text);
            }
            else
            {
                Ortak.Kapan(e.CloseReason.ToString());
                Application.Exit(); //en alttaki parola kontrol uygulamasını kapatmak için
            }
        }
    }
}
