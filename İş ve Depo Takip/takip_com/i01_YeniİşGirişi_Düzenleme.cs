using ArgeMup.HazirKod.Ekİşlemler;
using İş_ve_Depo_Takip.takip_com.UçNokta;
using System;
using System.Collections.Generic;

namespace İş_ve_Depo_Takip.takip_com
{
    public class İşlem_YeniİşGirişi_Düzenleme_
    {
        public void Çalıştır(Banka.Talep_Ekle_Detaylar_ Detaylar)
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                Haberleşme_ H = new takip_com.Haberleşme_();

                try
                {
                    if (!TanımDönüştürücü.Bul(Detaylar.SeriNo, out string Uzak))
                    {
                        new İşlem_YeniİşGirişi_().Çalıştır(Detaylar);
                    }
                    else
                    {
                        H.Başlat(Ortak.GeçerliKullanıcı_EkranAdı);

                        var Klinik = Klinik_Dosyalama_.KontrolEt(Detaylar.Müşteri);
                        Detaylar.SeriNo = Uzak;
                        Tedavi_ Tedavi = Tedaviler_Dosyalama_.Tablo_Oku("/" + Klinik.Key + "/Tbl/A", Detaylar.SeriNo);

                        Tedavi.Hasta = Detaylar.Hasta;
                        Tedavi.Notlar = Detaylar.Notlar;
                        if (Detaylar.İskonto != null && float.TryParse(Detaylar.İskonto, out float isk)) Tedavi.İskonto_Yüzde = isk;

                        for (int i = 0; i < Detaylar.İşTürleri.Count; i++)
                        {
                            Detaylar.İşTürleri[i] = Detaylar.İşTürleri[i].Trim();
                            İşTürü_Dosyalama_.KontrolEt(Detaylar.İşTürleri[i]);

                            decimal? ücr = null;
                            if (decimal.TryParse(Detaylar.Ücretler[i], out decimal _ücr)) ücr = _ücr;

                            DateTime? çt = null;
                            if (Detaylar.ÇıkışTarihleri[i] != null) çt = Detaylar.ÇıkışTarihleri[i].TarihSaate().ToUniversalTime();

                            Tedavi.İşler.Add(
                                Detaylar.GirişTarihleri[i],
                                new İş_()
                                {
                                    işTürü = Detaylar.İşTürleri[i],
                                    Ücret_ElleGirilen = ücr,
                                    AdetVeKonum = Detaylar.Adetler[i],
                                    İşÇıkışTarihi = çt
                                });
                        }

                        List<string> de = new List<string>();
                        foreach (var dsy in Detaylar.DosyaEkleri)
                        {
                            if (System.IO.Path.IsPathRooted(dsy)) de.Add(dsy);
                        }

                        H.Gönder_Tedavi("PUT", "/" + Klinik.Key + "/Tbl/" + Detaylar.SeriNo, Tedavi, de);

#if DEBUG
                        Console.WriteLine("bitti yig dzl");
#endif
                    }
                }
                catch (System.Exception ex) { Ortak.Günlük(ex.ToString()); }
                finally { H.Başlat(null); }
            });
        }
    }
}
