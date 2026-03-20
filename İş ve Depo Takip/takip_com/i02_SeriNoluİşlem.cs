using ArgeMup.HazirKod.Ekİşlemler;
using İş_ve_Depo_Takip.takip_com.UçNokta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace İş_ve_Depo_Takip.takip_com
{
    public class İşlem_SeriNolu_
    {
        public void KliniğeGönder(string Klinik_EkranAdı, List<string> SeriNolar)
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                Haberleşme_ H = new takip_com.Haberleşme_();

                try
                {
                    H.Başlat(Ortak.GeçerliKullanıcı_EkranAdı);

                    var Klinik = Klinik_Dosyalama_.KontrolEt(Klinik_EkranAdı);

                    string smd = D_TarihSaat_UTC.Yazıya(DateTime.UtcNow);
                    Tedaviler_SeriNoluİşlem_ sn_ler = new Tedaviler_SeriNoluİşlem_();
                    sn_ler.Hepsi = new Dictionary<string, string>();
                    for (int i = 0; i < SeriNolar.Count; i++)
                    {
                        if (!TanımDönüştürücü.Bul(SeriNolar[i], out string Uzak)) continue;

                        sn_ler.Hepsi.Add(Uzak, smd);
                    }
                    if (sn_ler.Hepsi.Count == 0) return;

                    H.İstekYap("PUT", Ortak.Sayfa_Tedavi + "/" + Klinik.Key + "/Sni/Kg", sn_ler);

#if DEBUG
                    Console.WriteLine("bitti kg");
#endif
                }
                catch (System.Exception ex) { Ortak.Günlük(ex.ToString()); }
                finally { H.Başlat(null); }
            }); 
        }

        public void TeslimEdildiOlarakİşaretle(string Klinik_EkranAdı, List<string> SeriNolar)
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                Haberleşme_ H = new takip_com.Haberleşme_();

                try
                {
                    H.Başlat(Ortak.GeçerliKullanıcı_EkranAdı);

                    var Klinik = Klinik_Dosyalama_.KontrolEt(Klinik_EkranAdı);

                    string smd = D_TarihSaat_UTC.Yazıya(DateTime.UtcNow);
                    Tedaviler_SeriNoluİşlem_ sn_ler = new Tedaviler_SeriNoluİşlem_();
                    sn_ler.Hepsi = new Dictionary<string, string>();
                    for (int i = 0; i < SeriNolar.Count; i++)
                    {
                        if (!TanımDönüştürücü.Bul(SeriNolar[i], out string Uzak)) continue;

                        sn_ler.Hepsi.Add(Uzak, smd);
                    }
                    if (sn_ler.Hepsi.Count == 0) return;

                    H.İstekYap("PUT", Ortak.Sayfa_Tedavi + "/" + Klinik.Key + "/Sni/Te", sn_ler);

#if DEBUG
                    Console.WriteLine("bitti te");
#endif
                }
                catch (System.Exception ex) { Ortak.Günlük(ex.ToString()); }
                finally { H.Başlat(null); }
            });
        }

        public void Sil(string Klinik_EkranAdı, List<string> SeriNolar)
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                Haberleşme_ H = new takip_com.Haberleşme_();

                try
                {
                    H.Başlat(Ortak.GeçerliKullanıcı_EkranAdı);

                    var Klinik = Klinik_Dosyalama_.KontrolEt(Klinik_EkranAdı);

                    string sn_ler = "";
                    for (int i = 0; i < SeriNolar.Count; i++)
                    {
                        if (!TanımDönüştürücü.Bul(SeriNolar[i], out string Uzak)) continue;

                        sn_ler += "/" + Uzak;
                    }
                    if (string.IsNullOrWhiteSpace(sn_ler)) return;

                    H.İstekYap("DELETE", Ortak.Sayfa_Tedavi + "/" + Klinik.Key + "/Sni/Sil" + sn_ler, null);

                    for (int i = 0; i < SeriNolar.Count; i++)
                    {
                        TanımDönüştürücü.Sil(SeriNolar[i]);
                    }

#if DEBUG
                    Console.WriteLine("bitti sil");
#endif
                }
                catch (System.Exception ex) { Ortak.Günlük(ex.ToString()); }
                finally { H.Başlat(null); }
            });
        }

        public void ÖdemeTalebiOluştur(string Klinik_EkranAdı, List<string> SeriNolar, string DosyaAdı)
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                Haberleşme_ H = new takip_com.Haberleşme_();

                try
                {
                    H.Başlat(Ortak.GeçerliKullanıcı_EkranAdı);

                    var Klinik = Klinik_Dosyalama_.KontrolEt(Klinik_EkranAdı);

                    #region ücretlendirme
                    İş_ve_Depo_Takip.Banka.Müşteri_KDV_İskonto(Klinik_EkranAdı, out bool KDV_Ekle, out double KDV_Yüzde, out bool İskonto_Yap, out double İskonto_Yüzde, out string BirimÜcretBoşİseYapılacakHesaplama);
                    Ucretlendirme_Klinik_Dosyalama_ kl = new Ucretlendirme_Klinik_Dosyalama_();
                    kl.ÜcretBoşİseYapılacakHesaplama = BirimÜcretBoşİseYapılacakHesaplama;
                    kl.AltToplamaUygulananİskonto_Yüzde = İskonto_Yap ? (float)İskonto_Yüzde : 0;
                    kl.KDV = KDV_Ekle;
                    kl.ÖzelİşTürüÜcretleri = new Dictionary<string, string>();
                    foreach (var it in İş_ve_Depo_Takip.Banka.İşTürü_Listele())
                    {
                        string snç = İş_ve_Depo_Takip.Banka.Ücretler_BirimÜcret(Klinik_EkranAdı, it, out double değeri);
                        if (snç == null)
                        {
                            kl.ÖzelİşTürüÜcretleri.Add(it.Trim(), değeri.Yazıya() + (değeri == 0 ? " %=0%" : null));
                            İş_ve_Depo_Takip.takip_com.UçNokta.İşTürü_Dosyalama_.KontrolEt(it.Trim());
                        }
                    }
                    kl.Ücretlendir(Klinik.Key);

                    Ucretlendirme_Dosyalama_ ü = new Ucretlendirme_Dosyalama_();
                    if (KDV_Ekle) ü.KDV = (float)KDV_Yüzde;
                    ü.İşTürü_Detaylar = new Dictionary<string, Ucretlendirme_İşTürü_>();
                    foreach (var it in İş_ve_Depo_Takip.Banka.İşTürü_Listele())
                    {
                        string snç = İş_ve_Depo_Takip.Banka.Ücretler_BirimÜcret(null, it, out double değeri);
                        if (snç == null)
                        {
                            ü.İşTürü_Detaylar.Add(it.Trim(), new Ucretlendirme_İşTürü_() { Ücret = değeri.Yazıya() + (değeri == 0 ? " %=0%" : null) });
                        }
                    }
                    ü.Ücretlendir();
                    #endregion

                    string smd = D_TarihSaat_UTC.Yazıya(DateTime.UtcNow);
                    Tedaviler_SeriNoluİşlem_ sn_ler = new Tedaviler_SeriNoluİşlem_();
                    sn_ler.Hepsi = new Dictionary<string, string>();
                    for (int i = 0; i < SeriNolar.Count; i++)
                    {
                        if (!TanımDönüştürücü.Bul(SeriNolar[i], out string Uzak)) continue;

                        sn_ler.Hepsi.Add(Uzak, smd);
                    }
                    if (sn_ler.Hepsi.Count == 0) return;
                    var json = Json.Nesneden_Yazıya(sn_ler, sn_ler.GetType());
                    var içerik = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                    HttpResponseMessage cevap = H.İstekYap_Detaylı("PUT", Ortak.Sayfa_Tedavi + "/" + Klinik.Key + "/Sni/Ot", içerik);
                    string Ot = cevap.Headers.Contains("Ot") ? cevap.Headers.GetValues("Ot").First() : null;
                    
                    TanımDönüştürücü.Ekle(DosyaAdı, Ot);

#if DEBUG
                    Console.WriteLine("bitti öt");
#endif
                }
                catch (System.Exception ex) { Ortak.Günlük(ex.ToString()); }
                finally { H.Başlat(null); }
            });
        }

        public void ÖdemeTalebiİptalEt(string Klinik_EkranAdı, string DosyaAdı)
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                Haberleşme_ H = new takip_com.Haberleşme_();

                try
                {
                    if (!TanımDönüştürücü.Bul(DosyaAdı, out string Uzak)) return;

                    H.Başlat(Ortak.GeçerliKullanıcı_EkranAdı);

                    var Klinik = Klinik_Dosyalama_.KontrolEt(Klinik_EkranAdı);

                    HttpResponseMessage cevap = H.İstekYap_Detaylı("DELETE", Ortak.Sayfa_Tedavi + "/" + Klinik.Key + "/Sni/Ot/" + Uzak, null);

                    TanımDönüştürücü.Sil(DosyaAdı);

#if DEBUG
                    Console.WriteLine("bitti öt ipt");
#endif
                }
                catch (System.Exception ex) { Ortak.Günlük(ex.ToString()); }
                finally { H.Başlat(null); }
            });
        }

        public void Öde(string Klinik_EkranAdı, string DosyaAdı, string AlınanÖdemeMiktarı, string Notlar)
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                Haberleşme_ H = new takip_com.Haberleşme_();

                try
                {
                    if (!TanımDönüştürücü.Bul(DosyaAdı, out string Uzak)) return;

                    H.Başlat(Ortak.GeçerliKullanıcı_EkranAdı);

                    var Klinik = Klinik_Dosyalama_.KontrolEt(Klinik_EkranAdı);

                    var ödm = new Tedaviler_Ödeme_();
                    ödm.Notlar = Notlar;
                    ödm.AlınanÖdeme = Convert.ToDecimal(AlınanÖdemeMiktarı.NoktalıSayıya());

                    H.İstekYap("PUT", Ortak.Sayfa_Tedavi + "/" + Klinik.Key + "/Sni/O/" + Uzak, ödm);

                    TanımDönüştürücü.Sil(DosyaAdı);

#if DEBUG
                    Console.WriteLine("bitti ödeme");
#endif
                }
                catch (System.Exception ex) { Ortak.Günlük(ex.ToString()); }
                finally { H.Başlat(null); }
            });
        }
    }
}
