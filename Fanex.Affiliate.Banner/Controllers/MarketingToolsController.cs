// ----------------------------------------------------------------------
// <copyright file="MarketingToolsController.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/09/23">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.Banner.Controllers
{
    using System;
    using System.Configuration;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web.Mvc;
    using Criteria;
    using DTO;
    using Fanex.Affiliate.Services.Impl;
    using Fanex.Affiliate.Services.Interface;
    using Microsoft.Practices.EnterpriseLibrary.Caching;

    /// <summary>
    /// Marketing Tools controller of Affiliate Banner.
    /// </summary>
    public class MarketingToolsController : Controller
    {
        #region Variables

        private const int TEXT = 0;
        private const int IMAGE = 1;
        private const int FLASH = 2;

        private const string SWFVERSIONDETECTJS = "SwfVersDetection.js";
        private const string AFFMEDIAJS = "affiliate.media.js";
        private const string FLASHJS = "flash.js";

        private static string[] fileNames = { "text", "image", "flash1" };
        private static Regex _regex = new Regex(@"\.swf", RegexOptions.IgnoreCase);
        private static string _systemName;

        #endregion

        #region Services
        /// <summary>
        /// Define Payment service.
        /// </summary>
        private readonly IBannerService _bannerService;
        #endregion Services

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the MarketingToolsController class.
        /// </summary>
        /// <param name="bannerService">Banner Service.</param>
        public MarketingToolsController(IBannerService bannerService)
        {
            _bannerService = bannerService;
        }

        /// <summary>
        /// Initializes a new instance of the MarketingToolsController class.
        /// </summary>
        public MarketingToolsController()
        {
            _bannerService = new BannerService();
        }
        #endregion Constructor
        /// <summary>
        /// Action Error page.
        /// </summary>
        /// <returns>Action result.</returns>
        [HttpGet]
        public ActionResult Error()
        {
            return View();
        }

        /// <summary>
        /// Action page not found.
        /// </summary>
        /// <returns>Action result.</returns>
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
        public ContentResult GetScript(int id, string signuplink)
        {
            try
            {
                _systemName = _bannerService.BannerGetSystemName("Aff_Banner_GetSystemName");

                var scriptContent = ReadFile(Server.MapPath("~/Scripts/" + AFFMEDIAJS));
                scriptContent = scriptContent.Replace("{requesttime}", ConfigurationManager.AppSettings["RequestTime"]);
                scriptContent = scriptContent.Replace("{url}", Common.FullDomainName + "/MarketingTools/Refresh");
                scriptContent = scriptContent.Replace("{id}", id.ToString());
                scriptContent = scriptContent.Replace("{systemname}", _systemName);
                scriptContent = scriptContent.Replace("{afflink}", Server.UrlEncode(signuplink + "?ref="));

                return Content(scriptContent.ToString(), "text/javascript");
            }
            catch (Exception exception)
            {
                return Content(exception.Message, "text/plain");
            }
            finally
            {
                BannerUpdateStatistic(id);
            }
        }

        /// <summary>
        /// Get the script to display banner to client.
        /// </summary>
        /// <param name="id">Affiliate ID.</param>
        /// <param name="signuplink">Sign up link.</param>
        /// <returns>Content result.</returns>
        public ContentResult Refresh(int id, string signuplink)
        {
            _systemName = _bannerService.BannerGetSystemName("Aff_Banner_GetSystemName");
            var bannerInfo = GetFirstBannerRowInfo(id);
            if (bannerInfo != null)
            {
                if (!string.IsNullOrEmpty(bannerInfo.Content))
                {
                    var script = new StringBuilder();
                    var fileName = fileNames[bannerInfo.ImageType] + ".htm";

                    var bannerStrBuilder = ReadFile(Server.MapPath("~/Content/templates/" + fileName));
                    bannerStrBuilder.Replace("{systemname}", _systemName).Replace("{id}", id.ToString());
                    bannerStrBuilder = new StringBuilder(MinifyHtml(bannerStrBuilder.ToString()));
                    bannerStrBuilder.Replace("{afflink}", signuplink);

                    GenerateScript(id, signuplink, _systemName, bannerInfo, ref bannerStrBuilder, ref script);
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
                        + "var content = \"" + bannerStrBuilder.ToString() + "\";"
                        + "var r = replaceHtml('a','banner_" + _systemName + id + "',content); \n"
                        + " if(!r) {var el = document.getElementById('bannerContainer'); if(el) el.innerHTML =content;} \n" + script
                        + ";",
                        "text/javascript");
                }
            }
            
            return Content(string.Empty);
        }

        /// <summary>
        /// Generate script.
        /// </summary>
        /// <param name="bannerID">Input banner ID.</param>
        /// <param name="signupLink">Sign up Link.</param>
        /// <param name="systemName">System name.</param>
        /// <param name="bannerInfo">Banner information.</param>
        /// <param name="bannerStrBuilder">Banner string builder.</param>
        /// <param name="script">Script string builder.</param>
        public void GenerateScript(
            int bannerID,
            string signupLink,
            string systemName,
            Banner bannerInfo,
            ref StringBuilder bannerStrBuilder,
            ref StringBuilder script)
        {
            switch (bannerInfo.ImageType)
            {
                case TEXT:
                    bannerStrBuilder.Replace("{content}", bannerInfo.Content.Replace("\r\n", "<br/>"));
                    break;
                case IMAGE:
                    bannerStrBuilder.Replace(
                    "{src}",
                    string.Format("{0}/Content/Banners/{1}?v={2}", Common.FullDomainName, Server.HtmlDecode(bannerInfo.Content), bannerInfo.Version));
                    break;
                default:
                    // FLASH
                    var sizes = bannerInfo.Size.ToString().Split('x');
                    var bannerURL = string.Format(
                        "{0}/Content/Banners/{1}?v={2}",
                        Common.FullDomainName,
                        Server.HtmlDecode(bannerInfo.Content),
                        bannerInfo.Version);
                    bannerStrBuilder.Replace("{src}", bannerURL);
                    bannerStrBuilder.Replace("{width}", sizes[0]); // Width
                    bannerStrBuilder.Replace("{height}", sizes[1]); // Height

                    var file = ReadFile(Server.MapPath("~/Scripts/" + SWFVERSIONDETECTJS));
                    script = ReadFile(Server.MapPath("~/Content/templates/" + FLASHJS));

                    script.Replace("{width}", sizes[0]); // Width
                    script.Replace("{height}", sizes[1]); // Height
                    script.Replace("{afflink}", signupLink);
                    script.Replace("{srcnoswf}", _regex.Replace(bannerURL, string.Empty));
                    script.Replace("{systemname}", systemName);
                    script.Replace("{id}", bannerID.ToString());
                    script.Insert(0, file);
                    break;
            }
        }

        /// <summary>
        /// Simplify HTML string.
        /// </summary>
        /// <param name="html">Original HTML string.</param>
        /// <returns>Simplified HTML string.</returns>
        public string MinifyHtml(string html)
        {
            html = Regex.Replace(html, @"\n|\t", " ");
            html = Regex.Replace(html, @">\s+<", "><").Trim();
            html = Regex.Replace(html, @"\s{2,}", " ");

            return html;
        }

        /// <summary>
        /// Store banner statistic into database.
        /// </summary>
        /// <param name="bannerID">Input Banner ID.</param>
        public void BannerUpdateStatistic(int bannerID)
        {
            var criteria = new BannerUpdateStatisticCriteria
            {
                BannerID = bannerID
            };

            _bannerService.BannerUpdateStatistic(criteria);
        }

        /// <summary>
        /// Get first row of banner information.
        /// </summary>
        /// <param name="bannerID">Input banner ID.</param>
        /// <returns>Banner data object.</returns>
        public Banner GetFirstBannerRowInfo(int bannerID)
        {
            var criteria = new BannerInfoCriteria
            {
                BannerID = bannerID
            };

            return _bannerService.BannerSelectInfo(criteria);
        }

        /// <summary>
        /// Read script file.
        /// </summary>
        /// <param name="fileScript">Script file name.</param>
        /// <returns>Script's content.</returns>
        public StringBuilder ReadFile(string fileScript)
        {
            // Script existed in cache.
            if (CacheFactory.GetCacheManager().GetData(fileScript) != null)
            {
                return new StringBuilder(CacheFactory.GetCacheManager().GetData(fileScript).ToString());
            }

            // Read script from file.
            var scriptContent = new StringBuilder();
            using (var fileReader = System.IO.File.OpenText(fileScript))
            {
                scriptContent.Append(fileReader.ReadToEnd());
                fileReader.Close();
            }

            CacheFactory.GetCacheManager().Add(fileScript, scriptContent.ToString());
            return scriptContent;
        }
    }
}
