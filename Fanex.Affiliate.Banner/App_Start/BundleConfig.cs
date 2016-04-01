// ----------------------------------------------------------------------
// <copyright file="BundleConfig.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/09/23">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.Banner
{
    using System.Web.Optimization;

    /// <summary>
    /// Bundle config class.
    /// </summary>
    public static class BundleConfig
    {
        /// <summary>
        /// Register bundles.
        /// </summary>
        /// <param name="bundles">Bundle Collection.</param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));
        }
    }
}