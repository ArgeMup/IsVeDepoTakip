using ArgeMup.HazirKod;
using ArgeMup.HazirKod.Ekİşlemler;
using System;
using System.Linq;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip.Ekranlar
{
    public partial class Takvim : Form
    {
        double[] Süreler = null;
        IDepo_Eleman Gecici_Ayarlar;
        bool Gecikmeleri_gün_bazında_hesapla;

        public Takvim()
        {
            InitializeComponent();
            Ortak.YeniSayfaAçmaTalebi = null;

            //görsel çiziminin iyileşmsi için
            typeof(Control).InvokeMember("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty, null, Tablo, new object[] { DoubleBuffered });

            Gecici_Ayarlar = Ortak.GeçiciDepolama_PencereKonumları_Oku(this);
            Hatırlatıcılar_Filtrele_Yaklaşanlar.Checked = Gecici_Ayarlar.Oku_Bit("Hatırlatıcılar_Filtrele", true, 0);
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

            Gecikmeleri_gün_bazında_hesapla = Ayarlar.Oku_Bit("Gecikmeleri gün bazında hesapla", true);
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

                case Keys.F4:
                    TabloİçeriğiArama.SelectAll();
                    TabloİçeriğiArama.Focus();
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

            IDepo_Eleman DosyaEkleri = Banka.Tablo_Dal(null, Banka.TabloTürü.DosyaEkleri, "Dosya Ekleri", true);
            int sayac_Yaklaşanlar = 0, sayac_Gecikenler = 0, sayac_SeriNoluİş = 0, sayac_ÖdemeTalebi = 0, sayac_KullanıcıNotu = 0;
            System.Collections.Generic.List<DataGridViewRow> dizi = new System.Collections.Generic.List<DataGridViewRow>();
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

                object[] dizin = null;
                string UyarıTarihi_yazı = h.UyarıTarihi.ToString( "dd MMM ddd" + (Gecikmeleri_gün_bazında_hesapla ? null : " hh:mm") );
                switch (h.Tip)
                {
                    case Ortak.Hatırlatıcılar.Tip_.SeriNoluİş_DevamEdenTablosundan:
                    case Ortak.Hatırlatıcılar.Tip_.SeriNoluİş_TakvimTablosundan:
                        Banka.Talep_Bul_Detaylar_ Detaylar = Banka.Talep_Bul(h.İçerik, h.Müşteri, Banka.TabloTürü.DevamEden);
                        double Toplam = 0;
                        Banka.Talep_Ayıkla_SeriNoDalı(Detaylar.SeriNoDalı, out string Hasta, out string İşGirişTarihleri, out string İşÇıkışTarihleri, out string İşler, ref Toplam);
                        Banka.Talep_Ayıkla_SeriNoDalı(Detaylar.SeriNoDalı, out _, out _, out _, out string Notlar, out _);

                        //Eğer varsa dosya eki sayısının notlar eklenmesi
                        IDepo_Eleman SeriNonun_DosyaEkleri = DosyaEkleri.Bul(Detaylar.SeriNoDalı.Adı);
                        if (SeriNonun_DosyaEkleri != null)
                        {
                            int DosyaEkiSayısı = SeriNonun_DosyaEkleri.Elemanları.Length;
                            if (DosyaEkiSayısı > 0)
                            {
                                if (Notlar.DoluMu()) Notlar += Environment.NewLine + Environment.NewLine;

                                Notlar += "Dosya ekleri : " + DosyaEkiSayısı;
                            }
                        }

                        dizin = new object[] { h.İçerik, h.Müşteri, Hasta, İşGirişTarihleri, İşÇıkışTarihleri, İşler, UyarıTarihi_yazı, Notlar };
                        sayac_SeriNoluİş++;
                        break;

                    case Ortak.Hatırlatıcılar.Tip_.ÖdemeTalebi_KendiTablosundan:
                    case Ortak.Hatırlatıcılar.Tip_.ÖdemeTalebi_TakvimTablosundan:
                        IDepo_Eleman ödemedalı = Banka.Tablo_Dal(h.Müşteri, Banka.TabloTürü.ÖdemeTalepEdildi, "Ödeme", false, h.BaşlangışTarihi.Yazıya(ArgeMup.HazirKod.Dönüştürme.D_TarihSaat.Şablon_DosyaAdı2));
                        Banka.Talep_Ayıkla_ÖdemeDalı_Açıklama(ödemedalı, out string Açıklama, out _, out _);
                        dizin = new object[] { "Ödeme Talebi", h.Müşteri, null, Banka.Yazdır_Tarih(h.BaşlangışTarihi.Yazıya()), null, Açıklama, UyarıTarihi_yazı, null };
                        sayac_ÖdemeTalebi++;
                        break;

                    case Ortak.Hatırlatıcılar.Tip_.KullanıcıNotu:
                        dizin = new object[] { "Not", null, null, Banka.Yazdır_Tarih(h.BaşlangışTarihi.Yazıya()), null, h.İçerik, UyarıTarihi_yazı, null };
                        sayac_KullanıcıNotu++;
                        break;
                }

                DataGridViewRow yeni_satır = new DataGridViewRow();
                yeni_satır.CreateCells(Tablo, dizin);
                DataGridViewCell yeni_satır_6 = yeni_satır.Cells[6];

                if (h.UyarıTarihi < şimdi)
                {
                    //geciken
                    yeni_satır_6.ToolTipText = "Gecikti";
                    yeni_satır_6.Style.BackColor = System.Drawing.Color.Salmon;
                    sayac_Gecikenler++;
                }
                else
                {
                    //yaklaşan
                    if (h.Tip == Ortak.Hatırlatıcılar.Tip_.SeriNoluİş_TakvimTablosundan || 
                        h.Tip == Ortak.Hatırlatıcılar.Tip_.ÖdemeTalebi_TakvimTablosundan ||
                        (h.Tip == Ortak.Hatırlatıcılar.Tip_.KullanıcıNotu && (h.UyarıTarihi - h.BaşlangışTarihi).TotalDays > Süreler[0]))
                    {
                        yeni_satır_6.Style.BackColor = System.Drawing.Color.Khaki;
                        yeni_satır_6.ToolTipText = "En az 1 kere ertelendi, yaklaşıyor";
                    }
                    else yeni_satır_6.ToolTipText = "Yaklaşıyor";
                    sayac_Yaklaşanlar++;
                }

                yeni_satır_6.ToolTipText += Environment.NewLine + string.Format("{0:,0.0}", (h.UyarıTarihi - şimdi).TotalDays) + " gün";
                yeni_satır.Tag = h;
                
                dizi.Add(yeni_satır);
            }

            Gecici_Ayarlar.Yaz("Hatırlatıcılar_Filtrele", Hatırlatıcılar_Filtrele_Yaklaşanlar.Checked, 0);
            Gecici_Ayarlar.Yaz("Hatırlatıcılar_Filtrele", Hatırlatıcılar_Filtrele_Gecikenler.Checked, 1);
            Gecici_Ayarlar.Yaz("Hatırlatıcılar_Filtrele", Hatırlatıcılar_Filtrele_İşler.Checked, 2);
            Gecici_Ayarlar.Yaz("Hatırlatıcılar_Filtrele", Hatırlatıcılar_Filtrele_Notlar.Checked, 3);
            Gecici_Ayarlar.Yaz("Hatırlatıcılar_Filtrele", Hatırlatıcılar_Filtrele_ÖdemeTalepleri.Checked, 4);

            Hatırlatıcılar_Filtrele_Yaklaşanlar.Text = "Yaklaşanlar (" + sayac_Yaklaşanlar + ")";
            Hatırlatıcılar_Filtrele_Gecikenler.Text = "Gecikenler (" + sayac_Gecikenler + ")";
            Hatırlatıcılar_Filtrele_İşler.Text = "İşler (" + sayac_SeriNoluİş + ")";
            Hatırlatıcılar_Filtrele_Notlar.Text = "Notlar (" + sayac_KullanıcıNotu + ")";
            Hatırlatıcılar_Filtrele_ÖdemeTalepleri.Text = "Ödeme Talepleri (" + sayac_ÖdemeTalebi + ")";

            Tablo.Rows.AddRange(dizi.ToArray());
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
