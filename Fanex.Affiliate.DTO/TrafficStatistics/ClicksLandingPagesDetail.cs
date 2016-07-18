namespace Fanex.Affiliate.DTO
{
    /// <summary>
    /// Clicks Landing Pages Detail.
    /// </summary>
    public class ClicksLandingPagesDetail
    {
        public long TheCount { get; set; }

        public string UrlReferrer { get; set; }

        public int TotalClick { get; set; }

        public int UniqueClick { get; set; }

        public int Signups { get; set; }

        public int Fdm { get; set; }
    }
}