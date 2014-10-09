// ----------------------------------------------------------------------
// <copyright file="ReferrersStatisticsDetail.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Jeff.Nguyen" date="2013/07/02">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.DTO
{
    using System;

    /// <summary>
    /// Referrers Statistics Detail.
    /// </summary>
    public class ReferrersStatisticsDetail
    {
        public long TheCount { get; set; }

        public string UrlReferrer { get; set; }

        public int TotalClick { get; set; }

        public int UniqueClick { get; set; }

        public int Signups { get; set; }

        public int FDM { get; set; }
    }
}
