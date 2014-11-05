// ----------------------------------------------------------------------
// <copyright file="AccountController.BetList.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/12/03">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Web.Mvc;
    using System.Web.Script.Serialization;
    using Configurations;
    using Criteria;
    using Enumerations;
    using Fanex.Affiliate.DTO;
    using Fanex.Affiliate.WebServices.Criteria;
    using Fanex.Affiliate.WebServices.DTO.BetList;
    using Fanex.Affiliate.WebServices.ViewModels;
    using Fanex.BetList.Core.Builder;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.TicketHelper;
    using Utilities;
    using ViewModels;
    using Web;
    using Web.Filter;

    /// <summary>
    /// All action after login.
    /// </summary>
    [Authorization]
    public partial class AccountController
    {
        #region BetList

        /// <summary>
        /// View bet list.
        /// </summary>
        /// <param name="username">User name value.</param>
        /// /// <param name="product">Product value.</param>
        /// /// <param name="winlostdate">Win loss date value.</param>
        /// /// <param name="currency">Currency value.</param>
        /// <returns>Result value.</returns>
        [HttpGet]
        public ActionResult BetList(string username, string product, string winlostdate, string currency)
        {
            DateTime winlostDate;
            DateTime.TryParse(winlostdate, out winlostDate);
            var customerInfoBetList = BetListService.GetPlayerIbcInfo(AppSettings.AffiliateName + "$" + username);
            var exchangeRate = GetExchangeRate(currency, winlostDate);
            CurrentSession.ExchangeRate = exchangeRate;
            var supportedProducts = GetSupportedProductsForWebService();

            var betlistSOAP = new AffBetlistService.BetlistSOAP
            {
                AffiliateName = AppSettings.AffiliateName.ToLower(),
                Username = CurrentSession.Username,
                Product = product,
                FromDate = winlostDate,
                ToDate = winlostDate,
                CurrencyName = CurrentSession.Currency,
                CurrencyId = int.Parse(currency.Trim()),
                TargetCurrencyId = CurrentSession.CurrencyID,
                ExchangeRate = exchangeRate,
                SupportedProducts = supportedProducts
            };

            var affBetlistService = new AffBetlistService.AffiliateAPI();
            var modelByteArray = affBetlistService.GetBetlist(betlistSOAP);
            var viewModel = Common.ByteArrayToObject<BetListViewModel>(modelByteArray);

            return View(viewModel);
        }
        
        /// <summary>
        /// Get data for running mix parlay bet list.
        /// </summary>
        /// <param name="transId">Transaction Id.</param>
        /// <returns>Dynamic objects, contain Mix Parlay result.</returns>
        [HttpGet]
        public dynamic BetListParlay(long transId)
        {
            var affBetlistService = new AffBetlistService.AffiliateAPI();
            var ticket = affBetlistService.GetBetlistParlay(
                AppSettings.AffiliateName,
                transId.ToString(),
                CurrentSession.ExchangeRate.ToString());
            var ticketsText = Common.ByteArrayToListObjects<TicketText>(ticket);
            var jsonSerialiser = new JavaScriptSerializer();

            return jsonSerialiser.Serialize(ticketsText);
        }

        /// <summary>
        /// Action Get for System Parlay Detail.
        /// </summary>
        /// <param name="criteria">The metal model.</param>
        /// <returns>The action result.</returns>
        [HttpGet]
        public ActionResult SystemParlayDetail(AffBetlistService.BetListSystemParlayDetailCriteria criteria)
        {
            var affBetlistService = new AffBetlistService.AffiliateAPI();
            var systemParlayDataText = affBetlistService.GetBetlistSystemParlay(
                AppSettings.AffiliateName,
                CurrentSession.ExchangeRate.ToString(),
                criteria);
            var viewModel = new SystemParlayDetailViewModel
            {
                DetailText = systemParlayDataText,
                Param = new BetListSystemParlayDetailCriteria
                {
                    AffiliateGroup = criteria.AffiliateGroup,
                    UserId = criteria.UserId,
                    TransId = criteria.TransId,
                    RefNo = criteria.RefNo,
                    BetId = criteria.BetId,
                    CustId = criteria.CustId,
                    Outstanding = criteria.Outstanding
                }
            };

            return View(viewModel);
        }

        /// <summary>
        /// Get supported products.
        /// </summary>
        /// <returns>List of Product.</returns>
        public List<Product> GetSupportedProducts()
        {
            var productCriteria = new ProductCriteria
            {
                Language = AppSession.Language,
                AffId = CurrentSession.AffId,
                MonthDate = null
            };

            return ProductService.GetProductList(productCriteria).ToList();
        }

        /// <summary>
        /// Get supported products.
        /// </summary>
        /// <returns>An array of supported products.</returns>
        public AffBetlistService.Product[] GetSupportedProductsForWebService()
        {
            var products = GetSupportedProducts();
            List<AffBetlistService.Product> supportedProducts = new List<AffBetlistService.Product>();
            
            foreach (var product in products)
            {
                supportedProducts.Add(new AffBetlistService.Product
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    OriginalProductName = product.OriginalProductName,
                    TotalQualifiedMembers = product.TotalQualifiedMembers
                });
            }

            return supportedProducts.ToArray();
        }

        /// <summary>
        /// Get Exchange rate.
        /// </summary>
        /// <param name="fromCurrency">From currency.</param>
        /// <param name="winlostDate">Win loss date.</param>
        /// <returns>Exchange rate.</returns>
        public decimal GetExchangeRate(string fromCurrency, DateTime winlostDate)
        {
            var exchangeCriteria = new ExchangeRateCriteria
            {
                FromCurrency = int.Parse(fromCurrency.Trim()),
                ToCurrency = CurrentSession.CurrencyID,
                Date = winlostDate
            };

            return (decimal)BetListService.GetExchangeRate(exchangeCriteria);
        }

        #endregion
    }
}