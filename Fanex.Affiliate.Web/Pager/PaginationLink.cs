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
