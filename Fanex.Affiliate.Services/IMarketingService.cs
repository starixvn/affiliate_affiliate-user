namespace Fanex.Affiliate.Services
{
    using System.Collections.Generic;
    using Criteria;
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