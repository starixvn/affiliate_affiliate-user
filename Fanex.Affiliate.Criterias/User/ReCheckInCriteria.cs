namespace Fanex.Affiliate.Criteria
{

    /// <summary>
    /// Re-Check In Criteria.
    /// </summary>
    public class ReCheckInCriteria : CriteriaBaseImpl
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