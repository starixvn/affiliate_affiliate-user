namespace Fanex.Affiliate.Criteria
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Banner Update Statistic Criteria.
    /// </summary>
    public class BannerUpdateStatisticCriteria : CriteriaBase
    {
        public int BannerID { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_Banner_UpdateStatistic";
        }
    }
}
