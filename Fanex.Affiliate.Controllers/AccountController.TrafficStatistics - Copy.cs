// --------------------------------------------------------------------------
// <copyright file="AccountController.TrafficStatistics.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.06.24">Created</change>
//   <change who="Kaden.Trinh" date="2013.06.12">Implement action: Statement history</change>
//   <change who="Kaden.Trinh" date="2013.06.12">Implement action: Open Popup Daily Earning on Statement page</change>
//   <change who="Jeff.Nguyen" date="2013.06.26">Referrers Pages</change>
//   <change who="Leo.Vo" date="2013.06.29">Landing Pages Statistics</change>
// </history>

namespace Fanex.Affiliate.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Configurations;
    using Criterias;
    using DTO;
    using ViewModels;
    using Web.Pager;

    /// <summary>
    ///     All action for Referrals and Landing page.
    /// </summary>
    public partial class AccountController
    {
        #region Statistics Clicks Landing Pages

        /// <summary>
        ///     Traffic statistics landing pages.
        /// </summary>
        /// <returns>Return view.</returns>
        [HttpGet]
        public ActionResult LandingPages()
        {
            var destinateDate = DateTime.Now.Date;
            var startDate = destinateDate.AddMonths(-1);
            var criteria = new ClicksLandingPagesCriteria
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
        public ActionResult LandingPages(ClicksLandingPagesCriteria criteria)
        {
            var pageList = SystemSettingService.ListSystemSetting(new SystemSettingCriteria
            {
                IsType = true,
                Type = "CustomerPage"
            });

            criteria.AffId = CurrentSession.AffId;

            if (!ModelState.IsValid)
            {
                var destinateDate = DateTime.Now.Date;
                var startDate = destinateDate.AddMonths(-1);
                criteria = new ClicksLandingPagesCriteria
                {
                    AffId = CurrentSession.AffId,
                    FromDate = startDate,
                    ToDate = destinateDate,
                };
            }

            var data = criteria.IsValid() ? AccountService.GetLandingPagesStatistics(criteria) : new List<TrafficStatistic>();
            var model = new TrafficStatisticsViewModel
            {
                PageSelected = "All",
                PageList = pageList as List<SystemSetting>,
                LanguageList = AppSettings.BaseLanguages.ToList(),
                FromDate = criteria.FromDate,
                ToDate = criteria.ToDate,
                LandingPagesStatisticsList = data
            };

            return View(model);
        }

        /// <summary>
        ///     Traffic statistics landing pages detail.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>Return view.</returns>
        [HttpGet]
        public ActionResult LandingPagesDetail(ClicksLandingPagesCriteria criteria)
        {
            var itemPerPage = criteria.PageSize > 0 ? criteria.PageSize : AppSettings.PageSize;
            var currentPageIndex = criteria.Page > 0 ? criteria.Page : 1;
            var totalPage = 0;

            criteria.AffId = CurrentSession.AffId;
            criteria.PageSize = itemPerPage;
            criteria.Page = currentPageIndex;
            criteria.IsDetail = true;
            var data = AccountService.GetLandingPagesStatisticsDetail(criteria, ref totalPage);

            return View(data.ToPagedList(currentPageIndex, itemPerPage, totalPage * itemPerPage));
        }

        #endregion Statistics Clicks Landing Pages

        #region Statistics Referrers Pages

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
                    AffId = CurrentSession.AffId,
                    FromDate = startDate,
                    ToDate = destinateDate,
                };
            }

            criteria.IsReferrersStatistics = true;
            criteria.AffId = CurrentSession.AffId;
            var model = new TrafficStatisticsViewModel
            {
                ReferrersStatisticsList = criteria.IsValid() ? AccountService.GetReferrersStatistics(criteria) : new List<TrafficStatistic>(),
                FromDate = criteria.FromDate,
                ToDate = criteria.ToDate
            };

            return View(model);
        }

        /// <summary>
        ///     Traffic statistics landing pages detail.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>Return view.</returns>
        [HttpGet]
        public ActionResult ReferrersStatisticsDetail(TrafficStatisticCriteria criteria)
        {
            var itemPerPage = criteria.PageSize > 0 ? criteria.PageSize : AppSettings.PageSize;
            var currentPageIndex = criteria.Page > 0 ? criteria.Page : 1;
            var totalPage = 0;

            criteria.AffId = CurrentSession.AffId;
            criteria.PageSize = itemPerPage;
            criteria.Page = currentPageIndex;
            criteria.IsReferrersStatisticsDetail = true;
            var data = AccountService.GetReferrersStatisticsDetail(criteria, ref totalPage);

            return View(data.ToPagedList(currentPageIndex, itemPerPage, totalPage * itemPerPage));
        }

        #endregion Statistics Referrers Pages
    }
}