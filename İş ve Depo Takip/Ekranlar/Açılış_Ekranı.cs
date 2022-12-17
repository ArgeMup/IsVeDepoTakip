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
        YeniYazılımKontrolü_ YeniYazılımKontrolü = new YeniYazılımKontrolü_();

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
            Icon = Properties.Resources.kendi;

            Controls.Add(P_AnaMenü); P_AnaMenü.Dock = DockStyle.Fill; P_AnaMenü.Visible = false;
            Controls.Add(P_Ayarlar); P_Ayarlar.Dock = DockStyle.Fill; P_Ayarlar.Visible = false;
            Controls.Add(P_Parola); P_Parola.Dock = DockStyle.Fill; P_Parola.Visible = false;
            Controls.Add(P_YeniParola); P_YeniParola.Dock = DockStyle.Fill; P_YeniParola.Visible = false;

#if DEBUG
            P_AnaMenü.Visible = true;

            //Tuş_Click(Tüm_Talepler, null);
#else
            IDepo_Eleman b_kullanıcı = Banka.Tablo_Dal(null, Banka.TabloTürü.Ayarlar, "Kullanıcı Şifresi");
            if (b_kullanıcı == null || string.IsNullOrEmpty(b_kullanıcı[0]))
            {
                //ilk kez çalışırılıyor
                Klasör_ kls = new Klasör_(Ortak.Klasör_Banka);
                if (kls.Dosyalar.Count > 0)
                {
                    Hide();
                    throw new Exception("Büyük Hata AA");
                }

                P_YeniParola.Visible = true;
            }
            else
            {
                P_Parola.Visible = true;
            }

            YeniYazılımKontrolü.Başlat(new Uri("https://github.com/ArgeMup/IsVeDepoTakip/blob/main/%C4%B0%C5%9F%20ve%20Depo%20Takip/bin/Release/%C4%B0%C5%9F%20ve%20Depo%20Takip.exe?raw=true"));
#endif
        }
        private void Açılış_Ekranı_DoubleClick(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void Açılış_Ekranı_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                if (!P_YeniParola.Visible)
                {
                    Parola_Giriş.Text = "";
                    P_Parola.Visible = true;
                    P_Ayarlar.Visible = false;
                    P_AnaMenü.Visible = false;
                }
            }
            else if (P_Parola.Visible) Parola_Giriş.Focus();
        }
        private void Açılış_Ekranı_FormClosing(object sender, FormClosingEventArgs e)
        {
#if DEBUG
            e = new FormClosingEventArgs(CloseReason.None, false);
#endif

            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (!P_YeniParola.Visible)
                {
                    Parola_Giriş.Text = "";
                    P_Parola.Visible = true;
                    P_Ayarlar.Visible = false;
                    P_AnaMenü.Visible = false;
                }

                e.Cancel = true;
                WindowState = FormWindowState.Minimized;
            }
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Banka.Çıkış_İşlemleri();

            YeniYazılımKontrolü.Durdur();
            ArgeMup.HazirKod.ArkaPlan.Ortak.Çalışsın = false;
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

        private void YeniParola_Kaydet_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(YeniParola_1.Text)) return;

            if (YeniParola_1.Text.Length < 6)
            {
                Hata.SetError(YeniParola_Etiket, "En az 6 karakter giriniz");
                return;
            }

            if (YeniParola_1.Text != YeniParola_2.Text)
            {
                Hata.SetError(YeniParola_Etiket, "Girilen parolalar uyuşmuyor");
                return;
            }

            Parola.Kaydet(YeniParola_1.Text);

            Klasör_ kls = new Klasör_(Ortak.Klasör_Banka);
            if (kls.Dosyalar.Count > 0)
            {
                Hide();
                throw new Exception("Büyük Hata AAA");
            }

            Banka.Değişiklikleri_Kaydet();

            P_YeniParola.Visible = false;
            Parola_Giriş.Text = "";
            P_Parola.Visible = true;
        }
        private void Parola_Kontrol_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Parola_Giriş.Text) || 
                Parola_Giriş.Text.Length < 6 ||
                !Parola.KontrolEt(Parola_Giriş.Text)) return;

            P_Parola.Visible = false;
            P_AnaMenü.Visible = true;
        }
        private void Ayarlar_Click(object sender, EventArgs e)
        {
            P_AnaMenü.Visible = false;
            P_Ayarlar.Visible = true;
        }
        private void Ayarlar_Geri_Click(object sender, EventArgs e)
        {
            P_Ayarlar.Visible = false;
            P_AnaMenü.Visible = true;
        }
        private void Parola_Giriş_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) Parola_Kontrol_Click(null, null);
        }
    }
}
