// --------------------------------------------------------------------------
// <copyright file="HomeController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.05.28">Created</change>
// </history>

namespace Fanex.Affiliate.Controllers
{
    using System.Web.Mvc;
    using System.Xml;
    using Configurations;
    using Criteria;
    using Services.Interface;

    /// <summary>
    /// Home Controller.
    /// </summary>
    public class HomeController : BaseController
    {
        #region Services

        /// <summary>
        /// Define account service.
        /// </summary>
        private readonly IAccountService _accountService;

        #endregion Services

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController" /> class.
        /// </summary>
        /// <param name="accountService">Account Service.</param>
        public HomeController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        #endregion Constructor

        /// <summary>
        /// Action Home.
        /// </summary>
        /// <returns>Return view.</returns>
        [HttpGet]
        public ActionResult Index()
        {
            var model = _accountService.GetDefaultProductCommission(new DefaultCommissionCriteria());
            return View(model);
        }

        /// <summary>
        /// Load Affiliate Setting.
        /// </summary>
        /// <returns>Return view.</returns>
        [HttpGet]
        public ActionResult AffiliateSetting()
        {
            ViewBag.SupportedLanguages = AppSettings.AffiliateSupportedLanguages;
            ViewBag.AllLanguages = AppSettings.AllLanguages;
            return View();
        }

        /// <summary>
        /// Save Affiliate Setting.
        /// </summary>
        /// <param name="langsList">Language list.</param>
        /// <returns>Return view.</returns>
        [HttpPost]
        public string AffiliateSetting(string langsList)
        {
            try
            {
                var config = new XmlDocument();
                config.Load(Server.MapPath("~/Web.AppSettings.config"));

                var supportedLangs = config.SelectSingleNode("appSettings/add[@key='AffiliateSupportedLanguages']");
                if (supportedLangs != null)
                {
                    supportedLangs.Attributes["value"].Value = langsList;
                }

                config.Save(Server.MapPath("~/Web.AppSettings.config"));

                return "success";
            }
            catch
            {
                return "failed";
            }
        }

        /// <summary>
        /// Tests this instance.
        /// </summary>
        /// <returns>Return view.</returns>
        public ActionResult Test()
        {
            return Content("Test only");
        }
    }
}