namespace Fanex.Affiliate.Services
{
    using System.Collections.Generic;
    using Common.Helper;
    using Criteria;
    using DTO;
    using Fanex.Affiliate.Resources;

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
            var result = GetData<BannerType>(criteria);

            foreach (var item in result)
            {
                if (!string.IsNullOrEmpty(item.Value))
                {
                    var value = Lang.ResourceManager.GetString(item.Value.Replace(" ", string.Empty));
                    item.Value = string.IsNullOrEmpty(value) ? item.Value : value;
                }
            }

            return result;
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