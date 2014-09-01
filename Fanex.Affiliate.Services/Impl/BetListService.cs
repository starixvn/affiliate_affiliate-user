// ----------------------------------------------------------------------
// <copyright file="BetListService.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/12/05">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.Services.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using BetList.Core.Entities;
    using Common;
    using Common.Helper;
    using Criteria;
    using DataAccess.Impl;
    using DataAccess.Interface;
    using DTO;
    using DTO.BetList;
    using Interface;
    using Result.Core.DTO;
    using ViewModels;

    /// <summary>
    /// Account Service.
    /// </summary>
    public class BetListService : QueryService, IBetListService
    {
        #region Fields

        /// <summary>
        /// Define Account DataAccess.
        /// </summary>
        private readonly IBetListDataAccess _betlistDataAccess;

        private readonly string _subDomainPath = string.Empty;

        #endregion Fields

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BetListService" /> class.
        /// </summary>
        public BetListService()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BetListService" /> class.
        /// </summary>
        /// <param name="betlistDataAccess">Account data access.</param>
        private BetListService(IBetListDataAccess betlistDataAccess)
        {
            _betlistDataAccess = betlistDataAccess ?? new BetListDataAccess();
        }

        #endregion Constructor

        #region BetList

        /// <summary>
        /// Get Bet List.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Ticket results.</returns>
        public TicketResult GetBetList(BetListCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            return _betlistDataAccess.GetBetList(criteria);
        }

        /// <summary>
        /// Get Bet List Parlay.
        /// </summary>
        /// <param name="transId">Input transaction ID.</param>
        /// <returns>Ticket results.</returns>
        public TicketResult GetBetListParlay(long transId)
        {
            return _betlistDataAccess.GetBetListParlay(transId);
        }

        /// <summary>
        /// Get System Parlay Detail data.
        /// </summary>
        /// <param name="criteria">The metal model.</param>
        /// <returns>The system parlay.</returns>
        public SystemParlayData GetSystemParlayDetail(SystemParlayDetailCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            return _betlistDataAccess.GetSystemParlayDetail(criteria);
        }

        /// <summary>
        /// Get exchange rate.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Exchange rate.</returns>
        public float GetExchangeRate(ExchangeRateCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            return _betlistDataAccess.GetExchangeRate(criteria);
        }

        /// <summary>
        /// Get balance up and move bet dates.
        /// </summary>
        /// <param name="criteria">Balance up and move bet criteria.</param>
        public void GetBalanceUpMoveBetDate(BalanceUpMoveBetCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            _betlistDataAccess.GetBalanceUpMoveBetDate(criteria);
        }

        /// <summary>
        /// Get player's username.
        /// </summary>
        /// <param name="playerUsername">Affiliate username.</param>
        /// <returns>Return player's Info.</returns>
        public CustomerInfoBetList GetPlayerIbcInfo(string playerUsername)
        {
            return _betlistDataAccess.GetPlayerIbcInfo(playerUsername);
        }

        #endregion

        #region BetList.Results

        public IList<Sportbook> GetPopUpMatch(int matchId, int licenseeId, int userId)
        {
            var data = _betlistDataAccess.GetPopUpMatch(matchId, -1, licenseeId, userId);
            return BindSportsbook(data);
        }

        /// <summary>
        /// Get parlay match for pop up page.
        /// </summary>
        /// <param name="refNo">The reference no.</param>
        /// <returns>The parlay data list.</returns>
        [SuppressMessage(
           "StyleCop.CSharp.Nexcel.NexcelCustomRules",
           "SP2101:MethodMustNotContainMoreLinesThan",
           Justification = "Reviewed. Suppression is OK here.")]
        public IList<Parlay> GetPopUpParlayMatch(long refNo)
        {
            var data = _betlistDataAccess.GetPopUpParalayMatch(refNo);
            if (data != null && data.Rows.Count > 0)
            {
                var dataList = data.AsEnumerable();
                var dataParlay = new List<Parlay>();

                foreach (var row in dataList)
                {
                    var leagueId = int.Parse(row["LeagueID"].ToString());
                    if (dataParlay.All(i => i.LeagueId != leagueId))
                    {
                        dataParlay.Add(
                            new Parlay
                            {
                                LeagueId = leagueId,
                                SportId = int.Parse(row["sporttype"].ToString()),
                                Sportbook =
                                    dataList.Where(i => i["LeagueID"].ToString() == leagueId.ToString(CultureInfo.InvariantCulture))
                                        .Select(
                                            i =>
                                                new Sportbook
                                                {
                                                    AwayId = int.Parse(i["awayID"].ToString()),
                                                    HomeId = int.Parse(i["homeID"].ToString()),
                                                    BetStatus =
                                                        i["betstatus"] != DBNull.Value
                                                            ? i["betstatus"].ToString()
                                                            : string.Empty,
                                                    LeagueId = leagueId,
                                                    MatchId = int.Parse(i["matchid"].ToString()),
                                                    EventStatus = i["eventstatus"].ToString(),
                                                    FinalAwayScore =
                                                        i["finalawayscore"] != DBNull.Value
                                                            ? int.Parse(i["finalawayscore"].ToString())
                                                            : new int?(),
                                                    HtAwayScore =
                                                        i["htawayscore"] != DBNull.Value
                                                            ? int.Parse(i["htawayscore"].ToString())
                                                            : new int?(),
                                                    Color = i.Table.Columns.Contains("lcolor") &&
                                                        i["lcolor"] != DBNull.Value
                                                            ? i["lcolor"].ToString()
                                                            : string.Empty,
                                                    FinalHomeScore =
                                                        i["finalhomescore"] != DBNull.Value
                                                            ? int.Parse(i["finalhomescore"].ToString())
                                                            : new int?(),
                                                    HtHomeScore =
                                                        i["finalhomescore"] != DBNull.Value
                                                            ? int.Parse(i["hthomescore"].ToString())
                                                            : new int?(),
                                                    EventDate = DateTime.Parse(i["eventdate"].ToString()),
                                                    IsNeutral = bool.Parse(i["IsNeutral"].ToString()),
                                                    IsPending = bool.Parse(i["IsPending"].ToString()),
                                                    KickOffTime = DateTime.Parse(i["kickofftime"].ToString()),
                                                    MatchCode = i["matchcode"].ToString(),
                                                    GoalRecord = i["goalrecord"].ToString(),
                                                    ResultValue = "6-1 *30-40 40-30* *40-0 40-0* *40-0 40-0* *40-0",
                                                    IsHTCal = Convert.ToBoolean(i["IsHTCal"]),
                                                    Calculate = Convert.ToBoolean(i["Calculate"]),
                                                    RefNo = refNo.ToString(CultureInfo.InvariantCulture),
                                                })
                                        .ToList()
                            });
                    }
                }

                return dataParlay;
            }

            return new List<Parlay>();
        }

        public IList<NumberGame> GetPopUpNumberGameMatchs(int matchId)
        {
            var data = _betlistDataAccess.GetPopUpNumberGameMatch(matchId);
            return BindingNumberGame(data);
        }

        public IList<LiveCasino> GetPopUpLiveCasinoMatch(int matchId, int licenseeId, int userId)
        {
            var dataLc = _betlistDataAccess.GetPopUpMatch(matchId, -1, licenseeId, userId);
            return BindingLiveCasino(dataLc);
        }

        /// <summary>
        /// Get racing data for popup page.
        /// </summary>
        /// <param name="matchId">The match id.</param>
        /// <param name="raceNo">The reference number.</param>
        /// <param name="licenseeId">The licensee id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>The racing data list.</returns>
        public IList<Racing> GetPopUpRacingMatch(int matchId, int raceNo, int licenseeId, int userId)
        {
            var dataRc = _betlistDataAccess.GetPopUpMatch(matchId, raceNo, licenseeId, userId);
            return BindingRacing(dataRc);
        }

        #endregion

        #region BetList.Results.RNGCasino

        /// <summary>
        /// Get RNG Casino Result.
        /// </summary>
        /// <param name="criteria">The metal model.</param>
        /// <returns>The RNG Casino result list.</returns>
        public List<RNGCasinoResult> GetRNGCasinoResult(BetListResultsRNGCasinoCriteria criteria)
        {
            var casinoList = new List<RNGCasinoResult>();
            DataTable dataTable;
            GetResultFromAPI(criteria, out dataTable);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    var dataRow = dataTable.Rows[i];
                    var casino = MapDataToCasinoItem(dataRow);

                    casino.Index = i + 1;
                    casino.Result = casino.Result.Replace("/Form", this._subDomainPath + "/Form");
                    casino.Result = casino.Result.Replace("/img", this._subDomainPath + "/img");
                    if (criteria.IsShowPic)
                    {
                        casino.Result = casino.Result.Replace("<img", "<br /><img");
                    }

                    casinoList.Add(casino);
                }
            }

            return casinoList;
        }

        /// <summary>
        /// Get RNG casino detail result.
        /// </summary>
        /// <param name="criteria">The metal model.</param>
        /// <returns>The detail result.</returns>
        public BetListResultsRNGCasinoDetailViewModel GetRNGCasinoResultDetail(BetListResultsRNGCasinoDetailCriteria criteria)
        {
            var resultModel = new BetListResultsRNGCasinoDetailViewModel();
            var casinoLib = new CasinoLibrary.SBAgent.Report();
            var arrayTables = casinoLib.WinLossDetail(criteria.UserBetId);

            if (arrayTables != null && arrayTables.Length > 1)
            {
                var headTable = arrayTables[0];
                var detailTable = arrayTables[1];

                if (headTable != null && headTable.Rows.Count > 0 && detailTable != null)
                {
                    ProcessResultDetail(resultModel, headTable, detailTable);
                }
            }

            return resultModel;
        }

        #endregion

        #region BetList.Results.Utilities

        /// <summary>
        /// Mapping data to Sports-book DTO.
        /// </summary>
        /// <param name="data">The data table.</param>
        /// <returns>The sports-book data list.</returns>
        private static IList<Sportbook> BindSportsbook(DataTable data)
        {
            if (data != null && data.Rows.Count > 0)
            {
                return
                    data.AsEnumerable()
                        .Select(
                            i =>
                                new Sportbook
                                {
                                    AwayId = int.Parse(i["awayID"].ToString()),
                                    HomeId = int.Parse(i["homeID"].ToString()),
                                    BetStatus =
                                        i["betstatus"] != DBNull.Value ? i["betstatus"].ToString() : string.Empty,
                                    LeagueId = int.Parse(i["leagueid"].ToString()),
                                    MatchId = int.Parse(i["matchid"].ToString()),
                                    EventStatus = i["eventstatus"].ToString(),
                                    FinalAwayScore =
                                        i["finalawayscore"] != DBNull.Value
                                            ? int.Parse(i["finalawayscore"].ToString())
                                            : new int?(),
                                    HtAwayScore =
                                        i["htawayscore"] != DBNull.Value ? int.Parse(i["htawayscore"].ToString()) : -1,
                                    Color = i.Table.Columns.Contains("lcolor") && i["lcolor"] != DBNull.Value ? i["lcolor"].ToString() : string.Empty,
                                    FinalHomeScore =
                                        i["finalhomescore"] != DBNull.Value
                                            ? int.Parse(i["finalhomescore"].ToString())
                                            : new int?(),
                                    HtHomeScore =
                                        i["hthomescore"] != DBNull.Value
                                            ? int.Parse(i["hthomescore"].ToString())
                                            : new int?(),
                                    EventDate = DateTime.Parse(i["eventdate"].ToString()),
                                    IsNeutral = bool.Parse(i["IsNeutral"].ToString()),
                                    IsPending = bool.Parse(i["IsPending"].ToString()),
                                    KickOffTime = DateTime.Parse(i["kickofftime"].ToString()),
                                    MatchCode = i["matchcode"].ToString(),
                                    GoalRecord = i["goalrecord"].ToString(),
                                    ResultValue = "6-1 *30-40 40-30* *40-0 40-0* *40-0 40-0* *40-0",
                                    IsHTCal = Convert.ToBoolean(i["IsHTCal"]),
                                    Calculate = Convert.ToBoolean(i["Calculate"]),
                                    RefNo = "12"
                                })
                        .ToList();
            }

            return new List<Sportbook>();
        }

        /// <summary>
        /// Mapping data to NG DTO.
        /// </summary>
        /// <param name="dataNg">The data table.</param>
        /// <returns>The NG data list.</returns>
        private static IList<NumberGame> BindingNumberGame(DataTable dataNg)
        {
            if (dataNg != null && dataNg.Rows.Count > 0)
            {
                dataNg.AsEnumerable().OrderBy(i => i["leagueId"]).OrderBy(i => i["matchid"]).OrderBy(i => i["seq"]);
                var dataListNg = new List<NumberGame>();
                foreach (var mtc in dataNg.AsEnumerable())
                {
                    var matchId = int.Parse(mtc["matchid"].ToString());
                    var existMatch = dataListNg.SingleOrDefault(i => i.MatchId == matchId);
                    if (existMatch != null)
                    {
                        existMatch.Seg.Add(int.Parse(mtc["seq"].ToString()));
                        existMatch.Ball.Add(int.Parse(mtc["number"].ToString()));
                    }
                    else
                    {
                        dataListNg.Add(
                            new NumberGame
                            {
                                LeagueId = int.Parse(mtc["leagueid"].ToString()),
                                MatchId = int.Parse(mtc["matchid"].ToString()),
                                EventStatus = mtc["eventstatus"].ToString(),
                                EventDate = DateTime.Parse(mtc["eventdate"].ToString()),
                                IsPending = mtc.Table.Columns.Contains("isrefund") && mtc["isrefund"].ToString() != "0",
                                KickOffTime = DateTime.Parse(mtc["kickofftime"].ToString()),
                                MatchCode = mtc["matchcode"].ToString(),
                                Seg = new List<int> { int.Parse(mtc["seq"].ToString()) },
                                Ball = new List<int> { int.Parse(mtc["number"].ToString()) }
                            });
                    }
                }

                return dataListNg;
            }

            return new List<NumberGame>();
        }

        /// <summary>
        /// Mapping data to Live casino DTO.
        /// </summary>
        /// <param name="dataLc">The data table.</param>
        /// <returns>The live casino data list.</returns>
        private static IList<LiveCasino> BindingLiveCasino(DataTable dataLc)
        {
            if (dataLc != null && dataLc.Rows.Count > 0)
            {
                return
                    dataLc.AsEnumerable()
                        .Select(
                            i =>
                                new LiveCasino
                                {
                                    ShowTime = i["showtime"].ToString(),
                                    LeagueId = int.Parse(i["leagueid"].ToString()),
                                    EventStatus = i["eventstatus"].ToString(),
                                    FinalAwayScore =
                                        i["finalawayscore"] != DBNull.Value ? int.Parse(i["finalawayscore"].ToString()) : -1,
                                    FinalHomeScore =
                                        i["finalhomescore"] != DBNull.Value ? int.Parse(i["finalhomescore"].ToString()) : -1,
                                    EventDate = DateTime.Parse(i["eventdate"].ToString()),
                                    IsPending = bool.Parse(i["IsPending"].ToString()),
                                    KickOffTime = DateTime.Parse(i["kickofftime"].ToString()),
                                    IsHTCal = Convert.ToBoolean(i["IsHTCal"]),
                                    Calculate = Convert.ToBoolean(i["Calculate"]),
                                    BetStatus = i["betstatus"] != DBNull.Value ? i["betstatus"].ToString() : string.Empty,
                                    Bettype = int.Parse(i["homeid2"].ToString())
                                })
                        .ToList();
            }

            return new List<LiveCasino>();
        }

        /// <summary>
        /// Mapping racing data from data table.
        /// </summary>
        /// <param name="dataRc">The data table.</param>
        /// <returns>The racing data list.</returns>
        private static IList<Racing> BindingRacing(DataTable dataRc)
        {
            if (dataRc != null && dataRc.Rows.Count > 0)
            {
                return
                    dataRc.AsEnumerable()
                        .Select(
                            i =>
                                new Racing
                                {
                                    LeagueId = int.Parse(i["meetingid"].ToString()),
                                    EngineId = i["HorseId"].ToString(),
                                    EngineNo = i["HorseNo"].ToString(),
                                    IsFavor = i["IsFavor"] != DBNull.Value && i["IsFavor"].ToString().Equals("1") ? true : false,
                                    IsSCR = i["IsSCR"] != DBNull.Value && i["IsSCR"].ToString().Equals("1") ? true : false,
                                    Place = int.Parse(i["place"].ToString()),
                                    PlaceOdds = decimal.Parse(i["placeodds"].ToString()),
                                    WinOdds = decimal.Parse(i["odds"].ToString()),
                                    RaceName = i["racename"] != DBNull.Value ? int.Parse(i["racename"].ToString()) : 0,
                                    KickOffTime = i["kickofftime"] != DBNull.Value ? DateTime.Parse(i["kickofftime"].ToString()) : DateTime.MinValue,
                                    Favourite = i["IsFavor"] != DBNull.Value ? i["IsFavor"].ToString() : string.Empty,
                                    RowCount = int.Parse(i["rowcount"].ToString()),
                                    SCROfMatch = i["SCR"].ToString(),
                                    TFOfMatch = i["TF"].ToString()
                                })
                        .ToList();
            }

            return new List<Racing>();
        }

        #endregion

        #region BetList.Results.RNGCasino.Utilities

        /// <summary>
        /// Get result from API.
        /// </summary>
        /// <param name="criteria">The metal model.</param>
        /// <param name="dataTable">The data table.</param>
        private static void GetResultFromAPI(
            BetListResultsRNGCasinoCriteria criteria,
            out DataTable dataTable)
        {
            var casinoLib = new CasinoLibrary.SBAgent.Report();
            dataTable = casinoLib.WinLossHandHistory(criteria.RefNo, criteria.BetType, RNGCasinoResult._CASINO_GROUP1, criteria.IsShowPic);
            if (dataTable == null)
            {
                dataTable = casinoLib.RealTimeWinLossHandHistory(
                criteria.FromDate,
                criteria.ToDate,
                criteria.CustId,
                RNGCasinoResult._CASINO_GROUP1,
                criteria.IsShowPic);
            }
        }

        /// <summary>
        /// Process result detail.
        /// </summary>
        /// <param name="resultModel">The result.</param>
        /// <param name="headTable">The header data.</param>
        /// <param name="detailTable">The detail data.</param>
        private void ProcessResultDetail(
            BetListResultsRNGCasinoDetailViewModel resultModel,
            DataTable headTable,
            DataTable detailTable)
        {
            foreach (DataRow dr in headTable.Rows)
            {
                var headItem = MapDataToHeaderItem(dr);
                resultModel.CasinoDetailHeaderList.Add(headItem);
            }

            foreach (DataRow dr in detailTable.Rows)
            {
                var detailItem = MapDataToDetailItem(dr);
                resultModel.CasinoDetailList.Add(detailItem);
            }
        }

        /// <summary>
        /// Get columns by language.
        /// </summary>
        /// <param name="columnName">The original column name.</param>
        /// <returns>The language column.</returns>
        private string GetColumnMultiLang(string columnName)
        {
            var langKey = "_En";

            return columnName + langKey;
        }

        private string GetDataRowExtension(DataRow dr, string columnName)
        {
            return dr.Table.Columns.Contains(columnName) ? dr[columnName].ToString() : string.Empty;
        }

        private string GetDataRowExtensionMultiple(DataRow dr, string columnName, string extColumnName)
        {
            return dr.Table.Columns.Contains(columnName)
                ? dr[columnName].ToString()
                : dr.Table.Columns.Contains(extColumnName)
                    ? dr[extColumnName].ToString()
                    : string.Empty;
        }

        /// <summary>
        /// Mapping data from API to DTO.
        /// </summary>
        /// <param name="dr">The data row.</param>
        /// <returns>The RNG Casino.</returns>
        private RNGCasinoResult MapDataToCasinoItem(DataRow dr)
        {
            var gameTypeName = GetDataRowExtension(dr, GetColumnMultiLang("GameTypeName"));
            gameTypeName = gameTypeName.Replace("<BR>", " ");

            return new RNGCasinoResult
            {
                SessionLogId = GetDataRowExtension(dr, "SessionLogId"),
                PlayerHistoryNo = GetDataRowExtension(dr, "PlayerHistoryNo"),
                GameId = GetDataRowExtension(dr, "GameId"),
                GameNo = GetDataRowExtension(dr, "GameNo"),
                GameTypeId = Converter.ToInt(GetDataRowExtension(dr, "GameTypeId")),
                BeginBalance = Converter.ToDecimal(GetDataRowExtension(dr, "BeginBalance")),
                EndBlance = Converter.ToDecimal(GetDataRowExtension(dr, "EndBalance")),
                Bet = Converter.ToDecimal(GetDataRowExtension(dr, "Bet")),
                Stake = Converter.ToDecimal(GetDataRowExtension(dr, "Stake")),
                Win = Converter.ToDecimal(GetDataRowExtension(dr, "Win")),
                WinLoss = Converter.ToDecimal(GetDataRowExtension(dr, "WinLoss")),
                UserBetId = Converter.ToInt(GetDataRowExtension(dr, "UserBetID")),
                GameTableId = Converter.ToInt(GetDataRowExtension(dr, "GameTableID")),
                StartTime = Converter.ToDateTime(GetDataRowExtension(dr, "StartTime")).ToString("M/d/yyyy\nhh:mm:ss tt"),
                EndTime = Converter.ToDateTime(GetDataRowExtension(dr, "EndTime")).ToString("M/d/yyyy\nhh:mm:ss tt"),
                ShoeNo = GetDataRowExtension(dr, "ShoeNo"),
                HandNo = GetDataRowExtension(dr, "HandNo"),
                ResultId = GetDataRowExtension(dr, "ResultId"),
                UserName = GetDataRowExtension(dr, "UserName"),
                Result = GetDataRowExtension(dr, GetColumnMultiLang("Result")),
                Status = Converter.ToInt(GetDataRowExtension(dr, "Status")),
                GameGroupId = Converter.ToInt(GetDataRowExtension(dr, "GameGroupId")),
                GameTypeNo = GetDataRowExtension(dr, "GameTypeNo"),
                GameTypeName = gameTypeName,
                GameGroupName = GetDataRowExtension(dr, GetColumnMultiLang("GameGroupName")),
                Currency = GetDataRowExtension(dr, "Currency"),
                TableName = GetDataRowExtension(dr, "TableName"),
            };
        }

        /// <summary>
        /// Map data from API.
        /// </summary>
        /// <param name="dr">The data row.</param>
        /// <returns>The detail data.</returns>
        private RNGCasinoResultWinlossDetail MapDataToDetailItem(DataRow dr)
        {
            return new RNGCasinoResultWinlossDetail
            {
                BetTypeId = Converter.ToInt(GetDataRowExtension(dr, "BetTypeId")),
                BetTypeName = GetDataRowExtension(dr, GetColumnMultiLang("BetTypeName")),
                Bet = Converter.ToDecimal(GetDataRowExtension(dr, "Bet")),
                Odds = Converter.ToDecimal(GetDataRowExtension(dr, "Odds")),
                Win = Converter.ToDecimal(GetDataRowExtension(dr, "Win")),
                WinLoss = Converter.ToDecimal(GetDataRowExtension(dr, "WinLoss")),
                GameTypeId = Converter.ToInt(GetDataRowExtension(dr, "GameTypeID")),
                Currency = GetDataRowExtension(dr, "Currency"),
                BetContent = GetDataRowExtension(dr, "BetContent"),
            };
        }

        /// <summary>
        /// Map data from API for header section.
        /// </summary>
        /// <param name="dr">The data row.</param>
        /// <returns>The detail header data.</returns>
        private RNGCasinoResultWinlossDetailHeader MapDataToHeaderItem(DataRow dr)
        {
            return new RNGCasinoResultWinlossDetailHeader
            {
                ShowType = GetDataRowExtension(dr, "ShowType"),
                TableName = GetDataRowExtension(dr, GetColumnMultiLang("TableName")),
                BetTime = Converter.ToDateTime(GetDataRowExtension(dr, "BetTime")),
                UserName = GetDataRowExtension(dr, "UserName"),
                ResultName = GetDataRowExtension(dr, GetColumnMultiLang("ResultName")),
                Result = GetDataRowExtension(dr, "Result"),
                PlayerHistoryNo = GetDataRowExtension(dr, "PlayerHistoryNo_Result"),
                Symbol = GetDataRowExtension(dr, "Symbol"),
                CoinSize = GetDataRowExtension(dr, "CoinSize"),
                CoinSizeAmount = GetDataRowExtension(dr, GetColumnMultiLang("CoinSizeAmount")),
                Bet = GetDataRowExtension(dr, "Bet"),
                Odds = GetDataRowExtension(dr, "Odds"),
                Multiplier = GetDataRowExtensionMultiple(dr, "Multiplier_Result", "Multiplier"),
                Win = GetDataRowExtension(dr, "Win"),
                WinResult = GetDataRowExtensionMultiple(dr, "Win_Result", "WinResult"),
                ModeResult = GetDataRowExtension(dr, GetColumnMultiLang("ModeResult")),
                AutoSpinResult = GetDataRowExtension(dr, GetColumnMultiLang("AutoSpinResult")),
                FreeSpinResult = GetDataRowExtension(dr, GetColumnMultiLang("FreeSpinResult")),
                BonusName = GetDataRowExtension(dr, GetColumnMultiLang("BonusName")),
                LineNo = GetDataRowExtension(dr, "LineNo"),
                TotalLineBet = GetDataRowExtension(dr, "TotalLineBet"),
                RoundTypeResult = GetDataRowExtension(dr, GetColumnMultiLang("RoundTypeResult")),
                BetPerLine = GetDataRowExtension(dr, "BetPerLine"),
                RankResult = GetDataRowExtension(dr, GetColumnMultiLang("RankResult")),
                FinalHandResult = GetDataRowExtension(dr, "FinalResult"),
                BetTypeName = GetDataRowExtension(dr, GetColumnMultiLang("BetTypeName")),
                AutoPlayResult = GetDataRowExtension(dr, GetColumnMultiLang("AutoPlayResult")),
                ActionName = GetDataRowExtension(dr, GetColumnMultiLang("ActionName")),
                WinLossResult = GetDataRowExtension(dr, "WinLoss_Result"),
                HandNoResult = GetDataRowExtension(dr, "HandNoResult"),
                PickNo = GetDataRowExtension(dr, "Pick_No"),
                HitNo = GetDataRowExtension(dr, "Hit_No"),
                PickCount = Converter.ToInt(GetDataRowExtension(dr, "Pick_Count")),
                HitCount = Converter.ToInt(GetDataRowExtension(dr, "Hit_Count")),
            };
        }

        #endregion
    }
}