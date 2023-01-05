using ArgeMup.HazirKod;
using ArgeMup.HazirKod.Ekİşlemler;
using İş_ve_Depo_Takip.Ekranlar;
using System;
using System.Linq;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip
{
    public partial class Açılış_Ekranı : Form
    {
        Form ÖndekiEkran = null;
        bool ÖndekiEkran_ctrl_tuşuna_basıldı = false;
        KlavyeFareGozlemcisi_ ÖndekiEkran_KlaFaGö = null;
        Timer ÖndekiEkran_Zamanlayıcı = null;

        YeniYazılımKontrolü_ YeniYazılımKontrolü = new YeniYazılımKontrolü_();
        int Parola_EnAzKarakterSayısı = 4;

        public Açılış_Ekranı()
        {
            InitializeComponent();
        
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

            if (Ortak.Kullanıcı_KüçültüldüğündeParolaSor)
            {
                ÖndekiEkran_KlaFaGö = new KlavyeFareGozlemcisi_();

                ÖndekiEkran_Zamanlayıcı = new Timer();
                ÖndekiEkran_Zamanlayıcı.Interval = 5000;
                ÖndekiEkran_Zamanlayıcı.Tick += T_Tick;
                ÖndekiEkran_Zamanlayıcı.Start();

                string[] rsm_ler = System.IO.Directory.GetFiles(Ortak.Klasör_Diğer_ArkaPlanResimleri, "*.*", System.IO.SearchOption.AllDirectories);
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
                    Form f = ÖndekiEkran == null ? this : ÖndekiEkran;
                    ÖndekiEkran_Zamanlayıcı.Stop();
                    
                    if (f.WindowState == FormWindowState.Minimized)
                    {
                        ÖndekiEkran_Zamanlayıcı.Interval = 5000;
                    }
                    else
                    {
                        if ((DateTime.Now - ÖndekiEkran_KlaFaGö.SonKlavyeFareOlayıAnı).TotalSeconds < Ortak.Kullanıcı_KüçültüldüğündeParolaSor_sn)
                        {
                            //kullanıcı bilgisayarı kullanıyor
                            f.Opacity = 1;
                            ÖndekiEkran_Zamanlayıcı.Interval = 5000;

                            if (f == this && ÖndekiEkran_Zamanlayıcı.Tag != null)
                            {
                                //parola ekranı görünüyor, resimleri döndür
                                Depo_ d = ÖndekiEkran_Zamanlayıcı.Tag as Depo_;
                                int syc = d.Oku_TamSayı("sayac");
                                System.Drawing.Image r = System.Drawing.Image.FromFile(d.Oku("resimler", null, syc++));
                                if (syc >= d.Oku_TamSayı("toplam")) syc = 0;
                                d.Yaz("sayac", syc);

                                if (P_AnaMenü.Visible)
                                {
                                    P_AnaMenü.BackgroundImage.Dispose();
                                    P_AnaMenü.BackgroundImage = r;
                                }
                                else if(P_Ayarlar.Visible)
                                {
                                    P_Ayarlar.BackgroundImage.Dispose();
                                    P_Ayarlar.BackgroundImage = r;
                                }
                                else if (P_Parola.Visible)
                                {
                                    P_Parola.BackgroundImage.Dispose();
                                    P_Parola.BackgroundImage = r;
                                }
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

                                //yedeklemeye başla
                                Parola_Kontrol.Enabled = false;
                                Hata.SetError(Parola_Kontrol, "Lütfen yedekleme devam ederken bekleyiniz");
                                new System.Threading.Tasks.Task(new Action(() =>
                                {
                                    Banka.Yedekle();

                                    Parola_Kontrol.Invoke(new Action(() =>
                                    {
                                        Hata.Clear();
                                        Parola_Kontrol.Enabled = true;
                                    }));
                                })).Start();
                            }
                        }
                    }

                    ÖndekiEkran_Zamanlayıcı.Start();
                }
            }
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
                else if (Ortak.Kullanıcı_KüçültüldüğündeParolaSor)
                {
                    Parola_Giriş.Text = "";
                    P_Parola.Visible = true;
                    P_Ayarlar.Visible = false;
                    P_AnaMenü.Visible = false;
                }
            }
            else
            {
                Opacity = 1;

                if (P_Parola.Visible) Parola_Giriş.Focus();
            }
        }
        private void Açılış_Ekranı_FormClosing(object sender, FormClosingEventArgs e)
        {
#if DEBUG
            e = new FormClosingEventArgs(CloseReason.None, false);
#endif

            if (!P_YeniParola.Visible && e.CloseReason == CloseReason.UserClosing)
            {
                if (Ortak.Kullanıcı_KüçültüldüğündeParolaSor)
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

            if (a == "Parola")
            {
                P_Ayarlar.Visible = false;
                P_YeniParola.Visible = true;
                P_YeniParola.Tag = "Normal Çalışma";
            }
            else
            {
                //Yeni yan uygulamayı oluştur
                switch (a)
                {
                    case "Yeni İş Girişi": ÖndekiEkran = new Yeni_Talep_Girişi(); break;
                    case "Tüm İşler": ÖndekiEkran = new Tüm_Talepler(); break;
                    case "Malzemeler": ÖndekiEkran = new Malzemeler(); break;
                    case "Müşteriler": ÖndekiEkran = new Müşteriler(); break;
                    case "İş Türleri": ÖndekiEkran = new İş_Türleri(); break;
                    case "Ücretler": ÖndekiEkran = new Ücretler(); break;
                    case "Bütçe": ÖndekiEkran = new Bütçe(); break;
                    case "Yazdırma": ÖndekiEkran = new Yazdırma(); break;
                    case "E-posta": ÖndekiEkran = new Ayarlar_Eposta(); break;
                    case "Diğer": ÖndekiEkran = new Ayarlar_Diğer(); break;
                }

                ÖndekiEkran.Shown += ÖndekiEkran_Shown;
                ÖndekiEkran.KeyDown += ÖndekiEkran_Tuş;
                ÖndekiEkran.KeyUp += ÖndekiEkran_Tuş;
                ÖndekiEkran.MouseWheel += ÖndekiEkran_MouseWheel;
                ÖndekiEkran.Resize += ÖndekiEkran_Resize;
                ÖndekiEkran.FormClosing += ÖndekiEkran_FormClosing;
                ÖndekiEkran.FormClosed += ÖndekiEkran_FormClosed;
                ÖndekiEkran.KeyPreview = true;
                YanUygulamayaGeç();
            }
        }
        private void ÖndekiEkran_Shown(object sender, EventArgs e)
        {
            Ortak.GeçiciDepolama_PencereKonumları_Yaz(ÖndekiEkran);
        } 
        private void ÖndekiEkran_Tuş(object sender, KeyEventArgs e)
        {
            ÖndekiEkran_ctrl_tuşuna_basıldı = e.Control;
        }
        private void ÖndekiEkran_MouseWheel(object sender, MouseEventArgs e)
        {
            if (ÖndekiEkran_ctrl_tuşuna_basıldı)
            {
                ÖndekiEkran.WindowState = FormWindowState.Normal;
                if (e.Delta > 0) ÖndekiEkran.Font = new System.Drawing.Font(ÖndekiEkran.Font.FontFamily, ÖndekiEkran.Font.Size + 0.2f);
                else ÖndekiEkran.Font = new System.Drawing.Font(ÖndekiEkran.Font.FontFamily, ÖndekiEkran.Font.Size - 0.2f);
            }
        }
        private void ÖndekiEkran_Resize(object sender, EventArgs e)
        {
            if (ÖndekiEkran.WindowState == FormWindowState.Minimized)
            {
                if (Ortak.Kullanıcı_KüçültüldüğündeParolaSor)
                {
                    ÖndekiEkran.Hide();

                    Show();
                    WindowState = FormWindowState.Minimized;
                }
            }
            else Ortak.GeçiciDepolama_PencereKonumları_Yaz(ÖndekiEkran);
        }
        private void ÖndekiEkran_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Control[] kydt = ÖndekiEkran.Controls.Find("Kaydet", true);
                if (kydt == null || kydt.Length != 1 || !kydt[0].Enabled) return;

                DialogResult Dr = MessageBox.Show("Değişiklikleri kaydetmeden çıkmak istediğinize emin misiniz?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                e.Cancel = Dr == DialogResult.No;
            }
        }
        private void ÖndekiEkran_FormClosed(object sender, FormClosedEventArgs e)
        {
            Ortak.GeçiciDepolama_PencereKonumları_Yaz(ÖndekiEkran);
            ÖndekiEkran = null;

            Show();
            WindowState = FormWindowState.Normal;
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

            if (ÖndekiEkran != null) YanUygulamayaGeç();
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
            foreach (System.Collections.Generic.KeyValuePair<string, string> a in Ortak.Gösterge_UyarıVerenMalzemeler)
            {
                //<mlz adı> : 0.0 kg\n gibi
                mesaj += a.Key + " : " + a.Value + " kaldı\n";
            }
            mesaj = mesaj.TrimEnd('\n');

            Hata.Clear();
            if (!string.IsNullOrEmpty(mesaj)) Hata.SetError(Malzemeler, mesaj);
        }

        void YanUygulamayaGeç()
        {
            try
            {
                Hide();

                Ortak.GeçiciDepolama_PencereKonumları_Oku(ÖndekiEkran);
                ÖndekiEkran.Opacity = 1;
                ÖndekiEkran.ShowDialog();
            }
            catch (Exception ex)
            {
                ex.Günlük();
                Banka.Değişiklikler_TamponuSıfırla();
                Klasör.AslınaUygunHaleGetir(Ortak.Klasör_Banka2, Ortak.Klasör_Banka, true, Ortak.EşZamanlıİşlemSayısı);

                MessageBox.Show("Bir sorun oluştu, uygulama yedekler ile kontrol edildi ve bir sorun görülmedi" + Environment.NewLine +
                    "Lütfen son işleminizi tekrar deneyiniz." + Environment.NewLine + Environment.NewLine + ex.Message, Text);

                ÖndekiEkran_FormClosed(null, null);
            }
        }
    }
}
