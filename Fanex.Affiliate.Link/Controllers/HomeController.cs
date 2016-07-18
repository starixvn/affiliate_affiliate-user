namespace Fanex.Affiliate.Link.Controllers
{
    using System;
    using System.Globalization;
    using System.Web.Mvc;
    using Criteria;
    using Services;
    using Utilities;

    /// <summary>
    /// Home controller for affiliate link.
    /// </summary>
    public class HomeController : Controller
    {
        #region Const

        #endregion Const

        #region Services
        
        private readonly ITrackingSystemService _trackingSystemService;
        
        #endregion Services

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the HomeController class.
        /// </summary>
        public HomeController()
        {
            _trackingSystemService = new TrackingSystemService();
        }

        #endregion Constructor

        /// <summary>
        /// Main controller of this class.
        /// </summary>
        /// <returns>Action result.</returns>
        public ActionResult Default()
        {
            return View();
        }

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
        /// Handler when user click on affiliate's link.
        /// </summary>
        /// <param name="affId">Affiliate ID.</param>
        /// <param name="pageId">Page identification.</param>
        /// <param name="lang">Language ID.</param>
        /// <returns>Action result.</returns>
        public ActionResult Link(int affId, string pageId, string lang)
        {
            if (Request.Url != null)
            {
                var affDomain = Request.Url.ToString();
                var urlReferrer = Request.UrlReferrer != null ? Request.UrlReferrer.AbsoluteUri : null;
                var domainReferrer = Request.UrlReferrer != null ? Request.UrlReferrer.Host : null;

                if (string.IsNullOrEmpty(urlReferrer) && !string.IsNullOrEmpty(Request["ref"]))
                {
                    urlReferrer = Request["ref"];
                    var referrerUri = new Uri(urlReferrer);
                    domainReferrer = string.IsNullOrEmpty(referrerUri.Host) ? null : referrerUri.Host;
                }

                var criteria = new TrackingSystemCriteria
                {
                    AffDomain = affDomain,
                    AffiliateID = affId,
                    PageCode = pageId,
                    LanguageCode = lang,
                    IP = Common.GetIPAddressOfClient(),
                    Browser = Request.Browser.Browser,
                    URLReferrer = urlReferrer,
                    DomainReferrer = domainReferrer,
                    OS = Common.GetOsName(Request.UserAgent),
                    Device = Common.GetDeviceName(Request.UserAgent),
                    BrowserVersion = Request.Browser.Version,
                    BrowserResolution = Request.Browser.ScreenPixelsWidth + "x" + Request.Browser.ScreenPixelsHeight,
                    BrowserLanguage = string.IsNullOrEmpty(Request.ServerVariables["HTTP_ACCEPT_LANGUAGE"])
                        ? string.Empty
                        : Request.ServerVariables["HTTP_ACCEPT_LANGUAGE"],
                    UserAgent = Request.UserAgent,
                    Country = Common.ResolveCountry() != null
                        ? Common.ResolveCountry().EnglishName
                        : CultureInfo.CurrentCulture.ToString()
                };

                var redirectUrl = _trackingSystemService.InsertStatistic(criteria);
                if (!string.IsNullOrEmpty(redirectUrl))
                {
                    return Redirect(redirectUrl);
                }
                else
                {
                    ViewBag.Result = "This affiliate does not exists. Please contact administrator!";
                }
            }

            return View();
        }
    }
}
