// --------------------------------------------------------------------------
// <copyright file="IMarketingService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Muller.Nguyen" date="2013.06.28">Created</change>
// </history>

namespace Fanex.Affiliate.Services.Interface
{
    using System.Collections.Generic;
    using Criterias;
    using DTO;

    /// <summary>
    /// Interface Media service.
    /// </summary>
    public interface IMarketingService
    {
        IEnumerable<BannerType> GetBannerTypeList(BannerTypeCriteria criteria);

        IEnumerable<Domain> GetDomainList(DomainCriteria criteria);

        IEnumerable<Marketing> GetBannerMediaList(MarketingCriteria criteria, ref int totalPages);
    }
}