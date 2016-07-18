namespace Fanex.Affiliate.Services
{
    using Common.Helper;
    using Criteria;
    using DataAccess;

    /// <summary>
    /// Tracking system statistics.
    /// </summary>
    public class TrackingSystemService : ITrackingSystemService
    {
        #region Fields

        /// <summary>
        /// Define Account DataAccess.
        /// </summary>
        private readonly ITrackingSystemDataAccess _trackingSystemDataAccess;

        #endregion Fields

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackingSystemService" /> class.
        /// </summary>
        public TrackingSystemService()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackingSystemService" /> class.
        /// </summary>
        /// <param name="trackingSystemDataAccess">Account data access.</param>
        private TrackingSystemService(ITrackingSystemDataAccess trackingSystemDataAccess)
        {
            _trackingSystemDataAccess = trackingSystemDataAccess ?? new TrackingSystemDataAccess();
        }

        #endregion Constructor

        /// <summary>
        /// Insert statistics into database.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Success or fail.</returns>
        public string InsertStatistic(TrackingSystemCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            return _trackingSystemDataAccess.InsertStatistic(criteria);
        }
    }
}
