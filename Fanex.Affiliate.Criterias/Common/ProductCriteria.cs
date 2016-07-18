namespace Fanex.Affiliate.Criteria
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// Product Criteria.
    /// </summary>
    public class ProductCriteria : CriteriaBase
    {
        public ProductCriteria()
        {
            MonthDate = null;
            Language = "en-US";
        }

        public int AffId { get; set; }

        public DateTime? MonthDate { get; set; }

        public string Language { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_User_ProductSelByAffID";
        }
    }
}