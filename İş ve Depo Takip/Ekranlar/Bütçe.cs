using ArgeMup.HazirKod;
using System;
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

        #region _1_
        private void _1_Tablo_DoubleClick(object sender, EventArgs e)
        {
            if (_1_Tablo.RowCount < 1) return;
            bool b = !(bool)_1_Tablo[0, 0].Value;

            for (int i = 0; i < _1_Tablo.RowCount; i++)
            {
                _1_Tablo[0, i].Value = b;
            }
        }
        private void _1_Tablo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || e.ColumnIndex > 0) return;

            _1_Tablo[0, e.RowIndex].Value = !(bool)_1_Tablo[0, e.RowIndex].Value;

            _1_Hesapla(null, null);
        }

        bool _1_Hesapla_Çalışıyor = false;
        bool _1_Hesapla_KapatmaTalebi = false;
        int _1_Hesapla_Tik = 0;
        private void _1_Hesapla(object sender, EventArgs e)
        {
            if (_1_Hesapla_Çalışıyor) { _1_Hesapla_KapatmaTalebi = true; return; }
            _1_Hesapla_Çalışıyor = true;
            _1_Hesapla_KapatmaTalebi = false;
            _1_Hesapla_Tik = Environment.TickCount + 500;
            _1_AltToplam.BackColor = System.Drawing.Color.Salmon;

            if (_1_Dizi == null)
            {
                İlerlemeÇubuğu.Minimum = 0;
                İlerlemeÇubuğu.Value = 0;
                İlerlemeÇubuğu.Maximum = 0;
                İlerlemeÇubuğu.Visible = true;

                System.Collections.Generic.List<string> Müşteriler = Banka.Müşteri_Listele();
                for (int i = 0; i < Müşteriler.Count && !_1_Hesapla_KapatmaTalebi; i++)
                {
                    İlerlemeÇubuğu.Maximum += 2 + Banka.Dosya_Listele(Müşteriler[i], false).Length;

                    if (_1_Hesapla_Tik < Environment.TickCount) { Application.DoEvents(); _1_Hesapla_Tik = Environment.TickCount + 500; }
                }

                _1_Dizi = new Bütçe_Gelir_Gider_[Müşteriler.Count];

                for (int i = 0; i < Müşteriler.Count && !_1_Hesapla_KapatmaTalebi; i++)
                {
                    Bütçe_Gelir_Gider_ gg = new Bütçe_Gelir_Gider_();

                    İlerlemeÇubuğu.Value++;
                    _1_Hesapla_2(Müşteriler[i], Banka.Talep_Listele(Müşteriler[i], Banka.TabloTürü.DevamEden), out gg.gelir_devameden, out gg.gider_devameden);

                    İlerlemeÇubuğu.Value++;
                    _1_Hesapla_2(Müşteriler[i], Banka.Talep_Listele(Müşteriler[i], Banka.TabloTürü.TeslimEdildi), out gg.gelir_teslimedilen, out gg.gider_teslimedilen);

                    string[] l = Banka.Dosya_Listele(Müşteriler[i], false);
                    for (int s = 0; s < l.Length && !_1_Hesapla_KapatmaTalebi; s++)
                    {
                        İlerlemeÇubuğu.Value++;
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

                    if (_1_Hesapla_Tik < Environment.TickCount) { Application.DoEvents(); _1_Hesapla_Tik = Environment.TickCount + 500; }
                }

                _1_Tablo.Rows.Clear();
                _1_Tablo.RowCount = _1_Dizi.Length;
                for (int i = 0; i < _1_Tablo.RowCount; i++)
                {
                    _1_Tablo[0, i].Value = true;
                }
            }

            if (_1_Tablo.SortedColumn != null)
            {
                DataGridViewColumn col = _1_Tablo.SortedColumn;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                col.SortMode = DataGridViewColumnSortMode.Automatic;
            }

            for (int i = 0; i < _1_Dizi.Length; i++)
            {
                Bütçe_Gelir_Gider_ gg = _1_Dizi[i];

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
               
                _1_Tablo[1, i].Value = gg.müşteri;

                _1_Tablo[2, i].Value = Banka.Yazdır_Ücret(top_gelir);
                _1_Tablo[2, i].Tag = top_gelir;
                _1_Tablo[2, i].ToolTipText = gg.gelir_ipucu;

                _1_Tablo[3, i].Value = Banka.Yazdır_Ücret(top_gider);
                _1_Tablo[3, i].Tag = top_gider;
                _1_Tablo[3, i].ToolTipText = gg.gider_ipucu;

                _1_Tablo[4, i].Value = Banka.Yazdır_Ücret(top_gelir - top_gider);
                _1_Tablo[4, i].Tag = top_gelir - top_gider;

                if (_1_Hesapla_Tik < Environment.TickCount) { Application.DoEvents(); _1_Hesapla_Tik = Environment.TickCount + 500; }
            }

            double gel = 0, gid = 0, fark = 0;
            for (int i = 0; i < _1_Dizi.Length; i++)
            {
                gel += (double)_1_Tablo[2, i].Tag;
                gid += (double)_1_Tablo[3, i].Tag;
                fark += (double)_1_Tablo[4, i].Tag;

                if (_1_Hesapla_Tik < Environment.TickCount) { Application.DoEvents(); _1_Hesapla_Tik = Environment.TickCount + 500; }
            }

            _1_AltToplam.Text = "Alt Toplam   /   " +
                "Gelir : " + Banka.Yazdır_Ücret(gel) + "   /   " +
                "Gider : " + Banka.Yazdır_Ücret(gid) + "   /   " +
                "Fark : " + Banka.Yazdır_Ücret(fark);

            İlerlemeÇubuğu.Visible = false;
            _1_Hesapla_Çalışıyor = false;
            if (!_1_Hesapla_KapatmaTalebi) _1_AltToplam.BackColor = System.Drawing.Color.YellowGreen;
        }
        private void _1_Hesapla_2(string Müşteri, Banka_Tablo_ bt, out double Gelir, out double Gider)
        {
            Gelir = 0; Gider = 0;
            if (_1_Hesapla_KapatmaTalebi) return;
            if (_1_Hesapla_Tik < Environment.TickCount) { Application.DoEvents(); _1_Hesapla_Tik = Environment.TickCount + 500; }

            foreach (IDepo_Eleman serino in bt.Talepler)
            {
                string HataMesajı = "";
                Banka.Talep_Ayıkla_İş(Müşteri, serino, ref Gelir, ref Gider, ref HataMesajı);

                if (!string.IsNullOrEmpty(HataMesajı))
                {
                    MessageBox.Show("Alttaki işler için ücret hesaplanamadı." + Environment.NewLine +
                        "Ekrandaki hesaplamaların eksik olduğunu göz önünde bulundurunuz." + Environment.NewLine + Environment.NewLine +
                        HataMesajı, Text);
                }
            }
        }
        #endregion
    }
}
