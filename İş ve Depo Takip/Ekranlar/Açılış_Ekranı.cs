using ArgeMup.HazirKod.Ekİşlemler;
using System;
using System.Linq;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip.Ekranlar
{
    public partial class Açılış_Ekranı : Form, IEkran
    {
        public Açılış_Ekranı()
        {
            InitializeComponent();

            Ortak.AnaEkran = this;
           
            Controls.Add(P_AnaMenü); P_AnaMenü.Dock = DockStyle.Fill;
            Controls.Add(P_Ayarlar); P_Ayarlar.Dock = DockStyle.Fill;
        }
        private void Açılış_Ekranı_Shown(object sender, EventArgs e)
        {
            P_AnaMenü.Visible = true;
        }
        private void Açılış_Ekranı_KeyDown(object sender, KeyEventArgs e)
        {
            if (P_Ayarlar.Visible)
            {
                switch (e.KeyCode)
                {
                    case Keys.Escape:
                        Ayarlar_Geri_Click(null, null);
                        break;
                }
            }
        }
        private void Açılış_Ekranı_FormClosing(object sender, FormClosingEventArgs e)
        {
#if DEBUG
            e = new FormClosingEventArgs(CloseReason.None, false);
#endif

            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                WindowState = FormWindowState.Minimized;
            }
        }
        private void Açılış_Ekranı_FormClosed(object sender, FormClosedEventArgs e)
        {
            Enabled = false;

            Ortak.Kapan(e.CloseReason.ToString());
            Application.Exit(); //en alttaki parola kontrol uygulamasını kapatmak için
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
            if (mesaj.DoluMu(true))
            {
                Hata.SetError(Ayarlar, mesaj);
                Hata.SetError(Malzemeler, mesaj);
            }

            if (Ortak.Hatırlatıcılar.YenidenKontrolEdilmeli || Ortak.Hatırlatıcılar.EnAz1GecikmişVar)
            {
                Hata.SetError(Takvim, "Süresi dolan hatırlatıcılarınız var.");
                Takvim.BackColor = System.Drawing.Color.Salmon;
            }
            else Takvim.BackColor = System.Drawing.Color.Transparent;

            if (Banka.Yedekleme_Hatalar.DoluMu())
            {
                İpUcu.SetToolTip(YedekleKapat, Banka.Yedekleme_Hatalar);
                YedekleKapat.BackColor = System.Drawing.Color.Salmon;
            }
            else
            {
                İpUcu.SetToolTip(YedekleKapat, null);
                YedekleKapat.BackColor = System.Drawing.Color.Transparent;
            }
        }

        private void Tuş_Click(object sender, EventArgs e)
        {
            //Yeni yan uygulamayı oluştur
            Form ÖndekiEkran;
            switch ((sender as Button).Text)
            {
                case "Yeni İş Girişi":  ÖndekiEkran = new Yeni_İş_Girişi(); break;
                case "Tüm İşler":       ÖndekiEkran = new Tüm_İşler(false); break;
                case "Takvim":          ÖndekiEkran = new Takvim(); break;
                case "Korumalı Alan":   ÖndekiEkran = new Korumalı_Alan(); break;
                case "Müşteriler":      ÖndekiEkran = new Müşteriler(); break;
                case "İş Türleri":      ÖndekiEkran = new İş_Türleri(); break;
                case "Ücretler":        ÖndekiEkran = new Ücretler(); break;
                case "Bütçe":           ÖndekiEkran = new Bütçe(); break;
                case "Malzemeler":      ÖndekiEkran = new Malzemeler(); break;
                case "Yazdırma":        ÖndekiEkran = new Yazdırma(true); break;
                case "E-posta":         ÖndekiEkran = new Ayarlar_Eposta(); break;
                case "Etiketleme":      ÖndekiEkran = new Etiketleme(); break;
                case "Değişkenler":     ÖndekiEkran = new Değişkenler_Ekranı(); break;
                case "Diğer":           ÖndekiEkran = new Ayarlar_Diğer(); break;
                case "Ücret Hesaplama": ÖndekiEkran = new Yeni_İş_Girişi(SeriNoTürü:Banka.TabloTürü.ÜcretHesaplama); break;
                default : throw new Exception("Tuş_Click " + (sender as Button).Text);
            }

            Ekranlar.ÖnYüzler.Ekle(ÖndekiEkran);
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
        private void YedekleKapat_Click(object sender, EventArgs e)
        {
            if (YedekleKapat.Text == "Yeniden başlat") Application.Restart();
            else
            {
                //Yedekle ve kapat
                Ekranlar.ÖnYüzler.Durdur();
                Ortak.Gösterge.Başlat("Yedekleniyor\nBekleyiniz", false, P_AnaMenü, 0);

                Banka.Yedekleme_EnAz1Kez_Değişiklikler_Kaydedildi = true;
                Banka.Yedekle_Tümü();
                while (Banka.Yedekleme_Tümü_Çalışıyor && Ortak.Gösterge.Çalışsın)
                {
                    System.Threading.Thread.Sleep(500);
                }

                Application.Exit();
            }
        }
        private void ParolayıDeğiştir_Click(object sender, EventArgs e)
        {
            Ekranlar.ÖnYüzler.Ekle(new Parola_Kontrol(false, true));
        }
        private void BarkodGirişi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BarkodSorgulama.SeçenekleriGöster(BarkodGirişi.Text);
                BarkodGirişi.SelectAll();
            }
        }

        void IEkran.ResimDeğiştir(System.Drawing.Image Resim)
        {
            if (P_AnaMenü.Visible)
            {
                P_AnaMenü.BackgroundImage.Dispose();
                P_AnaMenü.BackgroundImage = Resim;
            }
        }
    }
}
