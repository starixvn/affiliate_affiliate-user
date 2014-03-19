// --------------------------------------------------------------------------
// <copyright file="ITrafficStatisticService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.06.03">Created</change>
//   <change who="Kaden.Trinh" date="2013.06.14">Add function Landing Pages</change>
// </history>
namespace Fanex.Affiliate.Services.Interface
{
    using System.Collections.Generic;
    using Criteria;
    using DTO;

    /// <summary>
    /// Interface Traffic Statistic.
    /// </summary>
    public interface ITrafficStatisticService
    {
        /// <summary>
        /// Get list landing pages statistics.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Return list object.</returns>
        IEnumerable<TrafficStatistic> GetLandingPagesStatistics(TrafficStatisticCriteria criteria);
    }
}