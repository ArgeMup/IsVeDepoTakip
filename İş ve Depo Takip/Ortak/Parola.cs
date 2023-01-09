using ArgeMup.HazirKod;
using ArgeMup.HazirKod.Dönüştürme;

namespace İş_ve_Depo_Takip
{
    public static class Parola
    {
        public static string Yazı = "ArGeMuP İş Ve Depo Takip Uygulaması";
        public static byte[] Dizi = D_Yazı.BaytDizisine(Yazı);
        public static void Kaydet(string KullanıcıParalosu)
        {
            Banka.Ayarlar_Genel("Kullanıcı Şifresi", true)[0] = DoğrulamaKodu.Üret.Yazıdan(KullanıcıParalosu + "ArGeMuP");
        }
        public static bool KontrolEt(string KullanıcıParalosu)
        {
            return Banka.Ayarlar_Genel("Kullanıcı Şifresi")[0] == DoğrulamaKodu.Üret.Yazıdan(KullanıcıParalosu + "ArGeMuP");
        }
    }
}
