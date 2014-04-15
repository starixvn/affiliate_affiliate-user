// ----------------------------------------------------------------------
// <copyright file="SiteDomainInfo.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/08/20">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.DTO
{
    using System;
    using System.Text.RegularExpressions;
    using System.Web.Mvc;

    /// <summary>
    /// Site Domain Info.
    /// </summary>
    [Serializable]
    public class SiteDomainInfo
    {
        public string AffiliateDomain { get; set; }

        public string RootDomain { get; set; }

        public string MemberSiteDomain { get; set; }

        public string MemberSiteDomainWithoutProtocolnPort { get; set; }

        public string MemberSiteDomainEmail { get; set; }

        /// <summary>
        /// Parse to the site domain information.
        /// </summary>
        /// <param name="controller">Controller object.</param>
        /// <returns>Site domain information.</returns>
        public static SiteDomainInfo Parse(Controller controller)
        {
            var siteUrl = controller.Request.Url;
            var regex = new Regex(@":\d+");
            var host = regex.Replace(siteUrl.Authority, string.Empty);
            var scheme = siteUrl.Scheme;
            
            var affiliateDomain = string.Format("{0}://{1}{2}", scheme, host, controller.Url.Content("~/"));
            var rootDomain = affiliateDomain.Substring(0, affiliateDomain.Length - 1);
            var memberSiteDomain = affiliateDomain;
            var memberSiteDomainWithoutProtocolnPort = host;
            var memberSiteDomainEmail = string.Format("www.{0}", host);

            if (siteUrl.HostNameType == UriHostNameType.Dns)
            {
                var words = host.Split('.');
                if (words.Length > 2)
                {
                    memberSiteDomainWithoutProtocolnPort = string.Format("{0}.{1}", words[1], words[2]);
                    memberSiteDomain = string.Format("{0}://www.{1}", scheme, memberSiteDomainWithoutProtocolnPort);
                    memberSiteDomainEmail = string.Format("www.{0}.{1}", words[1], words[2]);
                }
            }

            return new SiteDomainInfo
            {
                AffiliateDomain = affiliateDomain,
                RootDomain = rootDomain,
                MemberSiteDomain = memberSiteDomain,
                MemberSiteDomainWithoutProtocolnPort = memberSiteDomainWithoutProtocolnPort,
                MemberSiteDomainEmail = memberSiteDomainEmail
            };
        }
    }
}