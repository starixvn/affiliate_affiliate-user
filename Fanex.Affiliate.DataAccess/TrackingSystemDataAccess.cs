// ----------------------------------------------------------------------
// <copyright file="TrackingSystemDataAccess.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/08/01">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.DataAccess
{
    using System;
    using Criteria;
    using Data;

    /// <summary>
    /// Tracking statistics.
    /// </summary>
    public class TrackingSystemDataAccess : ITrackingSystemDataAccess
    {
        /// <summary>
        /// Insert statistics into database.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Success or fail.</returns>
        public string InsertStatistic(TrackingSystemCriteria criteria)
        {
            using (var objectDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var currentParams = criteria.GetSpParams();
                objectDb.ExecuteNonQuery(currentParams);

                return Convert.ToInt16(currentParams["errcode"]) != 0
                    ? string.Empty
                    : Convert.ToString(currentParams["redirectpage"]);
            }
        }
    }
}
