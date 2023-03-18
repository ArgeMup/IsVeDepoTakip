using ArgeMup.HazirKod;
using ArgeMup.HazirKod.Ekİşlemler;
using System;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip.Ekranlar
{
    public partial class Ayarlar_Diğer : Form
    {
        IDepo_Eleman Ayarlar_Küçültüldüğünde = null, Ayarlar_Bilgisayar = null, Ayarlar_Takvim = null, Ayarlar_SürümKontrol = null, Ayarlar_DosyaEkleri = null;

        public Ayarlar_Diğer()
        {
            InitializeComponent();

            Ortak.GeçiciDepolama_PencereKonumları_Oku(this);

            Ayarlar_Takvim = Banka.Tablo_Dal(null, Banka.TabloTürü.Takvim, "Erteleme Süresi", true);
            Ayarlar_Bilgisayar = Banka.Ayarlar_BilgisayarVeKullanıcı(null, true);
            Ayarlar_Küçültüldüğünde = Banka.Ayarlar_Genel("Küçültüldüğünde Parola Sor", true);
            Ayarlar_SürümKontrol = Banka.Tablo_Dal(null, Banka.TabloTürü.KorumalıAlan, "Sürüm Sayısı", true);
            Ayarlar_DosyaEkleri = Banka.Tablo_Dal(null, Banka.TabloTürü.DosyaEkleri, "Dosya Silme Boyutu", true);

            Takvim_Erteleme_İşKabulTarihi.Text = Ayarlar_Takvim.Oku(null, "2", 0);
            Takvim_Erteleme_ÖdemeTalepTarihi.Text = Ayarlar_Takvim.Oku(null, "7", 1);
            Takvim_Erteleme_1.Text = Ayarlar_Takvim.Oku(null, "4", 2);
            Takvim_Erteleme_2.Text = Ayarlar_Takvim.Oku(null, "6", 3);
            Takvim_Erteleme_3.Text = Ayarlar_Takvim.Oku(null, "10", 4);
            Takvim_Erteleme_4.Text = Ayarlar_Takvim.Oku(null, "12", 5);
            Takvim_Erteleme_5.Text = Ayarlar_Takvim.Oku(null, "14", 6);
            Takvim_GecikmeleriGünBazındaHesapla.Checked = Ayarlar_Takvim.Oku_Bit("Gecikmeleri gün bazında hesapla", true);

            Klasör_Yedekleme_1.Text = Ayarlar_Bilgisayar.Oku("Klasör/Yedek", null, 0);
            Klasör_Yedekleme_2.Text = Ayarlar_Bilgisayar.Oku("Klasör/Yedek", null, 1);
            Klasör_Yedekleme_3.Text = Ayarlar_Bilgisayar.Oku("Klasör/Yedek", null, 2);
            Klasör_Yedekleme_4.Text = Ayarlar_Bilgisayar.Oku("Klasör/Yedek", null, 3);
            Klasör_Yedekleme_5.Text = Ayarlar_Bilgisayar.Oku("Klasör/Yedek", null, 4);
            Klasör_Pdf.Text = Ayarlar_Bilgisayar.Oku("Klasör/Pdf");

            KüçültüldüğündeParolaSor.Checked = Ayarlar_Küçültüldüğünde.Oku_Bit(null, true, 0);
            KüçültüldüğündeParolaSor_sn.Value = Ayarlar_Küçültüldüğünde.Oku_TamSayı(null, 60, 1);

            KorumalıAlan_SürümSayısı.Value = Ayarlar_SürümKontrol.Oku_TamSayı(null, 15);

            DosyaEkleri_BoyutuMB.Value = Ayarlar_DosyaEkleri.Oku_TamSayı(null, 1000);

            HttpSunucu_Etkin.Checked = Ayarlar_Bilgisayar.Oku_Bit("HttpSunucu/Etkin");
            string bilgisayar_adı = System.Net.Dns.GetHostName();
            System.Net.IPHostEntry ev = System.Net.Dns.GetHostEntry(bilgisayar_adı.DoluMu() ? bilgisayar_adı : "");
            if (ev != null && ev.AddressList != null)
            {
                bilgisayar_adı = "";
                foreach (var ip in ev.AddressList)
                {
                    if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        bilgisayar_adı += Environment.NewLine + ip.ToString() + "/A" + (DateTime.Now.Year - 2000).ToString() + "1";
                    }
                }

                İpUcu_Genel.SetToolTip(HttpSunucu_Açıklama, bilgisayar_adı);
            }

            Kaydet.Enabled = false;
        }

        private void Ayar_Değişti(object sender, EventArgs e)
        {
            Kaydet.Enabled = true;
        }
        private void Kaydet_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Klasör_Yedekleme_1.Text)) Klasör_Yedekleme_1.Text = null;
            else
            {
                Klasör_Yedekleme_1.Text = Klasör_Yedekleme_1.Text.TrimEnd(' ', '\\') + "\\";
                if (!Klasör.Oluştur(Klasör_Yedekleme_1.Text))
                {
                    MessageBox.Show("Yedek klasörü 1 oluşturulamıyor", Text);
                    Klasör_Yedekleme_1.Focus();
                    return;
                }
            }

            if (string.IsNullOrWhiteSpace(Klasör_Yedekleme_2.Text)) Klasör_Yedekleme_2.Text = null;
            else
            {
                Klasör_Yedekleme_2.Text = Klasör_Yedekleme_2.Text.TrimEnd(' ', '\\') + "\\";
                if (!Klasör.Oluştur(Klasör_Yedekleme_2.Text))
                {
                    MessageBox.Show("Yedek klasörü 2 oluşturulamıyor", Text);
                    Klasör_Yedekleme_2.Focus();
                    return;
                }
            }

            if (string.IsNullOrWhiteSpace(Klasör_Yedekleme_3.Text)) Klasör_Yedekleme_3.Text = null;
            else
            {
                Klasör_Yedekleme_3.Text = Klasör_Yedekleme_3.Text.TrimEnd(' ', '\\') + "\\";
                if (!Klasör.Oluştur(Klasör_Yedekleme_3.Text))
                {
                    MessageBox.Show("Yedek klasörü 3 oluşturulamıyor", Text);
                    Klasör_Yedekleme_3.Focus();
                    return;
                }
            }

            if (string.IsNullOrWhiteSpace(Klasör_Yedekleme_4.Text)) Klasör_Yedekleme_4.Text = null;
            else
            {
                Klasör_Yedekleme_4.Text = Klasör_Yedekleme_4.Text.TrimEnd(' ', '\\') + "\\";
                if (!Klasör.Oluştur(Klasör_Yedekleme_4.Text))
                {
                    MessageBox.Show("Yedek klasörü 4 oluşturulamıyor", Text);
                    Klasör_Yedekleme_4.Focus();
                    return;
                }
            }

            if (string.IsNullOrWhiteSpace(Klasör_Yedekleme_5.Text)) Klasör_Yedekleme_5.Text = null;
            else
            {
                Klasör_Yedekleme_5.Text = Klasör_Yedekleme_5.Text.TrimEnd(' ', '\\') + "\\";
                if (!Klasör.Oluştur(Klasör_Yedekleme_5.Text))
                {
                    MessageBox.Show("Yedek klasörü 5 oluşturulamıyor", Text);
                    Klasör_Yedekleme_5.Focus();
                    return;
                }
            }

            if (string.IsNullOrWhiteSpace(Klasör_Pdf.Text)) Klasör_Pdf.Text = null;
            else
            {
                Klasör_Pdf.Text = Klasör_Pdf.Text.TrimEnd(' ', '\\') + "\\";
                if (!Klasör.Oluştur(Klasör_Pdf.Text))
                {
                    MessageBox.Show("Pdf klasörü oluşturulamıyor", Text);
                    return;
                }
            }

            string gecici = Takvim_Erteleme_İşKabulTarihi.Text;
            if (!Ortak.YazıyıSayıyaDönüştür(ref gecici, "İş kabul tarihi erteleme kutucuğu", null, 0.1))
            {
                Takvim_Erteleme_İşKabulTarihi.Focus();
                return;
            }
            Takvim_Erteleme_İşKabulTarihi.Text = gecici;

            gecici = Takvim_Erteleme_ÖdemeTalepTarihi.Text;
            if (!Ortak.YazıyıSayıyaDönüştür(ref gecici, "Ödeme talep tarihi erteleme kutucuğu", null, 0.1))
            {
                Takvim_Erteleme_ÖdemeTalepTarihi.Focus();
                return;
            }
            Takvim_Erteleme_ÖdemeTalepTarihi.Text = gecici;

            gecici = Takvim_Erteleme_1.Text;
            if (!Ortak.YazıyıSayıyaDönüştür(ref gecici, "Erteleme seçeneği 1 kutucuğu", null, 0.1))
            {
                Takvim_Erteleme_1.Focus();
                return;
            }
            Takvim_Erteleme_1.Text = gecici;

            gecici = Takvim_Erteleme_2.Text;
            if (!Ortak.YazıyıSayıyaDönüştür(ref gecici, "Erteleme seçeneği 2 kutucuğu", null, 0.1))
            {
                Takvim_Erteleme_2.Focus();
                return;
            }
            Takvim_Erteleme_2.Text = gecici;

            gecici = Takvim_Erteleme_3.Text;
            if (!Ortak.YazıyıSayıyaDönüştür(ref gecici, "Erteleme seçeneği 3 kutucuğu", null, 0.1))
            {
                Takvim_Erteleme_3.Focus();
                return;
            }
            Takvim_Erteleme_3.Text = gecici;

            gecici = Takvim_Erteleme_4.Text;
            if (!Ortak.YazıyıSayıyaDönüştür(ref gecici, "Erteleme seçeneği 4 kutucuğu", null, 0.1))
            {
                Takvim_Erteleme_4.Focus();
                return;
            }
            Takvim_Erteleme_4.Text = gecici;

            gecici = Takvim_Erteleme_5.Text;
            if (!Ortak.YazıyıSayıyaDönüştür(ref gecici, "Erteleme seçeneği 5 kutucuğu", null, 0.1))
            {
                Takvim_Erteleme_5.Focus();
                return;
            }
            Takvim_Erteleme_5.Text = gecici;

            Ayarlar_Takvim[0] = Takvim_Erteleme_İşKabulTarihi.Text;
            Ayarlar_Takvim[1] = Takvim_Erteleme_ÖdemeTalepTarihi.Text;
            Ayarlar_Takvim[2] = Takvim_Erteleme_1.Text;
            Ayarlar_Takvim[3] = Takvim_Erteleme_2.Text;
            Ayarlar_Takvim[4] = Takvim_Erteleme_3.Text;
            Ayarlar_Takvim[5] = Takvim_Erteleme_4.Text;
            Ayarlar_Takvim[6] = Takvim_Erteleme_5.Text;
            Ayarlar_Takvim.Yaz("Gecikmeleri gün bazında hesapla", Takvim_GecikmeleriGünBazındaHesapla.Checked);

            Ayarlar_Bilgisayar.Yaz("Klasör/Yedek", Klasör_Yedekleme_1.Text, 0);
            Ayarlar_Bilgisayar.Yaz("Klasör/Yedek", Klasör_Yedekleme_2.Text, 1);
            Ayarlar_Bilgisayar.Yaz("Klasör/Yedek", Klasör_Yedekleme_3.Text, 2);
            Ayarlar_Bilgisayar.Yaz("Klasör/Yedek", Klasör_Yedekleme_4.Text, 3);
            Ayarlar_Bilgisayar.Yaz("Klasör/Yedek", Klasör_Yedekleme_5.Text, 4);
            Ayarlar_Bilgisayar.Yaz("Klasör/Pdf", Klasör_Pdf.Text);

            Ayarlar_Küçültüldüğünde.Yaz(null, KüçültüldüğündeParolaSor.Checked, 0);
            Ayarlar_Küçültüldüğünde.Yaz(null, (int)KüçültüldüğündeParolaSor_sn.Value, 1);

            Ayarlar_SürümKontrol.Yaz(null, (int)KorumalıAlan_SürümSayısı.Value);

            Ayarlar_DosyaEkleri.Yaz(null, (int)DosyaEkleri_BoyutuMB.Value);

            Ayarlar_Bilgisayar.Yaz("HttpSunucu/Etkin", HttpSunucu_Etkin.Checked);

            Banka.Değişiklikleri_Kaydet(Kaydet);

            Ortak.Kullanıcı_Klasör_Yedek = Ayarlar_Bilgisayar.Bul("Klasör/Yedek", true).İçeriği;
            Ortak.Kullanıcı_Klasör_Pdf = Klasör_Pdf.Text;
            Ortak.Kullanıcı_KüçültüldüğündeParolaSor = KüçültüldüğündeParolaSor.Checked;
            Ortak.Kullanıcı_KüçültüldüğündeParolaSor_sn = (int)KüçültüldüğündeParolaSor_sn.Value;
            Kaydet.Enabled = false;
        }
    }
}
