// --------------------------------------------------------------------------
// <copyright file="ProgramController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.05.29">Created</change>
// </history>

namespace Fanex.Affiliate.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// Program Controller.
    /// </summary>
    public class ProgramController : BaseController
    {
        /// <summary>
        /// Action program.
        /// </summary>
        /// <returns>Return view.</returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}