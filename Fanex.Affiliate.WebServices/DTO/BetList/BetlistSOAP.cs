// ----------------------------------------------------------------------
// <copyright file="BetlistSOAP.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2014/10/17">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.WebServices.DTO.BetList
{
    using System;
    using System.Collections.Generic;
    using DTO.BetList;
    using Fanex.Affiliate.DTO;

    /// <summary>
    /// Bet list SOAP class.
    /// </summary>
    public class BetlistSOAP
    {
        public string AffiliateName { get; set; }

        public string Username { get; set; }

        public string Product { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string CurrencyName { get; set; }

        public int CurrencyId { get; set; }

        public int TargetCurrencyId { get; set; }

        public decimal ExchangeRate { get; set; }

        public IEnumerable<Product> SupportedProducts { get; set; }
    }
}