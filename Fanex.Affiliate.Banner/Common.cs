namespace Fanex.Affiliate.Banner
{
    using System;
    using System.Configuration;
    using System.Web;

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
    }
}