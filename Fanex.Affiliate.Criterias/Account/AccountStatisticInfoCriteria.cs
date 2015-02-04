// ----------------------------------------------------------------------
// <copyright file="AccountStatisticInfoCriteria.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2014/03/19">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.Criteria
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// Overview Criteria.
    /// </summary>
    public class AccountStatisticInfoCriteria : CriteriaBase
    {
        public int AffId { get; set; }

        [SpParam(SqlParamName = "MonthDate")]
        public DateTime MonthAndYear
        {
            get
            {
                return new DateTime(Year, Month, 1);
            }
        }

        [SpParam(Ignored = true)]
        public int Month { get; set; }

        [SpParam(Ignored = true)]
        public int Year { get; set; }

        // Output parameters
        public int TotalActivePlayer { get; set; }

        public int TotalPlayerDepUp { get; set; }

        public int NewSignUpDep { get; set; }

        public int NewSignUp { get; set; }

        public int TotalPlayer { get; set; }

        public decimal BalBroughtforward { get; set; }

        public decimal TotalDeposit { get; set; }

        public decimal TotalDepositUp { get; set; }

        public decimal TotalTurnover { get; set; }

        public decimal TotalTurnoverUp { get; set; }

        public decimal TotalWinlostUp { get; set; }

        public decimal TotalWinlost { get; set; }

        public int TotalUniqueClicks { get; set; }

        public int TotalClicks { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_User_StatisticsSel";
        }

        public override bool IsValid()
        {
            return AffId > 0 && Month > 0 && Year > 0;
        }
    }
}