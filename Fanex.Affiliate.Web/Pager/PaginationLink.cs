// ----------------------------------------------------------------------
// <copyright file="PaginationLink.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/08/21">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.Web.Pager
{
    /// <summary>
    /// Pagination Link.
    /// </summary>
    public class PaginationLink
    {
        public bool Active { get; set; }

        public bool IsCurrent { get; set; }

        public int TotalItemCount { get; set; }

        public string ClassName { get; set; }
    }
}
