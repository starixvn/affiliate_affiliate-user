// ----------------------------------------------------------------------
// <copyright file="HotNewsService.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Brian.Dinh" date="2013/07/15">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.Services.Impl
{
    using Caching;
    using Configurations;
    using Criteria;
    using DataAccess.Impl;
    using DataAccess.Interface;
    using DTO;
    using Fanex.Common.Helper;
    using Interface;
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
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HotNewsService" /> class.
        /// </summary>
        /// <param name="hotNewsUsDataAccess">HotNews DataAccess.</param>
        private HotNewsService(IHotNewsDataAccess hotNewsUsDataAccess)
        {
            _cacheManager = CacheService.CreateInstance(CacheType.MemoryCache);
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
            var hotNews = _cacheManager.Get<HotNews>(HotNewsCacheKey);

            if (hotNews == null)
            {
                hotNews = _hotNewsDataAccess.GetHotNews(criteria);
                _cacheManager.Set<HotNews>(HotNewsCacheKey, hotNews,  AppSettings.TimeHotMessage);
            }

            return new HotNewsViewModel
                {
                    Important = hotNews.Important,
                    Message = hotNews.GetMessageByLanguage(criteria.Language)
                };
        }
    }
}