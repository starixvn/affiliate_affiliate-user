// --------------------------------------------------------------------------
// <copyright file="WebApiConfig.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.06.11">Created</change>
// </history>
namespace Fanex.Affiliate.Site
{
    using System.Web.Http;

    /// <summary>
    /// Web Config.
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Register config route.
        /// </summary>
        /// <param name="config">Http Configuration.</param>
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
        }
    }
}