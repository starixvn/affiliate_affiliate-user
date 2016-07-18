namespace Fanex.Affiliate.DTO
{
    using System;
    using System.Globalization;
    using Resources;

    /// <summary>
    /// Utility data object.
    /// </summary>
    public static class Utility
    {
        /// <summary>
        /// Convert to date time.
        /// </summary>
        /// <param name="dtmTime">Date time parameter.</param>
        /// <param name="slash">Slash parameter.</param>
        /// <param name="checkFullDate">Is full date or not.</param>
        /// <param name="hasTime">Has time or not.</param>
        /// <returns>Date time string.</returns>
        public static string ConvertDateTime(object dtmTime, string slash, bool checkFullDate, bool hasTime)
        {
            var culture = new CultureInfo("en-US");
            var date = Convert.ToDateTime(dtmTime, culture);
            if (checkFullDate)
            {
                var resultString = date.ToString("MM" + slash + "dd" + slash) + date.ToString("yyyy", culture);
                return resultString + (hasTime ? " " + date.ToString("HH:mm:ss") : string.Empty);
            }
            else
            {
                return date.ToString("MMMM" + slash, culture) + date.ToString("yyyy", culture);
            }
        }

        /// <summary>
        /// Get status name.
        /// </summary>
        /// <param name="status">Status parameter.</param>
        /// <returns>Status string.</returns>
        public static string Status(string status)
        {
            switch (status.ToLower())
            {
                case "running":
                case "pending":
                    return Lang.Proccessing;
                case "completed":
                    return Lang.Approved;
                case "cancelled":
                    return Lang.Cancelled;
                case "approved":
                    return Lang.Approved;
                case "rejected":
                    return Lang.Rejected;
                case "current":
                    return Lang.Current;
                default:
                    return status;
            }
        }
    }
}