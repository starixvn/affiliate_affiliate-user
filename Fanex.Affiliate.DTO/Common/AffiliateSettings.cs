// ----------------------------------------------------------------------
// <copyright file="AffiliateSettings.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2014/03/26">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.DTO
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Affiliate Settings.
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
            SystemDisplayName = string.Empty;
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

        public string SystemDisplayName { get; set; }

        public Uri BannerScriptUrl { get; set; }

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
