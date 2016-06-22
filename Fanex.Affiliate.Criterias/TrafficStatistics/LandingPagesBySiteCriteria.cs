namespace Fanex.Affiliate.Criteria
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Fanex.Data.Repository;
    using Resources;

    /// <summary>
    /// Landing Pages By Site Criteria.
    /// </summary>
    public class LandingPagesBySiteCriteria : CriteriaBase
    {
        public int AffId { get; set; }

        public string Domain { get; set; }

        [SpParam(SqlParamName = "SportCode")]
        public string PageSelected { get; set; }

        [SpParam(SqlParamName = "LanguageCode")]
        public string LanguageSelected { get; set; }
        
        [Required(ErrorMessageResourceName = "FromDateIsRequired", ErrorMessageResourceType = typeof(Lang))]
        public DateTime FromDate { get; set; }

        [Required(ErrorMessageResourceName = "ToDateIsRequired", ErrorMessageResourceType = typeof(Lang))]
        public DateTime ToDate { get; set; }

        public int PageSize { get; set; }

        [SpParam(SqlParamName = "PageIndex")]
        public int Page { get; set; }

        // Output parameters
        public int TotalPages { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_User_ClickStatisticsDetailBySite";
        }

        public override bool IsValid()
        {
            return AffId > 0 && FromDate <= ToDate;
        }
    }
}