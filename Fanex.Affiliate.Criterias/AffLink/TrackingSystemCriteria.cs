namespace Fanex.Affiliate.Criteria
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// Criteria for getting the player's info.
    /// </summary>
    public class TrackingSystemCriteria : CriteriaBase
    {
        public int AffiliateID { get; set; }

        public string AffDomain { get; set; }

        public string PageCode { get; set; }

        public string LanguageCode { get; set; }

        public string IP { get; set; }

        public string Browser { get; set; }

        public string URLReferrer { get; set; }

        public string DomainReferrer { get; set; }

        public string OS { get; set; }

        public string Device { get; set; }

        public string BrowserVersion { get; set; }

        public string BrowserResolution { get; set; }

        public string BrowserLanguage { get; set; }

        public string UserAgent { get; set; }

        public string Country { get; set; }

        public string Language { get; set; }

        // Output parameters
        public string RedirectPage { get; set; }

        public long ClickID { get; set; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_Link_AffiliateInsClickStatistics";
        }
    }
}