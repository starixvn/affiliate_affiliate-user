namespace Fanex.Affiliate.Utilities
{
    using System;
    using System.Web.Mvc;
    using Fanex.Affiliate.Configurations;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class UrlUtil
    {
        /// <summary>
        /// Gets the base URL.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <returns>Return URI.</returns>
        public static Uri GetBaseUrl(Controller controller)
        {
            var request = controller.Request;

            if (request == null || request.Url == null)
            {
                return null;
            }
            
            var subDomainPath = controller.Url.Content("~/");

            if (subDomainPath.EndsWith("/", StringComparison.OrdinalIgnoreCase))
            {
                subDomainPath = subDomainPath.Remove(subDomainPath.Length - 1);
            }

            return new Uri(request.Url.Scheme + Uri.SchemeDelimiter + request.Url.Host
                + ((request.Url.IsDefaultPort || AppSettings.Environment.Equals("Production", StringComparison.OrdinalIgnoreCase))
                ? string.Empty : ":" + request.Url.Port) + subDomainPath);
        }
    }
}
