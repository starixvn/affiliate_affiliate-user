namespace Fanex.Affiliate.Web.Pager
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;

    /// <summary>
    /// Paging Extensions.
    /// </summary>
    public static class PagingExtensions
    {
        #region HtmlHelper extensions

        /// <summary>
        /// Pager function.
        /// </summary>
        /// <param name="htmlHelper">Html helper.</param>
        /// <param name="pageSize">Page size.</param>
        /// <param name="currentPage">Current page.</param>
        /// <param name="totalItemCount">Total item count.</param>
        /// <returns>Pager object.</returns>
        public static Pager Pager(this HtmlHelper htmlHelper, int pageSize, int currentPage, int totalItemCount)
        {
            return new Pager(htmlHelper, pageSize, currentPage, totalItemCount);
        }

        /// <summary>
        /// Pager has Ajax Option.
        /// </summary>
        /// <param name="htmlHelper">Html helper.</param>
        /// <param name="pageSize">Page size.</param>
        /// <param name="currentPage">Current page.</param>
        /// <param name="totalItemCount">Total item count.</param>
        /// <param name="ajaxOptions">Ajax options.</param>
        /// <returns>Pager object.</returns>
        public static Pager Pager(this HtmlHelper htmlHelper, int pageSize, int currentPage, int totalItemCount, AjaxOptions ajaxOptions)
        {
            return new Pager(htmlHelper, pageSize, currentPage, totalItemCount).Options(o => o.AjaxOptions(ajaxOptions));
        }

        #endregion HtmlHelper extensions

        #region IQueryable<T> extensions

        /// <summary>
        /// To Paged List object.
        /// </summary>
        /// <typeparam name="T">I object parameter.</typeparam>
        /// <param name="source">Source parameter.</param>
        /// <param name="pageIndex">Page index.</param>
        /// <param name="pageSize">Page size.</param>
        /// <param name="totalCount">Total count.</param>
        /// <returns>Page list.</returns>
        public static IPagedList<T> ToPagedList<T>(this IQueryable<T> source, int pageIndex, int pageSize, int? totalCount = null)
        {
            return new PagedList<T>(source, pageIndex, pageSize, totalCount);
        }

        #endregion IQueryable<T> extensions

        #region IEnumerable<T> extensions

        /// <summary>
        /// To Paged List object.
        /// </summary>
        /// <typeparam name="T">I object parameter.</typeparam>
        /// <param name="source">Source parameter.</param>
        /// <param name="pageIndex">Page index.</param>
        /// <param name="pageSize">Page size.</param>
        /// <param name="totalCount">Total count.</param>
        /// <returns>Page list.</returns>
        public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> source, int pageIndex, int pageSize, int? totalCount = null)
        {
            return new PagedList<T>(source, pageIndex, pageSize, totalCount);
        }

        #endregion IEnumerable<T> extensions
    }
}