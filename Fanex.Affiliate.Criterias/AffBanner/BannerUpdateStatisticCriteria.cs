namespace Fanex.Affiliate.Criteria
{

    /// <summary>
    /// Banner Update Statistic Criteria.
    /// </summary>
    public class BannerUpdateStatisticCriteria : CriteriaBaseImpl
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
