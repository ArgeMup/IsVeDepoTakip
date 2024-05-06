﻿using ArgeMup.HazirKod;
using ArgeMup.HazirKod.Ekİşlemler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip.Ekranlar
{
    public partial class Ayarlar_Bütçe : Form
    {
        struct Bütçe_Gelir_Gider_
        {
            public double gelir_devameden, gelir_teslimedilen, gelir_ödemebekleyen;
            public double gider_devameden, gider_teslimedilen, gider_ödemebekleyen;
            public double ÖnÖdeme;
            public string müşteri, gelir_ipucu, gider_ipucu;
        };
        Bütçe_Gelir_Gider_[] _1_Dizi = null;

        public Ayarlar_Bütçe()
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
            if (Sekmeler.SelectedTab == Sekmeler_GenelAnlamda)
            {
                //genel anlamda

                //Müşteriler kapsamında yazısını ara
                int Müşteriler_kapsamında = -1;
                for (int i = 0; i < _2_Tablo.RowCount; i++)
                {
                    if ((string)_2_Tablo[_2_Tablo_Açıklama.Index, i].Value == "Müşteriler kapsamında")
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
            else if (Sekmeler.SelectedTab == Sekmeler_GelirGiderTakip)
            {
                IDepo_Eleman Ayarlar = Banka.Ayarlar_BilgisayarVeKullanıcı("GelirGiderTakip", true);
                _4_EpostaGönderimi_Saat.Value = Ayarlar.Oku_TamSayı(null, 18, 0);
                _4_EpostaGönderimi_YazdırmaŞablonu.Text = Ayarlar.Oku(null, null, 1);
                _4_EpostaGönderimi_Kişiler.Text = Ayarlar.Oku(null, null, 2);
                ÖnYüzler_Kaydet_4_Kaydet.Enabled = false;
            }
        }
        #region _1_
        private void _1_Tablo_DoubleClick(object sender, EventArgs e)
        {
            if (_1_Tablo.RowCount < 1) return;
            bool b = !(bool)_1_Tablo[_1_Tablo_Seç.Index, 0].Value;

            for (int i = 0; i < _1_Tablo.RowCount; i++)
            {
                if (!_1_Tablo.Rows[i].Visible) continue;
                _1_Tablo[_1_Tablo_Seç.Index, i].Value = b;
            }

            _1_Hesapla(null, null);
        }
        private void _1_Tablo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (e.ColumnIndex == 0)
            {
                //Seçme sutunu
                _1_Tablo[_1_Tablo_Seç.Index, e.RowIndex].Value = !(bool)_1_Tablo[_1_Tablo_Seç.Index, e.RowIndex].Value;

                _1_Hesapla(null, null);
            }
            else if (e.ColumnIndex == 5)
            {
                //Listele tuşu
                _3_Başlat((string)_1_Tablo[_1_Tablo_Müşteri.Index, e.RowIndex].Value);
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
                Ortak.Gösterge.Bitir();

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
                    gg.ÖnÖdeme = Banka.Müşteri_ÖnÖdemeMiktarı(gg.müşteri);
                    _1_Dizi[i] = gg;
                }

                _1_Tablo.Rows.Clear();
                _1_Tablo.RowCount = _1_Dizi.Length;
                for (int i = 0; i < _1_Tablo.RowCount; i++)
                {
                    _1_Tablo[_1_Tablo_Seç.Index, i].Value = true;
                    _1_Tablo[_1_Tablo_Müşteri.Index, i].Value = _1_Dizi[i].müşteri;

                    _1_Tablo[_1_Tablo_Ödeme.Index, i].Value = Banka.Yazdır_Ücret(_1_Dizi[i].ÖnÖdeme);
                    if (_1_Dizi[i].ÖnÖdeme > 0) _1_Tablo[_1_Tablo_Ödeme.Index, i].Style.BackColor = System.Drawing.Color.Green;
                    else if (_1_Dizi[i].ÖnÖdeme < 0) _1_Tablo[_1_Tablo_Ödeme.Index, i].Style.BackColor = System.Drawing.Color.Red;
                }
                Ortak.Gösterge.Bitir();
            }

            //talep edilen müşterilerin talep edilen değerlerini hesapla
            double top_önödeme = 0;
            for (int i = 0; i < _1_Tablo.RowCount; i++)
            {
                Bütçe_Gelir_Gider_ gg = _1_Dizi.First(x => x.müşteri == (string)_1_Tablo[_1_Tablo_Müşteri.Index, i].Value);

                double top_gelir = 0, top_gider = 0;
                if ((bool)_1_Tablo[_1_Tablo_Seç.Index, i].Value)
                {
                    if (_1_Gelir_DevamEden.Checked) top_gelir += gg.gelir_devameden;
                    if (_1_Gelir_TeslimEdildi.Checked) top_gelir += gg.gelir_teslimedilen;
                    if (_1_Gelir_ÖdemeTalepEdildi.Checked) top_gelir += gg.gelir_ödemebekleyen;
                    if (_1_Gider_DevamEden.Checked) top_gider += gg.gider_devameden;
                    if (_1_Gider_TeslimEdildi.Checked) top_gider += gg.gider_teslimedilen;
                    if (_1_Gider_ÖdemeTalepEdildi.Checked) top_gider += gg.gider_ödemebekleyen;
                    top_önödeme += gg.ÖnÖdeme;
                }

                _1_Tablo[_1_Tablo_Gelir.Index, i].Value = top_gelir;
                _1_Tablo[_1_Tablo_Gelir.Index, i].ToolTipText = gg.gelir_ipucu;

                _1_Tablo[_1_Tablo_Gider.Index, i].Value = top_gider;
                _1_Tablo[_1_Tablo_Gider.Index, i].ToolTipText = gg.gider_ipucu;

                _1_Tablo[_1_Tablo_Fark.Index, i].Value = top_gelir - top_gider;
            }
            top_önödeme *= -1;

            //talep edilenlerin çıktılarını topla
            double gel = 0, gid = 0, fark = 0;
            for (int i = 0; i < _1_Dizi.Length; i++)
            {
                gel += (double)_1_Tablo[_1_Tablo_Gelir.Index, i].Value;
                gid += (double)_1_Tablo[_1_Tablo_Gider.Index, i].Value;
                fark += (double)_1_Tablo[_1_Tablo_Fark.Index, i].Value;
            }

            //çıktıları yazdır
            _1_AltToplam.Text = "Alt Toplam   /   " +
                "Gelir : " + Banka.Yazdır_Ücret(gel) + "   /   " +
                "Gider : " + Banka.Yazdır_Ücret(gid) + "   /   " +
                "Fark : " + Banka.Yazdır_Ücret(fark) + "   /   " +
                "Alacak : " + Banka.Yazdır_Ücret(top_önödeme) + "   /   " +
                "Genel Toplam : " + Banka.Yazdır_Ücret(fark + top_önödeme);

            _1_Gelir.Tag = gel;
            _1_Gider.Tag = gid;

            _1_AltToplam.BackColor = gel > gid ? System.Drawing.Color.YellowGreen : System.Drawing.Color.Salmon;
        }
        private void _1_Hesapla_2(string Müşteri, Banka_Tablo_ bt, out double Gelir, out double Gider)
        {
            Gelir = 0; Gider = 0;
           
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

            Banka.Müşteri_KDV_İskonto(Müşteri, out bool KDV_Ekle, out double KDV_Yüzde, out bool İskonto_Yap, out double İskonto_Yüzde, out _);
            double İskonto_Hesaplanan = İskonto_Yap ? Gelir / 100 * İskonto_Yüzde : 0;
            double KDV_Hesaplanan = KDV_Ekle ? (Gelir - İskonto_Hesaplanan) / 100 * KDV_Yüzde : 0;
            Gelir = Gelir - İskonto_Hesaplanan + KDV_Hesaplanan;
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
            Ortak.Gösterge.Bitir();

            string çizelgeç_dosyayolu = Klasör.Depolama(Klasör.Kapsamı.Geçici, null, "Çizelgeç", "") + "\\Çizelgeç.exe";
            YeniYazılımKontrolü_ yyk = new YeniYazılımKontrolü_();
            if (Ortak.DosyaGüncelMi(çizelgeç_dosyayolu, 0, 0)) yyk.KontrolTamamlandı = true;
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
                Ortak.Gösterge.Başlat("Kaydediliyor", true, Sekmeler, tümü_sayac);

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
                başlık = şimdi.Yazıya() + ";Bilgi;ArGeMuP " + Kendi.Adı + " " + Kendi.Sürümü_Dosya;
                içerik = (başlık + Environment.NewLine).BaytDizisine();
                fs.Write(içerik, 0, içerik.Length);
                
                Ortak.Gösterge.Bitir();
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
                Ortak.Gösterge.Bitir();
            }

            if (!yyk.KontrolTamamlandı || !File.Exists(çizelgeç_dosyayolu)) Ortak.Çalıştır.DosyaGezginindeGöster(DosyaAdı);
            else Ortak.Çalıştır.UygulamayıDoğrudanÇalıştır(çizelgeç_dosyayolu, new string[] { DosyaAdı });

            yyk.Durdur();
            Ortak.Gösterge.Bitir();
        }
        #endregion

        #region _2_
        private void _2_Tablo_DoubleClick(object sender, EventArgs e)
        {
            if (_2_Tablo.RowCount < 1) return;
            bool b = !(bool)_2_Tablo[_2_Tablo_Seç.Index, 0].Value;

            for (int i = 0; i < _2_Tablo.RowCount; i++)
            {
                if (!_2_Tablo.Rows[i].Visible) continue;
                _2_Tablo[_2_Tablo_Seç.Index, i].Value = b;
            }
        }
        private void _2_Tablo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || e.ColumnIndex > 0) return;

            _2_Tablo[_2_Tablo_Seç.Index, e.RowIndex].Value = !(bool)_2_Tablo[_2_Tablo_Seç.Index, e.RowIndex].Value;

            _2_Hesapla(null, null);
        }
        private void _2_Tablo_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex >= 1 && e.ColumnIndex <= 3) ÖnYüzler_Kaydet_2_Kaydet.Enabled = true;

            if (e.ColumnIndex == 0 || e.ColumnIndex == 2 || e.ColumnIndex == 3) _2_Hesapla(null, null);
        }

        private void _2_Hesapla(object sender, EventArgs e)
        {
            double gelir_top = 0, gider_top = 0;

            for (int i = 0; i < _2_Tablo.RowCount; i++)
            {
                double gelir = 0, gider = 0;

                if (!string.IsNullOrEmpty((string)_2_Tablo[_2_Tablo_Gelir.Index, i].Value))
                {
                    string gecici = (string)_2_Tablo[_2_Tablo_Gelir.Index, i].Value;
                    if (!Ortak.YazıyıSayıyaDönüştür(ref gecici,
                        (string)_2_Tablo[_2_Tablo_Açıklama.Index, i].Value + " (gelir sutununun " + (i + 1).ToString() + ". satırı)",
                        "Ücretlendirmek istemiyorsanız boş olarak bırakınız.", 0))
                    {
                        _2_Tablo[_2_Tablo_Gelir.Index, i].Style.BackColor = System.Drawing.Color.Salmon;
                        return;
                    }
                    _2_Tablo[_2_Tablo_Gelir.Index, i].Style.BackColor = System.Drawing.Color.White;

                    _2_Tablo[_2_Tablo_Gelir.Index, i].Value = gecici;
                    gelir = gecici.NoktalıSayıya();
                }

                if (!string.IsNullOrEmpty((string)_2_Tablo[_2_Tablo_Gider.Index, i].Value))
                {
                    string gecici = (string)_2_Tablo[_2_Tablo_Gider.Index, i].Value;
                    if (!Ortak.YazıyıSayıyaDönüştür(ref gecici,
                        (string)_2_Tablo[_2_Tablo_Açıklama.Index, i].Value + " (gider sutununun " + (i + 1).ToString() + ". satırı)",
                        "Ücretlendirmek istemiyorsanız boş olarak bırakınız.", 0))
                    {
                        _2_Tablo[_2_Tablo_Gider.Index, i].Style.BackColor = System.Drawing.Color.Salmon;
                        return;
                    }
                    _2_Tablo[_2_Tablo_Gider.Index, i].Style.BackColor = System.Drawing.Color.White;

                    _2_Tablo[_2_Tablo_Gider.Index, i].Value = gecici;
                    gider = gecici.NoktalıSayıya();
                }

                _2_Tablo[_2_Tablo_Fark.Index, i].Value = (gelir - gider).Yazıya();

                if (_2_Tablo[_2_Tablo_Seç.Index, i].Value == null) _2_Tablo[_2_Tablo_Seç.Index, i].Value = true;
                if ((bool)_2_Tablo[_2_Tablo_Seç.Index, i].Value)
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
                if ((string)_2_Tablo[_2_Tablo_Açıklama.Index, i].Value == "Müşteriler kapsamında") continue;

                Ayarlar_GenelAnlamda.Yaz(i.ToString(), (string)_2_Tablo[_2_Tablo_Açıklama.Index, i].Value, 0);
                Ayarlar_GenelAnlamda.Yaz(i.ToString(), (string)_2_Tablo[_2_Tablo_Gelir.Index, i].Value, 1);
                Ayarlar_GenelAnlamda.Yaz(i.ToString(), (string)_2_Tablo[_2_Tablo_Gider.Index, i].Value, 2);
            }

            Banka.Değişiklikleri_Kaydet(ÖnYüzler_Kaydet_2_Kaydet);
            ÖnYüzler_Kaydet_2_Kaydet.Enabled = false;
        }
        #endregion
        
        #region _3_
        string _3_Müşteri;
        private void _3_Başlat(string Müşteri)
        {
            if (!Banka.Müşteri_MevcutMu(Müşteri)) { _3_Müşteri = null; return; }
            _3_Müşteri = Müşteri;

            Seklemeler_Ödemeler.Text = "Ödemeler - " + _3_Müşteri;
            Sekmeler.SelectedIndex = 2; //ödemeler

            Sekmeler.Enabled = false;
            Banka.Müşteri_Ödemeler_TablodaGöster(_3_Müşteri, _3_Tablo, (int)_3_Görüntüle_Adet.Value);
            Sekmeler.Enabled = true;
        }
        private void _3_Görüntüle_Listele_Click(object sender, EventArgs e)
        {
            _3_Başlat(_3_Müşteri);
        }
        string _3_Yazdır()
        {
            if (_3_Müşteri.BoşMu()) return null;

            ArgeMup.HazirKod.Depo_ depo = Banka.Tablo(_3_Müşteri, Banka.TabloTürü.Ödemeler);
            if (depo == null || depo.Elemanları.Length == 0) return null;

            string dosyayolu = Ortak.Klasör_Gecici + "Ödemeler_" + DateTime.Now.Yazıya(ArgeMup.HazirKod.Dönüştürme.D_TarihSaat.Şablon_DosyaAdı2) + ".pdf";

            Ayarlar_Yazdırma y = new Ayarlar_Yazdırma();
            y.Ödemeler_Yazdır(depo, dosyayolu, (int)_3_Görüntüle_Adet.Value, _3_Yazdırma_NotlarDahil.Checked);
            y.Dispose();

            return dosyayolu;
        }
        private void _3_Yazdırma_Yazdır_Click(object sender, EventArgs e)
        {
            string dosyayolu = _3_Yazdır();
            if (dosyayolu.BoşMu()) return;

            if (!string.IsNullOrEmpty(Ortak.Kullanıcı_Klasör_Pdf) &&
                !Ortak.Klasör_KendiKlasörleriİçindeMi(Ortak.Kullanıcı_Klasör_Pdf))
            {
                string gerçekdosyadı = "Ödemeler_" + DateTime.Now.Yazıya(ArgeMup.HazirKod.Dönüştürme.D_TarihSaat.Şablon_DosyaAdı2) + ".pdf";
                string hedef = Ortak.Kullanıcı_Klasör_Pdf + _3_Müşteri + "\\" + gerçekdosyadı;
                if (!Temkinli.Dosya.Kopyala(dosyayolu, hedef))
                {
                    MessageBox.Show("Üretilen pdf kullanıcı klasörüne kopyalanamadı", Text);
                }
                else
                {
                    if (_3_Yazdırma_VeGörüntüle.Checked) Ortak.Çalıştır.UygulamayaİşletimSistemiKararVersin(hedef);
                    if (_3_Yazdırma_VeKlasörüAç.Checked) Ortak.Çalıştır.DosyaGezginindeGöster(hedef);
                }
            }
            else
            {
                if (_3_Yazdırma_VeGörüntüle.Checked) Ortak.Çalıştır.UygulamayaİşletimSistemiKararVersin(dosyayolu);
                if (_3_Yazdırma_VeKlasörüAç.Checked) Ortak.Çalıştır.DosyaGezginindeGöster(dosyayolu);
            }

            IDepo_Eleman Ayrl_Kullanıcı = Banka.Ayarlar_Kullanıcı(Name, null);
            Ayrl_Kullanıcı.Yaz("_3_Yazdırma_VeGörüntüle", _3_Yazdırma_VeGörüntüle.Checked);
            Ayrl_Kullanıcı.Yaz("_3_Yazdırma_VeKlasörüAç", _3_Yazdırma_VeKlasörüAç.Checked);
        }
        private void _3_Eposta_Kişiye_CheckedChanged(object sender, EventArgs e)
        {
            _3_Eposta_Gönder.Text = _3_Eposta_Kişiye.Checked ? "Kişiye Gönder" : "Müşteriye Gönder";
        }
        private void _3_Eposta_Gönder_Click(object sender, EventArgs e)
        {
            if (_3_Müşteri.BoşMu()) return;

            if (!Eposta.BirEpostaHesabıEklenmişMi)
            {
                MessageBox.Show("Bir eposta hesabı tanımlanması gerekmektedir." + Environment.NewLine + "Ana Ekran - Ayarlar - E-posta sayfasınından ayarlar gözden geçirilebilir", Text);
                return;
            }

            string FirmaİçiKişiler = null;
            if (_3_Eposta_Kişiye.Checked)
            {
                IDepo_Eleman fik = Banka.Ayarlar_Genel("Eposta/Firma İçi Kişiler");
                if (fik != null) FirmaİçiKişiler = fik.Oku(null);

                if (FirmaİçiKişiler.BoşMu(true))
                {
                    MessageBox.Show("Firma içi kişi adresi bulunamadı" + Environment.NewLine + "Ana Ekran - Ayarlar - Eposta sayfasını kullanabilirsiniz", Text);
                    return;
                }
            }
            else
            {
                IDepo_Eleman m = Banka.Ayarlar_Müşteri(_3_Müşteri, "Eposta");
                if (m == null || string.IsNullOrEmpty(m.Oku("Kime") + m.Oku("Bilgi") + m.Oku("Gizli")))
                {
                    MessageBox.Show("Müşteriye tanımlı e-posta adresi bulunamadı" + Environment.NewLine + "Ana Ekran - Ayarlar - Müşteriler sayfasını kullanabilirsiniz", Text);
                    return;
                }
            }

            string dosyayolu = _3_Yazdır();
            if (dosyayolu.BoşMu())
            {
                MessageBox.Show("Hiç kayıt bulunamadı", Text);
                return;
            }

            DialogResult Dr = MessageBox.Show("Oluşturulan belge e-posta yoluyla gönderilecek" +
            Environment.NewLine + Environment.NewLine +
            _3_Müşteri + Environment.NewLine + Environment.NewLine +
            FirmaİçiKişiler + Environment.NewLine + Environment.NewLine +
            "Devam etmek için Evet tuşuna basınız", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (Dr == DialogResult.No) return;

            if (_3_Eposta_Kişiye.Checked) Eposta.Gönder_Kişiye(FirmaİçiKişiler, _3_Müşteri, new string[] { dosyayolu }, _GeriBildirimİşlemei_Tamamlandı);
            else Eposta.Gönder_Müşteriye(_3_Müşteri, new string[] { dosyayolu }, _GeriBildirimİşlemei_Tamamlandı);
            void _GeriBildirimİşlemei_Tamamlandı(string Sonuç)
            {
                if (!string.IsNullOrEmpty(Sonuç)) MessageBox.Show(Sonuç, Text);
            }
        }
        #endregion

        #region _4_ Gelir Gider Takip
        private void _4_AyarlarSayfasınıAç_Click(object sender, EventArgs e)
        {
            GelirGiderTakip.Komut_SayfaAç(GelirGiderTakip.Şube_Talep_Komut_.Sayfa_Ayarlar);
        }
        private void _4_CariDökümSayfasınıAç_Click(object sender, EventArgs e)
        {
            GelirGiderTakip.Komut_SayfaAç(GelirGiderTakip.Şube_Talep_Komut_.Sayfa_CariDöküm);
        }
        private void _4_Yazdır_Click(object sender, EventArgs e)
        {
            string dsy = Ortak.Klasör_Gecici + "Cari_Döküm_" + DateTime.Now.Yazıya(ArgeMup.HazirKod.Dönüştürme.D_TarihSaat.Şablon_DosyaAdı2) + ".pdf";
            string snç = GelirGiderTakip.Komut_Yazdır(dsy, _4_EpostaGönderimi_YazdırmaŞablonu.Text);
            if (snç.BoşMu()) Ortak.Çalıştır.UygulamayaİşletimSistemiKararVersin(dsy);
            else MessageBox.Show(snç, Text);
        }
        private void _4_EpostaGönderimi_Ayar_Değişti(object sender, EventArgs e)
        {
            ÖnYüzler_Kaydet_4_Kaydet.Enabled = true;
        }
        private void _4_EpostaGönderimi_Dene_Click(object sender, EventArgs e)
        {
            Ayarlar_Bütçe.CariDökümüHergünEpostaİleGönder_Hatırlatıcı_GerBildirimİşlemi(null, null);
        }
        private void ÖnYüzler_Kaydet_4_Kaydet_Click(object sender, EventArgs e)
        {
            IDepo_Eleman Ayarlar = Banka.Ayarlar_BilgisayarVeKullanıcı("GelirGiderTakip", true);
            Ayarlar.Yaz(null, (int)_4_EpostaGönderimi_Saat.Value, 0);
            Ayarlar.Yaz(null, _4_EpostaGönderimi_YazdırmaŞablonu.Text, 1);
            Ayarlar.Yaz(null, _4_EpostaGönderimi_Kişiler.Text, 2);

            Banka.Değişiklikleri_Kaydet(ÖnYüzler_Kaydet_4_Kaydet);
            ÖnYüzler_Kaydet_4_Kaydet.Enabled = false;

            CariDökümüHergünEpostaİleGönder_Başlat();
        }
        public static bool CariDökümüHergünEpostaİleGönder_Başlat()
        {
            IDepo_Eleman Ayarlar = Banka.Ayarlar_BilgisayarVeKullanıcı("GelirGiderTakip");

            if (Ayarlar != null &&
                Ayarlar.Oku(null, null, 2).DoluMu() && //kişiler
                Eposta.BirEpostaHesabıEklenmişMi)
            {
                DateTime t = DateTime.Now;
                t = new DateTime(t.Year, t.Month, t.Day, Ayarlar.Oku_TamSayı(null), 0, 0);

                if (Ortak.Hatırlatıcı == null) Ortak.Hatırlatıcı = new ArgeMup.HazirKod.ArkaPlan.Hatırlatıcı_();
                Ortak.Hatırlatıcı.Sil("GelirGiderTakip Eposta Gönderimi");
                Ortak.Hatırlatıcı.Ekle("GelirGiderTakip Eposta Gönderimi", t, null, CariDökümüHergünEpostaİleGönder_Hatırlatıcı_GerBildirimİşlemi, null, true);
                return true;
            }
            else
            {
                Ortak.Hatırlatıcı?.AyarlarıOku(true);
                Ortak.Hatırlatıcı = null;
                return false;
            }
        }
        static int CariDökümüHergünEpostaİleGönder_Hatırlatıcı_GerBildirimİşlemi(string TakmaAdı, object Hatırlatıcı)
        {
            if(CariDökümüHergünEpostaİleGönder_Başlat())
            {
                IDepo_Eleman Ayarlar = Banka.Ayarlar_BilgisayarVeKullanıcı("GelirGiderTakip");

                string[] ek = new string[] { Ortak.Klasör_Gecici + "Cari_Döküm_" + DateTime.Now.Yazıya(ArgeMup.HazirKod.Dönüştürme.D_TarihSaat.Şablon_DosyaAdı2) + ".pdf" };
                string snç = GelirGiderTakip.Komut_Yazdır(ek[0], Ayarlar.Oku(null, null, 1 /*şablon*/));
                snç.Günlük("_4_EpostaGönder Aşama 1 ");
                if (File.Exists(ek[0]) && snç.BoşMu() /*herşey yolunda*/) snç = "Güncel durum ekteki gibidir.";
                else { ek = null; snç = "Gelir Gider Takip Yazdırma işlem sonucu " + Environment.NewLine + Environment.NewLine + snç; }
                
                string mesaj = "<h1>Sayın " + Banka.İşyeri_Adı + "</h1>" +
                    "<br>" + snç +
                    "<br><br>İyi çalışmalar.";

                Eposta.Gönder_Kişiye(Ayarlar.Oku(null, null, 2), "Güncel Ödemeler Hk.", mesaj, ek, _GeriBildirimİşlemei_Tamamlandı);
                void _GeriBildirimİşlemei_Tamamlandı(string Sonuç)
                {
                    snç.Günlük("_4_EpostaGönder Aşama 2 ");
                }
            }
            
            return -1;
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
