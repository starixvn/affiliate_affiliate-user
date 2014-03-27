// --------------------------------------------------------------------------
// <copyright file="Product.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.06.10">Created</change>
// </history>

namespace Fanex.Affiliate.DTO
{
    /// <summary>
    /// Product data object.
    /// </summary>
    public class Product
    {
        public byte ProductId { get; set; }

        public string ProductName { get; set; }

        public int TotalQualifiedMembers { get; set; }
    }
}