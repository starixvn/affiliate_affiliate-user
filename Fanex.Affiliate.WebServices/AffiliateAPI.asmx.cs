// ----------------------------------------------------------------------
// <copyright file="AffiliateAPI.asmx.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2014/02/25">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.WebServices
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.Services;
    using System.Xml;
    using System.Xml.Serialization;
    using Criteria;
    using DTO.BetList;
    using Enumerations;
    using Fanex.BetList.Core.Builder;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.TicketHelper;
    using Fanex.Data.Configuration;
    using Services;
    using SOAP;
    using Utilities;
    using ViewModels;

    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]

    /// <summary>
    /// Affiliate's services.
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class AffiliateAPI : System.Web.Services.WebService
    {
        #region Properties

        private readonly IAffiliateService _affiliateService;

        private readonly IBetListService _betlistService;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="AffiliateAPI" /> class.
        /// </summary>
        public AffiliateAPI()
        {
            DbSettingProviderManager.Current.Start();
            _affiliateService = new AffiliateService();
            _betlistService = new BetListService();
        }

        #endregion

        #region Affiliate's Email - Utilities

        /// <summary>
        /// Convert an object into XML format.
        /// </summary>
        /// <param name="inputObject">Input object.</param>
        /// <returns>XML String.</returns>
        public static string ToXml(object inputObject)
        {
            XmlSerializer serializer = new XmlSerializer(inputObject.GetType());

            using (StringWriter textWriter = new StringWriter())
            {
                serializer.Serialize(textWriter, inputObject);
                return textWriter.ToString();
            }
        }

        #endregion

        #region Affiliate's Email - Web Methods

        /// <summary>
        /// Get Affiliate Email.
        /// </summary>
        /// <param name="siteName">Site Name.</param>
        /// <param name="btag">Identify B tag.</param>
        /// <returns>Affiliate email.</returns>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Reviewed. Suppression is OK here.")]
        [WebMethod]
        public string GetAffiliateEmail(string siteName, string btag)
        {
            try
            {
                var affId = int.Parse(btag.Trim().Split('_')[1]);
                var criteria = new AffiliateInfoCriteria { AffId = affId };
                return ToXml(_affiliateService.GetAffiliateInfo(criteria, siteName.Trim().ToLower()));
            }
            catch
            {
                return ToXml(new AffiliateInfo(100, "Invalid Site Name."));
            }
        }

        #endregion

        #region Affiliate's Betlist - Utilities

        /// <summary>
        /// Generate Bet list criteria.
        /// </summary>
        /// <param name="betlistSOAP">Bet list SOAP object.</param>
        /// <returns>BetListCriteria object.</returns>
        public BetListCriteria GenerateBetlistCriteria(BetlistSOAP betlistSOAP)
        {
            var balanceUpMoveBetCriteria = new BalanceUpMoveBetCriteria();
            _betlistService.GetBalanceUpMoveBetDate(balanceUpMoveBetCriteria);
            var customerInfoBetList = _betlistService.GetPlayerIbcInfo(betlistSOAP.AffiliateName + "$" + betlistSOAP.Username);
            var affiliateGroup = betlistSOAP.AffiliateName.Trim() == "12bet" ? "_12bet" : string.Empty;

            BetListCriteria criteria;
            if (betlistSOAP.FromDate < balanceUpMoveBetCriteria.BalanceUpDay)
            {
                criteria = new BetListArchiveCriteria
                {
                    CustId = customerInfoBetList.CustId,
                    FromDate = betlistSOAP.FromDate,
                    ToDate = betlistSOAP.ToDate,
                    AffiliateGroup = affiliateGroup
                };
            }
            else
            {
                criteria = new BetListNormalCriteria
                {
                    CustId = customerInfoBetList.CustId,
                    FromDate = betlistSOAP.FromDate,
                    ToDate = betlistSOAP.ToDate,
                    MoveBetsDay = balanceUpMoveBetCriteria.MoveBetsDay,
                    AffiliateGroup = affiliateGroup
                };
            }

            ViewBetListByProduct(criteria, betlistSOAP.Product, betlistSOAP.SupportedProducts);

            return criteria;
        }

        /// <summary>
        /// View bet list by product.
        /// </summary>
        /// <param name="criteria">Bet list criteria.</param>
        /// <param name="product">Product parameter.</param>
        /// <param name="supportedProducts">List of supported products.</param>
        public void ViewBetListByProduct(BetListCriteria criteria, string product, IEnumerable<Product> supportedProducts)
        {
            if (product == "All")
            {
                foreach (var item in supportedProducts)
                {
                    ViewBetListBySpecificProduct(criteria, item.ProductName);
                }
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
        public void ViewBetListBySpecificProduct(BetListCriteria criteria, string product)
        {
            switch (product)
            {
                case "Sportsbook":
                    criteria.ShowSB = true;
                    break;
                case "RNG Casino":
                    criteria.ShowCasino = true;
                    break;
                case "Number Game":
                    criteria.ShowNG = true;
                    break;
                case "Live Casino":
                    criteria.ShowLiveCasino = true;
                    break;

                case "a":
                    criteria.ShowSB = true;
                    break;
                case "RNG b":
                    criteria.ShowCasino = true;
                    break;
                case "b Game":
                    criteria.ShowNG = true;
                    break;
                case "d Casino":
                    criteria.ShowLiveCasino = true;
                    break;
                default:
                    criteria.ShowSB = true;
                    break;
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
                        ticket, ticketHelper, ticketResult.TicketDataList, criteria, username),
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

        #endregion

        #region Affiliate's Betlist - Web Methods

        [WebMethod]
        public BetListViewModel GetBetlistViewModel(BetlistSOAP betlistSOAP)
        {
            var criteria = GenerateBetlistCriteria(betlistSOAP);
            var ticketResults = _betlistService.GetBetList(criteria);

            var viewModel = new BetListViewModel()
            {
                TicketTextList = Ticket(ticketResults, criteria, betlistSOAP.Username, betlistSOAP.ExchangeRate),
                IsShowSystemIdAndExternalId = false,
                IsShowWinLoss = true,
                TicketResult = ticketResults,
                ExchangeRate = betlistSOAP.ExchangeRate
            };

            return viewModel;
        }

        [WebMethod]
        public TicketResult GetBetlistParlay(string affiliateName, string transactionId)
        {
            var affiliateGroup = affiliateName.Trim() == "12bet" ? "_12bet" : string.Empty;
            var criteria = new BetListParlayCriteria
            {
                AffiliateGroup = affiliateGroup,
                TransID = long.Parse(transactionId)
            };

            return _betlistService.GetBetListParlay(criteria);
        }

        [WebMethod]
        public SystemParlayData GetBetlistSystemParlay(string affiliateName, BetListSystemParlayDetailCriteria criteria)
        {
            criteria.AffiliateGroup = affiliateName.Trim() == "12bet" ? "_12bet" : string.Empty;

            return _betlistService.GetSystemParlayDetail(criteria);
        }

        #endregion

        #region Web Methods - Affiliate's Results

        #endregion
    }
}