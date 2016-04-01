// --------------------------------------------------------------------------
// <copyright file="FilterConfig.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.05.28">Created</change>
// </history>

namespace Fanex.Affiliate.Site
{
    using System.Web.Mvc;
    using Web.Filter;

    /// <summary>
    /// Filter Config.
    /// </summary>
    public static class FilterConfig
    {
        /// <summary>
        /// Register Global Filters.
        /// </summary>
        /// <param name="filters">Global Filter Collection.</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new UndermaintenanceAttribute());
        }
    }
}