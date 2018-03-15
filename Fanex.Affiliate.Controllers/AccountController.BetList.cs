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

    [Authorization]
    public partial class AccountController
    {
        #region BetList

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

        public List<DTO.Product> GetSupportedProducts(DateTime? winlostDate)
        {
            var productCriteria = new ProductCriteria
            {
                Language = AppSession.Language,
                AffId = CurrentSession.AffId,
                MonthDate = winlostDate
            };

            return _productService.GetProductList(productCriteria).ToList();
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

        [HttpGet]
        public ActionResult CashOutDetailTicket(CashOutDetailViewModel model)
        {
            var criteria = new BetListDetailCriteria
            {
                TransId = model.TransId,
                RefNo = model.RefNo,
                BetType = model.BetType,
                BetdaqId = model.BetdaqId,
                ExchangeRate = CurrentSession.ExchangeRate
            };

            var affBetlistService = new AffiliateAPISoapClient();
            ViewBag.Data = affBetlistService.GetBetListCashOutDetail(criteria, model.IsCashOut, AppSession.Language);
            ViewBag.IsCashOut = model.IsCashOut;

            return View("~/Views/Shared/Account/CashOutTicketDetail.cshtml");
        }

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

        public decimal GetExchangeRate(string fromCurrency, DateTime winlostDate)
        {
            var exchangeCriteria = new ExchangeRateCriteria
            {
                FromCurrency = int.Parse(fromCurrency.Trim()),
                ToCurrency = CurrentSession.CurrencyID,
                Date = winlostDate
            };

            return (decimal)_betListService.GetExchangeRate(exchangeCriteria);
        }

        #endregion BetList

        private bool GetValidationResult(string username)
        {
            var criteria = new ValidationCriteria
            {
                AffId = CurrentSession.AffId,
                MemberName = username ?? string.Empty,
                UserCurrencies = CurrentSession.CurrencyID.ToString()
            };

            _betListService.ValidatePermission(criteria);

            return criteria.BelongLevel2;
        }
    }
}