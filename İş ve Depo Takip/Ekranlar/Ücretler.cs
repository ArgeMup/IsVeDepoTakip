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
        private void Ücretler_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Kaydet.Enabled)
            {
                DialogResult Dr = MessageBox.Show("Değişiklikleri kaydetmeden çıkmak istediğinize emin misiniz?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                e.Cancel = Dr == DialogResult.No;
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
            Zam_Yap.Enabled = false;
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
            Zam_Yap.Enabled = true;
            Kaydet.Enabled = false;
        }

        private void Zam_Yap_Click(object sender, EventArgs e)
        {
            string yüzde_y = Zam_Miktarı.Text;
            if (!Ortak.YazıyıSayıyaDönüştür(ref yüzde_y, "Artış miktarı kutucuğu"))
            {
                Zam_Miktarı.Focus();
                return;
            }
            Zam_Miktarı.Text = yüzde_y;
            double yüzde_s = yüzde_y.NoktalıSayıya();

            if (!string.IsNullOrWhiteSpace(AramaÇubuğu_İşTürü.Text))
            {
                string soru = "Tablo içeriği filtrelendiğinden sadece GÖRÜNEN üyeler ücretlendiririlecek." + Environment.NewLine +
                    "Devam etmek için Evet tuşuna basınız";

                DialogResult Dr = MessageBox.Show(soru, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (Dr == DialogResult.No) return;
            }

            for (int i = 0; i < Tablo.RowCount; i++)
            {
                if (Tablo[1, i].Visible && Tablo[1, i].Value != null)
                {
                    try
                    {
                        double s = ((string)Tablo[1, i].Value).NoktalıSayıya();
                        s = (s / 100.0 * yüzde_s) + s;
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
                if (!string.IsNullOrEmpty((string)Tablo[1, i].Value))
                {
                    string miktar = (string)Tablo[1, i].Value;
                    if (!Ortak.YazıyıSayıyaDönüştür(ref miktar,
                        (string)Tablo[0, i].Value + " (ücret sutununun " + (i + 1).ToString() + ". satırı)",
                        "Ücretlendirmek istemiyorsanız boş olarak bırakınız." + Environment.NewLine +
                        "Eğer " + (i + 1) + ". satır görünmüyor ise arama filtresini kaldırınız",
                        0)) return;

                    Tablo[1, i].Value = miktar;
                }
            }

            DialogResult Dr = MessageBox.Show("Değişiklikleri kaydetmek istediğinize emin misiniz?" +
                (string.IsNullOrWhiteSpace(AramaÇubuğu_İşTürü.Text) ? null : Environment.NewLine + Environment.NewLine + "Tablo içeriği filtrelendiğinden görünmeseler bile TÜM girdiler kaydedilecektir."),
                Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
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
