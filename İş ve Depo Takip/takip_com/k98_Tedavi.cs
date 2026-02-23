using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Serialization;

namespace İş_ve_Depo_Takip.takip_com.UçNokta
{
    public class Tedaviler_Dosyalama_
    {
        [JsonInclude, JsonPropertyName("T")]
        public Dictionary<string, Tedavi_> Tedaviler; //SeriNo , Detaylar

        [JsonInclude, JsonPropertyName("Ot")]
        public Tedaviler_ÖdemeTalebi_ ÖdemeTalebi;

        [JsonInclude, JsonPropertyName("Od")]
        public Tedaviler_Ödeme_ Ödeme;

        //////////////////////////////////////////////////////

        // <summary>
        // /KlinikGolgesi/Tbl/ A
        //KlinikGolgesi/Tbl/ B veya C /DosyaAdı
        // </summary>
        public static (Tedaviler_Dosyalama_ Tedaviler_Dosyalama, string DosyaEki_İndirmeBağlantısı) Tablo_Oku(string Sayfa)
        {
            Haberleşme_ H = new Haberleşme_();

            HttpResponseMessage cevap = H.İstekYap_Detaylı("GET", Ortak.Sayfa_Tedavi + Sayfa, null, 200);

            string yazı_olarak = cevap.Content.ReadAsStringAsync().Result;
            Tedaviler_Dosyalama_ çıktı = (Tedaviler_Dosyalama_)Json.Yazıdan_Nesneye(yazı_olarak, typeof(Tedaviler_Dosyalama_));
            if (çıktı == null) throw new Exception("istek içeriği nesne olarak alınamadı");

            if (!cevap.Headers.TryGetValues("DoEk_Eb", out IEnumerable<string> DosyaEki_İndirmeBağlantısı)) DosyaEki_İndirmeBağlantısı = null;

            return (çıktı, DosyaEki_İndirmeBağlantısı?.ToArray()[0]);
        }

        // <summary>
        // /KlinikGolgesi/Tbl/ A                     -> SeriNo
        //KlinikGolgesi/Tbl/ B veya C /DosyaAdı    -> SeriNo
        // </summary>
        public static Tedavi_ Tablo_Oku(string Sayfa, string Seri_No)
        {
            (Tedaviler_Dosyalama_ Tablo, _) = Tablo_Oku(Sayfa);

            if (Tablo.Tedaviler == null ||
                !Tablo.Tedaviler.ContainsKey(Seri_No)) throw new Exception();

            return Tablo.Tedaviler[Seri_No];
        }
    }

    public class Tedaviler_ÖdemeTalebi_
    {
        [JsonInclude, JsonPropertyName("Tar_ot")]
        public DateTime ÖdemeTalebiTarihi;

        [JsonInclude, JsonPropertyName("Tt")]
        public decimal TedavilerToplam;
        [JsonInclude, JsonPropertyName("K")]
        public float? KDV_Yüzde;
        [JsonInclude, JsonPropertyName("I")]
        public float? İskonto_Yüzde; // AltToplamaUygulananİskonto_Yüzde

        //[JsonInclude, JsonPropertyName("i_a")]
        //public string? İlaveÖdeme_Açıklama;
        //[JsonInclude, JsonPropertyName("i_m")]
        //public decimal? İlaveÖdeme_Miktar;
    }

    public class Tedaviler_Ödeme_
    {
        //[JsonInclude, JsonPropertyName("Tar_o")]
        //public DateTime ÖdemeTarihi;
        [JsonInclude, JsonPropertyName("N")]
        public string Notlar;

        //[JsonInclude, JsonPropertyName("moo")]
        //public decimal MevcutÖnÖdeme;
        [JsonInclude, JsonPropertyName("ao")]
        public decimal AlınanÖdeme;
    }

    public enum Tedavi_Durumu_ { Bilinmiyor, DevamEdiyor, TeslimEdildi, ÖdemeTalepEdildi, Ödendi, Silindi }

    public class Tedavi_
    {
        [JsonInclude, JsonPropertyName("H")]
        public string Hasta;
        [JsonInclude, JsonPropertyName("I")]
        public float? İskonto_Yüzde;
        [JsonInclude, JsonPropertyName("N")]
        public string Notlar;
        [JsonInclude, JsonPropertyName("Tt")]
        public DateTime? TeslimEdilmeTarihi; //varsa teslim edildi
        [JsonInclude, JsonPropertyName("Is")]
        public Dictionary<string, İş_> İşler; //İş Kabul Tarihi, Detaylar
        [JsonInclude, JsonPropertyName("De")]
        public Dictionary<string /*Kısayolu*/, DosyaEki_> DosyaEkleri; // Dsy\\De\\Fir\\<Firma>\\Kl\\<Golge Adı>\\<SeriNo>\\<Kısayolu (soyadsız)>
    }

    public class İş_
    {
        [JsonInclude, JsonPropertyName("It")]
        public string işTürü;
        [JsonInclude, JsonPropertyName("Ict")]
        public DateTime? İşÇıkışTarihi;
        [JsonInclude, JsonPropertyName("Uc")]
        public decimal? Ücret;
        [JsonInclude, JsonPropertyName("Uc_e")]
        public decimal? Ücret_ElleGirilen;
        [JsonInclude, JsonPropertyName("AvK")]
        public byte[] AdetVeKonum;
        //null, bos dizi    -> 1 adet
        //1                 -> 1 adet
        //1, 15             -> 1 adet
        //3                 -> 3 adet
        //3, 15,25,35       -> 3 adet
    }

    public class DosyaEki_
    {
        [JsonInclude, JsonPropertyName("E")]
        public DateTime EklenmeTarihi;
        [JsonInclude, JsonPropertyName("T")]
        public string _UygunOlanTip_Veya_DosyaAdı;

        public enum Tipi_
        {
            Resim,
            Video,
            Ses,
            Diger //3d dosyalar
        };

        public void Yeni(string Tipi, string DosyaAdı)
        {
            Tipi = Tipi.Trim().ToLower();

            if (Icerik_Tipi(Tipi) == Tipi_.Diger)
            {
                DosyaAdı = System.Text.RegularExpressions.Regex.Replace(DosyaAdı.Trim(), @"[^a-zA-Z0-9.\-_]", "]");
                if (DosyaAdı.Length > 55) DosyaAdı = DosyaAdı.Substring(DosyaAdı.Length - 55);

                _UygunOlanTip_Veya_DosyaAdı = DosyaAdı;
            }
            else
            {
                _UygunOlanTip_Veya_DosyaAdı = Tipi;
            }

            if (string.IsNullOrWhiteSpace(_UygunOlanTip_Veya_DosyaAdı)) _UygunOlanTip_Veya_DosyaAdı = "]";

            EklenmeTarihi = DateTime.UtcNow;
        }

        public static Tipi_ Icerik_Tipi(string UygunOlanTip_Veya_DosyaAdı)
        {
            if (UygunOlanTip_Veya_DosyaAdı.Contains("image/"))
            {
                return Tipi_.Resim;
            }
            else if (UygunOlanTip_Veya_DosyaAdı.Contains("video/"))
            {
                return Tipi_.Video;
            }
            else if (UygunOlanTip_Veya_DosyaAdı.Contains("audio/"))
            {
                return Tipi_.Ses;
            }
            else
            {
                return Tipi_.Diger;
            }
        }
        public static string Icerik_Tipi_Http(string UygunOlanTip_Veya_DosyaAdı)
        {
            if (Icerik_Tipi(UygunOlanTip_Veya_DosyaAdı) == Tipi_.Diger) return "application/octet-stream";
            else return UygunOlanTip_Veya_DosyaAdı;
        }
    }
}
