// --------------------------------------------------------------------------
// <copyright file="AccountOverviewCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.06.10">Created and add Properties</change>
// </history>
namespace Fanex.Affiliate.Criterias
{
    using System;
    using NAP.Framework.Data;

    /// <summary>
    /// Overview Criteria.
    /// </summary>
    public class AccountOverviewCriteria : CriteriaBase
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

        [SpParam(Ignored = true)]
        public bool IsCommSetting { get; set; }

        public int TotalActivePlayer { get; set; }

        public int TotalPlayerDepUp { get; set; }

        public int TotalPlayer { get; set; }

        public int NewSignUp { get; set; }

        public int NewSignUpDep { get; set; }

        public int TotalUniqueClicks { get; set; }

        public int TotalClicks { get; set; }

        public decimal BalBroughtforward { get; set; }

        public decimal TotalDeposit { get; set; }

        public decimal TotalDepositUp { get; set; }

        public decimal TotalTurnover { get; set; }

        public decimal TotalTurnoverUp { get; set; }

        public decimal TotalWinlostUp { get; set; }

        public decimal TotalWinlost { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return IsCommSetting ? "Aff_CommGroupSelDetail" : "Aff_StatisticsSel";
        }

        public override bool IsValid()
        {
            return Month > 0 && Year > 0;
        }
    }
}