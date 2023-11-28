namespace Fanex.Affiliate.Link
{
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// Route configuration.
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

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults:
                new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                });

            routes.MapRoute(
                name: "Link",
                url: "{affId}/{pageId}/{lang}/index.htm",
                defaults:
                new
                {
                    controller = "Home",
                    action = "Link",
                    affId = UrlParameter.Optional,
                    pageId = UrlParameter.Optional,
                    lang = UrlParameter.Optional
                });
        }
    }
}