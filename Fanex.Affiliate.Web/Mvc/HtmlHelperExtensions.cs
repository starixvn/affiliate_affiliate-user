// --------------------------------------------------------------------------
// <copyright file="HtmlHelperExtensions.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.05.28">Created</change>
//   <change who="Kaden.Trinh" date="2013.06.14">Add new extension DropDownListFor</change>
// </history>

namespace Fanex.Affiliate.Web.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;

    /// <summary>
    /// Implement html extension.
    /// </summary>
    public static class HtmlHelperExtensions
    {
        #region Dropdownlist

        /// <summary>
        /// Extension to Generate dropdown list.
        /// </summary>
        /// <typeparam name="TModel">T model parameter.</typeparam>
        /// <typeparam name="TProperty">T property parameter.</typeparam>
        /// <param name="htmlHelper">Html helper.</param>
        /// <param name="expression">Expression parameter.</param>
        /// <param name="selectList">Select list.</param>
        /// <param name="firstItem">First item.</param>
        /// <param name="htmlAttributes">Html attributes.</param>
        /// <returns>MVC Html String.</returns>
        public static MvcHtmlString DropDownListFor<TModel, TProperty>(
           this HtmlHelper<TModel> htmlHelper,
           Expression<Func<TModel, TProperty>> expression,
           IEnumerable<SelectListItem> selectList,
           SelectListItem firstItem,
           object htmlAttributes)
        {
            if (firstItem != null)
            {
                if (firstItem.Value != null)
                {
                    var list = new List<SelectListItem> { firstItem };

                    if (selectList != null)
                    {
                        list.AddRange(selectList);
                    }

                    selectList = list;
                }
            }

            return SelectExtensions.DropDownListFor(htmlHelper, expression, selectList, htmlAttributes);
        }

        #endregion Dropdownlist
        
        /// <summary>
        /// Generate external link.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="urlString">The URL parameter.</param>
        /// <param name="linkText">The link text.</param>
        /// <returns>MVC Html String.</returns>
        public static MvcHtmlString Hyperlink(this HtmlHelper helper, string urlString, string linkText)
        {
            return MvcHtmlString.Create(string.Format("<a href='{0}' target='_blank'>{1}</a>", urlString, linkText));
        }

        /// <summary>
        /// Generate external link.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="urlString">The URL parameter.</param>
        /// <returns>MVC Html String.</returns>
        public static MvcHtmlString Hyperlink(this HtmlHelper helper, string urlString)
        {
            return Hyperlink(helper, urlString, urlString);
        }

        public static MvcHtmlString Truncate(this HtmlHelper helper, string source, int maxLength = 120)
        {
            if (source.Length > maxLength)
            {
                return MvcHtmlString.Create(string.Format("{0}...", source.Substring(0, maxLength - 3)));
            }

            return MvcHtmlString.Create(source);
        }
    }
}