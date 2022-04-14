namespace Fanex.Affiliate.Banner
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Fanex.Data;
    using Fanex.Data.SqlServer;
    using Logging;

    /// <summary>
    /// MVC Application.
    /// </summary>
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            LoggingConfig.RegisterSentry();
            DbSettingProviderManager
                .StartNewSession()
                .WithSqlServer("~/Configurations/SpNames/SpNames.xml")
                .Run();
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
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

            //var message = exception.Message + Environment.NewLine + exception.StackTrace;
            //LogService.Error("Affiliate Banner Error!" + Environment.NewLine + message + Environment.NewLine + Request.Url);
        }
    }
}