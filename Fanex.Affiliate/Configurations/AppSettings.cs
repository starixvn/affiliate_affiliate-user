namespace Fanex.Affiliate.Configurations
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Globalization;
    using System.Linq;
    using Caching;
    using Criteria;
    using DTO;
    using Logging;
    using Services;

    /// <summary>
    /// App Settings.
    /// </summary>
    public static class AppSettings
    {
        private const string AffiliateSettingsCacheKey = "AffiliateSettingsCacheKey";

        private const int CacheDuration = 60;

        private static readonly Lazy<ISystemSettingService> SystemSetting = new 
            Lazy<ISystemSettingService>(() => new SystemSettingService());

        private static readonly ICache CacheInstance = CacheService.CreateInstance(CacheType.MemoryCache);
        
        #region AppSettings

        public static string Environment
        {
            get
            {
                return GetValue("Environment");
            }
        }

        public static string PiwikSiteId
        {
            get
            {
                return GetValue("PiwikSiteId");
            }
        }

        public static string Layout
        {
            get
            {
                return GetValue("Layout");
            }
        }

        public static string Theme
        {
            get
            {
                return GetValue("Theme");
            }
        }       

        public static int PageSize
        {
            get
            {
                return Convert.ToInt32(GetValue("PageSize"));
            }
        }

        public static int ServerId
        {
            get
            {
                return Convert.ToInt32(GetValue("ServerID"));
            }
        }

        public static int InputAffType
        {
            get
            {
                return Convert.ToInt32(GetValue("InputAffType"));
            }
        }

        public static bool IsKiosk
        {
            get
            {
                return Convert.ToInt32(GetValue("InputAffType")) == 3;
            }
        }

        public static string DefaultLanguage
        {
            get
            {
                return GetValue("DefaultLanguage");
            }
        }

        public static string BccMail
        {
            get
            {
                return GetValue("ITMail");
            }
        }

        public static string MailContact
        {
            get
            {
                return GetValue("MailContact");
            }
        }
        
        public static bool IsTesting
        {
            get
            {
                return GetValue("Environment").Equals("Production", StringComparison.OrdinalIgnoreCase) == false;
            }
        }
        
        public static string AffCustomerServiceUrl
        {
            get
            {
                return GetValue("AffCustomerServiceUrl");
            }
        }

        public static string LinkFormat
        {
            get
            {
                return GetValue("LinkFormat");
            }
        }

        public static string DefaultTarget
        {
            get
            {
                return GetValue("DefaultTarget");
            }
        }

        public static string DefaultCurrency
        {
            get
            {
                return GetValue("DefaultCurrency");
            }
        }

        public static string IsShowZeroCommission
        {
            get
            {
                return GetValue("IsShowZeroCommission");
            }
        }

        public static bool IsShowDetailDailyEarnings
        {
            get
            {
                return GetValue("IsShowDetailDailyEarnings") == "1";
            }
        }

        public static int TimeHotMessage
        {
            get
            {
                return Convert.ToInt32(GetValue("TimeHotMessage"));
            }
        }

        public static string EmailHeaderBGColor
        {
            get
            {
                return GetValue("EmailHeaderBgColor");
            }
        }

        public static string EmailHeaderFontColor
        {
            get
            {
                return GetValue("EmailHeaderFontColor");
            }
        }

        public static string EmailContentBGColor
        {
            get
            {
                return GetValue("EmailContentBgColor");
            }
        }

        public static string EmailContentFontColor
        {
            get
            {
                return GetValue("EmailContentFontColor");
            }
        }

        public static string EmailCopyrightBGColor
        {
            get
            {
                return GetValue("EmailCopyrightBgColor");
            }
        }

        public static string EmailCopyrightFontColor
        {
            get
            {
                return GetValue("EmailCopyrightFontColor");
            }
        }

        public static string EmailFooterBGColor
        {
            get
            {
                return GetValue("EmailFooterBgColor");
            }
        }

        public static string EmailFooterFontColor
        {
            get
            {
                return GetValue("EmailFooterFontColor");
            }
        }

        #endregion AppSettings

        #region AffiliateSettings

        public static string MinimumWithdrawalAmount
        {
            get
            {
                return AffiliateSettings.MinimumWithdrawalAmount;
            }
        }

        public static string AffiliateBaseCurrencyList
        {
            get
            {
                return AffiliateSettings.AffiliateBaseCurrencyList;
            }
        }

        public static string AffiliateCurrencyListMultipliedBy
        {
            get
            {
                return AffiliateSettings.AffiliateCurrencyListMultipliedBy;
            }
        }

        public static string MulitpliedBy
        {
            get
            {
                return AffiliateSettings.MulitpliedBy;
            }
        }

        public static string AffiliateSupportedLanguages
        {
            get
            {
                return AffiliateSettings.AffiliateSupportedLanguages;
            }
        }

        public static string AffiliateName
        {
            get
            {
                return GetValue("AffiliateName");
            }
        }

        public static string SystemDisplayName
        {
            get
            {
                return AffiliateSettings.SystemDisplayName;
            }
        }

        public static DateTime SystemStartDate
        {
            get
            {
                return AffiliateSettings.SystemStartDate;
            }
        }

        public static IEnumerable<Language> BaseLanguages
        {
            get
            {
                return AffiliateSettings.BaseLanguage;
            }
        }

        public static IEnumerable<Language> AllLanguages
        {
            get
            {
                return AffiliateSettings.AllLanguage;
            }
        }

        public static IEnumerable<Country> BaseCountries
        {
            get
            {
                return AffiliateSettings.BaseCountries;
            }
        }

        public static IEnumerable<Exchange> BaseCurrencies
        {
            get
            {
                return AffiliateSettings.BaseCurrencies;
            }
        }

        public static IEnumerable<Exchange> AffiliateCurrencies
        {
            get
            {
                return AffiliateSettings.AffCurrencies;
            }
        }

        public static IEnumerable<Currency> PlayerCurrencies
        {
            get
            {
                return AffiliateSettings.PlayerCurrencies;
            }
        }

        public static string BannerDomain
        {
            get
            {
                return AffiliateSettings.BannerDomain;
            }
        }

        public static int LicenseeID
        {
            get
            {
                return AffiliateSettings.LicenseeID;
            }
        }

        public static string SystemName
        {
            get
            {
                return AffiliateSettings.SystemName;
            }
        }

        public static Uri BannerScriptUrl
        {
            get
            {
                return AffiliateSettings.BannerScriptUrl;
            }
        }

        public static string BannerDirectory
        {
            get
            {
                return AffiliateSettings.BannerDirectory;
            }
        }

        public static int CompanyNegativeNetWin
        {
            get
            {
                return (int)(float.Parse(AffiliateSettings.CompanyNegativeNetWin) * (float)100);
            }
        }

        public static string DefaultCurrencySymbol
        {
            get
            {
                return CultureInfo.GetCultures(CultureTypes.AllCultures)
                    .Where(c => !c.IsNeutralCulture)
                    .Select(culture =>
                    {
                        try
                        {
                            return new RegionInfo(culture.LCID);
                        }
                        catch
                        {
                            return null;
                        }
                    })
                    .Where(ri => ri != null && ri.ISOCurrencySymbol == AppSettings.DefaultCurrency)
                    .Select(ri => ri.CurrencySymbol)
                    .FirstOrDefault();
            }
        }

        #endregion AffiliateSettings

        private static AffiliateSettings AffiliateSettings
        {
            get
            {
                if (CacheInstance.Contains(AffiliateSettingsCacheKey))
                {
                    return CacheInstance.Get<AffiliateSettings>(AffiliateSettingsCacheKey);
                }

                try
                {
                    var affiliateSettings = LoadAffiliateSettings();
                    CacheInstance.Set(AffiliateSettingsCacheKey, affiliateSettings, CacheDuration);

                    return affiliateSettings;
                }
                catch (Exception exception)
                {
                    LogService.Error("Error when call LoadAffiliateSettings" + exception.Message, exception);

                    throw;
                }
            }
        }

        #region Private Methods

        /// <summary>
        /// Get key value.
        /// </summary>
        /// <param name="key">Input key.</param>
        /// <returns>Return value.</returns>
        private static string GetValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        /// <summary>
        /// Get Common Affiliate Setting String.
        /// </summary>
        /// <param name="commonSetting">Common Setting.</param>
        /// <param name="key">The key name.</param>
        /// <returns>String value.</returns>
        private static string GetCommonAffiliateSettingString(IList<SystemSetting> commonSetting, string key)
        {
            try
            {
                return commonSetting.First(x => x.SName == key).SValue;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Get Common Affiliate Setting Date.
        /// </summary>
        /// <param name="commonSetting">Common Setting.</param>
        /// <param name="key">The key name.</param>
        /// <returns>Date time.</returns>
        private static DateTime GetCommonAffiliateSettingDate(IList<SystemSetting> commonSetting, string key)
        {
            try
            {
                var value = commonSetting.First(x => x.SName == key).SValue;
                return DateTime.Parse(value);
            }
            catch (Exception exception)
            {
                throw new Exception("Invalid key " + key, exception);
            }
        }

        /// <summary>
        /// Get Common Affiliate Setting Date.
        /// </summary>
        /// <param name="commonSetting">Common Setting.</param>
        /// <param name="key">The key name.</param>
        /// <returns>Date time.</returns>
        private static int GetCommonAffiliateSettingInteger(IList<SystemSetting> commonSetting, string key)
        {
            try
            {
                var value = commonSetting.First(x => x.SName == key).SValue;
                return int.Parse(value);
            }
            catch (Exception exception)
            {
                throw new Exception("Invalid key " + key, exception);
            }
        }

        /// <summary>
        /// Parse to affiliate settings.
        /// </summary>
        /// <returns>Affiliate Settings.</returns>
        private static AffiliateSettings LoadAffiliateSettings()
        {
            var affiliateSettings = new AffiliateSettings();

            var allSystemSettings = SystemSetting.Value.GetAllSystemSettings(new SystemSettingsCriteria
            {
                Environment = GetValue("Environment")
            });

            var commonSetting = allSystemSettings.Tables[0].AsEnumerable().Select(
                                    row => new SystemSetting
                                    {
                                        SName = row["SName"].ToString(),
                                        SValue = row["SValue"].ToString(),
                                        Environment = row["Environment"].ToString()
                                    }).ToList();

            // common settings for whole system
            affiliateSettings.CommonSettings = commonSetting;
            affiliateSettings.SystemStartDate = GetCommonAffiliateSettingDate(commonSetting, "SystemStartDate");
            affiliateSettings.BannerDomain = GetCommonAffiliateSettingString(commonSetting, "BannerDomain");
            affiliateSettings.LicenseeID = GetCommonAffiliateSettingInteger(commonSetting, "LicenseeID");
            affiliateSettings.SystemName = GetCommonAffiliateSettingString(commonSetting, "SystemName");
            affiliateSettings.SystemDisplayName = string.IsNullOrEmpty(GetCommonAffiliateSettingString(commonSetting, "SystemDisplayName"))
                                            ? GetCommonAffiliateSettingString(commonSetting, "SystemName")
                                            : GetCommonAffiliateSettingString(commonSetting, "SystemDisplayName");
            affiliateSettings.BannerScriptUrl = new Uri(GetCommonAffiliateSettingString(commonSetting, "BannerScriptUrl"));
            affiliateSettings.BannerDirectory = GetCommonAffiliateSettingString(commonSetting, "BannerDirectory");
            affiliateSettings.AffiliateSupportedLanguages = GetCommonAffiliateSettingString(commonSetting, "AFF_AffiliateSupportedLanguages");
            affiliateSettings.AffiliateBaseCurrencyList = GetCommonAffiliateSettingString(commonSetting, "AffiliateCurrencies");
            affiliateSettings.MinimumWithdrawalAmount = GetCommonAffiliateSettingString(commonSetting, "AFF_MinPlayer" + GetValue("DefaultCurrency"));
            affiliateSettings.CompanyNegativeNetWin = GetCommonAffiliateSettingString(commonSetting, "AFF_MinimumNegativeCommission");

            // get system languages
            GetSystemLanguages(affiliateSettings, allSystemSettings);

            // get system currencies
            GetSystemCurrencies(affiliateSettings, allSystemSettings);

            // multiplied by values
            affiliateSettings.AffiliateCurrencyListMultipliedBy = allSystemSettings.Tables[8].Rows.Count > 0
                ? allSystemSettings.Tables[8].Rows[0]["CurrencyMultipliedBy"].ToString()
                : string.Empty;
            affiliateSettings.MulitpliedBy = allSystemSettings.Tables[8].Rows.Count > 0
                ? allSystemSettings.Tables[8].Rows[0]["MultipliedBy"].ToString()
                : string.Empty;

            return affiliateSettings;
        }

        /// <summary>
        /// Get system languages.
        /// </summary>
        /// <param name="affiliateSettings">Affiliate system settings.</param>
        /// <param name="allSystemSettings">Source data set.</param>
        private static void GetSystemLanguages(AffiliateSettings affiliateSettings, DataSet allSystemSettings)
        {
            affiliateSettings.AllLanguage = allSystemSettings.Tables[1].AsEnumerable().Select(
                                            row => new Language
                                            {
                                                LanguageId = row["LanguageID"].ToString(),
                                                LanguageName = row["LanguageName"].ToString()
                                            });

            affiliateSettings.BaseLanguage = allSystemSettings.Tables[2].AsEnumerable().Select(
                                             row => new Language
                                             {
                                                 LanguageId = row["LanguageID"].ToString(),
                                                 LanguageName = row["LanguageName"].ToString()
                                             });
        }

        /// <summary>
        /// Get system currencies.
        /// </summary>
        /// <param name="affiliateSettings">Affiliate system settings.</param>
        /// <param name="allSystemSettings">Source data set.</param>
        private static void GetSystemCurrencies(AffiliateSettings affiliateSettings, DataSet allSystemSettings)
        {
            affiliateSettings.AllCurrencies = allSystemSettings.Tables[3].AsEnumerable().Select(
                                               row => new Exchange
                                               {
                                                   ExchangeID = Convert.ToInt16(row["ExchangeID"].ToString()),
                                                   Code = row["Code"].ToString()
                                               });

            affiliateSettings.AffCurrencies = allSystemSettings.Tables[4].AsEnumerable().Select(
                                               row => new Exchange
                                               {
                                                   ExchangeID = Convert.ToInt16(row["ExchangeID"].ToString()),
                                                   Code = row["Code"].ToString(),
                                                   Prefix = row["Prefix"].ToString()
                                               });

            affiliateSettings.BaseCurrencies = allSystemSettings.Tables[5].AsEnumerable().Select(
                                                row => new Exchange
                                                {
                                                    ExchangeID = Convert.ToInt16(row["ExchangeID"].ToString()),
                                                    Code = row["Code"].ToString(),
                                                    Prefix = row["Prefix"].ToString()
                                                });

            affiliateSettings.PlayerCurrencies = allSystemSettings.Tables[6].AsEnumerable().Select(
                                                  row => new Currency
                                                  {
                                                      CurrencyCode = row["CurrencyCode"].ToString(),
                                                      CurrencyId = Convert.ToInt16(row["CurrencyID"].ToString())
                                                  });

            affiliateSettings.BaseCountries = allSystemSettings.Tables[7].AsEnumerable().Select(
                                               row => new Country
                                               {
                                                   CountryId = row["CountryID"].ToString(),
                                                   CountryName = row["CountryName"].ToString(),
                                                   Description = row["Description"].ToString(),
                                                   Icon = row["Icon"].ToString(),
                                                   DisplayOrder = Convert.ToInt16(row["DisplayOrder"].ToString()),
                                                   IsEnabled = Convert.ToBoolean(row["IsEnabled"].ToString())
                                               });
        }

        #endregion Private Methods
    }
}