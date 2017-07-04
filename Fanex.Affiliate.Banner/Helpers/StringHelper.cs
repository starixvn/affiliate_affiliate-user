namespace Fanex.Affiliate.Banner.Helpers
{
    using System;
    using System.Text;

    public static class StringHelper
    {
        public static string EncodeBase64(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string DecodeBase64(string text)
        {
            try
            {
                byte[] data = Convert.FromBase64String(text);
                return Encoding.UTF8.GetString(data);
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}