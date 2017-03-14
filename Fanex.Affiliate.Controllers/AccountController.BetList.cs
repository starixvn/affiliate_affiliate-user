namespace Fanex.Affiliate.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using AffBetlistService;
    using Configurations;
    using Criteria;
    using Fanex.Affiliate.Criteria.BetList;
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
            var isValid = GetValidationResult(username);

            if (!isValid)
            {
                ViewBag.Data = string.Empty;

                return View();
            }

            DateTime winlostDate;
            DateTime.TryParse(winlostdate, out winlostDate);
            var exchangeRate = GetExchangeRate(currency, winlostDate);
            CurrentSession.ExchangeRate = exchangeRate;
            var supportedProducts = GetSupportedProductsForWebService(winlostDate);

            var betlistSOAP = new BetlistSOAP
            {
                AffiliateName = AppSettings.AffiliateName.ToLower(),
                Username = username,
                Product = product,
                FromDate = winlostDate,
                ToDate = winlostDate,
                ExchangeRate = exchangeRate,
                SupportedProducts = supportedProducts,
                Language = AppSession.Language
            };

            var affBetlistService = new AffiliateAPISoapClient();

            ViewBag.Data = affBetlistService.GetBetlist(betlistSOAP);

            return View();
        }

        /// <summary>
        /// Get data for running mix parlay bet list.
        /// </summary>
        /// <param name="transId">Transaction Id.</param>
        /// <returns>Dynamic objects, contain Mix Parlay result.</returns>
        [HttpGet]
        public dynamic BetListParlay(long transId)
        {
            var affBetlistService = new AffiliateAPISoapClient();
            var tickets = affBetlistService.GetBetlistParlay(
                AppSettings.AffiliateName,
                transId.ToString(),
                CurrentSession.ExchangeRate.ToString(),
                AppSession.Language);

            return tickets;
        }

        [HttpGet]
        public dynamic NewBetListParlay(long transId, string betTeam, string betCheck, decimal handicap1, decimal handicap2)
        {
            var affBetlistService = new AffBetlistService.AffiliateAPISoapClient();
            var tickets = affBetlistService.GetNewBetlistParlay(
                AppSettings.AffiliateName,
                transId.ToString(),
                CurrentSession.ExchangeRate.ToString(),
                betTeam,
                betCheck,
                handicap1,
                handicap2,
                AppSession.Language);

            return tickets;
        }

        /// <summary>
        /// Action Get for System Parlay Detail.
        /// </summary>
        /// <param name="criteria">The metal model.</param>
        /// <returns>The action result.</returns>
        [HttpGet]
        public ActionResult SystemParlayDetail(AffBetlistService.BetListSystemParlayDetailCriteria criteria)
        {
            var affBetlistService = new AffiliateAPISoapClient();
            var systemParlayDataText = affBetlistService.GetBetlistSystemParlay(
                AppSettings.AffiliateName,
                CurrentSession.ExchangeRate.ToString(),
                criteria,
                AppSession.Language);
            ViewBag.Data = systemParlayDataText;

            return View();
        }

        /// <summary>
        /// Get supported products.
        /// </summary>
        /// <param name="winlostDate">Win lost date.</param>
        /// <returns>List of Product.</returns>
        public List<DTO.Product> GetSupportedProducts(DateTime? winlostDate)
        {
            var productCriteria = new ProductCriteria
            {
                Language = AppSession.Language,
                AffId = CurrentSession.AffId,
                MonthDate = winlostDate
            };

            return ProductService.GetProductList(productCriteria).ToList();
        }

        [HttpGet]
        public ActionResult RngCasinoDetailTicket(CasinoDetailViewModel model)
        {
            var criteria = new BetListDetailCriteria
            {
                CustId = model.CustId,
                RefNo = model.TransId,
                ExchangeRate = CurrentSession.ExchangeRate
            };

            var affBetlistService = new AffiliateAPISoapClient();
            ViewBag.Data = affBetlistService.GetBetListRngCasinoDetail(SessionManager.CurrentSession.Username, criteria);

            return View("~/Views/Shared/Account/CasinoDetailTicket.cshtml");
        }

        [HttpGet]
        public ActionResult AgCasinoDetailTicket(CasinoDetailViewModel model)
        {
            var criteria = new BetListDetailCriteria
            {
                CustId = model.CustId,
                RefNo = model.TransId,
                ExchangeRate = CurrentSession.ExchangeRate
            };

            var affBetlistService = new AffiliateAPISoapClient();
            ViewBag.Data = affBetlistService.GetBetListAgCasinoDetail(SessionManager.CurrentSession.Username, criteria);

            return View("~/Views/BetList/CasinoDetailTicket.cshtml");
        }

        /// <summary>
        /// Get supported products.
        /// </summary>
        /// <param name="winlostDate">Win lost date.</param>
        /// <returns>An array of supported products.</returns>
        public AffBetlistService.Product[] GetSupportedProductsForWebService(DateTime? winlostDate)
        {
            var products = GetSupportedProducts(winlostDate);
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

        #endregion BetList

        /// <summary>
        /// Get Validation Result.
        /// </summary>
        /// <param name="username">User Name.</param>
        /// <returns>Validation Result.</returns>
        private bool GetValidationResult(string username)
        {
            var criteria = new ValidationCriteria
            {
                AffId = CurrentSession.AffId,
                MemberName = username ?? string.Empty
            };

            BetListService.ValidatePermission(criteria);

            return criteria.BelongLevel2;
        }
    }
}