namespace Fanex.Affiliate.Site
{
    using System;
    using System.Collections.Generic;
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
    using Data.SqlServer;
    using Fanex.Affiliate.Web.Helpers;
    using Fanex.Data;
    using Logging;
    using Web;
    using Web.Mvc;

    public class MvcApplication : HttpApplication
    {
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Reviewed. Suppression is OK here.")]
        protected void Application_Start()
        {
            LoggingConfig.RegisterSentry();
            DbSettingProviderManager
                .StartNewSession()
                .WithSqlServer("~/Configurations/SpNames/SpName.xml")
                .Run();
            UMConfig.RegisterGlobalFilters(GlobalFilters.Filters);
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

            LogHelper.WriteError(exception);

            if (AppSettings.IsProduction && Request.RequestContext.HttpContext.Request.IsAjaxRequest())
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
    }
}