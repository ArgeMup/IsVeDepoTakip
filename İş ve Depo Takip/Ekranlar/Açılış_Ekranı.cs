using ArgeMup.HazirKod;
using ArgeMup.HazirKod.Ekİşlemler;
using İş_ve_Depo_Takip.Ekranlar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip
{
    public partial class Açılış_Ekranı : Form
    {
        YeniYazılımKontrolü_ YeniYazılımKontrolü = new YeniYazılımKontrolü_();
        int Parola_EnAzKarakterSayısı = 4;

        public Açılış_Ekranı()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Text = Kendi.Adı + " " + Kendi.Sürümü_Dosya;
            Icon = Properties.Resources.kendi;

            Controls.Add(P_AnaMenü); P_AnaMenü.Dock = DockStyle.Fill; P_AnaMenü.Visible = false;
            Controls.Add(P_Ayarlar); P_Ayarlar.Dock = DockStyle.Fill; P_Ayarlar.Visible = false;
            Controls.Add(P_Parola); P_Parola.Dock = DockStyle.Fill; P_Parola.Visible = false;
            Controls.Add(P_YeniParola); P_YeniParola.Dock = DockStyle.Fill; P_YeniParola.Visible = false;
        }
        private void Açılış_Ekranı_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();
            Banka.Giriş_İşlemleri(AçılışYazısı);
            Controls.Remove(AçılışYazısı);
            AçılışYazısı.Dispose();

#if DEBUG
            P_AnaMenü.Visible = true;

            //Tuş_Click(Yeni_Talep_Girişi, null);
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
                YeniParola_1.Focus();
            }
            else
            {
                P_Parola.Visible = true;
                Parola_Giriş.Focus();
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
                if (P_YeniParola.Visible)
                {
                    Application.Exit();
                }
                else if (Ortak.Kullanıcı_AçılışEkranıİçinParaloİste)
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

            if (!P_YeniParola.Visible && e.CloseReason == CloseReason.UserClosing)
            {
                if (Ortak.Kullanıcı_AçılışEkranıİçinParaloİste)
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

            try
            {
                switch (a)
                {
                    case "Yeni İş Girişi": new Yeni_Talep_Girişi().ShowDialog(); break;
                    case "Tüm İşler": new Tüm_Talepler().ShowDialog(); break;
                    case "Malzemeler": new Malzemeler().ShowDialog(); break;
                    case "Müşteriler": new Müşteriler().ShowDialog(); break;
                    case "İş Türleri": new İş_Türleri().ShowDialog(); break;
                    case "Yazdırma": new Yazdırma().ShowDialog(); break;
                    case "Ücretler": new Ücretler().ShowDialog(); break;
                    case "E-posta": new Ayarlar_Eposta().ShowDialog(); break;
                    case "Diğer": new Ayarlar_Diğer().ShowDialog(); break;
                    case "Parola":
                        P_Ayarlar.Visible = false;
                        P_YeniParola.Visible = true;
                        P_YeniParola.Tag = "Normal Çalışma";
                        break;
                }
            }
            catch (Exception ex) 
            { 
                ex.Günlük();
                Banka.Değişiklikler_TamponuSıfırla();
                Klasör.AslınaUygunHaleGetir(Ortak.Klasör_Banka2, Ortak.Klasör_Banka, true, Ortak.EşZamanlıİşlemSayısı);

                MessageBox.Show("Bir sorun oluştu, uygulama yedekler ile kontrol edildi ve bir sorun görülmedi" + Environment.NewLine +
                    "Lütfen son işleminizi tekrar deneyiniz." + Environment.NewLine + Environment.NewLine + ex.Message, Text);
            }

            Show();
        }

        private void YeniParola_Kaydet_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(YeniParola_1.Text)) return;
            
            if (YeniParola_1.Text.Length < Parola_EnAzKarakterSayısı)
            {
                Hata.SetError(YeniParola_Etiket, "En az " + Parola_EnAzKarakterSayısı + " karakter giriniz");
                return;
            }

            if (YeniParola_1.Text != YeniParola_2.Text)
            {
                Hata.SetError(YeniParola_Etiket, "Girilen parolalar uyuşmuyor");
                return;
            }

            Parola.Kaydet(YeniParola_1.Text);

            if ((string)P_YeniParola.Tag != "Normal Çalışma")
            {
                Klasör_ kls = new Klasör_(Ortak.Klasör_Banka);
                if (kls.Dosyalar.Count > 0)
                {
                    Hide();
                    throw new Exception("Büyük Hata AAA");
                }
            }
            
            Banka.Değişiklikleri_Kaydet();

            P_YeniParola.Visible = false;
            Parola_Giriş.Text = "";
            P_Parola.Visible = true;
        }
        private void Parola_Kontrol_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Parola_Giriş.Text) || 
                Parola_Giriş.Text.Length < Parola_EnAzKarakterSayısı ||
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

        private void P_AnaMenü_VisibleChanged(object sender, EventArgs e)
        {
            Ortak.Gösterge_UyarıVerenMalzemeler = Ortak.Gösterge_UyarıVerenMalzemeler.Where(x => !string.IsNullOrEmpty(x.Value)).ToDictionary(i => i.Key, i => i.Value);
            
            string mesaj = "";
            foreach (KeyValuePair<string, string> a in Ortak.Gösterge_UyarıVerenMalzemeler)
            {
                //<mlz adı> : 0.0 kg\n gibi
                mesaj += a.Key + " : " + a.Value + " kaldı\n";
            }
            mesaj = mesaj.TrimEnd('\n');
            
            if (string.IsNullOrEmpty(mesaj)) Hata.Clear();
            else Hata.SetError(Malzemeler, mesaj);
        }
    }
}
