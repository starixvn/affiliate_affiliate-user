namespace Fanex.Affiliate.Banner.Controllers
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        private const string ErrorNotFoundImagePath = "~/Content/img/error404.png";

        private static readonly string[] AllowedExtensions = new string[] { ".gif", ".jpg", ".png", ".jpeg", ".swf" };

        [HttpGet]
        public ActionResult Index(int id)
        {
            return View();
        }

        [HttpGet]
        public ActionResult Banner(string encodedFilePath)
        {
            var folderPath = ConfigurationManager.AppSettings["BannerFolder"];
            string filePath = DecodeBase64(encodedFilePath);
            var fullPath = Path.Combine(folderPath, filePath);
            if (string.IsNullOrEmpty(filePath) || !IsValidFileExtension(filePath) || !System.IO.File.Exists(fullPath))
            {
                return File(Server.MapPath(ErrorNotFoundImagePath), "image/png");
            }

            return base.File(fullPath, MimeMapping.GetMimeMapping(filePath));
        }

        private static string DecodeBase64(string text)
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

        private static bool IsValidFileExtension(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLower();
            return AllowedExtensions.Any(x => x == extension);
        }
    }
}