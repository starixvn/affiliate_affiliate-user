namespace Fanex.Affiliate.Site
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Fanex.Affiliate.Configurations;

    /// <summary>
    /// Route Config.
    /// </summary>
    public static class RouteConfig
    {
        /// <summary>
        /// Register Routes.
        /// </summary>
        /// <param name="routes">Route Collection.</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            var controllerAssembly = "Fanex.Affiliate.Controllers." + AppSettings.AffiliateName.ToLower();
            var controllerCounter = Assembly.Load("Fanex.Affiliate.Controllers").GetTypes()
                .Where(t => t.FullName.ToLower().Contains(controllerAssembly.ToLower())).Count();
            if (controllerCounter <= 0)
            {
                controllerAssembly = "Fanex.Affiliate.Controllers";
            }

            routes.MapRoute(
                    name: "Default",
                    url: "{controller}/{action}/{id}",
                    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                    namespaces: new string[] { controllerAssembly });
        }
    }
}