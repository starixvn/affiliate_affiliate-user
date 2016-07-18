namespace Fanex.Affiliate.Criteria
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Customer Information Criteria.
    /// </summary>
    public class CustomerInfoCriteria : CriteriaBase
    {
        public string AffUsername { get; set; }

        public string Username { get; set; }

        public int AffType { get; set; }

        public int PageSize { get; set; }

        // Output parameters
        public int PageIndex { get; set; }

        public int TotalPages { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_User_MemberListSel";
        }
    }
}