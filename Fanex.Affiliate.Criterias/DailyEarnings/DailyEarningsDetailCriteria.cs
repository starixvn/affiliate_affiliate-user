namespace Fanex.Affiliate.Criteria
{
    using System;

    /// <summary>
    /// Daily Earnings Detail Criteria.
    /// </summary>
    public class DailyEarningsDetailCriteria : CriteriaBaseImpl
    {
        public DailyEarningsDetailCriteria()
        {
            AffId = 0;
            ProductId = 0;
            MonthDate = null;
        }

        public int AffId { get; set; }

        public int ProductId { get; set; }

        public DateTime? MonthDate { get; set; }

        // Output parameters
        public string AffCurrency { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_User_EarningSelDetail";
        }
    }
}