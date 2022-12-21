using ArgeMup.HazirKod;
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
            Müşteriler.Items.Clear();

            List<string> İşTürleri = Banka.İşTürü_Listele();
            Tablo_İş_Türü.Items.AddRange(İşTürleri.ToArray());

            if (Müşteri == null)
            {
                Müşteriler.Items.AddRange(Banka.Müşteri_Listele().ToArray());
            }
            else
            {
                Müşteriler.DropDownStyle = ComboBoxStyle.DropDown;
                Müşteriler.Text = Müşteri;
                Müşteriler.Enabled = false;
                Hasta.Enabled = false;

                IDepo_Eleman elm_ları = Banka.Tablo_Dal(Müşteri, Banka.TabloTürü.DevamEden, "Talepler/" + SeriNo);
                if (elm_ları == null) throw new Exception(Müşteri + " / Devam Eden / Talepler / " + SeriNo + " bulunamadı");

                Text += " - " + elm_ları.Adı;
                Hasta.Text = elm_ları[0];
                İskonto.Text = elm_ları[1];
                Notlar.Text = elm_ları[2];

                string hata_bilgilendirmesi = "";
                Tablo.RowCount = elm_ları.Elemanları.Length + 1;
                for (int i = 0; i < elm_ları.Elemanları.Length; i++)
                {
                    if (!İşTürleri.Contains(elm_ları.Elemanları[i][0]))
                    {
                        //eskiden varolan şuanda bulunmayan bir iş türü 
                        hata_bilgilendirmesi += (i + 1) + ". satırdaki \"" + elm_ları.Elemanları[i][0] + "\" olarak tanımlı iş türü şuanda mevcut olmadığından satır içeriği boş olarak bırakıldı" + Environment.NewLine;
                    }
                    else Tablo[0, i].Value = elm_ları.Elemanları[i][0]; //iş türü
                    
                    Tablo[1, i].Value = elm_ları.Elemanları[i][2]; //ücret
                    Tablo[2, i].Value = elm_ları.Elemanları[i][1]; //tarih
                    elm_ları.Elemanları[i].Sil(null);

                    Tablo.Rows[i].ReadOnly = true;
                }

                bool _ = elm_ları.İçiBoşOlduğuİçinSilinecek; //geçerli kaydı sil, kaydet tuşuna basınca tekrar oluşturulacak

                if (!string.IsNullOrEmpty(hata_bilgilendirmesi)) MessageBox.Show(hata_bilgilendirmesi, Text);
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
            if (Müşteri == null && Müşteriler.Items.Count > 0) Müşteriler.DroppedDown = true;
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

        private void Müşteriler_SelectedIndexChanged(object sender, EventArgs e)
        {
            Hasta.Items.Clear();
            if (!Banka.Müşteri_MevcutMu(Müşteriler.Text)) return;

            IDepo_Eleman d = Banka.Tablo_Dal(Müşteriler.Text, Banka.TabloTürü.DevamEden, "Talepler");
            if (d == null || d.Elemanları.Length < 1) return;

            foreach (IDepo_Eleman ee in d.Elemanları)
            {
                if (ee.İçiBoşOlduğuİçinSilinecek) continue;
                string ha = ee[0] + " -=> (" + ee.Adı + (string.IsNullOrEmpty(ee[3]) ? " Devam Eden)" : " Teslim Edildi)");
                Hasta.Items.Add(ha);
            }

            Hasta.Focus();
        }
        private void Hasta_Leave(object sender, EventArgs e)
        {
            Hasta.Text = Hasta.Text.Trim();
            if (string.IsNullOrWhiteSpace(Hasta.Text)) return;

            DialogResult Dr;
            if (Hasta.Items.Contains(Hasta.Text))
            {
                int konum = Hasta.Text.IndexOf(" -=> (");
                if (konum < 0)
                {
                    MessageBox.Show(Hasta.Text + " içeriğinden seri no okunamadı, hasta kutucuğundan tekrar seçim yapınız", Text);
                    return;
                }
                string sn = Hasta.Text.Substring(konum + 6).TrimEnd(')');
                konum = sn.IndexOf(" ");
                if (konum < 0)
                {
                    MessageBox.Show(Hasta.Text + " içeriğinden seri no okunamadı, hasta kutucuğundan tekrar seçim yapınız", Text);
                    return;
                }
                sn = sn.Substring(0, konum);

                string soru;
                if (Hasta.Text.EndsWith(" Devam Eden)"))
                {
                    soru = "Yeni bir iş oluşturmak yerine" + Environment.NewLine +
                    Hasta.Text + Environment.NewLine +
                    "kaydını güncellemek ister misiniz?";
                }
                else if (Hasta.Text.EndsWith(" Teslim Edildi)"))
                {
                    soru = "Seçtiğiniz hastaya ait kayıt TESLİM EDİLMİŞ olarak görünüyor." + Environment.NewLine + Environment.NewLine +
                    "İşleme devam ederseniz kayıt DEVAM EDİYOR olarak işaretlenecektir." + Environment.NewLine + Environment.NewLine +
                    "Yeni bir iş oluşturmak yerine" + Environment.NewLine +
                    Hasta.Text + Environment.NewLine +
                    "kaydını güncellemek ister misiniz?";
                }
                else return;

                Dr = MessageBox.Show(soru, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (Dr == DialogResult.No) return;

                new Yeni_Talep_Girişi(Müşteriler.Text, sn).ShowDialog();
                Banka.Değişiklikler_TamponuSıfırla();
                Kaydet.Enabled = false;
                Close();
            }
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
            }
           
            Kaydet.Enabled = true;
        }

        private void Kaydet_Click(object sender, EventArgs e)
        {
            if (!Banka.Müşteri_MevcutMu(Müşteriler.Text))
            {
                MessageBox.Show("Geçerli bir müşteri seçiniz", Text);
                Müşteriler.DroppedDown = true;
                return;
            }

            if (string.IsNullOrWhiteSpace(Hasta.Text))
            {
                MessageBox.Show("Hasta kutucuğu boş olmamalıdır" + Environment.NewLine + "örneğin hasta adı veya iş talep numarası yazılabilir", Text);
                Hasta.Focus();
                return;
            }
            Hasta.Text = Hasta.Text.Trim();

            try
            {
                if (!string.IsNullOrWhiteSpace(İskonto.Text))
                {
                    double i = İskonto.Text.NoktalıSayıya();
                    if (i > 100 || i < 0)
                    {
                        MessageBox.Show("İskonto kutucuğuna 0 ile 100 aralığında bir değer giriniz", Text);
                        İskonto.Focus();
                        return;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("İskonto kutucuğu içeriği sayıya dönüştürülemedi", Text);
                İskonto.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(Notlar.Text)) Notlar.Text = null;

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
                            double ü = ((string)Tablo[1, i].Value).NoktalıSayıya();
                            if (ü < 0)
                            {
                                MessageBox.Show("Tablodaki " + (i + 1) + ". satırdaki \"Ücret\" içeriği 0, boş veya sıfırdan büyük olmalı", Text);
                                return;
                            }
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

            Banka.Talep_Ekle(Müşteriler.Text, Hasta.Text, İskonto.Text, Notlar.Text, it_leri, ücret_ler, tarih_ler, SeriNo);
            Banka.Değişiklikleri_Kaydet();
            
            Kaydet.Enabled = false;
            Close();
        }
    }
}
