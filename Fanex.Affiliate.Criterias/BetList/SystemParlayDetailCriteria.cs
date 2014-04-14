// ----------------------------------------------------------------------
// <copyright file="SystemParlayDetailCriteria.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/11/28">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.Criteria
{
    using System;
    using NAP.Framework.Data;

    /// <summary>
    /// System Parlay Detail Criteria.
    /// </summary>
    public class SystemParlayDetailCriteria : CriteriaBase
    {
        public int LicenseeId { get; set; }

        public int UserId { get; set; }

        public long TransId { get; set; }

        public long RefNo { get; set; }

        public long BetId { get; set; }

        public int CustId { get; set; }

        public DateTime MoveBetsDay { get; set; }

        public DateTime BalanceUpDay { get; set; }

        // Output parameters
        public decimal Outstanding { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_User_BetListSystemParlayDetail";
        }
    }
}
