// --------------------------------------------------------------------------
// <copyright file="TrafficStatisticService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.06.03">Created</change>
// </history>
namespace Fanex.Affiliate.Services.Impl
{
    using System.Collections.Generic;
    using Common.Helper;
    using Criteria;
    using DTO;
    using Interface;

    /// <summary>
    /// Traffic Statistic Service.
    /// </summary>
    public class TrafficStatisticService : QueryService, ITrafficStatisticService
    {
        /// <summary>
        /// Get list landing pages statistics.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Return list object.</returns>
        public IEnumerable<TrafficStatistic> GetLandingPagesStatistics(TrafficStatisticCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            return GetData<TrafficStatistic>(criteria);
        }
    }
}