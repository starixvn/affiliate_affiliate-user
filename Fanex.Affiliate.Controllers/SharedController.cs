// --------------------------------------------------------------------------
// <copyright file="SharedController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Leo.Vo" date="2013.07.17">Created</change>
// </history>

namespace Fanex.Affiliate.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// Shared Controller.
    /// </summary>
    public class SharedController : BaseController
    {
        [ChildActionOnly]
        public ActionResult Footer()
        {
            var siteDomainInfo = GetSiteDomainInfo();
            return PartialView(siteDomainInfo);
        }
    }
}