// --------------------------------------------------------------------------
// <copyright file="BannerService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013.09.24">Created</change>
// </history>

namespace Fanex.Affiliate.Services
{
    using Common.Helper;
    using Criteria;
    using DataAccess;
    using DTO;

    /// <summary>
    /// Account Service.
    /// </summary>
    public class BannerService : QueryService, IBannerService
    {
        #region Fields

        /// <summary>
        /// Define Account DataAccess.
        /// </summary>
        private readonly IBannerDataAccess _bannerDataAccess;

        #endregion Fields

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BannerService" /> class.
        /// </summary>
        public BannerService()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BannerService" /> class.
        /// </summary>
        /// <param name="bannerDataAccess">Banner data access.</param>
        private BannerService(IBannerDataAccess bannerDataAccess)
        {
            _bannerDataAccess = bannerDataAccess ?? new BannerDataAccess();
        }

        #endregion Constructor
                
        /// <summary>
        /// Select Banner Information.
        /// </summary>
        /// <param name="criteria">Banner criteria.</param>
        /// <returns>Banner data object.</returns>
        public Banner BannerSelectInfo(BannerInfoCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            return _bannerDataAccess.BannerSelectInfo(criteria);
        }

        /// <summary>
        /// Update Banner statistic.
        /// </summary>
        /// <param name="criteria">Banner criteria.</param>
        public void BannerUpdateStatistic(BannerUpdateStatisticCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            _bannerDataAccess.BannerUpdateStatistic(criteria);
        }

        /// <summary>
        /// Get System Name.
        /// </summary>
        /// <param name="settingKey">SP string name.</param>
        /// <returns>System name.</returns>
        public string BannerGetSystemName(string settingKey)
        {
            return _bannerDataAccess.BannerGetSystemName(settingKey);
        }
    }
}