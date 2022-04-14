namespace Fanex.Affiliate.Criteria
{

    /// <summary>
    /// Banner Info Criteria.
    /// </summary>
    public class BannerInfoCriteria : CriteriaBaseImpl
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
