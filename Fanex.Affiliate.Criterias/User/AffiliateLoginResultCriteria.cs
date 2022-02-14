namespace Fanex.Affiliate.Criteria
{

    /// <summary>
    /// Authenticate User Criteria.
    /// </summary>
    public class AffiliateLoginResultCriteria : CriteriaBaseImpl
    {
        public int AffId { get; set; }

        public bool IsSuccess { get; set; }

        public int ServerID { get; set; }

        public string LoginIP { get; set; }

        public string NetSessionID { get; set; }

        // Output parameters
        public int OnUserID { get; set; }

        public int ErrCode { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_User_SecureLoginUpdate";
        }

        public override bool IsValid()
        {
            return AffId != 0;
        }
    }
}