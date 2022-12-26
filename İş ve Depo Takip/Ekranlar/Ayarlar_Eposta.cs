using ArgeMup.HazirKod;
using ArgeMup.HazirKod.Ekİşlemler;
using ArgeMup.HazirKod.EşZamanlıÇokluErişim;
using System;
using System.Drawing;
using System.IO;
using System.Net.Mail;
using System.Threading;
using System.Windows.Forms;
using static ArgeMup.HazirKod.Depo_Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace İş_ve_Depo_Takip.Ekranlar
{
    public partial class Ayarlar_Eposta : Form
    {
        IDepo_Eleman Ayarlar = null;

        public Ayarlar_Eposta()
        {
            InitializeComponent();
        }
        public void Ayarlar_Eposta_Load(object sender, System.EventArgs e)
        {
            Ortak.GeçiciDepolama_PencereKonumları_Oku(this);

            Ayarlar = Banka.Tablo_Dal(null, Banka.TabloTürü.Ayarlar, "Eposta", true);

            Sunucu_Adres.Text = Ayarlar.Oku("Sunucu/Adresi", Sunucu_Adres.Text);
            Sunucu_ErişimNoktası.Text = Ayarlar.Oku("Sunucu/Erişim Noktası", Sunucu_ErişimNoktası.Text);
            Sunucu_SSL.Checked = Ayarlar.Oku_Bit("Sunucu/SSL", true);
            Gönderici_Ad.Text = Ayarlar.Oku("Gönderici/Adı", Gönderici_Ad.Text);
            Gönderici_Adres.Text = Ayarlar.Oku("Gönderici/Adresi", Gönderici_Adres.Text);
            Gönderici_Şifre.Text = Ayarlar.Oku("Gönderici/Şifresi");
            Mesaj_Konu.Text = Ayarlar.Oku("Mesaj/Konu", Mesaj_Konu.Text);
            Mesaj_İçerik.Text = Ayarlar.Oku("Mesaj/İçerik", Mesaj_İçerik.Text);
            Kaydet.Enabled = false;

            KeyDown += Ayarlar_Eposta_Tuş;
            KeyUp += Ayarlar_Eposta_Tuş;
            MouseWheel += Ayarlar_Eposta_MouseWheel;
            KeyPreview = true;
        }
        bool ctrl_tuşuna_basıldı = false;
        private void Ayarlar_Eposta_Tuş(object sender, KeyEventArgs e)
        {
            ctrl_tuşuna_basıldı = e.Control;
        }
        private void Ayarlar_Eposta_MouseWheel(object sender, MouseEventArgs e)
        {
            if (ctrl_tuşuna_basıldı)
            {
                WindowState = FormWindowState.Normal;
                if (e.Delta > 0) Font = new Font(Font.FontFamily, Font.Size + 0.2f);
                else Font = new Font(Font.FontFamily, Font.Size - 0.2f);
            }
        }
        private void Ayarlar_Eposta_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Kaydet.Enabled)
            {
                DialogResult Dr = MessageBox.Show("Değişiklikleri kaydetmeden çıkmak istediğinize emin misiniz?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                e.Cancel = Dr == DialogResult.No;
            }
        }
        private void Ayarlar_Eposta_FormClosed(object sender, FormClosedEventArgs e)
        {
            Ortak.GeçiciDepolama_PencereKonumları_Yaz(this);
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
            Banka.Değişiklikleri_Kaydet();

            Ortak.Kullanıcı_Eposta_hesabı_mevcut = !string.IsNullOrEmpty(Gönderici_Şifre.Text);
            
            Kaydet.Enabled = false;
        }
        private void GöndermeyiDene_Click(object sender, EventArgs e)
        {
            ArgeMup.HazirKod.Depo_ d = Banka.ÖrnekMüşteriTablosuOluştur();
            string dosyayolu = Ortak.Klasör_Gecici + Path.GetRandomFileName() + ".pdf";

            Yazdırma y = new Yazdırma();
            y.Yazdırma_Load(null, null);
            y.Yazdır_Depo(d, dosyayolu);

            ArgeMup.HazirKod.Depo_ müşteri = new ArgeMup.HazirKod.Depo_();
            müşteri.Yaz("Örnek Müşteri Adı/Eposta/Kime", Gönderici_Adres.Text);

            string snç = EpostaGönder(müşteri.Elemanları[0], new string[] { dosyayolu });
            if (!string.IsNullOrEmpty(snç)) MessageBox.Show(snç, Text);
        }

        public string EpostaGönder(IDepo_Eleman Müşteri, string[] DosyaEkleri, int ZamanAşımı_msn = 15000)
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
                    mail.Body = Mesaj_İçerik.Text.Replace("%Müşteri%", Müşteri.Adı);
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

                Thread.Sleep(100);
            }

            return HataMesajı;
        }
    }
}