namespace Fanex.Affiliate.Criteria
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Statement Criteria.
    /// </summary>
    public class StatementCriteria : CriteriaBase
    {
        public int AffId { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        // Output parameters
        public int TotalPages { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_User_AffiliateStatementHistorySel";
        }
    }
}