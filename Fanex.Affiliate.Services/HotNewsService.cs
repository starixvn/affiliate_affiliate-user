namespace Fanex.Affiliate.Services
{
    using Caching;
    using Common.Helper;
    using Configurations;
    using Criteria;
    using DataAccess;
    using DTO;
    using Fanex.Caching.Memory;
    using System;
    using ViewModels;

    /// <summary>
    /// Hot News Service.
    /// </summary>
    public class HotNewsService : IHotNewsService
    {
        #region Fields

        /// <summary>
        /// HotNews DataAccess.
        /// </summary>
        private const string HotNewsCacheKey = "Fanex.Cache.Affiliate.HotNews";

        private readonly IHotNewsDataAccess _hotNewsDataAccess;

        private readonly ICache _cacheManager;

        #endregion Fields

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="HotNewsService" /> class.
        /// </summary>
        public HotNewsService()
            : this(null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HotNewsService" /> class.
        /// </summary>
        /// <param name="hotNewsUsDataAccess">HotNews DataAccess.</param>
        private HotNewsService(IHotNewsDataAccess hotNewsUsDataAccess, ICache cache)
        {
            _cacheManager = cache ?? new MemoryCache(new MemoryCacheOptions());
            _hotNewsDataAccess = hotNewsUsDataAccess ?? new HotNewsDataAccess();
        }

        #endregion Constructor

        /// <summary>
        /// Get hot news.
        /// </summary>
        /// <param name="criteria">Hot News Criteria.</param>
        /// <returns>Hot news view model.</returns>
        public HotNewsViewModel GetHotNews(HotNewsCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            _cacheManager.TryGet<HotNews>(HotNewsCacheKey, out HotNews hotNews);

            if (hotNews == null)
            {
                hotNews = _hotNewsDataAccess.GetHotNews(criteria);
                var cachePolicy = new CacheItemOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(AppSettings.TimeHotMessage));
                _cacheManager.Set<HotNews>(HotNewsCacheKey, hotNews, cachePolicy);
            }

            return new HotNewsViewModel
                {
                    Important = hotNews.Important,
                    Message = hotNews.GetMessageByLanguage(criteria.Language)
                };
        }
    }
}