// ----------------------------------------------------------------------
// <copyright file="IAccountService.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2014/09/01">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.Services
{
    using System.Collections.Generic;
    using Criteria;
    using DTO;
    using ViewModels;

    /// <summary>
    /// Interface Account service.
    /// </summary>
    public interface IAccountService
    {
        #region Account Overview
        /// <summary>
        /// Get statistics for Affiliate.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <param name="listProduct">Output list product.</param>
        /// <returns>Return object.</returns>
        StatisticsOverview GetStatisticsOverview(AccountStatisticInfoCriteria criteria, ref List<Product> listProduct);

        /// <summary>
        /// Get Current Commission Setting.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Return object.</returns>
        AccountOverviewViewModel GetCommissionSetting(AccountCommGroupCriteria criteria);

        #endregion

        /// <summary>
        /// Get statement history.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <param name="totalPage">Total page for paging.</param>
        /// <returns>Return object list.</returns>
        IEnumerable<Statement> GetStatementHistory(StatementCriteria criteria, ref int totalPage);

        /// <summary>
        /// Get customer information.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Return object list.</returns>
        IEnumerable<CustomerInfo> GetCustomerInfo(CustomerInfoCriteria criteria);

        /// <summary>
        /// Landing page click statistics.
        /// </summary>
        /// <param name="criteria">Landing page criteria.</param>
        /// <returns>Return list.</returns>
        IEnumerable<TrafficStatistic> GetLandingPagesStatistics(LandingPagesByPageCriteria criteria);

        /// <summary>
        /// Landing page click statistics detail.
        /// </summary>
        /// <param name="criteria">Landing page criteria.</param>
        /// <param name="totalPage">Total page for paging.</param>
        /// <returns>Return list.</returns>
        IEnumerable<ClicksLandingPagesDetail> GetLandingPagesStatisticsDetail(LandingPagesBySiteCriteria criteria, ref int totalPage);

        /// <summary>
        /// Get Referrers Statistics.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Return object list.</returns>
        IEnumerable<TrafficStatistic> GetReferrersStatistics(TrafficStatisticCriteria criteria);

        /// <summary>
        /// Get Referrers Statistics Detail.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <param name="totalPage">Total page for paging.</param>
        /// <returns>Return object list.</returns>
        IEnumerable<ReferrersStatisticsDetail> GetReferrersStatisticsDetail(TrafficStatisticDetailCriteria criteria, ref int totalPage);

        /// <summary>
        /// Get Daily Earnings.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>List of daily earning.</returns>
        IEnumerable<DailyEarning> GetDailyEarnings(DailyEarningCriteria criteria);

        /// <summary>
        /// Get Daily Earnings Detail.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>List of daily earning.</returns>
        IEnumerable<DailyEarningsDetail> GetDailyEarningsDetail(DailyEarningsDetailCriteria criteria);

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        void ChangePassword(ChangePasswordCriteria criteria);

        /// <summary>
        /// Get Default Product Commission.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Default Product Commission Model.</returns>
        DefaultProductCommissionViewModel GetDefaultProductCommission(DefaultCommissionCriteria criteria);
    }
}