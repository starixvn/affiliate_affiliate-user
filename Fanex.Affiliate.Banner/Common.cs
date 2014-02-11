// ----------------------------------------------------------------------
// <copyright file="Common.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/09/23">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.Banner
{
    using System;
    using System.Configuration;
    using System.Web;
    using Microsoft.Practices.EnterpriseLibrary.Caching;
    using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;

    /// <summary>
    /// Common class for affiliate banner.
    /// </summary>
    public static class Common
    {
        public static string FullDomainName
        {
            get
            {
                var variables = HttpContext.Current.Request.ServerVariables;

                var protocol = HttpContext.Current.Request.IsSecureConnection ? "https://" : "http://";
                var domain = variables["SERVER_NAME"];
                var port = variables["SERVER_PORT"];
                var fulldomain = protocol + domain;
                if (!ConfigurationManager.AppSettings["Environment"].ToString().Equals("Production", StringComparison.OrdinalIgnoreCase))
                {
                    fulldomain = fulldomain + ":" + port;
                }
                    
                fulldomain = fulldomain + (HttpRuntime.AppDomainAppVirtualPath == "/" ? string.Empty : HttpRuntime.AppDomainAppVirtualPath);
                return fulldomain;
            }
        }

        public static bool CacheContainsException(string message)
        {
            var key = message.GetHashCode().ToString();
            if (CacheFactory.GetCacheManager().Contains(key))
            {
                return true;
            }

            CacheFactory.GetCacheManager().Add(key, null, CacheItemPriority.None, null, new AbsoluteTime(TimeSpan.FromDays(1)));
            return false;
        }
    }
}