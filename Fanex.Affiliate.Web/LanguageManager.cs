// --------------------------------------------------------------------------
// <copyright file="LanguageManager.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.06.17">Created</change>
// </history>

namespace Fanex.Affiliate.Web
{
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Resources;
    using System.Threading;
    using Configurations;
    using Resources;

    /// <summary>
    /// Language Mapper.
    /// </summary>
    public class LanguageManager
    {
        /// <summary>
        /// Resource mapper language.
        /// </summary>
        private readonly ResourceManager _resourceManagerLang;

        /// <summary>
        /// Initializes a new instance of the <see cref="LanguageManager"/> class.
        /// </summary>
        public LanguageManager()
        {
            _resourceManagerLang = new ResourceManager(typeof(Lang)) { IgnoreCase = true };
        }

        /// <summary>
        /// Set Culture by Language code.
        /// </summary>
        /// <param name="language">Language parameter.</param>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Reviewed. Suppression is OK here.")]
        public void SetCulture(string language)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);

            Thread.CurrentThread.CurrentCulture.DateTimeFormat = CultureInfo.CreateSpecificCulture("en-US").DateTimeFormat;
            Thread.CurrentThread.CurrentCulture.NumberFormat = CultureInfo.CreateSpecificCulture("en-US").NumberFormat;
        }

        /// <summary>
        /// Get Text by Key.
        /// </summary>
        /// <param name="key">Key parameter.</param>
        /// <returns>Resource data.</returns>
        public string GetResourceData(string key)
        {
            if (!ReferenceEquals(_resourceManagerLang, null))
            {
                return _resourceManagerLang.GetString(key);
            }

            return string.Empty;
        }
    }
}