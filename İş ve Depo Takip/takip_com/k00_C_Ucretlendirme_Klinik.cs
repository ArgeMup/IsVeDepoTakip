using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace İş_ve_Depo_Takip.takip_com.UçNokta
{
    // Tek klinik icin özel
    // Banka\\Fir\\<Golge Adı>\\Kl\\<Golge Adı>\\Ucr.mup 

    public class Ucretlendirme_Klinik_Dosyalama_
    {
        [JsonInclude, JsonPropertyName("O")]
        public Dictionary<string, string> ÖzelİşTürüÜcretleri; //iş türü adı, özel ücret

        [JsonInclude, JsonPropertyName("B")]
        public string ÜcretBoşİseYapılacakHesaplama;

        [JsonInclude, JsonPropertyName("I")]
        public float AltToplamaUygulananİskonto_Yüzde;

        [JsonInclude, JsonPropertyName("K")]
        public bool KDV;

        //////////////////////////////////////////////////////////////////////////

        public void Ücretlendir(string Gölgesi_Klinik)
        {
            Haberleşme_ H = new Haberleşme_();
            
            H.İstekYap("PUT", Ortak.Sayfa_Ücretlendirme + "/Kl/" + Gölgesi_Klinik, this);
        }
    }
}
