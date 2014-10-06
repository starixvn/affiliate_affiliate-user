// ----------------------------------------------------------------------
// <copyright file="BundleConfig.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2014/03/27">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.Site
{
    using System.Web.Optimization;
    using BundleTransformer.Core.Orderers;
    using BundleTransformer.Core.Transformers;
    using Configurations;

    /// <summary>
    /// Bundle Config.
    /// </summary>
    public static class BundleConfig
    {
        /// <summary>
        /// For more information on Bundling.
        /// </summary>
        /// <param name="bundles">Bundle Collection.</param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterComponents(bundles);
            RegisterCore(bundles);
        }

        /// <summary>
        /// Registers CORE bundles.
        /// </summary>
        /// <param name="bundles">The bundle collection.</param>
        private static void RegisterCore(BundleCollection bundles)
        {
            bundles.Add(new Bundle("~/css/core")
            {
                Transforms = { new CssTransformer(), new CssMinify() },
                Orderer = new NullOrderer()
            }
            .Include(string.Format("~/Content/themes/theme{0}/index.less", AppSettings.Theme)));

            bundles.Add(new ScriptBundle("~/js/core")
                .Include("~/Scripts/libraries/head.js")
                .Include("~/Scripts/site/query.string.js")
                .Include("~/Scripts/site/paging.js")
                .Include("~/Scripts/site/global.js")
                .Include("~/Scripts/site/scroller.js"));
        }

        #region Components

        /// <summary>
        /// Registers the JAVASCRIPT component bundles.
        /// </summary>
        /// <param name="bundles">The bundle collection.</param>
        private static void RegisterComponents(BundleCollection bundles)
        {
            // Jquery, Jquery-UI, Jquery Validation
            RegisterJquery(bundles);

            RegisterDateTimePicker(bundles);

            RegisterFunctions(bundles);
        }

        /// <summary>
        /// Registers the JQUERY.
        /// </summary>
        /// <param name="bundles">The bundles.</param>
        private static void RegisterJquery(BundleCollection bundles)
        {
            bundles.Add(new Bundle("~/css/jquery")
            {
                Transforms = { new CssTransformer(), new CssMinify() },
                Orderer = new NullOrderer()
            }
            .Include("~/Content/themes/base/jquery-ui.css"));

            bundles.Add(new ScriptBundle("~/js/jquery")
                .Include("~/Scripts/libraries/jquery-1.10.1.js")
                .Include("~/Scripts/libraries/jquery-ui-1.10.3.js")
                .Include("~/Scripts/libraries/jquery-ui-languages.js")
                .Include("~/Scripts/libraries/jquery.formattyping.js")
                .Include("~/Scripts/libraries/jquery.blockUI.js")
                .Include("~/Scripts/libraries/jquery.validate.js")
                .Include("~/Scripts/libraries/jquery.validate.unobtrusive.js")
                .Include("~/Scripts/libraries/cryptoJS.js"));
        }

        /// <summary>
        /// Registers the JQUERY.
        /// </summary>
        /// <param name="bundles">The bundles.</param>
        private static void RegisterDateTimePicker(BundleCollection bundles)
        {
            bundles.Add(new Bundle("~/css/datetimepicker")
            {
                Transforms = { new CssTransformer(), new CssMinify() },
                Orderer = new NullOrderer()
            }
            .Include("~/Content/themes/base/jquery.ui.datepicker.css")
            .Include("~/Content/themes/base/calendar.less"));
        }

        #endregion Components

        #region Functions

        /// <summary>
        /// Registers the Functions.
        /// </summary>
        /// <param name="bundles">The bundles.</param>
        private static void RegisterFunctions(BundleCollection bundles)
        {
            RegisterFunctionUser(bundles);
            RegisterFunctionAccount(bundles);
            RegisterFunctionBetList(bundles);
        }

        /// <summary>
        /// Registers Bet List.
        /// </summary>
        /// <param name="bundles">The bundles.</param>
        private static void RegisterFunctionAccount(BundleCollection bundles)
        {
            // Logged in account
            bundles.Add(new ScriptBundle("~/js/loggedinaccount")
                .Include("~/Scripts/site/loggedin-account.js"));

            // Change password
            bundles.Add(new Bundle("~/css/changepwd")
            {
                Transforms = { new CssTransformer(), new CssMinify() },
                Orderer = new NullOrderer()
            }
            .Include("~/Content/themes/base/jquery.ui.tooltip.css"));

            bundles.Add(new ScriptBundle("~/js/changepwd")
                .Include("~/Scripts/site/change-password.js"));

            // Daily earnings
            bundles.Add(new ScriptBundle("~/js/dailyearnings")
                .Include("~/Scripts/site/dailyearnings-detail.js"));

            // Landing pages
            bundles.Add(new ScriptBundle("~/js/landingpages")
                .Include("~/Scripts/site/landingpages.js"));

            // Media gallery
            bundles.Add(new ScriptBundle("~/js/mediagallery")
                .Include("~/Scripts/site/mediagallery.js"));

            // Payment request
            bundles.Add(new ScriptBundle("~/js/paymentrequest")
                .Include("~/Scripts/site/paymentrequest.js"));

            // Referrers statistics
            bundles.Add(new ScriptBundle("~/js/referrersstatistics")
                .Include("~/Scripts/site/referrersstatistics.js"));
        }

        /// <summary>
        /// Registers Bet List.
        /// </summary>
        /// <param name="bundles">The bundles.</param>
        private static void RegisterFunctionUser(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js/signup")
                .Include("~/Scripts/site/sign-up.js"));

            bundles.Add(new ScriptBundle("~/js/forgetpwd")
                .Include("~/Scripts/site/forget-password.js"));

            bundles.Add(new ScriptBundle("~/js/signin")
                .Include("~/Scripts/site/sign-in.js"));

            bundles.Add(new ScriptBundle("~/js/message")
                .Include("~/Scripts/site/message.js"));

            bundles.Add(new ScriptBundle("~/js/contactus")
                .Include("~/Scripts/site/contact-us.js"));
        }

        /// <summary>
        /// Registers Bet List.
        /// </summary>
        /// <param name="bundles">The bundles.</param>
        private static void RegisterFunctionBetList(BundleCollection bundles)
        {
            bundles.Add(new Bundle("~/css/betlist")
            {
                Transforms = { new CssTransformer(), new CssMinify() },
                Orderer = new NullOrderer()
            }
            .Include("~/Content/betlist/betlist.css")
            .Include("~/Content/betlist/betlist.core.css")
            .Include("~/Content/betlist/systemparlay.detail.css")
            .Include("~/Content/betlist/rngcasino.handhistory.css")
            .Include("~/Content/betlist/fanex.result.css"));

            bundles.Add(new ScriptBundle("~/js/betlist")
                .Include("~/Scripts/site/betlist.js"));

            bundles.Add(new ScriptBundle("~/js/betlistrngcasino")
                .Include("~/Scripts/site/rngcasino-results.js"));

            bundles.Add(new Bundle("~/css/systemparlaydetails")
            {
                Transforms = { new CssTransformer(), new CssMinify() },
                Orderer = new NullOrderer()
            }
            .Include("~/Content/betlist/global.content.css")
            .Include("~/Content/betlist/systemparlay.detail.css"));
        }

        #endregion Functions
    }
}