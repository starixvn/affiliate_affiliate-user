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
    using System.Web.Mvc;
    using System.Web.Script.Serialization;
    using Configurations;
    using Criteria;
    using Enumerations;
    using Fanex.Affiliate.DTO;
    using Fanex.Affiliate.WebServices.Criteria;
    using Fanex.Affiliate.WebServices.DTO.BetList;
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
            
            var balanceUpMoveBetCriteria = new BalanceUpMoveBetCriteria();
            BetListService.GetBalanceUpMoveBetDate(balanceUpMoveBetCriteria);

            var customerInfoBetList = BetListService.GetPlayerIbcInfo(AppSettings.AffiliateName + "$" + username);
            var exchangeRate = GetExchangeRate(currency, winlostDate);
            CurrentSession.ExchangeRate = exchangeRate;
            var supportedProducts = GetSupportedProducts();
            
            var betlistSOAP = new BetlistSOAP
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

            var model = new BetListViewModel();

            return View(model);
        }

        /// <summary>
        /// Get data for running mix parlay bet list.
        /// </summary>
        /// <param name="transId">Transaction Id.</param>
        /// <returns>Dynamic objects, contain Mix Parlay result.</returns>
        [HttpGet]
        public dynamic BetListParlay(long transId)
        {
            // var ticketResult = BetListService.GetBetListParlay(transId);
            // var exchangeRate = CurrentSession.ExchangeRate == -1 ? 1 : CurrentSession.ExchangeRate;
            var ticket = new List<TicketText>(); // Ticket(ticketResult, new BetListCriteria(), string.Empty, exchangeRate, 1, true);
            var jsonSerialiser = new JavaScriptSerializer();

            return jsonSerialiser.Serialize(ticket);
        }

        /// <summary>
        /// Action Get for System Parlay Detail.
        /// </summary>
        /// <param name="criteria">The metal model.</param>
        /// <returns>The action result.</returns>
        [HttpGet]
        public ActionResult SystemParlayDetail(BetListSystemParlayDetailCriteria criteria)
        {
            var balanceUpMoveBetCriteria = new BalanceUpMoveBetCriteria();
            BetListService.GetBalanceUpMoveBetDate(balanceUpMoveBetCriteria);
            criteria.BalanceUpDay = balanceUpMoveBetCriteria.BalanceUpDay;
            criteria.MoveBetsDay = balanceUpMoveBetCriteria.MoveBetsDay;
            criteria.UserId = AppSettings.LicenseeID;

            var systemParlayData = BetListService.GetSystemParlayDetail(criteria);
            ExchangeSystemParlayTickets(systemParlayData);
            var builder = new BetListHTMLBuilder();
            var systemParlayText = builder.BuildSystemParlayDetail(new TicketHelper(), systemParlayData);
            var viewModel = new SystemParlayDetailViewModel
            {
                DetailText = systemParlayText,
                Param = criteria
            };

            return View(viewModel);
        }

        /// <summary>
        /// Exchange system parlay ticket from user's currency to affiliate's currency.
        /// </summary>
        /// <param name="systemParlayData">Original system parlay data object.</param>
        public void ExchangeSystemParlayTickets(SystemParlayData systemParlayData)
        {
            systemParlayData.Outstanding *= CurrentSession.ExchangeRate;
            foreach (var ticket in systemParlayData.TicketList)
            {
                ticket.Comm *= CurrentSession.ExchangeRate;
                ticket.PlayerComm *= CurrentSession.ExchangeRate;
                ticket.Stake *= CurrentSession.ExchangeRate;
                ticket.WinLost *= CurrentSession.ExchangeRate;
            }
        }

        /// <summary>
        /// Get supported products.
        /// </summary>
        /// <returns>List of Product.</returns>
        public IEnumerable<Product> GetSupportedProducts()
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