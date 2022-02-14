namespace Fanex.Affiliate.Criteria
{
    using Fanex.Data;

    /// <summary>
    /// Overview Criteria.
    /// </summary>
    public class HotNewsCriteria : CriteriaBaseImpl
    {
        public int? MsgId { get; set; }

        [SpParam(Ignored = true)]
        public string Language { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_User_HotNewsDetail_New";
        }
    }
}