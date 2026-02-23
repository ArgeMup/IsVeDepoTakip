using System.Text.Json.Serialization;

namespace İş_ve_Depo_Takip.takip_com.UçNokta
{
    public static class Kullanıcı
    {
        private static Kullanıcı_Ayarlar_ _Ayarlar = null;

        public static Kullanıcı_Ayarlar_ Ayarlar
        {
            get
            {
                if (_Ayarlar == null)
                {
                    Haberleşme_ H = new Haberleşme_();
                    _Ayarlar = H.İstekYap<Kullanıcı_Ayarlar_>("GET", takip_com.Ortak.Sayfa_KullanıcıAyarları);
                }

                return _Ayarlar;
            }
            set
            {
                _Ayarlar = null;
            }
        }
    }

    public class Kullanıcı_Ayarlar_
    {
        [JsonInclude, JsonPropertyName("r")]
        public Rol_ Rol;

        [JsonInclude, JsonPropertyName("k")]
        public Klinik_Dosyalama_ Klinikler;

        [JsonInclude, JsonPropertyName("i")]
        public İşTürü_Dosyalama_ İşTürleri;

        [JsonInclude, JsonPropertyName("t")]
        public string Tarayıcı; //Kullanıcının tarayıcısındaki
    }
}
