using ArgeMup.HazirKod;
using ArgeMup.HazirKod.Ekİşlemler;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip.Ekranlar
{
    public partial class Ücretler : Form
    {
        public Ücretler()
        {
            InitializeComponent();

            Ortak.GeçiciDepolama_PencereKonumları_Oku(this);
        }
        private void Tüm_Talepler_Load(object sender, EventArgs e)
        {
            AramaÇubuğu_Müşteri_Liste = Banka.Müşteri_Listele();
            AramaÇubuğu_Müşteri_Liste.Add("Tüm müşteriler için ortak");
            Müşterıler.Items.AddRange(AramaÇubuğu_Müşteri_Liste.ToArray());

            Tablo.Rows.Clear();
            AramaÇubuğu_İşTürü_Liste = Banka.İşTürü_Listele();
            foreach (string it in AramaÇubuğu_İşTürü_Liste)
            {
                int y = Tablo.RowCount;
                Tablo.RowCount++;

                Tablo[0, y].Value = it;
            }

            Zam_Miktarı.Text = "0";

            Müşterıler.Text = "Tüm müşteriler için ortak";
            Kaydet.Enabled = false;
            splitContainer1.Panel1.Enabled = true;
            Müşterıler.Focus();

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
        private void Tüm_Talepler_FormClosed(object sender, FormClosedEventArgs e)
        {
            Ortak.GeçiciDepolama_PencereKonumları_Yaz(this);
        }

        List<string> AramaÇubuğu_Müşteri_Liste = null;
        private void AramaÇubuğu_Müşteri_TextChanged(object sender, EventArgs e)
        {
            splitContainer1.Panel2.Enabled = false;
            Müşterıler.Items.Clear();

            if (string.IsNullOrEmpty(AramaÇubuğu_Müşteri.Text))
            {
                Müşterıler.Items.AddRange(AramaÇubuğu_Müşteri_Liste.ToArray());
            }
            else
            {
                AramaÇubuğu_Müşteri.Text = AramaÇubuğu_Müşteri.Text.ToLower();
                Müşterıler.Items.AddRange(AramaÇubuğu_Müşteri_Liste.FindAll(x => x.ToLower().Contains(AramaÇubuğu_Müşteri.Text)).ToArray());
            }
        }

        List<string> AramaÇubuğu_İşTürü_Liste = null;
        private void AramaÇubuğu_İşTürü_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(AramaÇubuğu_İşTürü.Text))
            {
                for (int i = 0; i < Tablo.RowCount; i++)
                {
                    Tablo.Rows[i].Visible = true;
                }
            }
            else
            {
                AramaÇubuğu_İşTürü.Text = AramaÇubuğu_İşTürü.Text.ToLower();
                for (int i = 0; i < Tablo.RowCount; i++)
                {
                    Tablo.Rows[i].Visible = ((string)Tablo[0, i].Value).ToLower().Contains(AramaÇubuğu_İşTürü.Text);
                }
            }
        }

        private void Müşterıler_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Müşterıler.Text == "Tüm müşteriler için ortak")
            {
                Banka.Ücretler_TablodaGöster(Tablo, null);
            }
            else Banka.Ücretler_TablodaGöster(Tablo, Müşterıler.Text);

            splitContainer1.Panel1.Enabled = true;
            splitContainer1.Panel2.Enabled = true;
            Kaydet.Enabled = false;
        }

        private void Zam_Yap_Click(object sender, EventArgs e)
        {
            double yüzde;
            try
            {
                yüzde = Zam_Miktarı.Text.NoktalıSayıya();
            }
            catch (Exception)
            {
                MessageBox.Show("Artış miktarı kutucuğu içerisindeki yazı ( " + Zam_Miktarı.Text + " ) sayıya dönüştürülemedi");
                Zam_Miktarı.Focus();
                return;
            }

            for (int i = 0; i < Tablo.RowCount; i++)
            {
                if (Tablo[1, i].Visible && Tablo[1, i].Value != null)
                {
                    try
                    {
                        double s = ((string)Tablo[1, i].Value).NoktalıSayıya();
                        s = (s / 100.0 * yüzde) + s;
                        Tablo[1, i].Value = s.Yazıya();
                    }
                    catch (Exception) { }
                }
            }
        }

        private void Tablo_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            splitContainer1.Panel1.Enabled = false;
            Kaydet.Enabled = true;
        }

        private void Kaydet_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Tablo.RowCount; i++)
            {
                try
                {
                    string s = (string)Tablo[1, i].Value;
                    if (!string.IsNullOrEmpty(s)) s.NoktalıSayıya();
                }
                catch (Exception)
                {
                    MessageBox.Show((i + 1) + ". satırdaki " + Tablo[0, i].Value + " ücreti sayıya dönüştürülemedi, düzenleyip tekrar deneyiniz" + Environment.NewLine + "Ücretlendirmek istemiyorsanız boş olarak bırakınız", Text);
                    Tablo.Focus();
                    return;
                }
            }

            DialogResult Dr = MessageBox.Show("Değişiklikleri kaydetmek istediğinize emin misiniz?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (Dr == DialogResult.No) return;

            if (Müşterıler.Text == "Tüm müşteriler için ortak")
            {
                Banka.Ücretler_TablodakileriKaydet(Tablo, null);
            }
            else Banka.Ücretler_TablodakileriKaydet(Tablo, Müşterıler.Text);
            Banka.Değişiklikleri_Kaydet();

            Kaydet.Enabled = false;
            splitContainer1.Panel1.Enabled = true;
        }
    }
}
