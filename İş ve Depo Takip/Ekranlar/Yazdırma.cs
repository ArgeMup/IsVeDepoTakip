using System.Drawing.Printing;
using System.Drawing;
using System.Windows.Forms;
using System;
using ArgeMup.HazirKod;
using System.Drawing.Text;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using ArgeMup.HazirKod.Ekİşlemler;

namespace İş_ve_Depo_Takip.Ekranlar
{
    public partial class Yazdırma : Form
    {
        IDepo_Eleman Ayarlar = null, Ayarlar_Bilgisayar = null;

        public Yazdırma(bool ÖnyüzüGöster = false)
        {
            InitializeComponent();

            Yazcılar.Items.Clear();
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                Yazcılar.Items.Add(PrinterSettings.InstalledPrinters[i]);
            }
            if (Yazcılar.Items.Count < 1)
            {
                Yazcılar.Items.Add("Yazıcı bulunamadı");
                Yazcılar.SelectedIndex = 0;
                Enabled = false;
                return;
            }

            Ayarlar_Bilgisayar = Banka.Ayarlar_BilgisayarVeKullanıcı("Yazdırma", true);
            Ayarlar = Banka.Ayarlar_Genel("Yazdırma", true);
            string bulunan = "";
            if (string.IsNullOrEmpty(Ayarlar_Bilgisayar.Oku("Yazıcı")))
            {
                foreach (string y in Yazcılar.Items)
                {
                    if (y.ToLower().Contains("pdf"))
                    {
                        DosyayaYazdır.Checked = true;
                        bulunan = y;
                        break;
                    }
                }
            }
            if (string.IsNullOrEmpty(bulunan)) bulunan = (string)Yazcılar.Items[0];
            Yazcılar.Text = Ayarlar_Bilgisayar.Oku("Yazıcı", bulunan);

            KarakterKümeleri.Items.Clear();
            foreach (var kk in new InstalledFontCollection().Families)
            {
                KarakterKümeleri.Items.Add(kk.Name);
            }
            KarakterKümeleri.Text = Ayarlar.Oku("Karakterler", "Calibri");

            KenarBoşluğu.Value = (decimal)Ayarlar_Bilgisayar.Oku_Sayı("Yazıcı", 15, 1);
            DosyayaYazdır.Checked = Ayarlar_Bilgisayar.Oku_Bit("Yazıcı", true, 2);
            KarakterBüyüklüğü_Müşteri.Value = (decimal)Ayarlar.Oku_Sayı("Karakterler/Müşteri", 12);
            KarakterBüyüklüğü_Başlıklar.Value = (decimal)Ayarlar.Oku_Sayı("Karakterler/Başlık", 10);
            KarakterBüyüklüğü_Diğerleri.Value = (decimal)Ayarlar.Oku_Sayı("Karakterler/Diğer", 8);
            FirmaLogo_Genişlik.Value = (decimal)Ayarlar.Oku_Sayı("Firma Logosu", 30, 0);
            FirmaLogo_Yükseklik.Value = (decimal)Ayarlar.Oku_Sayı("Firma Logosu", 15, 1);

            if (ÖnyüzüGöster)
            {
                KarakterKümeleri.SelectedIndexChanged += Ayar_Değişti;
                DosyayaYazdır.CheckedChanged += Ayar_Değişti;
                KenarBoşluğu.ValueChanged += Ayar_Değişti;
                KarakterBüyüklüğü_Müşteri.ValueChanged += Ayar_Değişti;
                KarakterBüyüklüğü_Başlıklar.ValueChanged += Ayar_Değişti;
                KarakterBüyüklüğü_Diğerleri.ValueChanged += Ayar_Değişti;
                FirmaLogo_Genişlik.ValueChanged += Ayar_Değişti;
                FirmaLogo_Yükseklik.ValueChanged += Ayar_Değişti;
                Ayar_Değişti(null, null);
            }
            else Hide();

            Kaydet.Enabled = false;
        }

        private void Kaydet_Click(object sender, EventArgs e)
        {
            Ayarlar_Bilgisayar.Yaz("Yazıcı", Yazcılar.Text);
            Ayarlar_Bilgisayar.Yaz("Yazıcı", (double)KenarBoşluğu.Value, 1);
            Ayarlar_Bilgisayar.Yaz("Yazıcı", DosyayaYazdır.Checked, 2);
            Ayarlar.Yaz("Karakterler", KarakterKümeleri.Text);
            Ayarlar.Yaz("Karakterler/Müşteri", (double)KarakterBüyüklüğü_Müşteri.Value);
            Ayarlar.Yaz("Karakterler/Başlık", (double)KarakterBüyüklüğü_Başlıklar.Value);
            Ayarlar.Yaz("Karakterler/Diğer", (double)KarakterBüyüklüğü_Diğerleri.Value);
            Ayarlar.Yaz("Firma Logosu", (double)FirmaLogo_Genişlik.Value, 0);
            Ayarlar.Yaz("Firma Logosu", (double)FirmaLogo_Yükseklik.Value, 1);

            Banka.Değişiklikleri_Kaydet(Kaydet);
            Kaydet.Enabled = false;
        }
        private void Ayar_Değişti(object sender, EventArgs e)
        {
            Kaydet.Enabled = true;

            try
            {
                Yazdır_Depo(Banka.ÖrnekMüşteriTablosuOluştur());
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message, Text);
            }
        }

        #region Doğrudan Yazdırma
        class Bir_Yazı_Yazdırma_Detayları_
        {
            public Graphics Grafik;
            public Pen Kalem;
            public Font KarakterKümesi;
            public Bir_Yazı_ Yazı;

            public float Sol, Üst, Genişlik, Yükseklik;
            public float Aralık_Sol_Sağ = 0.05f;
            public bool DikeydeOrtalanmış = true;
            public bool YataydaOrtalanmış = false;
            public bool SağaYaslanmış = false;
            public bool Çerçeve = true;

            public void Yazdır(Brush YazıRengi = null)
            {
                float gecici_sol = Sol;
                float gecici_üst = Üst;

                if (YataydaOrtalanmış) gecici_sol += (Genişlik - Yazı.Boyut.Width) / 2;
                else if (SağaYaslanmış) gecici_sol += (Genişlik - Yazı.Boyut.Width) + Aralık_Sol_Sağ;
                else if (Aralık_Sol_Sağ > 0) gecici_sol += KarakterKümesi.Height * Aralık_Sol_Sağ;

                if (DikeydeOrtalanmış) gecici_üst += (Yükseklik - Yazı.Boyut.Height) / 2;

                RectangleF r = new RectangleF(gecici_sol, gecici_üst, Genişlik, Yükseklik);
                Grafik.DrawString(Yazı.Yazı, KarakterKümesi, YazıRengi??Brushes.Black, r);

                if (Çerçeve) Grafik.DrawRectangle(Kalem, Sol, Üst, Genişlik, Yükseklik);
            }
        }
        class Bir_Yazı_
        {
            public SizeF Boyut;
            public string Yazı;
        }
        class Bir_Satır_Bilgi_
        {
            public Bir_Yazı_
                SıraNo = new Bir_Yazı_(),
                Hasta = new Bir_Yazı_(),
                İş = new Bir_Yazı_(),
                Ücret = new Bir_Yazı_();
            public float EnYüksek_Yükseklik;
        }
        class Bir_Sayfa_
        {
            public float Sol, Üst, Genişlik, Yükseklik, Yükseklik_YazılarİçinKullanılabilir;
            public float Sutun_SıraNo_Genişlik, Sutun_Hasta_Genişlik, Sutun_İş_Genişlik, Sutun_ÖdemeAçıklama_Genişik, Sutun_Ödeme_Genişik;
            public Font KaKü_Müşteri, KaKü_Başlık, KaKü_Diğer;

            public List<Bir_Satır_Bilgi_> Yazılar = new List<Bir_Satır_Bilgi_>();

            public Bir_Yazı_ SonrakiSayfaYazısı = new Bir_Yazı_();
            public float YazılarİçinToplamYükseklik = 0;
            public int ŞimdikiSayfaSayısı = 1, ToplamSayfaSayısı = 0;

            public Bir_Yazı_ NotlarYazısı = null;
            public List<Bir_Satır_Bilgi_> Ödemeler = new List<Bir_Satır_Bilgi_>();
            public float Ödemeler_Notlar_Yükseklik = 0;
        }
        void Hesaplat(Bir_Sayfa_ Sayfa, Depo_ Depo, Graphics Grafik)
        {
            List<float> HastaAdları = new List<float>();
            List<float> İşler = new List<float>();

            SizeF s = new SizeF(100, 100);//a4 ten büyük
            float genişlik_boşluk = Grafik.MeasureString("Ğ", Sayfa.KaKü_Başlık, s).Width;
            Sayfa.Sutun_Ödeme_Genişik = 0;

            //son sayfanın ölçülmesi
            SizeF sf_ss = new SizeF(Sayfa.Genişlik, Sayfa.Yükseklik);
            IDepo_Eleman l = Depo.Bul("Ödeme");
            if (l != null)
            {
                Banka.Talep_Ayıkla_ÖdemeDalı(l, out List<string> Açıklamalar, out List<string> Ödemeler, out string _, out string Notlar, out _, true);
                if (!string.IsNullOrEmpty(Notlar))
                {
                    Sayfa.NotlarYazısı = new Bir_Yazı_();
                    Sayfa.NotlarYazısı.Yazı = Notlar;
                    Sayfa.NotlarYazısı.Boyut = Grafik.MeasureString(Sayfa.NotlarYazısı.Yazı, Sayfa.KaKü_Diğer, sf_ss);
                    Sayfa.Ödemeler_Notlar_Yükseklik += Sayfa.NotlarYazısı.Boyut.Height;
                }

                for (int i = 0; i < Açıklamalar.Count; i++)
                {
                    Bir_Satır_Bilgi_ a = new Bir_Satır_Bilgi_();
                    a.Hasta.Yazı = Açıklamalar[i];
                    a.Hasta.Boyut = Grafik.MeasureString(a.Hasta.Yazı, Sayfa.KaKü_Başlık, sf_ss);
                    HastaAdları.Add(a.Hasta.Boyut.Width);

                    a.İş.Yazı = Ödemeler[i];
                    a.İş.Boyut = Grafik.MeasureString(a.İş.Yazı, Sayfa.KaKü_Başlık, sf_ss);
                    İşler.Add(a.İş.Boyut.Width);

                    Sayfa.Ödemeler.Add(a);

                    Sayfa.Ödemeler_Notlar_Yükseklik += a.Hasta.Boyut.Height;
                }
                Sayfa.Sutun_Ödeme_Genişik = İşler.Max() + genişlik_boşluk; //en geniş ücret yazısı
                Sayfa.Sutun_ÖdemeAçıklama_Genişik = Sayfa.Genişlik - Sayfa.Sutun_Ödeme_Genişik; //geriye kalan genişliği açıklama kısmına ver
            }

            HastaAdları.Clear();
            İşler.Clear();
            genişlik_boşluk = Grafik.MeasureString("Ğ", Sayfa.KaKü_Diğer, s).Width;
            l = Depo.Bul("Talepler");
            for (int i = 0; i < l.Elemanları.Length; i++)
            {
                double ücret = 0;
                Banka.Talep_Ayıkla_SeriNoDalı(l.Elemanları[i], out string Hasta, out string İşler_Tümü, ref ücret);
                Bir_Satır_Bilgi_ a = new Bir_Satır_Bilgi_();

                a.SıraNo.Yazı = (i + 1).Yazıya();
                a.Hasta.Yazı = Hasta;
                a.İş.Yazı = İşler_Tümü;
                a.Ücret.Yazı = Banka.Yazdır_Ücret(ücret);
                Sayfa.Yazılar.Add(a);

                HastaAdları.Add(Grafik.MeasureString(Hasta, Sayfa.KaKü_Diğer, s).Width + genişlik_boşluk);
                İşler.Add(Grafik.MeasureString(İşler_Tümü, Sayfa.KaKü_Diğer, s).Width + genişlik_boşluk);
            }

            Sayfa.Sutun_SıraNo_Genişlik = Grafik.MeasureString(l.Elemanları.Length.Yazıya(), Sayfa.KaKü_Diğer, s).Width + genişlik_boşluk;
            float EnGeniş_HastaAdı = HastaAdları.Count == 0 ? 0 : HastaAdları.Max();
            float EnGeniş_İş = İşler.Count == 0 ? 0 : İşler.Max();

            float SıraNoÜcret_hariç_genişlik = Sayfa.Genişlik - Sayfa.Sutun_SıraNo_Genişlik - Sayfa.Sutun_Ödeme_Genişik;
            float fark = SıraNoÜcret_hariç_genişlik - (EnGeniş_HastaAdı + EnGeniş_İş);
            if (fark > 0)
            {
                Sayfa.Sutun_Hasta_Genişlik = EnGeniş_HastaAdı + (fark * 0.5f);
                Sayfa.Sutun_İş_Genişlik = EnGeniş_İş + (fark * 0.5f);
            }
            else
            {
                fark /= 2;

                Sayfa.Sutun_Hasta_Genişlik = EnGeniş_HastaAdı + fark;
                Sayfa.Sutun_İş_Genişlik = EnGeniş_İş + fark;
            }

            //sınırlandırılmış sutun genişliklkerine göre yazıların son gen ve yük hesabı
            SizeF sf_sn = new SizeF(Sayfa.Sutun_SıraNo_Genişlik, Sayfa.Yükseklik);
            SizeF sf_h = new SizeF(Sayfa.Sutun_Hasta_Genişlik, Sayfa.Yükseklik);
            SizeF sf_i = new SizeF(Sayfa.Sutun_İş_Genişlik, Sayfa.Yükseklik);
            SizeF sf_ö = new SizeF(Sayfa.Sutun_Ödeme_Genişik, Sayfa.Yükseklik);
            foreach (Bir_Satır_Bilgi_ a in Sayfa.Yazılar)
            {
                a.SıraNo.Boyut = Grafik.MeasureString(a.SıraNo.Yazı, Sayfa.KaKü_Diğer, sf_sn);
                a.Hasta.Boyut = Grafik.MeasureString(a.Hasta.Yazı, Sayfa.KaKü_Diğer, sf_h);
                a.İş.Boyut = Grafik.MeasureString(a.İş.Yazı, Sayfa.KaKü_Diğer, sf_i);
                a.Ücret.Boyut = Grafik.MeasureString(a.Ücret.Yazı, Sayfa.KaKü_Diğer, sf_ö);

                a.EnYüksek_Yükseklik = a.İş.Boyut.Height;
                if (a.EnYüksek_Yükseklik < a.Hasta.Boyut.Height) a.EnYüksek_Yükseklik = a.Hasta.Boyut.Height;
                Sayfa.YazılarİçinToplamYükseklik += a.EnYüksek_Yükseklik;
            }

            //toplam sayfa sayısı hesabı
            List<Bir_Satır_Bilgi_> Yazılar2 = new List<Bir_Satır_Bilgi_>(Sayfa.Yazılar);
            float Kullanılabilir_Yükseklik = Sayfa.Yükseklik_YazılarİçinKullanılabilir - Grafik.MeasureString("ŞÇÖĞ", Sayfa.KaKü_Diğer, sf_ss).Height;
            Sayfa.ToplamSayfaSayısı = 1; float konum = 0;
            while (Yazılar2.Count > 0)
            {
                if (konum + Yazılar2[0].EnYüksek_Yükseklik > Kullanılabilir_Yükseklik)
                {
                    Sayfa.ToplamSayfaSayısı++;
                    konum = 0;
                }
                else
                {
                    konum += Yazılar2[0].EnYüksek_Yükseklik;
                    Yazılar2.RemoveAt(0);
                }
            }

            //Sonraki sayfa yazısının ölçülmesi
            Sayfa.SonrakiSayfaYazısı.Yazı = "Toplam " + Sayfa.Yazılar.Count + " iş, sayfa _ArGeMuP_ / " + Sayfa.ToplamSayfaSayısı + ", #" + System.IO.Path.GetRandomFileName().Replace(".", "");
            Sayfa.SonrakiSayfaYazısı.Boyut = Grafik.MeasureString(Sayfa.SonrakiSayfaYazısı.Yazı, Sayfa.KaKü_Diğer, sf_ss);
        }

        public void Yazdır_Depo(Depo_ Depo, string DosyaAdı = null)
        {
            Bir_Sayfa_ Sayfa = null;
            PrintDocument pd = new PrintDocument();
            pd.PrintController = new StandardPrintController(); //Yazdırılıyor yazısının gizlenmesi
            pd.OriginAtMargins = true;

            if (DosyaAdı != null)
            {
                Klasör.Oluştur(Path.GetDirectoryName(DosyaAdı));
                pd.PrinterSettings.PrintFileName = DosyaAdı;
            }
            else
            {
                pd.EndPrint += Pd_EndPrint;
                Önizleme.Document = pd;
            }

            pd.PrinterSettings.PrintToFile = DosyayaYazdır.Checked;
            pd.PrinterSettings.PrinterName = Yazcılar.Text;
            pd.PrintPage += pd_ÖrnekSayfa;
            if (!pd.PrinterSettings.IsValid) throw new Exception("Yazıcı kullanılamıyor " + pd.PrinterSettings.PrinterName);

            if (DosyaAdı != null)
            {
                pd.Print();
                pd.Dispose();

                if (!Ortak.Dosya_TutmayaÇalış(DosyaAdı)) throw new Exception("Pdf dosyası oluşturulamadı" + Environment.NewLine + DosyaAdı);
            }

            void pd_ÖrnekSayfa(object senderr, PrintPageEventArgs ev)
            {
                //MarginBounds 25,4 25,4 159,258 246,126
                //PageBounds 0 0 210,058 296,926
                //tümü mm olarak
                ev.Graphics.PageUnit = GraphicsUnit.Millimeter;
                ev.Graphics.ResetTransform();
                ev.Graphics.Clear(Color.White);

                if (Sayfa == null)
                {
                    Sayfa = new Bir_Sayfa_();
                    Sayfa.Sol = (ev.PageBounds.X * (float)0.254) + (ev.PageSettings.HardMarginX * (float)0.254) + (float)KenarBoşluğu.Value;
                    Sayfa.Üst = (ev.PageBounds.Y * (float)0.254) + (ev.PageSettings.HardMarginY * (float)0.254) + (float)KenarBoşluğu.Value;
                    Sayfa.Genişlik = (ev.PageBounds.Width * (float)0.254) - (2 * Sayfa.Sol);
                    Sayfa.Yükseklik = (ev.PageBounds.Height * (float)0.254) - (2 * Sayfa.Üst);

                    Sayfa.KaKü_Müşteri = new Font(KarakterKümeleri.Text, (int)KarakterBüyüklüğü_Müşteri.Value, FontStyle.Bold);
                    Sayfa.KaKü_Başlık = new Font(KarakterKümeleri.Text, (int)KarakterBüyüklüğü_Başlıklar.Value, FontStyle.Bold);
                    Sayfa.KaKü_Diğer = new Font(KarakterKümeleri.Text, (int)KarakterBüyüklüğü_Diğerleri.Value);

                    Sayfa.Yükseklik_YazılarİçinKullanılabilir = Sayfa.Yükseklik - (float)FirmaLogo_Yükseklik.Value - ev.Graphics.MeasureString("ŞÇÖĞ", Sayfa.KaKü_Başlık).Height /*Başlık*/;
                    if (Sayfa.Yükseklik_YazılarİçinKullanılabilir <= 0) throw new Exception("Yazıcının kullanılabilir sayfa yüksekliği uygun değil, farklı bir yazıcı seçiniz veya ayarları kontrol ediniz").Günlük();

                    Depo = Banka.YazdırmayaHazırla_İşTürüAdları(Depo);
                    Hesaplat(Sayfa, Depo, ev.Graphics);
                }

                float YazdırmaKonumu_Üst = Sayfa.Üst, YazdırmaKonumu_Yükseklik = Sayfa.Yükseklik;
                Bir_Yazı_Yazdırma_Detayları_ y = new Bir_Yazı_Yazdırma_Detayları_();
                y.Grafik = ev.Graphics;
                y.Kalem = new Pen(Color.Black, 0.1F) { DashStyle = System.Drawing.Drawing2D.DashStyle.Solid };
                y.Yazı = new Bir_Yazı_();

                //logo
                ev.Graphics.DrawImage(Ortak.Firma_Logo, Sayfa.Sol, Sayfa.Üst, (float)FirmaLogo_Genişlik.Value, (float)FirmaLogo_Yükseklik.Value);

                //Yazdırma zamanı
                SizeF s1 = new SizeF(Sayfa.Genişlik - (float)FirmaLogo_Genişlik.Value, (float)FirmaLogo_Yükseklik.Value);
                y.KarakterKümesi = Sayfa.KaKü_Başlık;
                y.Yazı.Yazı = Banka.Yazdır_Tarih(DateTime.Now.Yazıya());
                y.Yazı.Boyut = ev.Graphics.MeasureString(y.Yazı.Yazı, y.KarakterKümesi, s1);
                y.Sol = Sayfa.Sol + (Sayfa.Genişlik - y.Yazı.Boyut.Width);
                y.Üst = Sayfa.Üst;
                y.Genişlik = y.Yazı.Boyut.Width;
                y.Yükseklik = y.Yazı.Boyut.Height;
                y.Çerçeve = false;
                y.Yazdır();

                #region Müşteri
                s1 = new SizeF(Sayfa.Genişlik - (float)FirmaLogo_Genişlik.Value - y.Yazı.Boyut.Width, (float)FirmaLogo_Yükseklik.Value);
                y.Yazı.Yazı = "Sayın " + Depo["Tür", 1];
                y.Yazı.Boyut = ev.Graphics.MeasureString(y.Yazı.Yazı, Sayfa.KaKü_Müşteri, s1);
                while (y.Yazı.Boyut.Height >= (float)FirmaLogo_Yükseklik.Value)
                {
                    //logo yüksekliğine sığmayan yazının karakterini küçült
                    Sayfa.KaKü_Müşteri = new Font(Sayfa.KaKü_Müşteri.FontFamily, Sayfa.KaKü_Müşteri.Size - 0.5f);

                    y.Yazı.Boyut = ev.Graphics.MeasureString(y.Yazı.Yazı, Sayfa.KaKü_Müşteri, s1);
                }

                y.KarakterKümesi = Sayfa.KaKü_Müşteri;
                y.Sol = Sayfa.Sol + (float)FirmaLogo_Genişlik.Value;
                y.Üst = Sayfa.Üst;
                y.Genişlik = s1.Width;
                y.Yükseklik = s1.Height;
                y.Yazdır();
                YazdırmaKonumu_Üst += y.Yükseklik;
                YazdırmaKonumu_Yükseklik -= y.Yükseklik;
                #endregion

                #region Çerçeveler
                //Pen k = new Pen(Color.Red, 0.1f);
                //ev.Graphics.DrawRectangle(k, Sayfa.Sol, YazdırmaKonumu_Üst, Sayfa.Sutun_Hasta_Genişlik, YazdırmaKonumu_Yükseklik); //hastaadı
                //ev.Graphics.DrawRectangle(k, Sayfa.Sol + Sayfa.Sutun_Hasta_Genişlik, YazdırmaKonumu_Üst, Sayfa.Sutun_İş_Genişlik, YazdırmaKonumu_Yükseklik); //işler
                ev.Graphics.DrawRectangle(y.Kalem, Sayfa.Sol, Sayfa.Üst, Sayfa.Genişlik, Sayfa.Yükseklik); //dış çerçeve
                #endregion

                #region Başlıklar
                s1 = new SizeF(Sayfa.Sutun_Hasta_Genişlik, Sayfa.Yükseklik);
                y.KarakterKümesi = Sayfa.KaKü_Başlık;
                y.YataydaOrtalanmış = true;

                y.Yazı.Yazı = "Hasta";
                y.Yazı.Boyut = ev.Graphics.MeasureString("Hasta", y.KarakterKümesi, s1);
                y.Sol = Sayfa.Sol;
                y.Üst = YazdırmaKonumu_Üst;
                y.Genişlik = Sayfa.Sutun_SıraNo_Genişlik + Sayfa.Sutun_Hasta_Genişlik;
                y.Yükseklik = y.Yazı.Boyut.Height;
                y.Çerçeve = true;
                y.Yazdır();

                y.Yazı.Yazı = "İşler";
                y.Yazı.Boyut = ev.Graphics.MeasureString("İşler", y.KarakterKümesi, s1);
                y.Sol = y.Sol + y.Genişlik;
                y.Üst = YazdırmaKonumu_Üst;
                y.Genişlik = Sayfa.Sutun_İş_Genişlik;
                y.Yükseklik = y.Yazı.Boyut.Height;
                y.Yazdır();

                if (Sayfa.Sutun_Ödeme_Genişik > 0)
                {
                    y.Yazı.Yazı = "Ücret";
                    y.Yazı.Boyut = ev.Graphics.MeasureString("Ücret", y.KarakterKümesi, s1);
                    y.Sol = y.Sol + y.Genişlik;
                    y.Üst = YazdırmaKonumu_Üst;
                    y.Genişlik = Sayfa.Sutun_Ödeme_Genişik;
                    y.Yükseklik = y.Yazı.Boyut.Height;
                    y.Yazdır();
                }

                YazdırmaKonumu_Üst += y.Yükseklik;
                YazdırmaKonumu_Yükseklik -= y.Yükseklik;
                #endregion

                #region Hasta + işler
                y.KarakterKümesi = Sayfa.KaKü_Diğer;

                while (Sayfa.Yazılar.Count > 0)
                {
                    if (Sayfa.Yazılar[0].EnYüksek_Yükseklik + Sayfa.SonrakiSayfaYazısı.Boyut.Height > YazdırmaKonumu_Yükseklik)
                    {
                        //daha fazla yazdırılacak iş var, sonraki sayfaya geç
                        SonrakiSayfaYazısınıYazdır();
                        ev.HasMorePages = true;
                        return;
                    }

                    y.Yükseklik = Sayfa.Yazılar[0].EnYüksek_Yükseklik;
                    y.Üst = YazdırmaKonumu_Üst;

                    y.YataydaOrtalanmış = true;
                    y.Yazı = Sayfa.Yazılar[0].SıraNo;
                    y.Sol = Sayfa.Sol;
                    y.Genişlik = Sayfa.Sutun_SıraNo_Genişlik;
                    y.Yazdır();

                    y.YataydaOrtalanmış = false;
                    y.Yazı = Sayfa.Yazılar[0].Hasta;
                    y.Sol = y.Sol + Sayfa.Sutun_SıraNo_Genişlik;
                    y.Genişlik = Sayfa.Sutun_Hasta_Genişlik;
                    y.Yazdır();

                    y.Yazı = Sayfa.Yazılar[0].İş;
                    y.Sol = y.Sol + Sayfa.Sutun_Hasta_Genişlik;
                    y.Genişlik = Sayfa.Sutun_İş_Genişlik;
                    y.Yazdır();

                    if (Sayfa.Sutun_Ödeme_Genişik > 0)
                    {
                        y.YataydaOrtalanmış = true;
                        y.Yazı = Sayfa.Yazılar[0].Ücret;
                        y.Sol = y.Sol + Sayfa.Sutun_İş_Genişlik;
                        y.Genişlik = Sayfa.Sutun_Ödeme_Genişik;
                        y.Yazdır();
                    }

                    YazdırmaKonumu_Üst += y.Yükseklik;
                    YazdırmaKonumu_Yükseklik -= y.Yükseklik;
                    Sayfa.Yazılar.RemoveAt(0);
                }

                if (Sayfa.Ödemeler_Notlar_Yükseklik > YazdırmaKonumu_Yükseklik)
                {
                    //ödemeleri yazdıracak boşluk yok sonraki sayfaya atla
                    ev.HasMorePages = true;
                }
                else
                {
                    //ödemeler yazısını yazdır
                    y.YataydaOrtalanmış = false;
                    YazdırmaKonumu_Üst = Sayfa.Yükseklik - Sayfa.Ödemeler_Notlar_Yükseklik + Sayfa.Üst;

                    if (Sayfa.NotlarYazısı != null)
                    {
                        y.Yazı = Sayfa.NotlarYazısı;
                        y.Sol = Sayfa.Sol;
                        y.Üst = YazdırmaKonumu_Üst;
                        y.Genişlik = Sayfa.Genişlik;
                        y.Yükseklik = y.Yazı.Boyut.Height;
                        y.Yazdır();

                        YazdırmaKonumu_Üst += y.Yazı.Boyut.Height;
                    }

                    y.SağaYaslanmış = true;
                    y.KarakterKümesi = Sayfa.KaKü_Başlık;
                    foreach (Bir_Satır_Bilgi_ a in Sayfa.Ödemeler)
                    {
                        y.Yazı = a.Hasta;
                        y.Sol = Sayfa.Sol;
                        y.Üst = YazdırmaKonumu_Üst;
                        y.Genişlik = Sayfa.Sutun_ÖdemeAçıklama_Genişik;
                        y.Yükseklik = y.Yazı.Boyut.Height;
                        y.Yazdır();

                        y.Yazı = a.İş;
                        y.Sol = Sayfa.Sol + y.Genişlik;
                        y.Üst = YazdırmaKonumu_Üst;
                        y.Genişlik = Sayfa.Genişlik - y.Genişlik;
                        y.Yazdır( a.Hasta.Yazı == "Devreden Tutar" && a.İş.Yazı.StartsWith("-") ? Brushes.Red : Brushes.Black );

                        YazdırmaKonumu_Üst += y.Yükseklik;
                    }
                }

                y.KarakterKümesi = Sayfa.KaKü_Diğer;
                SonrakiSayfaYazısınıYazdır();
                #endregion

                void SonrakiSayfaYazısınıYazdır()
                {
                    y.YataydaOrtalanmış = false;
                    y.SağaYaslanmış = false;
                    y.Çerçeve = false;

                    y.Yazı.Yazı = Sayfa.SonrakiSayfaYazısı.Yazı.Replace("_ArGeMuP_", (Sayfa.ŞimdikiSayfaSayısı++).ToString());
                    y.Yazı.Boyut = ev.Graphics.MeasureString(y.Yazı.Yazı, y.KarakterKümesi, new SizeF(Sayfa.Genişlik, Sayfa.SonrakiSayfaYazısı.Boyut.Height));

                    y.Sol = Sayfa.Sol;
                    y.Üst = Sayfa.Üst + Sayfa.Yükseklik - y.Yazı.Boyut.Height;
                    y.Genişlik = Sayfa.Genişlik;
                    y.Yükseklik = y.Yazı.Boyut.Height;
                    y.Yazdır();
                }
            }
            void Pd_EndPrint(object senderr, PrintEventArgs ee)
            {
                Önizleme.Rows = Sayfa.ToplamSayfaSayısı;
            }
        }
        #endregion
    }
}
