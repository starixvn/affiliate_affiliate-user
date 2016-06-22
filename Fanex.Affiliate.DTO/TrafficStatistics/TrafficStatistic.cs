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