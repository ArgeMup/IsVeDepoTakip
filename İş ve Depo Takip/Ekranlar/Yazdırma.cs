using System.Drawing.Printing;
using System.Drawing;
using System.Windows.Forms;
using System;
using ArgeMup.HazirKod;
using System.Drawing.Text;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace İş_ve_Depo_Takip.Ekranlar
{
    public partial class Yazdırma : Form
    {
        IDepo_Eleman Ayarlar = null;

        public Yazdırma()
        {
            InitializeComponent();

            Ortak.GeçiciDepolama_PencereKonumları_Oku(this);
        }
        public void Yazdırma_Load(object sender, System.EventArgs e)
        {
            Yazcılar.Items.Clear();
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                Yazcılar.Items.Add(PrinterSettings.InstalledPrinters[i]);
            }
            if (Yazcılar.Items.Count < 1)
            {
                Enabled = false;
                return;
            }

            Ayarlar = Banka.Tablo_Dal(null, Banka.TabloTürü.Ayarlar, "Yazdırma", true);
            string bulunan = "";
            if (string.IsNullOrEmpty(Ayarlar.Oku("Yazıcı")))
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
            Yazcılar.Text = Ayarlar.Oku("Yazıcı", bulunan);

            KarakterKümeleri.Items.Clear();
            foreach (var kk in new InstalledFontCollection().Families)
            {
                KarakterKümeleri.Items.Add(kk.Name);
            }
            KarakterKümeleri.Text = Ayarlar.Oku("Karakterler", "Calibri");

            KenarBoşluğu.Value = (decimal)Ayarlar.Oku_Sayı("Yazıcı", 15, 1);
            DosyayaYazdır.Checked = Ayarlar.Oku_Bit("Yazıcı", true, 2);
            KarakterBüyüklüğü_Müşteri.Value = (decimal)Ayarlar.Oku_Sayı("Karakterler/Müşteri", 12);
            KarakterBüyüklüğü_Başlıklar.Value = (decimal)Ayarlar.Oku_Sayı("Karakterler/Başlık", 10);
            KarakterBüyüklüğü_Diğerleri.Value = (decimal)Ayarlar.Oku_Sayı("Karakterler/Diğer", 8);
            FirmaLogo_Genişlik.Value = (decimal)Ayarlar.Oku_Sayı("Firma Logosu", 30, 0);
            FirmaLogo_Yükseklik.Value = (decimal)Ayarlar.Oku_Sayı("Firma Logosu", 15, 1);

            KarakterKümeleri.SelectedIndexChanged += Ayar_Değişti;
            DosyayaYazdır.CheckedChanged += Ayar_Değişti;
            KenarBoşluğu.ValueChanged += Ayar_Değişti;
            KarakterBüyüklüğü_Müşteri.ValueChanged += Ayar_Değişti;
            KarakterBüyüklüğü_Başlıklar.ValueChanged += Ayar_Değişti;
            KarakterBüyüklüğü_Diğerleri.ValueChanged += Ayar_Değişti;
            FirmaLogo_Genişlik.ValueChanged += Ayar_Değişti;
            FirmaLogo_Yükseklik.ValueChanged += Ayar_Değişti;

            KeyDown += Yazdırma_Tuş;
            KeyUp += Yazdırma_Tuş;
            MouseWheel += Yazdırma_MouseWheel;
            KeyPreview = true;
        }
        bool ctrl_tuşuna_basıldı = false;
        private void Yazdırma_Tuş(object sender, KeyEventArgs e)
        {
            ctrl_tuşuna_basıldı = e.Control;
        }
        private void Yazdırma_MouseWheel(object sender, MouseEventArgs e)
        {
            if (ctrl_tuşuna_basıldı)
            {
                WindowState = FormWindowState.Normal;
                if (e.Delta > 0) Font = new Font(Font.FontFamily, Font.Size + 0.2f);
                else Font = new Font(Font.FontFamily, Font.Size - 0.2f);
            }
        }
        private void Yazdırma_Shown(object sender, EventArgs e)
        {
            Ayar_Değişti(null, null);
            Kaydet.Enabled = false;
        }
        private void Yazdırma_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Kaydet.Enabled)
            {
                DialogResult Dr = MessageBox.Show("Değişiklikleri kaydetmeden çıkmak istediğinize emin misiniz?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                e.Cancel = Dr == DialogResult.No;
            }
        }
        private void Yazdırma_FormClosed(object sender, FormClosedEventArgs e)
        {
            Ortak.GeçiciDepolama_PencereKonumları_Yaz(this);
        }

        private void Kaydet_Click(object sender, EventArgs e)
        {
            Ayarlar.Yaz("Yazıcı", Yazcılar.Text);
            Ayarlar.Yaz("Karakterler", KarakterKümeleri.Text);
            Ayarlar.Yaz("Yazıcı", (double)KenarBoşluğu.Value, 1);
            Ayarlar.Yaz("Yazıcı", DosyayaYazdır.Checked, 2);
            Ayarlar.Yaz("Karakterler/Müşteri", (double)KarakterBüyüklüğü_Müşteri.Value);
            Ayarlar.Yaz("Karakterler/Başlık", (double)KarakterBüyüklüğü_Başlıklar.Value);
            Ayarlar.Yaz("Karakterler/Diğer", (double)KarakterBüyüklüğü_Diğerleri.Value);
            Ayarlar.Yaz("Firma Logosu", (double)FirmaLogo_Genişlik.Value, 0);
            Ayarlar.Yaz("Firma Logosu", (double)FirmaLogo_Yükseklik.Value, 1);

            Banka.Değişiklikleri_Kaydet();
            Kaydet.Enabled = false;
        }
        private void Ayar_Değişti(object sender, EventArgs e)
        {
            Kaydet.Enabled = true;

            Depo_ Depo = new Depo_();
            Depo.Yaz("Müşteri", "Örnek Müşteri Adı");

            int örnek_iş_sayısı = 0;
            DateTime t = DateTime.Now.AddYears(-1);
            for (int i = 1; i <= 35; i++) //talep sayısı
            {
                if (++örnek_iş_sayısı > 6) örnek_iş_sayısı = 1;

                Depo.Yaz("Talepler/SeriNo" + i, "Örnek Hasta Adı " + i, 0);
                if (örnek_iş_sayısı == 1 || örnek_iş_sayısı == 5) Depo.Yaz("Talepler/SeriNo" + i, i, 1);

                for (int ii = 1; ii <= örnek_iş_sayısı; ii++)
                {
                    Depo.Yaz("Talepler/SeriNo" + i + "/" + ii, "Örnek İş Türü " + ii, 0);
                    Depo.Yaz("Talepler/SeriNo" + i + "/" + ii, t, 1);
                    Depo.Yaz("Talepler/SeriNo" + i + "/" + ii, i * ii * 10, 2);

                    t = t.AddDays(1);
                }
            }
            Depo.Yaz("Ödeme", t, 1);
            Depo.Yaz("Ödeme", "Fatura No : ASDF123466", 2);
            Depo.Yaz("Ödeme/Alt Toplam", 123456789);
            Depo.Yaz("Ödeme/İlave Ödeme", "Örnek İlave ödeme açıklaması", 0);
            Depo.Yaz("Ödeme/İlave Ödeme", 35.79, 1);

            Yazdır_Depo(Depo);
        }

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
        }
        class Bir_Yazı_
        {
            public SizeF Boyut;
            public string Yazı;
        }
        class Bir_Satır_Bilgi_
        {
            public Bir_Yazı_ Hasta = new Bir_Yazı_(), İş = new Bir_Yazı_();
            public float EnYüksek_Yükseklik;
        }
        class Bir_Sayfa_
        {
            public float Sol, Üst, Genişlik, Yükseklik, Yükseklik_YazılarİçinKullanılabilir;
            public float Sutun_Hasta_Genişlik, Sutun_İş_Genişlik, Sutun_ÖdemeAçıklama_Genişik;
            public Font KaKü_Müşteri, KaKü_Başlık, KaKü_Diğer;

            public List<Bir_Satır_Bilgi_> Yazılar = new List<Bir_Satır_Bilgi_>();

            public Bir_Yazı_ SonrakiSayfaYazısı = new Bir_Yazı_();
            public float YazılarİçinToplamYükseklik = 0;
            public int ŞimdikiSayfaSayısı = 1, ToplamSayfaSayısı = 0;

            public Bir_Yazı_ NotlarYazısı = null, ÖdendiğiTarihYazısı = null;
            public List<Bir_Satır_Bilgi_> Ödemeler = new List<Bir_Satır_Bilgi_>();
            public float Ödemeler_Notlar_Yükseklik = 0;
        }
        void Hesaplat(Bir_Sayfa_ Sayfa, Depo_ Depo, Graphics Grafik)
        {
            SizeF s = new SizeF(100, 100);//a4 ten büyük
            List<float> HastaAdları = new List<float>();
            List<float> İşler = new List<float>();
            //List<float> Ücretler = new List<float>();

            float genişlik_boşluk = Grafik.MeasureString("ZT", Sayfa.KaKü_Diğer, s).Width;

            IDepo_Eleman l = Depo.Bul("Talepler");
            foreach (IDepo_Eleman sn in l.Elemanları)
            {
                double ücret = 0;
                Banka.Talep_Ayıkla_İş(sn, out string Hasta, out string İşler_Tümü, ref ücret);
                Bir_Satır_Bilgi_ a = new Bir_Satır_Bilgi_();
                a.Hasta.Yazı = Hasta;
                a.İş.Yazı = İşler_Tümü;
                Sayfa.Yazılar.Add(a);

                HastaAdları.Add(Grafik.MeasureString(Hasta, Sayfa.KaKü_Diğer, s).Width + genişlik_boşluk);
                İşler.Add(Grafik.MeasureString(İşler_Tümü, Sayfa.KaKü_Diğer, s).Width + genişlik_boşluk);

                //Ücretler.Add(Grafik.MeasureString(ücret.Yazıya(), KarakterKümesi, s).Width + genişlik_boşluk + genişlik_boşluk);
            }
            //Banka.Talep_Ayıkla_Ödeme()

            float EnGeniş_HastaAdı = HastaAdları.Max();
            float EnGeniş_İş = İşler.Max();
            float EnGeniş_Ücret = 0/*Ücretler.Max()*/;
            float ücret_hariç_genişlik = Sayfa.Genişlik - EnGeniş_Ücret;
            float fark = ücret_hariç_genişlik - (EnGeniş_HastaAdı + EnGeniş_İş);
            if (fark > 0)
            {
                Sayfa.Sutun_Hasta_Genişlik = EnGeniş_HastaAdı + (fark * 0.5f);
                Sayfa.Sutun_İş_Genişlik = EnGeniş_İş + (fark * 0.5f);

                //Sutun_Ücret.Sol = Sutun_İşler.Sol + Sutun_İşler.Genişlik;
                //Sutun_Ücret.Genişlik = ö.EnGeniş_Ücret + (fark * 0.2f);
            }
            else
            {
                fark /= 2;

                Sayfa.Sutun_Hasta_Genişlik = EnGeniş_HastaAdı + fark;
                Sayfa.Sutun_İş_Genişlik = EnGeniş_İş + fark;

                //Sutun_Ücret.Sol = Sutun_İşler.Sol + Sutun_İşler.Genişlik;
                //Sutun_Ücret.Genişlik = (Kullanılabilir_Alan.Sol + Kullanılabilir_Alan.Genişlik) - Sutun_Ücret.Sol;
            }

            //sınırlandırılmış sutun genişliklkerine göre yazıların son gen ve yük hesabı
            SizeF sf_h = new SizeF(Sayfa.Sutun_Hasta_Genişlik, Sayfa.Yükseklik);
            SizeF sf_i = new SizeF(Sayfa.Sutun_İş_Genişlik, Sayfa.Yükseklik);
            foreach (Bir_Satır_Bilgi_ a in Sayfa.Yazılar)
            {
                a.Hasta.Boyut = Grafik.MeasureString(a.Hasta.Yazı, Sayfa.KaKü_Diğer, sf_h);
                a.İş.Boyut = Grafik.MeasureString(a.İş.Yazı, Sayfa.KaKü_Diğer, sf_i);

                a.EnYüksek_Yükseklik = a.İş.Boyut.Height;
                if (a.EnYüksek_Yükseklik < a.Hasta.Boyut.Height) a.EnYüksek_Yükseklik = a.Hasta.Boyut.Height;
                Sayfa.YazılarİçinToplamYükseklik += a.EnYüksek_Yükseklik;
            }

            //son sayfanın ölçülmesi
            SizeF sf_ss = new SizeF(Sayfa.Genişlik, Sayfa.Yükseklik);
            l = Depo.Bul("Ödeme");
            if (l != null)
            {
                Banka.Talep_Ayıkla_Ödeme(l, out List<string> Açıklamalar, out List<string> Ücretler, out string _, out string Ödendi, out string Notlar);
                if (!string.IsNullOrEmpty(Notlar))
                {
                    Sayfa.NotlarYazısı = new Bir_Yazı_();
                    Sayfa.NotlarYazısı.Yazı = Notlar;
                    Sayfa.NotlarYazısı.Boyut = Grafik.MeasureString(Sayfa.NotlarYazısı.Yazı, Sayfa.KaKü_Diğer, sf_ss);
                    Sayfa.Ödemeler_Notlar_Yükseklik += Sayfa.NotlarYazısı.Boyut.Height;
                }

                if (!string.IsNullOrEmpty(Ödendi))
                {
                    Sayfa.ÖdendiğiTarihYazısı = new Bir_Yazı_();
                    Sayfa.ÖdendiğiTarihYazısı.Yazı = "Ödendi : " + Ödendi;
                    Sayfa.ÖdendiğiTarihYazısı.Boyut = Grafik.MeasureString(Sayfa.ÖdendiğiTarihYazısı.Yazı, Sayfa.KaKü_Diğer, sf_ss);
                }

                HastaAdları.Clear();
                İşler.Clear();
                for (int i = 0; i < Açıklamalar.Count; i++)
                {
                    Bir_Satır_Bilgi_ a = new Bir_Satır_Bilgi_();
                    a.Hasta.Yazı = Açıklamalar[i];
                    a.Hasta.Boyut = Grafik.MeasureString(a.Hasta.Yazı, Sayfa.KaKü_Diğer, sf_ss);
                    HastaAdları.Add(a.Hasta.Boyut.Width);

                    a.İş.Yazı = Ücretler[i];
                    a.İş.Boyut = Grafik.MeasureString(a.İş.Yazı, Sayfa.KaKü_Diğer, sf_ss);
                    İşler.Add(a.İş.Boyut.Width);

                    Sayfa.Ödemeler.Add(a);

                    Sayfa.Ödemeler_Notlar_Yükseklik += a.Hasta.Boyut.Height;
                }
                EnGeniş_İş = İşler.Max() + (genişlik_boşluk * 2); //en geniş ücret yazısı
                Sayfa.Sutun_ÖdemeAçıklama_Genişik = Sayfa.Genişlik - EnGeniş_İş; //geriye kalan genişliği açıklama kısmına ver
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
        void Yazdır(Bir_Yazı_Yazdırma_Detayları_ y)
        {
            float gecici_sol = y.Sol;
            float gecici_üst = y.Üst;

            if (y.YataydaOrtalanmış) gecici_sol += (y.Genişlik - y.Yazı.Boyut.Width) / 2;
            else if (y.SağaYaslanmış) gecici_sol += (y.Genişlik - y.Yazı.Boyut.Width) + y.Aralık_Sol_Sağ;
            else if (y.Aralık_Sol_Sağ > 0) gecici_sol += y.KarakterKümesi.Height * y.Aralık_Sol_Sağ;

            if (y.DikeydeOrtalanmış) gecici_üst += (y.Yükseklik - y.Yazı.Boyut.Height) / 2;

            RectangleF r = new RectangleF(gecici_sol, gecici_üst, y.Genişlik, y.Yükseklik);
            y.Grafik.DrawString(y.Yazı.Yazı, y.KarakterKümesi, Brushes.Black, r);

            if (y.Çerçeve) y.Grafik.DrawRectangle(y.Kalem, y.Sol, y.Üst, y.Genişlik, y.Yükseklik);
        }
        public void Yazdır_Depo(Depo_ Depo, string DosyaAdı = null)
        {
            Bir_Sayfa_ Sayfa = null;
            PrintDocument pd = new PrintDocument();
            pd.OriginAtMargins = true;

            if (DosyaAdı != null) pd.PrinterSettings.PrintFileName = DosyaAdı;
            else
            {
                pd.EndPrint += Pd_EndPrint;
                Önizleme.Document = pd;
            }

            pd.PrinterSettings.PrintToFile = DosyayaYazdır.Checked;
            pd.PrinterSettings.PrinterName = Yazcılar.Text;
            pd.PrintPage += pd_ÖrnekSayfa;
            if (!pd.PrinterSettings.IsValid)
            {
                MessageBox.Show("Yazıcı kullanılamıyor " + pd.PrinterSettings.PrinterName);
                return;
            }

            if (DosyaAdı != null)
            {
                pd.Print();
                pd.Dispose();
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

                    Hesaplat(Sayfa, Depo, ev.Graphics);
                }

                float YazdırmaKonumu_Üst = Sayfa.Üst, YazdırmaKonumu_Yükseklik = Sayfa.Yükseklik;
                Bir_Yazı_Yazdırma_Detayları_ y = new Bir_Yazı_Yazdırma_Detayları_();
                y.Grafik = ev.Graphics;
                y.Kalem = new Pen(Color.Black, 0.1F) { DashStyle = System.Drawing.Drawing2D.DashStyle.Solid };
                y.Yazı = new Bir_Yazı_();

                //logo
                ev.Graphics.DrawImage(Ortak.Yazdırma_Logo, Sayfa.Sol, Sayfa.Üst, (float)FirmaLogo_Genişlik.Value, (float)FirmaLogo_Yükseklik.Value);

                #region Müşteri
                SizeF s1 = new SizeF(Sayfa.Genişlik - (float)FirmaLogo_Genişlik.Value, (float)FirmaLogo_Yükseklik.Value);
                y.Yazı.Boyut = ev.Graphics.MeasureString("Sayın " + Depo.Oku("Müşteri"), Sayfa.KaKü_Müşteri, s1);
                while (y.Yazı.Boyut.Height > (float)FirmaLogo_Yükseklik.Value)
                {
                    //logo yüksekliğine sığmayan yazının karakterini küçült
                    Sayfa.KaKü_Müşteri = new Font(Sayfa.KaKü_Müşteri.FontFamily, Sayfa.KaKü_Müşteri.Size - 0.5f);

                    y.Yazı.Boyut = ev.Graphics.MeasureString("Sayın " + Depo.Oku("Müşteri"), Sayfa.KaKü_Müşteri, s1);
                }
                
                y.KarakterKümesi = Sayfa.KaKü_Müşteri;
                y.Yazı.Yazı = "Sayın " + Depo.Oku("Müşteri");
                y.Sol = Sayfa.Sol + (float)FirmaLogo_Genişlik.Value;
                y.Üst = Sayfa.Üst;
                y.Genişlik = s1.Width;
                y.Yükseklik = s1.Height;
                Yazdır(y);
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
                y.Genişlik = Sayfa.Sutun_Hasta_Genişlik;
                y.Yükseklik = y.Yazı.Boyut.Height;
                Yazdır(y);

                y.Yazı.Yazı = "İşler";
                y.Yazı.Boyut = ev.Graphics.MeasureString("İşler", y.KarakterKümesi, s1);
                y.Sol = Sayfa.Sol + Sayfa.Sutun_Hasta_Genişlik;
                y.Üst = YazdırmaKonumu_Üst;
                y.Genişlik = Sayfa.Sutun_İş_Genişlik;
                y.Yükseklik = y.Yazı.Boyut.Height;
                Yazdır(y);

                YazdırmaKonumu_Üst += y.Yükseklik;
                YazdırmaKonumu_Yükseklik -= y.Yükseklik;
                #endregion

                #region Hasta + işler
                y.KarakterKümesi = Sayfa.KaKü_Diğer;
                y.YataydaOrtalanmış = false;

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

                    y.Yazı = Sayfa.Yazılar[0].Hasta;
                    y.Sol = Sayfa.Sol;
                    y.Üst = YazdırmaKonumu_Üst;
                    y.Genişlik = Sayfa.Sutun_Hasta_Genişlik;
                    Yazdır(y);

                    y.Yazı = Sayfa.Yazılar[0].İş;
                    y.Sol = Sayfa.Sol + Sayfa.Sutun_Hasta_Genişlik;
                    y.Üst = YazdırmaKonumu_Üst;
                    y.Genişlik = Sayfa.Sutun_İş_Genişlik;
                    Yazdır(y);

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
                    YazdırmaKonumu_Üst = Sayfa.Yükseklik - Sayfa.Ödemeler_Notlar_Yükseklik + Sayfa.Üst;

                    if (Sayfa.NotlarYazısı != null)
                    {
                        y.Yazı = Sayfa.NotlarYazısı;
                        y.Sol = Sayfa.Sol;
                        y.Üst = YazdırmaKonumu_Üst;
                        y.Genişlik = Sayfa.Genişlik;
                        y.Yükseklik = y.Yazı.Boyut.Height;
                        Yazdır(y);

                        YazdırmaKonumu_Üst += y.Yazı.Boyut.Height;
                    }

                    if (Sayfa.ÖdendiğiTarihYazısı != null)
                    {
                        y.Yazı = Sayfa.ÖdendiğiTarihYazısı;
                        y.Sol = Sayfa.Sol;
                        y.Üst = YazdırmaKonumu_Üst;
                        y.Genişlik = Sayfa.Genişlik;
                        y.Yükseklik = y.Yazı.Boyut.Height;
                        y.Çerçeve = false;
                        Yazdır(y);
                    }

                    y.Çerçeve = true;
                    y.YataydaOrtalanmış = false;
                    y.SağaYaslanmış = true;
                    foreach (Bir_Satır_Bilgi_ a in Sayfa.Ödemeler)
                    {
                        y.Yazı = a.Hasta;
                        y.Sol = Sayfa.Sol;
                        y.Üst = YazdırmaKonumu_Üst;
                        y.Genişlik = Sayfa.Sutun_ÖdemeAçıklama_Genişik;
                        y.Yükseklik = y.Yazı.Boyut.Height;
                        Yazdır(y);

                        y.Yazı = a.İş;
                        y.Sol = Sayfa.Sol + y.Genişlik;
                        y.Üst = YazdırmaKonumu_Üst;
                        y.Genişlik = Sayfa.Genişlik - y.Genişlik;
                        Yazdır(y);

                        YazdırmaKonumu_Üst += y.Yükseklik;
                    }
                }

                SonrakiSayfaYazısınıYazdır();
                #endregion

                void SonrakiSayfaYazısınıYazdır()
                {
                    y.YataydaOrtalanmış = false;
                    y.SağaYaslanmış = false;

                    y.Yazı.Yazı = Sayfa.SonrakiSayfaYazısı.Yazı.Replace("_ArGeMuP_", (Sayfa.ŞimdikiSayfaSayısı++).ToString());
                    y.Yazı.Boyut = ev.Graphics.MeasureString(y.Yazı.Yazı, y.KarakterKümesi, new SizeF(Sayfa.Genişlik, Sayfa.SonrakiSayfaYazısı.Boyut.Height));
                    
                    y.Sol = Sayfa.Sol;
                    y.Üst = Sayfa.Üst + Sayfa.Yükseklik - y.Yazı.Boyut.Height;
                    y.Genişlik = Sayfa.Genişlik;
                    y.Yükseklik = y.Yazı.Boyut.Height;
                    Yazdır(y);
                }
            }
            void Pd_EndPrint(object senderr, PrintEventArgs ee)
            {
                Önizleme.Rows = Sayfa.ToplamSayfaSayısı;
            }
        }
    }
}
