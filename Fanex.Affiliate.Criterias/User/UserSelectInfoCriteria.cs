namespace Fanex.Affiliate.Criteria
{
    using Fanex.Data.Repository;

    /// <summary>
    /// User Select Info Criteria.
    /// </summary>
    public class UserSelectInfoCriteria : CriteriaBase
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