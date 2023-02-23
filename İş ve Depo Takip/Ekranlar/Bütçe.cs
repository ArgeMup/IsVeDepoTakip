﻿using ArgeMup.HazirKod;
using ArgeMup.HazirKod.Ekİşlemler;
using System;
using System.Linq;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip.Ekranlar
{
    public partial class Bütçe : Form
    {
        struct Bütçe_Gelir_Gider_
        {
            public double gelir_devameden, gelir_teslimedilen, gelir_ödemebekleyen;
            public double gider_devameden, gider_teslimedilen, gider_ödemebekleyen;
            public string müşteri, gelir_ipucu, gider_ipucu;
        };
        Bütçe_Gelir_Gider_[] _1_Dizi = null;

        public Bütçe()
        {
            InitializeComponent();

            Ortak.GeçiciDepolama_PencereKonumları_Oku(this);

            IDepo_Eleman Ayarlar_GenelAnlamda = Banka.Ayarlar_Genel("Bütçe/Genel Anlamda");
            if (Ayarlar_GenelAnlamda != null ) 
            {
                foreach (IDepo_Eleman a in Ayarlar_GenelAnlamda.Elemanları)
                {
                    _2_Tablo.Rows.Add(new object[] { true, a[0], a[1], a[2] });
                }
            }
        }
        private void Bütçe_Shown(object sender, EventArgs e)
        {
            if (Sekmeler.Tag == null)
            {
                Sekmeler.Tag = 0;

                Application.DoEvents();
                _1_Hesapla(null, null);
            }
        }
        private void Sekmeler_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Sekmeler.SelectedIndex == 1)
            {
                //genel anlamda

                //Müşteriler kapsamında yazısını ara
                int Müşteriler_kapsamında = -1;
                for (int i = 0; i < _2_Tablo.RowCount; i++)
                {
                    if ((string)_2_Tablo[1, i].Value == "Müşteriler kapsamında")
                    {
                        Müşteriler_kapsamında = i;
                        break;
                    }
                }

                if (Müşteriler_kapsamında >= 0) _2_Tablo.Rows.RemoveAt(Müşteriler_kapsamında);

                double gel = (double)_1_Gelir.Tag;
                double gid = (double)_1_Gider.Tag;
                double fark = gel - gid;
                _2_Tablo.Rows.Insert(0, new object[] { true, "Müşteriler kapsamında", gel.Yazıya(), gid.Yazıya(), fark.Yazıya() });

                _2_Hesapla(null, null);
            }
        }

        #region _1_
        private void _1_Tablo_DoubleClick(object sender, EventArgs e)
        {
            if (_1_Tablo.RowCount < 1) return;
            bool b = !(bool)_1_Tablo[0, 0].Value;

            for (int i = 0; i < _1_Tablo.RowCount; i++)
            {
                if (!_1_Tablo[0, i].Visible) continue;
                _1_Tablo[0, i].Value = b;
            }

            _1_Hesapla(null, null);
        }
        private void _1_Tablo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (e.ColumnIndex == 0)
            {
                //Seçme sutunu
                _1_Tablo[0, e.RowIndex].Value = !(bool)_1_Tablo[0, e.RowIndex].Value;

                _1_Hesapla(null, null);
            }
            else if (e.ColumnIndex == 5)
            {
                //Listele tuşu
                Seklemeler_Ödemeler.Text = "Ödemeler - " + _1_Tablo[1, e.RowIndex].Value;
                Sekmeler.SelectedIndex = 2; //ödemeler

                Sekmeler.Enabled = false;
                Banka.Müşteri_Ödemeler_TablodaGöster((string)_1_Tablo[1, e.RowIndex].Value, _3_Tablo);
                Sekmeler.Enabled = true;
            }
        }

        private void _1_Hesapla(object sender, EventArgs e)
        {
            //ilk açılışta 1 kere hesaplat
            if (_1_Dizi == null)
            {
                _1_AltToplam.BackColor = System.Drawing.Color.Khaki;
                Ortak.Gösterge.Başlat("Sayılıyor", true, null, 0);
                int kademe = 0;

                System.Collections.Generic.List<string> Müşteriler = Banka.Müşteri_Listele();
                for (int i = 0; i < Müşteriler.Count && Ortak.Gösterge.Çalışsın; i++)
                {
                    kademe += 2 + Banka.Dosya_Listele_Müşteri(Müşteriler[i], false).Length;
                }

                Ortak.Gösterge.Başlat("Hesaplanıyor", true, null, kademe);
                _1_Dizi = new Bütçe_Gelir_Gider_[Müşteriler.Count];

                for (int i = 0; i < Müşteriler.Count && Ortak.Gösterge.Çalışsın; i++)
                {
                    Bütçe_Gelir_Gider_ gg = new Bütçe_Gelir_Gider_();

                    Ortak.Gösterge.İlerleme = 1;
                    _1_Hesapla_2(Müşteriler[i], Banka.Talep_Listele(Müşteriler[i], Banka.TabloTürü.DevamEden), out gg.gelir_devameden, out gg.gider_devameden);

                    Ortak.Gösterge.İlerleme = 1;
                    _1_Hesapla_2(Müşteriler[i], Banka.Talep_Listele(Müşteriler[i], Banka.TabloTürü.TeslimEdildi), out gg.gelir_teslimedilen, out gg.gider_teslimedilen);

                    string[] l = Banka.Dosya_Listele_Müşteri(Müşteriler[i], false);
                    for (int s = 0; s < l.Length && Ortak.Gösterge.Çalışsın; s++)
                    {
                        Ortak.Gösterge.İlerleme = 1;
                        _1_Hesapla_2(Müşteriler[i], Banka.Talep_Listele(Müşteriler[i], Banka.TabloTürü.ÖdemeTalepEdildi, l[s]), out double gelll, out double giddd);

                        gg.gelir_ödemebekleyen += gelll;
                        gg.gider_ödemebekleyen += giddd;
                    }

                    gg.müşteri = Müşteriler[i];
                    gg.gelir_ipucu =
                        "Devam eden : " + Banka.Yazdır_Ücret(gg.gelir_devameden) + Environment.NewLine +
                        "Teslim edilen : " + Banka.Yazdır_Ücret(gg.gelir_teslimedilen) + Environment.NewLine +
                        "Ödeme talebi : " + Banka.Yazdır_Ücret(gg.gelir_ödemebekleyen);
                    gg.gider_ipucu =
                        "Devam eden : " + Banka.Yazdır_Ücret(gg.gider_devameden) + Environment.NewLine +
                        "Teslim edilen : " + Banka.Yazdır_Ücret(gg.gider_teslimedilen) + Environment.NewLine +
                        "Ödeme talebi : " + Banka.Yazdır_Ücret(gg.gider_ödemebekleyen);
                    _1_Dizi[i] = gg;
                }

                _1_Tablo.Rows.Clear();
                _1_Tablo.RowCount = _1_Dizi.Length;
                for (int i = 0; i < _1_Tablo.RowCount; i++)
                {
                    _1_Tablo[0, i].Value = true;
                    _1_Tablo[1, i].Value = _1_Dizi[i].müşteri;

                    double ÖnÖdemeMiktarı = Banka.Müşteri_ÖnÖdemeMiktarı(_1_Dizi[i].müşteri);
                    _1_Tablo[5, i].Value = Banka.Yazdır_Ücret(ÖnÖdemeMiktarı);
                    if (ÖnÖdemeMiktarı > 0) _1_Tablo[5, i].Style.BackColor = System.Drawing.Color.Green;
                    else if (ÖnÖdemeMiktarı < 0) _1_Tablo[5, i].Style.BackColor = System.Drawing.Color.Red;
                }

                Ortak.Gösterge.Bitir();
            }

            //talep edilen müşterilerin talep edilen değerlerini hesapla
            for (int i = 0; i < _1_Tablo.RowCount; i++)
            {
                Bütçe_Gelir_Gider_ gg = _1_Dizi.First(x => x.müşteri == (string)_1_Tablo[1, i].Value);

                double top_gelir = 0, top_gider = 0;
                if ((bool)_1_Tablo[0, i].Value)
                {
                    if (_1_Gelir_DevamEden.Checked) top_gelir += gg.gelir_devameden;
                    if (_1_Gelir_TeslimEdildi.Checked) top_gelir += gg.gelir_teslimedilen;
                    if (_1_Gelir_ÖdemeTalepEdildi.Checked) top_gelir += gg.gelir_ödemebekleyen;
                    if (_1_Gider_DevamEden.Checked) top_gider += gg.gider_devameden;
                    if (_1_Gider_TeslimEdildi.Checked) top_gider += gg.gider_teslimedilen;
                    if (_1_Gider_ÖdemeTalepEdildi.Checked) top_gider += gg.gider_ödemebekleyen;
                }

                _1_Tablo[2, i].Value = top_gelir;
                _1_Tablo[2, i].ToolTipText = gg.gelir_ipucu;

                _1_Tablo[3, i].Value = top_gider;
                _1_Tablo[3, i].ToolTipText = gg.gider_ipucu;

                _1_Tablo[4, i].Value = top_gelir - top_gider;
            }

            //talep edilenlerin çıktılarını topla
            double gel = 0, gid = 0, fark = 0;
            for (int i = 0; i < _1_Dizi.Length; i++)
            {
                gel += (double)_1_Tablo[2, i].Value;
                gid += (double)_1_Tablo[3, i].Value;
                fark += (double)_1_Tablo[4, i].Value;
            }

            //çıktıları yazdır
            _1_AltToplam.Text = "Alt Toplam   /   " +
                "Gelir : " + Banka.Yazdır_Ücret(gel) + "   /   " +
                "Gider : " + Banka.Yazdır_Ücret(gid) + "   /   " +
                "Fark : " + Banka.Yazdır_Ücret(fark);

            _1_Gelir.Tag = gel;
            _1_Gider.Tag = gid;

            _1_AltToplam.BackColor = gel > gid ? System.Drawing.Color.YellowGreen : System.Drawing.Color.Salmon;
        }
        private void _1_Hesapla_2(string Müşteri, Banka_Tablo_ bt, out double Gelir, out double Gider)
        {
            Gelir = 0; Gider = 0;
            if (!Ortak.Gösterge.Çalışsın) return;
           
            foreach (IDepo_Eleman serino in bt.Talepler)
            {
                string HataMesajı = "";
                Banka.Talep_Ayıkla_SeriNoDalı(Müşteri, serino, ref Gelir, ref Gider, ref HataMesajı);

#if !DEBUG
                if (!string.IsNullOrEmpty(HataMesajı))
                {
                    MessageBox.Show("Alttaki işler için ücret hesaplanamadı." + Environment.NewLine +
                        "Ekrandaki hesaplamaların eksik olduğunu göz önünde bulundurunuz." + Environment.NewLine + Environment.NewLine +
                        HataMesajı, Text);
                }
#endif
            }
        }
        #endregion

        #region _2_
        private void _2_Tablo_DoubleClick(object sender, EventArgs e)
        {
            if (_2_Tablo.RowCount < 1) return;
            bool b = !(bool)_2_Tablo[0, 0].Value;

            for (int i = 0; i < _2_Tablo.RowCount; i++)
            {
                if (!_2_Tablo[0, i].Visible) continue;
                _2_Tablo[0, i].Value = b;
            }
        }
        private void _2_Tablo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || e.ColumnIndex > 0) return;

            _2_Tablo[0, e.RowIndex].Value = !(bool)_2_Tablo[0, e.RowIndex].Value;

            _2_Hesapla(null, null);
        }
        private void _2_Tablo_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex >= 1 && e.ColumnIndex <= 3) Kaydet.Enabled = true;

            if (e.ColumnIndex == 0 || e.ColumnIndex == 2 || e.ColumnIndex == 3) _2_Hesapla(null, null);
        }

        private void _2_Hesapla(object sender, EventArgs e)
        {
            double gelir_top = 0, gider_top = 0;

            for (int i = 0; i < _2_Tablo.RowCount; i++)
            {
                double gelir = 0, gider = 0;

                if (!string.IsNullOrEmpty((string)_2_Tablo[2, i].Value))
                {
                    string gecici = (string)_2_Tablo[2, i].Value;
                    if (!Ortak.YazıyıSayıyaDönüştür(ref gecici,
                        (string)_2_Tablo[1, i].Value + " (gelir sutununun " + (i + 1).ToString() + ". satırı)",
                        "Ücretlendirmek istemiyorsanız boş olarak bırakınız.", 0))
                    {
                        _2_Tablo[2, i].Style.BackColor = System.Drawing.Color.Salmon;
                        return;
                    }
                    _2_Tablo[2, i].Style.BackColor = System.Drawing.Color.White;

                    _2_Tablo[2, i].Value = gecici;
                    gelir = gecici.NoktalıSayıya();
                }

                if (!string.IsNullOrEmpty((string)_2_Tablo[3, i].Value))
                {
                    string gecici = (string)_2_Tablo[3, i].Value;
                    if (!Ortak.YazıyıSayıyaDönüştür(ref gecici,
                        (string)_2_Tablo[1, i].Value + " (gider sutununun " + (i + 1).ToString() + ". satırı)",
                        "Ücretlendirmek istemiyorsanız boş olarak bırakınız.", 0))
                    {
                        _2_Tablo[3, i].Style.BackColor = System.Drawing.Color.Salmon;
                        return;
                    }
                    _2_Tablo[3, i].Style.BackColor = System.Drawing.Color.White;

                    _2_Tablo[3, i].Value = gecici;
                    gider = gecici.NoktalıSayıya();
                }

                _2_Tablo[4, i].Value = (gelir - gider).Yazıya();

                if (_2_Tablo[0, i].Value == null) _2_Tablo[0, i].Value = true;
                if ((bool)_2_Tablo[0, i].Value)
                {
                    gelir_top += gelir;
                    gider_top += gider;
                }
            }

            //çıktıları yazdır
            _2_AltToplam.Text = "Alt Toplam   /   " +
                "Gelir : " + Banka.Yazdır_Ücret(gelir_top) + "   /   " +
                "Gider : " + Banka.Yazdır_Ücret(gider_top) + "   /   " +
                "Fark : " + Banka.Yazdır_Ücret(gelir_top - gider_top);

            _2_AltToplam.BackColor = gelir_top > gider_top ? System.Drawing.Color.YellowGreen : System.Drawing.Color.Salmon;
        }
        #endregion

        private void Kaydet_Click(object sender, EventArgs e)
        {
            IDepo_Eleman Ayarlar_GenelAnlamda = Banka.Ayarlar_Genel("Bütçe/Genel Anlamda", true);
            Ayarlar_GenelAnlamda.Sil(null, false, true);
            for (int i = 0; i < _2_Tablo.RowCount; i++)
            {
                if ((string)_2_Tablo[1, i].Value == "Müşteriler kapsamında") continue;

                Ayarlar_GenelAnlamda.Yaz(i.ToString(), (string)_2_Tablo[1, i].Value, 0);
                Ayarlar_GenelAnlamda.Yaz(i.ToString(), (string)_2_Tablo[2, i].Value, 1);
                Ayarlar_GenelAnlamda.Yaz(i.ToString(), (string)_2_Tablo[3, i].Value, 2);
            }

            Banka.Değişiklikleri_Kaydet(Kaydet);
            Kaydet.Enabled = false;
        }

        bool TabloİçeriğiArama_Çalışıyor = false;
        bool TabloİçeriğiArama_KapatmaTalebi = false;
        int TabloİçeriğiArama_Tik = 0;
        int TabloİçeriğiArama_Sayac_Bulundu = 0;
        private void TabloİçeriğiArama_TextChanged(object sender, EventArgs e)
        {
            DataGridView Tablo;
            switch (Sekmeler.SelectedIndex)
            {
                case 0: Tablo = _1_Tablo; break;
                case 1: Tablo = _2_Tablo; break;
                case 2: Tablo = _3_Tablo; break;
                default: return;
            }

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
                for (int sutun = 1; sutun < Tablo.Columns.Count; sutun++)
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
    }
}
