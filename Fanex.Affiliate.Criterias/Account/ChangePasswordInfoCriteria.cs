namespace Fanex.Affiliate.Criteria
{

    /// <summary>
    /// Change Password Criteria.
    /// </summary>
    public class ChangePasswordInfoCriteria : CriteriaBaseImpl
    {
        public int AffId { get; set; }

        public int ErrCode { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_User_SecureUpdPasswordInfo";
        }
    }
}