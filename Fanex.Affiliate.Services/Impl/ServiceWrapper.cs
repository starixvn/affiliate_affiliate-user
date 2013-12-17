// --------------------------------------------------------------------------
// <copyright file="ServiceWrapper.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.07.04">Created</change>
// </history>

namespace Fanex.Affiliate.Services.Impl
{
    using System;
    using System.Xml.Linq;
    using Fanex.Affiliate.Core.MemberSiteService;

    /// <summary>
    /// Service for getting info from Taiwan side's APIs.
    /// </summary>
    public class ServiceWrapper
    {
        private static ServiceSoapClient _client;

        private static ServiceSoapClient Client
        {
            get 
            {
                return _client ??
                       (_client = new ServiceSoapClient("ServiceSoap"));
            }
        }

        /// <summary>
        /// Get Register List.
        /// </summary>
        /// <param name="siteName">Site name.</param>
        /// <param name="targetDate">Target date.</param>
        /// <returns>Xml element.</returns>
        public static XElement GetRegList(string siteName, DateTime targetDate)
        {
            return Client.GetRegList(siteName, targetDate);
        }

        /// <summary>
        /// Update B's tag of a link.
        /// </summary>
        /// <param name="siteName">Site name.</param>
        /// <param name="licenseeUserName">Licensee username.</param>
        /// <param name="newBtag">New B's tag.</param>
        /// <returns>Xml element.</returns>
        public static XElement UpdateBtage(string siteName, string licenseeUserName, string newBtag)
        {
            return Client.UpdateBtage(siteName, licenseeUserName, newBtag);
        }

        /// <summary>
        /// Get Licensee Customer Info.
        /// </summary>
        /// <param name="siteName">Site name.</param>
        /// <param name="licenseeUserName">Licensee username.</param>
        /// <returns>Xml element.</returns>
        public static XElement GetLicenseeCustInfo(string siteName, string licenseeUserName)
        {
            return Client.GetLicenseeCustInfo(siteName, licenseeUserName);
        }

        /// <summary>
        /// Transfer request.
        /// </summary>
        /// <param name="siteName">Site name.</param>
        /// <param name="licenseeUserName">Licensee username.</param>
        /// <param name="amount">Amount transfer.</param>
        /// <param name="dummyAccount">Account to transfer.</param>
        /// <returns>Xml element.</returns>
        public static XElement Transfer(string siteName, string licenseeUserName, double amount, int dummyAccount)
        {
            return Client.Transfer(siteName, licenseeUserName, amount, dummyAccount);
        }
    }
}