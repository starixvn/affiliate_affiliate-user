namespace Fanex.Affiliate.Site
{
    using System;
    using System.Data.SqlClient;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using System.Web.Script.Serialization;
    using System.Web.Security;
    using CasinoSetting;
    using Configurations;
    using Data.Configuration;
    using Logging;
    using Web;
    using Web.Mvc;

    /// <summary>
    /// Application Global config.
    /// Note: For instructions on enabling IIS6 or IIS7 classic mode,
    /// visit http://go.microsoft.com/?LinkId=9394801.
    /// </summary>
    public class MvcApplication : HttpApplication
    {
        /// <summary>
        /// Application Start.
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Reviewed. Suppression is OK here.")]
        protected void Application_Start()
        {
            // Start setting connect to DB
            DbSettingProviderManager.Current.Start();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Register my types, for instance:
            SimpleInjectorInitializer.Initialize();

            // Customnize Razor ViewEngine
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new CustomRazorViewEngine());
            ViewEngines.Engines.Add(new RazorViewEngine());

            log4net.Config.XmlConfigurator.Configure();
            log4net.LogManager.GetLogger("GeneralLog").Info("Start");

            if (AppSettings.Environment.IndexOf("Local", StringComparison.OrdinalIgnoreCase) < 0 &&
                !AppSettings.Environment.Equals("SIT", StringComparison.OrdinalIgnoreCase))
            {
                SettingMgr.GetInstance().Initialize(SiteEnum.AgentSite);
            }
        }

        /// <summary>
        /// Application Error.
        /// </summary>
        /// <param name="sender">Object sender.</param>
        /// <param name="e">Error arguments.</param>
        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            var httpException = exception as HttpException;
            if (httpException != null && httpException.GetHttpCode() == 404)
            {
                return;
            }

            StringBuilder message = BuildErrorMessage(exception);

            LogService.Error(message.ToString());

            if (!AppSettings.IsTesting && Request.RequestContext.HttpContext.Request.IsAjaxRequest())
            {
                Context.ClearError();
                Response.ContentType = "application/json";
                Response.StatusCode = 500;
                Response.Write(new JavaScriptSerializer().Serialize(new
                {
                    Success = false,
                    Message = exception.Message,
                    ErrorCode = exception.GetHashCode(),
                    ErrorMessage = exception.Message
                }));
            }
        }

        /// <summary>
        /// Session Start.
        /// </summary>
        /// <param name="sender">Object sender.</param>
        /// <param name="e">Error arguments.</param>
        protected void Session_Start(object sender, EventArgs e)
        {
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                // old authentication, kill it
                FormsAuthentication.SignOut();

                // or use Response.Redirect to go to a different page
                FormsAuthentication.RedirectToLoginPage("Session=Expired");
                HttpContext.Current.Response.End();
            }
        }

        private StringBuilder BuildErrorMessage(Exception exception)
        {
            var message = new StringBuilder()
                            .AppendLine("Affiliate: " + AppSettings.AffiliateName)
                            .AppendLine("Error:" + exception.Message)
                            .AppendLine("Url: " + Request.RawUrl)
                            .AppendLine("UrlReferrer: " + Request.UrlReferrer)
                            .AppendLine("QUERYSTRING: " + Request.QueryString)
                            .AppendLine("FORM: " + string.Join("~", Request.Form))
                            .AppendLine("Browser:" + Request.Browser.Type.ToString())
                            .AppendLine("BrowserVer:" + Request.Browser.Version.ToString())
                            .AppendLine("Agent:" + Request.UserAgent.ToString());

            var currenSession = SessionManager.CurrentSession;
            if (currenSession != null)
            {
                message.AppendLine("AffId: " + currenSession.AffId)
                    .AppendLine("AffType: " + currenSession.AffType)
                    .AppendLine("IsInternalSite: " + currenSession.IsInternalSite)
                    .AppendLine("Username: " + currenSession.Username);
            }

            var sqlException = exception as SqlException;
            if (sqlException != null)
            {
                message.AppendLine("Procedure: " + sqlException.Procedure)
                    .AppendLine("Line Number: " + sqlException.LineNumber)
                    .AppendLine("Server: " + sqlException.Server);
            }

            message.AppendLine("StackTrace: " + exception.StackTrace);
            return message;
        }
    }
}