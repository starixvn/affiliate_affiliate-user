// ----------------------------------------------------------------------
// <copyright file="ITrackingSystemService.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/08/01">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.Services
{
    using Criteria;

    /// <summary>
    /// Tracking system statistics.
    /// </summary>
    public interface ITrackingSystemService
    {
        /// <summary>
        /// Insert statistics into database.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Success or fail.</returns>
        string InsertStatistic(TrackingSystemCriteria criteria);
    }
}
