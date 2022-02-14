namespace Fanex.Affiliate.Criteria
{

    /// <summary>
    /// Default Group Commission Criteria.
    /// </summary>
    public class DefaultCommissionCriteria : CriteriaBaseImpl
    {
        public DefaultCommissionCriteria()
        {
            Environment = string.Empty;
        }

        public string Environment { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_User_GetDefaultProductCommission";
        }
    }
}