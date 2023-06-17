using ArgeMup.HazirKod;
using ArgeMup.HazirKod.Ekİşlemler;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip.Ekranlar
{
    public partial class Değişkenler_Ekranı : Form
    {
        #region Yasaklı kelimeler ile uygunluk kontrolü
        List<string> YasakKelimeler_Ad = new List<string>();
        List<string> YasakKelimeler_İçerik = new List<string>();
        bool Değişken_Ad_içerik_UygunMu(DataGridViewRow satır)
        {
            if (satır.ReadOnly) return true;

            DataGridViewCell Ad = satır.Cells[Tablo_Adı.Index];
            DataGridViewCell İçerik = satır.Cells[Tablo_İçeriği.Index];

            string gecici = Ad.Value as string;
            if (gecici.BoşMu(true)) return false;
            foreach (string biri in YasakKelimeler_Ad)
            {
                if (gecici.Contains(biri))
                {
                    Ad.Style.BackColor = Color.Salmon;
                    Ad.ToolTipText = "Yasaklı içerik bulundu (" + biri + ")";
                    return false;
                }
            }

            gecici = İçerik.Value as string;
            if (gecici.BoşMu(true)) return false;
            foreach (string biri in YasakKelimeler_İçerik)
            {
                if (gecici.Contains(biri))
                {
                    İçerik.Style.BackColor = Color.Salmon;
                    İçerik.ToolTipText = "Yasaklı içerik bulundu (" + biri + ")";
                    return false;
                }
            }

            Ad.Style.BackColor = Color.White;
            Ad.ToolTipText = null;
            İçerik.Style.BackColor = Color.White;
            İçerik.ToolTipText = null;
            return true;
        }
        #endregion

        public Değişkenler_Ekranı()
        {
            InitializeComponent();

            //görsel çiziminin iyileşmsi için
            typeof(Control).InvokeMember("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty, null, Tablo, new object[] { DoubleBuffered });

            YasakKelimeler_Ad.AddRange(Değişkenler.Sabitler);
            YasakKelimeler_Ad.AddRange(Değişkenler.YasakKelimeler_Ad);
            YasakKelimeler_Ad.Add("Ortak Ücreti");
            YasakKelimeler_Ad.Add("Maliyeti");
            YasakKelimeler_İçerik.Add("%Ortak Ücreti%");
            YasakKelimeler_İçerik.Add("%Maliyeti%");

            Değişkenler.Tümü = null;
            foreach (var biri in Değişkenler.Tümü)
            {
                Tablo.Rows.Add(new object[] { biri.Key, biri.Value });
            }
            for (int i = 0; i < Değişkenler.Sabitler.Length; i++)
            {
                Tablo.Rows[i].ReadOnly = true;
                Tablo.Rows[i].Cells[Tablo_İçeriği.Index].ToolTipText = "Uygulama tarafından internet aracılığıyla edinilir." + Environment.NewLine +
                    "Eğer bu değişkeni kullandıysanız ve " + Environment.NewLine +
                    "internet bağlantısı kopmuş veya " + Environment.NewLine +
                    "web sitesi cevap vermiyor ise " + Environment.NewLine +
                    "uygulama 15 sn ye kadar hareketsiz kalabiir.";
            }
            
            if (Tablo.Rows.Count > 0) Tablo_CellValueChanged(null, new DataGridViewCellEventArgs(Tablo_İçeriği.Index, 0));

            Tablo.ClearSelection();
            Kaydet.Enabled = false;
            Tablo.Enabled = true;
        }
        private void Değişkenler_Ekranı_FormClosed(object sender, FormClosedEventArgs e)
        {
            Değişkenler.Tümü = null;
        }

        private void Tablo_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || Tablo.Tag != null) return;
            Tablo.Tag = 0;

            if (e.ColumnIndex != Tablo_Değeri.Index)
            {
                string içeriği = Tablo[e.ColumnIndex, e.RowIndex].Value as string;
                if (içeriği != null)
                {
                    if (e.ColumnIndex == Tablo_İçeriği.Index) içeriği = Değişkenler.Düzenle(içeriği);

                    içeriği = içeriği.Trim();
                    Tablo[e.ColumnIndex, e.RowIndex].Value = içeriği;
                }

                //asıl değişkenlerin alınması
                Dictionary<string, string> TümDeğişkenler = new Dictionary<string, string>(Değişkenler.Tümü);
                
                //yeni değişkenlerin asıl değişkenler içine eklenmesi
                foreach (DataGridViewRow satır in Tablo.Rows)
                {
                    if (!Değişken_Ad_içerik_UygunMu(satır)) continue;

                    string adı = satır.Cells[Tablo_Adı.Index].Value as string;
                    içeriği = satır.Cells[Tablo_İçeriği.Index].Value as string;

                    if (TümDeğişkenler.ContainsKey(adı)) TümDeğişkenler[adı] = içeriği;
                    else TümDeğişkenler.Add(adı, içeriği);
                }

                foreach (DataGridViewRow satır in Tablo.Rows)
                {
                    if (!Değişken_Ad_içerik_UygunMu(satır)) continue;

                    içeriği = satır.Cells[Tablo_İçeriği.Index].Value as string;

                    string snç = Değişkenler.Hesapla(içeriği, out double Çıktı, TümDeğişkenler);
                    satır.Cells[Tablo_Değeri.Index].Value = snç.DoluMu() ? snç : Çıktı.Yazıya();
                    if (snç.DoluMu())
                    {
                        satır.Cells[Tablo_Değeri.Index].Style.BackColor = Color.Salmon;
                        satır.Cells[Tablo_Değeri.Index].ToolTipText = null;
                    }
                    else if (Çıktı <= 0)
                    {
                        satır.Cells[Tablo_Değeri.Index].Style.BackColor = Color.Khaki;
                        satır.Cells[Tablo_Değeri.Index].ToolTipText = "Sonucun <=0 olduğunu göz önünde bulundurunuz.";
                    }
                    else
                    {
                        satır.Cells[Tablo_Değeri.Index].Style.BackColor = Color.White;
                        satır.Cells[Tablo_Değeri.Index].ToolTipText = null;
                    }
                }
            }

            Kaydet.Enabled = true;
            Tablo.Tag = null;
        }

        private void Kaydet_Click(object sender, EventArgs e)
        {
            IDepo_Eleman Ayarlar = Banka.Ayarlar_Genel("Değişkenler", true);
            Ayarlar.Sil(null, false, true);
            for (int i = Değişkenler.Sabitler.Length; i < Tablo.Rows.Count; i++)
            {
                DataGridViewRow satır = Tablo.Rows[i];

                string adı = satır.Cells[Tablo_Adı.Index].Value as string;
                string içeriği = satır.Cells[Tablo_İçeriği.Index].Value as string;

                if (!Değişken_Ad_içerik_UygunMu(satır)) continue;

                Ayarlar.Yaz(adı, içeriği);
            }

            Banka.Değişiklikleri_Kaydet(Kaydet);
            Kaydet.Enabled = false;
            Değişkenler.Tümü = null;
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
                if (Tablo.Rows[satır].IsNewRow) continue;

                bool bulundu = false;
                for (int sutun = 0; sutun < Tablo.Columns.Count; sutun++)
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

            TabloİçeriğiArama.BackColor = System.Drawing.Color.White;
            TabloİçeriğiArama_Çalışıyor = false;
            Tablo.ClearSelection();

            if (TabloİçeriğiArama_KapatmaTalebi) TabloİçeriğiArama_TextChanged(null, null);
            TabloİçeriğiArama_KapatmaTalebi = false;
        }
        private void TabloİçeriğiArama_DoubleClick(object sender, EventArgs e)
        {
            Clipboard.SetText(İpUcu_Genel.GetToolTip(TabloİçeriğiArama));
        }
    }
}
