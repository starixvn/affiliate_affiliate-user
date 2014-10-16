// ----------------------------------------------------------------------
// <copyright file="BalanceUpMoveBetCriteria.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/11/20">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.WebServices.Criteria
{
    using System;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Criteria to get Balance-Up and Move-Bet dates.
    /// </summary>
    public class BalanceUpMoveBetCriteria : CriteriaBase
    {
        // Output parameters
        [SpParam(Ignored = true)]
        public DateTime MoveBetsDay { get; set; }

        [SpParam(Ignored = true)]
        public DateTime BalanceUpDay { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_User_SelBalanceUpDay";
        }
    }
}