// ----------------------------------------------------------------------
// <copyright file="ExchangeRateCriteria.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/11/25">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.Criteria
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// Bet List Criteria.
    /// </summary>
    public class ExchangeRateCriteria : CriteriaBase
    {
        public int FromCurrency { get; set; }

        public int ToCurrency { get; set; }

        public DateTime Date { get; set; }

        // Output paramaters
        public float Rate { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_User_GetExchangeRate";
        }
    }
}