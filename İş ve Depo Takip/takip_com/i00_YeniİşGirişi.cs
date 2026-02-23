using İş_ve_Depo_Takip.takip_com.UçNokta;
using System;

namespace İş_ve_Depo_Takip.takip_com
{
    public class İşlem_YeniİşGirişi_
    {
        public void Çalıştır(Banka.Talep_Ekle_Detaylar_ Detaylar)
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                Haberleşme_ H = new takip_com.Haberleşme_();

                try
                {
                    H.Başlat(Ortak.GeçerliKullanıcı_EkranAdı);

                    var Klinik = Klinik_Dosyalama_.KontrolEt(Detaylar.Müşteri);

                    Tedavi_ Tedavi = new Tedavi_();
                    Tedavi.Hasta = Detaylar.Hasta;
                    Tedavi.Notlar = Detaylar.Notlar;
                    if (Detaylar.İskonto != null && float.TryParse(Detaylar.İskonto.Replace(".", ","), out float isk)) Tedavi.İskonto_Yüzde = isk;

                    Tedavi.İşler = new System.Collections.Generic.Dictionary<string, İş_>();
                    for (int i = 0; i < Detaylar.İşTürleri.Count; i++)
                    {
                        Detaylar.İşTürleri[i] = Detaylar.İşTürleri[i].Trim();
                        İşTürü_Dosyalama_.KontrolEt(Detaylar.İşTürleri[i]);

                        decimal? ücr = null;
                        if (decimal.TryParse(Detaylar.Ücretler[i], out decimal _ücr)) ücr = _ücr;

                        Tedavi.İşler.Add(
                            Detaylar.GirişTarihleri[i],
                            new İş_()
                            {
                                işTürü = Detaylar.İşTürleri[i],
                                Ücret_ElleGirilen = ücr,
                                AdetVeKonum = Detaylar.Adetler[i],
                            });
                    }

                    string uzakserino = H.Gönder_Tedavi("POST", "/" + Klinik.Key + "/Tbl", Tedavi, Detaylar.DosyaEkleri);
                    TanımDönüştürücü.Ekle(Detaylar.SeriNo, uzakserino);

#if DEBUG
                    Console.WriteLine("bitti yig");
#endif
                }
                catch (System.Exception ex) { Ortak.Günlük(ex.ToString()); }
                finally { H.Başlat(null); }
            });             
        }
    }
}
