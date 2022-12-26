﻿using ArgeMup.HazirKod.Ekİşlemler;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip
{
    public partial class İş_Türleri : Form
    {
        public İş_Türleri()
        {
            InitializeComponent();
        }
        private void İş_Türleri_Load(object sender, System.EventArgs e)
        {
            Liste.Items.Clear();
            AramaÇubuğu_Liste = Banka.İşTürü_Listele();
            Liste.Items.AddRange(AramaÇubuğu_Liste.ToArray());
            if (Liste.Items.Count > 0) Sil.Enabled = true;

            Malzeme_Liste = Banka.Malzeme_Listele();
            string[] m_l = Malzeme_Liste.ToArray();
            Malzeme_SeçimKutusu.Items.AddRange(m_l);
            Tablo_Malzeme.Items.AddRange(m_l);

            Ortak.GeçiciDepolama_PencereKonumları_Oku(this);

            KeyDown += İş_Türleri_Tuş;
            KeyUp += İş_Türleri_Tuş;
            MouseWheel += İş_Türleri_MouseWheel;
            KeyPreview = true;
        }
        bool ctrl_tuşuna_basıldı = false;
        private void İş_Türleri_Tuş(object sender, KeyEventArgs e)
        {
            ctrl_tuşuna_basıldı = e.Control;
        }
        private void İş_Türleri_MouseWheel(object sender, MouseEventArgs e)
        {
            if (ctrl_tuşuna_basıldı)
            {
                WindowState = FormWindowState.Normal;
                if (e.Delta > 0) Font = new Font(Font.FontFamily, Font.Size + 0.2f);
                else Font = new Font(Font.FontFamily, Font.Size - 0.2f);
            }
        }
        private void İş_Türleri_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Kaydet.Enabled)
            {
                DialogResult Dr = MessageBox.Show("Değişiklikleri kaydetmeden çıkmak istediğinize emin misiniz?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                e.Cancel = Dr == DialogResult.No;
            }
        }
        private void İş_Türleri_FormClosed(object sender, FormClosedEventArgs e)
        {
            Ortak.GeçiciDepolama_PencereKonumları_Yaz(this);
        }

        List<string> AramaÇubuğu_Liste = null;
        private void AramaÇubuğu_TextChanged(object sender, EventArgs e)
        {
            splitContainer1.Panel2.Enabled = false;
            Liste.Items.Clear();

            if (string.IsNullOrEmpty(AramaÇubuğu.Text))
            {
                Liste.Items.AddRange(AramaÇubuğu_Liste.ToArray());
            }
            else
            {
                AramaÇubuğu.Text = AramaÇubuğu.Text.ToLower();
                Liste.Items.AddRange(AramaÇubuğu_Liste.FindAll(x => x.ToLower().Contains(AramaÇubuğu.Text)).ToArray());
            }
        }

        List<string> Malzeme_Liste = null;
        private void Malzeme_TextChanged(object sender, EventArgs e)
        {
            Malzeme_SeçiliSatıraKopyala.Enabled = false;
            Malzeme_SeçimKutusu.Items.Clear();

            if (string.IsNullOrEmpty(Malzeme_AramaÇubuğu.Text))
            {
                Malzeme_SeçimKutusu.Items.AddRange(Malzeme_Liste.ToArray());
            }
            else
            {
                Malzeme_AramaÇubuğu.Text = Malzeme_AramaÇubuğu.Text.ToLower();
                Malzeme_SeçimKutusu.Items.AddRange(Malzeme_Liste.FindAll(x => x.ToLower().Contains(Malzeme_AramaÇubuğu.Text)).ToArray());
            }
        }
        private void Malzeme_SeçimKutusu_SelectedValueChanged(object sender, System.EventArgs e)
        {
            Malzeme_SeçiliSatıraKopyala.Enabled = !string.IsNullOrEmpty(Malzeme_SeçimKutusu.Text);
        }
        private void Malzeme_SeçiliSatıraKopyala_Click(object sender, EventArgs e)
        {
            var l = Tablo.SelectedRows;
            if (l == null || l.Count != 1) return;

            int konum = l[0].Index;
            if (konum == Tablo.RowCount - 1) Tablo.RowCount++;

            Tablo[0, konum].Value = Malzeme_SeçimKutusu.Text;
        }

        private void Liste_SelectedValueChanged(object sender, System.EventArgs e)
        {
            Sil.Enabled = !string.IsNullOrEmpty(Liste.Text);

            if (!Sil.Enabled) { splitContainer1.Panel2.Enabled = false; return; }
            Yeni.Text = Liste.Text;

            Banka.İşTürü_Malzemeler_TablodaGöster(Tablo, Liste.Text, out string Notları);
            Notlar.Text = Notları;

            splitContainer1.Panel1.Enabled = true;
            splitContainer1.Panel2.Enabled = true;
            Kaydet.Enabled = false;
        }
        private void Sil_Click(object sender, System.EventArgs e)
        {
            if (Liste.SelectedIndex < 0 || Liste.SelectedIndex >= Liste.Items.Count) return;

            DialogResult Dr = MessageBox.Show(Liste .Text + " öğesini silmek istediğinize emin misiniz?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (Dr == DialogResult.No) return;

            Banka.İşTürü_Sil(Liste.Text);
            Banka.Değişiklikleri_Kaydet();

            AramaÇubuğu_Liste.Remove(Liste.Text);
            Liste.Items.RemoveAt(Liste.SelectedIndex);
        }
        private void Yeni_TextChanged(object sender, System.EventArgs e)
        {
            Ekle.Enabled = !string.IsNullOrWhiteSpace(Yeni.Text);
        }
        private void Ekle_Click(object sender, System.EventArgs e)
        {
            if (Banka.İşTürü_MevcutMu(Yeni.Text))
            {
                MessageBox.Show("Önceden eklenmiş", Text);
                return;
            }

            Banka.İşTürü_Ekle(Yeni.Text);
            Banka.Değişiklikleri_Kaydet();

            Liste.Items.Add(Yeni.Text);
            AramaÇubuğu_Liste.Add(Yeni.Text);
        }

        private void Tablo_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.CellStyle.BackColor = Color.White;
        }
        private void Tablo_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;

            Ayar_Değişti(null, null);

            if (e.ColumnIndex == 0) Tablo[2, e.RowIndex].Value = Banka.Malzeme_Birimi((string)Tablo[0, e.RowIndex].Value);
        }
        
        private void Ayar_Değişti(object sender, EventArgs e)
        {
            splitContainer1.Panel1.Enabled = false;
            Kaydet.Enabled = true;
        }
        
        private void Kaydet_Click(object sender, EventArgs e)
        {
            List<string> Malzemeler = new List<string>();
            List<string> Miktarlar = new List<string>();
            List<string> EklenmişMalzmeler = new List<string>();

            for (int i = 0; i < Tablo.RowCount - 1; i++)
            {
                try
                {
                    double miktar = double.Parse((string)Tablo[1, i].Value);
                    if (miktar <= 0)
                    {
                        continue; //silmek için kullanılıyor
                        //MessageBox.Show("Tablodaki " + (i + 1).ToString() + ". satırdaki miktar kutucuğuna 0 dan büyük bir değer giriniz veya siliniz", Text);
                        //return;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Tablodaki " + (i + 1).ToString() + ". satırdaki miktar kutucuğu içeriği sayıya dönüştürülemedi.", Text);
                    return;
                }

                if (!Banka.Malzeme_MevcutMu((string)Tablo[0, i].Value))
                {
                    MessageBox.Show("Tablodaki " + (i + 1) + ". satırdaki \"Malzeme\" uygun değil", Text);
                    return;
                }

                if (EklenmişMalzmeler.Contains((string)Tablo[0, i].Value))
                {
                    MessageBox.Show("Tablodaki " + (i + 1).ToString() + ". satırdaki " + (string)Tablo[0, i].Value + " malzemesi ikinci kez seçilmiş, lütfen fazla olanı siliniz", Text);
                    return;
                }
                else EklenmişMalzmeler.Add((string)Tablo[0, i].Value); 

                Malzemeler.Add((string)Tablo[0, i].Value);
                Miktarlar.Add((string)Tablo[1, i].Value);
            }

            if (Malzemeler.Count < 1)
            {
                MessageBox.Show("Tabloda hiç geçerli girdi bulunamadı", Text);
                return;
            }

            Banka.İşTürü_Malzemeler_Kaydet(Liste.Text, Malzemeler, Miktarlar, Notlar.Text.Trim());
            Banka.Değişiklikleri_Kaydet();

            splitContainer1.Panel1.Enabled = true;
            Liste_SelectedValueChanged(null, null);
        }
    }
}
