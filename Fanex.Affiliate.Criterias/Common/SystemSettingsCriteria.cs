namespace Fanex.Affiliate.Criteria
{

    /// <summary>
    /// System Settings Criteria.
    /// </summary>
    public class SystemSettingsCriteria : CriteriaBaseImpl
    {
        public string Environment { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_User_GetAllAffSystemSettings";
        }
    }
}