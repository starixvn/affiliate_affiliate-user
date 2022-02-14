namespace Fanex.Affiliate.Criteria
{
    using Fanex.Data;

    /// <summary>
    /// Criteria for getting the player's info.
    /// </summary>
    public class PaymentCriteria : CriteriaBaseImpl
    {
        public int AffId { get; set; }

        // Output parameters
        public string PreferredPayment { get; set; }

        [SpParam(SqlParamName = "BalBroughtForward")]
        public decimal BalanceBroughtForward { get; set; }

        [SpParam(SqlParamName = "MinAmount")]
        public decimal MinimumWithdrawalAmount { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_User_PaymentRequestSelInfoByAff";
        }
    }
}