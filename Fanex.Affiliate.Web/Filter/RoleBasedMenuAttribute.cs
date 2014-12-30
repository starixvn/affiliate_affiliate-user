// ----------------------------------------------------------------------
// <copyright file="RoleBasedMenuAttribute.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/10/29">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.Web.Filter
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Configurations;
    using DTO;
    using Logging;

    /// <summary>
    /// Role Based Menu Attribute.
    /// </summary>
    public sealed class RoleBasedMenuAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// On action executing.
        /// </summary>
        /// <param name="filterContext">Action Executing Context.</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                if (filterContext.ActionDescriptor.ControllerDescriptor.ControllerName == "Account")
                {
                    var userRole = AppSettings.IsKiosk ? "Kiosk" : "Normal";
                    var requestAction = filterContext.ActionDescriptor.ActionName.ToLower();
                    var itemCounter = MenuSettings.Menus().Where(
                        l => l.ActionName.ToLower() == requestAction && (l.Roles == "All" || l.Roles == userRole)).Count();

                    var menuItem = new MenuItem() { Roles = "Default" };
                    if (MenuSettings.Menus().Where(l => l.ActionName.ToLower() == requestAction).Count() == 0)
                    {
                        menuItem = new MenuItem() { Roles = "All" };
                    }
                    else if (itemCounter > 0)
                    {
                        menuItem = MenuSettings.Menus().Where(l => l.ActionName.ToLower() == requestAction && (l.Roles == "All" || l.Roles == userRole)).First();
                    }

                    if ((menuItem.Roles != "All" && menuItem.Roles != userRole) 
                        || (requestAction == "dailyearningsdetail" && !AppSettings.IsShowDetailDailyEarnings))
                    {
                        filterContext.HttpContext.Response.BufferOutput = true;
                        filterContext.HttpContext.Response.Redirect("~/notify/accessdenied");

                        filterContext.Controller.ViewData.ModelState.AddModelError("ErrAuthorized", "Unauthorized.");
                    }
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
