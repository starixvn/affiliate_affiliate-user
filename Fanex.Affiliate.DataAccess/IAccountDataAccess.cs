// --------------------------------------------------------------------------
// <copyright file="IAccountDataAccess.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.05.30">Created</change>
//   <change who="Kaden.Trinh" date="2013.06.10">Implement funcion get statistics overview account</change>
//   <change who="Kaden.Trinh" date="2013.06.11">Implement funcion get Current Commission Setting</change>
// </history>

namespace Fanex.Affiliate.DataAccess
{
    using System.Collections.Generic;
    using Criteria;
    using DTO;
    using Fanex.BetList.Core.Entities;
    using ViewModels;

    /// <summary>
    /// Interface Account.
    /// </summary>
    public interface IAccountDataAccess
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
        /// Get Daily Earnings.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>List of daily earning.</returns>
        IEnumerable<DailyEarning> GetDailyEarnings(DailyEarningCriteria criteria);

        /// <summary>
        /// Get Daily Earnings Detail.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>List of daily earning detail.</returns>
        IEnumerable<DailyEarningsDetail> GetDailyEarningsDetail(DailyEarningsDetailCriteria criteria);

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>Old-Password string.</returns>
        string GetChangePasswordInfo(ChangePasswordInfoCriteria criteria);

        /// <summary>
        /// Update change password result into DB.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        void UpdateChangePasswordResult(ChangePasswordCriteria criteria);

        /// <summary>
        /// Get Default Product Commission.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Default Product Commission Model.</returns>
        DefaultProductCommissionViewModel GetDefaultProductCommission(DefaultCommissionCriteria criteria);

        /// <summary>
        /// Get Customer Info.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Customer Info Model.</returns>
        IEnumerable<CustomerInfo> GetCustomerInfo(CustomerInfoCriteria criteria);
    }
}