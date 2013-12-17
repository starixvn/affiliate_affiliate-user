// --------------------------------------------------------------------------
// <copyright file="TrafficStatisticCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.06.03">Created and add Properties</change>
// </history>
namespace Fanex.Affiliate.Criterias
{
    using System;
    using NAP.Framework.Data;

    /// <summary>
    /// Traffic Statistic Criteria.
    /// </summary>
    public class TrafficStatisticCriteria : CriteriaBase
    {
        public int AffId { get; set; }

        public string Domain { get; set; }

        [SpParam(SqlParamName = "PageIndex")]
        public int Page { get; set; }

        public int TotalPages { get; set; }

        public int PageSize { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public bool IsReferrersStatistics { get; set; }

        public bool IsReferrersStatisticsDetail { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            string settingKey = "Aff_StatementHistorySel";

            if (IsReferrersStatistics)
            {
                settingKey = "Aff_ClickStatisticsSelBySite";
            }

            if (IsReferrersStatisticsDetail)
            {
                settingKey = "Aff_ClickStatisticsDetailBySite";
            }

            return settingKey;
        }

        /// <summary>
        /// Check whether criteria is valid.
        /// </summary>
        /// <returns>Valid or not.</returns>
        public override bool IsValid()
        {
            return FromDate <= ToDate;
        }
    }
}