namespace Fanex.Affiliate.Criteria
{

    /// <summary>
    /// System Setting Criteria.
    /// </summary>
    public class SystemSettingsByTypeCriteria : CriteriaBaseImpl
    {
        public string Type { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_User_SystemSettingsSelByType";
        }

        public override bool IsValid()
        {
            return !string.IsNullOrEmpty(Type);
        }
    }
}