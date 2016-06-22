namespace Fanex.Affiliate.Criteria
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// Bet List Criteria.
    /// </summary>
    public class ExchangeRateCriteria : CriteriaBase
    {
        public int FromCurrency { get; set; }

        public int ToCurrency { get; set; }

        public DateTime Date { get; set; }

        // Output paramaters
        public float Rate { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_User_GetExchangeRate";
        }
    }
}