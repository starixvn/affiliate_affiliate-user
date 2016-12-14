namespace Fanex.Affiliate.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Configurations;
    using Criteria;
    using DTO;
    using ViewModels;
    using Web.Pager;

    /// <summary>
    ///     All action for Referrals and Landing page.
    /// </summary>
    public partial class AccountController
    {
        #region Traffic Statistics - Clicks Landing Pages

        /// <summary>
        ///     Traffic statistics landing pages.
        /// </summary>
        /// <returns>Return view.</returns>
        [HttpGet]
        public ActionResult LandingPages()
        {
            var destinateDate = DateTime.Now.Date;
            var startDate = destinateDate.AddMonths(-1);
            var criteria = new LandingPagesByPageCriteria
            {
                AffId = CurrentSession.AffId,
                FromDate = startDate,
                ToDate = destinateDate,
            };

            return LandingPages(criteria);
        }

        /// <summary>
        ///     Traffic statistics landing pages.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Return view.</returns>
        [HttpPost]
        [ValidateInput(true)]
        public ActionResult LandingPages(LandingPagesByPageCriteria criteria)
        {
            var pageList = SystemSettingService.ListSystemSetting(new SystemSettingsByTypeCriteria
            {
                Type = "CustomerPage"
            });

            criteria.AffId = CurrentSession.AffId;
            if (!ModelState.IsValid)
            {
                var destinateDate = DateTime.Now.Date;
                var startDate = destinateDate.AddMonths(-1);
                criteria = new LandingPagesByPageCriteria
                {
                    AffId = CurrentSession.AffId,
                    FromDate = startDate,
                    ToDate = destinateDate,
                };
            }

            var data = criteria.IsValid() ? AccountService.GetLandingPagesStatistics(criteria) : new List<TrafficStatistic>();
            var viewModel = new TrafficStatisticsViewModel
            {
                PageSelected = "All",
                PageList = pageList as List<SystemSetting>,
                LanguageList = AppSettings.BaseLanguages.ToList(),
                FromDate = criteria.FromDate,
                ToDate = criteria.ToDate,
                LandingPagesStatisticsList = data
            };

            return View(viewModel);
        }

        /// <summary>
        ///     Traffic statistics landing pages detail.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>Return view.</returns>
        [HttpGet]
        public ActionResult LandingPagesDetail(LandingPagesBySiteCriteria criteria)
        {
            var itemPerPage = criteria.PageSize > 0 ? criteria.PageSize : AppSettings.PageSize;
            var currentPageIndex = criteria.Page > 0 ? criteria.Page : 1;
            var totalPage = 0;

            criteria.AffId = CurrentSession.AffId;
            criteria.PageSize = itemPerPage;
            criteria.Page = currentPageIndex;
            var data = AccountService.GetLandingPagesStatisticsDetail(criteria, ref totalPage);

            return View(data.ToPagedList(currentPageIndex, itemPerPage, totalPage * itemPerPage));
        }

        #endregion Traffic Statistics - Clicks Landing Pages

        #region Traffic Statistics - Referrers Pages

        /// <summary>
        ///     Statistics Referrers Pages.
        /// </summary>
        /// <returns>Return view.</returns>
        [HttpGet]
        public ActionResult ReferrersStatistics()
        {
            var criteria = new TrafficStatisticCriteria
            {
                FromDate = DateTime.Now.AddMonths(-1),
                ToDate = DateTime.Now
            };

            return ReferrersStatistics(criteria);
        }

        /// <summary>
        ///     Statistics Referrers Pages.
        /// </summary>
        /// <param name="criteria">Traffic statistic criteria.</param>
        /// <returns>Return view.</returns>
        [HttpPost]
        public ActionResult ReferrersStatistics(TrafficStatisticCriteria criteria)
        {
            if (!ModelState.IsValid)
            {
                var destinateDate = DateTime.Now.Date;
                var startDate = destinateDate.AddMonths(-1);
                criteria = new TrafficStatisticCriteria
                {
                    FromDate = startDate,
                    ToDate = destinateDate,
                };
            }

            criteria.AffId = CurrentSession.AffId;
            var viewModel = new TrafficStatisticsViewModel
            {
                ReferrersStatisticsList = criteria.IsValid() ? AccountService.GetReferrersStatistics(criteria) : new List<TrafficStatistic>(),
                FromDate = criteria.FromDate,
                ToDate = criteria.ToDate
            };

            return View(viewModel);
        }

        /// <summary>
        ///     Traffic statistics landing pages detail.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>Return view.</returns>
        [HttpGet]
        public ActionResult ReferrersStatisticsDetail(TrafficStatisticDetailCriteria criteria)
        {
            var itemPerPage = criteria.PageSize > 0 ? criteria.PageSize : AppSettings.PageSize;
            var currentPageIndex = criteria.Page > 0 ? criteria.Page : 1;
            var totalPage = 0;

            criteria.AffId = CurrentSession.AffId;
            criteria.PageSize = itemPerPage;
            criteria.Page = currentPageIndex;
            var data = AccountService.GetReferrersStatisticsDetail(criteria, ref totalPage);

            return View(data.ToPagedList(currentPageIndex, itemPerPage, totalPage * itemPerPage));
        }

        #endregion Traffic Statistics - Referrers Pages
    }
}