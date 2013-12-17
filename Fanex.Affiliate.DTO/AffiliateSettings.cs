// -----------------------------------------------------------------------
// <copyright file="AffiliateSettings.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Affiliate.DTO
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class AffiliateSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AffiliateSettings"/> class.
        /// </summary>
        public AffiliateSettings()
        {
            SystemStartDate = new DateTime();
            BannerDomain = string.Empty;
            LicenseeID = 0;
            SystemName = string.Empty;
            BannerScriptUrl = string.Empty;
            BannerDirectory = string.Empty;
            AffiliateSupportedLanguages = string.Empty;
            AffiliateBaseCurrencyList = string.Empty;
            MinimumWithdrawalAmount = string.Empty;
            AffiliateCurrencyListMultipliedBy = string.Empty;
            MulitpliedBy = string.Empty;
            CompanyNegativeNetWin = string.Empty;
        }

        public DateTime SystemStartDate { get; set; }

        public string BannerDomain { get; set; }

        public int LicenseeID { get; set; }

        public string SystemName { get; set; }

        public string BannerScriptUrl { get; set; }

        public string BannerDirectory { get; set; }

        public string AffiliateSupportedLanguages { get; set; }

        public string AffiliateBaseCurrencyList { get; set; }

        public string MinimumWithdrawalAmount { get; set; }

        public string AffiliateCurrencyListMultipliedBy { get; set; }

        public string MulitpliedBy { get; set; }

        public string CompanyNegativeNetWin { get; set; }

        public IEnumerable<SystemSetting> CommonSettings { get; set; }

        public IEnumerable<Language> BaseLanguage { get; set; }

        public IEnumerable<Language> AllLanguage { get; set; }

        public IEnumerable<Country> BaseCountries { get; set; }

        public IEnumerable<Exchange> BaseCurrencies { get; set; }

        public IEnumerable<Exchange> AllCurrencies { get; set; }

        public IEnumerable<Exchange> AffCurrencies { get; set; }

        public IEnumerable<Currency> PlayerCurrencies { get; set; }
    }
}
