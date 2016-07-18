namespace Fanex.Affiliate.Utilities
{
    using System.Resources;
    using Resources;

    /// <summary>
    /// Format products.
    /// </summary>
    public static class ProductsFormatter
    {
        /// <summary>
        /// Globalization for Product's Names.
        /// </summary>
        /// <param name="value">Product names.</param>
        /// <param name="shouldTrimFirst">Option to trim product name before formatting.</param>
        /// <returns>Globalized product name.</returns>
        public static string Format(string value, bool shouldTrimFirst = true)
        {
            var globalizedProduct = shouldTrimFirst
                ? new ResourceManager(typeof(Lang)).GetString(value.Replace(" ", string.Empty))
                : new ResourceManager(typeof(Lang)).GetString(value);

            return string.IsNullOrEmpty(globalizedProduct) ? value : globalizedProduct;
        }
    }
}