// --------------------------------------------------------------------------
// <copyright file="UndermaintenanceAttribute.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.07.05">Created</change>
// </history>

namespace Fanex.Affiliate.Web.Filter
{
    using System.Web.Mvc;
    using System.Web.Routing;
    using Services;

    /// <summary>
    /// Attribute for under maintenance service.
    /// </summary>
    public sealed class UndermaintenanceAttribute : ActionFilterAttribute
    {
        private bool _isEnabled;

        public UndermaintenanceAttribute(bool isEnabled = true)
        {
            _isEnabled = isEnabled;
        }

        /// <summary>
        /// Action filter for under maintenance service.
        /// </summary>
        /// <param name="filterContext">Action Executing Context.</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (_isEnabled == false)
            {
                base.OnActionExecuting(filterContext);
                return;
            }

            string startTime;
            string endTime;

            if (UnderMaintenanceService.GetDownTime(out startTime, out endTime) && !filterContext.HttpContext.Response.IsRequestBeingRedirected)
            {
                var controllerName = filterContext.Controller.GetType().Name;
                var actionName = filterContext.ActionDescriptor.ActionName;

                var routeDictionary = new RouteValueDictionary(
                    new
                    {
                        controller = "Notify",
                        action = "Index"
                    });
                filterContext.Result = new RedirectToRouteResult(routeDictionary);
            }

            base.OnActionExecuting(filterContext);
        }
    }
}