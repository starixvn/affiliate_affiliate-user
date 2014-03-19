// --------------------------------------------------------------------------
// <copyright file="IBannerService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013.09.24">Created</change>
// </history>

namespace Fanex.Affiliate.Services.Interface
{
    using Criteria;
    using DTO;

    /// <summary>
    /// Interface Account service.
    /// </summary>
    public interface IBannerService
    {
        /// <summary>
        /// Select Banner Information.
        /// </summary>
        /// <param name="criteria">Banner criteria.</param>
        /// <returns>Banner data object.</returns>
        Banner BannerSelectInfo(BannerInfoCriteria criteria);

        /// <summary>
        /// Update Banner statistic.
        /// </summary>
        /// <param name="criteria">Banner criteria.</param>
        void BannerUpdateStatistic(BannerUpdateStatisticCriteria criteria);

        /// <summary>
        /// Get System Name.
        /// </summary>
        /// <param name="settingKey">SP string name.</param>
        /// <returns>System name.</returns>
        string BannerGetSystemName(string settingKey);
    }
}