// ----------------------------------------------------------------------
// <copyright file="Currency.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2014/03/26">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.DTO
{
    /// <summary>
    /// Currency Object.
    /// </summary>
    public class Currency
    {
        public short CurrencyId { get; set; }

        public string CurrencyCode { get; set; }

        public string CurrencyName { get; set; }
    }
}
