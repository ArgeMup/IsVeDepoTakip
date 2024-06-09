using ArgeMup.HazirKod;

namespace İş_ve_Depo_Takip
{
    public static class ArkaPlamUygulamaları
    {
        static IDepo_Eleman Ayarlar_Gizlilik_ArkaPlanUygulamaları;
        
        public static bool EtkinMi
        {
            get 
            {
                Ayarlar_Gizlilik_ArkaPlanUygulamaları = Banka.Ayarlar_Genel("Gizlilik/Arka Plan Uygulamaları");

                return 
                    Banka.K_lar.ParolaKontrolüGerekiyorMu &&
                    Ayarlar_Gizlilik_ArkaPlanUygulamaları != null &&
                    ArgeMup.HazirKod.Ekranlar.Kullanıcılar.Parola_Kontrol(Ayarlar_Gizlilik_ArkaPlanUygulamaları[0], Ayarlar_Gizlilik_ArkaPlanUygulamaları[1]);
            }
        }

        public static bool ÇalışabilirMi
        {
            get
            {
                if (Banka.K_lar.GeçerliKullanıcı != null) return true;
                if (!EtkinMi) return false;

                ArgeMup.HazirKod.Ekranlar.Kullanıcılar.Parola_Kontrol(Ayarlar_Gizlilik_ArkaPlanUygulamaları[0], Ayarlar_Gizlilik_ArkaPlanUygulamaları[1], false);
                return Banka.K_lar.GeçerliKullanıcı != null;
            }
        }
    }
}
