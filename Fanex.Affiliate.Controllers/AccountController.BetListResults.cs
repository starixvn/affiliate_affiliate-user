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
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Web.Mvc;
    using Configurations;
    using Criteria;
    using Enumerations;
    using Fanex.Affiliate.DTO.BetList;
    using Fanex.Affiliate.Web;
    using Fanex.Common;
    using Fanex.Result.Core;
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

        private List<string> _resultsSupportedLanguages = new List<string>() { "en-US", "zh-TW", "zh-CN", "ja-JP", "th-TH", "ko-KR", "vi-VN" };
        
        #endregion

        #region BetList.Results
        /// <summary>
        /// The pop up result page.
        /// </summary>
        /// <param name="criteria">The metal model.</param>
        /// <returns>The action result.</returns>
        [HttpGet]
        public ActionResult BetListResults(AffBetlistService.BetListResultsCriteria criteria)
        {
            ViewBag.Data = GetPopUpData(criteria);
            return View();
        }

        /// <summary>
        /// Get data for pop-up page.
        /// </summary>
        /// <param name="criteria">The metal model.</param>
        /// <returns>HTML formatted of data.</returns>
        public string GetPopUpData(AffBetlistService.BetListResultsCriteria criteria)
        {
            var affBetlistService = new AffBetlistService.AffiliateAPI();
            var a = affBetlistService.GetBetlistResults(
                AppSettings.AffiliateName,
                CurrentSession.Language,
                criteria);

            return affBetlistService.GetBetlistResults(
                AppSettings.AffiliateName,
                CurrentSession.Language,
                criteria);
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
            criteria.CurrencyId = customerInfoBetList.CurrencyId;
            var data = criteria.IsValid()
                ? BetListService.GetRNGCasinoResult(criteria)
                : new List<RNGCasinoResult>();

            ExchangeRNGCasinoValues(data);
            var totalAmount = new List<RNGCasinoResult>();
            if (data != null)
            {
                totalAmount.Add(new RNGCasinoResult
                {
                    Stake = data.Sum(x => x.Stake),
                    Bet = data.Sum(x => x.Bet),
                    WinLoss = data.Sum(x => x.WinLoss),
                    Result = string.Empty
                });
            }

            var model = new BetListResultsRNGCasinoViewModel(data, totalAmount, criteria);
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
            var group = GetGroupByGameTypeId(criteria.GameTypeId);
            if (string.IsNullOrEmpty(group))
            {
                return View("~/Views/Shared/Account/RNGCasinoDetail/NoGameErr.cshtml");
            }

            var viewModel = criteria.IsValid()
                ? BetListService.GetRNGCasinoResultDetail(criteria)
                : new BetListResultsRNGCasinoDetailViewModel();

            viewModel.Param = criteria;
            ExchangeRNGCasinoDetailValues(viewModel);

            return (viewModel.CasinoDetailHeaderList != null && viewModel.CasinoDetailHeaderList.Count > 0)
                ? View(string.Format(ViewPathTemplate, group), viewModel)
                : View("~/Views/Shared/Account/RNGCasinoDetail/Noinfo.cshtml");
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
            
            foreach (var detailHeaderItem in originalData.CasinoDetailHeaderList)
            {
                detailHeaderItem.Win = ExchangeValueIncludingCurrency(detailHeaderItem.Win);
                detailHeaderItem.BetValue *= CurrentSession.ExchangeRate;
                detailHeaderItem.BetBaseCurrencyValue *= CurrentSession.ExchangeRate;
                detailHeaderItem.CoinSize = ExchangeValueIncludingCurrency(detailHeaderItem.CoinSize);
                detailHeaderItem.CoinSizeBaseCurrencyValue *= CurrentSession.ExchangeRate;
                detailHeaderItem.CoinSizeValue *= CurrentSession.ExchangeRate;
                if (detailHeaderItem.CoinSizeAmount.Contains("X"))
                {
                    var coinSizeAmountParts = detailHeaderItem.CoinSizeAmount.Split('X');
                    detailHeaderItem.CoinSizeAmount = ExchangeValueIncludingCurrency(coinSizeAmountParts[0]);
                    detailHeaderItem.CoinSizeAmount = FormaterCurrency.Format(decimal.Parse(detailHeaderItem.CoinSizeAmount));
                    detailHeaderItem.CoinSizeAmount += " X" + coinSizeAmountParts[1];
                }

                detailHeaderItem.WinBaseCurrencyValue *= CurrentSession.ExchangeRate;
                detailHeaderItem.WinLossResult = ExchangeValueIncludingCurrency(detailHeaderItem.WinLossResult);
                detailHeaderItem.WinLossResultBaseCurrecyValue *= CurrentSession.ExchangeRate;
                detailHeaderItem.WinResult = ExchangeValueIncludingCurrency(detailHeaderItem.WinResult);
                detailHeaderItem.WinResultBaseCurrencyValue *= CurrentSession.ExchangeRate;
            }
        }

        /// <summary>
        /// Exchange Value Including Currency code.
        /// </summary>
        /// <param name="originalValue">Value including currency code.</param>
        /// <returns>Exchanged value.</returns>
        public string ExchangeValueIncludingCurrency(string originalValue)
        {
            var regexRemoveLetters = new Regex(@"[a-zA-Z\s]");

            return string.IsNullOrEmpty(originalValue.Trim())
                ? "0.00"
                : (decimal.Parse(regexRemoveLetters.Replace(originalValue, string.Empty)) * CurrentSession.ExchangeRate).ToString();
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