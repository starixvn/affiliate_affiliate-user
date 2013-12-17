// --------------------------------------------------------------------------
// <copyright file="PaymentStatus.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.07.05">Created</change>
// </history>
namespace Fanex.Affiliate.Enumerations
{
    public enum PaymentStatus
    {
        /// <summary>
        /// Processing payment request.
        /// </summary>
        Pending,

        /// <summary>
        /// Cancel payment request.
        /// </summary>
        Cancelled
    }
}