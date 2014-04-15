// ----------------------------------------------------------------------
// <copyright file="TicketResult.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2014/04/15">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.DTO.BetList
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class TicketResult
    {
        public TicketResult()
        {
            MainTicketList = new List<AffTicket>();
            TicketDataList = new List<ITicketData>();
        }

        public IList<AffTicket> MainTicketList { get; set; }

        public List<ITicketData> TicketDataList { get; set; }

        // Sum value
        public int TotalRows { get; set; }

        public decimal SumWinLoss { get; set; }

        public decimal SumLicenseeWl { get; set; }

        public decimal SumComm { get; set; }

        public decimal SumPlayerComm { get; set; }

        public decimal SumStake { get; set; }
    }
}