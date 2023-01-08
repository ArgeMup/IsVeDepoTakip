using ArgeMup.HazirKod;
using System;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip.Ekranlar
{
    public partial class Ayarlar_Diğer : Form
    {
        IDepo_Eleman Ayarlar_Küçültüldüğünde = null, Ayarlar_Bilgisayar = null;

        public Ayarlar_Diğer()
        {
            InitializeComponent();

            Ortak.GeçiciDepolama_PencereKonumları_Oku(this);

            Ayarlar_Bilgisayar = Banka.Ayarlar_BilgisayarVeKullanıcı("Klasör", true);
            Ayarlar_Küçültüldüğünde = Banka.Ayarlar_Genel("Küçültüldüğünde Parola Sor", true);

            Klasör_Yedekleme_1.Text = Ayarlar_Bilgisayar.Oku("Yedek", null, 0);
            Klasör_Yedekleme_2.Text = Ayarlar_Bilgisayar.Oku("Yedek", null, 1);
            Klasör_Yedekleme_3.Text = Ayarlar_Bilgisayar.Oku("Yedek", null, 2);
            Klasör_Yedekleme_4.Text = Ayarlar_Bilgisayar.Oku("Yedek", null, 3);
            Klasör_Yedekleme_5.Text = Ayarlar_Bilgisayar.Oku("Yedek", null, 4);
            Klasör_Pdf.Text = Ayarlar_Bilgisayar.Oku("Pdf");
            KüçültüldüğündeParolaSor.Checked = Ayarlar_Küçültüldüğünde.Oku_Bit(null, true, 0);
            KüçültüldüğündeParolaSor_sn.Value = Ayarlar_Küçültüldüğünde.Oku_TamSayı(null, 60, 1);
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

            Ayarlar_Bilgisayar.Yaz("Yedek", Klasör_Yedekleme_1.Text, 0);
            Ayarlar_Bilgisayar.Yaz("Yedek", Klasör_Yedekleme_2.Text, 1);
            Ayarlar_Bilgisayar.Yaz("Yedek", Klasör_Yedekleme_3.Text, 2);
            Ayarlar_Bilgisayar.Yaz("Yedek", Klasör_Yedekleme_4.Text, 3);
            Ayarlar_Bilgisayar.Yaz("Yedek", Klasör_Yedekleme_5.Text, 4);
            Ayarlar_Bilgisayar.Yaz("Pdf", Klasör_Pdf.Text);
            Ayarlar_Küçültüldüğünde.Yaz(null, KüçültüldüğündeParolaSor.Checked, 0);
            Ayarlar_Küçültüldüğünde.Yaz(null, (int)KüçültüldüğündeParolaSor_sn.Value, 1);
            Banka.Değişiklikleri_Kaydet();

            Ortak.Kullanıcı_Klasör_Yedek = Ayarlar_Bilgisayar.Bul("Yedek").İçeriği;
            Ortak.Kullanıcı_Klasör_Pdf = Klasör_Pdf.Text;
            Ortak.Kullanıcı_KüçültüldüğündeParolaSor = KüçültüldüğündeParolaSor.Checked;
            Ortak.Kullanıcı_KüçültüldüğündeParolaSor_sn = (int)KüçültüldüğündeParolaSor_sn.Value;
            Kaydet.Enabled = false;
        }
    }
}
