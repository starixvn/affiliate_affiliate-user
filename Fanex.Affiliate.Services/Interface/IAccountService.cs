// --------------------------------------------------------------------------
// <copyright file="IAccountService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.05.30">Created</change>
//   <change who="Kaden.Trinh" date="2013.06.11">Implement funcion get Current Commission Setting</change>
// </history>

namespace Fanex.Affiliate.Services.Interface
{
    using System.Collections.Generic;
    using Criterias;
    using DTO;
    using ViewModels;

    /// <summary>
    /// Interface Account service.
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Get statistics for Affiliate.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <param name="listProduct">Output list product.</param>
        /// <returns>Return object.</returns>
        StatisticsOverview GetStatisticsOverview(AccountOverviewCriteria criteria, ref List<Product> listProduct);

        /// <summary>
        /// Get Current Commission Setting.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Return object.</returns>
        AccountOverviewViewModel GetCommissionSetting(AccountOverviewCriteria criteria);

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
        /// <param name="totalPage">Total page for paging.</param>
        /// <returns>Return object list.</returns>
        IEnumerable<CustomerInfo> GetCustomerInfo(CustomerInfoCriteria criteria, out int totalPage);

        /// <summary>
        /// Landing page click statistics.
        /// </summary>
        /// <param name="criteria">Landing page criteria.</param>
        /// <returns>Return list.</returns>
        IEnumerable<TrafficStatistic> GetLandingPagesStatistics(ClicksLandingPagesCriteria criteria);

        /// <summary>
        /// Landing page click statistics detail.
        /// </summary>
        /// <param name="criteria">Landing page criteria.</param>
        /// <param name="totalPage">Total page for paging.</param>
        /// <returns>Return list.</returns>
        IEnumerable<ClicksLandingPagesDetail> GetLandingPagesStatisticsDetail(ClicksLandingPagesCriteria criteria, ref int totalPage);

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
        IEnumerable<ReferrersStatisticsDetail> GetReferrersStatisticsDetail(TrafficStatisticCriteria criteria, ref int totalPage);

        /// <summary>
        /// Get Daily Earnings.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <param name="affCurrency">Affiliate currency.</param>
        /// <returns>List of daily earning.</returns>
        IEnumerable<DailyEarning> GetDailyEarnings(DailyEarningCriteria criteria, out string affCurrency);

        /// <summary>
        /// Get Daily Earnings Detail.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <param name="affCurrency">Affiliate currency.</param>
        /// <returns>List of daily earning.</returns>
        IEnumerable<DailyEarningsDetail> GetDailyEarningsDetail(DailyEarningsDetailCriteria criteria, out string affCurrency);

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