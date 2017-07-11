namespace Fanex.Affiliate.Banner.Controllers
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Runtime.Caching;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web.Mvc;
    using Criteria;
    using DTO;
    using Services;

    public class MarketingToolsController : Controller
    {
        private const int TEXT = 0;
        private const int IMAGE = 1;

        private const string SWFVERSIONDETECTJS = "SwfVersDetection.js";

        private const string AFFMEDIAJS = "affiliate.media.js";

        private const string FLASHJS = "flash.js";

        private static readonly string[] TemplateFileNames = { "text", "image", "flash" };

        private static readonly Regex SwfExtensionRegex = new Regex(@"\.swf", RegexOptions.IgnoreCase);

        private readonly IBannerService _bannerService;

        public MarketingToolsController() : this(null)
        {
        }

        public MarketingToolsController(IBannerService bannerService)
        {
            _bannerService = bannerService ?? new BannerService();
        }

        private string SystemName
        {
            get
            {
                return ConfigurationManager.AppSettings["SystemName"];
            }
        }

        [HttpGet]
        public ActionResult Error()
        {
            return View();
        }

        [HttpGet]
        public ActionResult PageNotFound()
        {
            return View();
        }

        /// <summary>
        /// Get the script to display banner to client.
        /// </summary>
        /// <param name="id">Affiliate ID.</param>
        /// <param name="signuplink">Sign up link.</param>
        /// <returns>Content result.</returns>
        [HttpGet]
        public ContentResult GetScript(int id, string signuplink)
        {
            try
            {
                var scriptContent = ReadFile(Server.MapPath("~/Scripts/" + AFFMEDIAJS));
                scriptContent = scriptContent.Replace("{requesttime}", ConfigurationManager.AppSettings["RequestTime"]);
                scriptContent = scriptContent.Replace("{url}", Common.FullDomainName + "/MarketingTools/Refresh");
                scriptContent = scriptContent.Replace("{id}", id.ToString());
                scriptContent = scriptContent.Replace("{systemname}", SystemName);
                scriptContent = scriptContent.Replace("{afflink}", Server.UrlEncode(signuplink + "?ref="));

                return Content(scriptContent.ToString(), "text/javascript");
            }
            catch (IOException exception)
            {
                return Content(exception.Message, "text/plain");
            }
            finally
            {
                UpdateBannerStatistic(id);
            }
        }

        /// <summary>
        /// Get the script to display banner to client.
        /// </summary>
        /// <param name="id">Affiliate ID.</param>
        /// <param name="signuplink">Sign up link.</param>
        /// <returns>Content result.</returns>
        [HttpGet]
        public ContentResult Refresh(int id, string signuplink)
        {
            var bannerInfo = GetFirstBannerRowInfo(id);
            if (bannerInfo == null || string.IsNullOrEmpty(bannerInfo.Content))
            {
                return Content(string.Empty);
            }

            var fileName = TemplateFileNames[bannerInfo.ImageType] + ".htm";

            bannerInfo.BannerStringBuilder = ReadFile(Server.MapPath("~/Content/templates/" + fileName));
            bannerInfo.BannerStringBuilder.Replace("{systemname}", SystemName).Replace("{id}", id.ToString());
            bannerInfo.BannerStringBuilder = new StringBuilder(MinifyHtml(bannerInfo.BannerStringBuilder.ToString()));
            bannerInfo.BannerStringBuilder.Replace("{afflink}", signuplink);

            var script = GenerateScript(id, signuplink, SystemName, bannerInfo);
            return Content(
                @"function hasClass(element, cls) {
                                return (' ' + element.className + ' ').indexOf(' ' + cls + ' ') > -1;
                            };
                            function replaceHtml(tag,classname,html) {
                          var els = document.getElementsByTagName(tag), i, flag = false;
                          for (i = 0; i < els.length; i++){
                            if (hasClass(els[i],classname)){ if(!flag) flag = true; els[i].innerHTML = html;}}
                          return flag;
                        };"
                + "var content = \"" + bannerInfo.BannerStringBuilder.ToString() + "\";"
                + "var r = replaceHtml('a','banner_" + SystemName + id + "',content); \n"
                + " if(!r) {var el = document.getElementById('bannerContainer'); if(el) el.innerHTML =content;} \n" + script
                + ";",
                "text/javascript");
        }

        private static string MinifyHtml(string html)
        {
            html = Regex.Replace(html, @"\n|\t", " ");
            html = Regex.Replace(html, @">\s+<", "><").Trim();
            html = Regex.Replace(html, @"\s{2,}", " ");

            return html;
        }

        private static StringBuilder ReadFile(string fileScript)
        {
            var cache = MemoryCache.Default;
            var fileContent = cache.Get(fileScript);
            if (fileContent != null)
            {
                return new StringBuilder(fileContent.ToString());
            }

            var scriptContent = new StringBuilder();
            scriptContent.Append(System.IO.File.ReadAllText(fileScript));

            cache.Add(fileScript, scriptContent.ToString(), DateTimeOffset.Now.AddMinutes(30));
            return scriptContent;
        }

        private StringBuilder GenerateScript(
            int bannerId,
            string signupLink,
            string systemName,
            Banner bannerInfo)
        {
            var script = new StringBuilder();
            switch (bannerInfo.ImageType)
            {
                case TEXT:
                    bannerInfo.BannerStringBuilder.Replace("{content}", bannerInfo.Content.Replace("\r\n", "<br/>"));
                    break;

                case IMAGE:
                    bannerInfo.BannerStringBuilder.Replace(
                    "{src}",
                    string.Format("{0}/Home/Banner/{1}?v={2}", Common.FullDomainName, Uri.EscapeUriString(bannerInfo.Content), bannerInfo.Version));
                    break;

                default:
                    // FLASH
                    var sizes = bannerInfo.Size.ToString().Split('x');
                    var bannerURL = string.Format(
                        "{0}/Home/Banner/{1}?v={2}",
                        Common.FullDomainName,
                        Uri.EscapeUriString(bannerInfo.Content),
                        bannerInfo.Version);
                    bannerInfo.BannerStringBuilder.Replace("{src}", bannerURL);
                    bannerInfo.BannerStringBuilder.Replace("{width}", sizes[0]);
                    bannerInfo.BannerStringBuilder.Replace("{height}", sizes[1]);

                    var file = ReadFile(Server.MapPath("~/Scripts/" + SWFVERSIONDETECTJS));
                    script = ReadFile(Server.MapPath("~/Content/templates/" + FLASHJS));
                    script.Replace("{width}", sizes[0]);
                    script.Replace("{height}", sizes[1]);
                    script.Replace("{afflink}", signupLink);
                    script.Replace("{srcnoswf}", SwfExtensionRegex.Replace(bannerURL, string.Empty));
                    script.Replace("{systemname}", systemName);
                    script.Replace("{id}", bannerId.ToString());
                    script.Insert(0, file);
                    break;
            }

            return script;
        }

        private void UpdateBannerStatistic(int bannerId)
        {
            var criteria = new BannerUpdateStatisticCriteria
            {
                BannerID = bannerId
            };

            _bannerService.BannerUpdateStatistic(criteria);
        }

        private Banner GetFirstBannerRowInfo(int bannerId)
        {
            var criteria = new BannerInfoCriteria
            {
                BannerID = bannerId
            };

            return _bannerService.BannerSelectInfo(criteria);
        }
    }
}