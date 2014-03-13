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
    using System.Text.RegularExpressions;
    using System.Web.Mvc;
    using System.Web.Script.Serialization;
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
            ViewBag.Data = GetPopUpData(criteria);
            return View();
        }

        /// <summary>
        /// Get data for pop-up page.
        /// </summary>
        /// <param name="criteria">The metal model.</param>
        /// <returns>HTML formatted of data.</returns>
        public string GetPopUpData(BetListResultsCriteria criteria)
        {
            var sportType = (SportType)criteria.SportType;
            var licenseeId = AppSettings.LicenseeID;
            var customerInfoBetList = BetListService.GetPlayerIbcInfo(AppSettings.AffiliateName + "$" + criteria.UserName);
            var setting = new ResultSetting
            {
                SportType = sportType,
                IsOutRight = criteria.IsOutRight.HasValue && criteria.IsOutRight.Value > 0,
                IsParlayPopUp = criteria.Bettype == (int)BetType.MixParlay || criteria.Bettype == (int)BetType.CombinationMixParlay,
                IsPopUp = true,
                LanguageCode = CurrentSession.Language
            };

            if (setting.IsParlayPopUp && criteria.RefNo != null)
            {
                var parlayData = BetListService.GetPopUpParlayMatch(criteria.RefNo.Value);
                return ResultEngine.HtmlParser(parlayData, setting);
            }

            return GetNormalPopUpData(criteria, sportType, licenseeId, customerInfoBetList.CustId, setting);
        }

        /// <summary>
        /// Get normal pop up data.
        /// </summary>
        /// <param name="criteria">The metal model.</param>
        /// <param name="sportType">The sport type.</param>
        /// <param name="licenseeId">The licensee id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="setting">The setting result.</param>
        /// <returns>HTML formatted of data.</returns>
        public string GetNormalPopUpData(BetListResultsCriteria criteria, SportType sportType, int licenseeId, int userId, ResultSetting setting)
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

                    var dataRc = BetListService.GetPopUpRacingMatch(
                        criteria.MatchId,
                        criteria.RaceNo.Value,
                        licenseeId,
                        userId);
                    return dataRc != null ? ResultEngine.HtmlParser(dataRc, setting) : string.Empty;

                case SportType.LiveCasino:
                    var dataLc = BetListService.GetPopUpLiveCasinoMatch(
                        criteria.MatchId,
                        licenseeId,
                        userId);
                    return dataLc != null ? ResultEngine.HtmlParser(dataLc, setting) : string.Empty;

                case SportType.NumberGame:
                    var dataNg = BetListService.GetPopUpNumberGameMatchs(criteria.MatchId);
                    return dataNg != null ? ResultEngine.HtmlParser(dataNg, setting) : string.Empty;

                default:
                    var data = BetListService.GetPopUpMatch(criteria.MatchId, licenseeId, userId);
                    return data != null ? ResultEngine.HtmlParser(data, setting) : string.Empty;
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
            var customerInfoBetList = BetListService.GetPlayerIbcInfo(AppSettings.AffiliateName + "$" + criteria.UserName);
            criteria.BaseCurrency = criteria.BaseCurrency ?? string.Empty;
            criteria.ExternalId = customerInfoBetList.Username;
            criteria.SystemId = customerInfoBetList.IBCUsername;
            criteria.CurrencyId = customerInfoBetList.CurrencyId;
            var data = criteria.IsValid()
                ? BetListService.GetRNGCasinoResult(criteria)
                : new List<RNGCasinoResult>();
            
            ExchangeRNGCasinoValues(data);
            var total = new List<RNGCasinoResult>();
            if (data != null)
            {
                total.Add(new RNGCasinoResult
                {
                    Stake = data.Sum(x => x.Stake),
                    Bet = data.Sum(x => x.Bet),
                    WinLoss = data.Sum(x => x.WinLoss),
                    Result = string.Empty
                });
            }

            var model = new BetListResultsRNGCasinoViewModel(data, total, criteria);
            model.Currency = CurrentSession.Currency;

            return View("~/Views/Shared/Account/BetListResults_RNGCasino.cshtml", model);
        }

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
            var model = criteria.IsValid()
                ? BetListService.GetRNGCasinoResultDetail(criteria)
                : new BetListResultsRNGCasinoDetailViewModel();

            model.Param = criteria;
            ExchangeRNGCasinoDetailValues(model);

            if (string.IsNullOrEmpty(group))
            {
                return this.View("~/Views/Shared/Account/RNGCasinoDetail/NoGameErr.cshtml");
            }

            if (model.CasinoDetailHeaderList != null && model.CasinoDetailHeaderList.Count > 0)
            {
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
            foreach (var detailItem in originalData.CasinoDetailList)
            {
                detailItem.Bet *= CurrentSession.ExchangeRate;
                detailItem.Win *= CurrentSession.ExchangeRate;
                detailItem.WinLoss *= CurrentSession.ExchangeRate;
            }

            var regexRemoveLetters = new Regex(@"[a-zA-Z\s]");
            foreach (var detailHeaderItem in originalData.CasinoDetailHeaderList)
            {
                detailHeaderItem.Win = string.IsNullOrEmpty(detailHeaderItem.Win.Trim())
                    ? "0.00"
                    : (decimal.Parse(regexRemoveLetters.Replace(detailHeaderItem.Win, string.Empty)) * CurrentSession.ExchangeRate).ToString();
                detailHeaderItem.BetValue *= CurrentSession.ExchangeRate;
                detailHeaderItem.BetBaseCurrencyValue *= CurrentSession.ExchangeRate;
                detailHeaderItem.CoinSize = string.IsNullOrEmpty(detailHeaderItem.CoinSize.Trim())
                    ? "0.00"
                    : (decimal.Parse(regexRemoveLetters.Replace(detailHeaderItem.CoinSize, string.Empty)) * CurrentSession.ExchangeRate).ToString();
                detailHeaderItem.CoinSizeBaseCurrencyValue *= CurrentSession.ExchangeRate;
                detailHeaderItem.CoinSizeValue *= CurrentSession.ExchangeRate;
                if (detailHeaderItem.CoinSizeAmount.Contains("X"))
                {
                    var coinSizeAmountParts = detailHeaderItem.CoinSizeAmount.Split('X');
                    detailHeaderItem.CoinSizeAmount = string.IsNullOrEmpty(coinSizeAmountParts[0])
                    ? "0.00"
                    : (decimal.Parse(regexRemoveLetters.Replace(coinSizeAmountParts[0].Trim(), string.Empty)) * CurrentSession.ExchangeRate).ToString();
                    detailHeaderItem.CoinSizeAmount = FormaterCurrency.Format(decimal.Parse(detailHeaderItem.CoinSizeAmount));
                    detailHeaderItem.CoinSizeAmount += " X" + coinSizeAmountParts[1];
                }
                detailHeaderItem.WinBaseCurrencyValue *= CurrentSession.ExchangeRate;
                detailHeaderItem.WinLossResult = string.IsNullOrEmpty(detailHeaderItem.WinLossResult.Trim())
                    ? "0.00"
                    : (decimal.Parse(regexRemoveLetters.Replace(detailHeaderItem.WinLossResult, string.Empty)) * CurrentSession.ExchangeRate).ToString();
                detailHeaderItem.WinLossResultBaseCurrecyValue *= CurrentSession.ExchangeRate;
                detailHeaderItem.WinResult = string.IsNullOrEmpty(detailHeaderItem.WinResult.Trim())
                    ? "0.00"
                    : (decimal.Parse(regexRemoveLetters.Replace(detailHeaderItem.WinResult, string.Empty)) * CurrentSession.ExchangeRate).ToString();
                detailHeaderItem.WinResultBaseCurrencyValue *= CurrentSession.ExchangeRate;
            }
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