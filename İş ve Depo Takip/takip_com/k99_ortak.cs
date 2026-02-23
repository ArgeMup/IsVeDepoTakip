using ArgeMup.HazirKod;
using ArgeMup.HazirKod.Ekİşlemler;
using System;
using System.Collections.Generic;
using System.IO;

namespace İş_ve_Depo_Takip.takip_com
{
    public static class BuSite
    {
        public static readonly string Sürüm = "s0.2.10";

#if DEBUG
        public static readonly string Adres_Ham = "localhost";
        public static readonly string Adres_Tam = "http://localhost:5158";
#else
        public static readonly string Adres_Ham = "lab.tedavitakip.com";
        public static readonly string Adres_Tam = "https://lab.tedavitakip.com";
#endif
    }

    public static class Ortak
    {
        public static readonly string
           Sayfa_Firma = "/Kmt/Firma",
           Sayfa_Kullanıcı = "/Kmt/Kullanici",
           Sayfa_Kullanıcı_Girişi = "/Kmt/KlcGrs",
           Sayfa_Rol = "/Kmt/Rol",
           Sayfa_Klinik = "/Kmt/Klinik",
           Sayfa_İşTürü = "/Kmt/IsTuru",
           Sayfa_Tedavi = "/Kmt/Tedavi",
           Sayfa_Ücretlendirme = "/Kmt/Ucr",
           Sayfa_KullanıcıAyarları = "/Kmt/KlncAyrlr",
           Sayfa_Aktarma = "/Aktr";

        public static string GeçerliKullanıcı_EkranAdı
        {
            get
            {
                return Banka.K_lar.KullancıAdı;
            }
        }

        public static void Günlük(string Mesaj)
        {
            try
            {
                Mesaj = DateTime.Now.Yazıya() + " " + Mesaj; 
                Console.WriteLine(Mesaj);
                File.AppendAllText(Kendi.Klasörü + "\\takip_gunluk.txt", Mesaj + "\n");
            }
            catch (Exception) { }
        }
    
        public static void Başlat()
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                File.WriteAllBytes(Kendi.Klasörü + "\\Microsoft.Bcl.AsyncInterfaces.dll", Properties.Resources.Microsoft_Bcl_AsyncInterfaces);
                File.WriteAllBytes(Kendi.Klasörü + "\\System.Buffers.dll", Properties.Resources.System_Buffers);
                File.WriteAllBytes(Kendi.Klasörü + "\\System.IO.Pipelines.dll", Properties.Resources.System_IO_Pipelines);
                File.WriteAllBytes(Kendi.Klasörü + "\\System.Memory.dll", Properties.Resources.System_Memory);
                File.WriteAllBytes(Kendi.Klasörü + "\\System.Numerics.Vectors.dll", Properties.Resources.System_Numerics_Vectors);
                File.WriteAllBytes(Kendi.Klasörü + "\\System.Runtime.CompilerServices.Unsafe.dll", Properties.Resources.System_Runtime_CompilerServices_Unsafe);
                File.WriteAllBytes(Kendi.Klasörü + "\\System.Text.Encodings.Web.dll", Properties.Resources.System_Text_Encodings_Web);
                File.WriteAllBytes(Kendi.Klasörü + "\\System.Text.Json.dll", Properties.Resources.System_Text_Json);
                File.WriteAllBytes(Kendi.Klasörü + "\\System.Threading.Tasks.Extensions.dll", Properties.Resources.System_Threading_Tasks_Extensions);
            });
        }
    }

    public static class TanımDönüştürücü
    {
        static string dsy = Kendi.Klasörü + "\\TanimDonusum.txt";
        static Dictionary<string, string> sözlük = null;

        static void oku()
        {
            if (sözlük == null)
            {
                if (File.Exists(dsy))
                {
                    sözlük = Json.Yazıdan_Nesneye(File.ReadAllText(dsy), typeof(Dictionary<string, string>)) as Dictionary<string, string>;
                }
                else sözlük = new Dictionary<string, string>();
            }
        }
        static void yaz()
        {
            var a = Json.Nesneden_Yazıya(sözlük, sözlük.GetType());
            File.WriteAllText(dsy, a);
        }

        public static void Ekle(string Yerel, string Uzak)
        {
            oku();
            sözlük [Yerel] = Uzak;
            yaz();
        }
        public static void Sil(string Yerel)
        {
            oku();
            sözlük.Remove(Yerel);
            yaz();
        }

        public static bool Bul(string Yerel, out string Uzak)
        {
            Uzak = null;

            oku();

            return sözlük.TryGetValue(Yerel, out Uzak);
        }
    }
}
