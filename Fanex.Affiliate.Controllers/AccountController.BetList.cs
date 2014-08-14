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
    using DTO.BetList;
    using Enumerations;
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
        public ActionResult BetList(string username, int product, string winlostdate, string currency)
        {
            DateTime winlostDate;
            DateTime.TryParse(winlostdate, out winlostDate);
            var balanceUpMoveBetCriteria = new BalanceUpMoveBetCriteria();
            BetListService.GetBalanceUpMoveBetDate(balanceUpMoveBetCriteria);
            var customerInfoBetList = BetListService.GetPlayerIbcInfo(AppSettings.AffiliateName + "$" + username);
            var criteria = new BetListCriteria
            {
                LicenseeId = AppSettings.LicenseeID,
                UserId = AppSettings.LicenseeID,
                FromDate = winlostDate,
                ToDate = winlostDate,
                MoveBetsDay = balanceUpMoveBetCriteria.MoveBetsDay,
                BalanceUpDay = balanceUpMoveBetCriteria.BalanceUpDay,
                CustId = customerInfoBetList.CustId
            };

            ViewBetListByProduct(criteria, product);
            var ticketResults = BetListService.GetBetList(criteria);
            var exchangeCriteria = new ExchangeRateCriteria
            {
                FromCurrency = int.Parse(currency.Trim()),
                ToCurrency = CurrentSession.CurrencyID,
                Date = winlostDate
            };
            var exchangeRate = (decimal)BetListService.GetExchangeRate(exchangeCriteria);
            CurrentSession.ExchangeRate = exchangeRate;

            var model = new BetListViewModel()
            {
                TicketTextList = Ticket(ticketResults, criteria, username, exchangeRate),
                IsShowSystemIdAndExternalId = false,
                IsShowWinLoss = true,
                TicketResult = ticketResults,
                ExchangeRate = exchangeRate
            };

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
            var ticketResult = BetListService.GetBetListParlay(transId);
            var exchangeRate = CurrentSession.ExchangeRate == -1 ? 1 : CurrentSession.ExchangeRate;
            var ticket = Ticket(ticketResult, new BetListCriteria(), string.Empty, exchangeRate, 1, true);
            var jsonSerialiser = new JavaScriptSerializer();

            return jsonSerialiser.Serialize(ticket);
        }

        /// <summary>
        /// Action Get for System Parlay Detail.
        /// </summary>
        /// <param name="criteria">The metal model.</param>
        /// <returns>The action result.</returns>
        [HttpGet]
        public ActionResult SystemParlayDetail(SystemParlayDetailCriteria criteria)
        {
            var balanceUpMoveBetCriteria = new BalanceUpMoveBetCriteria();
            BetListService.GetBalanceUpMoveBetDate(balanceUpMoveBetCriteria);
            criteria.BalanceUpDay = balanceUpMoveBetCriteria.BalanceUpDay;
            criteria.MoveBetsDay = balanceUpMoveBetCriteria.MoveBetsDay;
            criteria.LicenseeId = AppSettings.LicenseeID;
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
        /// Generate ticket text.
        /// </summary>
        /// <param name="ticketResult">Ticket result Parameter value.</param>
        /// <param name="criteria"> Parameter value.</param>
        /// <param name="username">User's Name.</param>
        /// <param name="exchangeRate">Exchange rate value.</param>
        /// <param name="startIndex">Start index Parameter value.</param>
        /// <param name="showOdds">Show Odds or not.</param>
        /// <returns>Result value.</returns>
        [NonAction]
        public List<TicketText> Ticket(
            TicketResult ticketResult,
            BetListCriteria criteria,
            string username,
            decimal exchangeRate = 1,
            int startIndex = 1,
            bool showOdds = false)
        {
            var builder = new BetListHTMLBuilder();
            var ticketTextList = new List<TicketText>();
            var ticketHelper = new TicketHelper();
            var baseCurrency = criteria == null ? string.Empty : criteria.BaseCurrency;
            decimal sumWinloss = 0;

            foreach (var ticket in ticketResult.MainTicketList)
            {
                if (ticket.BetTypeId == (int)BetType.CombinationMixParlay)
                {
                    var custid = ticket.CustId != 0 ? ticket.CustId : criteria.CustId;
                    var formatDetail = @"showCombMPDetail({0},{1},{2},{3},&quot;{4}&quot;,&quot;{5}&quot;)";
                    var betTeam = ticketHelper.GetBetTypeNameById(ticket.BetTypeId) + " - " + ticketHelper.GetCombinationMPOfferName(ticket.BetTeam);
                    ticketHelper.ClientDetailSystemParlayFunction = string.Format(
                        formatDetail,
                        ticket.TransId,
                        ticket.RefNo,
                        ticket.BetId,
                        custid,
                        betTeam,
                        baseCurrency);
                }

                ticketHelper.IsShowOddsWithChoice = showOdds;
                ticket.Stake *= exchangeRate;
                ticket.ActualStake *= exchangeRate;
                var ticketText = new TicketText
                {
                    Index = startIndex++,
                    ExternalId = ticket.ExternalId,
                    SystemId = ticket.UserName,
                    TransTime = builder.BuildTrans(ticket),
                    Status = builder.BuildAffBetListResultStatus(
                        ticket, ticketHelper, ticketResult.TicketDataList, criteria, CurrentSession.CurrencyID, CurrentSession.Currency, username),
                    Choice = builder.BuildChoice(ticket, ticketHelper, ticketResult.TicketDataList, false),
                    Odds = builder.BuildOdds(ticket, ticketResult.TicketDataList, CacheRef.GetOddsTypeName),
                    Stake = builder.BuildStake(ticket),
                    WinLoss = string.Format("{0:#,##0.00}", ticket.Winlost * exchangeRate)
                };

                sumWinloss += ticket.Winlost;
                ticketTextList.Add(ticketText);
            }

            ticketResult.SumWinLoss = sumWinloss;

            return ticketTextList;
        }

        /// <summary>
        /// View bet list by product.
        /// </summary>
        /// <param name="criteria">Bet list criteria.</param>
        /// <param name="product">Product parameter.</param>
        public void ViewBetListByProduct(BetListCriteria criteria, int product)
        {
            if (product == 0)
            {
                ViewBetListByAllProducts(criteria);
            }
            else
            {
                ViewBetListBySpecificProduct(criteria, product);
            }
        }

        /// <summary>
        /// View Bet List By Specific Product.
        /// </summary>
        /// <param name="criteria">Bet List Criteria.</param>
        /// <param name="product">Selected product.</param>
        public void ViewBetListBySpecificProduct(BetListCriteria criteria, int product)
        {
            switch (product)
            {
                case 1:
                    criteria.ShowSB = true;
                    break;
                case 2:
                    criteria.ShowCS = true;
                    break;
                case 3:
                    criteria.ShowNG = true;
                    break;
                case 4:
                    criteria.ShowLC = true;
                    break;
                default:
                    criteria.ShowSB = true;
                    break;
            }
        }

        /// <summary>
        /// View Bet List By All Products.
        /// </summary>
        /// <param name="criteria">Bet List Criteria.</param>
        public void ViewBetListByAllProducts(BetListCriteria criteria)
        {
            var productCriteria = new ProductCriteria
            {
                Language = AppSession.Language,
                AffId = CurrentSession.AffId,
                MonthDate = null
            };

            var listProduct = ProductService.GetProductList(productCriteria).ToList();

            foreach (var item in listProduct)
            {
                ViewBetListBySpecificProduct(criteria, item.ProductId);
            }
        }

        #endregion
    }
}