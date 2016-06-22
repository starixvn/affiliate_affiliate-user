namespace Fanex.Affiliate.DataAccess
{
    using Criteria;
    using DTO;

    /// <summary>
    /// Hot News Data Access.
    /// </summary>
    public interface IHotNewsDataAccess
    {
        /// <summary>
        /// Get hot news.
        /// </summary>
        /// <param name="criteria">Hot news criteria.</param>
        /// <returns>Hot news model.</returns>
        HotNews GetHotNews(HotNewsCriteria criteria);
    }
}
