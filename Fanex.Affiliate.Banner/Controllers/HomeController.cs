// ----------------------------------------------------------------------
// <copyright file="HomeController.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/11/01">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.Banner.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// Home controller of Affiliate Banner.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Main page of Affiliate Banner.
        /// </summary>
        /// <returns>Action Result.</returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}
