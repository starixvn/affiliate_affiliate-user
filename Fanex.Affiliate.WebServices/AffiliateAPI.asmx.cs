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
    using Fanex.Affiliate.Criteria;
    using Fanex.Affiliate.DTO;
    using Fanex.BetList.Core.Builder;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.TicketHelper;
    using Fanex.Data.Configuration;
    using Fanex.Result.Core;
    using Services;
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

        private List<string> _resultsSupportedLanguages = new List<string>() { "en-US", "zh-TW", "zh-CN", "ja-JP", "th-TH", "ko-KR", "vi-VN" };

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

        /// <summary>
        /// Get Bet list View Model.
        /// </summary>
        /// <param name="betlistSOAP">Bet list SOAP object.</param>
        /// <returns>Bet list View Model.</returns>
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

        /// <summary>
        /// Get Bet list Parlay.
        /// </summary>
        /// <param name="affiliateName">Affiliate Name.</param>
        /// <param name="transactionId">Transaction ID.</param>
        /// <param name="exchangeRate">Exchange Rate.</param>
        /// <returns>Ticket Result.</returns>
        [WebMethod]
        public List<TicketText> GetBetlistParlay(string affiliateName, string transactionId, string exchangeRate)
        {
            var affiliateGroup = affiliateName.Trim() == "12bet" ? "_12bet" : string.Empty;
            var criteria = new BetListParlayCriteria
            {
                AffiliateGroup = affiliateGroup,
                TransID = long.Parse(transactionId)
            };

            var ticketResults = _betlistService.GetBetListParlay(criteria);

            return Ticket(ticketResults, new BetListCriteria(), string.Empty, decimal.Parse(exchangeRate), 1, true);
        }

        /// <summary>
        /// Get Bet list System Parlay.
        /// </summary>
        /// <param name="affiliateName">Affiliate Name.</param>
        /// <param name="criteria">Bet List System Parlay Detail Criteria.</param>
        /// <returns>System Parlay Data.</returns>
        [WebMethod]
        public SystemParlayData GetBetlistSystemParlay(string affiliateName, BetListSystemParlayDetailCriteria criteria)
        {
            criteria.AffiliateGroup = affiliateName.Trim() == "12bet" ? "_12bet" : string.Empty;

            return _betlistService.GetSystemParlayDetail(criteria);
        }

        #endregion

        #region Affiliate's Results - Utilities

        /// <summary>
        /// Get normal pop up data.
        /// </summary>
        /// <param name="affiliateGroup">Affiliate Group.</param>
        /// <param name="criteria">The metal model.</param>
        /// <param name="sportType">The sport type.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="licenseeId">The licensee id.</param>
        /// <param name="setting">The setting result.</param>
        /// <returns>HTML formatted of data.</returns>
        public string GetNormalPopUpData(
            string affiliateGroup,
            BetListResultsCriteria criteria,
            SportType sportType,
            int userId,
            int licenseeId,
            ResultSetting setting)
        {
            switch (sportType)
            {
                case SportType.HorseRacing:
                case SportType.Greyhound:
                case SportType.Harness:
                case SportType.VirtualHorseRacing:
                    if (criteria.RaceNo == null)
                    {
                        return string.Empty;
                    }

                    var dataRc = _betlistService.GetPopUpRacingMatch(
                        affiliateGroup,
                        criteria.MatchId,
                        criteria.RaceNo.Value,
                        licenseeId,
                        userId);
                    return dataRc != null ? ResultEngine.HtmlParser(dataRc, setting) : string.Empty;

                case SportType.LiveCasino:
                    var dataLc = _betlistService.GetPopUpLiveCasinoMatch(
                        affiliateGroup,
                        criteria.MatchId,
                        licenseeId,
                        userId);
                    return dataLc != null ? ResultEngine.HtmlParser(dataLc, setting) : string.Empty;

                case SportType.NumberGame:
                    var dataNg = _betlistService.GetPopUpNumberGameMatchs(affiliateGroup, criteria.MatchId);
                    return dataNg != null ? ResultEngine.HtmlParser(dataNg, setting) : string.Empty;

                default:
                    var data = _betlistService.GetPopUpMatch(affiliateGroup, criteria.MatchId, licenseeId, userId);
                    return data != null ? ResultEngine.HtmlParser(data, setting) : string.Empty;
            }
        }

        #endregion

        #region Affiliate's Results - Web Methods

        /// <summary>
        /// Get results html string.
        /// </summary>
        /// <param name="affiliateName">Affiliate Name.</param>
        /// <param name="sessionLanguage">Session Language.</param>
        /// <param name="licenseeId">Licensee ID.</param>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Result's Html string.</returns>
        [WebMethod]
        public string GetResultsString(string affiliateName, string sessionLanguage, string licenseeId, BetListResultsCriteria criteria)
        {
            var sportType = (SportType)criteria.SportType;
            var customerInfoBetList = _betlistService.GetPlayerIbcInfo(affiliateName + "$" + criteria.UserName);
            var affiliateGroup = affiliateName.Trim() == "12bet" ? "_12bet" : string.Empty;
            var displaySettings = new ResultSetting
            {
                SportType = sportType,
                IsOutRight = criteria.IsOutRight.HasValue && criteria.IsOutRight.Value > 0,
                IsParlayPopUp = criteria.Bettype == (int)BetType.MixParlay || criteria.Bettype == (int)BetType.CombinationMixParlay,
                IsPopUp = true,
                LanguageCode = _resultsSupportedLanguages.Exists(i => i.Equals(sessionLanguage)) ? sessionLanguage : "en-US"
            };

            if (displaySettings.IsParlayPopUp && criteria.RefNo != null)
            {
                var parlayData = _betlistService.GetPopUpParlayMatch(affiliateGroup, criteria.RefNo.Value);
                return ResultEngine.HtmlParser(parlayData, displaySettings);
            }

            return GetNormalPopUpData(affiliateGroup, criteria, sportType, customerInfoBetList.CustId, int.Parse(licenseeId), displaySettings);
        }

        #endregion

        #region Affiliate's Results - RNG Casino - Utilities
        #endregion

        #region Affiliate's Results - RNG Casino - Web Methods
        
        [WebMethod]
        public List<RNGCasinoResult> GetRNGCasino(string affiliateName, BetListResultsRNGCasinoCriteria criteria)
        {
            var customerInfoBetList = _betlistService.GetPlayerIbcInfo(affiliateName + "$" + criteria.UserName);
            criteria.BaseCurrency = criteria.BaseCurrency ?? string.Empty;
            criteria.ExternalId = customerInfoBetList.Username;
            criteria.CurrencyId = customerInfoBetList.CurrencyId;
            
            return criteria.IsValid()
                ? _betlistService.GetRNGCasinoResult(criteria)
                : new List<RNGCasinoResult>();
        }

        [WebMethod]
        public BetListResultsRNGCasinoDetailViewModel GetRNGCasinoDetail(BetListResultsRNGCasinoDetailCriteria criteria)
        {
            criteria.CurrencyName = criteria.CurrencyName.ToUpper();
            
            return criteria.IsValid()
                ? _betlistService.GetRNGCasinoResultDetail(criteria)
                : new BetListResultsRNGCasinoDetailViewModel();
        }

        #endregion
    }
}