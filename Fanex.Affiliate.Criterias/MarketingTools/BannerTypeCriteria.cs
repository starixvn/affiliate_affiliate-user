namespace Fanex.Affiliate.Criteria
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Banner Type Criteria.
    /// </summary>
    public class BannerTypeCriteria : CriteriaBase
    {
        public BannerTypeCriteria()
        {
            Language = "en-US";
        }

        public string TypeName { get; set; }

        public string Language { get; set; }

        public int? ParentType { get; set; }

        [SpParam(Ignored = true)]
        public bool GenAll { get; set; }

        [SpParam(Ignored = true)]
        public string Category { get; set; }

        [SpParam(Ignored = true)]
        public int ActiveValue { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_User_BannerSelType";
        }
    }
}
