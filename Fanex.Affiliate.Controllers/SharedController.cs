namespace Fanex.Affiliate.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Configurations;
    using Criteria;
    using ONEbook.UM.Filters;
    using Services;
    using Web;

    [CheckUMFilter]
    public class SharedController : BaseController
    {
        #region Services

        private readonly IHotNewsService _hotNewsService;

        private readonly ISystemSettingService _systemSettingService;

        #endregion

        #region Constructors

        public SharedController(IHotNewsService hotNewsService, ISystemSettingService systemSettingService)
        {
            _hotNewsService = hotNewsService;
            _systemSettingService = systemSettingService;
        }
        #endregion

        [ChildActionOnly]
        public PartialViewResult Language()
        {
            var languageList = AppSettings.BaseLanguages;
            var queryLang = languageList.FirstOrDefault(lang => lang.LanguageId.Contains(AppSession.Language));
            var defaultLang = languageList.FirstOrDefault(lang => lang.LanguageId.Contains(AppSettings.DefaultLanguage));

            var selectedLang = queryLang != null ? queryLang.LanguageName : defaultLang.LanguageName;
            return PartialView(Tuple.Create(languageList, selectedLang));
        }

        [HttpPost]
        public ActionResult ChangeLanguage(string lang)
        {
            AppSession.Language = lang;
            return Json(string.Empty);
        }

        [ChildActionOnly]
        public PartialViewResult Clock()
        {
            var sysDateTime = _systemSettingService.GetSystemDateTime(new SystemDateTimeCriteria());
            return PartialView(sysDateTime);
        }

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

        [ChildActionOnly]
        public ActionResult Footer()
        {
            var siteDomainInfo = GetSiteDomainInfo();
            return PartialView(siteDomainInfo);
        }
    }
}