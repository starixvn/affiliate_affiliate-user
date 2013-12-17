// ----------------------------------------------------------------------
// <copyright file="Pager.cs" company="Nexcel Solutions Vietnam">
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
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;
    using System.Web.Routing;
    using Resources;
    
    /// <summary>
    /// Pager class.
    /// </summary>
    public class Pager : IHtmlString
    {
        private readonly HtmlHelper _htmlHelper;

        private readonly int _pageSize;

        private readonly int _currentPage;

        private readonly int _totalItemCount;

        private readonly PagerOptions _pagerOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="Pager"/> class.
        /// </summary>
        /// <param name="htmlHelper">Html helper.</param>
        /// <param name="pageSize">Page size.</param>
        /// <param name="currentPage">Current page.</param>
        /// <param name="totalItemCount">Total item count.</param>
        public Pager(HtmlHelper htmlHelper, int pageSize, int currentPage, int totalItemCount)
        {
            _htmlHelper = htmlHelper;
            _pageSize = pageSize;
            _currentPage = currentPage - 1;
            _totalItemCount = totalItemCount;
            _pagerOptions = new PagerOptions();
        }

        /// <summary>
        /// Pager Options.
        /// </summary>
        /// <param name="buildOptions">Build options.</param>
        /// <returns>Pager object.</returns>
        public Pager Options(Action<PagerOptionsBuilder> buildOptions)
        {
            buildOptions(new PagerOptionsBuilder(_pagerOptions));
            return this;
        }

        /// <summary>
        /// Build Page Model.
        /// </summary>
        /// <param name="generateUrl">Generate URL.</param>
        /// <returns>Pagination Model.</returns>
        public PaginationModel BuildPaginationModel(Func<int, string> generateUrl)
        {
            var model = new PaginationModel();

            var pageCount = (int)Math.Ceiling(_totalItemCount / (double)_pageSize);

            // Previous
            if (_currentPage == 1)
            {
                model.PaginationLinks.Add(new PaginationLink { Active = false, ClassName = "pagingFirst pagingDisable" });
                model.PaginationLinks.Add(new PaginationLink { Active = false, ClassName = "pagingPrev pagingDisable" });
            }
            else
            {
                model.PaginationLinks.Add(new PaginationLink { Active = true, ClassName = "pagingFirst" });
                model.PaginationLinks.Add(new PaginationLink { Active = true, ClassName = "pagingPrev" });
            }

            model.PaginationLinks.Add(
                new PaginationLink
                {
                    Active = true, 
                    IsCurrent = true, 
                    ClassName = _currentPage.ToString(), 
                    TotalItemCount = pageCount
                });

            if (_currentPage == pageCount)
            {
                model.PaginationLinks.Add(new PaginationLink { Active = false, ClassName = "pagingNext pagingDisable" });
                model.PaginationLinks.Add(new PaginationLink { Active = false, ClassName = "pagingLast pagingDisable" });
            }
            else
            {
                model.PaginationLinks.Add(new PaginationLink { Active = true, ClassName = "pagingNext" });
                model.PaginationLinks.Add(new PaginationLink { Active = true, ClassName = "pagingLast" });
            }

            return model;
        }

        /// <summary>
        /// Convert to Html String.
        /// </summary>
        /// <returns>Html string.</returns>
        public string ToHtmlString()
        {
            var model = BuildPaginationModel(GeneratePageUrl);

            if (!string.IsNullOrEmpty(_pagerOptions.DisplayTemplate))
            {
                var templatePath = string.Format("DisplayTemplates/{0}", _pagerOptions.DisplayTemplate);
                return _htmlHelper.Partial(templatePath, model).ToHtmlString();
            }
            else
            {
                var sb = new StringBuilder();

                foreach (var paginationLink in model.PaginationLinks)
                {
                    if (paginationLink.Active)
                    {
                        if (paginationLink.IsCurrent)
                        {
                            var currentPage = string.Format(
                                "<input type=\"text\" name=\"PageIndex\" class=\"pagingCurrent\" maxlength=\"4\" value=\"{0}\" total-item=\"{1}\">",
                                paginationLink.ClassName,
                                paginationLink.TotalItemCount);
                            sb.AppendFormat(string.Format(Lang.Paging, currentPage, paginationLink.TotalItemCount));
                        }
                        else
                        {
                            sb.AppendFormat("<input type=\"button\"  class='icon {0}'/>", paginationLink.ClassName);
                        }
                    }
                    else
                    {
                        sb.AppendFormat("<input type=\"button\"  class='icon {0}' disabled='disabled'/>", paginationLink.ClassName);
                    }
                }

                return sb.ToString();
            }
        }

        /// <summary>
        /// Generate Page URL.
        /// </summary>
        /// <param name="pageNumber">Page number.</param>
        /// <returns>Return Page URL.</returns>
        private string GeneratePageUrl(int pageNumber)
        {
            var viewContext = _htmlHelper.ViewContext;
            var routeDataValues = viewContext.RequestContext.RouteData.Values;
            RouteValueDictionary pageLinkValueDictionary;

            if (pageNumber == 1 && !_pagerOptions.AlwaysAddFirstPageNumber)
            {
                pageLinkValueDictionary = new RouteValueDictionary(_pagerOptions.RouteValues);
                if (routeDataValues.ContainsKey(_pagerOptions.PageRouteValueKey))
                {
                    routeDataValues.Remove(_pagerOptions.PageRouteValueKey);
                }
            }
            else
            {
                pageLinkValueDictionary = new RouteValueDictionary(_pagerOptions.RouteValues) { { _pagerOptions.PageRouteValueKey, pageNumber } };
            }

            if (!pageLinkValueDictionary.ContainsKey("controller") && routeDataValues.ContainsKey("controller"))
            {
                pageLinkValueDictionary.Add("controller", routeDataValues["controller"]);
            }

            if (!pageLinkValueDictionary.ContainsKey("action") && routeDataValues.ContainsKey("action"))
            {
                pageLinkValueDictionary.Add("action", routeDataValues["action"]);
            }

            var virtualPathForArea = RouteTable.Routes.GetVirtualPathForArea(_htmlHelper.ViewContext.RequestContext, pageLinkValueDictionary);
            var url = virtualPathForArea == null ? null : virtualPathForArea.VirtualPath;

            if (url != null)
            {
                var paramsList = HttpContext.Current.Request.QueryString;
                var checkExistedParam = url.IndexOf('?') != -1;
                foreach (string key in paramsList.Cast<string>().Where(key => key != PagerOptions.DefaultDefaults.DEFAULTPAGEROUTEVALUEKEY))
                {
                    if (checkExistedParam)
                    {
                        url += "&" + key + "=" + paramsList[key];
                    }
                    else
                    {
                        url += "?" + key + "=" + paramsList[key];
                    }
                }
            }

            return url;
        }
    }
}