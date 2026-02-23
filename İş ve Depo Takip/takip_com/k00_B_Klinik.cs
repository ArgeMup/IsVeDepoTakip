using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace İş_ve_Depo_Takip.takip_com.UçNokta
{
    public class Klinik_Dosyalama_
    {
        [JsonInclude]
        public Dictionary<string, Klinik_> Hepsi = new Dictionary<string, Klinik_>(); //Gölge, Detaylar

        public static KeyValuePair<string, Klinik_> KontrolEt(string klinik_EkranAdı)
        {
            klinik_EkranAdı = klinik_EkranAdı.Trim();

            var a = Bul_YoksaEkle(klinik_EkranAdı);
            return a;
        }
        static KeyValuePair<string, Klinik_> Bul_YoksaEkle(string klinik_EkranAdı)
        {
            klinik_EkranAdı = klinik_EkranAdı.Trim();

            var a = Kullanıcı.Ayarlar;
            var b = a.Klinikler.Hepsi.FirstOrDefault(x => x.Value.Ad == klinik_EkranAdı);
            if (!string.IsNullOrWhiteSpace(b.Key)) return b;

            //üret
            Kullanıcı.Ayarlar = null;
            Haberleşme_ H = new Haberleşme_();
            H.İstekYap("POST", Ortak.Sayfa_Klinik + "/" + D_HexYazı.NormalYazıdan(klinik_EkranAdı), null);
            
            a = Kullanıcı.Ayarlar;
            b = a.Klinikler.Hepsi.FirstOrDefault(x => x.Value.Ad == klinik_EkranAdı);
            if (string.IsNullOrWhiteSpace(b.Key)) throw new System.Exception("üretilemedi");
            return b;
        }
        static void Düzenle(string Gölgesi_Klinik, bool Gizli, string Notlar)
        {
            Haberleşme_ H = new Haberleşme_();

            Klinik_ Klinik = new Klinik_();
            Klinik.Gizli = Gizli;
            Klinik.Notlar = Notlar;

            H.İstekYap("PUT", Ortak.Sayfa_Klinik + "/" + Gölgesi_Klinik, Klinik);
        }
    }

    public class Klinik_
    {
        [JsonInclude]
        public bool Gizli;
        [JsonInclude]
        public string Ad;

        [JsonInclude, JsonPropertyName("N")]
        public string Notlar;
    }
}
