namespace Fanex.Affiliate.DataAccess
{
    using Criteria;
    using DTO;

    /// <summary>
    /// Interface Account.
    /// </summary>
    public interface IBannerDataAccess
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