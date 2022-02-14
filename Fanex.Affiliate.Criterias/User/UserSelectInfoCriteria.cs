namespace Fanex.Affiliate.Criteria
{

    /// <summary>
    /// User Select Info Criteria.
    /// </summary>
    public class UserSelectInfoCriteria : CriteriaBaseImpl
    {
        public int AffID { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_User_AffiliateSelEditInfo";
        }
    }
}