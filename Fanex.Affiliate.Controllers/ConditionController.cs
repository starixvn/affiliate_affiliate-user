// --------------------------------------------------------------------------
// <copyright file="ConditionController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.05.29">Created</change>
// </history>

namespace Fanex.Affiliate.Controllers
{
    using System.Web.Mvc;
    using ONEbook.UM.Filters;

    /// <summary>
    /// Condition Controller.
    /// </summary>
    [CheckUMFilter]
    public class ConditionController : BaseController
    {
        /// <summary>
        /// Get Condition page.
        /// </summary>
        /// <returns>Return View.</returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}