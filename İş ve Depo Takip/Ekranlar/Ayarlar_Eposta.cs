using ArgeMup.HazirKod;
using ArgeMup.HazirKod.Ekİşlemler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip.Ekranlar
{
    public partial class Ayarlar_Eposta : Form
    {
        IDepo_Eleman Ayarlar = null;

        public Ayarlar_Eposta()
        {
            InitializeComponent();

            Ortak.GeçiciDepolama_PencereKonumları_Oku(this);
        
            Ayarlar = Banka.Ayarlar_Genel("Eposta", true);

            Sunucu_Adres.Text = Ayarlar.Oku("Sunucu/Adresi", Sunucu_Adres.Text);
            Sunucu_ErişimNoktası.Text = Ayarlar.Oku("Sunucu/Erişim Noktası", Sunucu_ErişimNoktası.Text);
            Sunucu_SSL.Checked = Ayarlar.Oku_Bit("Sunucu/SSL", true);
            Gönderici_Ad.Text = Ayarlar.Oku("Gönderici/Adı", Gönderici_Ad.Text);
            Gönderici_Adres.Text = Ayarlar.Oku("Gönderici/Adresi", Gönderici_Adres.Text);
            Gönderici_Şifre.Text = Ayarlar.Oku("Gönderici/Şifresi");
            Mesaj_Konu.Text = Ayarlar.Oku("Mesaj/Konu", Mesaj_Konu.Text);
            Mesaj_İçerik.Text = Ayarlar.Oku("Mesaj/İçerik", Mesaj_İçerik.Text);
            Kaydet.Enabled = false;
        }
        
        private void Ayar_Değişti(object sender, EventArgs e)
        {
            Kaydet.Enabled = true;
        }
        private void Kaydet_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(Sunucu_ErişimNoktası.Text, out _))
            {
                MessageBox.Show("Sunucu Erişim Noktası kutucuğu sayıya çevirilemedi", Text);
                return;
            }

            Ayarlar.Yaz("Sunucu/Adresi", Sunucu_Adres.Text);
            Ayarlar.Yaz("Sunucu/Erişim Noktası", Sunucu_ErişimNoktası.Text.NoktalıSayıya());
            Ayarlar.Yaz("Sunucu/SSL", Sunucu_SSL.Checked);
            Ayarlar.Yaz("Gönderici/Adı", Gönderici_Ad.Text);
            Ayarlar.Yaz("Gönderici/Adresi", Gönderici_Adres.Text);
            Ayarlar.Yaz("Gönderici/Şifresi", Gönderici_Şifre.Text);
            Ayarlar.Yaz("Mesaj/Konu", Mesaj_Konu.Text);
            Ayarlar.Yaz("Mesaj/İçerik", Mesaj_İçerik.Text);
            Banka.Değişiklikleri_Kaydet(Kaydet);

            Ortak.Kullanıcı_Eposta_hesabı_mevcut = !string.IsNullOrEmpty(Gönderici_Şifre.Text);
            
            Kaydet.Enabled = false;
        }
        private void GöndermeyiDene_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(Sunucu_ErişimNoktası.Text, out int _))
            {
                MessageBox.Show("Sunucu erişim noktası kutucuğu içeriği sayıya dönüştürülemedi", Text);
                Sunucu_ErişimNoktası.Focus();
                return;
            }

            GöndermeyiDene.Enabled = false;

            ArgeMup.HazirKod.Depo_ d = Banka.ÖrnekMüşteriTablosuOluştur();
            string dosyayolu = Ortak.Klasör_Gecici + Path.GetRandomFileName() + ".pdf";

            Yazdırma y = new Yazdırma();
            y.Yazdır_Depo(d, dosyayolu);
            y.Dispose();

            d.Yaz("Eposta/Kime", Gönderici_Adres.Text);
            string snç = EpostaGönder(d, new string[] { dosyayolu });
            if (!string.IsNullOrEmpty(snç)) MessageBox.Show(snç, Text);

            GöndermeyiDene.Enabled = true;
        }

        public string EpostaGönder(ArgeMup.HazirKod.Depo_ Müşteri, string[] DosyaEkleri, int ZamanAşımı_msn = 15000)
        {
            string HataMesajı = "";
            int za = Environment.TickCount + ZamanAşımı_msn;
            while (za > Environment.TickCount)
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(Gönderici_Adres.Text, Gönderici_Ad.Text, System.Text.Encoding.UTF8);

                    string[] l = Müşteri.Oku("Eposta/Kime", "").Split(';');
                    foreach (string a in l) if (!string.IsNullOrEmpty(a)) mail.To.Add(a);

                    l = Müşteri.Oku("Eposta/Bilgi", "").Split(';');
                    foreach (string a in l) if (!string.IsNullOrEmpty(a)) mail.CC.Add(a);

                    l = Müşteri.Oku("Eposta/Gizli", "").Split(';');
                    foreach (string a in l) if (!string.IsNullOrEmpty(a)) mail.Bcc.Add(a);

                    mail.Subject = Mesaj_Konu.Text;
                    mail.SubjectEncoding = System.Text.Encoding.UTF8;
                    mail.Body = Mesaj_İçerik.Text.Replace("%Müşteri%", Müşteri["Tür", 1] /*adı*/);
                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                    mail.IsBodyHtml = true;
                    foreach (string ek in DosyaEkleri)
                    {
                        mail.Attachments.Add(new Attachment(ek));
                    }
                    
                    SmtpClient client = new SmtpClient();
                    client.Credentials = new System.Net.NetworkCredential(Gönderici_Adres.Text, Gönderici_Şifre.Text);
                    client.Port = int.Parse(Sunucu_ErişimNoktası.Text);
                    client.Host = Sunucu_Adres.Text;
                    client.EnableSsl = Sunucu_SSL.Checked;
                    client.Send(mail);

                    return null;
                }
                catch (Exception ex) { HataMesajı = ex.Message; }

                System.Threading.Thread.Sleep(100);
            }

            return HataMesajı;
        }
        public void EpostaGönder_İstisna(Exception İstisna, int ZamanAşımı_msn = 60000)
        {
            if (!Ortak.Kullanıcı_Eposta_hesabı_mevcut) return;

            System.Threading.Tasks.Task.Run(() =>
            {
                List<string> DosyaEkleri = new List<string>();
                string HataMesajı = "İstisna " + DateTime.Now.Yazıya();

                try
                {
                    while (İstisna != null)
                    {
                        HataMesajı += Environment.NewLine + İstisna.ToString();
                        İstisna = İstisna.InnerException;
                    }
                }
                catch (Exception) { }

                try
                {
                    //Son 15 günlük dosyası
                    System.Threading.Thread.Sleep(100); //Günlük dosyalarının oluşması için
                    Klasör_ gnlk = new Klasör_(Kendi.Klasörü + "\\Günlük");
                    gnlk.Sırala_EskidenYeniye();
                    gnlk.Klasörler = new List<string>();
                    if (gnlk.Dosyalar.Count > 15) gnlk.Dosyalar.RemoveRange(0, gnlk.Dosyalar.Count - 15);
                    string dsy_gnlk = Ortak.Klasör_Gecici + Path.GetRandomFileName() + "_Günlük.zip";
                    if (SıkıştırılmışDosya.Klasörden(gnlk, dsy_gnlk)) DosyaEkleri.Add(dsy_gnlk);
                }
                catch (Exception) { }

                try
                {
                    //Banka içeriği
                    string dsy_bnk = Ortak.Klasör_Gecici + Path.GetRandomFileName() + "_Banka.zip";
                    if (SıkıştırılmışDosya.Klasörden(Ortak.Klasör_Banka, dsy_bnk)) DosyaEkleri.Add(dsy_bnk);
                }
                catch (Exception) { }

                int za = Environment.TickCount + ZamanAşımı_msn;
                while (za > Environment.TickCount)
                {
                    try
                    {
                        MailMessage mail = new MailMessage();
                        mail.From = new MailAddress(Gönderici_Adres.Text, Gönderici_Ad.Text, System.Text.Encoding.UTF8);

                        mail.To.Add("ArgeMup@yandex.com");
                        mail.Subject = "İstisna Hk.";
                        mail.SubjectEncoding = System.Text.Encoding.UTF8;
                        mail.Body = "Oluşan hataya dair detaylar :" + Environment.NewLine + HataMesajı.Replace("|", Environment.NewLine);
                        mail.BodyEncoding = System.Text.Encoding.UTF8;
                        mail.IsBodyHtml = true;
                        foreach (string ek in DosyaEkleri)
                        {
                            mail.Attachments.Add(new Attachment(ek));
                        }

                        SmtpClient client = new SmtpClient();
                        client.Credentials = new System.Net.NetworkCredential(Gönderici_Adres.Text, Gönderici_Şifre.Text);
                        client.Port = int.Parse(Sunucu_ErişimNoktası.Text);
                        client.Host = Sunucu_Adres.Text;
                        client.EnableSsl = Sunucu_SSL.Checked;
                        client.Send(mail);

                        break;
                    }
                    catch (Exception) { }

                    System.Threading.Thread.Sleep(1000);
                }
            });
        }
    }
}