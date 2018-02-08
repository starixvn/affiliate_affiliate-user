namespace Fanex.Affiliate.Site
{
    using System;
    using System.Data.SqlClient;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
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

    public class MvcApplication : HttpApplication
    {
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Reviewed. Suppression is OK here.")]
        protected void Application_Start()
        {
            DbSettingProviderManager.Current.Start();
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            SimpleInjectorInitializer.Initialize();

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

        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            var httpException = exception as HttpException;
            if (httpException != null && httpException.GetHttpCode() == 404)
            {
                return;
            }

            var message = BuildErrorMessage(exception);

            LogService.Error(message.ToString());

            if (!AppSettings.IsTesting && Request.RequestContext.HttpContext.Request.IsAjaxRequest())
            {
                Context.ClearError();
                Response.ContentType = "application/json";
                Response.StatusCode = 500;
                Response.Write(new JavaScriptSerializer().Serialize(new
                {
                    Success = false,
                    exception.Message,
                    ErrorCode = exception.GetHashCode(),
                    ErrorMessage = exception.Message
                }));
            }
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage("Session=Expired");
                HttpContext.Current.Response.End();
            }
        }

        private StringBuilder BuildErrorMessage(Exception exception)
        {
            var message = new StringBuilder()
                            .AppendLine($"Affiliate: {AppSettings.AffiliateName}")
                            .AppendLine($"Error:{exception.Message}")
                            .AppendLine($"Url: { Request.RawUrl}")
                            .AppendLine($"UrlReferrer: {Request.UrlReferrer} ")
                            .AppendLine($"QUERYSTRING: {Request.QueryString}")
                            .AppendLine($"FORM: {string.Join("~", Request.Form)}")
                            .AppendLine($"Browser:{Request.Browser.Type}")
                            .AppendLine($"BrowserVer:{Request.Browser.Version}")
                            .AppendLine($"Agent:{Request.UserAgent}")
                            .AppendLine($"Source:{exception.Source}")
                            .AppendLine($"TargetSite:{exception.TargetSite}");

            var currenSession = SessionManager.CurrentSession;
            if (currenSession != null)
            {
                message.AppendLine($"AffId: {currenSession.AffId}")
                    .AppendLine($"AffType: { currenSession.AffType}")
                    .AppendLine($"IsInternalSite: {currenSession.IsInternalSite}")
                    .AppendLine($"Username: {currenSession.Username}")
                    .AppendLine($"CurrencyID: {currenSession.CurrencyID}")
                    .AppendLine($"Currency: {currenSession.Currency}");
            }

            var sqlException = exception as SqlException;
            if (sqlException != null)
            {
                message.AppendLine($"Procedure: { sqlException.Procedure}")
                    .AppendLine($"Line Number: {sqlException.LineNumber}")
                    .AppendLine($"Server: {sqlException.Server}");
            }

            message.AppendLine($"StackTrace: {exception.StackTrace}");
            message.AppendLine($"InnerException: {exception.InnerException}");

            var cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            message.AppendLine($"CultureTypes: {CultureTypes.SpecificCultures}");
            message.AppendLine($"Currency Code: {string.Join(",", cultures.Select(x => new RegionInfo(x.LCID).ISOCurrencySymbol))}");
            return message;
        }
    }
}