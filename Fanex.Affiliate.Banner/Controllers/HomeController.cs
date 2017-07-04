namespace Fanex.Affiliate.Banner.Controllers
{
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Helpers;

    public class HomeController : Controller
    {
        private const string ErrorNotFoundImagePath = "~/Content/img/error404.png";

        private static readonly string[] AllowedExtensions = new string[] { ".gif", ".jpg", ".png", ".jpeg", ".swf" };

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Banner(string encodedFilePath)
        {
            var folderPath = ConfigurationManager.AppSettings["BannerFolder"];
            string filePath = StringHelper.DecodeBase64(encodedFilePath);
            var fullPath = Path.Combine(folderPath, filePath);
            if (string.IsNullOrEmpty(filePath) || !IsValidFileExtension(filePath) || !System.IO.File.Exists(fullPath))
            {
                return File(Server.MapPath(ErrorNotFoundImagePath), "image/png");
            }

            return base.File(fullPath, MimeMapping.GetMimeMapping(filePath));
        }

        private static bool IsValidFileExtension(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLower();
            return AllowedExtensions.Any(x => x == extension);
        }
    }
}