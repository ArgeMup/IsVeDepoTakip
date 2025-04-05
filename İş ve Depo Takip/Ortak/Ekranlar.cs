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

        public Önyüz_(Form ekran)
        {
            Ekran = ekran;
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
        static int ShiftTuşunaBasılıyor_ = 0;
        public static bool ShiftTuşunaBasılıyor
        {
            get
            {
                #if DEBUG
                    return true;
                #else
                    bool sonuç = ShiftTuşunaBasılıyor_ > Environment.TickCount;
                    ShiftTuşunaBasılıyor_ = 0;
                    return sonuç;
                #endif
            }
        } 
        static List<Önyüz_> Tümü = new List<Önyüz_>();
        static KlavyeFareGozlemcisi_ ÖndekiEkran_KlaFaGö = null;
        static System.Windows.Forms.Timer ÖndekiEkran_Zamanlayıcı = null;
        static List<string> GüncellenenSeriNolar = new List<string>();
        
        public static Form AnaEkran
        {
            get
            {
                if (Tümü.Count == 0) return null;

                return Tümü.First().Ekran;
            }
        }

        public static void Başlat()
        {
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
                Form f = Tümü.Count > 0 ? Tümü.Last().Ekran : null;
                ÖndekiEkran_Zamanlayıcı.Stop();

                if (f == null || f.WindowState == FormWindowState.Minimized)
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

            Ekran.KeyDown += Ekran_KeyDown;
            Ekran.Resize += Ekran_Resize;
            Ekran.FormClosing += Ekran_FormClosing;
            Ekran.FormClosed += Ekran_FormClosed;
            
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
        public static void PencereleriKapat_PdfleriSil()
        {
            GüncellenenSeriNolar.Clear();

            foreach (Önyüz_ önyüz in Tümü)
            {
                try
                {
                    önyüz.Ekran.KeyDown -= Ekran_KeyDown;
                    önyüz.Ekran.Resize -= Ekran_Resize;
                    önyüz.Ekran.FormClosing -= Ekran_FormClosing;
                    önyüz.Ekran.FormClosed -= Ekran_FormClosed;

                    önyüz.Ekran.Dispose();
                }
                catch (Exception) { }
            }

            Tümü.Clear();

            Dosya.Sil_SayısınaGöre(Ortak.Klasör_Gecici, 0, new List<string>() { "*.pdf" });
        }
        public static void Durdur()
        {
            ÖndekiEkran_Zamanlayıcı?.Dispose();
            ÖndekiEkran_Zamanlayıcı = null;
            ÖndekiEkran_KlaFaGö?.Dispose();
            ÖndekiEkran_KlaFaGö = null;

            PencereleriKapat_PdfleriSil();
        }

        private static void Ekran_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1: //Yeni iş girişi
                    if (Banka.K_lar.İzinliMi(Banka.K_lar.İzin.Yeni_iş_oluşturabilir)) Ekle(new Yeni_İş_Girişi());
                    break;

                case Keys.F2: //Tüm işler
                    if (Banka.K_lar.İzinliMi(new Banka.K_lar.İzin[] { Banka.K_lar.İzin.Devam_eden_işler_içinde_işlem_yapabilir, Banka.K_lar.İzin.Tamamlanmış_işler_içinde_işlem_yapabilir })) Ekle(new Tüm_İşler());
                    break;

                case Keys.F3: //Tüm işler - Arama
                    if (Banka.K_lar.İzinliMi(new Banka.K_lar.İzin[] { Banka.K_lar.İzin.Devam_eden_işler_içinde_işlem_yapabilir, Banka.K_lar.İzin.Tamamlanmış_işler_içinde_işlem_yapabilir }))
                    {
                        Tüm_İşler ti = new Tüm_İşler();
                        Ekle(ti);
                        ti.AramaPenceresiniAç();
                    }
                    break;

                case Keys.F4: //Takvim
                    if (Banka.K_lar.İzinliMi(Banka.K_lar.İzin.Takvim_içinde_işlem_yapabilir)) Ekle(new Takvim());
                    break;

                case Keys.F5: //Gelir
                    if (Banka.K_lar.İzinliMi(Banka.K_lar.İzin.Gelir_gider_ekleyebilir)) GelirGiderTakip.Komut_SayfaAç(GelirGiderTakip.Şube_Talep_Komut_.Sayfa_GelirGiderEkle, true);
                    break;

                case Keys.F6: //Gider
                    if (Banka.K_lar.İzinliMi(Banka.K_lar.İzin.Gelir_gider_ekleyebilir)) GelirGiderTakip.Komut_SayfaAç(GelirGiderTakip.Şube_Talep_Komut_.Sayfa_GelirGiderEkle);
                    break;

                case Keys.F7: //Cari döküm
                    if (Banka.K_lar.İzinliMi(Banka.K_lar.İzin.Gelir_gider_cari_dökümü_görebilir)) GelirGiderTakip.Komut_SayfaAç(GelirGiderTakip.Şube_Talep_Komut_.Sayfa_CariDöküm);
                    break;

                case Keys.Escape:
                    if (Tümü.Count > 1) Tümü.Last().Ekran.Close();
                    break;
            }

            ShiftTuşunaBasılıyor_ = e.Shift ? Environment.TickCount + 2000 : 0;
        }
        private static void Ekran_Resize(object sender, EventArgs e)
        {
            Form şimdiki = sender as Form;

            if (şimdiki.WindowState == FormWindowState.Minimized)
            {
                Banka.Yedekle_Tümü();

                if (Banka.K_lar.ParolaKontrolüGerekiyorMu)
                {
                    //şifre sayfasını aç
                    Banka.K_lar.GirişYap(true);
                }
            }
            else
            {
                şimdiki.Opacity = 1;
            }
        }
        private static void Ekran_FormClosing(object sender, FormClosingEventArgs e)
        {
            Önyüz_ öndeki;

            if (e.CloseReason == CloseReason.UserClosing)
            {
                öndeki = Tümü.First(x => x.Ekran == sender);

                foreach (Control ÜstEleman in öndeki.Ekran.Controls)
                {
                    _Bul_(ÜstEleman);
                }

                if (Tümü.Count == 1)
                {
#if DEBUG
                    Application.Exit();
#else
                    öndeki.Ekran.WindowState = FormWindowState.Minimized;
                    e.Cancel = true;
#endif
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

                Tümü.Remove(öndeki);

                if (Tümü.Count > 0)
                {
                    Önyüz_ Arkadaki = Tümü.Last();
                    Arkadaki.Ekran.Show();

                    if (Arkadaki.Ekran is Açılış_Ekranı)
                    {
                        if (Tümü.Count != 1) throw new Exception("Ekran_FormClosed - Arkadaki is Açılış_Ekranı - Tümü.Count > 1");

                        GüncellenenSeriNolar.Clear();
                    }
                    else if (öndeki.Ekran is Yeni_İş_Girişi_Açıklama) { }
                    else if (öndeki.Ekran is Yeni_İş_Girişi_Sürümler) { }
                    else if (Tümü.Count > 1)
                    {
                        if (GüncellenenSeriNolar.Count > 0 && !Arkadaki.Ekran.Disposing && !Arkadaki.Ekran.IsDisposed)
                        {
                            IGüncellenenSeriNolar arakontrol = Arkadaki.Ekran as IGüncellenenSeriNolar;
                            arakontrol?.KontrolEt(GüncellenenSeriNolar);
                        }
                    }
                }
                        
                Günlük.Ekle("Yan uygulama kapatıldı " + öndeki.Ekran.Text);
            }
        }
    }
}
