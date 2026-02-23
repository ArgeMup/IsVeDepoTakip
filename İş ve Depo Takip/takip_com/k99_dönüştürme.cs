using System;
using System.Text;

namespace İş_ve_Depo_Takip.takip_com
{
    public static class D_TarihSaat_UTC
    {
        private readonly static string Şablon = "yyyy-MM-ddTHH:mm:ss.fff'Z'";

        public static string Yazıya(DateTime Girdi)
        {
            return Girdi.ToString(Şablon);
        }

        public static DateTime Yazıdan(string Girdi)
        {
            return DateTime.ParseExact(
                Girdi,
                Şablon,
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.AdjustToUniversal | System.Globalization.DateTimeStyles.AssumeUniversal);
        }
    }

    public static class D_HexYazı
    {
        public static string NormalYazıya(string Girdi)
        {
            //0xAABB
            if (!Girdi.StartsWith("0x")) return null;

            byte[] girdi = FromHexString(Girdi.Substring(2));
            string çıktı = System.Text.Encoding.UTF8.GetString(girdi).Trim();

            if (string.IsNullOrWhiteSpace(çıktı)) return null;

            //okunabilir karakter kontrolü
            foreach (char biri in çıktı)
            {
                if (char.IsControl(biri)) return null;
            }

            return çıktı;
        }
        public static string NormalYazıdan(string Girdi)
        {
            var g = System.Text.Encoding.UTF8.GetBytes(Girdi);
            var d = ToHexString(g);
            return "0x" + d;
        }

        public static byte[] FromHexString(string hex)
        {
            byte[] bytes = new byte[hex.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return bytes;
        }
        public static string ToHexString(byte[] bytes)
        {
            StringBuilder hex = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
            {
                hex.Append(b.ToString("X2")); // X2: Büyük harf Hex, x2: Küçük harf Hex
            }
            return hex.ToString();
        }
    }
}
