namespace Fanex.Affiliate.DataAccess
{
    using System;
    using Criteria;
    using Data;
    using Fanex.Data.SqlServer;

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
            using (var objectDb = new SqlObjectDb(criteria.GetSettingKey()))
            {
                objectDb.ExecuteNonQuery(criteria);
                var currentParams = objectDb.GetOutputParams();

                return Convert.ToInt16(currentParams["errcode"]) != 0
                    ? string.Empty
                    : Convert.ToString(currentParams["redirectpage"]);
            }
        }
    }
}
