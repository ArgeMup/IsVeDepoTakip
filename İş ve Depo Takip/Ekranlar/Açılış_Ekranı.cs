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

            Controls.Add(P_AnaMenü); P_AnaMenü.Dock = DockStyle.Fill;
            Controls.Add(P_Ayarlar); P_Ayarlar.Dock = DockStyle.Fill;

            bool İzin_Tüm_Talepler = Banka.İzinliMi(new Banka.Ayarlar_Kullanıcılar_İzin[] { Banka.Ayarlar_Kullanıcılar_İzin.Devam_eden_işler_içinde_işlem_yapabilir, Banka.Ayarlar_Kullanıcılar_İzin.Tamamlanmış_işler_içinde_işlem_yapabilir });
            bool İzin_Yeni_Talep_Girişi = Banka.İzinliMi(Banka.Ayarlar_Kullanıcılar_İzin.Yeni_iş_oluşturabilir);
            Tüm_Talepler.Visible = İzin_Tüm_Talepler;
            Yeni_Talep_Girişi.Visible = İzin_Yeni_Talep_Girişi;

            ParolayıDeğiştir.Visible = !Banka.İzinliMi(Banka.Ayarlar_Kullanıcılar_İzin.Ayarları_değiştirebilir);
            ÜcretHesaplama.Visible = İzin_Yeni_Talep_Girişi;
            BarkodGirişi.Visible = İzin_Tüm_Talepler;

            Ayarlar.Visible = Banka.İzinliMi(Banka.Ayarlar_Kullanıcılar_İzin.Ayarları_değiştirebilir);
            KorumalıAlan.Visible = Banka.İzinliMi(Banka.Ayarlar_Kullanıcılar_İzin.Korumalı_alan_içinde_işlem_yapabilir);
            Takvim.Visible = Banka.İzinliMi(Banka.Ayarlar_Kullanıcılar_İzin.Takvim_içinde_işlem_yapabilir);
        }
        private void Açılış_Ekranı_Shown(object sender, EventArgs e)
        {
            P_AnaMenü.Visible = true;

            int x = (Tüm_Talepler.Visible ? Tüm_Talepler.Width : 0) + (Yeni_Talep_Girişi.Visible ? Yeni_Talep_Girişi.Width : 0);
            if (x > 0)
            {
                x += 5; //orta ayraç
                x = (P_AnaMenü.Width - x) / 2;

                if (Yeni_Talep_Girişi.Visible) { Yeni_Talep_Girişi.Left = x; x += Yeni_Talep_Girişi.Width + 5; }
                if (Tüm_Talepler.Visible) { Tüm_Talepler.Left = x; }
            }

            bool EnAz1ElemanSeçildi = false;
            x = 5;
            if (Takvim.Visible) { Takvim.Left = x; x += Takvim.Width + Hata.GetIconPadding(Takvim) + Hata.Icon.Width + 5; EnAz1ElemanSeçildi = true; }
            if (KorumalıAlan.Visible) { KorumalıAlan.Left = x; x += KorumalıAlan.Width + 5; EnAz1ElemanSeçildi = true; }
            if (Ayarlar.Visible) { Ayarlar.Left = x; EnAz1ElemanSeçildi = true; }

            //gelir giderleri yer varsa en alta at
            if (EnAz1ElemanSeçildi) x = Takvim.Top - 5 - GelirGider_Ekle.Height;
            else x = Takvim.Top;
            GelirGider_Ekle.Top = x;
            GelirGider_CariDöküm.Top = x;
            x = 5;
            if (GelirGider_Ekle.Visible) { GelirGider_Ekle.Left = x; x += GelirGider_Ekle.Width + 5; }
            if (GelirGider_CariDöküm.Visible) { GelirGider_CariDöküm.Left = x; }

            x = YedekleKapat.Top - 5;
            if (ParolayıDeğiştir.Visible) { x -= ParolayıDeğiştir.Height; ParolayıDeğiştir.Top = x; x -= 5; }
            if (BarkodGirişi.Visible) { x -= BarkodGirişi.Height; BarkodGirişi.Top = x; x -= 5; }
            if (ÜcretHesaplama.Visible) { x -= ÜcretHesaplama.Height; ÜcretHesaplama.Top = x; }
            
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
                case "Yeni İş Girişi":      ÖndekiEkran = new Yeni_İş_Girişi(); break;
                case "Tüm İşler":           ÖndekiEkran = new Tüm_İşler(false); break;
                case "Takvim":              ÖndekiEkran = new Takvim(); break;
                case "Korumalı Alan":       ÖndekiEkran = new Korumalı_Alan(); break;
                case "Müşteriler":          ÖndekiEkran = new Müşteriler(); break;
                case "İş Türleri":          ÖndekiEkran = new Ayarlar_İş_Türleri(); break;
                case "Ücretler":            ÖndekiEkran = new Ayarlar_Ücretler(); break;
                case "Bütçe":               ÖndekiEkran = new Ayarlar_Bütçe(); break;
                case "Malzemeler":          ÖndekiEkran = new Ayarlar_Malzemeler(); break;
                case "Yazdırma":            ÖndekiEkran = new Ayarlar_Yazdırma(true); break;
                case "E-posta":             ÖndekiEkran = new Ayarlar_Eposta(); break;
                case "Etiketleme":          ÖndekiEkran = new Ayarlar_Etiketleme(); break;
                case "Değişkenler":         ÖndekiEkran = new Ayarlar_Değişkenler(); break;
                case "Diğer":               ÖndekiEkran = new Ayarlar_Diğer(); break;
                case "Ücret Hesaplama":     ÖndekiEkran = new Yeni_İş_Girişi(SeriNoTürü:Banka.TabloTürü.ÜcretHesaplama); break;
                case "Kullanıcılar":        ÖndekiEkran = new Ayarlar_Kullanıcılar(ArgeMup.HazirKod.Ekranlar.Kullanıcılar.İşlemTürü_.Ayarlar); break;
                case "Parolayı Değiştir":   ÖndekiEkran = new Ayarlar_Kullanıcılar(ArgeMup.HazirKod.Ekranlar.Kullanıcılar.İşlemTürü_.ParolaDeğiştirme); break;
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
