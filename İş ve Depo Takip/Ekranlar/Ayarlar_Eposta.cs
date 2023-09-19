using ArgeMup.HazirKod;
using ArgeMup.HazirKod.Ekİşlemler;
using System;
using System.IO;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip.Ekranlar
{
    public partial class Ayarlar_Eposta : Form
    {
        IDepo_Eleman Ayarlar = null;

        public Ayarlar_Eposta()
        {
            InitializeComponent();

            Ayarlar = Banka.Ayarlar_Genel("Eposta", true);

            Smtp_Sunucu_Adres.Text = Ayarlar.Oku("Sunucu Smtp/Adresi", Smtp_Sunucu_Adres.Text);
            Smtp_Sunucu_ErişimNoktası.Text = Ayarlar.Oku("Sunucu Smtp/Erişim Noktası", Smtp_Sunucu_ErişimNoktası.Text);
            Smtp_Sunucu_SSL.Checked = Ayarlar.Oku_Bit("Sunucu Smtp/SSL", true);
            Imap_Sunucu_Adres.Text = Ayarlar.Oku("Sunucu Imap/Adresi", Imap_Sunucu_Adres.Text);
            Imap_Sunucu_ErişimNoktası.Text = Ayarlar.Oku("Sunucu Imap/Erişim Noktası", Imap_Sunucu_ErişimNoktası.Text);
            Imap_Sunucu_SSL.Checked = Ayarlar.Oku_Bit("Sunucu Imap/SSL", true);
            Gönderici_Ad.Text = Ayarlar.Oku("Gönderici/Adı", Gönderici_Ad.Text);
            Gönderici_Adres.Text = Ayarlar.Oku("Gönderici/Adresi", Gönderici_Adres.Text);
            Gönderici_Şifre.Text = Ayarlar.Oku("Gönderici/Şifresi");
            BeyazListe.Text = Ayarlar.Oku("Beyaz Liste");
            Mesaj_Konu.Text = Ayarlar.Oku("Mesaj/Konu", Mesaj_Konu.Text);
            Mesaj_İçerik.Text = Ayarlar.Oku("Mesaj/İçerik", Mesaj_İçerik.Text);
            FirmaİçiKişiler.Text = Ayarlar.Oku("Firma İçi Kişiler");
            
            Kaydet.Enabled = false;
        }
        
        private void Ayar_Değişti(object sender, EventArgs e)
        {
            Kaydet.Enabled = true;
        }
        private void Kaydet_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(Smtp_Sunucu_ErişimNoktası.Text, out _))
            {
                MessageBox.Show("Sunucu Smtp Erişim Noktası kutucuğu sayıya çevirilemedi", Text);
                return;
            }

            if (!int.TryParse(Imap_Sunucu_ErişimNoktası.Text, out _))
            {
                MessageBox.Show("Sunucu Imap Erişim Noktası kutucuğu sayıya çevirilemedi", Text);
                return;
            }

            Gönderici_Adres.Text = Gönderici_Adres.Text.Trim().ToLower();
            BeyazListe.Text = BeyazListe.Text.Trim().ToLower();
            FirmaİçiKişiler.Text = FirmaİçiKişiler.Text.Trim().ToLower();

            Ayarlar.Yaz("Sunucu Smtp/Adresi", Smtp_Sunucu_Adres.Text);
            Ayarlar.Yaz("Sunucu Smtp/Erişim Noktası", Smtp_Sunucu_ErişimNoktası.Text.NoktalıSayıya());
            Ayarlar.Yaz("Sunucu Smtp/SSL", Smtp_Sunucu_SSL.Checked);
            Ayarlar.Yaz("Sunucu Imap/Adresi", Imap_Sunucu_Adres.Text);
            Ayarlar.Yaz("Sunucu Imap/Erişim Noktası", Imap_Sunucu_ErişimNoktası.Text.NoktalıSayıya());
            Ayarlar.Yaz("Sunucu Imap/SSL", Imap_Sunucu_SSL.Checked);
            Ayarlar.Yaz("Gönderici/Adı", Gönderici_Ad.Text);
            Ayarlar.Yaz("Gönderici/Adresi", Gönderici_Adres.Text);
            Ayarlar.Yaz("Gönderici/Şifresi", Gönderici_Şifre.Text);
            Ayarlar.Yaz("Beyaz Liste", BeyazListe.Text);
            Ayarlar.Yaz("Mesaj/Konu", Mesaj_Konu.Text);
            Ayarlar.Yaz("Mesaj/İçerik", Mesaj_İçerik.Text);
            Ayarlar.Yaz("Firma İçi Kişiler", FirmaİçiKişiler.Text);
            Banka.Değişiklikleri_Kaydet(Kaydet);

            Eposta.Durdur();

            Kaydet.Enabled = false;
        }
        
        private void GöndermeyiDene_Click(object sender, EventArgs e)
        {
            if (Kaydet.Enabled)
            {
                Kaydet_Click(null, null);
                if (Kaydet.Enabled) return; //birşeyler ters gitti
            }

            Eposta.Durdur();
            ArgeMup.HazirKod.Depo_ d = Banka.ÖrnekMüşteriTablosuOluştur();
            string dosyayolu = Ortak.Klasör_Gecici + Path.GetRandomFileName() + ".pdf";
            Yazdırma y = new Yazdırma();
            y.Yazdır_Depo(d, dosyayolu);
            y.Dispose();

            Eposta.Gönder(Gönderici_Adres.Text, null, null, "Deneme Mesajı", "Deneme Mesajı İçeriği", null, new string[] { dosyayolu }, _GeriBildirimİşlemi_Tamamlandı);
            void _GeriBildirimİşlemi_Tamamlandı(string Sonuç)
            {
                if (Sonuç.BoşMu()) Sonuç = "Eposta gönderme denemesi başarılı";
                MessageBox.Show(Sonuç, Text);
            }
        }
        private void Imap_AlmayıDene_Click(object sender, EventArgs e)
        {
            if (Kaydet.Enabled)
            {
                Kaydet_Click(null, null);
                if (Kaydet.Enabled) return; //birşeyler ters gitti
            }

            Eposta.Durdur();
            Eposta.YenileİşaretleSil(null, 365, false, null, null, null, _GeriBildirimİşlemi_Tamamlandı);
            void _GeriBildirimİşlemi_Tamamlandı(string Sonuç)
            {
                if (Sonuç.BoşMu())
                {
                    int adet = 0;
                    if (Directory.Exists(Eposta.EpostaAltyapısı_EpostalarınYolu + "\\Epostaları Yenile\\Gelen Kutusu")) adet = Directory.GetDirectories(Eposta.EpostaAltyapısı_EpostalarınYolu + "\\Epostaları Yenile\\Gelen Kutusu", "_*", SearchOption.TopDirectoryOnly).Length;
                    Sonuç = "Gelen kutusunda " + adet + " adet eposta var";
                }
                MessageBox.Show(Sonuç, Text);
            }
        }
    }
}