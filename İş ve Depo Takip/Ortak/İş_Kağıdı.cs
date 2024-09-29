using System;
using System.Collections.Generic;
using System.Drawing;

namespace İş_ve_Depo_Takip
{
    class İşKağıdı_Dişler_
    {
        static SizeF KullanılabilirAlan_mm, KullanılabilirAlan_Diş_mm;
        static Size KullanılabilirAlan_piksel_Resim;
        static List<byte> TümDişler;
        static float YakınlaşmaOranı;
        static bool Başladı = false;

        public static void Başlat(float İstenen_Genişlik_mm = 600, float İstenen_YÜkseklik_mm = 80, float YakınlaşmaOranı = 1.0f)
        {
            İşKağıdı_Dişler_.YakınlaşmaOranı = YakınlaşmaOranı;

            KullanılabilirAlan_mm = new SizeF(İstenen_Genişlik_mm, İstenen_YÜkseklik_mm);

            float Çarpan = 25.4f / 96.0f; //ekran dpi oranı https://learn.microsoft.com/en-us/windows/win32/learnwin32/dpi-and-device-independent-pixels
            KullanılabilirAlan_piksel_Resim = new Size((int)(KullanılabilirAlan_mm.Width * YakınlaşmaOranı / Çarpan), (int)(KullanılabilirAlan_mm.Height * YakınlaşmaOranı / Çarpan));

            KullanılabilirAlan_Diş_mm = new SizeF(KullanılabilirAlan_mm.Width / 16, KullanılabilirAlan_mm.Height / 2);
        }

        public static void Yazdır(List<byte> TümDişler, string HedefDosya)
        {
            if (!Başladı)
            {
                Başladı = true;
                Başlat();
            }

            if (TümDişler == null) TümDişler = new List<byte>();

            Image Resim = new Bitmap(KullanılabilirAlan_piksel_Resim.Width, KullanılabilirAlan_piksel_Resim.Height);

            Graphics Grafik = Graphics.FromImage(Resim);
            Grafik.ResetTransform();
            Grafik.PageUnit = GraphicsUnit.Millimeter;
            Görsel_ Görsel = new Görsel_();

            //18-11  21-28  8 er adet
            //48-41  31-38
            PointF SolÜstNokta = new PointF();
            for (byte i = 18; i >= 11; i--)
            {
                Görsel.Yazıİçeriği = TümDişler.Contains(i) ? i.ToString() : "";
                Görsel.Çerçeve = new RectangleF(SolÜstNokta, KullanılabilirAlan_Diş_mm);
                Görsel.Çizdir(Grafik);

                SolÜstNokta.X += KullanılabilirAlan_Diş_mm.Width - Görsel.ÇerçveKalınlığı;
            }

            float üst_orta = SolÜstNokta.X + (Görsel.ÇerçveKalınlığı / 2.0f);

            for (byte i = 21; i <= 28; i++)
            {
                Görsel.Yazıİçeriği = TümDişler.Contains(i) ? i.ToString() : "";
                Görsel.Çerçeve = new RectangleF(SolÜstNokta, KullanılabilirAlan_Diş_mm);
                Görsel.Çizdir(Grafik);

                SolÜstNokta.X += KullanılabilirAlan_Diş_mm.Width - Görsel.ÇerçveKalınlığı;
            }

            SolÜstNokta.X = 0;
            SolÜstNokta.Y += KullanılabilirAlan_Diş_mm.Height - Görsel.ÇerçveKalınlığı;
            float sol_orta = SolÜstNokta.Y;

            for (byte i = 48; i >= 41; i--)
            {
                Görsel.Yazıİçeriği = TümDişler.Contains(i) ? i.ToString() : "";
                Görsel.Çerçeve = new RectangleF(SolÜstNokta, KullanılabilirAlan_Diş_mm);
                Görsel.Çizdir(Grafik);

                SolÜstNokta.X += KullanılabilirAlan_Diş_mm.Width - Görsel.ÇerçveKalınlığı;
            }

            for (byte i = 31; i <= 38; i++)
            {
                Görsel.Yazıİçeriği = TümDişler.Contains(i) ? i.ToString() : "";
                Görsel.Çerçeve = new RectangleF(SolÜstNokta, KullanılabilirAlan_Diş_mm);
                Görsel.Çizdir(Grafik);

                SolÜstNokta.X += KullanılabilirAlan_Diş_mm.Width - Görsel.ÇerçveKalınlığı;
            }

            Görsel.Çizdir_Çizgi(Grafik, üst_orta, sol_orta);

            Görsel.Dispose();
            Grafik.Dispose();

            Resim.Save(HedefDosya, System.Drawing.Imaging.ImageFormat.Png);
            Resim.Dispose();
        }

        class Görsel_ : IDisposable
        {
            #region Değişkenler
            //Genel
            public RectangleF Çerçeve;
            public float Açı = 0, KarakterBüyüklüğü = 20.0f, ÇerçveKalınlığı = 1f;
            public Color Renk = Color.Black, Renk_ArkaPlan = Color.Transparent;

            //Genel Yazı
            public string Yazı_KarakterKümesi = "Calibri", Yazıİçeriği;
            public bool Yazı_Kalın = false;

            //İç Kullanım Yazı
            Font Yazı_KarakterKümesi_;
            SolidBrush Fırça_, Fırça_ArkaPlan_;
            StringFormat Yazı_Şekli_ = new StringFormat
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Center
            };
            #endregion

            public void Çizdir(Graphics Grafik)
            {
                if (YakınlaşmaOranı != 1) Grafik.ScaleTransform(YakınlaşmaOranı, YakınlaşmaOranı);

                if (Açı != 0)
                {
                    float _x = Çerçeve.X + (Çerçeve.Width / 2), _y = Çerçeve.Y + (Çerçeve.Height / 2);
                    Grafik.TranslateTransform(_x, _y); // döndürme noktası
                    Grafik.RotateTransform(Açı);
                    Grafik.TranslateTransform(-_x, -_y);
                }

                string Çözümlenmiş_İçerik = Yazıİçeriği;

                if (Yazı_KarakterKümesi_ == null)
                {
                    Fırça_ = new SolidBrush(Renk);
                    Fırça_ArkaPlan_ = new SolidBrush(Renk_ArkaPlan);

                    float KaBü_ = KarakterBüyüklüğü;
                    if (KaBü_ == 0)
                    {
                        if (string.IsNullOrEmpty(Çözümlenmiş_İçerik)) Çözümlenmiş_İçerik = " ";

                        SizeF Ölçü = new SizeF();
                        int KarakterSayısı = Çözümlenmiş_İçerik.Length, YazdırılanAdet = KarakterSayısı;

                        while (Ölçü.Width < Çerçeve.Width && Ölçü.Height < Çerçeve.Height && KarakterSayısı == YazdırılanAdet)
                        {
                            KaBü_ += 5;
                            Yazı_KarakterKümesi_ = new Font(Yazı_KarakterKümesi, KaBü_, Yazı_Kalın ? FontStyle.Bold : FontStyle.Regular, GraphicsUnit.Millimeter);
                            Ölçü = Grafik.MeasureString(Çözümlenmiş_İçerik, Yazı_KarakterKümesi_, Çerçeve.Size, Yazı_Şekli_, out YazdırılanAdet, out _);
                            Yazı_KarakterKümesi_.Dispose();
                        }

                        while (Ölçü.Width >= Çerçeve.Width || Ölçü.Height >= Çerçeve.Height || KarakterSayısı != YazdırılanAdet)
                        {
                            KaBü_ -= 0.1f;
                            Yazı_KarakterKümesi_ = new Font(Yazı_KarakterKümesi, KaBü_, Yazı_Kalın ? FontStyle.Bold : FontStyle.Regular, GraphicsUnit.Millimeter);
                            Ölçü = Grafik.MeasureString(Çözümlenmiş_İçerik, Yazı_KarakterKümesi_, Çerçeve.Size, Yazı_Şekli_, out YazdırılanAdet, out _);
                            Yazı_KarakterKümesi_.Dispose();
                        }
                    }

                    Yazı_KarakterKümesi_ = new Font(Yazı_KarakterKümesi, KaBü_, Yazı_Kalın ? FontStyle.Bold : FontStyle.Regular, GraphicsUnit.Millimeter);
                    if (Yazı_KarakterKümesi_.Name != Yazı_KarakterKümesi_.OriginalFontName) throw new System.Exception("Kayıtlı olan (" + Yazı_KarakterKümesi + ") mevcut karakter kümeleri arasında bulunmadığından açılamadı");
                }

                if (Renk_ArkaPlan != Color.Transparent) Grafik.FillRectangle(Fırça_ArkaPlan_, Çerçeve);

                Grafik.DrawString(Çözümlenmiş_İçerik, Yazı_KarakterKümesi_, Fırça_, Çerçeve, Yazı_Şekli_);

                //if (Çerçeve.Width > 0)
                //{
                //    Pen p = new Pen(Renk, ÇerçveKalınlığı);
                //    float yarı_kalınlık = ÇerçveKalınlığı / 2.0f;

                //    Grafik.DrawRectangle(p, Çerçeve.X + yarı_kalınlık, Çerçeve.Y + yarı_kalınlık, Çerçeve.Width - ÇerçveKalınlığı, Çerçeve.Height - ÇerçveKalınlığı);

                //    p.Dispose();
                //}

                if (Açı != 0)
                {
                    Grafik.RotateTransform(-Açı);
                }

                Grafik.ResetTransform();
            }
            public void Çizdir_Çizgi(Graphics Grafik, float üst_orta, float Sol_orta)
            {
                if (YakınlaşmaOranı != 1) Grafik.ScaleTransform(YakınlaşmaOranı, YakınlaşmaOranı);

                if (Açı != 0)
                {
                    float _x = Çerçeve.X + (Çerçeve.Width / 2), _y = Çerçeve.Y + (Çerçeve.Height / 2);
                    Grafik.TranslateTransform(_x, _y); // döndürme noktası
                    Grafik.RotateTransform(Açı);
                    Grafik.TranslateTransform(-_x, -_y);
                }

                Pen p = new Pen(Renk, ÇerçveKalınlığı * 4.0f);
                Grafik.DrawLine(p, üst_orta, 0, üst_orta, Sol_orta * 2);
                Grafik.DrawLine(p, 0, Sol_orta, üst_orta * 2, Sol_orta);
                p.Dispose();

                if (Açı != 0)
                {
                    Grafik.RotateTransform(-Açı);
                }

                Grafik.ResetTransform();
            }
            public void YenidenHesaplat()
            {
                Yazı_KarakterKümesi_?.Dispose(); Yazı_KarakterKümesi_ = null;
                Fırça_?.Dispose(); Fırça_ = null;
                Fırça_ArkaPlan_?.Dispose(); Fırça_ArkaPlan_ = null;
                Yazı_Şekli_?.Dispose(); Yazı_Şekli_ = null;
            }

            #region Idisposable
            private bool disposedValue;
            protected virtual void Dispose(bool disposing)
            {
                if (!disposedValue)
                {
                    if (disposing)
                    {
                        // TODO: dispose managed state (managed objects)

                        YenidenHesaplat();
                    }

                    // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                    // TODO: set large fields to null
                    disposedValue = true;
                }
            }

            // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
            ~Görsel_()
            {
                // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
                Dispose(disposing: false);
            }

            public void Dispose()
            {
                // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
                Dispose(disposing: true);
                GC.SuppressFinalize(this);
            }
            #endregion
        }
    }
}
