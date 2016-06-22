namespace Fanex.Affiliate.Services
{
    using System;
    using System.Xml.Linq;

    /// <summary>
    /// Interface payment service.
    /// </summary>
    public interface IServiceWrapper
    {
        /// <summary>
        /// Get Register List.
        /// </summary>
        /// <param name="siteName">Site name.</param>
        /// <param name="targetDate">Target date.</param>
        /// <returns>Xml element.</returns>
        XElement GetRegList(string siteName, DateTime targetDate);

        /// <summary>
        /// Update B's tag of a link.
        /// </summary>
        /// <param name="siteName">Site name.</param>
        /// <param name="licenseeUserName">Licensee username.</param>
        /// <param name="newBtag">New B's tag.</param>
        /// <returns>Xml element.</returns>
        XElement UpdateBtage(string siteName, string licenseeUserName, string newBtag);

        /// <summary>
        /// Get Licensee Customer Info.
        /// </summary>
        /// <param name="siteName">Site name.</param>
        /// <param name="licenseeUserName">Licensee username.</param>
        /// <returns>Xml element.</returns>
        XElement GetLicenseeCustInfo(string siteName, string licenseeUserName);

        /// <summary>
        /// Transfer request.
        /// </summary>
        /// <param name="siteName">Site name.</param>
        /// <param name="licenseeUserName">Licensee username.</param>
        /// <param name="amount">Amount transfer.</param>
        /// <param name="dummyAccount">Account to transfer.</param>
        /// <returns>Xml element.</returns>
        XElement Transfer(string siteName, string licenseeUserName, double amount, int dummyAccount);
    }
}