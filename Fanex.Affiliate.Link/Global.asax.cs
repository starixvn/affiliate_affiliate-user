namespace Fanex.Affiliate.Link
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Fanex.Data.Configuration;
    using Fanex.Logging;

    /// <summary>
    /// Global for Affiliate link.
    /// </summary>
    public class MvcApplication : HttpApplication
    {
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "Reviewed. Suppression is OK here.")]
        protected void Application_Start()
        {
            LoggingConfig.RegisterSentry();
            DbSettingProviderManager.Current.Start();
            AreaRegistration.RegisterAllAreas();
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

            string message = exception.Message +
                             "\r\nSOURCE: " + exception.Source +
                             "\r\nDOMAIN: " + Request.Headers["Host"] +
                             "\r\nFORM: " + string.Join("~", Request.Form) +
                             "\r\nUrl: " + Request.RawUrl +
                             "\r\nQUERYSTRING: " + string.Join("~", Request.QueryString) +
                             "\r\nTARGETSITE: " + exception.TargetSite +
                             "\r\nUrlReferrer: " + Request.UrlReferrer +
                             "\r\nSTACKTRACE: " + exception.StackTrace +
                             "\r\nBrowser:" + Request.Browser.Type.ToString() +
                             "\r\nBrowserVer:" + Request.Browser.Version.ToString() +
                             "\r\nAgent:" + Request.UserAgent.ToString();

            Logger.Log.Error("Affiliate Link Error!" + Environment.NewLine + message, exception);
        }
    }
}