namespace Fanex.Affiliate.Criteria
{
    using System.Web.Mvc;
    using Fanex.Data.Repository;

    /// <summary>
    /// Change Password Criteria.
    /// </summary>
    public class ChangePasswordInfoCriteria : CriteriaBase
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