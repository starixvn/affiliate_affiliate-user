// ----------------------------------------------------------------------
// <copyright file="Product.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2014/10/17">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.WebServices.DTO.BetList
{
    /// <summary>
    /// Product data object.
    /// </summary>
    public class Product
    {
        public byte ProductId { get; set; }

        public string ProductName { get; set; }

        public string OriginalProductName { get; set; }

        public int TotalQualifiedMembers { get; set; }
    }
}