// ----------------------------------------------------------------------
// <copyright file="BetListViewModel.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/11/19">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.ViewModels
{
    using System.Collections.Generic;
    using Fanex.Affiliate.DTO.BetList;

    /// <summary>
    /// Overview View Model.
    /// </summary>
    public class BetListViewModel
    {
        public List<TicketText> TicketTextList { get; set; }

        public bool IsShowSystemIdAndExternalId { get; set; }

        public bool IsShowWinLoss { get; set; }

        public decimal ExchangeRate { get; set; }

        public TicketResult TicketResult { get; set; }
    }
}