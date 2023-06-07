using ArgeMup.HazirKod;
using ArgeMup.HazirKod.DonanımHaberleşmesi;
using ArgeMup.HazirKod.Ekİşlemler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip.Ekranlar
{
    public partial class Barkod_Okuma_Seçenekleri : Form
    {
        readonly string Müşteri_, SeriNo_, Hasta_, EkTanım_;
        readonly Banka.TabloTürü Türü_;

        public Barkod_Okuma_Seçenekleri(Banka.Talep_Bul_Detaylar_ Detaylar)
        {
            InitializeComponent();

            Banka.Talep_Ayıkla_SeriNoDalı(Detaylar.SeriNoDalı, out SeriNo_, out Hasta_, out _, out _, out string TeslimEdilmeTarihi);
            Banka.Talep_Hesaplat_FirmaİçindekiSüreler(Detaylar.SeriNoDalı, out TimeSpan Firmaİçinde, out TimeSpan Toplam);

            Text += SeriNo_ + " " + Detaylar.Tür;

            Müşteri.Text = Detaylar.Müşteri;    
            Hasta.Text = Hasta_;              
            Süre.Text = "Toplam " + Toplam.TotalDays.ToString("0.0") + " gün, firma içinde " + Firmaİçinde.TotalDays.ToString("0.0") + " gün";

            Müşteri_ = Detaylar.Müşteri;
            EkTanım_ = Detaylar.EkTanım;
            Türü_ = Detaylar.Tür;

            if (Türü_ > Banka.TabloTürü.TeslimEdildi)
            {
                //sadece olunabilir
            }
            else
            {
                Banka.Talep_Ayıkla_İşTürüDalı(Detaylar.SeriNoDalı.Elemanları.Last(), out _, out _, out string ÇıkışTarihi, out _, out _);
                MüşteriyeGönder.Enabled = ÇıkışTarihi.BoşMu();
                İsaretle_TeslimEdildi.Enabled = TeslimEdilmeTarihi.BoşMu();
            }
        }

        private void Düzenle_Click(object sender, EventArgs e)
        {
            Close();
            Ekranlar.ÖnYüzler.Ekle(new Yeni_İş_Girişi(SeriNo_, Müşteri_, Türü_, EkTanım_));
        }
        private void MüşteriyeGönder_Click(object sender, EventArgs e)
        {
            Banka.Talep_İşaretle_DevamEden_MüşteriyeGönderildi(Müşteri_, new List<string>() { SeriNo_ });
            Banka.Değişiklikleri_Kaydet(MüşteriyeGönder);
            Close();
        }
        private void İsaretle_TeslimEdildi_Click(object sender, EventArgs e)
        {
            string snç = Banka.Talep_İşaretle_DevamEden_TeslimEdilen(Müşteri_, new List<string>() { SeriNo_ }, true);
            
            if (string.IsNullOrEmpty(snç))
            {
                //başarılı
                Banka.Değişiklikleri_Kaydet(İsaretle_TeslimEdildi);
                Close();
            }
            else
            {
                Banka.Değişiklikler_TamponuSıfırla();

                DialogResult Dr = MessageBox.Show(snç + Environment.NewLine + Environment.NewLine +
                    "Ücretler sayfasını açmak ister misiniz?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                
                if (Dr == DialogResult.Yes) Ekranlar.ÖnYüzler.Ekle(new Ücretler());
            }
        }
    }

    public static class BarkodSorgulama
    {
        static IDonanımHaberlleşmesi SeriPort = null;
        public static string SonMesaj = null;
        public static void Başlat()
        {
            IDepo_Eleman Ayarlar_Bilgisayar = Banka.Ayarlar_BilgisayarVeKullanıcı("Barkod Okuyucu");
            if (Ayarlar_Bilgisayar == null || Ayarlar_Bilgisayar[0].BoşMu() || Ayarlar_Bilgisayar[0] == "Kapalı") return;

            SeriPort = new SeriPort_(Ayarlar_Bilgisayar[0], 9600, GeriBildirim_Islemi_SeriPort, SatırSatırGönderVeAl:false, TekrarDeneme_ZamanAşımı_msn:-1);
        }
        public static void Durdur()
        {
            SeriPort?.Durdur();
            SeriPort = null;
        }
        static void GeriBildirim_Islemi_SeriPort(string Kaynak, GeriBildirim_Türü_ Tür, object İçerik, object Hatırlatıcı)
        {
            if (Tür == GeriBildirim_Türü_.BilgiGeldi)
            {
                SonMesaj = ((byte[])İçerik).Yazıya().TrimEnd('\r', '\n');
                SeçenekleriGöster(SonMesaj);
            }
            else SonMesaj = Kaynak + " " + Tür.ToString();
        }

        public static void SeçenekleriGöster(string Barkod)
        {
            if (Ortak.ParolaGirilmesiGerekiyor) return;

            string sn = Etiketleme.SeriNoyuBulmayaÇalış(Barkod).ToUpper();
            Banka.Talep_Bul_Detaylar_ detaylar = Banka.Talep_Bul(sn);
            if (detaylar == null) return;

            if (!Ortak.AnaEkran.InvokeRequired) Ekranlar.ÖnYüzler.Ekle(new Barkod_Okuma_Seçenekleri(detaylar));
            else
            {
                Ortak.AnaEkran.Invoke(new Action(() =>
                {
                    Ekranlar.ÖnYüzler.Ekle(new Barkod_Okuma_Seçenekleri(detaylar));
                }));
            }
        }
    }
}
