// --------------------------------------------------------------------------
// <copyright file="DailyEarning.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.06.10">Created</change>
// </history>

namespace Fanex.Affiliate.DTO
{
    using System;

    /// <summary>
    /// Daily earnings.
    /// </summary>
    public class DailyEarning
    {
        public DateTime WinLostDate { get; set; }

        public decimal Deposit { get; set; }

        public decimal Withdrawal { get; set; }

        public decimal MemberWinlost { get; set; }

        public decimal MemWinlostFake { get; set; }

        public decimal Bonus { get; set; }

        public decimal Adjustment { get; set; }

        public decimal Turnover { get; set; }

        public decimal Psp { get; set; }
    }
}