namespace Fanex.Affiliate.Criteria
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Fanex.Data;
    using Resources;

    /// <summary>
    /// Landing Pages By Page Criteria.
    /// </summary>
    public class LandingPagesByPageCriteria : CriteriaBaseImpl
    {
        public int AffId { get; set; }

        [Required(ErrorMessageResourceName = "FromDateIsRequired", ErrorMessageResourceType = typeof(Lang))]
        public DateTime FromDate { get; set; }

        [Required(ErrorMessageResourceName = "ToDateIsRequired", ErrorMessageResourceType = typeof(Lang))]
        public DateTime ToDate { get; set; }

        [SpParam(SqlParamName = "SportCode")]
        public string PageSelected { get; set; }

        [SpParam(SqlParamName = "LanguageCode")]
        public string LanguageSelected { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_User_ClickStatisticsSelByPage";
        }

        public override bool IsValid()
        {
            return AffId > 0 && FromDate <= ToDate;
        }
    }
}