using ArgeMup.HazirKod;
using ArgeMup.HazirKod.Ekİşlemler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip.Ekranlar
{
    public partial class Yeni_İş_Girişi : Form, IGüncellenenSeriNolar
    {
        readonly string SeriNo = null, YeniKayıtİçinTutulanSeriNo = null;
        readonly Banka.TabloTürü SeriNoTürü = Banka.TabloTürü.DevamEden_TeslimEdildi_ÖdemeTalepEdildi_Ödendi;
        bool SadeceOkunabilir = false, Notlar_TarihSaatEklendi = false;
		List<string> Müşteriler_Liste = null, Müşteriler_AltGrup_Liste = new List<string>(), Hastalar_Liste = new List<string>();
        Yeni_İş_Girişi_DosyaEkleri P_Yeni_İş_Girişi_DosyaEkleri = new Yeni_İş_Girişi_DosyaEkleri();
        Yeni_İş_Girişi_Epostalar P_Yeni_İş_Girişi_Epostalar = new Yeni_İş_Girişi_Epostalar();

        public Yeni_İş_Girişi(string SeriNo = null, string Müşteri = null, Banka.TabloTürü SeriNoTürü = Banka.TabloTürü.DevamEden_TeslimEdildi_ÖdemeTalepEdildi_Ödendi, string EkTanım = null)
        {
            InitializeComponent();

            if (string.IsNullOrWhiteSpace(SeriNo))
            {
                SeriNo = null;
            }
            else
            {
                this.SeriNo = SeriNo;
            }
            this.SeriNoTürü = SeriNoTürü;

            List<string> iş_tür_leri = Banka.İşTürü_Listele();
            string ipucu = "Arama Çubuğu";
            if (iş_tür_leri.Count > 0)
            {
                ipucu += Environment.NewLine + Environment.NewLine + "Alttaki şekilde arattırabilirsiniz." + Environment.NewLine + Environment.NewLine + iş_tür_leri[0] + Environment.NewLine + Environment.NewLine;
                string[] d = iş_tür_leri[0].Trim().ToLower().Split(' ');
                foreach (string dd in d)
                {
                    string ddd = dd.Trim();
                    if (ddd.BoşMu(true)) continue;
                    if (ddd.Length > 2) ipucu += ddd.Substring(0, 2) + " ";
                    else ipucu += ddd + " ";
                }
            }
            Liste_İşTürleri.Başlat(null, iş_tür_leri, "İş Türleri" + Environment.NewLine + ipucu, Banka.ListeKutusu_Ayarlar(true, false));

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
                ArgeMup.HazirKod.Ekranlar.ListeKutusu.Filtrele(Müşteriler_SeçimKutusu, Müşteriler_Liste);
                ArgeMup.HazirKod.Ekranlar.ListeKutusu.Filtrele(Hastalar_SeçimKutusu);

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
                        Müşteriler_AltGrup_AramaÇubuğu.ReadOnly = true;
                        Müşteriler_AltGrup_SeçimKutusu.Enabled = false;
                        Hastalar_AramaÇubuğu.ReadOnly = true;
                        Hastalar_AdVeSoyadıDüzelt.Enabled = false;
                        İskonto.ReadOnly = true;
                        Notlar.ReadOnly = true;
                        Ayraç_Kat_3_SolSağ.Panel1Collapsed = true;
                        Tablo.ReadOnly = true;
                        Seçili_Satırı_Sil.Enabled = false;
                        break;
                }

                Banka.Talep_Ayıkla_SeriNoDalı(detaylar.SeriNoDalı, out _, out string Hasta_, out string İskonto_, out string Notlar_, out _, out string AltGrup_);
                Text += " - " + SeriNo + " - " + SeriNoTürü.ToString();
                Hastalar_AramaÇubuğu.Text = Hasta_;
                İskonto.Text = İskonto_;
                Notlar.Text = Notlar_;

                if (Müşteriler_AltGrup_Liste.Count > 0) Müşteriler_AltGrup_SeçimKutusu.Text = AltGrup_;
                
                string hata_bilgilendirmesi = "";
                Tablo.RowCount = detaylar.SeriNoDalı.Elemanları.Length + 1;
                for (int i = 0; i < detaylar.SeriNoDalı.Elemanları.Length; i++)
                {
                    Banka.Talep_Ayıkla_İşTürüDalı(detaylar.SeriNoDalı.Elemanları[i], out string İşTürü, out string GirişTarihi, out string ÇıkışTarihi, out string Ücret1, out string _, out byte[] Kullanım_AdetVeKonum);

                    if (!iş_tür_leri.Contains(İşTürü) && !SadeceOkunabilir)
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

                Ayraç_Detaylar_EtiketSayısı.Panel2Collapsed = SadeceOkunabilir;

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

                if (ÖnYüzler.ShiftTuşunaBasılıyor)
                {
                    Sürümler.Visible = Yeni_İş_Girişi_Sürümler.Varmı(detaylar.SeriNoDalı);
                    Sürümler.Tag = detaylar.SeriNoDalı;
                }
            }

            if (SeriNoTürü == Banka.TabloTürü.ÜcretHesaplama)
            {
                KurlarVeSüreler.BackColor = System.Drawing.Color.Violet;
                Ayraç_Detaylar_EtiketSayısı.Panel2Collapsed = true;
            }

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

            if (SeriNo == null) Müşteriler_AramaÇubuğu.Focus();
            else Liste_İşTürleri.Odaklan();

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
            ArgeMup.HazirKod.Ekranlar.ListeKutusu.Filtrele(Müşteriler_SeçimKutusu, Müşteriler_Liste, Müşteriler_AramaÇubuğu.Text);

            Müşteriler_SeçimKutusu_SelectedIndexChanged(null, null);
        }
        private void Müşteriler_SeçimKutusu_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (Müşteriler_AltGrup.Visible) Müşteriler_AltGrup_AramaÇubuğu.Focus();
                else Hastalar_AramaÇubuğu.Focus();
            }
        }
        private void Müşteriler_SeçimKutusu_SelectedIndexChanged(object sender, EventArgs e)
        {
            GarantiKapsamındaOlabilir.Visible = false;
            Hastalar_Liste.Clear();
            ArgeMup.HazirKod.Ekranlar.ListeKutusu.Filtrele(Hastalar_SeçimKutusu);
            Müşteriler_AltGrup.Visible = false;
            if (Müşteriler_SeçimKutusu.SelectedIndex < 0 || !Banka.Müşteri_MevcutMu(Müşteriler_SeçimKutusu.Text)) return;

            Müşteriler_AltGrup_Liste = Banka.Müşteri_AltGrup_Listele(Müşteriler_SeçimKutusu.Text);
            if (Müşteriler_AltGrup_Liste.Count > 0)
            {
                ArgeMup.HazirKod.Ekranlar.ListeKutusu.Filtrele(Müşteriler_AltGrup_SeçimKutusu, Müşteriler_AltGrup_Liste, Müşteriler_AltGrup_AramaÇubuğu.Text);
                Müşteriler_AltGrup.Visible = true;
            }

            IDepo_Eleman Talepler = Banka.Tablo_Dal(Müşteriler_SeçimKutusu.Text, SeriNoTürü == Banka.TabloTürü.ÜcretHesaplama ? Banka.TabloTürü.ÜcretHesaplama : Banka.TabloTürü.DevamEden, "Talepler");
            if (Talepler == null || Talepler.Elemanları.Length < 1) return;

            foreach (IDepo_Eleman seri_no_dalı in Talepler.Elemanları)
            {
                if (seri_no_dalı.İçiBoşOlduğuİçinSilinecek) continue;
                
                Banka.Talep_Ayıkla_SeriNoDalı(seri_no_dalı, out string SeriNo, out string Hasta, out string _, out string _, out string TeslimEdilmeTarihi, out _);
                string ha = Hasta + " -=> (" + SeriNo + (SeriNoTürü == Banka.TabloTürü.ÜcretHesaplama ? " Ücret Hesaplama)" : (string.IsNullOrEmpty(TeslimEdilmeTarihi) ? " Devam Eden)" : " Teslim Edildi)"));
                Hastalar_Liste.Add(ha);
            }

            ArgeMup.HazirKod.Ekranlar.ListeKutusu.Filtrele(Hastalar_SeçimKutusu, Hastalar_Liste, Hastalar_AramaÇubuğu.Text);
        }

        private void Müşteriler_AltGrup_AramaÇubuğu_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (Müşteriler_AltGrup_SeçimKutusu.Items.Count > 0)
                {
                    Müşteriler_AltGrup_SeçimKutusu.SelectedIndex = 0;
                    Müşteriler_AltGrup_SeçimKutusu.Focus();
                }
            }
        }
        private void Müşteriler_AltGrup_AramaÇubuğu_TextChanged(object sender, EventArgs e)
        {
            if (SadeceOkunabilir) return;

            ArgeMup.HazirKod.Ekranlar.ListeKutusu.Filtrele(Müşteriler_AltGrup_SeçimKutusu, Müşteriler_AltGrup_Liste, Müşteriler_AltGrup_AramaÇubuğu.Text);
        }
        private void Müşteriler_AltGrup_SeçimKutusu_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                Hastalar_AramaÇubuğu.Focus();
            }
        }
        private void Müşteriler_AltGrup_SeçimKutusu_SelectedIndexChanged(object sender, EventArgs e)
        {
            Müşteriler_AltGrup_AramaÇubuğu.Text = Müşteriler_AltGrup_SeçimKutusu.SelectedIndex >= 0 ? Müşteriler_AltGrup_SeçimKutusu.Text : null;

            Değişiklik_Yapılıyor(null, null);
        }

        private void Hastalar_AramaÇubuğu_TextChanged(object sender, EventArgs e)
        {
            if (SeriNo != null) Değişiklik_Yapılıyor(null, null);

            ArgeMup.HazirKod.Ekranlar.ListeKutusu.Filtrele(Hastalar_SeçimKutusu, Hastalar_Liste, Hastalar_AramaÇubuğu.Text);
            GarantiKapsamındaOlabilir.Visible = false;
        }
        private void Hastalar_AramaÇubuğu_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                Liste_İşTürleri.Odaklan();
            }
        }
        private void Hastalar_AramaÇubuğu_Leave(object sender, EventArgs e)
        {
            if (Hastalar_AdVeSoyadıDüzelt.Checked)
            {
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

            if (GarantiKapsamındaOlabilir.Tag != null)
            {
                Tüm_İşler ti = GarantiKapsamındaOlabilir.Tag as Tüm_İşler;
                if (ti != null) ti.Dispose();

                GarantiKapsamındaOlabilir.Tag = null;
            }
            Hastalar_Hasta_GarantiKapsamındaMı(_İşlemSonucu_);
            void _İşlemSonucu_(bool _EvetOlabilir_, Form _TümİşlerÖrneği_)
            {
                Invoke(new Action(() =>
                {
                    GarantiKapsamındaOlabilir.Visible = _EvetOlabilir_;
                    GarantiKapsamındaOlabilir.Tag = _TümİşlerÖrneği_;
                }));
            }
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
        private void Hastalar_GarantiKapsamındaOlabilir_Click(object sender, EventArgs e)
        {
            GarantiKapsamındaOlabilir.Enabled = false;
            if (GarantiKapsamındaOlabilir.Tag == null)
            {
                Hastalar_Hasta_GarantiKapsamındaMı(_İşlemSonucu_);
                return;

                void _İşlemSonucu_(bool _EvetOlabilir_, Form _TümİşlerÖrneği_)
                {
                    Invoke(new Action(() =>
                    {
                        GarantiKapsamındaOlabilir.Visible = _EvetOlabilir_;
                        GarantiKapsamındaOlabilir.Tag = _TümİşlerÖrneği_;

                        if (GarantiKapsamındaOlabilir.Tag != null) Hastalar_GarantiKapsamındaOlabilir_Click(null, null);
                    }));
                }
            }

            ÖnYüzler.Ekle(GarantiKapsamındaOlabilir.Tag as Form);
            GarantiKapsamındaOlabilir.Tag = null;
            GarantiKapsamındaOlabilir.Enabled = true;
        }
        void Hastalar_Hasta_GarantiKapsamındaMı(Action<bool, Form> İşlemSonucu)
        {
            if (İşlemSonucu == null) throw new ArgumentNullException("İşlemSonucu boş olamaz");

            string müşteri_adı = Müşteriler_SeçimKutusu.Text;

            if (SeriNo == null &&                           //sadece yeni iş girişi
                Hastalar_SeçimKutusu.Items.Count == 0 &&    //birebir sonuç bulunamadı
                Banka.Müşteri_MevcutMu(müşteri_adı) && 
                Hastalar_AramaÇubuğu.Text.DoluMu(true))
            {
                int GarantiKontrolSüresi_Gün = Banka.Tablo_Dal(null, Banka.TabloTürü.Takvim, "Erteleme Süresi", true).Oku_TamSayı("Garanti Kontrol Süresi", 90);
                if (GarantiKontrolSüresi_Gün > 0)
                {
                    System.Threading.Tasks.Task.Run(() =>
                    {
                        DateTime GarantiKontrolSüresi_Bitiş = DateTime.Now;
                        DateTime GarantiKontrolSüresi_Başlangıç = GarantiKontrolSüresi_Bitiş.AddDays(GarantiKontrolSüresi_Gün * -1);

                        Tüm_İşler ti = null;
                        Invoke(new Action(() =>
                        {
                            ti = new Tüm_İşler();
                        }));

                        int adt = ti.AramaPenceresiniAç(GarantiKontrolSüresi_Başlangıç, GarantiKontrolSüresi_Bitiş, müşteri_adı, Hastalar_AramaÇubuğu.Text, true, true);

                        if (Disposing || IsDisposed || adt == 0)
                        {
                            ti.Dispose();
                            ti = null;
                            adt = 0;
                        }

                        İşlemSonucu(adt > 0, ti);
                    });

                    return;
                }
            }
           
            İşlemSonucu(false, null);
        }

        private void Liste_İşTürleri_DoubleClick(object sender, EventArgs e)
        {
            İştürü_SeçiliSatıraKopyala_Click(null, null);
        }
        private void İştürü_SeçiliSatıraKopyala_Click(object sender, EventArgs e)
        {
            string İt_adı = Liste_İşTürleri.SeçilenEleman_Adı;
            var l = Tablo.SelectedRows;
            if (l == null || l.Count != 1 || İt_adı.BoşMu()) return;
            if (!SadeceOkunabilirSatırİseKullanıcıyaSor_DevamEt(l[0])) return;

            int konum = l[0].Index;
            l[0].Selected = false;
            if (Tablo.RowCount < konum + 2) Tablo.RowCount++;
            Tablo.Rows[konum + 1].Selected = true;

            Tablo[Tablo_İş_Türü.Index, konum].Value = İt_adı;
            Tablo[Tablo_İş_Türü.Index, konum].ToolTipText = Banka.Ücretler_BirimÜcretMaliyet_Detaylı(Müşteriler_SeçimKutusu.Text, İt_adı);
            
            if (((string)Tablo[Tablo_Adet.Index, konum].Value).BoşMu()) Tablo[Tablo_Adet.Index, konum].Value = "1";
            Tablo[Tablo_Adet.Index, konum].Style.BackColor = System.Drawing.Color.Salmon;

            Liste_İşTürleri.Odaklan(true);
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

        private void EtiketSayısı_Enter(object sender, EventArgs e)
        {
            NumericUpDown nud = sender as NumericUpDown;
            if (nud.Value == 0) nud.Value = 1;
        }

        List<byte> Dişler_Tümü()
        {
            List<byte> tümü = new List<byte>();
            foreach (DataGridViewRow biri in Tablo.Rows)
            {
                byte[] şimdiki = biri.Cells[Tablo_Adet.Index].Tag as byte[];
                if (şimdiki == null || şimdiki.Length < 2) continue;

                for (int i = 1; i < şimdiki.Length; i++)
                {
                    tümü.Add(şimdiki[i]);
                }
            }

            return tümü.Distinct().ToList();
        }
        void Dişler_GörseliniGüncelle()
        {
            List<byte> tümü = Dişler_Tümü();

            List<byte> seçili_olan = new List<byte>();
            foreach (DataGridViewRow biri in Tablo.Rows)
            {
                if (!biri.Selected) continue;

                byte[] şimdiki = biri.Cells[Tablo_Adet.Index].Tag as byte[];
                if (şimdiki == null || şimdiki.Length < 2) continue;

                for (int i = 1; i < şimdiki.Length; i++)
                {
                    seçili_olan.Add(şimdiki[i]);
                }
            }

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

        private void AçıklamaEtiketi_Click(object sender, EventArgs e)
        {
            Yeni_İş_Girişi_Açıklama açklm = new Yeni_İş_Girişi_Açıklama();
            açklm.FormClosed += Açklm_FormClosed;
            ÖnYüzler.Ekle(açklm);

            void Açklm_FormClosed(object _sender_, FormClosedEventArgs _e_)
            {
                //Sadece okunabilir değil ise buraya geliyor
                string Açıklama = açklm.YazdırVeKaydet.Tag as string;
                if (Açıklama.BoşMu(true)) return;

                bool KaydetTuşuEtkin = ÖnYüzler_Kaydet.Enabled;
                Notlar.Text = Notlar.Text.Trim();
                if (Notlar.Text.DoluMu(true)) Notlar.Text += Environment.NewLine;
                Notlar.Text += DateTime.Now.ToString("dd MMM ddd") + " " + Açıklama;

                string müşteri_adı = Müşteriler_SeçimKutusu.Text, SonİşTürü = null, SonGirişTarihi = null;

                if (SeriNo.DoluMu(true) && müşteri_adı.DoluMu(true))
                {
                    Banka.Talep_Bul_Detaylar_ detaylar = Banka.Talep_Bul(SeriNo, müşteri_adı);
                    if (detaylar != null)
                    {
                        detaylar.SeriNoDalı[2] = Notlar.Text;
                        Banka.Değişiklikleri_Kaydet(AçıklamaEtiketi);
                        Ekranlar.ÖnYüzler.GüncellenenSeriNoyuİşaretle(SeriNo);

                        if (!KaydetTuşuEtkin) Kaydet_TuşGörünürlüğü(false);
                        Banka.Talep_Ayıkla_İşTürüDalı(detaylar.SeriNoDalı.Elemanları[detaylar.SeriNoDalı.Elemanları.Length - 1], out SonİşTürü, out SonGirişTarihi, out _, out _, out _, out _);
                    }
                }

                string sonuç = Ayarlar_Etiketleme.YeniİşGirişi_Etiket_Üret(Ayarlar_Etiketleme.YeniİşGirişi_Etiketi.Açıklama, 1, müşteri_adı, Hastalar_AramaÇubuğu.Text, SeriNo, SonGirişTarihi, SonİşTürü, false, Açıklama, null, null);
                if (sonuç.DoluMu()) MessageBox.Show("Açıklama etiketinin yazdırılması aşamasında bir sorun ile karşılaşıldı" + Environment.NewLine + Environment.NewLine + sonuç, "Açıklama Etiketi");
            }
        }

        private void ÖnYüzler_Kaydet_Click(object sender, EventArgs e)
        {
            if (!_Kaydet_()) return;

            Kaydet_TuşGörünürlüğü(false);
            Close();
        }
        private void KaydetVeEtiketiYazdır_Click(object sender, EventArgs e)
        {
            bool İşKaydıYapıldı = false;
            if (ÖnYüzler_Kaydet.Enabled)
            {
                if (!_Kaydet_()) return;
                else Kaydet_TuşGörünürlüğü(false);

                İşKaydıYapıldı = true;
            }

            if (EtiketSayısı_Kayıt.Value == 0 && EtiketSayısı_Acil.Value == 0) EtiketSayısı_Kayıt.Value = 1;

            List<byte> TümDişler = Dişler_Tümü();

            if (EtiketSayısı_Kayıt.Value > 0)
            {
                string sonuç = Ayarlar_Etiketleme.YeniİşGirişi_Etiket_Üret(Ayarlar_Etiketleme.YeniİşGirişi_Etiketi.Kayıt, (int)EtiketSayısı_Kayıt.Value, Müşteriler_SeçimKutusu.Text, Hastalar_AramaÇubuğu.Text, SeriNo ?? YeniKayıtİçinTutulanSeriNo, Banka.Yazdır_Tarih((string)Tablo[Tablo_Giriş_Tarihi.Index, Tablo.RowCount - 2].Value), (string)Tablo[Tablo_İş_Türü.Index, Tablo.RowCount - 2].Value, false, null, TümDişler, Notlar.Text);
                if (sonuç.DoluMu()) MessageBox.Show((İşKaydıYapıldı ? "İş kaydı yapıldı fakat y" : "Y") + "azdırma aşamasında bir sorun ile karşılaşıldı" + Environment.NewLine + Environment.NewLine + sonuç, Text);
            }

            if (EtiketSayısı_Acil.Value > 0)
            {
                string sonuç = Ayarlar_Etiketleme.YeniİşGirişi_Etiket_Üret(Ayarlar_Etiketleme.YeniİşGirişi_Etiketi.Acilİş, (int)EtiketSayısı_Acil.Value, Müşteriler_SeçimKutusu.Text, Hastalar_AramaÇubuğu.Text, SeriNo ?? YeniKayıtİçinTutulanSeriNo, Banka.Yazdır_Tarih((string)Tablo[Tablo_Giriş_Tarihi.Index, Tablo.RowCount - 2].Value), (string)Tablo[Tablo_İş_Türü.Index, Tablo.RowCount - 2].Value, false, null, TümDişler, Notlar.Text);
                if (sonuç.DoluMu()) MessageBox.Show((İşKaydıYapıldı ? "İş kaydı yapıldı fakat y" : "Y") + "azdırma aşamasında bir sorun ile karşılaşıldı" + Environment.NewLine + Environment.NewLine + sonuç, Text);
            }

            if (İşKağıdı.Checked)
            {
                string sonuç = Ayarlar_Etiketleme.YeniİşGirişi_Etiket_Üret(Ayarlar_Etiketleme.YeniİşGirişi_Etiketi.İşKağıdı, (int)EtiketSayısı_Acil.Value, Müşteriler_SeçimKutusu.Text, Hastalar_AramaÇubuğu.Text, SeriNo ?? YeniKayıtİçinTutulanSeriNo, Banka.Yazdır_Tarih((string)Tablo[Tablo_Giriş_Tarihi.Index, Tablo.RowCount - 2].Value), (string)Tablo[Tablo_İş_Türü.Index, Tablo.RowCount - 2].Value, false, null, TümDişler, Notlar.Text);
                if (sonuç.DoluMu()) MessageBox.Show((İşKaydıYapıldı ? "İş kaydı yapıldı fakat y" : "Y") + "azdırma aşamasında bir sorun ile karşılaşıldı" + Environment.NewLine + Environment.NewLine + sonuç, Text);
            }

            Close();
        }

        private void Sürümler_Click(object sender, EventArgs e)
        {
            ÖnYüzler.Ekle(new Yeni_İş_Girişi_Sürümler(Sürümler.Tag as IDepo_Eleman));
        }

        void Kaydet_TuşGörünürlüğü(bool Görünsün)
        {
            ÖnYüzler_Kaydet.Enabled = Görünsün;
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
           
            string müşteri_altgrup = null;
            if (Müşteriler_AltGrup.Visible && Müşteriler_AltGrup_Liste.Count > 0 && Müşteriler_AltGrup_AramaÇubuğu.Text.DoluMu(true))
            {
                if (!Banka.Müşteri_AltGrup_MevcutMu(Müşteriler_SeçimKutusu.Text, Müşteriler_AltGrup_AramaÇubuğu.Text))
                {
                    Banka.Müşteri_AltGrup_Ekle(Müşteriler_SeçimKutusu.Text, Müşteriler_AltGrup_AramaÇubuğu.Text);
                }

                müşteri_altgrup = Müşteriler_AltGrup_AramaÇubuğu.Text;
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
            Detaylar.Müşteri_AltGrubu = müşteri_altgrup;
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

            //acil iş olarak kaydedilmesi
            if (EtiketSayısı_Acil.Value > 0)
            {
                IDepo_Eleman tablo_hatırlatıcılar_SeriNoluİş = Banka.Tablo_Dal(null, Banka.TabloTürü.Takvim, "Hatırlatıcılar SeriNoluİş", true);
                tablo_hatırlatıcılar_SeriNoluİş.Yaz(SeriNo ?? YeniKayıtİçinTutulanSeriNo, t);
            }

            Banka.Talep_Ekle(Detaylar, SeriNoTürü == Banka.TabloTürü.ÜcretHesaplama);
            Banka.Ayarlar_Kullanıcı(Name, "Hastalar_AdVeSoyadıDüzelt").Yaz(null, Hastalar_AdVeSoyadıDüzelt.Checked);
            Banka.Değişiklikleri_Kaydet(ÖnYüzler_Kaydet);
            Ekranlar.ÖnYüzler.GüncellenenSeriNoyuİşaretle(Detaylar.SeriNo);

            P_Yeni_İş_Girişi_Epostalar.KullanılanEpostayıİşle();
            return true;
        }

        void IGüncellenenSeriNolar.KontrolEt(List<string> GüncellenenSeriNolar)
        {
            if (SeriNo.DoluMu() && GüncellenenSeriNolar.Contains(SeriNo))
            {
                ÖnYüzler_Kaydet.Enabled = false;
                Close();
            }
        }
    }
}
