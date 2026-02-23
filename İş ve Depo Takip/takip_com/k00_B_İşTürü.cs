using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace İş_ve_Depo_Takip.takip_com.UçNokta
{
    public class İşTürü_Dosyalama_
    {
        [JsonInclude]
        public Dictionary<string, İşTürü_> Hepsi = new Dictionary<string, İşTürü_>(); //Ad, Detaylar

        [JsonInclude, JsonPropertyName("t")]
        public bool TamamlayıcıİşVarMı; //Gizli olmayan

        public static void KontrolEt(string İştürü_Adı)
        {
            İştürü_Adı = İştürü_Adı.Trim();

            var a = Bul_YoksaEkle(İştürü_Adı);

            if (a.Tamamlayıcı == Banka.İştürü_Tamamlayıcıİş_Mi(İştürü_Adı) &&
                a.Gizli == false &&
                a.KliniğeGösterilenAdı == null) return;

            Düzenle(İştürü_Adı, false, null, Banka.İştürü_Tamamlayıcıİş_Mi(İştürü_Adı));
        }
        static İşTürü_ Bul_YoksaEkle(string İştürü_Adı)
        {
            var a = Kullanıcı.Ayarlar;
            if (a.İşTürleri.Hepsi.TryGetValue(İştürü_Adı, out İşTürü_ İşTürü)) return İşTürü;

            //üret
            Kullanıcı.Ayarlar = null;
            Haberleşme_ H = new Haberleşme_();
            H.İstekYap("POST", Ortak.Sayfa_İşTürü + "/" + D_HexYazı.NormalYazıdan(İştürü_Adı), null);

            a = Kullanıcı.Ayarlar;
            if (a.İşTürleri.Hepsi.TryGetValue(İştürü_Adı, out İşTürü)) return İşTürü;
            else throw new System.Exception("üretilemedi");
        }
        static void Düzenle(string İştürü_Adı, bool Gizli, string KliniğeGösterilenAdı, bool Tamamlayıcı)
        {
            Haberleşme_ H = new Haberleşme_();

            İşTürü_ İşTürü = new İşTürü_();
            İşTürü.Gizli = Gizli;
            İşTürü.KliniğeGösterilenAdı = KliniğeGösterilenAdı;
            İşTürü.Tamamlayıcı = Tamamlayıcı;

            H.İstekYap("PUT", Ortak.Sayfa_İşTürü + "/" + D_HexYazı.NormalYazıdan(İştürü_Adı), İşTürü);
        }
    }

    public class İşTürü_
    {
        [JsonInclude]
        public bool Gizli;

        [JsonInclude, JsonPropertyName("Ka")]
        public string KliniğeGösterilenAdı;
        [JsonInclude, JsonPropertyName("T")]
        public bool Tamamlayıcı;
    }

    public class İşTürü_Degisiklik_
    {
        [JsonInclude]
        public bool Gizli;

        [JsonInclude, JsonPropertyName("Ka")]
        public string KliniğeGösterilenAdı;
        [JsonInclude, JsonPropertyName("T")]
        public bool Tamamlayıcı;

        [JsonInclude]
        public string Ad; //Yeni adı
    }
}