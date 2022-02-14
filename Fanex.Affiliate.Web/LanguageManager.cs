namespace Fanex.Affiliate.Web
{
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Resources;
    using System.Threading;
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
            if (_resourceManagerLang is object)
            {
                return _resourceManagerLang.GetString(key);
            }

            return string.Empty;
        }
    }
}