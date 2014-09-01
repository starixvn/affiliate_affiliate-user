// --------------------------------------------------------------------------
// <copyright file="ClicksLandingPagesDetail.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Leo.Vo" date="2013.06.29">Created</change>
// </history>

namespace Fanex.Affiliate.DTO
{
    using System;

    /// <summary>
    /// Clicks Landing Pages Detail.
    /// </summary>
    public class ClicksLandingPagesDetail
    {
        public long TheCount { get; set; }

        public Uri UrlReferrer { get; set; }

        public int TotalClick { get; set; }

        public int UniqueClick { get; set; }

        public int Signups { get; set; }

        public int Fdm { get; set; }
    }
}