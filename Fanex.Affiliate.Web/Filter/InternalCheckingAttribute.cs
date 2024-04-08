namespace Fanex.Affiliate.Web.Filter
{
    using System;
    using System.Web.Mvc;
    using Fanex.Affiliate.Helpers;

    /// <summary>
    /// Internal Checking Attribute.
    /// </summary>
    public sealed class InternalCheckingAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// On action executing.
        /// </summary>
        /// <param name="filterContext">Action Executing Context.</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                var currentSession = SessionManager.CurrentSession;
                if (currentSession != null && currentSession.IsInternalSite)
                {
                    filterContext.Result = RedirectHelper.RedirectToAccessDeniedRouteResult();
                }
            }
            catch (Exception exception)
            {
                LogHelper.WriteError(exception);
                throw;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
