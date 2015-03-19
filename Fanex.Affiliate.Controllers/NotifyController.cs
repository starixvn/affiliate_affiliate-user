// ----------------------------------------------------------------------
// <copyright file="NotifyController.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/08/19">Created and add Properties</change>
// </history>

namespace Fanex.Affiliate.Controllers
{
    using System.Web.Mvc;
    using Configurations;
    using Services;

    /// <summary>
    /// Notify Controller.
    /// </summary>
    public class NotifyController : Controller
    {
        /// <summary>
        /// Action Notify.
        /// </summary>
        /// <returns>Return view.</returns>
        [HttpGet]
        public ActionResult Index()
        {
            string startTime;
            string endTime;

            if (!UnderMaintenanceService.GetDownTime(out startTime, out endTime))
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.StartTime = startTime;
            ViewBag.EndTime = endTime;
            ViewBag.AffCustomerServiceUrl = AppSettings.AffCustomerServiceUrl;
            ViewBag.ContactEmail = string.Format(
                "mailto:{0}?subject=Enquiries%20sent%20from%20{1}%20website", 
                AppSettings.MailContact,
                AppSettings.SystemDisplayName);

            return View();
        }

        /// <summary>
        /// Action Error page.
        /// </summary>
        /// <returns>Return view.</returns>
        [HttpGet]
        public ActionResult Error()
        {
            return View();
        }

        /// <summary>
        /// Action page not found.
        /// </summary>
        /// <returns>Return view.</returns>
        [HttpGet]
        public ActionResult PageNotFound()
        {
            return View();
        }

        /// <summary>
        /// Action access denied.
        /// </summary>
        /// <returns>Return view.</returns>
        [HttpGet]
        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}