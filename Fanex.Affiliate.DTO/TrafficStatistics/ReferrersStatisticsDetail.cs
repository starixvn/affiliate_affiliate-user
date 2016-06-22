namespace Fanex.Affiliate.DTO
{
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
