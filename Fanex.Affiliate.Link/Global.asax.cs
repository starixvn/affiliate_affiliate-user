namespace Fanex.Affiliate.Link
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Fanex.Affiliate.Helpers;
    using Fanex.Data;
    using Fanex.Data.SqlServer;

    /// <summary>
    /// Global for Affiliate link.
    /// </summary>
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            LoggingConfig.RegisterSentry();

            UMConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            DbSettingProviderManager
                .StartNewSession()
                .WithSqlServer("~/Configurations/SpNames/SpNames.xml")
                .Run();
        }

        /// <summary>
        /// Application Error.
        /// </summary>
        /// <param name="sender">Object sender.</param>
        /// <param name="e">Error arguments.</param>
        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            if (exception is HttpException)
            {
                return;
            }

            LogHelper.WriteError(exception, $"Request url: {Request.Url}");
        }
    }
}