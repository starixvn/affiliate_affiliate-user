namespace Fanex.Affiliate.Banner.Controllers
{
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Net.Mime;
    using System.Web;
    using System.Web.Mvc;
    using SFile = System.IO;
    public class HomeController : Controller
    {
        private const string ErrorNotFoundImagePath = "~/Content/img/error404.png";

        private static readonly string[] AllowedExtensions = { ".gif", ".jpg", ".png", ".jpeg", ".swf" };

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
      //  [OutputCache(Duration = 300, VaryByParam = "encodedFilePath")]
        public ActionResult Banner(string encodedFilePath)
        {
            var folderPath = ConfigurationManager.AppSettings["BannerFolder"];
            var filePath = HttpUtility.UrlDecode(encodedFilePath);
            var fullPath = Path.Combine(folderPath, filePath);
            var fileInfo = new FileInfo(fullPath);
            if (string.IsNullOrEmpty(filePath) || !IsValidFileExtension(filePath) || !fileInfo.Exists)
            {
                return File(Server.MapPath(ErrorNotFoundImagePath), "image/png");
            }

            using (var stream = fileInfo.OpenRead())
            {
                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    return new FileContentResult(memoryStream.ToArray(), MimeMapping.GetMimeMapping(fileInfo.Name));
                }
            }

        }

        private static bool IsValidFileExtension(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLower();
            return AllowedExtensions.Any(x => x == extension);
        }
    }
}