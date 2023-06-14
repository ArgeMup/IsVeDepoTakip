using ArgeMup.HazirKod;
using ArgeMup.HazirKod.Ekİşlemler;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace İş_ve_Depo_Takip
{
    class Değişkenler
    {
        public readonly static string[] Sabitler = new string[]
        {
            "TCmbDolar",
            "TCmbAvro",
            "DiğerDolar",
            "DiğerAvro"
        };
        public readonly static string[] YasakKelimeler_Ad = new string[]
        {
            "%",
            "#",
            "<=0",
            "Yuvarla ",
            "EnBüyük ",
            "EnKüçük ",
            "Koşul ",
            "KoşulaUyanİlk "
        };
        static Dictionary<string, string> Tümü_ = null;
        public static Dictionary<string, string> Tümü
        {
            get 
            { 
                if (Tümü_ == null)
                {
                    Tümü_ = new Dictionary<string, string>();
                    for (int i = 0; i < Sabitler.Length; i++) Tümü_.Add(Sabitler[i], "%" + Sabitler[i] + "%"); //kendi kendisini hesaplatması için

                    foreach (IDepo_Eleman dğş in Banka.Ayarlar_Genel("Değişkenler", true).Elemanları)
                    {
                        Tümü_.Add(dğş.Adı, dğş[0]);
                    }
                }

                return Tümü_;
            }
            set
            {
                Tümü_ = null;
            }
        }
        const int iç_içe_çağırma_sayacı_sabiti = 555;

        public static string Hesapla(string Formül, out double Çıktı, Dictionary<string, string> TümDeğişkenler = null)
        {
            //%Yuvarla <basamak>%   -> %Yuvarla 1% (Birler basamaını yukarı yuvarlar) 0->0, 1...10->10, 11...20->20
            //%değişken adı%        -> %döviz kuru%
            //Sabit sayı            -> 3,5
            //dört işlem            -> + - * /
            //İşlem sırası          -> parantezlerle
            //İşlem sonucu 0 dan küçük ise işleme devam etmek için -> %<=0%
            //Bir grup sayının en büyüğünü bulmak için -> %EnBüyük <p1> <p2>% değişken adında boşluk olmamalı
            //Bir grup sayının en küçüğünü bulmak için -> %EnKüçük <p1> <p2>% değişken adında boşluk olmamalı
            //%Koşul <Kıstas> <Evet> <Hayır>%
            //%KoşulaUyanİlk <()Kıstas> <Eleman1> <Elaman2> ...

            Çıktı = 0;
            string Formül_ilkHali = Formül;
            try
            {
                if (Formül.Contains("#")) Formül = Formül.Remove(Formül.IndexOf("#")); //notları sil

                string snç = _Hesapla_(Formül, out Çıktı, TümDeğişkenler ?? Tümü, 0);
                if (snç.DoluMu()) return snç;

                if (Çıktı <= 0 && Formül.Contains("%") && !Formül.Contains("%<=0%")) return "Formüllü bir işlemin hesaplama sonucu 0 dan büyük olmalı (" + Formül_ilkHali + " = " + Çıktı.Yazıya() + ")";

                return null;
            }
            catch (Exception ex)
            {
                return "Hesaplamada beklenmeyen durum oluştu. (" + ex.Message + ") (" + Formül_ilkHali + ")";
            }
        }
        static string _Hesapla_(string Formül, out double Çıktı, Dictionary<string, string> TümDeğişkenler, int iç_içe_çağırma_sayacı)
        {
            Çıktı = 0;
            if (Formül.Contains("#")) Formül = Formül.Remove(Formül.IndexOf("#")); //notları sil
            if (Formül.Contains("%<=0%")) Formül = Formül.Replace("%<=0%", "");

            int yuvarla = int.MinValue;
            if (Formül.Contains("%Yuvarla "))
            {
                string snç = _ArasındakiniAl_(Formül, @"%Yuvarla ", @"%", out string DeğişkenAdı);
                if (snç.DoluMu()) return snç;
                if (DeğişkenAdı.BoşMu()) return "Formülde yuvarlama basamak değeri mevcut değil (" + Formül + ")";

                if (int.TryParse(DeğişkenAdı, out yuvarla))
                {
                    Formül = Formül.Replace("%Yuvarla " + yuvarla + "%", "");
                }
            }

            while (ArgeMup.HazirKod.ArkaPlan.Ortak.Çalışsın && ++iç_içe_çağırma_sayacı < iç_içe_çağırma_sayacı_sabiti)
            {
                if (TümDeğişkenler.Count == 0) break;

                string snç = _ArasındakiniAl_(Formül, @"%", @"%", out string DeğişkenAdı), içerik = null;
                if (snç.DoluMu()) return snç;
                if (DeğişkenAdı.BoşMu()) break;

                if (DeğişkenAdı.StartsWith("EnBüyük ") || DeğişkenAdı.StartsWith("EnKüçük "))
                {
                    string[] dizi_alt_değişkenler = DeğişkenAdı.Split(' ');
                    if (dizi_alt_değişkenler == null || dizi_alt_değişkenler.Length < 3) return "EnBüyük işlemi girdileri geçersiz (" + DeğişkenAdı + ")";

                    double[] dizi_Alt_değişkenler_çıktı = new double[dizi_alt_değişkenler.Length - 1];
                    for (int i = 1; i < dizi_alt_değişkenler.Length; i++)
                    {
                        string dizi_alt_değişken_formül = TümDeğişkenler.ContainsKey(dizi_alt_değişkenler[i]) ? "%" + dizi_alt_değişkenler[i] + "%" : dizi_alt_değişkenler[i];
                        string snç_alt_değişken = _Hesapla_(dizi_alt_değişken_formül, out dizi_Alt_değişkenler_çıktı[i - 1], TümDeğişkenler, ++iç_içe_çağırma_sayacı);
                        if (snç_alt_değişken.DoluMu()) return snç_alt_değişken;
                    }

                    içerik = DeğişkenAdı.StartsWith("EnBüyük ") ? dizi_Alt_değişkenler_çıktı.Max().Yazıya() : dizi_Alt_değişkenler_çıktı.Min().Yazıya();
                }
                else if (DeğişkenAdı.StartsWith("Koşul "))
                {
                    string[] dizi_alt_değişkenler = DeğişkenAdı.Split(' ');
                    if (dizi_alt_değişkenler == null || dizi_alt_değişkenler.Length != 4) return "Koşul işlemi girdileri geçersiz (" + DeğişkenAdı + ")";

                    string dizi_alt_değişken_formül = TümDeğişkenler.ContainsKey(dizi_alt_değişkenler[1]) ? "%" + dizi_alt_değişkenler[1] + "%" : dizi_alt_değişkenler[1];
                    string snç_alt_değişken = _Hesapla_(dizi_alt_değişken_formül, out double dizi_Alt_değişkenler_çıktı, TümDeğişkenler, ++iç_içe_çağırma_sayacı);
                    if (snç_alt_değişken.DoluMu()) return snç_alt_değişken;

                    string kıstas = dizi_alt_değişkenler[dizi_Alt_değişkenler_çıktı > 0 ? 2 : 3];
                    dizi_alt_değişken_formül = TümDeğişkenler.ContainsKey(kıstas) ? "%" + kıstas + "%" : kıstas;
                    snç_alt_değişken = _Hesapla_(dizi_alt_değişken_formül, out dizi_Alt_değişkenler_çıktı, TümDeğişkenler, ++iç_içe_çağırma_sayacı);
                    if (snç_alt_değişken.DoluMu()) return snç_alt_değişken;

                    içerik = dizi_Alt_değişkenler_çıktı.Yazıya();
                }
                else if (DeğişkenAdı.StartsWith("KoşulaUyanİlk "))
                {
                    string[] dizi_alt_değişkenler = DeğişkenAdı.Split(' ');
                    if (dizi_alt_değişkenler == null || dizi_alt_değişkenler.Length < 3 || !dizi_alt_değişkenler[1].Contains("()")) return "KoşulaUyanİlk işlemi girdileri geçersiz (" + DeğişkenAdı + ")";

                    for (int i = 2; i < dizi_alt_değişkenler.Length && içerik == null; i++)
                    {
                        string dizi_alt_değişken_formül = TümDeğişkenler.ContainsKey(dizi_alt_değişkenler[i]) ? "%" + dizi_alt_değişkenler[i] + "%" : dizi_alt_değişkenler[i];
                        string snç_alt_değişken = _Hesapla_(dizi_alt_değişken_formül, out double dizi_Alt_değişkenler_çıktı, TümDeğişkenler, ++iç_içe_çağırma_sayacı);
                        if (snç_alt_değişken.DoluMu()) return snç_alt_değişken;

                        dizi_alt_değişken_formül = dizi_alt_değişkenler[1].Replace("()", dizi_Alt_değişkenler_çıktı.Yazıya());
                        snç_alt_değişken = _Hesapla_(dizi_alt_değişken_formül, out double dizi_Alt_değişkenler_çıktı_2, TümDeğişkenler, ++iç_içe_çağırma_sayacı);
                        if (snç_alt_değişken.DoluMu()) return snç_alt_değişken;

                        if (dizi_Alt_değişkenler_çıktı_2 > 0) içerik = dizi_Alt_değişkenler_çıktı.Yazıya();
                    }
                   
                    if (içerik == null) return "KoşulaUyanİlk işlemi girdilerinin hiçbiri uygun değil (" + DeğişkenAdı + ")";
                }
                else
                {
                    if (!TümDeğişkenler.ContainsKey(DeğişkenAdı)) return DeğişkenAdı + " adında bir değişken mevcut değil";

                    int Sabit_Değişken_Sıra_No = -1;
                    for (int i = 0; i < Sabitler.Length; i++)
                    {
                        if (Sabitler[i] == DeğişkenAdı)
                        {
                            Sabit_Değişken_Sıra_No = i;
                            break;
                        }
                    }

                    if (Sabit_Değişken_Sıra_No >= 0)
                    {
                        bool bekle = true;
                        Döviz.KurlarıAl(_GeriBildirim_Kurlar_);
                        System.Threading.Thread.Sleep(1);
                        while (ArgeMup.HazirKod.ArkaPlan.Ortak.Çalışsın && bekle)
                        {
                            System.Threading.Thread.Sleep(1);
                            Application.DoEvents();
                        }

                        void _GeriBildirim_Kurlar_(string Yazı, string[] Dizi)
                        {
                            TümDeğişkenler[DeğişkenAdı] = Dizi[Sabit_Değişken_Sıra_No];
                            bekle = false;
                        }
                    }

                    içerik = TümDeğişkenler[DeğişkenAdı];
                    if (içerik.Contains("%"))
                    {
                        string snç_2 = _Hesapla_(içerik, out double çıktı2, TümDeğişkenler, ++iç_içe_çağırma_sayacı);
                        if (snç_2.DoluMu()) return snç_2;
                        içerik = çıktı2.Yazıya();
                    }
                }
                
                Formül = Formül.Replace("%" + DeğişkenAdı + "%", içerik);
            }

            if (iç_içe_çağırma_sayacı >= iç_içe_çağırma_sayacı_sabiti) return "Formül çok fazla atıf yaptığı için atlandı (" + Formül + ")";

            DataTable İşlemci = new DataTable();
            Çıktı = Convert.ToDouble(İşlemci.Compute(Formül, null), System.Globalization.CultureInfo.InvariantCulture);
            if (yuvarla != int.MinValue) Çıktı = _Yuvarla_(Çıktı, yuvarla);
            return null;

            string _ArasındakiniAl_(string Girdi_, string Başlangıç_, string Bitiş_, out string DeğişkenAdı_)
            {
                DeğişkenAdı_ = null;

                int yuzde_karakteri_konumu_ilk = Girdi_.IndexOf(Başlangıç_);
                if (yuzde_karakteri_konumu_ilk < 0) return null;

                int yuzde_karakteri_konumu_son = Formül.IndexOf(Bitiş_, yuzde_karakteri_konumu_ilk + 1);
                if (yuzde_karakteri_konumu_son < 0) return "Formülde son % karakteri bulunamadı (" + Formül + ")";

                yuzde_karakteri_konumu_ilk += Başlangıç_.Length; //% yi atla
                yuzde_karakteri_konumu_son -= yuzde_karakteri_konumu_ilk; //adet ollarak kullan
                DeğişkenAdı_ = Formül.Substring(yuzde_karakteri_konumu_ilk, yuzde_karakteri_konumu_son);
                if (DeğişkenAdı_.BoşMu(true)) return "Formülün değişken adı boş (" + Formül + ")";

                return null;
            }
            double _Yuvarla_(double Sayı_, int Basamak_)
            {
                if (Basamak_ > 0)
                {
                    double basamak__ = Math.Pow(10, Basamak_);
                    return Math.Ceiling(Sayı_ / basamak__) * basamak__;
                }
                else if (Basamak_ == 0) return Math.Truncate(Sayı_);
                else
                {
                    Basamak_ = Math.Abs(Basamak_);
                    return Math.Round(Sayı_, Basamak_, MidpointRounding.AwayFromZero);
                }
            }
        }
    }
}
