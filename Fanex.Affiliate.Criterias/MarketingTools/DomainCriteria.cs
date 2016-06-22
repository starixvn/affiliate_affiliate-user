namespace Fanex.Affiliate.Criteria
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Domain Criteria.
    /// </summary>
    public class DomainCriteria : CriteriaBase
    {
        public int AffID { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_User_DomainAffClientSel";
        }
    }
}
