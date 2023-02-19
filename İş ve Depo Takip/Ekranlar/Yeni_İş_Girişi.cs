using ArgeMup.HazirKod;
using ArgeMup.HazirKod.Ekİşlemler;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip
{
    public partial class Yeni_İş_Girişi : Form
    {
        string Müşteri  = null, SeriNo = null, EkTanım = null; 
        Banka.TabloTürü SeriNoTürü = Banka.TabloTürü.DevamEden_TeslimEdildi_ÖdemeTalepEdildi_Ödendi;
        bool SadeceOkunabilir = false;
        List<string> Müşteriler_Liste = null, Hastalar_Liste = null, İşTürleri_Liste = null; 

        public Yeni_İş_Girişi()
        {
            InitializeComponent();

            Ortak.GeçiciDepolama_PencereKonumları_Oku(this);

            if (Ortak.YeniSayfaAçmaTalebi != null && Ortak.YeniSayfaAçmaTalebi.Length == 5)
            {
                //düzenleme için açılıyor
                Müşteri = Ortak.YeniSayfaAçmaTalebi[1] as string;
                SeriNo = Ortak.YeniSayfaAçmaTalebi[2] as string;
                SeriNoTürü = (Banka.TabloTürü)Ortak.YeniSayfaAçmaTalebi[3];
                EkTanım = Ortak.YeniSayfaAçmaTalebi[4] as string;
                Ortak.YeniSayfaAçmaTalebi = null;

                if (string.IsNullOrWhiteSpace(Müşteri) || string.IsNullOrWhiteSpace(SeriNo))
                {
                    Müşteri = null;
                    SeriNo = null;
                }
            }
        
            İşTürleri_SeçimKutusu.Items.Clear();
            İşTürleri_Liste = Banka.İşTürü_Listele();
            İşTürleri_SeçimKutusu.Items.AddRange(İşTürleri_Liste.ToArray());
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

            if (Müşteri == null)
            {
                Müşteriler_SeçimKutusu.Items.Clear();
                Müşteriler_Liste = Banka.Müşteri_Listele();
                Müşteriler_SeçimKutusu.Items.AddRange(Müşteriler_Liste.ToArray());

                Tablo_Giriş_Tarihi.Visible = false;
                Tablo_Çıkış_Tarihi.Visible = false;
            }
            else
            {
                Müşteriler_Liste = new List<string>();
                Müşteriler_Liste.Add(Müşteri);
                Müşteriler_AramaÇubuğu.Text = Müşteri;
                Müşteriler_SeçimKutusu.SelectedIndex = 0;
                Ayraç_Kat_1_2.SplitterDistance = (Müşteriler_AramaÇubuğu.Size.Height * 2) + (Müşteriler_AramaÇubuğu.Size.Height / 2);

                IDepo_Eleman seri_no_dalı;
                if (SeriNoTürü == Banka.TabloTürü.TeslimEdildi)
                {
                    seri_no_dalı = Banka.Tablo_Dal(Müşteri, Banka.TabloTürü.DevamEden, "Talepler/" + SeriNo);
                }
                else
                {
                    seri_no_dalı = Banka.Tablo_Dal(Müşteri, SeriNoTürü, "Talepler/" + SeriNo, false, EkTanım);
                }
                if (seri_no_dalı == null) throw new Exception(Müşteri + " / Devam Eden / Talepler / " + SeriNo + " bulunamadı");

                Müşteriler_SeçimKutusu.Enabled = false;
                Hastalar_SeçimKutusu.Enabled = false;
                switch (SeriNoTürü)
                {
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

                Banka.Talep_Ayıkla_SeriNoDalı(seri_no_dalı, out string _, out string Hasta, out string İskonto_, out string Notlar_, out string _);
                Text += " - " + SeriNo + " - " + SeriNoTürü.ToString();
                Hastalar_AramaÇubuğu.Text = Hasta;
                İskonto.Text = İskonto_;
                Notlar.Text = Notlar_;

                string hata_bilgilendirmesi = "";
                Tablo.RowCount = seri_no_dalı.Elemanları.Length + 1;
                for (int i = 0; i < seri_no_dalı.Elemanları.Length; i++)
                {
                    Banka.Talep_Ayıkla_İşTürüDalı(seri_no_dalı.Elemanları[i], out string İşTürü, out string GirişTarihi, out string ÇıkışTarihi, out string Ücret1, out string _);

                    if (!İşTürleri_Liste.Contains(İşTürü) && !SadeceOkunabilir)
                    {
                        //eskiden varolan şuanda bulunmayan bir iş türü 
                        hata_bilgilendirmesi += (i + 1) + ". satırdaki \"" + İşTürü + "\" olarak tanımlı iş türü şuanda mevcut olmadığından satır içeriği boş olarak bırakıldı" + Environment.NewLine;
                    }
                    else
                    {
                        Tablo.Rows[i].ReadOnly = true;
                        Tablo[0, i].Value = İşTürü;
                        Tablo[0, i].ToolTipText = Banka.Ücretler_BirimÜcret_Detaylı(Müşteri, İşTürü); //ücretlendirme ipucları
                    }

                    Tablo[1, i].Value = Ücret1;

                    //tarih giriş
                    Tablo[2, i].Value = Banka.Yazdır_Tarih(GirişTarihi);
                    Tablo[2, i].Tag = GirişTarihi.TarihSaate();
                    Tablo[2, i].ToolTipText = GirişTarihi;
                    
                    if (ÇıkışTarihi.DoluMu()) //tarih çıkış
                    {
                        Tablo[3, i].Value = Banka.Yazdır_Tarih(ÇıkışTarihi);
                        Tablo[3, i].Tag = ÇıkışTarihi.TarihSaate();
                        Tablo[3, i].ToolTipText = ÇıkışTarihi;
                    }
                }

                if (!string.IsNullOrEmpty(hata_bilgilendirmesi))
                {
                    MessageBox.Show(hata_bilgilendirmesi + Environment.NewLine + "Bu mesaj Notlar içerisine aktarıldı", Text);
                    Notlar.Text = hata_bilgilendirmesi + Notlar.Text;
                    Ayraç_Kat_2_3.SplitterDistance *= 2;
                }
            }
        }
        private void Yeni_İş_Girişi_Shown(object sender, EventArgs e)
        {
            Tablo.Rows[Tablo.RowCount - 1].Selected = true;

            Kaydet.Enabled = false;

            Müşteriler_AramaÇubuğu.Focus();
        }
        private void Yeni_İş_Girişi_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
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
            Müşteriler_SeçimKutusu.Items.Clear();
            Hastalar_SeçimKutusu.Items.Clear();

            Müşteriler_SeçimKutusu.Items.AddRange(Ortak.GrupArayıcı(Müşteriler_Liste, Müşteriler_AramaÇubuğu.Text));
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
            Hastalar_Liste = null;
            Hastalar_AramaÇubuğu.Text = "";
            Hastalar_SeçimKutusu.Items.Clear();
            Hastalar_SeçimKutusu.Enabled = false;
            if (!Banka.Müşteri_MevcutMu(Müşteriler_SeçimKutusu.Text)) return;

            IDepo_Eleman Talepler = Banka.Tablo_Dal(Müşteriler_SeçimKutusu.Text, Banka.TabloTürü.DevamEden, "Talepler");
            if (Talepler == null || Talepler.Elemanları.Length < 1) return;

            Hastalar_Liste = new List<string>();
            foreach (IDepo_Eleman seri_no_dalı in Talepler.Elemanları)
            {
                if (seri_no_dalı.İçiBoşOlduğuİçinSilinecek) continue;
                
                Banka.Talep_Ayıkla_SeriNoDalı(seri_no_dalı, out string SeriNo, out string Hasta, out string _, out string _, out string TeslimEdilmeTarihi);
                string ha = Hasta + " -=> (" + SeriNo + (string.IsNullOrEmpty(TeslimEdilmeTarihi) ? " Devam Eden)" : " Teslim Edildi)");
                Hastalar_Liste.Add(ha);
            }

            Hastalar_SeçimKutusu.Enabled = true;
            Hastalar_SeçimKutusu.Items.AddRange(Hastalar_Liste.ToArray());
            Hastalar_AramaÇubuğu_TextChanged(null, null);
        }

        private void Hastalar_AramaÇubuğu_TextChanged(object sender, EventArgs e)
        {
            if (Müşteri != null) Değişiklik_Yapılıyor(null, null);
            
            if (Hastalar_Liste == null) return;

            Hastalar_SeçimKutusu.Items.Clear();

            if (string.IsNullOrEmpty(Hastalar_AramaÇubuğu.Text))
            {
                Hastalar_SeçimKutusu.Items.AddRange(Hastalar_Liste.ToArray());
            }
            else
            {
                string aranan = Hastalar_AramaÇubuğu.Text.ToLower();
                Hastalar_SeçimKutusu.Items.AddRange(Hastalar_Liste.FindAll(x => x.ToLower().Contains(aranan)).ToArray());
            }
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
            if (!Hastalar_Liste.Contains(Hastalar_SeçimKutusu.Text)) return;

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
            else return;

            Dr = MessageBox.Show(soru, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (Dr == DialogResult.No) return;

            Kaydet.Enabled = false;
            Ortak.YeniSayfaAçmaTalebi = new object[] { "Yeni İş Girişi", Müşteriler_SeçimKutusu.Text, sn, SeriNoTürü, null };
            Close();
        }

        private void İşTürleri_AramaÇubuğu_TextChanged(object sender, EventArgs e)
        {
            İşTürleri_SeçimKutusu.Items.Clear();
            İştürü_SeçiliSatıraKopyala.Enabled = false;

            İşTürleri_SeçimKutusu.Items.AddRange(Ortak.GrupArayıcı(İşTürleri_Liste, İşTürleri_AramaÇubuğu.Text));
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
            if (l == null || l.Count != 1 || İşTürleri_SeçimKutusu.Text.BoşMu()) return;

            if (l[0].ReadOnly)
            {
                DialogResult Dr = MessageBox.Show((l[0].Index + 1).ToString() + ". satırdaki iş önceki döneme ait" + Environment.NewLine +
                        "Eğer kilidi kaldırılırsa halihazırdaki KABUL EDİLMİŞ BİLGİLERİ değiştirebileceksiniz." + Environment.NewLine +
                        "İlgili satırın KİLDİNİ AÇMAK istediğinize emin misiniz?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (Dr == DialogResult.No) return;

                l[0].ReadOnly = false;
            }

            int konum = l[0].Index;
            l[0].Selected = false;
            if (Tablo.RowCount < konum + 2) Tablo.RowCount++;
            Tablo.Rows[konum + 1].Selected = true;

            Tablo[0, konum].Value = İşTürleri_SeçimKutusu.Text;
            Tablo[0, konum].ToolTipText = Banka.Ücretler_BirimÜcret_Detaylı(Müşteriler_SeçimKutusu.Text, İşTürleri_SeçimKutusu.Text);
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
        }
        private void Tablo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != 3 || SadeceOkunabilir) return;

            if (Tablo.Rows[e.RowIndex].ReadOnly)
            {
                DialogResult Dr = MessageBox.Show((e.RowIndex + 1).ToString() + ". satırdaki iş önceki döneme ait" + Environment.NewLine +
                        "Eğer kilidi kaldırılırsa halihazırdaki KABUL EDİLMİŞ BİLGİLERİ değiştirebileceksiniz." + Environment.NewLine +
                        "İlgili satırın KİLDİNİ AÇMAK istediğinize emin misiniz?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (Dr == DialogResult.No) return;

                Tablo.Rows[e.RowIndex].ReadOnly = false;
            }

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

        private void Değişiklik_Yapılıyor(object sender, EventArgs e)
        {
            if (SadeceOkunabilir) return;

            Kaydet.Enabled = true;
        }

        private void Seçili_Satırı_Sil_Click(object sender, EventArgs e)
        {
            var l = Tablo.SelectedRows;
            if (l == null || l.Count > 1 || l[0].Index == Tablo.RowCount - 1) return;

            if (l[0].ReadOnly)
            {
                DialogResult Dr = MessageBox.Show((l[0].Index + 1).ToString() + ". satırdaki iş önceki döneme ait" + Environment.NewLine +
                        "Eğer kilidi kaldırılırsa halihazırdaki KABUL EDİLMİŞ BİLGİLERİ değiştirebileceksiniz." + Environment.NewLine +
                        "İlgili satırın KİLDİNİ AÇMAK istediğinize emin misiniz?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (Dr == DialogResult.No) return;

                l[0].ReadOnly = false;
                Seçili_Satırı_Sil.Text = "Seçili Satırı Sil";
            }
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
            if (!Banka.Müşteri_MevcutMu(Müşteriler_SeçimKutusu.Text))
            {
                MessageBox.Show("Geçerli bir müşteri seçiniz", Text);
                Müşteriler_SeçimKutusu.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(Hastalar_AramaÇubuğu.Text))
            {
                MessageBox.Show("Hasta kutucuğu boş olmamalıdır" + Environment.NewLine + "örneğin hasta adı veya iş talep numarası yazılabilir", Text);
                Hastalar_AramaÇubuğu.Focus();
                return;
            }
            Hastalar_AramaÇubuğu.Text = Hastalar_AramaÇubuğu.Text.Trim();

            if (!string.IsNullOrWhiteSpace(İskonto.Text))
            {
                string gecici = İskonto.Text;
                if (!Ortak.YazıyıSayıyaDönüştür(ref gecici, "İskonto kutucuğu", null, 0, 100))
                {
                    İskonto.Focus();
                    return;
                }

                if (gecici == "0") İskonto.Text = null;
                else İskonto.Text = gecici;
            }
            
            DateTime t = DateTime.Now;
            List<string> it_leri = new List<string>();
            List<string> ücret_ler = new List<string>();
            List<string> giriş_tarih_ler = new List<string>();
            List<string> çıkış_tarih_ler = new List<string>();
            for (int i = 0; i < Tablo.RowCount - 1; i++)
            {
                if (!Tablo.Rows[i].ReadOnly)
                {
                    //İş türü kontrolü
                    if (!Banka.İşTürü_MevcutMu((string)Tablo[0, i].Value))
                    {
                        MessageBox.Show("Tablodaki " + (i + 1) + ". satırdaki \"İş Türü\" uygun değil", Text);
                        return;
                    }

                    //Ücret kontrolü
                    if (!string.IsNullOrWhiteSpace((string)Tablo[1, i].Value))
                    {
                        string gecici = (string)Tablo[1, i].Value;
                        if (!Ortak.YazıyıSayıyaDönüştür(ref gecici, 
                            "Tablodaki ücret sutununun " + (i + 1).ToString() + ". satırı",
                            "İçeriği 0, boş veya sıfırdan büyük olabilir", 0)) return;

                        Tablo[1, i].Value = gecici;
                    }

                    //tarih giriş
                    if (Tablo[2, i].Tag == null)
                    {
                        Tablo[2, i].Value = t.Yazıya();
                        Tablo[2, i].Tag = t;

                        t = t.AddMilliseconds(2);
                    }
                }

                it_leri.Add((string)Tablo[0, i].Value);
                ücret_ler.Add(((string)Tablo[1, i].Value));
                giriş_tarih_ler.Add(((DateTime)Tablo[2, i].Tag).Yazıya());
                çıkış_tarih_ler.Add(Tablo[3, i].Tag == null ? null : ((DateTime)Tablo[3, i].Tag).Yazıya());
            }

            if (it_leri.Count == 0)
            {
                MessageBox.Show("Tabloda hiç geçerli girdi bulunamadı", Text);
                return;
            }

            Banka.Talep_Ekle(Müşteriler_SeçimKutusu.Text, Hastalar_AramaÇubuğu.Text, İskonto.Text, Notlar.Text.Trim(), it_leri, ücret_ler, giriş_tarih_ler, çıkış_tarih_ler, SeriNo);
            Banka.Ayarlar_Kullanıcı(Name, "Hastalar_AdVeSoyadıDüzelt").Yaz(null, Hastalar_AdVeSoyadıDüzelt.Checked);
            Banka.Değişiklikleri_Kaydet(Kaydet);

            Kaydet.Enabled = false;
            Close();
        }
    }
}
