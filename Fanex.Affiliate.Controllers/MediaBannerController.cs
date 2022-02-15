namespace Fanex.Affiliate.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Configurations;
    using Criteria;
    using DTO;
    using ONEbook.UM.AspNetMVC.Filters;
    using Services;
    using Utilities;
    using ViewModels;
    using Web;
    using Web.Filter;

    [Authorization]
    [RoleBasedMenu]
    [CheckUMFilter]
    public class MediaBannerController : BaseController
    {
        private readonly ISystemSettingService _systemSettingService;

        private readonly IMarketingService _marketingService;

        public MediaBannerController(
            ISystemSettingService systemSettingService,
            IMarketingService marketingService)
        {
            _systemSettingService = systemSettingService;
            _marketingService = marketingService;
        }

        [HttpGet]
        public ActionResult BannerList(MarketingCriteria criteria)
        {
            var totalPage = 0;
            var categoryList = GetProducts();

            var typeList = _marketingService.GetBannerTypeList(new BannerTypeCriteria { TypeName = "ImageType", Language = AppSession.Language });
            var sizeList = _marketingService.GetBannerTypeList(new BannerTypeCriteria { TypeName = "Size", Language = AppSession.Language });
            var domainList = _marketingService.GetDomainList(new DomainCriteria { AffID = CurrentSession.AffId });
            var linkList = _systemSettingService.ListSystemSetting(new SystemSettingsByTypeCriteria { Type = "CustomerPage" });
            criteria.PageSize = criteria.PageSize == 0 ? AppSettings.PageSize : criteria.PageSize;
            criteria.Type = (short)AppSettings.InputAffType;
            AssignCurrencyData(criteria);

            var bannerList = _marketingService.GetBannerMediaList(criteria, ref totalPage);
            var viewModel = new MarketingViewModel
            {
                CategoryList = categoryList,
                TypeList = typeList,
                SizeList = sizeList,
                DomainList = domainList,
                LinkList = linkList,
                TotalPages = totalPage,
                BannerList = bannerList
            };

            AssignDataToModel(viewModel, criteria);
            if (string.IsNullOrEmpty(viewModel.DomainSelected) && domainList.GetEnumerator().MoveNext())
            {
                viewModel.DomainSelected = ((List<Domain>)domainList)[0].DomainId;
            }

            if (string.IsNullOrEmpty(viewModel.LinkSelected))
            {
                viewModel.LinkSelected = AppSettings.DefaultTarget;
            }

            return View(viewModel);
        }

        public IEnumerable<BannerType> GetProducts()
        {
            var categoryList = _marketingService.GetBannerTypeList(
                new BannerTypeCriteria { TypeName = "MediaCategory", Language = AppSession.Language });

            // Globalize Product's names
            foreach (var item in categoryList)
            {
                item.Value = ProductsFormatter.Format(item.Value);
            }

            return categoryList;
        }

        [HttpGet]
        public ActionResult TextList(MarketingCriteria criteria)
        {
            var totalPage = 0;
            var categoryList = _marketingService.GetBannerTypeList(
                new BannerTypeCriteria { TypeName = "MediaCategory", Language = AppSession.Language });
            foreach (var item in categoryList)
            {
                item.Value = ProductsFormatter.Format(item.Value);
            }

            var domainList = _marketingService.GetDomainList(new DomainCriteria { AffID = CurrentSession.AffId });
            var linkList = _systemSettingService.ListSystemSetting(new SystemSettingsByTypeCriteria { Type = "CustomerPage" });
            criteria.PageSize = criteria.PageSize == 0 ? AppSettings.PageSize : criteria.PageSize;
            AssignCurrencyData(criteria);

            var bannerList = _marketingService.GetBannerMediaList(criteria, ref totalPage);
            var viewModel = new MarketingViewModel
            {
                CategoryList = categoryList,
                DomainList = domainList,
                LinkList = linkList,
                TotalPages = totalPage,
                BannerList = bannerList
            };

            AssignDataToModel(viewModel, criteria);
            if (string.IsNullOrEmpty(viewModel.DomainSelected) && domainList.Any())
            {
                viewModel.DomainSelected = domainList.First().DomainId;
            }

            if (string.IsNullOrEmpty(viewModel.LinkSelected))
            {
                viewModel.LinkSelected = AppSettings.DefaultTarget;
            }

            return View(viewModel);
        }

        [HttpPost]
        public JsonResult SubCategory(BannerTypeCriteria criteria)
        {
            criteria.TypeName = "MediaSubCategory";
            criteria.Language = AppSession.Language;
            var subCategoryList = _marketingService.GetBannerTypeList(criteria);

            return new JsonResult { Data = subCategoryList };
        }

        private static void AssignCurrencyData(MarketingCriteria criteria)
        {
            var currencies = AppSettings.PlayerCurrencies;
            if (criteria.Currency == null && currencies.Count() == 1)
            {
                criteria.Currency = AppSettings.PlayerCurrencies.First().CurrencyId;
            }
        }

        private void AssignDataToModel(MarketingViewModel model, MarketingCriteria criteria)
        {
            model.CurrencyList = AppSettings.PlayerCurrencies;
            model.BaseLanguages = AppSettings.BaseLanguages;
            model.PreferLanguage = CurrentSession.PreferredLanguage;
            model.CustomerID = CurrentSession.AffId;
            model.CategorySelected = (short)(!criteria.BannerCategory.HasValue ? 0 : criteria.BannerCategory);
            model.SubCategorySelected = (short)(!criteria.BannerSubCategory.HasValue ? 0 : criteria.BannerSubCategory);
            model.LanguageSelected = criteria.Language;
            model.CurrencySelected = (short)(!criteria.Currency.HasValue ? 0 : criteria.Currency);
            model.TypeSelected = (short)(!criteria.ImageType.HasValue ? 0 : criteria.ImageType);
            model.SizeSelected = (short)(!criteria.Size.HasValue ? 0 : criteria.Size);
            model.DomainSelected = criteria.Domain;
            model.LinkSelected = criteria.LinkTo;
            model.PageIndex = criteria.Page;
            model.PageSize = criteria.PageSize;
        }
    }
}