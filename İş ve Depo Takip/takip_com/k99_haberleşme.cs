using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace İş_ve_Depo_Takip.takip_com
{
    public class Haberleşme_
    {
        static string Çerez = null;
        public void Başlat(string Kullanıcı_EkranAdı)
        {
            if (Kullanıcı_EkranAdı == null) Çerez = null;
            else
            {
                Çerez = null;
                Kullanıcı_EkranAdı = "ivdt - " + Kullanıcı_EkranAdı;
                string eposta = D_HexYazı.NormalYazıdan(Kullanıcı_EkranAdı);
                string parola = ArgeMup.HazirKod.Dönüştürme.D_GeriDönülemezKarmaşıklaştırmaMetodu.Yazıdan("ivdt", 64).ToLower();
                Haberleşme_ H = new Haberleşme_();

                for (int i = 0; i < 5 && Çerez == null; i++)
                {
                    try
                    {
                        Çerez = H.OturumAnahtarınıAl("/" + eposta + "/" + parola + "/0");
                    }
                    catch (Exception)
                    {
                        Çerez = null;

                        System.Threading.Thread.Sleep(1500);
                    }
                }
            }
        }
        string OturumAnahtarınıAl(string Eposta_Parola_AçıkKalsın)
        {
            Çerez = null;

            HttpResponseMessage cevap = İstekYap_Detaylı("POST", Ortak.Sayfa_Kullanıcı_Girişi + Eposta_Parola_AçıkKalsın, null, 205);

            if (cevap.Headers.Contains("Set-Cookie"))
            {
                foreach (string biri in cevap.Headers.GetValues("Set-Cookie"))
                {
                    int knm = biri.IndexOf(';');
                    if (knm < 0) throw new Exception("kuki bilgisi hatalı " + biri);

                    //_fko=?hex?; expires=Fri, 07 Nov 2025 14:11:29 GMT; path=/; secure; samesite=strict; httponly
                    if (biri.StartsWith("_")) Çerez += biri.Remove(knm) + ";";
                }
            }

            if (((int)cevap.StatusCode) != 205) throw new Exception("dönüş kodu hatalı " + cevap.StatusCode);

            return Çerez;
        }

        HttpClient _http = new HttpClient(new HttpClientHandler { UseCookies = false });
        public int İstekYap(string Metod, string Sayfa, object Gönderilen, int Dönüş_kodu = 200)
        {
            HttpContent içerik = null;
            if (Gönderilen != null)
            {
                if (Gönderilen is string)
                {
                    içerik = new StringContent(Gönderilen as string, Encoding.UTF8, "text/plain");
                }
                else
                {
                    var json = Json.Nesneden_Yazıya(Gönderilen, Gönderilen.GetType());
                    içerik = new StringContent(json, Encoding.UTF8, "application/json");
                }
            }

            HttpResponseMessage cevap = İstekYap_Detaylı(Metod, Sayfa, içerik, Dönüş_kodu);

            return (int)cevap.StatusCode;
        }
        public T İstekYap<T>(string Metod, string Sayfa, object Gönderilen = null, int Dönüş_kodu = 200)
        {
            HttpContent içerik = null;
            if (Gönderilen != null)
            {
                if (Gönderilen is string)
                {
                    içerik = new StringContent(Gönderilen as string, Encoding.UTF8, "text/plain");
                }
                else
                {
                    var json = Json.Nesneden_Yazıya(Gönderilen, Gönderilen.GetType());
                    içerik = new StringContent(json, Encoding.UTF8, "application/json");
                }
            }

            HttpResponseMessage cevap = İstekYap_Detaylı(Metod, Sayfa, içerik, Dönüş_kodu);

            string yazı_olarak = cevap.Content.ReadAsStringAsync().Result;
            if (typeof(T) == typeof(string))
            {
                if (string.IsNullOrWhiteSpace(yazı_olarak)) throw new System.Exception("istek içeriği yazı olarak alınamadı");
                return (T)(object)yazı_olarak;
            }
            else
            {
                T çıktı = (T)Json.Yazıdan_Nesneye(yazı_olarak, typeof(T));
                if (çıktı == null) throw new System.Exception("istek içeriği nesne olarak alınamadı");
                return (T)(object)çıktı;
            }
        }
        public HttpResponseMessage İstekYap_Detaylı(string Metod, string Sayfa, HttpContent İçerik, int Dönüş_kodu = 200)
        {
            string[] segmentler = Sayfa.Split('/');
            var kodlanmisSegmentler = segmentler.Select(segment =>
            {
                if (string.IsNullOrEmpty(segment))
                {
                    return segment;
                }
                return System.Net.WebUtility.UrlEncode(segment);
            });
            Sayfa = BuSite.Adres_Tam + '/' + BuSite.Sürüm + string.Join("/", kodlanmisSegmentler);

            _http.DefaultRequestHeaders.Clear();
            _http.DefaultRequestHeaders.Add("X-Forwarded-Proto", "https");
            _http.DefaultRequestHeaders.Add("pi", "ivdt");

            if (!string.IsNullOrEmpty(Çerez))
            {
                _http.DefaultRequestHeaders.TryAddWithoutValidation("Cookie", Çerez);
            }

            HttpResponseMessage cevap;
#if DEBUG
            Console.WriteLine(Metod + " " + Sayfa);
#endif
            switch (Metod)
            {
                case "POST":
                    cevap = _http.PostAsync(Sayfa, İçerik).Result;
                    break;

                case "PUT":
                    cevap = _http.PutAsync(Sayfa, İçerik).Result;
                    break;

                case "GET":
                    cevap = _http.GetAsync(Sayfa).Result;
                    break;

                case "DELETE":
                    cevap = _http.DeleteAsync(Sayfa).Result;
                    break;

                case "PATCH":
                    var request = new HttpRequestMessage(new HttpMethod("PATCH"), Sayfa)
                    {
                        Content = new StringContent(İçerik.ReadAsStringAsync().Result, Encoding.UTF8, "application/json")
                    };

                    cevap = _http.SendAsync(request).Result;
                    break;

                default:
                    throw new ArgumentException("Geçersiz HTTP metod: " + Metod);
            }

            if (cevap.Headers.TryGetValues("Bilgi", out IEnumerable<string> Bilgi_ler))
            {
                string BirArada = null;
                foreach (string biri in Bilgi_ler)
                {
                    BirArada += biri;
                }
            }

            if (Dönüş_kodu > 0)
            {
                string Bilgi = cevap.Headers.Contains("Bilgi") ? Uri.UnescapeDataString(cevap.Headers.GetValues("Bilgi").First()) : null;
                string Hata = cevap.Headers.Contains("Hata") ? Uri.UnescapeDataString(cevap.Headers.GetValues("Hata").First()) : null;

                if (Dönüş_kodu != (int)cevap.StatusCode) throw new Exception(Metod + " " + Sayfa + " Beklenen:" + Dönüş_kodu + ", Alınan:" + (int)cevap.StatusCode + "\n" + Hata + Bilgi);
            }

            return cevap;
        }
        public string Gönder_Tedavi(string Metod, string Sayfa, UçNokta.Tedavi_ Tedavi, List<string> DosyaEkleri, int Dönüş_kodu = 200)
        {
            using (var form = new MultipartFormDataContent())
            {
                string tedavi_yazı = Json.Nesneden_Yazıya(Tedavi, typeof(UçNokta.Tedavi_));
                var igContent = new StringContent(tedavi_yazı, Encoding.UTF8, "application/json");
                form.Add(igContent, "ig");

                if (DosyaEkleri != null)
                {
                    foreach (var path in DosyaEkleri)
                    {
                        var fileName = Path.GetFileName(path);
                        var mimeType = GetMimeType(fileName);

                        var fileBytes = File.ReadAllBytes(path);
                        var fileContent = new ByteArrayContent(fileBytes);
                        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(mimeType);

                        form.Add(fileContent, "de_yeni" + ArgeMup.HazirKod.Rastgele.Yazı(), fileName);
                    }
                }

                HttpResponseMessage cevap = İstekYap_Detaylı(Metod, Ortak.Sayfa_Tedavi + Sayfa, form, Dönüş_kodu);

                //Yeni iş oluşturma
                string SeriNo = cevap.Headers.Contains("SeriNo") ? cevap.Headers.GetValues("SeriNo").First() : null;
                if (Metod == "POST" && cevap.IsSuccessStatusCode) return SeriNo;

                return null;
            }
        }
        string GetMimeType(string fileName)
        {
            string mimeType = "application/octet-stream"; // Varsayılan tip
            string ext = System.IO.Path.GetExtension(fileName).ToLower();

            using (Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext))
            {
                if (regKey != null && regKey.GetValue("Content Type") != null)
                {
                    mimeType = regKey.GetValue("Content Type").ToString();
                }
            }
            return mimeType;
        }
    }
}
