using ArgeMup.HazirKod.Dönüştürme;

namespace İş_ve_Depo_Takip
{
    public static class Parola
    {
        public static string Yazı = "ArGeMuP İş Ve Depo Takip Uygulaması";
        public static byte[] Dizi = D_Yazı.BaytDizisine(Yazı);

        public static string Yazı_Eposta = "ArGeMuP Eposta Uygulaması";
        public static string Yazı_GelirGiderTakip = "ArGeMuP Gelir Gider Takip Uygulaması";
    }
}
