// --------------------------------------------------------------------------
// <copyright file="MarketingService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Muller.Nguyen" date="2013.06.28">Created</change>
// </history>

namespace Fanex.Affiliate.Services.Impl
{
    using System.Collections.Generic;
    using Common.Helper;
    using Criteria;
    using DTO;
    using Interface;

    /// <summary>
    /// Marketing service.
    /// </summary>
    public class MarketingService : QueryService, IMarketingService
    {
        /// <summary>
        /// Get list banner.
        /// </summary>
        /// <param name="criteria">Banner type criteria.</param>
        /// <returns>List banner follow type.</returns>
        public IEnumerable<BannerType> GetBannerTypeList(BannerTypeCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            return GetData<BannerType>(criteria);
        }

        /// <summary>
        /// Get list affiliate domain.
        /// </summary>
        /// <param name="criteria">Domain criteria.</param>
        /// <returns>List affiliate domain.</returns>
        public IEnumerable<Domain> GetDomainList(DomainCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            return GetData<Domain>(criteria);
        }

        /// <summary>
        /// Get list media.
        /// </summary>
        /// <param name="criteria">Marketing criteria.</param>
        /// <param name="totalPages">Total page for paging.</param>
        /// <returns>List banner media.</returns>
        public IEnumerable<Marketing> GetBannerMediaList(MarketingCriteria criteria, ref int totalPages)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            return GetData<Marketing>(criteria, ref totalPages);
        }
    }
}