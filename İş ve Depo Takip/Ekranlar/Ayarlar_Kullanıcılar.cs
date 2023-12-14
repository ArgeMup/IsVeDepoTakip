using ArgeMup.HazirKod.Ekİşlemler;
using ArgeMup.HazirKod.Ekranlar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip.Ekranlar
{
    public partial class Ayarlar_Kullanıcılar : Form
    {
        bool İlkAçılışKontrolleriniYap;
        public Kullanıcılar.İşlemTürü_ İşlemTürü;

        public Ayarlar_Kullanıcılar(Kullanıcılar.İşlemTürü_ İşlemTürü, bool AşağıAtılmış = false, bool İlkAçılışKontrolleriniYap = false)
        {
            this.İşlemTürü = İşlemTürü;
            this.İlkAçılışKontrolleriniYap = İlkAçılışKontrolleriniYap;

            #region İlkAçılışKontrolleriniYap
            if (İlkAçılışKontrolleriniYap)
            {
                Text = ArgeMup.HazirKod.Kendi.Adı + " " + Ortak.YeniYazılımKontrolü_Mesajı_Sabiti;
                Icon = Properties.Resources.kendi;

#if DEBUG
                Ortak.YeniYazılımKontrolü.Durdur();
#else
                if (!System.IO.File.Exists(Ortak.Klasör_KullanıcıDosyaları + "YeniSurumuKontrolEtme.txt"))
                {
                    Ortak.YeniYazılımKontrolü.Başlat(new Uri("https://github.com/ArgeMup/IsVeDepoTakip/blob/main/%C4%B0%C5%9F%20ve%20Depo%20Takip/bin/Release/%C4%B0%C5%9F%20ve%20Depo%20Takip.exe?raw=true"), _YeniYazılımKontrolü_GeriBildirim_);

                    void _YeniYazılımKontrolü_GeriBildirim_(bool Sonuç, string Açıklama)
                    {
                        if (Sonuç) Ortak.YeniYazılımKontrolü_Mesajı = Açıklama.Replace("Güncel ", null);
                        else
                        {
                            if (Açıklama == "Durduruldu") return;
                            else if (Açıklama.Contains("github")) Açıklama = "Bağlantı kurulamadı";

                            Ortak.YeniYazılımKontrolü_Mesajı = "V" + ArgeMup.HazirKod.Kendi.Sürümü_Dosya + " Yeni sürüm kontrol hatası : " + Açıklama.Replace("\r", null).Replace("\n", null);
                        }

                        try
                        {
                            Invoke(new Action(() =>
                            {
                                ÖnYüzler.SürümKontrolMesajınıGüncelle();
                                Text = Text.Replace(Ortak.YeniYazılımKontrolü_Mesajı_Sabiti, Ortak.YeniYazılımKontrolü_Mesajı);
                            }));
                        }
                        catch (Exception) { }
                    }
                }
                else Ortak.YeniYazılımKontrolü.Durdur();
#endif
                Banka.Giriş_İşlemleri();
            }
            #endregion

            InitializeComponent();

            switch (İşlemTürü)
            {
                default:
                case Kullanıcılar.İşlemTürü_.Giriş:
                    ÖnYüzler.PencereleriKapat();
                    if (AşağıAtılmış) WindowState = FormWindowState.Minimized;
                    Ekran.GeriBildirim_GirişBaşarılı += Ekran_GeriBildirim_GirişBaşarılı;
                    break;

                case Kullanıcılar.İşlemTürü_.ParolaDeğiştirme:
                    Ekran.GeriBildirim_Değişiklikleri_Kaydet += Ekran_Değişiklikleri_Kaydet_ParolaDeğiştirme;
                    break;

                case Kullanıcılar.İşlemTürü_.Ayarlar:
                    GelirGiderTakip.Durdur();
                    Ekran.GeriBildirim_Değişiklikleri_Kaydet += Ekran_Değişiklikleri_Kaydet_Ayarlar;
                    break;
            }

            List<string> İzinler = new List<string>();
            for (int i = 0; i < (int)Banka.Ayarlar_Kullanıcılar_İzin.DiziElemanSayısı_; i++) { İzinler.Add(((Banka.Ayarlar_Kullanıcılar_İzin)i).Yazdır()); }
            Ekran.Başlat(İşlemTürü, İzinler, Banka.Kullanıcı_İzinleri_Tutucusu);

            Show();
        }
        private void Ayarlar_Kullanıcılar_Shown(object sender, EventArgs e)
        {
            if (İlkAçılışKontrolleriniYap && !Banka.Kullanıcı_İzinleri_Tutucusu.ParolaKontrolüGerekiyorMu)
            {
                Ekran_GeriBildirim_GirişBaşarılı();
            }
            else
            {
                Text = "ArGeMuP " + ArgeMup.HazirKod.Kendi.Adı + " " + Text + " " + Ortak.YeniYazılımKontrolü_Mesajı;
                Icon = Properties.Resources.kendi;
            }
        }
        private void Ayarlar_Kullanıcılar_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (İşlemTürü == Kullanıcılar.İşlemTürü_.Giriş || e.CloseReason != CloseReason.UserClosing)
            {
                Ortak.Kapan(e.CloseReason.ToString());
                Application.Exit(); //en alttaki parola kontrol uygulamasını kapatmak için
            }
        }

        private void Ekran_GeriBildirim_GirişBaşarılı()
        {
            bool Açılış_Ekranı_nı_Göster = true;

            if (İlkAçılışKontrolleriniYap)
            {
#if !DEBUG
                Ekranlar.Eposta.Girişİşlemleri();
                Ekranlar.BarkodSorgulama.Başlat();
                HttpSunucu.Başlat();
#endif

                Ekranlar.ÖnYüzler.Başlat();

                Hide();
            }
            else
            {
                if (İşlemTürü != Kullanıcılar.İşlemTürü_.Giriş) Açılış_Ekranı_nı_Göster = false;
                İşlemTürü = Kullanıcılar.İşlemTürü_.Boşta;

                Close();
            }

            if (Açılış_Ekranı_nı_Göster) Ekranlar.ÖnYüzler.Ekle(new Açılış_Ekranı());
        }
        private void Ekran_Değişiklikleri_Kaydet_ParolaDeğiştirme()
        {
            Banka.Kullanıcı_İzinleri_Kaydet();
            Banka.Değişiklikleri_Kaydet(null);
            Close();
        }
        private void Ekran_Değişiklikleri_Kaydet_Ayarlar()
        {
            if (Banka.Kullanıcı_İzinleri_Tutucusu.ParolaKontrolüGerekiyorMu)
            {
                int AyarlarıDeğiştirebilenKullanıcıSayısı = Banka.Kullanıcı_İzinleri_Tutucusu.Kişiler.Where(x => x.Parolası.DoluMu(true) && x.İzinliMi(Banka.Ayarlar_Kullanıcılar_İzin.Ayarları_değiştirebilir)).Count();
                if (AyarlarıDeğiştirebilenKullanıcıSayısı < 1)
                {
                    Banka.Değişiklikler_TamponuSıfırla();
                    MessageBox.Show("Son değişiklik ile hiçbir kullanıcı bu sayfaya ulaşamayacak." +
                        Environment.NewLine + Environment.NewLine +
                        "Öncelikle ayarları değiştirebilme hakkına sahip bir rol oluşturun" + Environment.NewLine +
                        "Sonra bu rolu parolası olan bir kulanıcıya eşleyin.",
                        "İşlem iptal edildi");
                    Ekran.Yenile(Banka.Kullanıcı_İzinleri_Tutucusu);
                    return;
                }
            }

            Banka.Kullanıcı_İzinleri_Kaydet();
            Banka.Değişiklikleri_Kaydet(null);
        }
    }
}