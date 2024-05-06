using ArgeMup.HazirKod;
using ArgeMup.HazirKod.DonanımHaberleşmesi;
using ArgeMup.HazirKod.Ekİşlemler;
using System;
using System.IO;

namespace İş_ve_Depo_Takip
{
    public static class HttpSunucu
    {
        static TcpSunucu_ Sunucu = null;
        static IDonanımHaberleşmesi Sunucu_DoHa = null;

        public static void Başlat()
        {
            IDepo_Eleman snc = Banka.Ayarlar_BilgisayarVeKullanıcı("Http Sunucu");
            if (snc == null || snc.Oku_TamSayı(null) <= 0) return;

            //Sayfadaki görsellerin oluşturulması
            using (MemoryStream ms = new MemoryStream())
            {
                Properties.Resources.kendi.Save(ms);
                File.WriteAllBytes(Ortak.Klasör_Gecici + "DoEk\\Uygulama.ico", ms.ToArray());
            }
            Ortak.Firma_Logo.Save(Ortak.Klasör_Gecici + "DoEk\\LOGO.bmp");

            Sunucu = new TcpSunucu_(snc.Oku_TamSayı(null), GeriBildirim_Islemi, SatırSatırGönderVeAl:false, SadeceYerel: false, Sessizlik_ZamanAşımı_msn:15000);
            Sunucu_DoHa = Sunucu;
        }
        public static void Bitir()
        {
            Sunucu?.Dispose();
            Sunucu = null;
            Sunucu_DoHa = null;
        }

        static void GeriBildirim_Islemi(string Kaynak, GeriBildirim_Türü_ Tür, object İçerik, object Hatırlatıcı)
        {
            if (Tür == GeriBildirim_Türü_.BilgiGeldi)
            {
                string[] istek = ((byte[])İçerik).Yazıya().Split('\n');
                string[] Sayfa_İçeriği = istek[0].Trim(' ', '\r').Split(' ')[1].Trim('/').Split('/');
                byte[] Gönderilecek_İçerik = null, Gönderilecek_Sayfa;
                string SayfaBaşlığı = "ArGeMuP " + Kendi.Adı + " " + Kendi.Sürüm;

                if (Sayfa_İçeriği[0] == "DoEk")
                {
                    string Soyadı, KapalıAdı;

                    if (Sayfa_İçeriği.Length == 2)
                    {
                        //uygulamaya ait dosyaşar
                        KapalıAdı = Ortak.Klasör_Gecici + "DoEk\\" + Sayfa_İçeriği[1];
                        Soyadı = Path.GetExtension(Sayfa_İçeriği[1]).Remove(0, 1);
                    }
                    else if (Sayfa_İçeriği.Length == 3)
                    {
                        //işlerin dosya ekleri
                        Sayfa_İçeriği[2] = AğAraçları.WebAdresinden_Yazıya(Sayfa_İçeriği[2]);
                        if (Sayfa_İçeriği[2] == null) goto Hata;

                        KapalıAdı = Banka.DosyaEkleri_GeciciKlasöreKopyala(Sayfa_İçeriği[1], Sayfa_İçeriği[2], out _);
                        Soyadı = Path.GetExtension(Sayfa_İçeriği[2]).Remove(0, 1);
                    }
                    else goto Hata; 

                    if (Soyadı == "bmp" || Soyadı == "png" || Soyadı == "gif") Soyadı = "image/" + Soyadı;
                    else if (Soyadı == "jpg") Soyadı = "image/jpeg";
                    else if (Soyadı == "ico") Soyadı = "image/x-icon";
                    else if (Soyadı == "pdf") Soyadı = "application/pdf";
                    else if (Soyadı == "txt") Soyadı = "text/plain";
                    else Soyadı = "application/octet-stream";

                    string ÜretilenEtag = File.GetLastWriteTime(KapalıAdı).Yazıya();
                    string TalepEdilenEtag = null;
                    foreach (string başlık in istek)
                    {
                        if (başlık.StartsWith("If-None-Match"))
                        {
                            TalepEdilenEtag = başlık.Substring("If-None-Match".Length).Trim(':', ' ', '\r');
                            break;
                        }
                    }

                    if (ÜretilenEtag == TalepEdilenEtag)
                    {
                        Gönderilecek_Sayfa = (
                                "HTTP/1.1 304 Not Modified" + Environment.NewLine +
                                "Server: " + SayfaBaşlığı + Environment.NewLine +
                                "Content-Length: 0" + Environment.NewLine +
                                "Connection: keep-alive" + Environment.NewLine +
                                Environment.NewLine).BaytDizisine();
                    }
                    else
                    {
                        Gönderilecek_İçerik = File.ReadAllBytes(KapalıAdı);
                        Gönderilecek_Sayfa = (
                                "HTTP/1.1 200 OK" + Environment.NewLine +
                                "Server: " + SayfaBaşlığı + Environment.NewLine +
                                "Content-Length: " + Gönderilecek_İçerik.Length + Environment.NewLine +
                                "Content-Type: " + Soyadı + Environment.NewLine +
                                "Cache-Control: max-age=604800, public" + Environment.NewLine + //1 hafta
                                "ETag: " + ÜretilenEtag + Environment.NewLine +
                                "Connection: keep-alive" + Environment.NewLine + Environment.NewLine).BaytDizisine();
                    }
                }
                else
                {
                    //Sayfa içeriği
                    int knm_sn = Sayfa_İçeriği[0].IndexOf("serino=");
                    if (knm_sn >= 0)
                    {
                        knm_sn += 7 /*serino=*/;
                        Sayfa_İçeriği[0] = Sayfa_İçeriği[0].Substring(knm_sn);
                    }
                    Sayfa_İçeriği[0] = Sayfa_İçeriği[0].ToUpper();

                    Banka.Talep_Bul_Detaylar_ Detaylar = Banka.Talep_Bul(Sayfa_İçeriği[0]);
                    string Hasta = "Bulunamadı", Notlar = null, MüşteriNotları = null, İşler = null, DosyaEkleri = null;
                    if (Detaylar != null)
                    {
                        Banka.Talep_Ayıkla_SeriNoDalı(Detaylar.SeriNoDalı, out _, out Hasta, out _, out Notlar, out _);

                        if (Notlar.DoluMu()) Notlar = Notlar.Replace("\n", "<br>");

                        foreach (IDepo_Eleman it in Detaylar.SeriNoDalı.Elemanları)
                        {
                            //<tr><td>31.12.2023</td><td>Levente iskelet üretimi</td></tr>
                            Banka.Talep_Ayıkla_İşTürüDalı(it, out string İşTürü, out string GirişTarihi, out string ÇıkışTarihi, out _, out _, out _);
                            İşler += "<tr><td>" + Banka.Yazdır_Tarih(GirişTarihi) + "</td><td>" + Banka.Yazdır_Tarih(ÇıkışTarihi) + "</td><td>" + İşTürü + "</td></tr>";
                        }

                        string dosya_eki_resim = "", dosya_eki_diğer = "";
                        DateTime ilk_tarih = DateTime.MinValue, EklenmeTarihi;
                        var l_dosya_ekleri = Banka.DosyaEkleri_Listele(Sayfa_İçeriği[0]);
                        for (int i = l_dosya_ekleri.Length - 1; i >= 0; i--)
                        {
                            Banka.DosyaEkleri_Ayıkla_SeriNoAltındakiDosyaEkiDalı(l_dosya_ekleri[i], out string DosyaAdı, out bool Html_denGöster);
                            if (!Html_denGöster) continue;
                     
                            string soyadı = Path.GetExtension(DosyaAdı).Remove(0, 1).ToLower();
                            Banka.DosyaEkleri_GeciciKlasöreKopyala(Sayfa_İçeriği[0], DosyaAdı, out EklenmeTarihi);

                            //güne göre gruplama
                            if (EklenmeTarihi.Day != ilk_tarih.Day ||
                                EklenmeTarihi.Month != ilk_tarih.Month ||
                                EklenmeTarihi.Year != ilk_tarih.Year)
                            {
                                if (ilk_tarih != DateTime.MinValue)
                                {
                                    //<br><br>Tarih Saat
                                    DosyaEkleri += "<br><br><b>" + Banka.Yazdır_Tarih(ilk_tarih.Yazıya()) + "</b>";
                                    DosyaEkleri += dosya_eki_diğer + "<br>" + dosya_eki_resim;

                                    dosya_eki_resim = "";
                                    dosya_eki_diğer = "";
                                }

                                ilk_tarih = EklenmeTarihi;
                            }

                            if (soyadı == "jpg" || soyadı == "png" || soyadı == "bmp" || soyadı == "gif")
                            {
                                //<img src="DoEk/1.jpg" alt="1" onclick="Buyut(this)" class="rsm" loading="lazy">
                                dosya_eki_resim += "<img src=\"DoEk/" + Sayfa_İçeriği[0] + "/" + Path.GetFileName(DosyaAdı) + "\" alt=\"" + Path.GetFileName(DosyaAdı) + "\" onclick=\"Buyut(this)\" class=\"rsm\" loading=\"lazy\">";
                            }
                            else
                            {
                                //<br><a href="DoEk/a.pdf" target="_blank">DosyaAdı</a>
                                dosya_eki_diğer += "<br><a href=\"DoEk/" + Sayfa_İçeriği[0] + "/" + Path.GetFileName(DosyaAdı) + "\" target=\"_blank\">" + Path.GetFileName(DosyaAdı) + "</a>";
                            }
                        }

                        //son ekleme yapılan güne ait dosyalar
                        if (dosya_eki_resim.DoluMu() || dosya_eki_diğer.DoluMu())
                        {
                            DosyaEkleri += "<br><br><b>" + Banka.Yazdır_Tarih(ilk_tarih.Yazıya()) + "</b>";
                            DosyaEkleri += dosya_eki_diğer + "<br>" + dosya_eki_resim;
                        }
                        
                        IDepo_Eleman Müşteri_Notlar = Banka.Ayarlar_Müşteri(Detaylar.Müşteri, "Notlar");
                        if (Müşteri_Notlar != null) { if (Müşteri_Notlar[0].DoluMu()) MüşteriNotları = Müşteri_Notlar[0].Replace("\n", "<br>"); }
                    }

                    string SayfaCevabı = Properties.Resources.SeriNoDetayları;
                    SayfaCevabı = SayfaCevabı.Replace("?=? Uygulama Adi ?=?", Sayfa_İçeriği[0] + " / " + SayfaBaşlığı);
                    SayfaCevabı = SayfaCevabı.Replace("?=? Müşteri ?=?", Detaylar != null ? Detaylar.Müşteri : "Bulunamadı");
                    SayfaCevabı = SayfaCevabı.Replace("?=? Hasta ?=?", Hasta);
                    SayfaCevabı = SayfaCevabı.Replace("?=? Seri No ?=?", Sayfa_İçeriği[0]);
                    SayfaCevabı = SayfaCevabı.Replace("<!--?=? Tablo İş Giriş Tarihi Ve İş ?=?-->", İşler);
                    SayfaCevabı = SayfaCevabı.Replace("<!--?=? Dosya Ekleri ?=?-->", DosyaEkleri);
                    if (Notlar.DoluMu(true)) SayfaCevabı = SayfaCevabı.Replace("<!--?=? Notlar ?=?-->", "<br><table><tr><th>Notlar</th></tr><tr><td>" + Notlar + "</td></tr></table>");
                    if (MüşteriNotları.DoluMu(true)) SayfaCevabı = SayfaCevabı.Replace("<!--?=? Müşteri Notları ?=?-->", "<br><table><tr><th>Müşteri Notları</th></tr><tr><td>" + MüşteriNotları + "</td></tr></table>");

                    Gönderilecek_İçerik = SayfaCevabı.BaytDizisine();
                    Gönderilecek_Sayfa = (
                            "HTTP/1.1 200 OK" + Environment.NewLine +
                            "Server: " + SayfaBaşlığı + Environment.NewLine +
                            "Content-Length: " + Gönderilecek_İçerik.Length + Environment.NewLine +
                            "Content-Type: text/html;charset=utf-8" + Environment.NewLine +
                            "Cache-Control: no-cache" + Environment.NewLine +
                            "Connection: keep-alive" + Environment.NewLine + Environment.NewLine).BaytDizisine();
                }

                if (Gönderilecek_İçerik == null) Sunucu_DoHa.Gönder(Gönderilecek_Sayfa, Kaynak);
                else
                {
                    byte[] çıktı = new byte[Gönderilecek_Sayfa.Length + Gönderilecek_İçerik.Length];
                    Array.Copy(Gönderilecek_Sayfa, 0, çıktı, 0, Gönderilecek_Sayfa.Length);
                    Array.Copy(Gönderilecek_İçerik, 0, çıktı, Gönderilecek_Sayfa.Length, Gönderilecek_İçerik.Length);
                    Sunucu_DoHa.Gönder(çıktı, Kaynak);
                }
            }
            return;

            Hata:
            Sunucu.Durdur(Kaynak); 
        }

        #region Seri Nolu İstek
        //GET /DoEk/Uygulama.ico HTTP/1.1
        //GET /DoEk/LOGO.bmp HTTP/1.1
        //GET /DoEk/SeriNo/asdfg.hjk.jpg HTTP/1.1
        //GET /A2389 HTTP/1.1
        //GET /serino=A2389 HTTP/1.1
        //GET /?serino=A2389 HTTP/1.1
        //Host: localhost
        //Connection: keep-alive
        //sec-ch-ua: "Google Chrome";v="111", "Not(A:Brand";v="8", "Chromium";v="111"
        //sec-ch-ua-mobile: ?0
        //sec-ch-ua-platform: "Windows"
        //If-None-Match: ETag
        //DNT: 1
        //Upgrade-Insecure-Requests: 1
        //User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/111.0.0.0 Safari/537.36
        //Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7
        //Sec-Fetch-Site: none
        //Sec-Fetch-Mode: navigate
        //Sec-Fetch-User: ?1
        //Sec-Fetch-Dest: document
        //Accept-Encoding: gzip, deflate, br
        //Accept-Language: tr-TR,tr;q=0.9,en-US;q=0.8,en;q=0.7
        //
        //

        //HTTP/1.1 200 OK
        //Server: Argemup Reklamı
        //Content-Length: 6                bayt olarak
        //Content-Type: text/html/plain image/x-icon/jpeg/png/bmp/gif application/pdf/octet-stream
        //Connection: keep-alive
        //ETag: görseller için
        //
        //içerik
        #endregion
    }

    public static class AğAraçları
    {
        static string Yerel_ip_ = null;
        public static string Yerel_ip
        {
            get 
            {
                if (Yerel_ip_ == null)
                {
                    System.Net.Sockets.Socket soket = null;
                    try
                    {
                        soket = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Dgram, 0);
                        soket.Connect("8.8.8.8", 65530);
                        System.Net.IPEndPoint uçnokta = soket.LocalEndPoint as System.Net.IPEndPoint;
                        Yerel_ip_ = uçnokta.Address.ToString();
                    }
                    catch (Exception) { }
                    finally { soket?.Dispose(); }

                    if (Yerel_ip_.BoşMu()) throw new Exception("Bilgisayarınızın yerel ip numarası okunamadı." + Environment.NewLine +
                        "İnternete bağlı bir bilgisayarda iseniz bağlantınızı kontrol ediniz." + Environment.NewLine +
                        "Sadece yerel ağda çalışan bir bilgisayarda iseniz (Ayarlar -> Etiketleme -> Barkod İçeriği) %BilgisayarınYerelAdresi% anahtarını silip yerine elle değer giriniz.");
                }

                return Yerel_ip_;
            }
        }

        public static string Htmlden_Yazıya(string Girdi)
        {
            if (Girdi.BoşMu(true)) return null;

            const string Eleme_Boşluk = @"(>|$)(\W|\n|\r)+<";           //Kodlama içindeki bir veya daha çok boşluk, satır sonu
            const string Eleme_Kodlaama = @"<[^>]*(>|$)";               //'<' ve '>' arasındaki tüm kodlama karakterleri
            const string Eleme_SatırSonu = @"<(br|BR)\s{0,1}\/{0,1}>";  //<br>,<br/>,<br />,<BR>,<BR/>,<BR />
            var Regex_SatırSonu = new System.Text.RegularExpressions.Regex(Eleme_SatırSonu, System.Text.RegularExpressions.RegexOptions.Multiline);
            var Regex_Kodlama = new System.Text.RegularExpressions.Regex(Eleme_Kodlaama, System.Text.RegularExpressions.RegexOptions.Multiline);
            var Regex_Boşluk = new System.Text.RegularExpressions.Regex(Eleme_Boşluk, System.Text.RegularExpressions.RegexOptions.Multiline);

            Girdi = System.Net.WebUtility.HtmlDecode(Girdi);
            Girdi = Regex_Boşluk.Replace(Girdi, "><");
            Girdi = Regex_SatırSonu.Replace(Girdi, Environment.NewLine);
            Girdi = Regex_Kodlama.Replace(Girdi, string.Empty);
            return Girdi;
        }

        public static string WebAdresinden_Yazıya(string Girdi)
        {
            if (Girdi.BoşMu(true)) return null; 

            while (Girdi.Contains("%"))
            {
                int k = Girdi.IndexOf("%");                                             //%C3%96deme
                byte _0 = Convert.ToByte(Girdi.Substring(k + 1, 2), 16);

                int s = 0; //110aaaaa 1110aaaa 11110aaa -> 1 lerin sayısı : karakteri oluşturmak için gereken bayt
                while ((_0 & 0x80) > 0) { s++; _0 <<= 1; }
                if (s > 4) return null;
                else if (s == 0) s = 1;

                s *= 3 /*%ab*/;
                string kesilecek = Girdi.Substring(k, s);                               //%C3%96
                string dönüştürülecek = kesilecek.Replace("%", "");                     //C396
                dönüştürülecek = dönüştürülecek.BaytDizisine_HexYazıdan().Yazıya();     //Ö
                Girdi = Girdi.Replace(kesilecek, dönüştürülecek);                       //Ödeme
            }

            return Girdi;
        }
    }

    public static class Döviz
    {
        static DateTime EnSonGüncelleme = DateTime.MinValue;
        static string Çıktı_yazı = null;
        static string[] Çıktı_dizi = null; //tcmb dolar avro, diğer dolar avro

        public static void KurlarıAl(Action<string, string[]> İşlem)
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                if ((DateTime.Now - EnSonGüncelleme).TotalMinutes > 5 || Çıktı_yazı.BoşMu() || Çıktı_yazı.Split(new string[] { "Okunamadı" }, StringSplitOptions.None).Length > 2)
                {
                    Çıktı_yazı = null;
                    Çıktı_dizi = new string[] { "-1", "-1", "-1", "-1" };

                    string Dosya_TCMB = Ortak.Klasör_Gecici + "TCMB_Kurlar.xml";
                    Dosya.Sil(Dosya_TCMB);
                    YeniYazılımKontrolü_ Kaynak_TCMB = new YeniYazılımKontrolü_();
                    Kaynak_TCMB.Başlat(new Uri("https://www.tcmb.gov.tr/kurlar/today.xml"), HedefDosyaYolu: Dosya_TCMB);

                    string Dosya_GenelPara = Ortak.Klasör_Gecici + "GenelPara_Kurlar.xml";
                    Dosya.Sil(Dosya_GenelPara);
                    System.Collections.Generic.Dictionary<string, string> İstekBaşlıkları = new System.Collections.Generic.Dictionary<string, string>() { { "User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36" } };
                    Dosya.AğÜzerinde_ Kaynak_GenelPara = new Dosya.AğÜzerinde_(new Uri("https://api.genelpara.com/embed/para-birimleri.json"), Dosya_GenelPara, İstekBaşlıkları: İstekBaşlıkları);

                    int za = Environment.TickCount + 15000;
                    while (ArgeMup.HazirKod.ArkaPlan.Ortak.Çalışsın && za > Environment.TickCount &&
                    (!Kaynak_TCMB.KontrolTamamlandı || !Kaynak_GenelPara.KontrolTamamlandı)) System.Threading.Thread.Sleep(350);

                    if (File.Exists(Dosya_TCMB))
                    {
                        try
                        {
                            System.Xml.XmlDocument xmlVerisi = new System.Xml.XmlDocument();
                            xmlVerisi.LoadXml(Dosya.Oku_Yazı(Dosya_TCMB));

                            string Tarih = xmlVerisi.SelectSingleNode("Tarih_Date").Attributes["Tarih"].InnerText;
                            string dolar = xmlVerisi.SelectSingleNode(string.Format("Tarih_Date/Currency[@Kod='{0}']/BanknoteSelling", "USD")).InnerText;
                            string avro = xmlVerisi.SelectSingleNode(string.Format("Tarih_Date/Currency[@Kod='{0}']/BanknoteSelling", "EUR")).InnerText;
                            Çıktı_yazı +=
                            "TCMB " + Tarih + " 15:30" + Environment.NewLine +
                            "Dolar = " + dolar + " ₺" + Environment.NewLine +
                            "Avro = " + avro + " ₺" + Environment.NewLine;

                            Çıktı_dizi[0] = dolar;
                            Çıktı_dizi[1] = avro;
                        }
                        catch (Exception) { }
                    }
                    else Çıktı_yazı += "TCMB Okunamadı" + Environment.NewLine;

                    if (File.Exists(Dosya_GenelPara))
                    {
                        try
                        {
                            string içerik = Dosya.Oku_Yazı(Dosya_GenelPara);
                            string dolar = _Al_(içerik, @"""USD"":{""satis"":""", @"""");
                            string avro = _Al_(içerik, @"""EUR"":{""satis"":""", @"""");

                            Çıktı_yazı +=
                            "Diğer " + File.GetLastWriteTime(Dosya_GenelPara).Yazıya("dd.MM.yyyy HH:mm") + Environment.NewLine +
                            "Dolar = " + dolar + " ₺" + Environment.NewLine +
                            "Avro = " + avro + " ₺";

                            Çıktı_dizi[2] = dolar;
                            Çıktı_dizi[3] = avro;

                            string _Al_(string Girdi, string Başlangıç, string Bitiş)
                            {
                                int knm_başla = Girdi.IndexOf(Başlangıç);
                                if (knm_başla < 0) return null;

                                knm_başla += Başlangıç.Length;
                                int knm_bitir = Girdi.IndexOf(Bitiş, knm_başla);
                                if (knm_bitir < 0) return null;

                                return Girdi.Substring(knm_başla, knm_bitir - knm_başla);
                            }
                        }
                        catch (Exception) { }
                    }
                    else Çıktı_yazı += "Diğer Okunamadı";

                    EnSonGüncelleme = DateTime.Now;
                    Kaynak_TCMB.Dispose();
                    Kaynak_GenelPara.Dispose();
                }

                İşlem?.Invoke(Çıktı_yazı, Çıktı_dizi);
            });
        }
        public static bool Oku(out string[] Kurlar)
        {
            Kurlar = Çıktı_dizi;

            return (DateTime.Now - EnSonGüncelleme).TotalMinutes <= 5;
        }
    }
}
