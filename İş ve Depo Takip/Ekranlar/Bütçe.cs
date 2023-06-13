using ArgeMup.HazirKod;
using ArgeMup.HazirKod.Ekİşlemler;
using System;
using System.Collections.Generic;
using System.IO;
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
                Ortak.Gösterge.Başlat("Sayılıyor", true, Sekmeler, 0);
                int kademe = 0;

                System.Collections.Generic.List<string> Müşteriler = Banka.Müşteri_Listele();
                for (int i = 0; i < Müşteriler.Count && Ortak.Gösterge.Çalışsın; i++)
                {
                    kademe += 2 + Banka.Dosya_Listele_Müşteri(Müşteriler[i], false).Length;
                }

                Ortak.Gösterge.Başlat("Hesaplanıyor", true, Sekmeler, kademe);
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
                string HataMesajı = Banka.Talep_Ayıkla_SeriNoDalı(Müşteri, serino, ref Gelir, ref Gider);
                if (!string.IsNullOrEmpty(HataMesajı))
                {
                    MessageBox.Show("Alttaki konuda hesaplama yapılamadı." + Environment.NewLine +
                        "Ekrandaki hesaplamaların eksik olduğunu göz önünde bulundurunuz." + Environment.NewLine + Environment.NewLine +
                        HataMesajı, Text);
                }
            }
        }

        private void _1_GenelDurumRaporu_Click(object sender, EventArgs e)
        {
            List<string> l_Dallar = new List<string>();
            Dictionary<string, double> l_MüşteriÖdemeleri = new Dictionary<string, double>(); //Müşteri Ödemeleri (₺)
            Dictionary<string, double> l_İşTürleri_GenelKullanım = new Dictionary<string, double>(); //İş Türleri|Genel Kullanım (adet)
            Dictionary<string, double> l_İşTürleri_MüşterilereGöreKullanım = new Dictionary<string, double>(); //İş Türleri|Müşterilere Göre Kullanım (adet)
            Dictionary<string, double> l_MalzemeKullanımı = new Dictionary<string, double>(); //Malzeme Kullanımı

            Ortak.Gösterge.Başlat("Sayılıyor", true, Sekmeler, 0);
            int kademe = 0;
            List<string> Müşteriler = Banka.Müşteri_Listele();
            List<string> Malzemeler = Banka.Malzeme_Listele();
            for (int i = 0; i < Müşteriler.Count && Ortak.Gösterge.Çalışsın; i++)
            {
                kademe += Banka.Dosya_Listele_Müşteri(Müşteriler[i], true).Length;
            }
            kademe += Malzemeler.Count;
            string başlık, anahtar;
            string[] başlıklar;

            string çizelgeç_dosyayolu = Klasör.Depolama(Klasör.Kapsamı.Geçici, null, "Çizelgeç", "") + "\\Çizelgeç.exe";
            YeniYazılımKontrolü_ yyk = new YeniYazılımKontrolü_();
            if (File.Exists(çizelgeç_dosyayolu)) yyk.KontrolTamamlandı = true;
            else yyk.Başlat(new Uri("https://github.com/ArgeMup/Cizelgec/blob/main/%C3%87izelge%C3%A7/bin/Release/%C3%87izelge%C3%A7.exe?raw=true"), null, çizelgeç_dosyayolu);

            Ortak.Gösterge.Başlat("Hesaplanıyor", true, Sekmeler, kademe);
            DateTime EnEskiTarih = DateTime.MaxValue;

            foreach (string Müşteri in Müşteriler)
            {
                if (!Ortak.Gösterge.Çalışsın) break;

                foreach (string Ödeme_Tarihi_dosyaadı in Banka.Dosya_Listele_Müşteri(Müşteri, true))
                {
                    if (!Ortak.Gösterge.Çalışsın) break;

                    Ortak.Gösterge.İlerleme = 1;
                    Depo_ d = Banka.Tablo(Müşteri, Banka.TabloTürü.Ödendi, false, Ödeme_Tarihi_dosyaadı);
                    Banka.Talep_Ayıkla_ÖdemeDalı_Açıklama(d["Ödeme"], out _, out _, out double GenelToplam);
                    DateTime Ödeme_Tarihi_t = Ödeme_Tarihi_dosyaadı.TarihSaate(ArgeMup.HazirKod.Dönüştürme.D_TarihSaat.Şablon_DosyaAdı2);
                    if (Ödeme_Tarihi_t < EnEskiTarih) EnEskiTarih = Ödeme_Tarihi_t;
                    string Ödeme_Tarihi_yıl_ay = Ödeme_Tarihi_t.ToString("yyyy M");

                    //Müşteri Ödemeleri (₺)|Müşteri 1|2022 12
                    anahtar = Müşteri + "|" + Ödeme_Tarihi_yıl_ay;
                    if (!l_MüşteriÖdemeleri.ContainsKey(anahtar)) l_MüşteriÖdemeleri.Add(anahtar, 0);
                    l_MüşteriÖdemeleri[anahtar] += GenelToplam;
                    if (!l_Dallar.Contains("Mö|" + Müşteri)) l_Dallar.Add("Mö|" + Müşteri);

                    //sn göre ayıklama
                    foreach (IDepo_Eleman sn in d["Talepler"].Elemanları)
                    {
                        if (!Ortak.Gösterge.Çalışsın) break;

                        //İş türlerine göre ayıklama
                        foreach (IDepo_Eleman it in sn.Elemanları)
                        {
                            if (!Ortak.Gösterge.Çalışsın) break;
                            
                            Banka.Talep_Ayıkla_İşTürüDalı(it, out string İşTürü, out _, out _, out _, out _, out _);

                            //İş Türleri|Genel Kullanım (adet)|iştürü|2022 12
                            anahtar = İşTürü + "|" + Ödeme_Tarihi_yıl_ay;
                            if (!l_İşTürleri_GenelKullanım.ContainsKey(anahtar)) l_İşTürleri_GenelKullanım.Add(anahtar, 0);
                            l_İşTürleri_GenelKullanım[anahtar] += 1;
                            if (!l_Dallar.Contains("İg|" + İşTürü)) l_Dallar.Add("İg|" + İşTürü);

                            //İş Türleri|Müşterilere Göre Kullanım (adet)|Müşteri|iştürü|2022 12
                            başlık = Müşteri + "|" + İşTürü;
                            anahtar = başlık + "|" + Ödeme_Tarihi_yıl_ay;
                            if (!l_İşTürleri_MüşterilereGöreKullanım.ContainsKey(anahtar)) l_İşTürleri_MüşterilereGöreKullanım.Add(anahtar, 0);
                            l_İşTürleri_MüşterilereGöreKullanım[anahtar] += 1;
                            if (!l_Dallar.Contains("İm|" + başlık)) l_Dallar.Add("İm|" + başlık);
                        }
                    }
                }
            }

            foreach (string Malzeme in Malzemeler)
            {
                if (!Ortak.Gösterge.Çalışsın) break;

                Ortak.Gösterge.İlerleme = 1;
                IDepo_Eleman mlz = Banka.Tablo_Dal(null, Banka.TabloTürü.Malzemeler, "Malzemeler/" + Malzeme);
                Banka.Malzeme_Ayıkla_MalzemeDalı(mlz, out _, out _, out string Birimi, out _, out _, out _);
                Banka.Malzeme_Ayıkla_MalzemeDalı_Tüketim(mlz, out _, out DateTime[] Dönemler, out double[] Dönemler_Kullanım);

                for (int i = 0; i < Dönemler.Length; i++)
                {
                    if (Dönemler[i] < EnEskiTarih) EnEskiTarih = Dönemler[i];

                    //Malzeme Kullanımı|malzeme|2022 12
                    başlık = Malzeme + (Birimi.DoluMu() ? " (" + Birimi + ")" : null);
                    anahtar = başlık + "|" + Dönemler[i].ToString("yyyy M");
                    if (!l_MalzemeKullanımı.ContainsKey(anahtar)) l_MalzemeKullanımı.Add(anahtar, 0);
                    l_MalzemeKullanımı[anahtar] += Dönemler_Kullanım[i];
                    if (!l_Dallar.Contains("Mk|" + başlık)) l_Dallar.Add("Mk|" + başlık);
                }
            }

            EnEskiTarih = new DateTime(EnEskiTarih.Year, EnEskiTarih.Month, 15);
            DateTime şimdi = DateTime.Now.AddMonths(1);
            l_Dallar = l_Dallar.Distinct().OrderBy(x => x).ToList();

            double[][] tümü = new double[l_Dallar.Count][];
            DateTime[] Tarihler = new DateTime[1024];
            for (int i = 0; i < l_Dallar.Count; i++) tümü[i] = new double[Tarihler.Length];
            int tümü_sayac = 1, konum_l_dallar;
            while (EnEskiTarih < şimdi && Ortak.Gösterge.Çalışsın)
            {
                //daralan dizinin genişletilmesi
                if (Tarihler.Length <= tümü_sayac)
                {
                    int yeni_adet = Tarihler.Length + 1024;

                    Array.Resize(ref Tarihler, yeni_adet);

                    for (int i = 0; i < tümü.Length && Ortak.Gösterge.Çalışsın; i++)
                    {
                        Array.Resize(ref tümü[i], yeni_adet);
                    }
                }

                string yıl_ay = "|" + EnEskiTarih.ToString("yyyy M");

                KeyValuePair<string, double>[] bulunanlar = l_MüşteriÖdemeleri.Where(x => x.Key.EndsWith(yıl_ay)).ToArray();
                foreach (KeyValuePair<string, double> biri in bulunanlar)
                {
                    if (!Ortak.Gösterge.Çalışsın) break;

                    if (biri.Key.DoluMu())
                    {
                        //Müşteri Ödemeleri (₺) - Mö
                        //string anahtar = Müşteri + "|" + Ödeme_Tarihi_yıl_ay;
                        başlık = "Mö|" + biri.Key.Split('|')[0];
                        konum_l_dallar = l_Dallar.IndexOf(başlık);
                        tümü[konum_l_dallar][tümü_sayac] = biri.Value;

                        l_MüşteriÖdemeleri.Remove(biri.Key);
                    }
                }

                bulunanlar = l_İşTürleri_GenelKullanım.Where(x => x.Key.EndsWith(yıl_ay)).ToArray();
                foreach (KeyValuePair<string, double> biri in bulunanlar)
                {
                    if (!Ortak.Gösterge.Çalışsın) break;

                    if (biri.Key.DoluMu())
                    {
                        //İş Türleri|Genel Kullanım (adet) - İg
                        //anahtar = İşTürü + "|" + Ödeme_Tarihi_yıl_ay;
                        başlık = "İg|" + biri.Key.Split('|')[0];
                        konum_l_dallar = l_Dallar.IndexOf(başlık);
                        tümü[konum_l_dallar][tümü_sayac] = biri.Value;

                        l_İşTürleri_GenelKullanım.Remove(biri.Key);
                    }
                }

                bulunanlar = l_İşTürleri_MüşterilereGöreKullanım.Where(x => x.Key.EndsWith(yıl_ay)).ToArray();
                foreach (KeyValuePair<string, double> biri in bulunanlar)
                {
                    if (!Ortak.Gösterge.Çalışsın) break;

                    if (biri.Key.DoluMu())
                    {
                        //İş Türleri|Müşterilere Göre Kullanım (adet) - İm
                        //anahtar = Müşteri + "|" + İşTürü + "|" + Ödeme_Tarihi_yıl_ay;
                        başlıklar = biri.Key.Split('|');
                        başlık = "İm|" + başlıklar[0] + "|" + başlıklar[1];
                        konum_l_dallar = l_Dallar.IndexOf(başlık);
                        tümü[konum_l_dallar][tümü_sayac] = biri.Value;

                        l_İşTürleri_MüşterilereGöreKullanım.Remove(biri.Key);
                    }
                }
                
                bulunanlar = l_MalzemeKullanımı.Where(x => x.Key.EndsWith(yıl_ay)).ToArray();
                foreach (KeyValuePair<string, double> biri in bulunanlar)
                {
                    if (!Ortak.Gösterge.Çalışsın) break;

                    if (biri.Key.DoluMu())
                    {
                        //Malzeme Kullanımı - Mk
                        //string anahtar = Malzeme + (Birimi.DoluMu() ? " (" + Birimi + ")" : null) + "|" + Dönemler[i].ToString("yyyy M");
                        başlık = "Mk|" + biri.Key.Split('|')[0];
                        konum_l_dallar = l_Dallar.IndexOf(başlık);
                        tümü[konum_l_dallar][tümü_sayac] = biri.Value;

                        l_MalzemeKullanımı.Remove(biri.Key);
                    }
                }

                Tarihler[tümü_sayac] = EnEskiTarih;
                tümü_sayac++;
                EnEskiTarih = EnEskiTarih.AddMonths(1);
            }

            if (l_MüşteriÖdemeleri.Count > 0 ||
                l_İşTürleri_GenelKullanım.Count > 0 ||
                l_İşTürleri_MüşterilereGöreKullanım.Count > 0 ||
                l_MalzemeKullanımı.Count > 0) throw new Exception("Bütçe/Rapor/En az 1 adet kullanılmayan bilgi kaldı");

            string DosyaAdı = Path.GetRandomFileName();
            while (File.Exists(Ortak.Klasör_Gecici + DosyaAdı)) DosyaAdı = Path.GetRandomFileName();
            DosyaAdı = Ortak.Klasör_Gecici + DosyaAdı + ".csv";
            şimdi = DateTime.Now;
            using (FileStream fs = File.OpenWrite(DosyaAdı))
            {
                //27.02.2023 17:44:57.701;Başlıklar;Başlık1;... 
                başlık = şimdi.Yazıya() + ";Başlıklar";
                for (int i = 0; i < l_Dallar.Count && Ortak.Gösterge.Çalışsın; i++)
                {
                    başlıklar = l_Dallar[i].Split('|');
                    if (başlıklar[0] == "Mö") başlık += ";Müşteri Ödemeleri (₺)|" + başlıklar[1];
                    else if (başlıklar[0] == "İg") başlık += ";İş Türleri|Genel Kullanım (adet)|" + başlıklar[1];
                    else if (başlıklar[0] == "İm") başlık += ";İş Türleri|Müşterilere Göre Kullanım (adet)|" + başlıklar[1] + "|" + başlıklar[2];
                    else if (başlıklar[0] == "Mk") başlık += ";Malzeme Kullanımı|" + başlıklar[1];
                }
                byte[] içerik = (başlık + Environment.NewLine).BaytDizisine();
                fs.Write(içerik, 0, içerik.Length);

                Ortak.Gösterge.Başlat("Kaydediliyor", true, Sekmeler, tümü_sayac);
                for (int y = 1; y < tümü_sayac && Ortak.Gösterge.Çalışsın; y++)
                {
                    Ortak.Gösterge.İlerleme = 1;

                    //27.02.2023 17:43:53.833;Sinyaller;Sinyal1;...
                    başlık = Tarihler[y].Yazıya() + ";Sinyaller";
                    for (int x = 0; x < l_Dallar.Count && Ortak.Gösterge.Çalışsın; x++)
                    {
                        başlık += ";" + tümü[x][y].Yazıya();
                    }

                    içerik = (başlık + Environment.NewLine).BaytDizisine();
                    fs.Write(içerik, 0, içerik.Length);
                }

                //27.02.2023 17:46:17.005;Bilgi;ArGeMuP uygulama V0.20
                başlık = şimdi.Yazıya() + ";Bilgi;ArGeMuP " + Kendi.Adı + " " + Kendi.Sürüm;
                içerik = (başlık + Environment.NewLine).BaytDizisine();
                fs.Write(içerik, 0, içerik.Length);
            }

            if (!yyk.KontrolTamamlandı)
            {
                Ortak.Gösterge.Başlat("Çizelgeç indiriliyor", true, Sekmeler, 15);
                tümü_sayac = 15;
                while (!yyk.KontrolTamamlandı && Ortak.Gösterge.Çalışsın && --tümü_sayac > 0)
                {
                    Ortak.Gösterge.İlerleme = 1;
                    System.Threading.Thread.Sleep(1000);
                }
            }

            if (!yyk.KontrolTamamlandı || !File.Exists(çizelgeç_dosyayolu)) System.Diagnostics.Process.Start("explorer.exe", "/select, " + DosyaAdı);
            else System.Diagnostics.Process.Start(çizelgeç_dosyayolu, "\"" + DosyaAdı + "\"");

            yyk.Durdur();
            Ortak.Gösterge.Bitir();
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

            if (e.ColumnIndex >= 1 && e.ColumnIndex <= 3) _2_Kaydet.Enabled = true;

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

        private void _2_Kaydet_Click(object sender, EventArgs e)
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

            Banka.Değişiklikleri_Kaydet(_2_Kaydet);
            _2_Kaydet.Enabled = false;
        }
        #endregion

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
