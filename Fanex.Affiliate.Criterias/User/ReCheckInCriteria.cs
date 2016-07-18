namespace Fanex.Affiliate.Criteria
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Re-Check In Criteria.
    /// </summary>
    public class ReCheckInCriteria : CriteriaBase
    {
        public int OnUserID { get; set; }

        public bool IsKickedOut { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_User_Recheckin";
        }
    }
}