// --------------------------------------------------------------------------
// <copyright file="DailyEarningCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.06.14">Created</change>
// </history>

namespace Fanex.Affiliate.Criteria
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// Daily Earning Criteria.
    /// </summary>
    public class DailyEarningCriteria : CriteriaBase
    {
        public DailyEarningCriteria()
        {
            AffId = 0;
            ProductId = 0;
            MonthDate = null;
            FromDate = null;
            ToDate = null;
        }

        public int AffId { get; set; }

        public int ProductId { get; set; }

        public DateTime? MonthDate { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        // Output parameters
        public string AffCurrency { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_User_EarningSelDaily";
        }
    }
}