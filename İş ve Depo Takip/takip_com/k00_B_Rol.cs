using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace İş_ve_Depo_Takip.takip_com.UçNokta
{
    public class Rol_Dosyalama_
    {
        [JsonInclude]
        public Dictionary<string, Rol_> Hepsi = new Dictionary<string, Rol_>(); //Gölge, Detaylar
    }

    public class Rol_
    {
        //Ortak
        public string Ad { get; set; }
        public bool Gizli { get; set; }
        public bool Sabit { get; set; }

        //Site sahibi genelinde
        [JsonPropertyName("f_d")] public bool Firmaların_ayarlarını_değiştirebilir { get; set; }
        [JsonPropertyName("f_g")] public bool Firmaları_ve_ayarlarını_görebilir { get; set; }

        //Site müşterisi firma (lab) sahipleri genelinde
        [JsonPropertyName("m_g")] public bool Mali_durumu_görebilir { get; set; }
        [JsonPropertyName("k_s")] public bool Kullanıcıları_silebilir { get; set; }
        [JsonPropertyName("k_ed")] public bool Kullanıcı_ekleyebilir_düzenleyebilir { get; set; }
        [JsonPropertyName("kl_s")] public bool Klinikleri_silebilir { get; set; }
        [JsonPropertyName("kl_ed")] public bool Klinik_ekleyebilir_düzenleyebilir { get; set; }
        [JsonPropertyName("it_s")] public bool İş_türlerini_silebilir { get; set; }
        [JsonPropertyName("it_u_d")] public bool İş_türlerinin_ücretlerini_değiştirebilir { get; set; }
        [JsonPropertyName("it_ed")] public bool İş_türü_ekleyebilir_düzenleyebilir { get; set; }
        [JsonPropertyName("it_u_ug")] public bool İş_türlerine_belirlenenin_dışında_ücret_girebilir { get; set; }
        [JsonPropertyName("it_u_g")] public bool iş_türlerinin_ücretlerini_görebilir { get; set; }
        [JsonPropertyName("m_koa")] public bool Klinikten_ödeme_alabilir { get; set; }
        [JsonPropertyName("m_kog")] public bool Kliniklerin_geçmiş_ödemelerini_görebilir { get; set; }
        [JsonPropertyName("ot_s")] public bool Ödeme_taleplerini_iptal_edebilir { get; set; }
        [JsonPropertyName("ot_g")] public bool Ödeme_taleplerini_görebilir { get; set; }
        [JsonPropertyName("ot_o")] public bool Ödeme_talebi_oluşturabilir { get; set; }
        [JsonPropertyName("de_s")] public bool İşleri_silebilir { get; set; }
        [JsonPropertyName("de_d")] public bool İşleri_düzenleyebilir { get; set; }
        [JsonPropertyName("de_e")] public bool Yeni_iş_oluşturabilir { get; set; }

        public void TümüneUygula(bool Site, bool Firma)
        {
            Gizli = false;
            Sabit = false;

            Firmaların_ayarlarını_değiştirebilir = Site;
            Firmaları_ve_ayarlarını_görebilir = Site;

            Mali_durumu_görebilir = Firma;
            Kullanıcıları_silebilir = Firma;
            Kullanıcı_ekleyebilir_düzenleyebilir = Firma;
            Klinikleri_silebilir = Firma;
            Klinik_ekleyebilir_düzenleyebilir = Firma;
            İş_türlerini_silebilir = Firma;
            İş_türlerinin_ücretlerini_değiştirebilir = Firma;
            İş_türü_ekleyebilir_düzenleyebilir = Firma;
            İş_türlerine_belirlenenin_dışında_ücret_girebilir = Firma;
            iş_türlerinin_ücretlerini_görebilir = Firma;
            Klinikten_ödeme_alabilir = Firma;
            Kliniklerin_geçmiş_ödemelerini_görebilir = Firma;
            Ödeme_taleplerini_iptal_edebilir = Firma;
            Ödeme_taleplerini_görebilir = Firma;
            Ödeme_talebi_oluşturabilir = Firma;
            İşleri_silebilir = Firma;
            İşleri_düzenleyebilir = Firma;
            Yeni_iş_oluşturabilir = Firma;
        }
    }
}