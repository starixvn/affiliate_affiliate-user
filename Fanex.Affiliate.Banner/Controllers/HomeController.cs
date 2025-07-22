namespace Fanex.Affiliate.Banner.Controllers
{
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        private const string ErrorNotFoundImagePath = "~/Content/img/image-error404.jpg";

        private static readonly string[] AllowedExtensions = { ".gif", ".jpg", ".png", ".jpeg", ".swf" };

        [HttpGet]
        public ActionResult Index()
        {
            return View(model: ConfigurationManager.AppSettings["SystemName"]);
        }

        [HttpGet]
        [OutputCache(Duration = 300, VaryByParam = "encodedFilePath")]
        public ActionResult Banner(string encodedFilePath)
        {
            var folderPaths = ConfigurationManager.AppSettings["BannerFolders"].Split(';');
            var filePath = HttpUtility.UrlDecode(encodedFilePath);
            var fullPaths = folderPaths.Select(x => Path.Combine(x, filePath));

            foreach (var path in fullPaths)
            {
                var fileInfo = new FileInfo(path);

                if (!string.IsNullOrEmpty(filePath) && IsValidFileExtension(filePath) && fileInfo.Exists)
                {
                    using (var stream = fileInfo.OpenRead())
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            stream.CopyTo(memoryStream);
                            return new FileContentResult(memoryStream.ToArray(), MimeMapping.GetMimeMapping(fileInfo.Name));
                        }
                    }
                }
            }

            return File(Server.MapPath(ErrorNotFoundImagePath), "image/png");
        }

        private static bool IsValidFileExtension(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLower();
            return AllowedExtensions.Any(x => x == extension);
        }
    }
}