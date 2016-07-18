namespace Fanex.Affiliate.Criteria
{
    using Fanex.Data.Repository;

    /// <summary>
    /// System Settings Criteria.
    /// </summary>
    public class SystemSettingsCriteria : CriteriaBase
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