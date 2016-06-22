namespace Fanex.Affiliate.ViewModels
{
    using System.Collections.Generic;
    using DTO;

    /// <summary>
    /// Marketing View Model.
    /// </summary>
    public class MarketingViewModel
    {
        public IEnumerable<BannerType> CategoryList { get; set; }

        public IEnumerable<Currency> CurrencyList { get; set; }

        public IEnumerable<BannerType> TypeList { get; set; }

        public IEnumerable<BannerType> SizeList { get; set; }

        public IEnumerable<Domain> DomainList { get; set; }

        public IEnumerable<SystemSetting> LinkList { get; set; }

        public IEnumerable<Language> BaseLanguages { get; set; }

        public IEnumerable<Marketing> BannerList { get; set; }

        public short CategorySelected { get; set; }

        public short SubCategorySelected { get; set; }

        public string LanguageSelected { get; set; }

        public short CurrencySelected { get; set; }

        public short TypeSelected { get; set; }

        public short SizeSelected { get; set; }

        public string DomainSelected { get; set; }

        public string LinkSelected { get; set; }

        public int TotalPages { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int NumberRow { get; set; }

        public int CustomerID { get; set; }

        public string PreferLanguage { get; set; }
    }
}