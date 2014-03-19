// ----------------------------------------------------------------------
// <copyright file="DailyEarningsDetailCriteria.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/08/22">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.Criteria
{
    using System;
    using NAP.Framework.Data;

    /// <summary>
    /// Daily Earnings Detail Criteria.
    /// </summary>
    public class DailyEarningsDetailCriteria : CriteriaBase
    {
        public DailyEarningsDetailCriteria()
        {
            AffId = 0;
            ProductId = 0;
            MonthDate = null;
        }

        public int AffId { get; set; }

        public int ProductId { get; set; }

        public DateTime? MonthDate { get; set; }

        public string AffCurrency { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_EarningSelDetail";
        }
    }
}