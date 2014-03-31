// ----------------------------------------------------------------------
// <copyright file="TrafficStatisticCriteria.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2014/03/25">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.Criteria
{
    using System;
    using NAP.Framework.Data;

    /// <summary>
    /// Traffic Statistic Criteria.
    /// </summary>
    public class TrafficStatisticCriteria : CriteriaBase
    {
        public int AffId { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        // Output parameters
        public int TotalClicks { get; set; }

        public int UniqueClicks { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_User_ClickStatisticsSelBySite";
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