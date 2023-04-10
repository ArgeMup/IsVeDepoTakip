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
        static IDonanımHaberlleşmesi Sunucu_DoHa = null;

        public static void Başlat()
        {
            if (Sunucu != null) return;

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
            if (Sunucu != null) Sunucu.Dispose();
            Sunucu = null;
            Sunucu_DoHa = null;
        }

        static void GeriBildirim_Islemi(string Kaynak, GeriBildirim_Türü_ Tür, object İçerik, object Hatırlatıcı)
        {
            if (Tür == GeriBildirim_Türü_.BilgiGeldi)
            {
                string[] istek = ((byte[])İçerik).Yazıya().Split('\n');
                string[] Sayfa_İçeriği = istek[0].Trim(' ', '\r').Split(' ')[1].Trim('/').Split('/');
                byte[] Gönderilecek_İçerik, Gönderilecek_Sayfa;
                string SayfaBaşlığı = "ArGeMuP " + Kendi.Adı + " " + Kendi.Sürüm;

                if (Sayfa_İçeriği[0] == "DoEk")
                {
                    string Soyadı, KapalıAdı;

                    if (Sayfa_İçeriği.Length == 2)
                    {
                        KapalıAdı = Ortak.Klasör_Gecici + "DoEk\\" + Sayfa_İçeriği[1];
                        Soyadı = Path.GetExtension(Sayfa_İçeriği[1]).Remove(0, 1);
                    }
                    else if (Sayfa_İçeriği.Length == 3)
                    {
                        while (Sayfa_İçeriği[2].Contains("%"))
                        {
                            int k = Sayfa_İçeriği[2].IndexOf("%");                                      //%C3%96deme
                            byte _0 = Convert.ToByte(Sayfa_İçeriği[2].Substring(k + 1, 2), 16);
                            
                            int s = 0; //110aaaaa 1110aaaa 11110aaa -> 1 lerin sayısı : karakteri oluşturmak için gereken bayt
                            while ((_0 & 0x80) > 0) { s++; _0 <<= 1; }
                            if (s > 4) goto Hata;
                            else if (s == 0) s = 1;

                            s = s * 3/*%ab*/;
                            string kesilecek = Sayfa_İçeriği[2].Substring(k, s);                        //%C3%96
                            string dönüştürülecek = kesilecek.Replace("%", "");                         //C396
                            dönüştürülecek = dönüştürülecek.BaytDizisine_HexYazıdan().Yazıya();         //Ö
                            Sayfa_İçeriği[2] = Sayfa_İçeriği[2].Replace(kesilecek, dönüştürülecek);     //Ödeme
                        }

                        KapalıAdı = Banka.DosyaEkleri_GeciciKlasöreKopyala(Sayfa_İçeriği[1], Sayfa_İçeriği[2]);
                        Soyadı = Path.GetExtension(Sayfa_İçeriği[2]).Remove(0, 1);
                    }
                    else goto Hata; 

                    if (Soyadı == "bmp" || Soyadı == "png") Soyadı = "image/" + Soyadı;
                    else if (Soyadı == "jpg") Soyadı = "image/jpeg";
                    else if (Soyadı == "ico") Soyadı = "image/x-icon";
                    else if (Soyadı == "pdf") Soyadı = "application/pdf";
                    else if (Soyadı == "txt") Soyadı = "text/plain";
                    else Soyadı = "application/octet-stream";
                    
                    Gönderilecek_İçerik = File.ReadAllBytes(KapalıAdı);
                    Gönderilecek_Sayfa = (
                            "HTTP/1.1 200 OK" + Environment.NewLine +
                            "Server: " + SayfaBaşlığı + Environment.NewLine +
                            "Content-Length: " + Gönderilecek_İçerik.Length + Environment.NewLine +
                            "Content-Type: " + Soyadı + Environment.NewLine +
                            "Connection: Closed" + Environment.NewLine + Environment.NewLine).BaytDizisine();
                }
                else
                {
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
                            Banka.Talep_Ayıkla_İşTürüDalı(it, out string İşTürü, out string GirişTarihi, out string ÇıkışTarihi, out _, out _);
                            İşler += "<tr><td>" + Banka.Yazdır_Tarih(GirişTarihi) + "</td><td>" + Banka.Yazdır_Tarih(ÇıkışTarihi) + "</td><td>" + İşTürü + "</td></tr>";
                        }

                        string dosya_eki_resim = "", dosya_eki_diğer = "";
                        System.Collections.Generic.List<string> l_DosyaEkleri = Banka.DosyaEkleri_Listele(Sayfa_İçeriği[0]);
                        foreach (string DosyaEki in l_DosyaEkleri)
                        {
                            string soyadı = Path.GetExtension(DosyaEki).Remove(0, 1).ToLower();
                            string kapalı_adı = Banka.DosyaEkleri_GeciciKlasöreKopyala(Sayfa_İçeriği[0], DosyaEki);

                            if (soyadı == "jpg" || soyadı == "png" || soyadı == "bmp")
                            {
                                //<img src="DoEk/1.jpg" alt="1" width="32%" onclick="Buyut(this)">
                                dosya_eki_resim += "<img src=\"DoEk/" + Sayfa_İçeriği[0] + "/" + Path.GetFileName(DosyaEki) + "\" alt=\"" + Path.GetFileName(DosyaEki) + "\" width=\"32%\" onclick=\"Buyut(this)\">";
                            }
                            else
                            {
                                //<a href="DoEk/a.pdf" target="_blank">DosyaAdı</a><br>
                                dosya_eki_diğer += "<a href=\"DoEk/" + Sayfa_İçeriği[0] + "/" + Path.GetFileName(DosyaEki) + "\" target=\"_blank\">" + Path.GetFileName(DosyaEki) + "</a><br>";
                            }
                        }
                        DosyaEkleri = dosya_eki_diğer + "<br>" + dosya_eki_resim;

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
                            "Connection: Closed" + Environment.NewLine + Environment.NewLine).BaytDizisine();
                }

                byte[] çıktı = new byte[Gönderilecek_Sayfa.Length + Gönderilecek_İçerik.Length];
                Array.Copy(Gönderilecek_Sayfa, 0, çıktı, 0, Gönderilecek_Sayfa.Length);
                Array.Copy(Gönderilecek_İçerik, 0, çıktı, Gönderilecek_Sayfa.Length, Gönderilecek_İçerik.Length);
                Sunucu_DoHa.Gönder(çıktı, Kaynak);
            }
            return;

            Hata:
            Sunucu.Durdur(Kaynak); 
        }

        #region Seri Nolu İstek
        //GET /DoEk/Uygulama.ico HTTP/1.1
        //GET /DoEk/LOGO.bmp HTTP/1.1
        //GET /DoEk/asdfg.hjk.jpg HTTP/1.1
        //GET /A2389 HTTP/1.1
        //Host: localhost
        //Connection: keep-alive
        //sec-ch-ua: "Google Chrome";v="111", "Not(A:Brand";v="8", "Chromium";v="111"
        //sec-ch-ua-mobile: ?0
        //sec-ch-ua-platform: "Windows"
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
        //Content-Type: text/html/plain image/x-icon/jpeg/png/bmp application/pdf/octet-stream
        //Connection: Closed
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
    }
}
