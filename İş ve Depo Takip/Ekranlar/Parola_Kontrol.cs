using ArgeMup.HazirKod;
using System;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip.Ekranlar
{
    public partial class Parola_Kontrol : Form, IEkran
    {
        const int Parola_EnAzKarakterSayısı = 4;
        readonly bool İlkAçılışKontrolleriniYap, YeniParolaOluştur;

        public Parola_Kontrol(bool İlkAçılışKontrolleriniYap, bool YeniParolaOluştur = false)
        {
            InitializeComponent();

            Ortak.ParolaGirilmesiGerekiyor = true;

            Text = Kendi.Adı + " " + Ortak.YeniYazılımKontrolü_Mesajı_Sabiti;
            Icon = Properties.Resources.kendi;
            this.İlkAçılışKontrolleriniYap = İlkAçılışKontrolleriniYap;
            this.YeniParolaOluştur = YeniParolaOluştur;

            Controls.Add(P_Parola); P_Parola.Dock = DockStyle.Fill;
            Controls.Add(P_YeniParola); P_YeniParola.Dock = DockStyle.Fill;
        }
        private void Parola_Kontrol_Shown(object sender, EventArgs e)
        {
            if (İlkAçılışKontrolleriniYap)
            {
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

                            Ortak.YeniYazılımKontrolü_Mesajı = "V" + Kendi.Sürümü_Dosya + " Yeni sürüm kontrol hatası : " + Açıklama.Replace("\r", null).Replace("\n", null);
                        }

                        Invoke(new Action(() =>
                        {
                            ÖnYüzler.SürümKontrolMesajınıGüncelle();
                            Text = Text.Replace(Ortak.YeniYazılımKontrolü_Mesajı_Sabiti, Ortak.YeniYazılımKontrolü_Mesajı);
                        }));
                    }
                }
                else Ortak.YeniYazılımKontrolü.Durdur();
#endif

                Label AçılışYazısı = new System.Windows.Forms.Label();
                AçılışYazısı.Dock = System.Windows.Forms.DockStyle.Fill;
                AçılışYazısı.Image = global::İş_ve_Depo_Takip.Properties.Resources.sag;
                AçılışYazısı.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                AçılışYazısı.Location = new System.Drawing.Point(0, 0);
                AçılışYazısı.Name = "AçılışYazısı";
                AçılışYazısı.Size = new System.Drawing.Size(378, 349);
                AçılışYazısı.TabIndex = 12;
                AçılışYazısı.Text = "Lütfen bekleyiniz";
                AçılışYazısı.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                Controls.Add(AçılışYazısı);

                Application.DoEvents();
                Banka.Giriş_İşlemleri(AçılışYazısı);

                Controls.Remove(AçılışYazısı);
                AçılışYazısı.Dispose();
            }

            if (YeniParolaOluştur)
            {
                P_YeniParola.Visible = true;
                YeniParola_1.Focus();
            }
            else
            {
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
#if DEBUG
                    if (İlkAçılışKontrolleriniYap) Kapan();
#endif
                    P_Parola.Visible = true;
                    Parola_Giriş.Focus();
                }
            }
        }
        private void Parola_Kontrol_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                if (P_YeniParola.Visible) WindowState = FormWindowState.Normal;
            }
            else if (P_Parola.Visible) Parola_Giriş.Focus();
        }
        private void Parola_Kontrol_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing || P_YeniParola.Visible) return;

            if (Ortak.ParolaGirilmesiGerekiyor)
            {
                e.Cancel = true;
                WindowState = FormWindowState.Minimized;
            }
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

            if (İlkAçılışKontrolleriniYap)
            {
                Klasör_ kls = new Klasör_(Ortak.Klasör_Banka);
                if (kls.Dosyalar.Count > 0)
                {
                    Hide();
                    throw new Exception("Büyük Hata AAA");
                }
            }
            
            Banka.Değişiklikleri_Kaydet(Tuş_YeniParola_Kaydet);

            P_YeniParola.Visible = false;
            Parola_Giriş.Text = "";
            P_Parola.Visible = true;
        }
        private void Parola_Kontrol_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Parola_Giriş.Text) || 
                Parola_Giriş.Text.Length < Parola_EnAzKarakterSayısı ||
                !Parola.KontrolEt(Parola_Giriş.Text)) return;

            Kapan();
        }
        private void Parola_Giriş_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) Parola_Kontrol_Click(null, null);
        }

        void Kapan()
        {
            Ortak.ParolaGirilmesiGerekiyor = false;

            if (İlkAçılışKontrolleriniYap)
            {
#if !DEBUG
                Ekranlar.Eposta.Girişİşlemleri();
                BarkodSorgulama.Başlat();
                HttpSunucu.Başlat();
#endif

                Ekranlar.ÖnYüzler.Başlat();

                Hide();
            }
            else
            {
                Close();
            }
        }
        void IEkran.ResimDeğiştir(System.Drawing.Image Resim)
        {
            if (P_Parola.Visible)
            {
                P_Parola.BackgroundImage.Dispose();
                P_Parola.BackgroundImage = Resim;
            }
        }
    }
}