namespace Fanex.Affiliate.Criteria
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// Traffic Statistic Criteria.
    /// </summary>
    public class TrafficStatisticDetailCriteria : CriteriaBase
    {
        public int AffId { get; set; }

        public string Domain { get; set; }

        public DateTime FromDate { get; set; }

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

        /// <summary>
        /// Check whether criteria is valid.
        /// </summary>
        /// <returns>Valid or not.</returns>
        public override bool IsValid()
        {
            return AffId > 0 && FromDate <= ToDate;
        }
    }
}