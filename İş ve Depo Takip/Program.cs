using ArgeMup.HazirKod;
using ArgeMup.HazirKod.Ekİşlemler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip
{
    internal static class Program
    {
        static UygulamaOncedenCalistirildiMi_ UyÖnÇa;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Günlük.Başlat(Kendi.Klasörü + "\\Günlük");

            try
            {
                UyÖnÇa = new UygulamaOncedenCalistirildiMi_();
                if (UyÖnÇa.KontrolEt()) UyÖnÇa.DiğerUygulamayıÖneGetir();
                else
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Açılış_Ekranı());
                }
            }
            catch (Exception ex) { ex.Günlük(); }
        }
    }
}
