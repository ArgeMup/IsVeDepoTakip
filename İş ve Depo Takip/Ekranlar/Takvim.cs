using ArgeMup.HazirKod;
using ArgeMup.HazirKod.Ekİşlemler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip.Ekranlar
{
    public partial class Takvim : Form
    {
        double[] Süreler = null;
        IDepo_Eleman Gecici_Ayarlar;

        public Takvim()
        {
            InitializeComponent();
            Ortak.YeniSayfaAçmaTalebi = null;

            Gecici_Ayarlar = Ortak.GeçiciDepolama_PencereKonumları_Oku(this);
            Hatırlatıcılar_Filtrele_Yaklaşanlar.Checked = Gecici_Ayarlar.Oku_Bit("Hatırlatıcılar_Filtrele", false, 0);
            Hatırlatıcılar_Filtrele_Gecikenler.Checked = Gecici_Ayarlar.Oku_Bit("Hatırlatıcılar_Filtrele", true, 1);
            Hatırlatıcılar_Filtrele_İşler.Checked = Gecici_Ayarlar.Oku_Bit("Hatırlatıcılar_Filtrele", true, 2);
            Hatırlatıcılar_Filtrele_Notlar.Checked = Gecici_Ayarlar.Oku_Bit("Hatırlatıcılar_Filtrele", true, 3);
            Hatırlatıcılar_Filtrele_ÖdemeTalepleri.Checked = Gecici_Ayarlar.Oku_Bit("Hatırlatıcılar_Filtrele", true, 4);

            Süreler = new double[7];
            IDepo_Eleman Ayarlar = Banka.Tablo_Dal(null, Banka.TabloTürü.Takvim, "Erteleme Süresi", true);
            for (int i = 0; i < Süreler.Length; i++)
            {
                Süreler[i] = Ayarlar.Oku_Sayı(null, i + 2, i);
            }

            SağTuşMenü_Ertele_1.Tag = 2;
            SağTuşMenü_Ertele_2.Tag = 3;
            SağTuşMenü_Ertele_3.Tag = 4;
            SağTuşMenü_Ertele_4.Tag = 5;
            SağTuşMenü_Ertele_5.Tag = 6;
        }
        private void Takvim_Shown(object sender, EventArgs e)
        {
            Hatırlatıcılar_Filtrele_CheckedChanged(null, null);
            TabloİçeriğiArama.Focus();
        }
        private void Takvim_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                    Ortak.YeniSayfaAçmaTalebi = new object[] { "Yeni İş Girişi", null, null, Banka.TabloTürü.DevamEden, null };
                    Close();
                    break;

                case Keys.F2:
                    Ortak.YeniSayfaAçmaTalebi = new string[] { "Tüm İşler", null };
                    Close();
                    break;

                case Keys.F3:
                    Ortak.YeniSayfaAçmaTalebi = new string[] { "Tüm İşler", "Arama" };
                    Close();
                    break;
            }
        }
        private void Hatırlatıcılar_Filtrele_CheckedChanged(object sender, EventArgs e)
        {
            if (Süreler == null) return;
            DateTime şimdi = DateTime.Now;
            Ortak.Gösterge.Başlat("Bekleyiniz", true, Tablo, Ortak.Hatırlatıcılar.Tümü.Length);

            if (Ortak.Hatırlatıcılar.YenidenKontrolEdilmeli) Ortak.Hatırlatıcılar.KontrolEt();

            Tablo.Rows.Clear();
            TabloİçeriğiArama.Text = "";
            if (Tablo.SortedColumn != null)
            {
                DataGridViewColumn col = Tablo.SortedColumn;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                col.SortMode = DataGridViewColumnSortMode.Automatic;
            }

            foreach (Ortak.Hatırlatıcılar.Hatırlatıcı_ h in Ortak.Hatırlatıcılar.Tümü)
            {
                if (!Ortak.Gösterge.Çalışsın) break;

                if (h.UyarıTarihi < şimdi)
                {
                    //geciken
                    if (!Hatırlatıcılar_Filtrele_Gecikenler.Checked) continue;
                }
                else
                {
                    //yaklaşan
                    if (!Hatırlatıcılar_Filtrele_Yaklaşanlar.Checked) continue;
                }

                switch (h.Tip)
                {
                    case Ortak.Hatırlatıcılar.Tip_.KullanıcıNotu:
                        if (!Hatırlatıcılar_Filtrele_Notlar.Checked) continue;
                        break;

                    case Ortak.Hatırlatıcılar.Tip_.SeriNoluİş_DevamEdenTablosundan:
                    case Ortak.Hatırlatıcılar.Tip_.SeriNoluİş_TakvimTablosundan:
                        if (!Hatırlatıcılar_Filtrele_İşler.Checked) continue;
                        break;

                    case Ortak.Hatırlatıcılar.Tip_.ÖdemeTalebi_KendiTablosundan:
                    case Ortak.Hatırlatıcılar.Tip_.ÖdemeTalebi_TakvimTablosundan:
                        if (!Hatırlatıcılar_Filtrele_ÖdemeTalepleri.Checked) continue;
                        break;
                }

                int satır = 0;
                switch (h.Tip)
                {
                    case Ortak.Hatırlatıcılar.Tip_.SeriNoluİş_DevamEdenTablosundan:
                    case Ortak.Hatırlatıcılar.Tip_.SeriNoluİş_TakvimTablosundan:
                        IDepo_Eleman seri_no_dalı = Banka.Talep_Bul_DevamEden(h.Müşteri, h.İçerik);
                        double Toplam = 0;
                        Banka.Talep_Ayıkla_SeriNoDalı(seri_no_dalı, out string Hasta, out string İşGirişTarihleri, out string İşÇıkışTarihleri, out string İşler, ref Toplam);
                        Banka.Talep_Ayıkla_SeriNoDalı(seri_no_dalı, out _, out _, out _, out string Notlar, out _);
                        satır = Tablo.Rows.Add(new object[] { h.İçerik, h.Müşteri, Hasta, İşGirişTarihleri, İşÇıkışTarihleri, İşler, h.UyarıTarihi, Notlar });
                        break;

                    case Ortak.Hatırlatıcılar.Tip_.ÖdemeTalebi_KendiTablosundan:
                    case Ortak.Hatırlatıcılar.Tip_.ÖdemeTalebi_TakvimTablosundan:
                        IDepo_Eleman ödemedalı = Banka.Tablo_Dal(h.Müşteri, Banka.TabloTürü.ÖdemeTalepEdildi, "Ödeme", false, h.BaşlangışTarihi.Yazıya(ArgeMup.HazirKod.Dönüştürme.D_TarihSaat.Şablon_DosyaAdı2));
                        Banka.Talep_Ayıkla_ÖdemeDalı_Açıklama(ödemedalı, "0", "0", out string Açıklama, out _, out _);
                        satır = Tablo.Rows.Add(new object[] { "Ödeme Talebi", h.Müşteri, null, Banka.Yazdır_Tarih(h.BaşlangışTarihi.Yazıya()), null, Açıklama, h.UyarıTarihi, null });
                        break;

                    case Ortak.Hatırlatıcılar.Tip_.KullanıcıNotu:
                        satır = Tablo.Rows.Add(new object[] { "Not", null, null, Banka.Yazdır_Tarih(h.BaşlangışTarihi.Yazıya()), null, h.İçerik, h.UyarıTarihi, null });
                        break;
                }

                if (h.UyarıTarihi < şimdi)
                {
                    //geciken
                    Tablo[6, satır].ToolTipText = "Gecikti";
                    Tablo[6, satır].Style.BackColor = System.Drawing.Color.Salmon;
                }
                else
                {
                    //yaklaşan
                    if (h.Tip == Ortak.Hatırlatıcılar.Tip_.SeriNoluİş_TakvimTablosundan || 
                        h.Tip == Ortak.Hatırlatıcılar.Tip_.ÖdemeTalebi_TakvimTablosundan ||
                        (h.Tip == Ortak.Hatırlatıcılar.Tip_.KullanıcıNotu && (h.UyarıTarihi - h.BaşlangışTarihi).TotalDays > Süreler[0]))
                    {
                        Tablo[6, satır].Style.BackColor = System.Drawing.Color.Khaki;
                        Tablo[6, satır].ToolTipText = "En az 1 kere ertelendi, yaklaşıyor";
                    }
                    else Tablo[6, satır].ToolTipText = "Yaklaşıyor";
                }

                Tablo[6, satır].ToolTipText += Environment.NewLine + string.Format("{0:,0.0}", (h.UyarıTarihi - şimdi).TotalDays) + " gün"; 
                Tablo.Rows[satır].Tag = h;
            }

            Gecici_Ayarlar.Yaz("Hatırlatıcılar_Filtrele", Hatırlatıcılar_Filtrele_Yaklaşanlar.Checked, 0);
            Gecici_Ayarlar.Yaz("Hatırlatıcılar_Filtrele", Hatırlatıcılar_Filtrele_Gecikenler.Checked, 1);
            Gecici_Ayarlar.Yaz("Hatırlatıcılar_Filtrele", Hatırlatıcılar_Filtrele_İşler.Checked, 2);
            Gecici_Ayarlar.Yaz("Hatırlatıcılar_Filtrele", Hatırlatıcılar_Filtrele_Notlar.Checked, 3);
            Gecici_Ayarlar.Yaz("Hatırlatıcılar_Filtrele", Hatırlatıcılar_Filtrele_ÖdemeTalepleri.Checked, 4);

            Tablo.ClearSelection();
            Ortak.Gösterge.Bitir();
        }

        bool TabloİçeriğiArama_Çalışıyor = false;
        bool TabloİçeriğiArama_KapatmaTalebi = false;
        int TabloİçeriğiArama_Tik = 0;
        int TabloİçeriğiArama_Sayac_Bulundu = 0;
        private void TabloİçeriğiArama_TextChanged(object sender, EventArgs e)
        {
            TabloİçeriğiArama_Tik = Environment.TickCount + 100;
            if (TabloİçeriğiArama.Text.Length < 2)
            {
                if (TabloİçeriğiArama_Sayac_Bulundu != 0)
                {
                    TabloİçeriğiArama.BackColor = System.Drawing.Color.Salmon;

                    for (int satır = 0; satır < Tablo.RowCount; satır++)
                    {
                        Tablo.Rows[satır].Visible = true;
                        if (TabloİçeriğiArama_Tik < Environment.TickCount) { Application.DoEvents(); TabloİçeriğiArama_Tik = Environment.TickCount + 100; }
                    }

                    TabloİçeriğiArama.BackColor = System.Drawing.Color.White;
                    TabloİçeriğiArama_Sayac_Bulundu = 0;
                }

                return;
            }

            if (TabloİçeriğiArama_Çalışıyor) { TabloİçeriğiArama_KapatmaTalebi = true; return; }

            TabloİçeriğiArama_Çalışıyor = true;
            TabloİçeriğiArama_KapatmaTalebi = false;
            TabloİçeriğiArama_Sayac_Bulundu = 0;
            TabloİçeriğiArama_Tik = Environment.TickCount + 500;
            TabloİçeriğiArama.BackColor = System.Drawing.Color.Salmon;

            string[] arananlar = TabloİçeriğiArama.Text.ToLower().Split(' ');
            for (int satır = 0; satır < Tablo.RowCount && !TabloİçeriğiArama_KapatmaTalebi; satır++)
            {
                bool bulundu = false;
                for (int sutun = 0; sutun < Tablo.Columns.Count; sutun++)
                {
                    if (Tablo[sutun, satır].Value == null) continue;

                    string içerik = Tablo[sutun, satır].Value.ToString();
                    if (string.IsNullOrEmpty(içerik))
                    {
                        /*gerçekleşme tarihi arka renklerinin bozulmaması için*/
                        if (sutun != 6) Tablo[sutun, satır].Style.BackColor = System.Drawing.Color.White;
                    }
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
                            /*gerçekleşme tarihi arka renklerinin bozulmaması için*/
                            if (sutun != 6) Tablo[sutun, satır].Style.BackColor = System.Drawing.Color.YellowGreen;
                            
                            bulundu = true;
                        }
                        else
                        {
                            /*gerçekleşme tarihi arka renklerinin bozulmaması için*/
                            if (sutun != 6) Tablo[sutun, satır].Style.BackColor = System.Drawing.Color.White;
                        } 
                    }
                }

                Tablo.Rows[satır].Visible = bulundu;
                if (bulundu) TabloİçeriğiArama_Sayac_Bulundu++;

                if (TabloİçeriğiArama_Tik < Environment.TickCount) { Application.DoEvents(); TabloİçeriğiArama_Tik = Environment.TickCount + 500; }
            }

            if (TabloİçeriğiArama_Sayac_Bulundu == 0) TabloİçeriğiArama_Sayac_Bulundu = -1;

            TabloİçeriğiArama.BackColor = System.Drawing.Color.White;
            TabloİçeriğiArama_Çalışıyor = false;
            Tablo.ClearSelection();

            if (TabloİçeriğiArama_KapatmaTalebi) TabloİçeriğiArama_TextChanged(null, null);
            TabloİçeriğiArama_KapatmaTalebi = false;
        }

        private void SağTuşMenü_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Tablo.SelectedRows.Count != 1) { e.Cancel = true; return; }

            Ortak.Hatırlatıcılar.Hatırlatıcı_ h = Tablo.Rows[Tablo.SelectedRows[0].Index].Tag as Ortak.Hatırlatıcılar.Hatırlatıcı_;
            DateTime şimdi = DateTime.Now;

            if (h.Tip == Ortak.Hatırlatıcılar.Tip_.ÖdemeTalebi_KendiTablosundan && h.UyarıTarihi >= şimdi) { e.Cancel = true; return; }

            if (h.UyarıTarihi < şimdi)
            {
                if (h.Tip == Ortak.Hatırlatıcılar.Tip_.ÖdemeTalebi_KendiTablosundan || h.Tip == Ortak.Hatırlatıcılar.Tip_.ÖdemeTalebi_TakvimTablosundan)
                {
                    SağTuşMenü_Ertele_Varsayılan.Text = Süreler[1].ToString() + " gün ertele -> " + şimdi.AddDays(Süreler[1]).ToString("f");
                    SağTuşMenü_Ertele_Varsayılan.Tag = 1;
                }
                else
                {
                    SağTuşMenü_Ertele_Varsayılan.Text = Süreler[0].ToString() + " gün ertele -> " + şimdi.AddDays(Süreler[0]).ToString("f");
                    SağTuşMenü_Ertele_Varsayılan.Tag = 0;
                }

                SağTuşMenü_Ertele_1.Text = Süreler[2].ToString() + " gün ertele -> " + şimdi.AddDays(Süreler[2]).ToString("f");
                SağTuşMenü_Ertele_2.Text = Süreler[3].ToString() + " gün ertele -> " + şimdi.AddDays(Süreler[3]).ToString("f");
                SağTuşMenü_Ertele_3.Text = Süreler[4].ToString() + " gün ertele -> " + şimdi.AddDays(Süreler[4]).ToString("f");
                SağTuşMenü_Ertele_4.Text = Süreler[5].ToString() + " gün ertele -> " + şimdi.AddDays(Süreler[5]).ToString("f");
                SağTuşMenü_Ertele_5.Text = Süreler[6].ToString() + " gün ertele -> " + şimdi.AddDays(Süreler[6]).ToString("f");

                SağTuşMenü_Ertele_Varsayılan.Visible = true;
                SağTuşMenü_Ertele_1.Visible = true;
                SağTuşMenü_Ertele_2.Visible = true;
                SağTuşMenü_Ertele_3.Visible = true;
                SağTuşMenü_Ertele_4.Visible = true;
                SağTuşMenü_Ertele_5.Visible = true;
                toolStripSeparator1.Visible = true;
            }
            else
            {
                SağTuşMenü_Ertele_Varsayılan.Visible = false;
                SağTuşMenü_Ertele_1.Visible = false;
                SağTuşMenü_Ertele_2.Visible = false;
                SağTuşMenü_Ertele_3.Visible = false;
                SağTuşMenü_Ertele_4.Visible = false;
                SağTuşMenü_Ertele_5.Visible = false;
                toolStripSeparator1.Visible = false;
            }

            //SağTuşMenü_ErtelemeyiKaldır.Visible = false;
            SağTuşMenü_ErtelemeyiKaldır_Sil.Visible = false;
            SağTuşMenü_MüşteriyeGönder.Visible = h.Tip == Ortak.Hatırlatıcılar.Tip_.SeriNoluİş_DevamEdenTablosundan || h.Tip == Ortak.Hatırlatıcılar.Tip_.SeriNoluİş_TakvimTablosundan;
            
            switch (h.Tip)
            {
                case Ortak.Hatırlatıcılar.Tip_.KullanıcıNotu:
                    SağTuşMenü_ErtelemeyiKaldır_Sil.Visible = true;
                    SağTuşMenü_ErtelemeyiKaldır_Sil.Text = "Sil";
                    break;

                case Ortak.Hatırlatıcılar.Tip_.SeriNoluİş_TakvimTablosundan:
                case Ortak.Hatırlatıcılar.Tip_.ÖdemeTalebi_TakvimTablosundan:
                    SağTuşMenü_ErtelemeyiKaldır_Sil.Visible = true;
                    SağTuşMenü_ErtelemeyiKaldır_Sil.Text = "Ertelemeyi Kaldır";
                    break;
            }
        }
        private void SağTuşMenü_Ertele_X_Click(object sender, EventArgs e)
        {
            double süre = Süreler[(int)(sender as ToolStripMenuItem).Tag];
            DateTime yeni_tarih = DateTime.Now.AddDays(süre);

            string soru = "Seçtiniz hatırlatıcı " + süre + " gün ertelenecek." + Environment.NewLine + Environment.NewLine +
                "İşleme devam etmek istiyor musunuz?";
            DialogResult Dr = MessageBox.Show(soru, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (Dr == DialogResult.No) return;

            IDepo_Eleman tablo_hatırlatıcılar_SeriNoluİş = Banka.Tablo_Dal(null, Banka.TabloTürü.Takvim, "Hatırlatıcılar SeriNoluİş", true);
            IDepo_Eleman tablo_hatırlatıcılar_ÖdemeTalebi = Banka.Tablo_Dal(null, Banka.TabloTürü.Takvim, "Hatırlatıcılar ÖdemeTalebi", true);
            IDepo_Eleman tablo_hatırlatıcılar_KullanıcıNotu = Banka.Tablo_Dal(null, Banka.TabloTürü.Takvim, "Hatırlatıcılar KullanıcıNotu", true);
            Ortak.Hatırlatıcılar.Hatırlatıcı_ h = Tablo.SelectedRows[0].Tag as Ortak.Hatırlatıcılar.Hatırlatıcı_;
                
            switch (h.Tip)
            {
                case Ortak.Hatırlatıcılar.Tip_.KullanıcıNotu:
                    tablo_hatırlatıcılar_KullanıcıNotu.Yaz(h.BaşlangışTarihi.Yazıya(), yeni_tarih);
                    break;

                case Ortak.Hatırlatıcılar.Tip_.SeriNoluİş_DevamEdenTablosundan:
                case Ortak.Hatırlatıcılar.Tip_.SeriNoluİş_TakvimTablosundan:
                    tablo_hatırlatıcılar_SeriNoluİş.Yaz(h.İçerik, yeni_tarih);
                    break;

                case Ortak.Hatırlatıcılar.Tip_.ÖdemeTalebi_KendiTablosundan:
                case Ortak.Hatırlatıcılar.Tip_.ÖdemeTalebi_TakvimTablosundan:
                    tablo_hatırlatıcılar_ÖdemeTalebi.Yaz(h.BaşlangışTarihi.Yazıya(ArgeMup.HazirKod.Dönüştürme.D_TarihSaat.Şablon_DosyaAdı2), yeni_tarih);
                    break;
            }

            h.UyarıTarihi = yeni_tarih;

            Banka.Değişiklikleri_Kaydet(Tablo);
            Hatırlatıcılar_Filtrele_CheckedChanged(null, null);
        }
        private void SağTuşMenü_ErtelemeyiKaldır_Sil_Click(object sender, EventArgs e)
        {
            Ortak.Hatırlatıcılar.Hatırlatıcı_ h = Tablo.SelectedRows[0].Tag as Ortak.Hatırlatıcılar.Hatırlatıcı_;

            string soru = (h.Tip == Ortak.Hatırlatıcılar.Tip_.KullanıcıNotu ? "Seçtiniz hatırlatıcı silinecek." : "Seçtiniz hatırlatıcı geçikti olarak işaretlenecek.") + Environment.NewLine + Environment.NewLine +
                "İşleme devam etmek istiyor musunuz?";
            DialogResult Dr = MessageBox.Show(soru, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (Dr == DialogResult.No) return;

            switch (h.Tip)
            {
                case Ortak.Hatırlatıcılar.Tip_.KullanıcıNotu:
                    Banka.Tablo_Dal(null, Banka.TabloTürü.Takvim, "Hatırlatıcılar KullanıcıNotu", true).Sil(h.BaşlangışTarihi.Yazıya());
                    break;

                case Ortak.Hatırlatıcılar.Tip_.SeriNoluİş_TakvimTablosundan:
                    Banka.Tablo_Dal(null, Banka.TabloTürü.Takvim, "Hatırlatıcılar SeriNoluİş", true).Sil(h.İçerik);
                    break;

                case Ortak.Hatırlatıcılar.Tip_.ÖdemeTalebi_TakvimTablosundan:
                    Banka.Tablo_Dal(null, Banka.TabloTürü.Takvim, "Hatırlatıcılar ÖdemeTalebi", true).Sil(h.BaşlangışTarihi.Yazıya(ArgeMup.HazirKod.Dönüştürme.D_TarihSaat.Şablon_DosyaAdı2));
                    break;
            }
            
            Banka.Değişiklikleri_Kaydet(Tablo);
            Hatırlatıcılar_Filtrele_CheckedChanged(null, null);
        }
        private void SağTuşMenü_MüşteriyeGönder_Click(object sender, EventArgs e)
        {
            string soru = "Seçtiniz iş müşteriye gönderildi olarak işaretlenecek." + Environment.NewLine + Environment.NewLine +
                "İşleme devam etmek istiyor musunuz?";
            DialogResult Dr = MessageBox.Show(soru, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (Dr == DialogResult.No) return;

            DataGridViewRow s = Tablo.SelectedRows[0];

            Banka.Talep_İşaretle_DevamEden_MüşteriyeGönderildi((string)Tablo[1, s.Index].Value, new string[] { (string)Tablo[0, s.Index].Value }.ToList());
            Banka.Değişiklikleri_Kaydet(Tablo);
            Hatırlatıcılar_Filtrele_CheckedChanged(null, null);
        }

        private void YeniOluştur_İçerik_TextChanged(object sender, EventArgs e)
        {
            YeniOluştur_Oluştur.Enabled = true;
        }
        private void YeniOluştur_Oluştur_Click(object sender, EventArgs e)
        {
            YeniOluştur_İçerik.Text = YeniOluştur_İçerik.Text.Trim();
            if (YeniOluştur_İçerik.Text.BoşMu()) return;

            DateTime t = DateTime.Now;
            Banka.Tablo_Dal(null, Banka.TabloTürü.Takvim, "Hatırlatıcılar KullanıcıNotu", true)[t.Yazıya()].İçeriği = new string[] { t.AddDays(Süreler[0]).Yazıya(), YeniOluştur_İçerik.Text };
            Banka.Değişiklikleri_Kaydet(YeniOluştur_Oluştur);
            Hatırlatıcılar_Filtrele_CheckedChanged(null, null);

            YeniOluştur_Oluştur.Enabled = false;
        }

        private void Tablo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Ortak.Hatırlatıcılar.Hatırlatıcı_ h = Tablo.SelectedRows[0].Tag as Ortak.Hatırlatıcılar.Hatırlatıcı_;
            if (h.Tip != Ortak.Hatırlatıcılar.Tip_.SeriNoluİş_DevamEdenTablosundan && h.Tip != Ortak.Hatırlatıcılar.Tip_.SeriNoluİş_TakvimTablosundan) return;

            string soru = "Seçtiğiniz iş açılacak." + Environment.NewLine + Environment.NewLine + "İşleme devam etmek istiyor musunuz?";
            DialogResult Dr = MessageBox.Show(soru, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (Dr == DialogResult.No) return;

            Ortak.YeniSayfaAçmaTalebi = new object[] { "Yeni İş Girişi", h.Müşteri, h.İçerik, Banka.TabloTürü.DevamEden, null };
            Close();
        }
    }
}
