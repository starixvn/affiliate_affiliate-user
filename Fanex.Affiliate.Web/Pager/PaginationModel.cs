// ----------------------------------------------------------------------
// <copyright file="PaginationModel.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/08/20">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.Web.Pager
{
    using System.Collections.Generic;
    using System.Web.Mvc.Ajax;

    /// <summary>
    /// Pagination Model.
    /// </summary>
    public class PaginationModel
    {
        public PaginationModel()
        {
            PaginationLinks = new List<PaginationLink>();
            AjaxOptions = null;
            Options = null;
        }

        public IList<PaginationLink> PaginationLinks { get; private set; }

        public AjaxOptions AjaxOptions { get; internal set; }

        public PagerOptions Options { get; internal set; }
    }
}