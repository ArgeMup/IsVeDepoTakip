﻿using ArgeMup.HazirKod;
using ArgeMup.HazirKod.Ekİşlemler;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip
{
    public partial class Yeni_İş_Girişi : Form
    {
        string Müşteri  = null, SeriNo = null;
        List<string> Müşteriler_Liste = null, Hastalar_Liste = null, İşTürleri_Liste = null; 

        public Yeni_İş_Girişi()
        {
            InitializeComponent();

            Ortak.GeçiciDepolama_PencereKonumları_Oku(this);

            if (Ortak.YeniSayfaAçmaTalebi != null)
            {
                //düzenleme için açılıyor
                Müşteri = (string)Ortak.YeniSayfaAçmaTalebi[1];
                SeriNo = (string)Ortak.YeniSayfaAçmaTalebi[2];
                Ortak.YeniSayfaAçmaTalebi = null;

                if (string.IsNullOrEmpty(Müşteri) || string.IsNullOrEmpty(SeriNo))
                {
                    Müşteri = null;
                    SeriNo = null;
                }
            }
        
            İşTürleri_SeçimKutusu.Items.Clear();
            İşTürleri_Liste = Banka.İşTürü_Listele();
            İşTürleri_SeçimKutusu.Items.AddRange(İşTürleri_Liste.ToArray());

            if (Müşteri == null)
            {
                Müşteriler_SeçimKutusu.Items.Clear();
                Müşteriler_Liste = Banka.Müşteri_Listele();
                Müşteriler_SeçimKutusu.Items.AddRange(Müşteriler_Liste.ToArray());

                Tablo_Kabul_Tarihi.Visible = false;
                Tablo_Çıkış_Tarihi.Visible = false;
            }
            else
            {
                Müşteriler_Liste = new List<string>();
                Müşteriler_Liste.Add(Müşteri);
                Müşteriler_AramaÇubuğu.Text = Müşteri;
                Müşteriler_SeçimKutusu.SelectedIndex = 0;
                Ayraç_Kat_1_2.SplitterDistance = (Müşteriler_AramaÇubuğu.Size.Height * 2) + (Müşteriler_AramaÇubuğu.Size.Height / 2);

                IDepo_Eleman elm_ları = Banka.Tablo_Dal(Müşteri, Banka.TabloTürü.DevamEden, "Talepler/" + SeriNo);
                if (elm_ları == null) throw new Exception(Müşteri + " / Devam Eden / Talepler / " + SeriNo + " bulunamadı");

                Text += " - " + elm_ları.Adı;
                Hastalar_AramaÇubuğu.Text = elm_ları[0];
                İskonto.Text = elm_ları[1];
                Notlar.Text = elm_ları[2];

                string hata_bilgilendirmesi = "";
                Tablo.RowCount = elm_ları.Elemanları.Length + 1;
                for (int i = 0; i < elm_ları.Elemanları.Length; i++)
                {
                    if (!İşTürleri_Liste.Contains(elm_ları.Elemanları[i][0]))
                    {
                        //eskiden varolan şuanda bulunmayan bir iş türü 
                        hata_bilgilendirmesi += (i + 1) + ". satırdaki \"" + elm_ları.Elemanları[i][0] + "\" olarak tanımlı iş türü şuanda mevcut olmadığından satır içeriği boş olarak bırakıldı" + Environment.NewLine;
                    }
                    else
                    {
                        Tablo.Rows[i].ReadOnly = true;
                        Tablo[0, i].Value = elm_ları.Elemanları[i][0]; //iş türü
                        Tablo[0, i].ToolTipText = Banka.Ücretler_BirimÜcret_Detaylı(Müşteri, elm_ları.Elemanları[i][0]); //ücretlendirme ipucları
                    }

                    Tablo[1, i].Value = elm_ları.Elemanları[i][2]; //ücret
                    if (elm_ları.Elemanları[i][1].DoluMu()) //tarih kabul
                    {
                        Tablo[2, i].Value = Banka.Yazdır_Tarih(elm_ları.Elemanları[i][1]);
                        Tablo[2, i].Tag = elm_ları.Elemanları[i].Oku_TarihSaat(null, default, 1);
                    }
                    if (elm_ları.Elemanları[i][4].DoluMu()) //tarih çıkış
                    {
                        Tablo[3, i].Value = Banka.Yazdır_Tarih(elm_ları.Elemanları[i][4]);
                        Tablo[3, i].Tag = elm_ları.Elemanları[i].Oku_TarihSaat(null, default, 4);
                    }
                    elm_ları.Elemanları[i].Sil(null);
                }

                Müşteriler_Grup.Enabled = false;
                Hastalar_Grup.Enabled = true;
                Hastalar_SeçimKutusu.Enabled = false;

                bool _ = elm_ları.İçiBoşOlduğuİçinSilinecek; //geçerli kaydı sil, kaydet tuşuna basınca tekrar oluşturulacak

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

        private void Müşteriler_AramaÇubuğu_TextChanged(object sender, EventArgs e)
        {
            Müşteriler_SeçimKutusu.Items.Clear();
            Hastalar_SeçimKutusu.Items.Clear();

            if (string.IsNullOrEmpty(Müşteriler_AramaÇubuğu.Text))
            {
                Müşteriler_SeçimKutusu.Items.AddRange(Müşteriler_Liste.ToArray());
            }
            else
            {
                Müşteriler_AramaÇubuğu.Text = Müşteriler_AramaÇubuğu.Text.ToLower();
                Müşteriler_SeçimKutusu.Items.AddRange(Müşteriler_Liste.FindAll(x => x.ToLower().Contains(Müşteriler_AramaÇubuğu.Text)).ToArray());
            }
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
        private void İşTürleri_AramaÇubuğu_TextChanged(object sender, EventArgs e)
        {
            İşTürleri_SeçimKutusu.Items.Clear();
            İştürü_SeçiliSatıraKopyala.Enabled = false;

            if (string.IsNullOrEmpty(İşTürleri_AramaÇubuğu.Text))
            {
                İşTürleri_SeçimKutusu.Items.AddRange(İşTürleri_Liste.ToArray());
            }
            else
            {
                İşTürleri_AramaÇubuğu.Text = İşTürleri_AramaÇubuğu.Text.ToLower();
                İşTürleri_SeçimKutusu.Items.AddRange(İşTürleri_Liste.FindAll(x => x.ToLower().Contains(İşTürleri_AramaÇubuğu.Text)).ToArray());
            }
        }

        private void Tablo_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;

            Kaydet.Enabled = true;
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
            if (e.RowIndex < 0 || e.ColumnIndex != 3) return;

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
            Kaydet.Enabled = true;
        }

        private void Müşteriler_SeçimKutusu_SelectedIndexChanged(object sender, EventArgs e)
        {
            Hastalar_Liste = null;
            Hastalar_AramaÇubuğu.Text = "";
            Hastalar_SeçimKutusu.Items.Clear();
            Hastalar_SeçimKutusu.Enabled = false;
            if (!Banka.Müşteri_MevcutMu(Müşteriler_SeçimKutusu.Text)) return;

            IDepo_Eleman d = Banka.Tablo_Dal(Müşteriler_SeçimKutusu.Text, Banka.TabloTürü.DevamEden, "Talepler");
            if (d == null || d.Elemanları.Length < 1) return;

            Hastalar_Liste = new List<string>();
            foreach (IDepo_Eleman ee in d.Elemanları)
            {
                if (ee.İçiBoşOlduğuİçinSilinecek) continue;
                
                string ha = ee[0] + " -=> (" + ee.Adı + (string.IsNullOrEmpty(ee[3]) ? " Devam Eden)" : " Teslim Edildi)");
                Hastalar_Liste.Add(ha);
            }

            Hastalar_SeçimKutusu.Enabled = true;
            Hastalar_SeçimKutusu.Items.AddRange(Hastalar_Liste.ToArray());
            Hastalar_AramaÇubuğu_TextChanged(null, null);
            Hastalar_AramaÇubuğu.Focus();
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
            if (hasta.EndsWith(" Devam Eden)"))
            {
                soru = "Yeni bir iş oluşturmak yerine" + Environment.NewLine +
                hasta + Environment.NewLine +
                "kaydını güncellemek ister misiniz?";
            }
            else if (hasta.EndsWith(" Teslim Edildi)"))
            {
                soru = "Seçtiğiniz hastaya ait kayıt TESLİM EDİLMİŞ olarak görünüyor." + Environment.NewLine + Environment.NewLine +
                "İşleme devam ederseniz kayıt DEVAM EDİYOR olarak işaretlenecektir." + Environment.NewLine + Environment.NewLine +
                "Yeni bir iş oluşturmak yerine" + Environment.NewLine +
                hasta + Environment.NewLine +
                "kaydını güncellemek ister misiniz?";
            }
            else return;

            Dr = MessageBox.Show(soru, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (Dr == DialogResult.No) return;

            Kaydet.Enabled = false;
            Ortak.YeniSayfaAçmaTalebi = new object[] { "Yeni İş Girişi", Müşteriler_SeçimKutusu.Text, sn };
            Close();
        }
        private void İşTürleri_SeçimKutusu_SelectedIndexChanged(object sender, EventArgs e)
        {
            İştürü_SeçiliSatıraKopyala.Enabled = İşTürleri_SeçimKutusu.Items.Count > 0;
        }

        private void İştürü_SeçiliSatıraKopyala_Click(object sender, EventArgs e)
        {
            var l = Tablo.SelectedRows;
            if (l == null || l.Count != 1) return;

            int konum = l[0].Index;
            if (konum == Tablo.RowCount - 1) Tablo.RowCount++;

            if (l[0].ReadOnly)
            {
                DialogResult Dr = MessageBox.Show((l[0].Index + 1).ToString() + ". satırdaki iş önceki döneme ait" + Environment.NewLine +
                        "Eğer kilidi kaldırılırsa halihazırdaki KABUL EDİLMİŞ BİLGİLERİ değiştirebileceksiniz." + Environment.NewLine +
                        "İlgili satırın KİLDİNİ AÇMAK istediğinize emin misiniz?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (Dr == DialogResult.No) return;

                l[0].ReadOnly = false;
            }

            Tablo[0, konum].Value = İşTürleri_SeçimKutusu.Text;
            Tablo[0, konum].ToolTipText = Banka.Ücretler_BirimÜcret_Detaylı(Müşteriler_SeçimKutusu.Text, İşTürleri_SeçimKutusu.Text); 
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
                Kaydet.Enabled = true;
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

                    //tarih kabul
                    if (Tablo[2, i].Tag == null)
                    {
                        Tablo[2, i].Value = t.Yazıya();
                        Tablo[2, i].Tag = t;
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
            Banka.Değişiklikleri_Kaydet();

            Kaydet.Enabled = false;
            Close();
        }
    }
}
