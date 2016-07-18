namespace Fanex.Affiliate.Criteria
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Overview Criteria.
    /// </summary>
    public class AccountCommGroupCriteria : CriteriaBase
    {
        public int AffId { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_User_CommGroupSelDetail";
        }

        public override bool IsValid()
        {
            return AffId > 0;
        }
    }
}