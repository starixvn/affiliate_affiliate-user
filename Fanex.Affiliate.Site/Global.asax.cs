namespace Fanex.Affiliate.Site
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using System.Web.Script.Serialization;
    using System.Web.Security;
    using Configurations;
    using Data.SqlServer;
    using Fanex.Affiliate.Services.Helpers;
    using Fanex.Data;
    using Web.Mvc;

    public class MvcApplication : HttpApplication
    {
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
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            if (exception is HttpException httpException && httpException.GetHttpCode() == 404)
            {
                return;
            }

            LogWebHelper.WriteError(exception);

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