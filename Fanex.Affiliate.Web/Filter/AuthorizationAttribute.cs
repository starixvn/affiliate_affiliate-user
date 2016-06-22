namespace Fanex.Affiliate.Web.Filter
{
    using System;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;
    using Logging;
    using Resources;

    /// <summary>
    /// Authorization attribute.
    /// </summary>
    public sealed class AuthorizationAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// On authorization.
        /// </summary>
        /// <param name="filterContext">Authorization Context.</param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            try
            {
                if (filterContext.HttpContext.User.Identity.IsAuthenticated == false ||
                    SessionManager.CurrentSession == null)
                {
                    DoUnAuthorized(filterContext);
                }
            }
            catch (Exception exception)
            {
                Logger.Log.Error(exception.Message, exception);
                throw;
            }
        }

        /// <summary>
        /// Do Un-authorized.
        /// </summary>
        /// <param name="filterContext">Authorization Context.</param>
        private static void DoUnAuthorized(AuthorizationContext filterContext)
        {
            var response = filterContext.HttpContext.Response;
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                response.Clear();
                response.StatusCode = (int)HttpStatusCode.Unauthorized;
                response.StatusDescription = string.Empty;
                response.Write(Lang.UnauthorizedMsg);
                response.End();
            }
            else
            {
                SessionManager.ClearAll();
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }
        }
    }
}