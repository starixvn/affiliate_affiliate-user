// ----------------------------------------------------------------------
// <copyright file="PagerOptionsBuilder.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/08/20">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.Web.Pager
{
    using System;
    using System.Web.Mvc.Ajax;
    using System.Web.Routing;

    /// <summary>
    /// Pager Option Builder.
    /// </summary>
    public class PagerOptionsBuilder
    {
        /// <summary>
        /// Private Pager Options.
        /// </summary>
        private readonly PagerOptions _pagerOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="PagerOptionsBuilder"/> class.
        /// </summary>
        /// <param name="pagerOptions">Pager options.</param>
        public PagerOptionsBuilder(PagerOptions pagerOptions)
        {
            _pagerOptions = pagerOptions;
        }

        /// <summary>
        /// Set the action name for the pager links. Note that we're always using the current controller.
        /// </summary>
        /// <param name="action">Action string.</param>
        /// <returns>Pager Options Builder.</returns>
        public PagerOptionsBuilder Action(string action)
        {
            if (action != null)
            {
                if (_pagerOptions.RouteValues.ContainsKey("action"))
                {
                    throw new ArgumentException("The values Dictionary already contains an action.", "action");
                }

                _pagerOptions.RouteValues.Add("action", action);
                _pagerOptions.Action = action;
            }

            return this;
        }

        /// <summary>
        /// Add a custom route value parameter for the pager links.
        /// </summary>
        /// <param name="name">Name parameter.</param>
        /// <param name="value">Value parameter.</param>
        /// <returns>Pager Options Builder.</returns>
        public PagerOptionsBuilder AddRouteValue(string name, object value)
        {
            _pagerOptions.RouteValues[name] = value;
            return this;
        }

        /// <summary>
        /// Set custom route value parameters for the pager links.
        /// </summary>
        /// <param name="routeValues">Route values.</param>
        /// <returns>Pager Options Builder.</returns>
        public PagerOptionsBuilder RouteValues(object routeValues)
        {
            RouteValues(new RouteValueDictionary(routeValues));
            return this;
        }

        /// <summary>
        /// Set custom route value parameters for the pager links.
        /// </summary>
        /// <param name="routeValues">Route values.</param>
        /// <returns>Pager Options Builder.</returns>
        public PagerOptionsBuilder RouteValues(RouteValueDictionary routeValues)
        {
            if (routeValues == null)
            {
                throw new ArgumentException("Route Values may not be null", "routeValues");
            }

            _pagerOptions.RouteValues = routeValues;
            if (!string.IsNullOrWhiteSpace(_pagerOptions.Action) && !_pagerOptions.RouteValues.ContainsKey("action"))
            {
                _pagerOptions.RouteValues.Add("action", _pagerOptions.Action);
            }

            return this;
        }

        /// <summary>
        /// Set the name of the DisplayTemplate view to use for rendering.
        /// </summary>
        /// <param name="displayTemplate">Display template.</param>
        /// <remarks>The view must have a model of IEnumerable&lt;PaginationModel&gt;.</remarks>
        /// <returns>Pager Options Builder.</returns>
        public PagerOptionsBuilder DisplayTemplate(string displayTemplate)
        {
            _pagerOptions.DisplayTemplate = displayTemplate;
            return this;
        }

        /// <summary>
        /// Set the maximum number of pages to show. The default is 10.
        /// </summary>
        /// <param name="maxNrOfPages">Max number of pages.</param>
        /// <returns>Pager Options Builder.</returns>
        public PagerOptionsBuilder MaxNrOfPages(int maxNrOfPages)
        {
            _pagerOptions.MaxNrOfPages = maxNrOfPages;
            return this;
        }

        /// <summary>
        /// Always add the page number to the generated link for the first page.
        /// </summary>
        /// <remarks>
        /// By default we don't add the page number for page 1 because it results in canonical links.
        /// Use this option to override this behavior.
        /// </remarks>
        /// <returns>Pager Options Builder.</returns>
        public PagerOptionsBuilder AlwaysAddFirstPageNumber()
        {
            _pagerOptions.AlwaysAddFirstPageNumber = true;
            return this;
        }

        /// <summary>
        /// Set the page routeValue key for pagination links.
        /// </summary>
        /// <param name="pageRouteValueKey">Page route key value.</param>
        /// <returns>Pager Options Builder.</returns>
        public PagerOptionsBuilder PageRouteValueKey(string pageRouteValueKey)
        {
            if (pageRouteValueKey == null)
            {
                throw new ArgumentException("pageRouteValueKey may not be null", "pageRouteValueKey");
            }

            _pagerOptions.PageRouteValueKey = pageRouteValueKey;
            return this;
        }

        /// <summary>
        /// Set the AjaxOptions.
        /// </summary>
        /// <param name="ajaxOptions">Ajax options.</param>
        /// <returns>Pager Options Builder.</returns>
        internal PagerOptionsBuilder AjaxOptions(AjaxOptions ajaxOptions)
        {
            _pagerOptions.AjaxOptions = ajaxOptions;
            return this;
        }
    }
}