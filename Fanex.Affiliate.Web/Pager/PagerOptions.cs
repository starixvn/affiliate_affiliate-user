// ----------------------------------------------------------------------
// <copyright file="PagerOptions.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/08/20">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.Web.Pager
{
    using System.Web.Mvc.Ajax;
    using System.Web.Routing;

    /// <summary>
    /// Pager Options.
    /// </summary>
    public class PagerOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PagerOptions"/> class.
        /// </summary>
        public PagerOptions()
        {
            RouteValues = new RouteValueDictionary();
            DisplayTemplate = Defaults.DisplayTemplate;
            MaxNrOfPages = Defaults.MaxNrOfPages;
            AlwaysAddFirstPageNumber = Defaults.AlwaysAddFirstPageNumber;
            PageRouteValueKey = Defaults.DefaultPageRouteValueKey;
        }
        
        public RouteValueDictionary RouteValues { get; internal set; }

        public string DisplayTemplate { get; internal set; }

        public int MaxNrOfPages { get; internal set; }

        public AjaxOptions AjaxOptions { get; internal set; }

        public bool AlwaysAddFirstPageNumber { get; internal set; }

        public string Action { get; internal set; }

        public string PageRouteValueKey { get; set; }

        /// <summary>
        /// Default of Defaults.
        /// </summary>
        public static class DefaultDefaults
        {
            public const int MAXNROFPAGES = 3;

            public const string DISPLAYTEMPLATE = null;

            public const bool ALWAYSADDFIRSTPAGENUMBER = false;

            public const string DEFAULTPAGEROUTEVALUEKEY = "page";
        }

        /// <summary>
        /// The static Defaults class allows you to set Pager defaults for the entire application.
        /// Set values at application startup.
        /// </summary>
        public static class Defaults
        {
            private static int _maxNrOfPages;

            private static string _displayTemplate;

            private static bool _alwaysAddFirstPageNumber;

            private static string _defaultPageRouteValueKey;

            public static int MaxNrOfPages
            {
                get
                {
                    return _maxNrOfPages;
                }
            }

            public static string DisplayTemplate
            {
                get
                {
                    return _displayTemplate;
                }
            }

            public static bool AlwaysAddFirstPageNumber
            {
                get
                {
                    return _alwaysAddFirstPageNumber;
                }
            }

            public static string DefaultPageRouteValueKey
            {
                get
                {
                    return _defaultPageRouteValueKey;
                }
            }

            public static void Reset()
            {
                _maxNrOfPages = DefaultDefaults.MAXNROFPAGES;
                _displayTemplate = DefaultDefaults.DISPLAYTEMPLATE;
                _alwaysAddFirstPageNumber = DefaultDefaults.ALWAYSADDFIRSTPAGENUMBER;
                _defaultPageRouteValueKey = DefaultDefaults.DEFAULTPAGEROUTEVALUEKEY;
            }
        }
    }
}