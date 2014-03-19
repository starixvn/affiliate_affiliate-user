// --------------------------------------------------------------------------
// <copyright file="HeaderController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.06.17">Created</change>
// </history>

namespace Fanex.Affiliate.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Configurations;
    using Criteria;
    using Services.Interface;
    using Web;

    /// <summary>
    /// Header Controller.
    /// </summary>
    public class HeaderController : BaseController
    {
        #region Services
        /// <summary>
        /// Define hot news service.
        /// </summary>
        private readonly IHotNewsService _hotNewsService;

        /// <summary>
        /// Define system setting service.
        /// </summary>
        private readonly ISystemSettingService _systemSettingService;

        #endregion

        #region Ctors

        public HeaderController(IHotNewsService hotNewsService, ISystemSettingService systemSettingService)
        {
            _hotNewsService = hotNewsService;
            _systemSettingService = systemSettingService;
        }
        #endregion

        /// <summary>
        /// Load language list to select box.
        /// </summary>
        /// <returns>Return partial view.</returns>
        [ChildActionOnly]
        public PartialViewResult Language()
        {
            var languageList = AppSettings.BaseLanguages;
            var queryLang = languageList.FirstOrDefault(lang => lang.LanguageId.Contains(AppSession.Language));
            var defaultLang = languageList.FirstOrDefault(lang => lang.LanguageId.Contains(AppSettings.DefaultLanguage));

            var selectedLang = queryLang != null ? queryLang.LanguageName : defaultLang.LanguageName;
            return PartialView(Tuple.Create(languageList, selectedLang));
        }

        /// <summary>
        /// Change language.
        /// </summary>
        /// <param name="lang">Language parameter.</param>
        /// <returns>Return view.</returns>
        [HttpPost]
        public ActionResult ChangeLanguage(string lang)
        {
            AppSession.Language = lang;
            return Json(string.Empty);
        }

        /// <summary>
        /// Load system date time.
        /// </summary>
        /// <returns>Return partial view.</returns>
        [ChildActionOnly]
        public PartialViewResult Clock()
        {
            var sysDateTime = _systemSettingService.GetSystemDateTime(new SystemDateTimeCriteria());
            return PartialView(sysDateTime);
        }

        /// <summary>
        /// Generate hot news view.
        /// </summary>
        /// <returns>Return partial view.</returns>
        [ChildActionOnly]
        public PartialViewResult HotNews()
        {
            var criteria = new HotNewsCriteria
            {
                Language = AppSession.Language
            };

            var model = _hotNewsService.GetHotNews(criteria);

            return PartialView("HotNews", model);
        }
    }
}