namespace Fanex.Affiliate.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Resources;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;
    using Fanex.Affiliate.Constants;
    using Fanex.Affiliate.DTO;
    using Fanex.Affiliate.Helpers;
    using Fanex.MailDeliver.MailClient;
    using Fanex.MailDeliver.MailClient.API;
    using Fanex.MailDeliver.Model;
    using Resources;

    /// <summary>
    /// Common class.
    /// </summary>
    public static class Common
    {
        private const string IPv4Regex = "^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$";

        /// <summary>
        /// The lang resource manager.
        /// </summary>
        private static readonly ResourceManager LangResourceManager = new ResourceManager(typeof(Lang));

        /// <summary>
        /// Contains all operating system mapping.
        /// </summary>
        private static IDictionary<string, string> _operatingSystemMapping = new Dictionary<string, string> 
        { 
                { "Windows NT 6.1", "Windows 7" },
                { "Windows NT 6.0", "Windows Vista" }, 
                { "Windows NT 5.1", "Windows XP" }, 
                { "Windows NT 5.0", "Windows 2000" }, 
                { "Windows NT 4.1", "Windows 98" }, 
                { "iPhone", "iOS" }, 
                { "iPad", "iOS" }, 
                { "iPod", "iOS" }, 
                { "Mac OS", "Mac OS" }, 
                { "BlackBerry", "BlackBerry" }, 
                { "Android", "Android" }, 
                { "Linux", "Linux" }, 
                { "Ubuntu", "Ubuntu" }
        };

        /// <summary>
        /// Contains all device mapping.
        /// </summary>
        private static IDictionary<string, string> _deviceMapping = new Dictionary<string, string> 
        { 
                { "iPhone", "iPhone" }, 
                { "iPad", "iPad" }, 
                { "iPod", "iPod" },  
                { "BlackBerry", "BlackBerry" }, 
                { "HTC", "HTC" }, 
                { "Motorola", "Motorola" }, 
                { "Nokia", "Nokia" }
        };

        /// <summary>
        /// Send email with detail content.
        /// </summary>
        /// <param name="message">Content message.</param>
        /// <param name="mailTo">Mail To parameter.</param>
        /// <param name="subject">Subject parameter.</param>
        /// <param name="mailBcc">Mail BCC parameter.</param>
        public static void SendEmail(string message, string mailTo, string subject, string mailBcc = "")
        {
            if (!Configurations.AppSettings.IsApiDataModifiable)
            {
                LogHelper.WriteInfo($"The Aff User doesn't send the mail to {mailTo}: {subject}");
                return;
            }

            var mailItem = new MailRecord
            {
                MailTo = mailTo,
                Subject = subject,
                Title = subject,
                Body = message,
                MailBcc = mailBcc
            };

            AppSettings.AppMailId = Configurations.AppSettings.AppMailId;
            AppSettings.AppMailAddress = Configurations.AppSettings.AppMailAddress;

            using (var client = new MailDeliver())
            {
                client.SendMail(mailItem);
            }
        }

        /// <summary>
        /// Get language name from language code.
        /// </summary>
        /// <param name="strLangCode">Language code.</param>
        /// <returns>Return original language.</returns>
        public static string GetLanguageOriginal(string strLangCode)
        {
            switch (strLangCode.ToLower())
            {
                case "en-us": return LangMap.EnUS;
                case "eneuro": return LangMap.EnEuro;
                case "zh-tw": return LangMap.ZhTW;
                case "zh-cn": return LangMap.ZhCN;
                case "th-th": return LangMap.ThTH;
                case "es-es": return LangMap.EsES;
                case "vi-vn":
                case "vi": return LangMap.ViVN;
                case "id-id": return LangMap.IdID;
                default:
                    return "en";
            }
        }

        /// <summary>
        /// Get error message from Lang resource by error code.
        /// </summary>
        /// <param name="errCode">Parameter error code.</param>
        /// <returns>Return Error's Message.</returns>
        public static string GetErrorMsg(int errCode)
        {
            var errMsg = LangResourceManager.GetString(string.Format("Msg{0}", errCode));
            return string.IsNullOrEmpty(errMsg) ? string.Empty : errMsg;
        }

        /// <summary>
        /// Convert to date.
        /// </summary>
        /// <param name="dtmTime">Input date time.</param>
        /// <param name="slash">Input slash.</param>
        /// <param name="checkFullDate">Input is full date or not.</param>
        /// <param name="hasTime">Input has time or not.</param>
        /// <returns>Return converted date time.</returns>
        public static string ConvertDateTime(object dtmTime, string slash, bool checkFullDate, bool hasTime)
        {
            return Utility.ConvertDateTime(dtmTime, slash, checkFullDate, hasTime);
        }

        /// <summary>
        /// Generate Image.
        /// </summary>
        /// <param name="width">Input width.</param>
        /// <param name="height">Input height.</param>
        /// <returns>Return image.</returns>
        public static Bitmap GenerateNoise(int width, int height)
        {
            var finalBmp = new Bitmap(width, height);

            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    finalBmp.SetPixel(x, y, Color.Gray);
                }
            }

            return finalBmp;
        }

        /// <summary>
        /// Get Country.
        /// </summary>
        /// <returns>Country English Name.</returns>
        public static string GetCountry()
        {
            var regionInfo = ResolveCountry();

            return regionInfo != null ? regionInfo.EnglishName : CultureInfo.CurrentCulture.ToString();
        }

        /// <summary>
        /// Get region info.
        /// </summary>
        /// <returns>Region info.</returns>
        private static RegionInfo ResolveCountry()
        {
            var culture = ResolveCulture();

            //solving a culture issue
            if (culture != null && culture.LCID != 0x7F)
            {
                return new RegionInfo(culture.Name);
            }
            return null;
        }

        /// <summary>
        /// Get culture info.
        /// </summary>
        /// <returns>Culture info.</returns>
        private static CultureInfo ResolveCulture()
        {
            var languages = HttpContext.Current.Request.UserLanguages;
            
            if (languages == null || languages.Length == 0)
            {
                return null;
            }

            try
            {
                var language = languages[0].ToLowerInvariant().Trim();
                return CultureInfo.CreateSpecificCulture(language);
            }
            catch (ArgumentException)
            {
                return null;
            }
        }

        /// <summary>
        /// Get IP address from client request.
        /// </summary>
        /// <returns>String of IP address.</returns>
        public static string GetIPAddressOfClient()
        {
            var request = HttpContext.Current.Request;
            var xActualIP = request.ServerVariables[HeaderKey.HTTP_X_ACTUAL_IP];
            var xForwardedFor = !string.IsNullOrWhiteSpace(xActualIP) ? xActualIP : request.ServerVariables[HeaderKey.HTTP_X_FORWARDED_FOR];

            var clientValidIP = GetValidIPAddress(xForwardedFor);

            if (string.IsNullOrWhiteSpace(clientValidIP))
            {
                clientValidIP = request.UserHostAddress;
            }

            return clientValidIP.Equals("::1") ? "127.0.0.1" : clientValidIP;
        }

        private static string GetValidIPAddress(string ips)
        {
            if (string.IsNullOrEmpty(ips))
            {
                return null;
            }

            var ipList = ips.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(i => i.Trim())
                        .Where(i => Regex.IsMatch(i, IPv4Regex, RegexOptions.None, TimeSpan.FromSeconds(2)));

            return ipList.FirstOrDefault();
        }

        public static string GetDomainReferrer(string urlReferrer)
        {
            if (Uri.IsWellFormedUriString(urlReferrer, UriKind.Absolute))
            {
                var referrerUri = new Uri(urlReferrer);
                return string.IsNullOrEmpty(referrerUri.Host) ? null : referrerUri.Host;
            }

            return null;
        }

        /// <summary>
        /// Get device info.
        /// </summary>
        /// <param name="userAgent">User's info.</param>
        /// <returns>Device info.</returns>
        public static string GetDeviceName(string userAgent)
        {
            if (string.IsNullOrEmpty(userAgent))
            {
                return @"Computer/Laptop";
            }

            foreach (var key in _deviceMapping.Keys.Where(key => userAgent.Contains(key)))
            {
                return _deviceMapping[key];
            }

            return @"Computer/Laptop";
        }

        /// <summary>
        /// Get OS info.
        /// </summary>
        /// <param name="userAgent">User's info.</param>
        /// <returns>Operating system info.</returns>
        public static string GetOsName(string userAgent)
        {
            if (string.IsNullOrEmpty(userAgent))
            {
                return string.Empty;
            }

            foreach (var key in _operatingSystemMapping.Keys.Where(key => userAgent.Contains(key)))
            {
                return _operatingSystemMapping[key];
            }

            return string.Empty;
        }

        /// <summary>
        /// Encrypt a string using SHA256.
        /// </summary>
        /// <param name="inputString">Input string.</param>
        /// <returns>Encrypted string.</returns>
        public static string EncryptUsingSHA256(string inputString)
        {
            var hashAlgorithm = new SHA256Managed();
            var hashData = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));

            return BitConverter.ToString(hashData).Replace("-", string.Empty).ToLower();
        }

        public static T ByteArrayToObject<T>(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            var structuredObject = (T)binForm.Deserialize(memStream);

            return structuredObject;
        }

        public static List<T> ByteArrayToListObjects<T>(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            var listStructuredObjects = (List<T>)binForm.Deserialize(memStream);

            return listStructuredObjects;
        }
    }
}