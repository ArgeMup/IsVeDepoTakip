﻿using ArgeMup.HazirKod;
using ArgeMup.HazirKod.Ekİşlemler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip.Ekranlar
{
    public partial class Yeni_İş_Girişi : Form, IGüncellenenSeriNolar
    {
        readonly string Müşteri = null, SeriNo = null, YeniKayıtİçinTutulanSeriNo = null, EkTanım = null;
        readonly Banka.TabloTürü SeriNoTürü = Banka.TabloTürü.DevamEden_TeslimEdildi_ÖdemeTalepEdildi_Ödendi;
        bool SadeceOkunabilir = false, Notlar_TarihSaatEklendi = false;
        List<string> Müşteriler_Liste = null, Hastalar_Liste = new List<string>(), İşTürleri_Liste = null;
        Yeni_İş_Girişi_DosyaEkleri P_Yeni_İş_Girişi_DosyaEkleri = new Yeni_İş_Girişi_DosyaEkleri();
        Yeni_İş_Girişi_Epostalar P_Yeni_İş_Girişi_Epostalar = new Yeni_İş_Girişi_Epostalar();

        public Yeni_İş_Girişi(string SeriNo = null, string Müşteri = null, Banka.TabloTürü SeriNoTürü = Banka.TabloTürü.DevamEden_TeslimEdildi_ÖdemeTalepEdildi_Ödendi, string EkTanım = null)
        {
            InitializeComponent();

            if (string.IsNullOrWhiteSpace(SeriNo))
            {
                Müşteri = null;
                SeriNo = null;
            }
            else
            {
                this.Müşteri = Müşteri;
                this.SeriNo = SeriNo;
                this.EkTanım = EkTanım;
            }
            this.SeriNoTürü = SeriNoTürü;

            İşTürleri_Liste = Banka.İşTürü_Listele();
            Ortak.GrupArayıcı(İşTürleri_SeçimKutusu, İşTürleri_Liste);

            string ipucu = "Arama Çubuğu";
            if (İşTürleri_Liste.Count > 0)
            {
                ipucu += Environment.NewLine + Environment.NewLine + "Alttaki şekilde arattırabilirsiniz." + Environment.NewLine + Environment.NewLine + İşTürleri_Liste[0] + Environment.NewLine + Environment.NewLine;
                string[] d = İşTürleri_Liste[0].Trim().ToLower().Split(' ');
                foreach (string dd in d)
                {
                    string ddd = dd.Trim();
                    if (ddd.BoşMu(true)) continue;
                    if (ddd.Length > 2) ipucu += ddd.Substring(0, 2) + " ";
                    else ipucu += ddd + " ";
                }
            }
            İpUcu_Genel.SetToolTip(İşTürleri_AramaÇubuğu, ipucu);
            Hastalar_AdVeSoyadıDüzelt.Checked = Banka.Ayarlar_Kullanıcı(Name, "Hastalar_AdVeSoyadıDüzelt").Oku_Bit(null, true);

            System.Windows.Forms.Padding pd = new System.Windows.Forms.Padding(0);
            foreach (Label biri in Tablo_Dişler.Controls)
            {
                if (byte.TryParse(biri.Text, out byte gecici)) biri.Tag = gecici;

                biri.Click += new System.EventHandler(Dişler_Değişiklik_Yapılıyor);
                biri.Margin = pd;
            }

            if (SeriNo == null)
            {
                Müşteriler_Liste = Banka.Müşteri_Listele();
                Ortak.GrupArayıcı(Müşteriler_SeçimKutusu, Müşteriler_Liste);
                Ortak.GrupArayıcı(Hastalar_SeçimKutusu);

                Tablo_Giriş_Tarihi.Visible = false;
                Tablo_Çıkış_Tarihi.Visible = false;

                if (SeriNoTürü == Banka.TabloTürü.ÜcretHesaplama) Text += " - Ücret Hesaplama";
                else
                {
                    YeniKayıtİçinTutulanSeriNo = Banka.SeriNo_Üret(false);
                    Text += " - " + YeniKayıtİçinTutulanSeriNo + " - YENİ";
                }
            }
            else
            {
                Müşteriler_Liste = new List<string>();
                Müşteriler_Liste.Add(Müşteri);
                Müşteriler_AramaÇubuğu.Text = Müşteri;
                Müşteriler_SeçimKutusu.SelectedIndex = 0;
                Ayraç_Kat_1_2.SplitterDistance = (Müşteriler_AramaÇubuğu.Size.Height * 2) + (Müşteriler_AramaÇubuğu.Size.Height / 2);

                Banka.Talep_Bul_Detaylar_ detaylar = Banka.Talep_Bul(SeriNo, Müşteri, SeriNoTürü, EkTanım);
                if (detaylar == null) throw new Exception(Müşteri + " / Devam Eden / Talepler / " + SeriNo + " bulunamadı");

                Banka.Talep_Hesaplat_FirmaİçindekiSüreler(detaylar.SeriNoDalı, out TimeSpan Firmaİçinde, out TimeSpan Toplam);
                KurlarVeSüreler.Tag = "Toplam " + Banka.Yazdır_Tarih_Gün(Toplam) + ", firma içinde " + Banka.Yazdır_Tarih_Gün(Firmaİçinde);

                Müşteriler_SeçimKutusu.Enabled = false;
                Hastalar_SeçimKutusu.Enabled = false;
                switch (SeriNoTürü)
                {
                    case Banka.TabloTürü.ÜcretHesaplama:
                        Müşteriler_Grup.Enabled = false;
                        Tablo_Çıkış_Tarihi.Visible = false;
                        break;

                    case Banka.TabloTürü.DevamEden:
                    case Banka.TabloTürü.TeslimEdildi:
                        Müşteriler_Grup.Enabled = false;
                        break;

                    case Banka.TabloTürü.ÖdemeTalepEdildi:
                    case Banka.TabloTürü.Ödendi:
                        SadeceOkunabilir = true;
                        Müşteriler_AramaÇubuğu.ReadOnly = true;
                        Hastalar_AramaÇubuğu.ReadOnly = true;
                        Hastalar_AdVeSoyadıDüzelt.Enabled = false;
                        İskonto.ReadOnly = true;
                        Notlar.ReadOnly = true;
                        Ayraç_Kat_3_SolSağ.Panel1Collapsed = true;
                        Tablo.ReadOnly = true;
                        Seçili_Satırı_Sil.Enabled = false;
                        break;
                }

                Banka.Talep_Ayıkla_SeriNoDalı(detaylar.SeriNoDalı, out string _, out string Hasta, out string İskonto_, out string Notlar_, out string _);
                Text += " - " + SeriNo + " - " + SeriNoTürü.ToString();
                Hastalar_AramaÇubuğu.Text = Hasta;
                İskonto.Text = İskonto_;
                Notlar.Text = Notlar_;

                string hata_bilgilendirmesi = "";
                Tablo.RowCount = detaylar.SeriNoDalı.Elemanları.Length + 1;
                for (int i = 0; i < detaylar.SeriNoDalı.Elemanları.Length; i++)
                {
                    Banka.Talep_Ayıkla_İşTürüDalı(detaylar.SeriNoDalı.Elemanları[i], out string İşTürü, out string GirişTarihi, out string ÇıkışTarihi, out string Ücret1, out string _, out byte[] Kullanım_AdetVeKonum);

                    if (!İşTürleri_Liste.Contains(İşTürü) && !SadeceOkunabilir)
                    {
                        //eskiden varolan şuanda bulunmayan bir iş türü 
                        hata_bilgilendirmesi += (i + 1) + ". satırdaki \"" + İşTürü + "\" olarak tanımlı iş türü şuanda mevcut olmadığından satır içeriği boş olarak bırakıldı" + Environment.NewLine;
                    }
                    else
                    {
                        Tablo.Rows[i].ReadOnly = true;
                        Tablo[Tablo_İş_Türü.Index, i].Value = İşTürü;
                        Tablo[Tablo_İş_Türü.Index, i].ToolTipText = Banka.Ücretler_BirimÜcretMaliyet_Detaylı(Müşteri, İşTürü); //ücretlendirme ipucları
                        Tablo[Tablo_Adet.Index, i].Value = Banka.Ücretler_AdetÇarpanı(Kullanım_AdetVeKonum).Yazıya();
                        Tablo[Tablo_Adet.Index, i].Tag = Kullanım_AdetVeKonum;
                    }

                    Tablo[Tablo_Ücret.Index, i].Value = Ücret1;

                    //tarih giriş
                    Tablo[Tablo_Giriş_Tarihi.Index, i].Value = Banka.Yazdır_Tarih(GirişTarihi);
                    Tablo[Tablo_Giriş_Tarihi.Index, i].Tag = GirişTarihi.TarihSaate();
                    Tablo[Tablo_Giriş_Tarihi.Index, i].ToolTipText = GirişTarihi;
                    
                    if (ÇıkışTarihi.DoluMu()) //tarih çıkış
                    {
                        Tablo[Tablo_Çıkış_Tarihi.Index, i].Value = Banka.Yazdır_Tarih(ÇıkışTarihi);
                        Tablo[Tablo_Çıkış_Tarihi.Index, i].Tag = ÇıkışTarihi.TarihSaate();
                        Tablo[Tablo_Çıkış_Tarihi.Index, i].ToolTipText = ÇıkışTarihi;
                    }
                }

                Tablo_Dişler.Enabled = !SadeceOkunabilir;
                Dişler_GörseliniGüncelle();
                
                if (!string.IsNullOrEmpty(hata_bilgilendirmesi))
                {
                    MessageBox.Show(hata_bilgilendirmesi + Environment.NewLine + "Bu mesaj Notlar içerisine aktarıldı", Text);
                    Notlar.Text = hata_bilgilendirmesi + Notlar.Text;
                    Ayraç_Kat_2_3.SplitterDistance *= 2;
                }

                var l_dosya_ekleri = Banka.DosyaEkleri_Listele(SeriNo);
                for (int i = l_dosya_ekleri.Length - 1; i >= 0; i--)
                {
                    Banka.DosyaEkleri_Ayıkla_SeriNoAltındakiDosyaEkiDalı(l_dosya_ekleri[i], out string DosyaAdı, out bool Html_denGöster);
                    P_Yeni_İş_Girişi_DosyaEkleri.P_DosyaEkleri_Liste.Items.Add(DosyaAdı, Html_denGöster);
                }
                P_DosyaEkleri_TuşunuGüncelle();
            }

            if (SeriNoTürü == Banka.TabloTürü.ÜcretHesaplama) KurlarVeSüreler.BackColor = System.Drawing.Color.Violet;

            P_Yeni_İş_Girişi_DosyaEkleri.SadeceOkunabilir = SadeceOkunabilir;
            P_Yeni_İş_Girişi_DosyaEkleri.ÖnYüzGörseliniGüncelle = P_DosyaEkleri_TuşunuGüncelle;
            P_Yeni_İş_Girişi_DosyaEkleri.SeriNo = SeriNo;
            P_Yeni_İş_Girişi_DosyaEkleri.P_DosyaEkleri_Geri.Click += new EventHandler(P_DosyaEkleri_Geri_Click);
            P_Yeni_İş_Girişi_DosyaEkleri.P_DosyaEkleri_GelenKutusunuAç.Click += new EventHandler(P_DosyaEkleri_GelenKutusunuAç_Click);
            DragDrop += new DragEventHandler(P_Yeni_İş_Girişi_DosyaEkleri.Yeni_İş_Girişi_DragDrop);
            DragEnter += new DragEventHandler(P_Yeni_İş_Girişi_DosyaEkleri.Yeni_İş_Girişi_DragEnter);
            Ortak.AltSayfayıYükle(P_DosyaEkleri, P_Yeni_İş_Girişi_DosyaEkleri);
            P_Yeni_İş_Girişi_DosyaEkleri.DeğişiklikYapıldı = false;

            P_Yeni_İş_Girişi_Epostalar.Çıkış_Geri.Click += new EventHandler(P_Epostalar_Geri_Click);
            Ortak.AltSayfayıYükle(P_Epostalar, P_Yeni_İş_Girişi_Epostalar);
            P_Yeni_İş_Girişi_Epostalar.DeğişiklikYapıldı = false;

            //Panelin görüntülenebilmesi için eklentiler
            Ayraç_Kat_3_SolSağ.Panel2.Controls.Remove(P_DosyaEkleri); Controls.Add(P_DosyaEkleri); P_DosyaEkleri.BringToFront();
            Ayraç_Kat_3_SolSağ.Panel2.Controls.Remove(P_Epostalar); Controls.Add(P_Epostalar); P_Epostalar.BringToFront();
        }
        private void Yeni_İş_Girişi_Shown(object sender, EventArgs e)
        {
            Tablo.Rows[Tablo.RowCount - 1].Selected = true;
            Kaydet_TuşGörünürlüğü(false);
            Müşteriler_AramaÇubuğu.Focus();

            Döviz.KurlarıAl(_GeriBildirim_Kurlar_);
            void _GeriBildirim_Kurlar_(string Yazı, string[] Sayı)
            {
                if (Disposing || IsDisposed) return;

                Invoke(new Action(() =>
                {
                    string süreler = KurlarVeSüreler.Tag as string;
                    KurlarVeSüreler.Text = (süreler.DoluMu() ? süreler + Environment.NewLine : null) + Yazı;
                }));
            }
        }
        private void Yeni_İş_Girişi_FormClosed(object sender, FormClosedEventArgs e)
        {
            P_Yeni_İş_Girişi_DosyaEkleri.Close();
            P_Yeni_İş_Girişi_Epostalar.Close();
        }

        private void Müşteriler_AramaÇubuğu_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (Müşteriler_SeçimKutusu.Items.Count > 0)
                {
                    Müşteriler_SeçimKutusu.SelectedIndex = 0;
                    Müşteriler_SeçimKutusu.Focus();
                }
            }
        }
        private void Müşteriler_AramaÇubuğu_TextChanged(object sender, EventArgs e)
        {
            Ortak.GrupArayıcı(Müşteriler_SeçimKutusu, Müşteriler_Liste, Müşteriler_AramaÇubuğu.Text);

            Müşteriler_SeçimKutusu_SelectedIndexChanged(null, null);
        }
        private void Müşteriler_SeçimKutusu_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                Hastalar_AramaÇubuğu.Focus();
            }
        }
        private void Müşteriler_SeçimKutusu_SelectedIndexChanged(object sender, EventArgs e)
        {
            Hastalar_Liste.Clear();
            Ortak.GrupArayıcı(Hastalar_SeçimKutusu);
            if (Müşteriler_SeçimKutusu.SelectedIndex < 0 || !Banka.Müşteri_MevcutMu(Müşteriler_SeçimKutusu.Text)) return;

            IDepo_Eleman Talepler = Banka.Tablo_Dal(Müşteriler_SeçimKutusu.Text, SeriNoTürü == Banka.TabloTürü.ÜcretHesaplama ? Banka.TabloTürü.ÜcretHesaplama : Banka.TabloTürü.DevamEden, "Talepler");
            if (Talepler == null || Talepler.Elemanları.Length < 1) return;

            foreach (IDepo_Eleman seri_no_dalı in Talepler.Elemanları)
            {
                if (seri_no_dalı.İçiBoşOlduğuİçinSilinecek) continue;
                
                Banka.Talep_Ayıkla_SeriNoDalı(seri_no_dalı, out string SeriNo, out string Hasta, out string _, out string _, out string TeslimEdilmeTarihi);
                string ha = Hasta + " -=> (" + SeriNo + (SeriNoTürü == Banka.TabloTürü.ÜcretHesaplama ? " Ücret Hesaplama)" : (string.IsNullOrEmpty(TeslimEdilmeTarihi) ? " Devam Eden)" : " Teslim Edildi)"));
                Hastalar_Liste.Add(ha);
            }

            Ortak.GrupArayıcı(Hastalar_SeçimKutusu, Hastalar_Liste, Hastalar_AramaÇubuğu.Text);
        }

        private void Hastalar_AramaÇubuğu_TextChanged(object sender, EventArgs e)
        {
            if (SeriNo != null) Değişiklik_Yapılıyor(null, null);
            
            Ortak.GrupArayıcı(Hastalar_SeçimKutusu, Hastalar_Liste, Hastalar_AramaÇubuğu.Text);
        }
        private void Hastalar_AramaÇubuğu_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                İşTürleri_AramaÇubuğu.Focus();
            }
        }
        private void Hastalar_AramaÇubuğu_Leave(object sender, EventArgs e)
        {
            if (!Hastalar_AdVeSoyadıDüzelt.Checked) return;

            string[] dizi = Hastalar_AramaÇubuğu.Text.Trim().Split(' ');
            if (dizi.Length < 2 || dizi.Length > 3) return;

            string yeni = "";
            for (int i = 0; i < dizi.Length - 1; i++)
            {
                yeni += dizi[i][0].ToString().ToUpper() + dizi[i].Substring(1).ToLower() + " ";
            }
            yeni += dizi[dizi.Length - 1].ToUpper();

            Hastalar_AramaÇubuğu.Text = yeni;
        }
        private void Hastalar_SeçimKutusu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Hastalar_SeçimKutusu.SelectedIndex < 0 || !Hastalar_Liste.Contains(Hastalar_SeçimKutusu.Text)) return;

            DialogResult Dr;
            string hasta = Hastalar_SeçimKutusu.Text;
            int konum = Hastalar_SeçimKutusu.Text.IndexOf(" -=> (");
            if (konum < 0) return;
            Hastalar_AramaÇubuğu.Text = hasta.Remove(konum);

            string sn = hasta.Substring(konum + 6).TrimEnd(')');
            konum = sn.IndexOf(" ");
            if (konum < 0) return;

            sn = sn.Substring(0, konum);

            string soru;
            Banka.TabloTürü SeriNoTürü;
            if (hasta.EndsWith(" Devam Eden)"))
            {
                soru = "Yeni bir iş oluşturmak yerine" + Environment.NewLine +
                hasta + Environment.NewLine +
                "kaydını güncellemek ister misiniz?";

                SeriNoTürü = Banka.TabloTürü.DevamEden;
            }
            else if (hasta.EndsWith(" Teslim Edildi)"))
            {
                soru = "Seçtiğiniz hastaya ait kayıt TESLİM EDİLMİŞ olarak görünüyor." + Environment.NewLine + Environment.NewLine +
                "İşleme devam ederseniz kayıt DEVAM EDİYOR olarak işaretlenecektir." + Environment.NewLine + Environment.NewLine +
                "Yeni bir iş oluşturmak yerine" + Environment.NewLine +
                hasta + Environment.NewLine +
                "kaydını güncellemek ister misiniz?";

                SeriNoTürü = Banka.TabloTürü.TeslimEdildi;
            }
            else if (hasta.EndsWith(" Ücret Hesaplama)"))
            {
                soru = "Yeni bir iş oluşturmak yerine" + Environment.NewLine +
                hasta + Environment.NewLine +
                "kaydını güncellemek ister misiniz?";

                SeriNoTürü = Banka.TabloTürü.ÜcretHesaplama;
            }
            else return;

            Dr = MessageBox.Show(soru, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (Dr == DialogResult.No) return;

            Kaydet_TuşGörünürlüğü(false);
            Close();
            Ekranlar.ÖnYüzler.Ekle(new Yeni_İş_Girişi(sn, Müşteriler_SeçimKutusu.Text, SeriNoTürü));
        }

        private void İşTürleri_AramaÇubuğu_TextChanged(object sender, EventArgs e)
        {
            İştürü_SeçiliSatıraKopyala.Enabled = false;

            Ortak.GrupArayıcı(İşTürleri_SeçimKutusu, İşTürleri_Liste, İşTürleri_AramaÇubuğu.Text);
        }
        private void İşTürleri_AramaÇubuğu_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (İşTürleri_SeçimKutusu.Items.Count > 0)
                {
                    İşTürleri_SeçimKutusu.SelectedIndex = 0;
                    İşTürleri_SeçimKutusu.Focus();
                }
            }
        }
        private void İşTürleri_SeçimKutusu_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                İştürü_SeçiliSatıraKopyala_Click(null, null);

                İşTürleri_AramaÇubuğu.Focus();
            }
        }
        private void İşTürleri_SeçimKutusu_SelectedIndexChanged(object sender, EventArgs e)
        {
            İştürü_SeçiliSatıraKopyala.Enabled = İşTürleri_SeçimKutusu.Items.Count > 0;
        }
        private void İştürü_SeçiliSatıraKopyala_Click(object sender, EventArgs e)
        {
            var l = Tablo.SelectedRows;
            if (l == null || l.Count != 1 || İşTürleri_SeçimKutusu.SelectedIndex < 0) return;
            if (!SadeceOkunabilirSatırİseKullanıcıyaSor_DevamEt(l[0])) return;

            int konum = l[0].Index;
            l[0].Selected = false;
            if (Tablo.RowCount < konum + 2) Tablo.RowCount++;
            Tablo.Rows[konum + 1].Selected = true;

            Tablo[Tablo_İş_Türü.Index, konum].Value = İşTürleri_SeçimKutusu.Text;
            Tablo[Tablo_İş_Türü.Index, konum].ToolTipText = Banka.Ücretler_BirimÜcretMaliyet_Detaylı(Müşteriler_SeçimKutusu.Text, İşTürleri_SeçimKutusu.Text);
            
            if (((string)Tablo[Tablo_Adet.Index, konum].Value).BoşMu()) Tablo[Tablo_Adet.Index, konum].Value = "1";
            Tablo[Tablo_Adet.Index, konum].Style.BackColor = System.Drawing.Color.Salmon;
        }
        
        private void Tablo_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;

            Değişiklik_Yapılıyor(null, null);
        }
        private void Tablo_SelectionChanged(object sender, EventArgs e)
        {
            var l = Tablo.SelectedRows;
            if (l == null || l.Count > 1) return;

            foreach (DataGridViewRow Row in l)
            {
                Seçili_Satırı_Sil.Text = Row.ReadOnly ? "Seçili satırın kilidini KALDIR" : "Seçili Satırı Sil";
            }

            Dişler_GörseliniGüncelle();
        }
        private void Tablo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != Tablo_Çıkış_Tarihi.Index || SadeceOkunabilir) return;
            if (!SadeceOkunabilirSatırİseKullanıcıyaSor_DevamEt(Tablo.Rows[e.RowIndex])) return;

            if (Tablo[e.ColumnIndex, e.RowIndex].Tag != null)
            {
                DialogResult Dr = MessageBox.Show("Çıkış tarihi bilgisini kaldırmak mı istiyorsunuz?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (Dr == DialogResult.No) return;

                Tablo[e.ColumnIndex, e.RowIndex].Value = null;
                Tablo[e.ColumnIndex, e.RowIndex].Tag = null;
            }
            else
            {
                DateTime t = DateTime.Now;
                Tablo[e.ColumnIndex, e.RowIndex].Value = Banka.Yazdır_Tarih(t.Yazıya());
                Tablo[e.ColumnIndex, e.RowIndex].Tag = t;
            }
        }

        private void Notlar_KeyDown(object sender, KeyEventArgs e)
        {
            if (SadeceOkunabilir || Notlar_TarihSaatEklendi) return;
            Notlar_TarihSaatEklendi = true;

            if (Notlar.Text.DoluMu(true)) Notlar.Text += Environment.NewLine;
            Notlar.Text += DateTime.Now.ToString("dd MMM ddd") + " ";
            Notlar.Select(Notlar.Text.Length, 0);
        }

        void Dişler_GörseliniGüncelle()
        {
            List<byte> tümü = new List<byte>();
            List<byte> seçili_olan = new List<byte>();
            foreach (DataGridViewRow biri in Tablo.Rows)
            {
                byte[] şimdiki = biri.Cells[Tablo_Adet.Index].Tag as byte[];
                if (şimdiki == null || şimdiki.Length < 2) continue;

                for (int i = 1; i < şimdiki.Length; i++)
                {
                    tümü.Add(şimdiki[i]);

                    if (biri.Selected) seçili_olan.Add(şimdiki[i]);
                }
            }
            tümü.Distinct();

            foreach (Label biri in Tablo_Dişler.Controls)
            {
                if (biri.Tag == null) continue; //çoklu seçim tuşları

                if (seçili_olan.Contains((byte)biri.Tag)) biri.BackColor = System.Drawing.Color.Orange;
                else if (tümü.Contains((byte)biri.Tag)) biri.BackColor = System.Drawing.Color.Wheat;
                else biri.BackColor = System.Drawing.SystemColors.Control;
            }
        }
        private void Dişler_Değişiklik_Yapılıyor(object sender, EventArgs e)
        {
            if (Tablo.SelectedRows.Count == 0 || Tablo.SelectedRows.Count > 1 || Tablo.SelectedRows[0].IsNewRow) { return; }
            if (!SadeceOkunabilirSatırİseKullanıcıyaSor_DevamEt(Tablo.SelectedRows[0])) return;

            byte[] dizi = (byte[])Tablo.SelectedRows[0].Cells[Tablo_Adet.Index].Tag;
            byte ElleGirilenAdet = 0;
            List<byte> dizi_liste;
            if (dizi == null) dizi_liste = new List<byte>();
            else
            {
                dizi_liste = dizi.ToList();
                ElleGirilenAdet = dizi_liste[0];
                dizi_liste.RemoveAt(0);
            }

            Label lbl = (Label)sender;
            if (lbl.Tag == null)
            {
                byte başla, bitir;
                if (lbl.Name == "ustsol")
                {
                    başla = 11;
                    bitir = 18;
                }
                else if (lbl.Name == "ustsag")
                {
                    başla = 21;
                    bitir = 28;
                }
                else if(lbl.Name == "ust")
                {
                    başla = 11;
                    bitir = 28;
                }
                else if (lbl.Name == "altsol")
                {
                    başla = 41;
                    bitir = 48;
                }
                else if (lbl.Name == "altsag")
                {
                    başla = 31;
                    bitir = 38;
                }
                else //alt
                {
                    başla = 31;
                    bitir = 48;
                }

                for (byte i = başla; i <= bitir; i++)
                {
                    _Tersle_(i);
                }
            }
            else _Tersle_((byte)lbl.Tag);

            if (ElleGirilenAdet + dizi_liste.Count == 0) Tablo.SelectedRows[0].Cells[Tablo_Adet.Index].Tag = null;
            else
            {
                dizi_liste.Insert(0, ElleGirilenAdet);
                Tablo.SelectedRows[0].Cells[Tablo_Adet.Index].Tag = dizi_liste.ToArray();
            }

            Dişler_GörseliniGüncelle();
            Değişiklik_Yapılıyor(null, null);

            void _Tersle_(byte eleman_)
            {
                if (dizi_liste.Contains(eleman_)) dizi_liste.Remove(eleman_);
                else dizi_liste.Add(eleman_);
            }
        }

        #region Dosya Ekleri
        private void DosyaEkleri_Click(object sender, EventArgs e)
        {
            P_Epostalar.Visible = false;
            P_DosyaEkleri.Visible = true;

            if (P_Yeni_İş_Girişi_DosyaEkleri.P_DosyaEkleri_Liste.Items.Count > 0) P_Yeni_İş_Girişi_DosyaEkleri.P_DosyaEkleri_Liste.SelectedIndex = 0;
        }
        private void P_DosyaEkleri_Geri_Click(object sender, EventArgs e)
        {
            P_DosyaEkleri.Visible = false;

            P_DosyaEkleri_TuşunuGüncelle();
        }
        private void P_DosyaEkleri_GelenKutusunuAç_Click(object sender, EventArgs e)
        {
            P_DosyaEkleri.Visible = false;
            P_Epostalar.Visible = true;
        }
        private void P_Epostalar_Geri_Click(object sender, EventArgs e)
        {
            if (P_Yeni_İş_Girişi_Epostalar.NotlarıVeDosyaEkleriniAl(out string Yazı, out string[] DosyaEkleri))
            {
                if (Yazı.DoluMu() && !Notlar.Text.Contains(Yazı))
                {
                    Notlar_KeyDown(null, null);
                    Notlar.AppendText(Yazı);
                }

                if (DosyaEkleri != null) P_Yeni_İş_Girişi_DosyaEkleri.Ekle(DosyaEkleri);
            }

            P_Epostalar.Visible = false;
            P_DosyaEkleri.Visible = true;
        }
        void P_DosyaEkleri_TuşunuGüncelle()
        {
            DosyaEkleri.Text = "Dosya Ekleri (" + P_Yeni_İş_Girişi_DosyaEkleri.P_DosyaEkleri_Liste.Items.Count + ")";
            P_Yeni_İş_Girişi_DosyaEkleri.P_DosyaEkleri_Liste.Enabled = P_Yeni_İş_Girişi_DosyaEkleri.P_DosyaEkleri_Liste.Items.Count > 0;
            if (P_Yeni_İş_Girişi_DosyaEkleri.DeğişiklikYapıldı) Kaydet_TuşGörünürlüğü(true);
        }
        #endregion

        private void Değişiklik_Yapılıyor(object sender, EventArgs e)
        {
            if (SadeceOkunabilir) return;

            Kaydet_TuşGörünürlüğü(true);
        }
        bool SadeceOkunabilirSatırİseKullanıcıyaSor_DevamEt(DataGridViewRow Satır)
        {
            if (Satır.ReadOnly)
            {
                DialogResult Dr = MessageBox.Show((Satır.Index + 1).ToString() + ". satırdaki iş önceki döneme ait" + Environment.NewLine +
                        "Eğer kilidi kaldırılırsa halihazırdaki KABUL EDİLMİŞ BİLGİLERİ değiştirebileceksiniz." + Environment.NewLine +
                        "İlgili satırın KİLDİNİ AÇMAK istediğinize emin misiniz?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (Dr == DialogResult.No) return false;

                Satır.ReadOnly = false;
                Seçili_Satırı_Sil.Text = "Seçili Satırı Sil";
            }

            return true;
        }

        private void Seçili_Satırı_Sil_Click(object sender, EventArgs e)
        {
            var l = Tablo.SelectedRows;
            if (l == null || l.Count > 1 || l[0].Index == Tablo.RowCount - 1) return;

            if (l[0].ReadOnly) SadeceOkunabilirSatırİseKullanıcıyaSor_DevamEt(l[0]);
            else
            {
                DialogResult Dr = MessageBox.Show((l[0].Index + 1).ToString() + ". satırdaki öğeyi KALICI OLARAK SİLMEK istediğinize emin misiniz?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (Dr == DialogResult.No) return;

                Tablo.Rows.Remove(l[0]);
                Değişiklik_Yapılıyor(null, null);
            }
        }
        private void Kaydet_Click(object sender, EventArgs e)
        {
            if (!_Kaydet_()) return;

            Kaydet_TuşGörünürlüğü(false);
            Close();
        }
        private void KaydetVeEtiketiYazdır_Click(object sender, EventArgs e)
        {
            bool İşKaydıYapıldı = false;
            if (Kaydet.Enabled)
            {
                if (!_Kaydet_()) return;
                else Kaydet_TuşGörünürlüğü(false);

                İşKaydıYapıldı = true;
            }

            string sonuç = Etiketleme.YeniİşGirişi_Etiket_Üret(Müşteriler_SeçimKutusu.Text, Hastalar_AramaÇubuğu.Text, SeriNo ?? YeniKayıtİçinTutulanSeriNo, Banka.Yazdır_Tarih((string)Tablo[Tablo_Giriş_Tarihi.Index, Tablo.RowCount - 2].Value), (string)Tablo[Tablo_İş_Türü.Index, Tablo.RowCount - 2].Value, false);
            if (sonuç.DoluMu()) MessageBox.Show((İşKaydıYapıldı ? "İş kaydı yapıldı fakat y" : "Y") + "azdırma aşamasında bir sorun ile karşılaşıldı" + Environment.NewLine + Environment.NewLine + sonuç, Text);

            Close();
        }
        void Kaydet_TuşGörünürlüğü(bool Görünsün)
        {
            Kaydet.Enabled = Görünsün;
            KaydetVeEtiketiYazdır.Visible = SeriNoTürü != Banka.TabloTürü.ÜcretHesaplama;

            if (Görünsün)
            {
                KaydetVeEtiketiYazdır.Enabled = true;
                KaydetVeEtiketiYazdır.Text = "Kaydet ve Etiketi Yazdır";
            }
            else
            {
                if (SeriNo == null) KaydetVeEtiketiYazdır.Enabled = false;
                else
                {
                    KaydetVeEtiketiYazdır.Enabled = true;
                    KaydetVeEtiketiYazdır.Text = "Etiketi Yazdır ve Kapat";
                }
            }
        }
        bool _Kaydet_()
        {
            if (!Banka.Müşteri_MevcutMu(Müşteriler_SeçimKutusu.Text))
            {
                MessageBox.Show("Geçerli bir müşteri seçiniz", Text);
                Müşteriler_SeçimKutusu.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(Hastalar_AramaÇubuğu.Text))
            {
                MessageBox.Show("Hasta kutucuğu boş olmamalıdır" + Environment.NewLine + "örneğin hasta adı veya iş talep numarası yazılabilir", Text);
                Hastalar_AramaÇubuğu.Focus();
                return false;
            }
            Hastalar_AramaÇubuğu.Text = Hastalar_AramaÇubuğu.Text.Trim();

            if (!string.IsNullOrWhiteSpace(İskonto.Text))
            {
                string gecici = İskonto.Text;
                if (!Ortak.YazıyıSayıyaDönüştür(ref gecici, "İskonto kutucuğu", null, 0, 100))
                {
                    İskonto.Focus();
                    return false;
                }

                if (gecici == "0") İskonto.Text = null;
                else İskonto.Text = gecici;
            }

            Banka.Talep_Ekle_Detaylar_ Detaylar = new Banka.Talep_Ekle_Detaylar_();
            Detaylar.SeriNo = SeriNo;
            Detaylar.Müşteri = Müşteriler_SeçimKutusu.Text;
            Detaylar.Hasta = Hastalar_AramaÇubuğu.Text;
            Detaylar.İskonto = İskonto.Text;
            Detaylar.Notlar = Notlar.Text.Trim();

            DateTime t = DateTime.Now;
            Detaylar.İşTürleri = new List<string>();
            Detaylar.Ücretler = new List<string>();
            Detaylar.GirişTarihleri = new List<string>();
            Detaylar.ÇıkışTarihleri = new List<string>();
            Detaylar.Adetler = new List<byte[]>();
            for (int i = 0; i < Tablo.RowCount - 1; i++)
            {
                if (!Tablo.Rows[i].ReadOnly)
                {
                    //İş türü kontrolü
                    if (!Banka.İşTürü_MevcutMu((string)Tablo[Tablo_İş_Türü.Index, i].Value))
                    {
                        MessageBox.Show("Tablodaki " + (i + 1) + ". satırdaki \"İş Türü\" uygun değil", Text);
                        return false;
                    }

                    //Adet kontrolü
                    string gecici = (string)Tablo[Tablo_Adet.Index, i].Value;
                    if (!Ortak.YazıyıSayıyaDönüştür(ref gecici,
                        "Tablodaki adet sutununun " + (i + 1).ToString() + ". satırı",
                        "Tamsayı olmalı", 1, 255, true)) return false;
                    Tablo[Tablo_Adet.Index, i].Value = gecici;

                    //Ücret kontrolü
                    if (!string.IsNullOrWhiteSpace((string)Tablo[Tablo_Ücret.Index, i].Value))
                    {
                        gecici = (string)Tablo[Tablo_Ücret.Index, i].Value;
                        if (!Ortak.YazıyıSayıyaDönüştür(ref gecici,
                            "Tablodaki ücret sutununun " + (i + 1).ToString() + ". satırı",
                            "İçeriği 0, boş veya sıfırdan büyük olabilir", 0)) return false;

                        Tablo[Tablo_Ücret.Index, i].Value = gecici;
                    }

                    //tarih giriş
                    if (Tablo[Tablo_Giriş_Tarihi.Index , i].Tag == null)
                    {
                        Tablo[Tablo_Giriş_Tarihi.Index, i].Value = t.Yazıya();
                        Tablo[Tablo_Giriş_Tarihi.Index, i].Tag = t;

                        t = t.AddMilliseconds(2);
                    }
                }

                Detaylar.İşTürleri.Add((string)Tablo[Tablo_İş_Türü.Index, i].Value);
                Detaylar.Ücretler.Add((string)Tablo[Tablo_Ücret.Index, i].Value);
                Detaylar.GirişTarihleri.Add(((DateTime)Tablo[Tablo_Giriş_Tarihi.Index, i].Tag).Yazıya());
                Detaylar.ÇıkışTarihleri.Add(Tablo[Tablo_Çıkış_Tarihi.Index, i].Tag == null ? null : ((DateTime)Tablo[Tablo_Çıkış_Tarihi.Index, i].Tag).Yazıya());

                byte adet = Convert.ToByte(Tablo[Tablo_Adet.Index, i].Value);
                byte[] adetler = Tablo[Tablo_Adet.Index, i].Tag as byte[];
                if (adetler == null || adetler.Length < 2)
                {
                    if (adet > 1) adetler = new byte[] { adet };
                    else adetler = null;
                }
                else adetler[0] = adet;
                Detaylar.Adetler.Add(adetler);
            }

            if (Detaylar.İşTürleri.Count == 0)
            {
                MessageBox.Show("Tabloda hiç geçerli girdi bulunamadı", Text);
                return false;
            }

            //Dosya eklerinin kaydedilmesi
            Detaylar.DosyaEkleri = new List<string>();
            Detaylar.DosyaEkleri_Html_denGöster = new List<bool>();
            for (int i = 0; i < P_Yeni_İş_Girişi_DosyaEkleri.P_DosyaEkleri_Liste.Items.Count; i++)
            {
                Detaylar.DosyaEkleri.Add(P_Yeni_İş_Girişi_DosyaEkleri.P_DosyaEkleri_Liste.Items[i].ToString());
                Detaylar.DosyaEkleri_Html_denGöster.Add(P_Yeni_İş_Girişi_DosyaEkleri.P_DosyaEkleri_Liste.GetItemChecked(i));
            }

            Banka.Talep_Ekle(Detaylar, SeriNoTürü == Banka.TabloTürü.ÜcretHesaplama);
            Banka.Ayarlar_Kullanıcı(Name, "Hastalar_AdVeSoyadıDüzelt").Yaz(null, Hastalar_AdVeSoyadıDüzelt.Checked);
            Banka.Değişiklikleri_Kaydet(Kaydet);
            Ekranlar.ÖnYüzler.GüncellenenSeriNoyuİşaretle(Detaylar.SeriNo);

            P_Yeni_İş_Girişi_Epostalar.KullanılanEpostayıİşle();

            return true;
        }

        void IGüncellenenSeriNolar.KontrolEt(List<string> GüncellenenSeriNolar)
        {
            if (SeriNo.DoluMu() && GüncellenenSeriNolar.Contains(SeriNo))
            {
                Kaydet.Enabled = false;
                Close();
            }
        }
    }
}
