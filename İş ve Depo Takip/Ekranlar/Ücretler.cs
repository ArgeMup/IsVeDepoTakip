using ArgeMup.HazirKod.Dönüştürme;
using ArgeMup.HazirKod.Ekİşlemler;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip.Ekranlar
{
    public partial class Ücretler : Form
    {
        bool MutlakaGüncelle = false;
        public Ücretler()
        {
            InitializeComponent();

            AramaÇubuğu_Müşteri_Liste = Banka.Müşteri_Listele();
            AramaÇubuğu_Müşteri_Liste.Insert(0, "Tüm müşteriler için ortak");
            Ortak.GrupArayıcı(Müşterıler, AramaÇubuğu_Müşteri_Liste);

            Tablo.Rows.Clear();
            foreach (string it in Banka.İşTürü_Listele())
            {
                int y = Tablo.RowCount;
                Tablo.RowCount++;

                Tablo[Tablo_İş_Türleri.Index, y].Value = it;
            }

            Zam_Miktarı.Text = "0";
            KDV.Text = Banka.Ayarlar_Genel("Bütçe/KDV", true).Oku_Sayı(null, 8).Yazıya();

            #region Değişkenler
            for (int i = 0; i < Değişkenler.Sabitler.Length; i++)
            {
                ToolStripMenuItem tsmi = new ToolStripMenuItem();
                tsmi.Text = Değişkenler.Tümü.Keys.ElementAt(i);
                tsmi.Click += SağTuşMenü_Değişkenler_Click;
                dahiliDeğişkenlerToolStripMenuItem.DropDownItems.Add(tsmi);
            }
            for (int i = Değişkenler.Sabitler.Length; i < Değişkenler.Tümü.Count; i++)
            {
                ToolStripMenuItem tsmi = new ToolStripMenuItem();
                tsmi.Text = Değişkenler.Tümü.Keys.ElementAt(i);
                tsmi.Click += SağTuşMenü_Değişkenler_Click;
                SağTuşMenü_Değişkenler.Items.Add(tsmi);
            }
            #endregion

            Müşterıler.Text = "Tüm müşteriler için ortak";
            Kaydet.Enabled = false;
            splitContainer1.Panel1.Enabled = true;
            Müşterıler.Focus();
        }
        private void Ücretler_Activated(object sender, EventArgs e)
        {
            if (MutlakaGüncelle)
            {
                MutlakaGüncelle = false;

                for (int i = 0; i < Tablo.Rows.Count; i++)
                {
                    Tablo_CellValueChanged(null, new DataGridViewCellEventArgs(Tablo_Ücret.Index, i));
                }
            }
        }
        private void SağTuşMenü_Değişkenler_Click(object sender, EventArgs e)
        {
            if (Tablo.SelectedCells.Count != 1 || Tablo.SelectedCells[0].ColumnIndex == Tablo_İş_Türleri.Index) return;

            Tablo.SelectedCells[0].Value += "%" + (sender as ToolStripMenuItem).Text + "%";
        }

        List<string> AramaÇubuğu_Müşteri_Liste = null;
        private void AramaÇubuğu_Müşteri_TextChanged(object sender, EventArgs e)
        {
            splitContainer1.Panel2.Enabled = false;
            Zam_Yap.Enabled = false;

            Ortak.GrupArayıcı(Müşterıler, AramaÇubuğu_Müşteri_Liste, AramaÇubuğu_Müşteri.Text);
        }

        bool TabloİçeriğiArama_Çalışıyor = false;
        bool TabloİçeriğiArama_KapatmaTalebi = false;
        int TabloİçeriğiArama_Tik = 0;
        int TabloİçeriğiArama_Sayac_Bulundu = 0;
        private void AramaÇubuğu_İşTürü_TextChanged(object sender, EventArgs e)
        {
            TabloİçeriğiArama_Tik = Environment.TickCount + 100;
            if (AramaÇubuğu_İşTürü.Text.Length < 2)
            {
                if (TabloİçeriğiArama_Sayac_Bulundu != 0)
                {
                    AramaÇubuğu_İşTürü.BackColor = System.Drawing.Color.Salmon;

                    for (int satır = 0; satır < Tablo.RowCount; satır++)
                    {
                        Tablo.Rows[satır].Visible = true;
                        if (TabloİçeriğiArama_Tik < Environment.TickCount) { Application.DoEvents(); TabloİçeriğiArama_Tik = Environment.TickCount + 100; }
                    }

                    AramaÇubuğu_İşTürü.BackColor = System.Drawing.Color.White;
                    TabloİçeriğiArama_Sayac_Bulundu = 0;
                }

                return;
            }

            if (TabloİçeriğiArama_Çalışıyor) { TabloİçeriğiArama_KapatmaTalebi = true; return; }

            TabloİçeriğiArama_Çalışıyor = true;
            TabloİçeriğiArama_KapatmaTalebi = false;
            TabloİçeriğiArama_Sayac_Bulundu = 0;
            TabloİçeriğiArama_Tik = Environment.TickCount + 500;
            AramaÇubuğu_İşTürü.BackColor = System.Drawing.Color.Salmon;

            string[] arananlar = AramaÇubuğu_İşTürü.Text.ToLower().Split(' ');
            for (int satır = 0; satır < Tablo.RowCount && !TabloİçeriğiArama_KapatmaTalebi; satır++)
            {
                if (Tablo.Rows[satır].IsNewRow) continue;

                bool bulundu = false;
                int AranabilirSutunSayısı = Müşterıler.Text == "Tüm müşteriler için ortak" ? Tablo.Columns.Count : 2 /* maliyet hariç */;
                for (int sutun = 0; sutun < AranabilirSutunSayısı; sutun++)
                {
                    if (Tablo[sutun, satır].Value == null) continue;

                    string içerik = Tablo[sutun, satır].Value.ToString();
                    if (string.IsNullOrEmpty(içerik)) Tablo[sutun, satır].Style.BackColor = System.Drawing.Color.White;
                    else
                    {
                        içerik = içerik.ToLower();
                        int bulundu_adet = 0;
                        foreach (string arn in arananlar)
                        {
                            if (!içerik.Contains(arn)) break;

                            bulundu_adet++;
                        }

                        if (bulundu_adet == arananlar.Length)
                        {
                            Tablo[sutun, satır].Style.BackColor = System.Drawing.Color.YellowGreen;
                            bulundu = true;
                        }
                        else Tablo[sutun, satır].Style.BackColor = System.Drawing.Color.White;
                    }
                }

                Tablo.Rows[satır].Visible = bulundu;
                if (bulundu) TabloİçeriğiArama_Sayac_Bulundu++;

                if (TabloİçeriğiArama_Tik < Environment.TickCount) { Application.DoEvents(); TabloİçeriğiArama_Tik = Environment.TickCount + 500; }
            }

            if (TabloİçeriğiArama_Sayac_Bulundu == 0) TabloİçeriğiArama_Sayac_Bulundu = -1;

            AramaÇubuğu_İşTürü.BackColor = System.Drawing.Color.White;
            TabloİçeriğiArama_Çalışıyor = false;
            Tablo.ClearSelection();

            if (TabloİçeriğiArama_KapatmaTalebi) AramaÇubuğu_İşTürü_TextChanged(null, null);
            TabloİçeriğiArama_KapatmaTalebi = false;
        }

        private void Müşterıler_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Müşterıler.Text == "Tüm müşteriler için ortak")
            {
                Banka.Ücretler_TablodaGöster(Tablo, null);
            }
            else Banka.Ücretler_TablodaGöster(Tablo, Müşterıler.Text);

            AramaÇubuğu_İşTürü_TextChanged(null, null);

            splitContainer1.Panel1.Enabled = true;
            splitContainer1.Panel2.Enabled = true;
            Zam_Yap.Enabled = true;
            Kaydet.Enabled = false;
        }
        private void Tablo_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 1) return;

            _SutunuHesapla_(Tablo_Ücret.Index);
            if (Tablo_Maliyet.Visible) _SutunuHesapla_(Tablo_Maliyet.Index);
            
            Ayar_Değişti(null, null);

            void _SutunuHesapla_(int SutunNo)
            {
                string Formül = Tablo[SutunNo, e.RowIndex].Value as string;
                if (Formül.DoluMu())
                {
                    if (SutunNo == Tablo_Ücret.Index) Formül = Formül.Replace("%Ortak Ücreti%", "");
                    else if (SutunNo == Tablo_Maliyet.Index) Formül = Formül.Replace("%Maliyeti%", "");
                    Formül = Değişkenler.Düzenle(Formül);
                    Tablo[SutunNo, e.RowIndex].Value = Formül;

                    if (Formül.Contains("#")) Formül = Formül.Remove(Formül.IndexOf("#")); //notları sil

                    if (Formül.DoluMu(true))
                    {
                        Dictionary<string, string> tüm_değişkenler = new Dictionary<string, string>(Değişkenler.Tümü);

                        string gecici = Tablo[Tablo_Ücret.Index, e.RowIndex].Value as string;
                        if (gecici.DoluMu()) tüm_değişkenler.Add("Ortak Ücreti", gecici.Replace("%=0%", ""));
                       
                        gecici = Tablo[Tablo_Maliyet.Index, e.RowIndex].Value as string;
                        if (gecici.DoluMu()) tüm_değişkenler.Add("Maliyeti", gecici.Replace("%=0%", ""));

                        bool eşit_sıfır_olabilir = Formül.Contains("%=0%");
                        if (eşit_sıfır_olabilir) Formül = Formül.Replace("%=0%", "");

                        string snç = Değişkenler.Hesapla(Formül, out double çıktı, tüm_değişkenler); //işlem sonucu
                        if (snç.DoluMu() || çıktı < 0 || (çıktı == 0 && !eşit_sıfır_olabilir))
                        {
                            Tablo[SutunNo, e.RowIndex].Style.BackColor = Color.Salmon;
                            Tablo[SutunNo, e.RowIndex].ToolTipText = snç.DoluMu() ? snç :
                                "İşlem sonucu : " + çıktı + Environment.NewLine +
                                "Normal şartlarda sıfırdan büyük olmalıdır." + Environment.NewLine +
                                "Sıfır değerini kullanabilmek için formüle %=0% (yüzde eşittir sıfır yüzde) kelimesini ekleyiniz." + Environment.NewLine +
                                "Sıfırdan küçük değerler kabul edilmemektedir.";
                        }
                        else if (çıktı == 0)
                        {
                            Tablo[SutunNo, e.RowIndex].Style.BackColor = Color.Khaki;
                            Tablo[SutunNo, e.RowIndex].ToolTipText = "Sonucun 0 olduğunu göz önünde bulundurunuz";
                        }
                        else
                        {
                            Tablo[SutunNo, e.RowIndex].Style.BackColor = Color.White;
                            Tablo[SutunNo, e.RowIndex].ToolTipText = çıktı.Yazıya();
                        }
                    }
                }
                else
                {
                    Tablo[SutunNo, e.RowIndex].Style.BackColor = Color.White;
                    Tablo[SutunNo, e.RowIndex].ToolTipText = null;
                }
            }
        }
        private void Ayar_Değişti(object sender, EventArgs e)
        {
            splitContainer1.Panel1.Enabled = false;
            Kaydet.Enabled = true;
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

            bool Formül_uyarısı_yapıldı = false;
            for (int i = 0; i < Tablo.RowCount; i++)
            {
                string içerik = Tablo[Tablo_Ücret.Index, i].Value as string;
                if (Tablo[Tablo_Ücret.Index, i].Visible && içerik.DoluMu(true))
                {
                    if (içerik.Contains("%"))
                    {
                        if (!Formül_uyarısı_yapıldı)
                        {
                            Formül_uyarısı_yapıldı = true;
                            MessageBox.Show("İçerisnde formül olan hücreler hesaplama dışında bırakılacak.", Text);
                        }
                        continue;
                    }

                    try
                    {
                        double s = içerik.NoktalıSayıya();
                        s = (s / 100.0 * yüzde_s) + s;
                        Tablo[Tablo_Ücret.Index, i].Value = s.Yazıya();
                    }
                    catch (Exception) { }
                }
            }
        }

        private void Değişkenler_Click(object sender, EventArgs e)
        {
            Ekranlar.ÖnYüzler.Ekle(new Değişkenler_Ekranı());
            MutlakaGüncelle = true;
        }

        private void Kaydet_Click(object sender, EventArgs e)
        {
            string kdv = KDV.Text;
            if (!Ortak.YazıyıSayıyaDönüştür(ref kdv, "KDV kutucuğu", null, 0, 100))
            {
                KDV.Focus();
                return;
            }
            KDV.Text = kdv;

            bool TümMüşterilerİçinOrtak = Müşterıler.Text == "Tüm müşteriler için ortak";

            for (int i = 0; i < Tablo.RowCount; i++)
            {
                if (!string.IsNullOrEmpty((string)Tablo[Tablo_Ücret.Index, i].Value))
                {
                    Tablo_CellValueChanged(null, new DataGridViewCellEventArgs(Tablo_Ücret.Index, i));
                    if (Tablo[Tablo_Ücret.Index, i].Style.BackColor == Color.Salmon)
                    {
                        MessageBox.Show("Lütfen " + (i + 1) + ". satırdaki uyarının bulunduğu formülü kontrol ediniz", Text);
                        return;
                    }
                }

                if (TümMüşterilerİçinOrtak && !string.IsNullOrEmpty((string)Tablo[Tablo_Maliyet.Index, i].Value))
                {
                    Tablo_CellValueChanged(null, new DataGridViewCellEventArgs(Tablo_Maliyet.Index, i));
                    if (Tablo[Tablo_Maliyet.Index, i].Style.BackColor == Color.Salmon)
                    {
                        MessageBox.Show("Lütfen " + (i + 1) + ". satırdaki uyarının bulunduğu formülü kontrol ediniz", Text);
                        return;
                    }
                }
            }

            DialogResult Dr = MessageBox.Show("Değişiklikleri kaydetmek istediğinize emin misiniz?" +
                (string.IsNullOrWhiteSpace(AramaÇubuğu_İşTürü.Text) ? null : Environment.NewLine + Environment.NewLine + "Tablo içeriği filtrelendiğinden görünmeseler bile TÜM girdiler kaydedilecektir."),
                Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (Dr == DialogResult.No) return;

            Banka.Ayarlar_Genel("Bütçe/KDV", true).Yaz(null, kdv);
            if (TümMüşterilerİçinOrtak)
            {
                Banka.Ücretler_TablodakileriKaydet(Tablo, null);
            }
            else Banka.Ücretler_TablodakileriKaydet(Tablo, Müşterıler.Text);
            Banka.Değişiklikleri_Kaydet(Kaydet);

            Kaydet.Enabled = false;
            splitContainer1.Panel1.Enabled = true;
        }
    }
}
