namespace Fanex.Affiliate.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Logging;

    /// <summary>
    /// Format currency.
    /// </summary>
    public static class FormaterCurrency
    {
        public const string Decimalformat20 = "{0:#,##0.00}";

        private const string Minus = "<span style='color: red'>{0}</span>";

        private static CultureInfo _defaultCulture = new CultureInfo("en-US");

        private static Dictionary<string, CultureInfo> _symbolsByCode = new Dictionary<string, CultureInfo>();
        private static object _spinLock = new object();
        private static Dictionary<string, CultureInfo> SymbolsByCode
        {
            get
            {
                try
                {
                    lock (_spinLock)
                    {
                        if (!_symbolsByCode.Any())
                        {
                            var regions = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
                            foreach (var item in regions)
                            {
                                var key = new RegionInfo(item.LCID).ISOCurrencySymbol;
                                if (!_symbolsByCode.ContainsKey(key))
                                {
                                    item.NumberFormat.CurrencyDecimalDigits = 2;
                                    _symbolsByCode.Add(key, item);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log.Error(ex.Message, ex);
                }

                return _symbolsByCode;
            }
        }

        /// <summary>
        /// Get currency symbol from currency code.
        /// </summary>
        /// <param name="currencyCode">Currency code.</param>
        /// <returns>Currency symbol.</returns>
        public static string GetCurrencySymbol(string currencyCode = null)
        {
            return string.IsNullOrEmpty(currencyCode) || !SymbolsByCode.ContainsKey(currencyCode) ?
                _defaultCulture.NumberFormat.CurrencySymbol : SymbolsByCode[currencyCode]?.NumberFormat.CurrencySymbol;
        }

        /// <summary>
        /// Set default currency from a currency code.
        /// </summary>
        /// <param name="currencyCode">Currency code.</param>
        public static void SetDefaultCurrency(string currencyCode)
        {
            _defaultCulture = GetCulture(currencyCode);
        }

        /// <summary>
        /// Get culture from a currency code.
        /// </summary>
        /// <param name="currencyCode">Currency code.</param>
        /// <returns>Culture information.</returns>
        public static CultureInfo GetCulture(string currencyCode)
        {
            var realCurrencyCode = string.IsNullOrEmpty(currencyCode) ? "MYR" :
                string.Equals(currencyCode, "UUS", StringComparison.OrdinalIgnoreCase) ? "USD" : currencyCode;

            return SymbolsByCode.ContainsKey(realCurrencyCode) ? SymbolsByCode[realCurrencyCode] : _defaultCulture;
        }

        /// <summary>
        /// Format the value based on culture.
        /// </summary>
        /// <param name="value">Object value.</param>
        /// <param name="culture">Culture information.</param>
        /// <param name="checkMinus">Check minus.</param>
        /// <param name="hasCurrency">Has currency.</param>
        /// <returns>Formatted value.</returns>
        public static string Format(object value, CultureInfo culture, bool checkMinus, bool hasCurrency)
        {
            double d = 0;
            double.TryParse(value.ToString(), out d);

            // Check whether adding currency symbol or not
            var currencySymbol = !hasCurrency ? string.Empty
                : (culture.NumberFormat.CurrencySymbol ?? _defaultCulture.NumberFormat.CurrencySymbol);
            var newValue = string.Format("{0}{1}", currencySymbol, string.Format(Decimalformat20, value));

            // Format negative value: replace "-value" by "(value)"
            if (d < 0 && checkMinus)
            {
                if (newValue.Contains("-"))
                {
                    newValue = string.Format("({0})", newValue.Replace("-", string.Empty));
                }

                newValue = string.Format(Minus, newValue);
            }

            return newValue;
        }

        /// <summary>
        /// Format the value based on culture.
        /// </summary>
        /// <param name="value">Object value.</param>
        /// <param name="currencySymbol">Currency symbol.</param>
        /// <param name="checkMinus">Check minus.</param>
        /// <param name="hasCurrency">Has currency.</param>
        /// <returns>Formatted value.</returns>
        public static string Format(object value, string currencySymbol, bool checkMinus, bool hasCurrency)
        {
            return Format(value, GetCulture(currencySymbol), checkMinus, hasCurrency);
        }

        /// <summary>
        /// Format the value based on culture.
        /// </summary>
        /// <param name="value">Object value.</param>
        /// <param name="checkMinus">Check minus.</param>
        /// <param name="hasCurrency">Has currency.</param>
        /// <returns>Formatted value.</returns>
        public static string Format(object value, bool checkMinus = true, bool hasCurrency = false)
        {
            return Format(value, _defaultCulture, checkMinus, hasCurrency);
        }
    }
}