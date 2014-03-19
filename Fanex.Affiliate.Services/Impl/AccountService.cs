// --------------------------------------------------------------------------
// <copyright file="AccountService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.05.30">Created</change>
//   <change who="Kaden.Trinh" date="2013.06.11">Implement funcion get Current Commission Setting</change>
// </history>
namespace Fanex.Affiliate.Services.Impl
{
    using System.Collections.Generic;
    using Common;
    using Common.Helper;
    using Criteria;
    using DataAccess.Impl;
    using DataAccess.Interface;
    using DTO;
    using Interface;
    using ViewModels;

    /// <summary>
    /// Account Service.
    /// </summary>
    public class AccountService : QueryService, IAccountService
    {
        #region Fields

        /// <summary>
        /// Define Account DataAccess.
        /// </summary>
        private readonly IAccountDataAccess _accountDataAccess;

        #endregion Fields

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountService" /> class.
        /// </summary>
        public AccountService()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountService" /> class.
        /// </summary>
        /// <param name="accountDataAccess">Account data access.</param>
        private AccountService(IAccountDataAccess accountDataAccess)
        {
            _accountDataAccess = accountDataAccess ?? new AccountDataAccess();
        }

        #endregion Constructor

        #region Account Overview
        /// <summary>
        /// Get statistics for Affiliate.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <param name="listProduct">Output list product.</param>
        /// <returns>Return object.</returns>
        public StatisticsOverview GetStatisticsOverview(AccountStatisticInfoCriteria criteria, ref List<Product> listProduct)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            return _accountDataAccess.GetStatisticsOverview(criteria, ref listProduct);
        }

        /// <summary>
        /// Get Current Commission Setting.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Return object.</returns>
        public AccountOverviewViewModel GetCommissionSetting(AccountCommGroupCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            return _accountDataAccess.GetCommissionSetting(criteria);
        }

        #endregion

        /// <summary>
        /// Get statement history.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <param name="totalPage">Total page for paging.</param>
        /// <returns>Return object list.</returns>
        public IEnumerable<Statement> GetStatementHistory(StatementCriteria criteria, ref int totalPage)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            return GetData<Statement>(criteria, ref totalPage);
        }

        /// <summary>
        /// Get customer information.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Return object list.</returns>
        public IEnumerable<CustomerInfo> GetCustomerInfo(CustomerInfoCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            return _accountDataAccess.GetCustomerInfo(criteria);
        }

        /// <summary>
        /// Landing page click statistics.
        /// </summary>
        /// <param name="criteria">Landing page criteria.</param>
        /// <returns>Return list.</returns>
        public IEnumerable<TrafficStatistic> GetLandingPagesStatistics(LandingPagesByPageCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            return GetData<TrafficStatistic>(criteria);
        }

        /// <summary>
        /// Landing page click statistics detail.
        /// </summary>
        /// <param name="criteria">Landing page criteria.</param>
        /// <param name="totalPage">Total page for paging.</param>
        /// <returns>Return list.</returns>
        public IEnumerable<ClicksLandingPagesDetail> GetLandingPagesStatisticsDetail(LandingPagesBySiteCriteria criteria, ref int totalPage)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            return GetData<ClicksLandingPagesDetail>(criteria, ref totalPage);
        }

        /// <summary>
        /// Get Referrers Statistics.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Return object list.</returns>
        public IEnumerable<TrafficStatistic> GetReferrersStatistics(TrafficStatisticCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            return GetData<TrafficStatistic>(criteria);
        }

        /// <summary>
        /// Get Referrers Statistics Detail.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <param name="totalPage">Total page for paging.</param>
        /// <returns>Return object list.</returns>
        public IEnumerable<ReferrersStatisticsDetail> GetReferrersStatisticsDetail(TrafficStatisticCriteria criteria, ref int totalPage)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            return GetData<ReferrersStatisticsDetail>(criteria, ref totalPage);
        }

        /// <summary>
        /// Get Daily Earnings.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>List of daily earning.</returns>
        public IEnumerable<DailyEarning> GetDailyEarnings(DailyEarningCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            return _accountDataAccess.GetDailyEarnings(criteria);
        }

        /// <summary>
        /// Get Daily Earnings Detail.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>List of daily earning.</returns>
        public IEnumerable<DailyEarningsDetail> GetDailyEarningsDetail(DailyEarningsDetailCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            return _accountDataAccess.GetDailyEarningsDetail(criteria);
        }

        #region Change Password

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        public void ChangePassword(ChangePasswordCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            criteria.OldPassword = Cryptography.CfsEnCode(criteria.OldPassword.Trim());
            criteria.NewPassword = Cryptography.CfsEnCode(criteria.NewPassword.Trim());
            _accountDataAccess.ChangePassword(criteria);
        }

        #endregion Change Password

        /// <summary>
        /// Get Default Product Commission.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Default Product Commission Model.</returns>
        public DefaultProductCommissionViewModel GetDefaultProductCommission(DefaultCommissionCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            return _accountDataAccess.GetDefaultProductCommission(criteria);
        }
    }
}