// --------------------------------------------------------------------------
// <copyright file="Payment.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.06.03">Created</change>
// </history>
namespace Fanex.Affiliate.DTO
{
    using System;

    /// <summary>
    /// Payment data object.
    /// </summary>
    public class Payment
    {
        public long TransID { get; set; }

        public long RefID { get; set; }

        public DateTime TransDate { get; set; }

        public DateTime CompletedDate { get; set; }

        public string Status { get; set; }

        public decimal Amount { get; set; }

        public string AffDesc { get; set; }
    }
}