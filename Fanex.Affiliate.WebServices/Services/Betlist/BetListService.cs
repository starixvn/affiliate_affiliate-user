// ----------------------------------------------------------------------
// <copyright file="BetListService.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/12/05">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.WebServices.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using BetList.Core.Entities;
    using Common.Helper;
    using Criteria;
    using DataAccess;
    using DTO.BetList;
    using Result.Core.DTO;

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
        /// <param name="criteria">Bet list parlay.</param>
        /// <returns>Ticket results.</returns>
        public TicketResult GetBetListParlay(BetListParlayCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            return _betlistDataAccess.GetBetListParlay(criteria);
        }

        /// <summary>
        /// Get System Parlay Detail data.
        /// </summary>
        /// <param name="criteria">The metal model.</param>
        /// <returns>The system parlay.</returns>
        public SystemParlayData GetSystemParlayDetail(BetListSystemParlayDetailCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            return _betlistDataAccess.GetSystemParlayDetail(criteria);
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
        /// <param name="affiliateGroup">Affiliate group.</param>
        /// <param name="playerUsername">Affiliate username.</param>
        /// <returns>Return player's Info.</returns>
        public CustomerInfoBetList GetPlayerIbcInfo(string affiliateGroup, string playerUsername)
        {
            return _betlistDataAccess.GetPlayerIbcInfo(affiliateGroup, playerUsername);
        }

        #endregion

        #region BetList.Results

        public IList<Sportsbook> GetPopUpMatch(string affiliateGroup, int matchId)
        {
            var data = _betlistDataAccess.GetPopUpMatch(affiliateGroup, matchId, -1);
            return BindSportsbook(data);
        }

        /// <summary>
        /// Get parlay match for pop up page.
        /// </summary>
        /// <param name="affiliateGroup">Affiliate Group.</param>
        /// <param name="refNo">The reference no.</param>
        /// <returns>The parlay data list.</returns>
        [SuppressMessage(
           "StyleCop.CSharp.Nexcel.NexcelCustomRules",
           "SP2101:MethodMustNotContainMoreLinesThan",
           Justification = "Reviewed. Suppression is OK here.")]
        public IList<Parlay> GetPopUpParlayMatch(string affiliateGroup, long refNo)
        {
            var data = _betlistDataAccess.GetPopUpParlayMatch(affiliateGroup, refNo);
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
                                                new Sportsbook
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

        public IList<NumberGame> GetPopUpNumberGameMatchs(string affiliateGroup, int matchId)
        {
            var data = _betlistDataAccess.GetPopUpNumberGameMatch(affiliateGroup, matchId);
            return BindingNumberGame(data);
        }

        public IList<LiveCasino> GetPopUpLiveCasinoMatch(string affiliateGroup, int matchId)
        {
            var dataLc = _betlistDataAccess.GetPopUpMatch(affiliateGroup, matchId, -1);
            return BindingLiveCasino(dataLc);
        }

        /// <summary>
        /// Get racing data for popup page.
        /// </summary>
        /// <param name="affiliateGroup">Affiliate Group.</param>
        /// <param name="matchId">The match id.</param>
        /// <param name="raceNo">The reference number.</param>
        /// <returns>The racing data list.</returns>
        public IList<Racing> GetPopUpRacingMatch(string affiliateGroup, int matchId, int raceNo)
        {
            var dataRc = _betlistDataAccess.GetPopUpMatch(affiliateGroup, matchId, raceNo);
            return BindingRacing(dataRc);
        }

        #endregion

        #region BetList.Results.RNGCasino
        
        #endregion

        #region BetList.Results.Utilities

        /// <summary>
        /// Mapping data to Sports-book DTO.
        /// </summary>
        /// <param name="data">The data table.</param>
        /// <returns>The sports-book data list.</returns>
        private static IList<Sportsbook> BindSportsbook(DataTable data)
        {
            if (data != null && data.Rows.Count > 0)
            {
                return
                    data.AsEnumerable()
                        .Select(
                            i =>
                                new Sportsbook
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
                                    IsHTCal = Convert.ToBoolean(i["IsHTCal"]),
                                    Calculate = Convert.ToBoolean(i["Calculate"]),
                                    RefNo = "12"
                                })
                        .ToList();
            }

            return new List<Sportsbook>();
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
                                    Bettype = int.Parse(i["homeid"].ToString())
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
        
        #endregion
    }
}