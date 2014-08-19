// ----------------------------------------------------------------------
// <copyright file="DailyEarningsDetail.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/08/22">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.DTO
{
    using System;

    /// <summary>
    /// Daily Earnings Detail.
    /// </summary>
    public class DailyEarningsDetail
    {
        public string Username { get; set; }

        public short Currency { get; set; }

        public DateTime WinLostDate { get; set; }

        public double Deposit { get; set; }

        public double Withdrawal { get; set; }

        public double Winlost { get; set; }

        public double MemWinlostFake { get; set; }

        public double Bonus { get; set; }

        public double Adjustment { get; set; }

        public double Turnover { get; set; }

        public double Psp { get; set; }

        public double PlayerComm { get; set; }

        public double PWin { get; set; }

        public double PShare { get; set; }

        public double NetGaming { get; set; }
    }
}