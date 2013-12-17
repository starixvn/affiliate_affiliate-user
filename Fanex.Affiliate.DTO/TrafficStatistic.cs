// --------------------------------------------------------------------------
// <copyright file="TrafficStatistic.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.06.03">Created</change>
// </history>
namespace Fanex.Affiliate.DTO
{
    /// <summary>
    /// Traffic Statistic.
    /// </summary>
    public class TrafficStatistic
    {
        public string DomainReferrer { get; set; }

        public string SportCode { get; set; }

        public string LanguageCode { get; set; }

        public int TotalClick { get; set; }

        public int UniqueClick { get; set; }

        public int Signups { get; set; }

        public int FDM { get; set; }
    }
}