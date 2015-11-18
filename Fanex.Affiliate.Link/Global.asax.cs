// ----------------------------------------------------------------------
// <copyright file="Global.asax.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/08/09">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.Link
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Fanex.Data.Configuration;
    using Fanex.Logging;

    /// <summary>
    /// Global for Affiliate link.
    /// </summary>
    public class MvcApplication : HttpApplication
    {
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "Reviewed. Suppression is OK here.")]
        protected void Application_Start()
        {
            DbSettingProviderManager.Current.Start();
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        /// <summary>
        /// Application Error.
        /// </summary>
        /// <param name="sender">Object sender.</param>
        /// <param name="e">Error arguments.</param>
        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            if (exception is HttpException)
            {
                return;
            }

            var message = exception.Message + Environment.NewLine + exception.StackTrace;
            LogService.Error("Affiliate Link Error!" + Environment.NewLine + message + Environment.NewLine + Request.Url);
       }
    }
}