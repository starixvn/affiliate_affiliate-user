// --------------------------------------------------------------------------
// <copyright file="BundleConfig.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.05.28">Created</change>
// </history>

namespace Fanex.Affiliate.Site
{   
    using System.Diagnostics.CodeAnalysis;
    using System.Web.Optimization;
    using BundleTransformer.Core.Orderers;
    using BundleTransformer.Core.Transformers;
    using Configurations;

    /// <summary>
    /// Bundle Config.
    /// </summary>
    public class BundleConfig
    {
        /// <summary>
        /// For more information on Bundling.
        /// </summary>
        /// <param name="bundles">Bundle Collection.</param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new Bundle("~/bundles/css")
            {
                Transforms = { new CssTransformer(), new CssMinify() },
                Orderer = new NullOrderer()
            }.Include("~/Content/themes/base/jquery-ui.css")
            .Include(string.Format("~/Content/betlist/betlist.css", AppSettings.Theme))
            .Include(string.Format("~/Content/betlist/betlist.core.css", AppSettings.Theme))
            .Include(string.Format("~/Content/betlist/systemparlay.detail.css", AppSettings.Theme))
            .Include(string.Format("~/Content/betlist/rngcasino.handhistory.css", AppSettings.Theme))
            .Include(string.Format("~/Content/betlist/fanex.result.css", AppSettings.Theme))
            .Include(string.Format("~/Content/themes/theme{0}/index.less", AppSettings.Theme)));

            bundles.Add(new ScriptBundle("~/bundles/headjs").Include(
                "~/Scripts/libraries/head.js"));

            bundles.Add(new ScriptBundle("~/bundles/js")
                .Include("~/Scripts/libraries/jquery-1.10.1.js")
                .Include("~/Scripts/libraries/jquery-ui-1.10.3.js")
                .Include("~/Scripts/libraries/jquery-ui-languages.js")
                .Include("~/Scripts/libraries/jquery.formattyping.js")
                .Include("~/Scripts/libraries/jquery.blockUI.js")
                .Include("~/Scripts/libraries/jquery.validate.js")
                .Include("~/Scripts/libraries/jquery.validate.unobtrusive.js")
                .Include("~/Scripts/site/Query.string.js")
                .Include("~/Scripts/site/Paging.js")
                .Include("~/Scripts/site/Global.js")
                .Include("~/Scripts/site/Scroller.js")
                .Include("~/Scripts/site/BetList.js")
                .Include("~/Scripts/site/RNGCasinoResults.js"));
        }
    }
}