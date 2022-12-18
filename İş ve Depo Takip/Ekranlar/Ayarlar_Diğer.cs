using ArgeMup.HazirKod;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip.Ekranlar
{
    public partial class Ayarlar_Diğer : Form
    {
        Depo_ Ayarlar = null;

        public Ayarlar_Diğer()
        {
            InitializeComponent();
        }
        private void Ayarlar_Diğer_Load(object sender, System.EventArgs e)
        {
            Ortak.GeçiciDepolama_PencereKonumları_Oku(this);

            Ayarlar = Banka.Tablo(null, Banka.TabloTürü.Ayarlar, true);

            Klasör_Yedekleme.Text = Ayarlar.Oku("Klasör/Yedek");
            Klasör_Pdf.Text = Ayarlar.Oku("Klasör/Pdf");
            AçılışEkranıİçinParaloİste.Checked = Ayarlar.Oku_Bit("AçılışEkranıİçinParaloİste", true);
            Kaydet.Enabled = false;

            KeyDown += Yeni_Talep_Girişi_Tuş;
            KeyUp += Yeni_Talep_Girişi_Tuş;
            MouseWheel += Yeni_Talep_Girişi_MouseWheel;
            KeyPreview = true;
        }
        bool ctrl_tuşuna_basıldı = false;
        private void Yeni_Talep_Girişi_Tuş(object sender, KeyEventArgs e)
        {
            ctrl_tuşuna_basıldı = e.Control;
        }
        private void Yeni_Talep_Girişi_MouseWheel(object sender, MouseEventArgs e)
        {
            if (ctrl_tuşuna_basıldı)
            {
                WindowState = FormWindowState.Normal;
                if (e.Delta > 0) Font = new Font(Font.FontFamily, Font.Size + 0.2f);
                else Font = new Font(Font.FontFamily, Font.Size - 0.2f);
            }
        }
        private void Ayarlar_Diğer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Kaydet.Enabled)
            {
                DialogResult Dr = MessageBox.Show("Değişiklikleri kaydetmeden çıkmak istediğinize emin misiniz?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                e.Cancel = Dr == DialogResult.No;
            }
        }
        private void Ayarlar_Diğer_FormClosed(object sender, FormClosedEventArgs e)
        {
            Ortak.GeçiciDepolama_PencereKonumları_Yaz(this);
        }
        
        private void Ayar_Değişti(object sender, EventArgs e)
        {
            Kaydet.Enabled = true;
        }
        private void Kaydet_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Klasör_Yedekleme.Text)) Klasör_Yedekleme.Text = null;
            else
            {
                Klasör_Yedekleme.Text = Klasör_Yedekleme.Text.TrimEnd(' ', '\\') + "\\";
                if (!Klasör.Oluştur(Klasör_Yedekleme.Text))
                {
                    MessageBox.Show("Yedek klasörü oluşturulamıyor", Text);
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

            Ayarlar.Yaz("Klasör/Yedek", Klasör_Yedekleme.Text);
            Ayarlar.Yaz("Klasör/Pdf", Klasör_Pdf.Text);
            Ayarlar.Yaz("AçılışEkranıİçinParaloİste", AçılışEkranıİçinParaloİste.Checked);
            Banka.Değişiklikleri_Kaydet();

            Ortak.Klasör_KullanıcıYedeği = Klasör_Yedekleme.Text;
            Ortak.Klasör_Pdf = Klasör_Pdf.Text;
            Ortak.AçılışEkranıİçinParaloİste = AçılışEkranıİçinParaloİste.Checked;

            Kaydet.Enabled = false;
        }
    }
}
