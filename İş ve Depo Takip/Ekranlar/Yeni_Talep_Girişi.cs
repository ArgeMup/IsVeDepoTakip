﻿using ArgeMup.HazirKod;
using ArgeMup.HazirKod.Ekİşlemler;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip
{
    public partial class Yeni_Talep_Girişi : Form
    {
        string Müşteri  = null, SeriNo = null;
        List<string> Müşteriler_Liste = null, Hastalar_Liste = null, İşTürleri_Liste = null; 

        public Yeni_Talep_Girişi(string Müşteri = null, string SeriNo = null)
        {
            InitializeComponent();

            Ortak.GeçiciDepolama_PencereKonumları_Oku(this);

            if (Müşteri == null || SeriNo == null) return;
            //düzenleme için açılacak
            this.Müşteri = Müşteri;
            this.SeriNo = SeriNo;
        }
        private void Yeni_Talep_Girişi_Load(object sender, EventArgs e)
        {
            İşTürleri_SeçimKutusu.Items.Clear();
            İşTürleri_Liste = Banka.İşTürü_Listele();
            string[] i_l = İşTürleri_Liste.ToArray();
            İşTürleri_SeçimKutusu.Items.AddRange(i_l);
            Tablo_İş_Türü.Items.AddRange(i_l);

            if (Müşteri == null)
            {
                Müşteriler_SeçimKutusu.Items.Clear();
                Müşteriler_Liste = Banka.Müşteri_Listele();
                Müşteriler_SeçimKutusu.Items.AddRange(Müşteriler_Liste.ToArray());
            }
            else
            {
                Müşteriler_Grup.Enabled = false;
                Hastalar_Grup.Enabled = false;

                Müşteriler_Liste = new List<string>();
                Müşteriler_Liste.Add(Müşteri);
                Müşteriler_AramaÇubuğu.Text = Müşteri;
                Müşteriler_SeçimKutusu.SelectedIndex = 0;
                Ayraç_Kat_1_2.SplitterDistance = (Müşteriler_AramaÇubuğu.Size.Height * 2) + (Müşteriler_AramaÇubuğu.Size.Height/2);

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
                    }

                    Tablo[1, i].Value = elm_ları.Elemanları[i][2]; //ücret
                    Tablo[2, i].Value = elm_ları.Elemanları[i][1]; //tarih
                    elm_ları.Elemanları[i].Sil(null);
                }

                bool _ = elm_ları.İçiBoşOlduğuİçinSilinecek; //geçerli kaydı sil, kaydet tuşuna basınca tekrar oluşturulacak

                if (!string.IsNullOrEmpty(hata_bilgilendirmesi))
                {
                    MessageBox.Show(hata_bilgilendirmesi + Environment.NewLine + "Bu mesaj Notlar içerisine aktarıldı", Text);
                    Notlar.Text = hata_bilgilendirmesi + Notlar.Text;
                    Ayraç_Kat_2_3.SplitterDistance *= 2; 
                }
            }

            Tablo.Rows[Tablo.RowCount - 1].Selected = true;
            
            KeyDown += Yeni_Talep_Girişi_Tuş;
            KeyUp += Yeni_Talep_Girişi_Tuş;
            MouseWheel += Yeni_Talep_Girişi_MouseWheel;
            KeyPreview = true;

            Kaydet.Enabled = false;
        }
        private void Yeni_Talep_Girişi_Shown(object sender, EventArgs e)
        {
            Müşteriler_AramaÇubuğu.Focus();
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
        private void Yeni_Talep_Girişi_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Kaydet.Enabled)
            {
                DialogResult Dr = MessageBox.Show("Değişiklikleri kaydetmeden çıkmak istediğinize emin misiniz?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                e.Cancel = Dr == DialogResult.No;
            }
        }
        private void Yeni_Talep_Girişi_FormClosed(object sender, FormClosedEventArgs e)
        {
            Ortak.GeçiciDepolama_PencereKonumları_Yaz(this);
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

        private void Tablo_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.CellStyle.BackColor = Color.White;
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

        private void Değişiklik_Yapılıyor(object sender, EventArgs e)
        {
            Kaydet.Enabled = true;
        }

        private void Müşteriler_SeçimKutusu_SelectedIndexChanged(object sender, EventArgs e)
        {
            Hastalar_Liste = null;
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

            new Yeni_Talep_Girişi(Müşteriler_SeçimKutusu.Text, sn).ShowDialog();
            Banka.Değişiklikler_TamponuSıfırla();
            Kaydet.Enabled = false;
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

            try
            {
                if (!string.IsNullOrWhiteSpace(İskonto.Text))
                {
                    double i = İskonto.Text.NoktalıSayıya(true, false);
                    if (i > 100 || i < 0)
                    {
                        MessageBox.Show("İskonto kutucuğuna 0 ile 100 aralığında bir değer giriniz", Text);
                        İskonto.Focus();
                        return;
                    }
                    else İskonto.Text = i.Yazıya();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("İskonto kutucuğu içeriği sayıya dönüştürülemedi", Text);
                İskonto.Focus();
                return;
            }
            
            DateTime t = DateTime.Now;
            List<string> it_leri = new List<string>();
            List<string> ücret_ler = new List<string>();
            List<string> tarih_ler = new List<string>();
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
                    try
                    {
                        if (!string.IsNullOrWhiteSpace((string)Tablo[1, i].Value))
                        {
                            double ü = ((string)Tablo[1, i].Value).NoktalıSayıya(true, false);
                            if (ü < 0)
                            {
                                MessageBox.Show("Tablodaki " + (i + 1) + ". satırdaki \"Ücret\" içeriği 0, boş veya sıfırdan büyük olmalı", Text);
                                return;
                            }
                            else Tablo[1, i].Value = ü.Yazıya();
                        }
                        //else
                        //{
                        //    MessageBox.Show("Tablodaki " + (i + 1) + ". satırdaki \"Ücret\" hanesine giriş yapılmamış" + Environment.NewLine +
                        //        "Eğer geçerli tarife üzerinden ücretlendirmek istiyorsanız İş Türü sutunundan farklı bir kalemi seçiniz" + Environment.NewLine +
                        //        "Ardından önceden seçili olan kalemi tekrar seçiniz", Text);
                        //    return;
                        //}
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Tablodaki " + (i + 1) + ". satırdaki \"Ücret\" sayıya dönüştürülemedi", Text);
                        return;
                    }

                    //tarih
                    Tablo[2, i].Value = t.Yazıya();
                }

                it_leri.Add((string)Tablo[0, i].Value);
                ücret_ler.Add(((string)Tablo[1, i].Value));
                tarih_ler.Add((string)Tablo[2, i].Value);
            }

            if (it_leri.Count == 0)
            {
                MessageBox.Show("Tabloda hiç geçerli girdi bulunamadı", Text);
                return;
            }

            Banka.Talep_Ekle(Müşteriler_SeçimKutusu.Text, Hastalar_AramaÇubuğu.Text, İskonto.Text, Notlar.Text.Trim(), it_leri, ücret_ler, tarih_ler, SeriNo);
            Banka.Değişiklikleri_Kaydet();

            Kaydet.Enabled = false;
            Close();
        }
    }
}
