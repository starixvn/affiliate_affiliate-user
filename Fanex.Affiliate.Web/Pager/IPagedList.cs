namespace Fanex.Affiliate.Web.Pager
{
    using System.Collections.Generic;

    public interface IPagedList
    {
        int PageCount { get; }

        int TotalItemCount { get; }

        int PageIndex { get; }

        int PageNumber { get; }

        int PageSize { get; }

        bool HasPreviousPage { get; }

        bool HasNextPage { get; }

        bool IsFirstPage { get; }

        bool IsLastPage { get; }

        int ItemStart { get; }

        int ItemEnd { get; }
    }

    /// <summary>
    /// Paging function for object T.
    /// </summary>
    /// <typeparam name="T">Type parameter.</typeparam>
    public interface IPagedList<out T> : IPagedList, IEnumerable<T>
    {
    }
}