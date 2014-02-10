// ----------------------------------------------------------------------
// <copyright file="AccountController.BetListResults.cs" company="Nexcel Solutions Vietnam">
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
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Web.Mvc;
    using Configurations;
    using Criterias;
    using DTO;
    using DTO.BetList;
    using Enumerations;
    using Fanex.Common;
    using Fanex.Result.Core;
    using Logging;
    using Resources;
    using Utilities;
    using ViewModels;
    using Web.Filter;

    /// <summary>
    /// All action after login.
    /// </summary>
    [Authorization]
    public partial class AccountController
    {
        #region Variables

        private const int MIXPARLAY = 9;

        private const int SYSTEMPARLAY = 29;

        private string _htmlResult;

        #endregion

        #region BetList.Results
        /// <summary>
        /// The pop up result page.
        /// </summary>
        /// <param name="criteria">The metal model.</param>
        /// <returns>The action result.</returns>
        [HttpGet]
        public ActionResult BetListResults(BetListResultsCriteria criteria)
        {
            GetPopUpData(criteria);
            ViewBag.Data = _htmlResult;

            return View();
        }

        /// <summary>
        /// Get data for pop-up page.
        /// </summary>
        /// <param name="criteria">The metal model.</param>
        public void GetPopUpData(BetListResultsCriteria criteria)
        {
            var sportType = (SportType)criteria.SportType;
            var licenseeId = AppSettings.LicenseeID;
            customerInfoBetList = BetListService.GetPlayerIbcInfo(AppSettings.AffiliateName + "$" + criteria.UserName);
            var userId = customerInfoBetList.CustId;
            var setting = new ResultSetting
            {
                SportType = sportType,
                IsOutRight = criteria.IsOutRight.HasValue && criteria.IsOutRight.Value > 0,
                IsParlayPopUp = criteria.Bettype == MIXPARLAY || criteria.Bettype == SYSTEMPARLAY,
                IsPopUp = true,
                LanguageCode = CurrentSession.Language
            };

            if (setting.IsParlayPopUp)
            {
                if (criteria.RefNo != null)
                {
                    var parlayData = BetListService.GetPopUpParlayMatch(criteria.RefNo.Value);
                    _htmlResult = ResultEngine.HtmlParser(parlayData, setting);
                }
            }
            else
            {
                GetNormalPopUpData(criteria, sportType, licenseeId, userId, setting);
            }
        }

        /// <summary>
        /// Get normal pop up data.
        /// </summary>
        /// <param name="criteria">The metal model.</param>
        /// <param name="sportType">The sport type.</param>
        /// <param name="licenseeId">The licensee id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="setting">The setting result.</param>
        [SuppressMessage(
           "StyleCop.CSharp.Nexcel.NexcelCustomRules",
           "SP2101:MethodMustNotContainMoreLinesThan",
           Justification = "Reviewed. Suppression is OK here.")]
        public void GetNormalPopUpData(BetListResultsCriteria criteria, SportType sportType, int licenseeId, int userId, ResultSetting setting)
        {
            switch (sportType)
            {
                case SportType.HorseRacing:
                case SportType.Greyhound:
                case SportType.Harness:
                case SportType.VirtualHorseRacing:
                    if (criteria.RaceNo != null)
                    {
                        var dataRc = BetListService.GetPopUpRacingMatch(
                            criteria.MatchId,
                            criteria.RaceNo.Value,
                            licenseeId,
                            userId);
                        if (dataRc != null)
                        {
                            this._htmlResult = ResultEngine.HtmlParser(dataRc, setting);
                        }
                    }

                    break;

                case SportType.LiveCasino:
                    var dataLc = BetListService.GetPopUpLiveCasinoMatch(
                        criteria.MatchId,
                        licenseeId,
                        userId);
                    if (dataLc != null)
                    {
                        this._htmlResult = ResultEngine.HtmlParser(dataLc, setting);
                    }

                    break;

                case SportType.NumberGame:
                    var dataNg = BetListService.GetPopUpNumberGameMatchs(criteria.MatchId);
                    if (dataNg != null)
                    {
                        _htmlResult = ResultEngine.HtmlParser(dataNg, setting);
                    }

                    break;

                default:
                    var data = BetListService.GetPopUpMatch(criteria.MatchId, licenseeId, userId);
                    if (data != null)
                    {
                        _htmlResult = ResultEngine.HtmlParser(data, setting);
                    }

                    break;
            }
        }

        #endregion

        #region BetList.Results.RNGCasino

        /// <summary>
        /// Get RNG Casino data.
        /// </summary>
        /// <param name="criteria">The meta model.</param>
        /// <returns>The data string.</returns>
        [HttpGet]
        public ActionResult RNGCasinoData(BetListResultsRNGCasinoCriteria criteria)
        {
            customerInfoBetList = BetListService.GetPlayerIbcInfo(AppSettings.AffiliateName + "$" + criteria.UserName);
            criteria.BaseCurrency = criteria.BaseCurrency ?? string.Empty;
            criteria.ExternalId = customerInfoBetList.Username;
            criteria.SystemId = customerInfoBetList.IBCUsername;
            criteria.CurrencyId = customerInfoBetList.CurrencyId;
            var data = criteria.Isvalid()
                ? BetListService.GetRNGCasinoResult(criteria)
                : new List<RNGCasinoResult>();
            
            ExchangeRNGCasinoValues(data);
            var total = new List<RNGCasinoResult>();

            if (data != null)
            {
                var item = new RNGCasinoResult
                {
                    Stake = data.Sum(x => x.Stake),
                    Bet = data.Sum(x => x.Bet),
                    WinLoss = data.Sum(x => x.WinLoss),
                    Result = string.Empty,
                };
                total.Add(item);
            }

            var model = new BetListResultsRNGCasinoViewModel();
            model.Param = criteria;
            model.Data = data;

            GenerateRNGCasinoHtmlString(model, total);
            ViewBag.Data = _htmlResult;

            return View("~/Views/Shared/Account/BetListResults.cshtml");
        }

        #region BetList.Results.RNGCasino.GenerateHtmlString
        
        /// <summary>
        /// Generate RNG Casino Html String.
        /// </summary>
        /// <param name="viewModel">BetList Results RNG Casino View Model.</param>
        /// <param name="total">List of total results.</param>
        public void GenerateRNGCasinoHtmlString(BetListResultsRNGCasinoViewModel viewModel, List<RNGCasinoResult> total)
        {
            var casinoHtmlString = GenerateRNGCasinoHtmlHeader(viewModel.Param);
            casinoHtmlString += GenerateRNGCasinoHtmlContent(viewModel, total);
            casinoHtmlString += GenerateRNGCasinoHtmlGrandTotal(total);
            casinoHtmlString += "</tbody></table>";

            _htmlResult = casinoHtmlString;
        }

        /// <summary>
        /// Generate RNG Casino Html Header.
        /// </summary>
        /// <param name="criteria">BetList Results RNG Casino Criteria.</param>
        /// <returns>Html string.</returns>
        public string GenerateRNGCasinoHtmlHeader(BetListResultsRNGCasinoCriteria criteria)
        {
            customerInfoBetList = BetListService.GetPlayerIbcInfo(AppSettings.AffiliateName + "$" + criteria.UserName);
            var resultsLabel = "Username: " + customerInfoBetList.Username + " - Ref No: ";
            resultsLabel += criteria.RefNo + "- View in " + CurrentSession.Currency + " - ";
            var checkboxStatusString = criteria.IsShowPic ? "checked='checked'" : string.Empty;
            var casinoHtmlHeader = "<div class=\"rngcasino_headerlabel\">" + resultsLabel;
            casinoHtmlHeader += "<input type='checkbox' id='chkIsShowPic' name='chkIsShowPic'" + checkboxStatusString + "> Results with pictures</div>";
            casinoHtmlHeader += "<table cellspacing=0 cellpadding=0 class=\"tblBetList\">";
            casinoHtmlHeader += "<thead><tr><th style=\"width: 20px\">#</th>";
            casinoHtmlHeader += "<th style=\"width: 120px\">Game ID</th>";
            casinoHtmlHeader += "<th style=\"width: 120px\">Player History No</th>";
            casinoHtmlHeader += "<th style=\"width: 120px\">Game Type</th>";
            casinoHtmlHeader += "<th style=\"width: 100px\">Game Group</th>";
            casinoHtmlHeader += "<th style=\"width: 100px\">Start Date</th>";
            casinoHtmlHeader += "<th style=\"width: 100px\">End Date</th>";
            casinoHtmlHeader += "<th style=\"width: 100px\">Start Balance</th>";
            casinoHtmlHeader += "<th style=\"width: 100px\">End Balance</th>";
            casinoHtmlHeader += "<th style=\"width: 80px\">Stake</th>";
            casinoHtmlHeader += "<th style=\"width: 80px\">Bet</th>";
            casinoHtmlHeader += "<th style=\"width: 80px\">Win/Loss</th>";
            casinoHtmlHeader += "<th style=\"width: 150px\">Results</th></tr></thead><tbody>";

            return casinoHtmlHeader;
        }

        /// <summary>
        /// Generate RNG Casino Html Content.
        /// </summary>
        /// <param name="viewModel">BetList Results RNG Casino View Model.</param>
        /// <param name="total">List of total results.</param>
        /// <returns>Html string.</returns>
        public string GenerateRNGCasinoHtmlContent(BetListResultsRNGCasinoViewModel viewModel, List<RNGCasinoResult> total)
        {
            if (viewModel.Data.Count() <= 0)
            {
                return "<tr><td colspan=\"13\" class=\"c\">" + Lang.NoInformation + "</td></tr>";
            }

            var sessionLogCounter = 0;
            var currentSessionID = viewModel.Data[0].SessionLogId;
            var casinoHtmlContent = "<tr><td colspan=\"13\" class=\"rngcasino_group\"> Session ID: " + currentSessionID + "</td></tr>";
            var showPicHtmlString = "</td><td class=\"rngcasino_alignleft\"><a href='javascript:;' class='hand-history-detail-link' data-url='";
            foreach (var item in viewModel.Data)
            {
                var rngCasinoDetailUrl = "?userbetid=" + item.UserBetId;
                rngCasinoDetailUrl += "&gametypeid=" + item.GameTypeId;
                rngCasinoDetailUrl += "&gametypename=" + item.GameTypeName;
                rngCasinoDetailUrl += "&currencyid=" + CurrentSession.CurrencyID;
                rngCasinoDetailUrl += "&currencyname=" + CurrentSession.Currency;
                rngCasinoDetailUrl += "&playerhistoryno=" + item.PlayerHistoryNo;

                if (item.SessionLogId != currentSessionID)
                {
                    casinoHtmlContent += GenerateRNGCasinoHtmlSubTotal(total[sessionLogCounter]);
                    casinoHtmlContent += "<tr><td colspan=\"13\" class=\"rngcasino_group\"> Session ID: " + currentSessionID + "</td></tr>";
                    sessionLogCounter++;
                }

                casinoHtmlContent += "<tr><td class=\"rngcasino_alignleft\">" + item.Index;
                casinoHtmlContent += "</td><td class=\"rngcasino_alignleft\">" + item.GameId;
                casinoHtmlContent += "</td><td class=\"rngcasino_alignleft\">" + item.PlayerHistoryNo;
                casinoHtmlContent += "</td><td class=\"rngcasino_alignleft\">" + item.GameTypeName;
                casinoHtmlContent += "</td><td class=\"rngcasino_alignleft\">" + item.GameGroupName;
                casinoHtmlContent += "</td><td class=\"c\">" + item.StartTime;
                casinoHtmlContent += "</td><td class=\"c\">" + item.EndTime;
                casinoHtmlContent += "</td><td class=\"\">" + FormaterCurrency.Format(item.BeginBalance);
                casinoHtmlContent += "</td><td class=\"\">" + FormaterCurrency.Format(item.EndBlance);
                casinoHtmlContent += "</td><td class=\"\">" + FormaterCurrency.Format(item.Stake);
                casinoHtmlContent += "</td><td class=\"\">" + FormaterCurrency.Format(item.Bet);
                casinoHtmlContent += "</td><td class=\"\">" + FormaterCurrency.Format(item.WinLoss);
                casinoHtmlContent += viewModel.Param.IsShowPic
                    ? showPicHtmlString + rngCasinoDetailUrl + "'>Detail:</a><br />" + item.Result + " </td></tr>"
                    : showPicHtmlString + rngCasinoDetailUrl + "'>" + item.Result + "</a></td></tr>";
            }

            casinoHtmlContent += GenerateRNGCasinoHtmlSubTotal(total.Last());

            return casinoHtmlContent;
        }

        /// <summary>
        /// Generate RNG Casino Html Footer.
        /// </summary>
        /// <param name="total">List of total results.</param>
        /// <returns>Html string.</returns>
        public string GenerateRNGCasinoHtmlGrandTotal(List<RNGCasinoResult> total)
        {
            decimal stake = 0;
            decimal bet = 0;
            decimal winloss = 0;

            foreach (var item in total)
            {
                stake += item.Stake;
                bet += item.Bet;
                winloss += item.WinLoss;
            }

            var casinoHtmlFooter = "<tr class=\"bl_footertotal footer\" id=\"tr_footer\">";
            casinoHtmlFooter += "<td class=\"r\" colspan=\"9\">Grand Total";
            casinoHtmlFooter += "</td><td class=\"r\">" + FormaterCurrency.Format(stake);
            casinoHtmlFooter += "</td><td class=\"r\">" + FormaterCurrency.Format(bet);
            casinoHtmlFooter += "</td><td class=\"r\">" + FormaterCurrency.Format(winloss);
            casinoHtmlFooter += "</td><td></td></tr>";

            return casinoHtmlFooter;
        }

        /// <summary>
        /// Generate RNG Casino Html Sub-Total.
        /// </summary>
        /// <param name="subtotal">Sub Total results.</param>
        /// <returns>Html string.</returns>
        public string GenerateRNGCasinoHtmlSubTotal(RNGCasinoResult subtotal)
        {
            var casinoHtmlSubTotal = "<tr class=\"rngcasino_subtotal\">";
            casinoHtmlSubTotal += "<td class=\"r\" colspan=\"9\">Total";
            casinoHtmlSubTotal += "</td><td class=\"r\">" + FormaterCurrency.Format(subtotal.Stake);
            casinoHtmlSubTotal += "</td><td class=\"r\">" + FormaterCurrency.Format(subtotal.Bet);
            casinoHtmlSubTotal += "</td><td class=\"r\">" + FormaterCurrency.Format(subtotal.WinLoss);
            casinoHtmlSubTotal += "</td><td></td></tr>";

            return casinoHtmlSubTotal;
        }

        #endregion

        /// <summary>
        /// Exchange RNG Casino Values.
        /// </summary>
        /// <param name="originalData">Original data.</param>
        public void ExchangeRNGCasinoValues(List<RNGCasinoResult> originalData)
        {
            foreach (var item in originalData)
            {
                item.BeginBalance *= CurrentSession.ExchangeRate;
                item.EndBlance *= CurrentSession.ExchangeRate;
                item.Bet *= CurrentSession.ExchangeRate;
                item.Stake *= CurrentSession.ExchangeRate;
                item.Win *= CurrentSession.ExchangeRate;
                item.WinLoss *= CurrentSession.ExchangeRate;
            }
        }

        /// <summary>
        /// RNG Casino detail.
        /// </summary>
        /// <param name="criteria">The meta model.</param>
        /// <returns>The action result.</returns>
        [HttpGet]
        public ViewResult RNGCasinoDetail(BetListResultsRNGCasinoDetailCriteria criteria)
        {
            criteria.CurrencyName = criteria.CurrencyName.ToUpper();
            const string ViewPathTemplate = "~/Views/Shared/Account/RNGCasinoDetail/Group{0}.cshtml";
            var group = this.GetGroupByGameTypeId(criteria.GameTypeId);
            LogService.Error("TEST RNG CASINO DATA - Before get data");
            var model = criteria.IsValid()
                ? BetListService.GetRNGCasinoResultDetail(criteria)
                : new BetListResultsRNGCasinoDetailViewModel();
            LogService.Error("TEST RNG CASINO DATA - After get data");

            model.Param = criteria;
            ExchangeRNGCasinoDetailValues(model);

            LogService.Error("TEST RNG CASINO DATA - After exchange data");
            if (string.IsNullOrEmpty(group))
            {
                return this.View("~/Views/Shared/Account/RNGCasinoDetail/NoGameErr.cshtml");
            }

            if (model.CasinoDetailHeaderList != null && model.CasinoDetailHeaderList.Count > 0)
            {
                LogService.Error("TEST RNG CASINO DATA - Before return view");
                return this.View(string.Format(ViewPathTemplate, group), model);
            }

            return this.View("~/Views/Shared/Account/RNGCasinoDetail/Noinfo.cshtml");
        }

        /// <summary>
        /// Exchange RNG Casino detail Values.
        /// </summary>
        /// <param name="originalData">Original data.</param>
        public void ExchangeRNGCasinoDetailValues(BetListResultsRNGCasinoDetailViewModel originalData)
        {
            var logString = "TEST AFF - RNG CASINO DATA - Exchange rate: " + CurrentSession.ExchangeRate;
            foreach (var detailItem in originalData.CasinoDetailList)
            {
                logString += Environment.NewLine + "BET o: " + detailItem.Bet + "#ex: " + detailItem.Bet*CurrentSession.ExchangeRate;
                logString += Environment.NewLine + "WIN o: " + detailItem.Win + "#ex: " + detailItem.Win * CurrentSession.ExchangeRate;
                logString += Environment.NewLine + "WINLOSS o: " + detailItem.WinLoss + "#ex: " + detailItem.WinLoss * CurrentSession.ExchangeRate;
                detailItem.Bet *= CurrentSession.ExchangeRate;
                detailItem.Win *= CurrentSession.ExchangeRate;
                detailItem.WinLoss *= CurrentSession.ExchangeRate;
            }

            foreach (var detailHeaderItem in originalData.CasinoDetailHeaderList)
            {
                logString += Environment.NewLine + "BetValue o: " + detailHeaderItem.BetValue + "#ex: " + detailHeaderItem.BetValue * CurrentSession.ExchangeRate;
                logString += Environment.NewLine + "BetBaseCurrencyValue o: " + detailHeaderItem.BetBaseCurrencyValue + "#ex: " + detailHeaderItem.BetBaseCurrencyValue * CurrentSession.ExchangeRate;
                logString += Environment.NewLine + "CoinSizeBaseCurrencyValue o: " + detailHeaderItem.CoinSizeBaseCurrencyValue + "#ex: " + detailHeaderItem.CoinSizeBaseCurrencyValue * CurrentSession.ExchangeRate;
                logString += Environment.NewLine + "CoinSizeValue o: " + detailHeaderItem.CoinSizeValue + "#ex: " + detailHeaderItem.CoinSizeValue * CurrentSession.ExchangeRate;
                logString += Environment.NewLine + "WinBaseCurrencyValue o: " + detailHeaderItem.WinBaseCurrencyValue + "#ex: " + detailHeaderItem.WinBaseCurrencyValue * CurrentSession.ExchangeRate;
                logString += Environment.NewLine + "WinLossResultBaseCurrecyValue o: " + detailHeaderItem.WinLossResultBaseCurrecyValue + "#ex: " + detailHeaderItem.WinLossResultBaseCurrecyValue * CurrentSession.ExchangeRate;
                logString += Environment.NewLine + "WinResultBaseCurrencyValue o: " + detailHeaderItem.WinResultBaseCurrencyValue + "#ex: " + detailHeaderItem.WinResultBaseCurrencyValue * CurrentSession.ExchangeRate;
                detailHeaderItem.BetValue *= CurrentSession.ExchangeRate;
                detailHeaderItem.BetBaseCurrencyValue *= CurrentSession.ExchangeRate;
                detailHeaderItem.CoinSizeBaseCurrencyValue *= CurrentSession.ExchangeRate;
                detailHeaderItem.CoinSizeValue *= CurrentSession.ExchangeRate;
                detailHeaderItem.WinBaseCurrencyValue *= CurrentSession.ExchangeRate;
                detailHeaderItem.WinLossResultBaseCurrecyValue *= CurrentSession.ExchangeRate;
                detailHeaderItem.WinResultBaseCurrencyValue *= CurrentSession.ExchangeRate;
            }

            LogService.Error(logString);
        }

        /// <summary>
        /// Get value by base currency.
        /// </summary>
        /// <param name="originVal">The original value.</param>
        /// <returns>The converted value.</returns>
        private decimal ExchangeBaseCurrencyValue(string originVal)
        {
            if (!string.IsNullOrEmpty(originVal))
            {
                var arr = originVal.Split(' ').ToList();
                if (arr.Any())
                {
                    return Converter.ToDecimal(arr.LastOrDefault()) * CurrentSession.ExchangeRate;
                }
            }

            return 0;
        }

        /// <summary>
        /// Get coin size value by currency.
        /// </summary>
        /// <param name="originVal">The original value.</param>
        /// <returns>The converted value.</returns>
        private string ExchangeCoinSizeValue(string originVal)
        {
            if (!string.IsNullOrEmpty(originVal))
            {
                var arr = originVal.Split(' ');
                if (arr.Count() > 3)
                {
                    return (Converter.ToDecimal(arr[1]) * CurrentSession.ExchangeRate) + " " + arr[2] + arr[3];
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Get game type Id.
        /// </summary>
        /// <param name="gameTypeId">The game type Id.</param>
        /// <returns>The game type id.</returns>
        [SuppressMessage(
              "StyleCop.CSharp.Nexcel.NexcelCustomRules",
              "SP2101:MethodMustNotContainMoreLinesThan",
              Justification = "Reviewed. Suppression is OK here.")]
        private string GetGroupByGameTypeId(int gameTypeId)
        {
            switch (gameTypeId)
            {
                case (int)CasinoGameType.Baccarat:
                case (int)CasinoGameType.Blackjack:
                case (int)CasinoGameType.CasinoHoldEm:
                case (int)CasinoGameType.BlackjackPerfectPair:
                case (int)CasinoGameType.BaccaratSuper6:
                case (int)CasinoGameType.ThreeKnights:
                case (int)CasinoGameType.DragonTiger:
                case (int)CasinoGameType.BattleWar:
                case (int)CasinoGameType.PokerTri:
                case (int)CasinoGameType.PaiGow:
                case (int)CasinoGameType.FaceUp21:
                case (int)CasinoGameType.ThreeCardsRummy:
                case (int)CasinoGameType.SicBo:
                case (int)CasinoGameType.RoulettePro:
                case (int)CasinoGameType.RoulettePro_Fast:
                case (int)CasinoGameType.MiniJacksorBetter:
                case (int)CasinoGameType.MiniBaccarat:
                case (int)CasinoGameType.MiniSicBo:
                case (int)CasinoGameType.MiniBlackjack:
                case (int)CasinoGameType.MiniCasinoHoldEm:
                    return "1";
                case (int)CasinoGameType.TexasHoldEm:
                    return "1-1";
                case (int)CasinoGameType.RedDog:
                case (int)CasinoGameType.MiniRoulette:
                case (int)CasinoGameType.FishPrawnCrab:
                case (int)CasinoGameType.CardWheel:
                case (int)CasinoGameType.FanTan:
                    return "2";
                case (int)CasinoGameType.MoneySlot:
                case (int)CasinoGameType.Engagement:
                case (int)CasinoGameType.FoxForce5:
                case (int)CasinoGameType.TattooArt:
                case (int)CasinoGameType.MiniMoneySlot:
                    return "3";
                case (int)CasinoGameType.Champions:
                case (int)CasinoGameType.WaterMargin:
                    return "4";
                case (int)CasinoGameType.WesternZodiac:
                    return "4-1";
                case (int)CasinoGameType.MoneyWheel:
                    return "5";
                case (int)CasinoGameType.JourneytotheWest:
                    return "6";
                case (int)CasinoGameType.ChineseZodiac:
                    return "6-1";
                case (int)CasinoGameType.JacksorBetter:
                    return "7-0";
                case (int)CasinoGameType.JungleWild:
                case (int)CasinoGameType.GodofProsperity:
                    return "7-1";
                case (int)CasinoGameType.No4_LineJacksorBetter:
                case (int)CasinoGameType.No25_LineJacksorBetter:
                case (int)CasinoGameType.No50_LineJacksorBetter:
                    return "7-2";
                case (int)CasinoGameType.No_10sOrBetter:
                    return "7-3";
                case (int)CasinoGameType.MonkeyHiLo:
                case (int)CasinoGameType.MahjongHiLo:
                    return "7-4";
                case (int)CasinoGameType.KenoChinese:
                case (int)CasinoGameType.KenoCamera:
                    return "7-5";
                case (int)CasinoGameType.TurfKing:
                    return "7-6";
                case (int)CasinoGameType.MamakCorner:
                case (int)CasinoGameType.WinterSlot:
                case (int)CasinoGameType.DragonSlot:
                    return "7-8";
                case (int)CasinoGameType.MayaPush:
                case (int)CasinoGameType.Knock2Eggs:
                case (int)CasinoGameType.HoHoHo:
                case (int)CasinoGameType.PokeMole:
                case (int)CasinoGameType.GongXiFaCai:
                case (int)CasinoGameType.FruitScratch:
                case (int)CasinoGameType.FlamesofOlympics:
                    return "8";
                case (int)CasinoGameType.PenaltyHat_Trick:
                case (int)CasinoGameType.FancyScratch:
                case (int)CasinoGameType.LoveStory:
                    return "9";
                default:
                    return string.Empty;
            }
        }

        #endregion
    }
}