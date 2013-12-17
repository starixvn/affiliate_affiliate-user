// --------------------------------------------------------------------------
// <copyright file="AccountController.Marketing.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.06.24">Created</change>
//   <change who="Muller.Nguyen" date="2013.06.28">Add action (POST,GET): MediaGallery, TextBox</change>
// </history>

namespace Fanex.Affiliate.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Web.Mvc;
    using Configurations;
    using Criterias;
    using DTO;
    using Utilities;
    using ViewModels;
    using Web;

    /// <summary>
    /// All action for Media Gallery and TextBox.
    /// </summary>
    public partial class AccountController
    {
        /// <summary>
        /// Get list Media Gallery.
        /// </summary>
        /// <param name="criteria">Marketing criteria.</param>
        /// <returns>Return view.</returns>
        [HttpGet]
        public ActionResult MediaGallery(MarketingCriteria criteria)
        {
            var totalPage = 0;
            var categoryList = GetProducts();

            var typeList = MarketingService.GetBannerTypeList(new BannerTypeCriteria { TypeName = "ImageType", Language = AppSession.Language });
            var sizeList = MarketingService.GetBannerTypeList(new BannerTypeCriteria { TypeName = "Size", Language = AppSession.Language });
            var domainList = MarketingService.GetDomainList(new DomainCriteria { AffID = CurrentSession.AffId });
            var linkList = SystemSettingService.ListSystemSetting(new SystemSettingCriteria { Type = "CustomerPage", IsType = true });
            criteria.PageSize = criteria.PageSize == 0 ? AppSettings.PageSize : criteria.PageSize;
            criteria.Type = (short)AppSettings.InputAffType;

            var bannerList = MarketingService.GetBannerMediaList(criteria, ref totalPage);
            var model = new MarketingViewModel
            {
                CategoryList = categoryList,
                CurrencyList = AppSettings.PlayerCurrencies,
                TypeList = typeList,
                SizeList = sizeList,
                DomainList = domainList,
                LinkList = linkList,
                BaseLanguages = AppSettings.BaseLanguages,
                PreferLanguage = CurrentSession.PreferredLanguage,
                CustomerID = CurrentSession.AffId,
                CategorySelected = (short)(!criteria.BannerCategory.HasValue ? 0 : criteria.BannerCategory),
                SubCategorySelected = (short)(!criteria.BannerSubCategory.HasValue ? 0 : criteria.BannerSubCategory),
                LanguageSelected = criteria.Language,
                CurrencySelected = (short)(!criteria.Currency.HasValue ? 0 : criteria.Currency),
                TypeSelected = (short)(!criteria.ImageType.HasValue ? 0 : criteria.ImageType),
                SizeSelected = (short)(!criteria.Size.HasValue ? 0 : criteria.Size),
                DomainSelected = criteria.Domain,
                LinkSelected = criteria.LinkTo,
                PageIndex = criteria.Page,
                PageSize = criteria.PageSize,
                TotalPages = totalPage,
                BannerList = bannerList
            };

            if (string.IsNullOrEmpty(model.DomainSelected) && domainList.GetEnumerator().MoveNext())
            {
                model.DomainSelected = ((List<Domain>)domainList)[0].DomainId;
            }

            if (string.IsNullOrEmpty(model.LinkSelected))
            {
                model.LinkSelected = AppSettings.DefaultTarget;
            }

            return View(model);
        }

        /// <summary>
        /// Get product list.
        /// </summary>
        /// <returns>A list of products.</returns>
        public IEnumerable<BannerType> GetProducts()
        {
            var categoryList = MarketingService.GetBannerTypeList(
                new BannerTypeCriteria { TypeName = "MediaCategory", Language = AppSession.Language });

            // Globalize Product's names
            foreach (var item in categoryList)
            {
                item.Value = ProductsFormatter.Format(item.Value);
            }

            return categoryList;
        }

        /// <summary>
        /// Get list TextBox.
        /// </summary>
        /// <param name="criteria">Marketing criteria.</param>
        /// <returns>Return view.</returns>
        [HttpGet]
        public ActionResult TextBox(MarketingCriteria criteria)
        {
            var totalPage = 0;
            var categoryList = MarketingService.GetBannerTypeList(
                new BannerTypeCriteria { TypeName = "MediaCategory", Language = AppSession.Language });
            foreach (var item in categoryList)
            {
                item.Value = ProductsFormatter.Format(item.Value);
            }

            var domainList = MarketingService.GetDomainList(new DomainCriteria { AffID = CurrentSession.AffId });
            var linkList = SystemSettingService.ListSystemSetting(new SystemSettingCriteria { Type = "CustomerPage", IsType = true });

            criteria.PageSize = criteria.PageSize == 0 ? AppSettings.PageSize : criteria.PageSize;

            var bannerList = MarketingService.GetBannerMediaList(criteria, ref totalPage);
            var model = new MarketingViewModel
            {
                CategoryList = categoryList,
                CurrencyList = AppSettings.PlayerCurrencies,
                DomainList = domainList,
                LinkList = linkList,
                BaseLanguages = AppSettings.BaseLanguages,
                PreferLanguage = CurrentSession.PreferredLanguage,
                CustomerID = CurrentSession.AffId,
                CategorySelected = (short)(!criteria.BannerCategory.HasValue ? 0 : criteria.BannerCategory),
                SubCategorySelected = (short)(!criteria.BannerSubCategory.HasValue ? 0 : criteria.BannerSubCategory),
                LanguageSelected = criteria.Language,
                CurrencySelected = (short)(!criteria.Currency.HasValue ? 0 : criteria.Currency),
                TypeSelected = (short)(!criteria.ImageType.HasValue ? 0 : criteria.ImageType),
                SizeSelected = (short)(!criteria.Size.HasValue ? 0 : criteria.Size),
                DomainSelected = criteria.Domain,
                LinkSelected = criteria.LinkTo,
                PageIndex = criteria.Page,
                PageSize = criteria.PageSize,
                TotalPages = totalPage,
                BannerList = bannerList
            };

            if (string.IsNullOrEmpty(model.DomainSelected) && domainList.GetEnumerator().MoveNext())
            {
                model.DomainSelected = ((List<Domain>)domainList)[0].DomainId;
            }

            if (string.IsNullOrEmpty(model.LinkSelected))
            {
                model.LinkSelected = AppSettings.DefaultTarget;
            }

            return View(model);
        }

        /// <summary>
        /// Marketing Category.
        /// </summary>
        /// <param name="criteria">Banner type criteria.</param>
        /// <returns>Return view.</returns>
        [HttpPost]
        public JsonResult MarketingSubCategory(BannerTypeCriteria criteria)
        {
            criteria.TypeName = "MediaSubCategory";
            criteria.Language = AppSession.Language;
            var subCategoryList = MarketingService.GetBannerTypeList(criteria);

            return new JsonResult { Data = subCategoryList };
        }
    }
}