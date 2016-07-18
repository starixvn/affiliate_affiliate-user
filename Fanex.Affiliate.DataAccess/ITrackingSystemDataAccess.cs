namespace Fanex.Affiliate.DataAccess
{
    using Criteria;

    public interface ITrackingSystemDataAccess
    {
        /// <summary>
        /// Insert statistics into database.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Success or fail.</returns>
        string InsertStatistic(TrackingSystemCriteria criteria);
    }
}
