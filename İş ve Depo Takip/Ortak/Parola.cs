using ArgeMup.HazirKod;
using ArgeMup.HazirKod.Dönüştürme;

namespace İş_ve_Depo_Takip
{
    public static class Parola
    {
        static string Yazı = "ArGeMuP İş Ve Depo Takip Uygulaması";
        public static byte[] Dizi = D_Yazı.BaytDizisine(Yazı);
        public static void Kaydet(string KullanıcıParalosu)
        {
            Banka.Tablo_Dal(null, Banka.TabloTürü.Ayarlar, "Kullanıcı Şifresi", true)[0] = DoğrulamaKodu.Üret.Yazıdan(KullanıcıParalosu + "ArGeMuP");
        }
        public static bool KontrolEt(string KullanıcıParalosu)
        {
            return Banka.Tablo_Dal(null, Banka.TabloTürü.Ayarlar, "Kullanıcı Şifresi")[0] == DoğrulamaKodu.Üret.Yazıdan(KullanıcıParalosu + "ArGeMuP");
        }
    }
}
