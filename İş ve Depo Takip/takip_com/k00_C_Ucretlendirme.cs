using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace İş_ve_Depo_Takip.takip_com.UçNokta
{
    // Tüm klinikler icin ortak
    // Banka\\Fir\\<Golge Adı>\\Ucr.mup

    public class Ucretlendirme_Dosyalama_
    {
        [JsonInclude, JsonPropertyName("it")]
        public Dictionary<string, Ucretlendirme_İşTürü_> İşTürü_Detaylar; //İş türü adı, Detaylar

        [JsonInclude, JsonPropertyName("De")]
        public Dictionary<string, string> Değişkenler; //adı, formül
        //Simdilik sadece Dolar ve Avro

        [JsonInclude]
        public float? KDV;

        public void Ücretlendir()
        {
            Haberleşme_ H = new Haberleşme_();

            H.İstekYap("PUT", Ortak.Sayfa_Ücretlendirme + "/De_It", this);
        }
    }

    public class Ucretlendirme_İşTürü_
    {
        [JsonInclude, JsonPropertyName("U")]
        public string Ücret;
        [JsonInclude, JsonPropertyName("M")]
        public string Maliyet;
    }
}
