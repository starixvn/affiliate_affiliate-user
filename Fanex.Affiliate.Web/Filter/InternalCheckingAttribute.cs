// ----------------------------------------------------------------------
// <copyright file="InternalCheckingAttribute.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/08/01">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.Web.Filter
{
    using System;
    using System.Web.Mvc;
    using Logging;

    /// <summary>
    /// Internal Checking Attribute.
    /// </summary>
    public class InternalCheckingAttribute : ActionFilterAttribute
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
                    filterContext.HttpContext.Response.BufferOutput = true;
                    filterContext.HttpContext.Response.Redirect("~/notify/accessdenied");

                    filterContext.Controller.ViewData.ModelState.AddModelError("ErrAuthorized", "Unauthorized.");
                }
            }
            catch (Exception exception)
            {
                Logger.Log.Error(exception.Message, exception);
                throw;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
