using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace İş_ve_Depo_Takip.takip_com
{
    public static class Json
    {
        class Dönüştürücü_TarihSaat_ : JsonConverter<DateTime>
        {
            public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                return D_TarihSaat_UTC.Yazıdan(reader.GetString());
            }

            public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(D_TarihSaat_UTC.Yazıya(value));
            }
        }
        static JsonSerializerOptions Seçenekler;
        public static void Başlat(System.Text.Json.Serialization.Metadata.IJsonTypeInfoResolver Çözücü)
        {
            Seçenekler = new JsonSerializerOptions{ };
            Seçenekler.Converters.Add(new Dönüştürücü_TarihSaat_());
        }

        public static string Nesneden_Yazıya(object Nesne, Type JsonlaşmaTipi)
        {
            return JsonSerializer.Serialize(Nesne, JsonlaşmaTipi, Seçenekler);
        }
        public static object Yazıdan_Nesneye(string Yazı, Type JsonlaşmaTipi)
        {
            return JsonSerializer.Deserialize(Yazı, JsonlaşmaTipi, Seçenekler);
        }
        
        public static T BağımsızKopya<T>(T Girdi)
        {
            string yazı = Nesneden_Yazıya(Girdi, typeof(T));
            object nesne = Yazıdan_Nesneye(yazı, typeof(T));
            return (T)nesne;
        }
    }
}
