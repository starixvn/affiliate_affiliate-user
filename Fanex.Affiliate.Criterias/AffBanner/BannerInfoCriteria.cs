namespace Fanex.Affiliate.Criteria
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Banner Info Criteria.
    /// </summary>
    public class BannerInfoCriteria : CriteriaBase
    {
        public int BannerID { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_Banner_SelInfo";
        }
    }
}
