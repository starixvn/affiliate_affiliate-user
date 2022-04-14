namespace Fanex.Affiliate.Criteria
{

    /// <summary>
    /// Log Out User Criteria.
    /// </summary>
    public class LogOutUserCriteria : CriteriaBaseImpl
    {
        public int ServerId { get; set; }

        public string Username { get; set; }

        public int AffId { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_User_Logout";
        }
    }
}