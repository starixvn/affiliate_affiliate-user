namespace Fanex.Affiliate.Criteria
{

    /// <summary>
    /// System Date Time Criteria.
    /// </summary>
    public class SystemDateTimeCriteria : CriteriaBaseImpl
    {
        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_User_AffiliateSelDBTime";
        }
    }
}