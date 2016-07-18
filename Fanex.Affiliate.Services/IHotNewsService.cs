namespace Fanex.Affiliate.Services
{
    using Criteria;
    using ViewModels;

    /// <summary>
    /// Hot News Service.
    /// </summary>
    public interface IHotNewsService
    {
        HotNewsViewModel GetHotNews(HotNewsCriteria criteria);
    }
}
