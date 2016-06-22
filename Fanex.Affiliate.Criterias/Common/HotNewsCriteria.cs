namespace Fanex.Affiliate.Criteria
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Overview Criteria.
    /// </summary>
    public class HotNewsCriteria : CriteriaBase
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