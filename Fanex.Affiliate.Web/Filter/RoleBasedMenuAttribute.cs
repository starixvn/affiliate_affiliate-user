namespace Fanex.Affiliate.Web.Filter
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Configurations;
    using DTO;
    using Fanex.Affiliate.Helpers;

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
                        filterContext.HttpContext.Response.ClearHeaders();
                        filterContext.HttpContext.Response.BufferOutput = true;
                        filterContext.HttpContext.Response.Redirect(VirtualPathUtility.ToAbsolute("~/notify/accessdenied"));

                        filterContext.Controller.ViewData.ModelState.AddModelError("ErrAuthorized", "Unauthorized.");
                    }
                }
            }
            catch (Exception exception)
            {

                LogHelper.WriteError(exception); ;
                throw;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
