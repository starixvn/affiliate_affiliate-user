// ----------------------------------------------------------------------
// <copyright file="BetListDataAccess.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/12/03">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.DataAccess
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using Criteria;
    using Data;
    using DTO;
    using DTO.BetList;
    using Fanex.BetList.Core.Entities;
    using Fanex.Common;

    /// <summary>
    /// Account data access.
    /// </summary>
    public class BetListDataAccess : IBetListDataAccess
    {
        #region BetList

        /// <summary>
        /// Read main ticket data.
        /// </summary>
        /// <param name="dataTable">Data table Parameter value.</param>
        /// <param name="baseCurrency"> Parameter value.</param>
        /// <returns>Result value.</returns>
        [SuppressMessage(
            "StyleCop.CSharp.Nexcel.NexcelCustomRules",
            "SP2101:MethodMustNotContainMoreLinesThan",
            Justification = "Reviewed. Suppression is OK here.")]
        public static IList<AffTicket> ReadMainTicketData(DataTable dataTable, string baseCurrency)
        {
            var playerCommCol = "playercomm" + baseCurrency;
            var commCol = "comm" + baseCurrency;
            var licenseeWlCol = "licenseewl" + baseCurrency;
            var actualStakeCol = "actual_stake" + baseCurrency;

            var columnCollection = dataTable.Columns;

            // String
            var containsUsername = columnCollection.Contains("username");
            var containsRefNo = columnCollection.Contains("refno");
            var containsMatchCode = columnCollection.Contains("matchcode");
            var containsStatus = columnCollection.Contains("status");
            var containsBetTeam = columnCollection.Contains("betteam");
            var containsBetStatus = columnCollection.Contains("betstatus");
            var containsShowTime = columnCollection.Contains("showtime");
            var containsOddsType = columnCollection.Contains("oddstype");
            var containsExternalId = columnCollection.Contains("nickname");
            var containsRace = columnCollection.Contains("race");

            // Boolean
            var containsLiveIndicator = columnCollection.Contains("liveindicator");
            var containsNeutral = columnCollection.Contains("isneutral");

            // Int
            var containsSportType = columnCollection.Contains("sporttype");
            var containsBetType = columnCollection.Contains("bettype");
            var containsLeagueId = columnCollection.Contains("leagueid");
            var containsHomeId = columnCollection.Contains("homeid");
            var containsAwayId = columnCollection.Contains("awayid");
            var containsMatchId = columnCollection.Contains("matchid");
            var containsCustId = columnCollection.Contains("custid");
            var containsCurrencyId = columnCollection.Contains("currencyid");
            var containsLiveHomeScore = columnCollection.Contains("livehomescore");
            var containsLiveAwayScore = columnCollection.Contains("liveawayscore");

            // Long
            var containsTransId = columnCollection.Contains("transid");
            var containsBetId = columnCollection.Contains("betid");

            // DateTime
            var containsWinlostDate = columnCollection.Contains("winlostdate");
            var containsTransDate = columnCollection.Contains("transdate");

            // Money
            var containsPlayerComm = columnCollection.Contains(playerCommCol);
            var containsComm = columnCollection.Contains(commCol);
            var containsLicenseeWl = columnCollection.Contains(licenseeWlCol);
            var containsWinlost = columnCollection.Contains("winlost");
            var containsStake = columnCollection.Contains("stake");
            var containsActualStake = columnCollection.Contains(actualStakeCol);
            var containsOdds = columnCollection.Contains("odds");
            var containsMasterOdds = columnCollection.Contains("modds");
            var containsHandicap1 = columnCollection.Contains("hdp1");
            var containsHandicap2 = columnCollection.Contains("hdp2");

            var result = new List<AffTicket>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                var ticket = new AffTicket();

                // String
                ticket.UserName = containsUsername ? Converter.ToString2(dataRow["username"]) : string.Empty;
                ticket.RefNo = containsRefNo ? Converter.ToString2(dataRow["refno"]) : string.Empty;
                ticket.MatchCode = containsMatchCode ? Converter.ToString2(dataRow["matchcode"]) : string.Empty;
                ticket.Status = containsStatus ? Converter.ToString2(dataRow["status"]) : string.Empty;
                ticket.BetTeam = containsBetTeam ? Converter.ToString2(dataRow["betteam"]) : string.Empty;
                ticket.EventStatus = containsBetStatus ? Converter.ToString2(dataRow["betstatus"]) : string.Empty;
                ticket.ShowTime = containsShowTime ? Converter.ToString2(dataRow["showtime"]) : string.Empty;
                ticket.OddsType = containsOddsType ? Converter.ToString2(dataRow["oddstype"]) : "0";
                ticket.ExternalId = containsExternalId ? Converter.ToString2(dataRow["nickname"]) : string.Empty;
                ticket.Race = containsRace ? Converter.ToString2(dataRow["race"]) : string.Empty;

                // Boolean
                ticket.IsLive = containsLiveIndicator ? ReadBoolean(dataRow["liveindicator"]) : false;
                ticket.IsNeutral = containsNeutral ? ReadBoolean(dataRow["isneutral"]) : false;

                // Int
                ticket.SportTypeId = containsSportType ? Converter.ToInt(dataRow["sporttype"]) : 0;
                ticket.BetTypeId = containsBetType ? Converter.ToInt(dataRow["bettype"]) : 0;
                ticket.LeagueId = containsLeagueId ? Converter.ToInt(dataRow["leagueid"]) : 0;
                ticket.HomeId = containsHomeId ? Converter.ToInt(dataRow["homeid"]) : 0;
                ticket.AwayId = containsAwayId ? Converter.ToInt(dataRow["awayid"]) : 0;
                ticket.MatchId = containsMatchId ? Converter.ToInt(dataRow["matchid"]) : 0;
                ticket.CustId = containsCustId ? Converter.ToInt(dataRow["custid"]) : 0;
                ticket.CurrencyId = containsCurrencyId ? Converter.ToInt(dataRow["currencyid"]) : 0;
                ticket.LiveHomeScore = containsLiveHomeScore ? Converter.ToInt(dataRow["livehomescore"]) : 0;
                ticket.LiveAwayScore = containsLiveAwayScore ? Converter.ToInt(dataRow["liveawayscore"]) : 0;

                // Long
                ticket.TransId = containsTransId ? Convert.ToInt64(dataRow["transid"]) : 0;
                ticket.BetId = containsBetId && !Convert.IsDBNull(dataRow["betid"]) ? new long?(Convert.ToInt64(dataRow["betid"])) : null;

                // DateTime
                ticket.WinlostDate = containsWinlostDate ? Converter.ToDateTime(dataRow["winlostdate"]) : DateTime.Now;
                ticket.TransDate = containsTransDate ? Converter.ToDateTime(dataRow["transdate"]) : DateTime.Now;

                // Money
                ticket.PlayerComm = containsPlayerComm ? Converter.ToDecimal(dataRow[playerCommCol]) : 0;
                ticket.Comm = containsComm ? Converter.ToDecimal(dataRow[commCol]) : 0;
                ticket.LicenseeWl = containsLicenseeWl ? Converter.ToDecimal(dataRow[licenseeWlCol]) : 0;
                ticket.Winlost = containsWinlost ? Converter.ToDecimal(dataRow["winlost"]) : 0;
                ticket.Stake = containsStake ? Converter.ToDecimal(dataRow["stake"]) : 0;
                ticket.ActualStake = containsActualStake ? Converter.ToDecimal(dataRow[actualStakeCol]) : 0;
                ticket.Odds = containsOdds ? Converter.ToDecimal(dataRow["odds"]) : 0;
                ticket.MasterOdds = containsMasterOdds ? Converter.ToDecimal(dataRow["modds"]) : 0;
                ticket.Handicap1 = containsHandicap1 ? Converter.ToDecimal(dataRow["hdp1"]) : 0;
                ticket.Handicap2 = containsHandicap2 ? Converter.ToDecimal(dataRow["hdp2"]) : 0;

                result.Add(ticket);
            }

            return result;
        }

        /// <summary>
        /// Read reference ticket.
        /// </summary>
        /// <param name="data">Data Parameter value.</param>
        /// <param name="baseCurrency"> Parameter value.</param>
        /// <returns>Result value.</returns>
        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Reviewed.")]
        public static List<ITicketData> ReadReferneceTicket(DataTable data, string baseCurrency)
        {
            var result = new List<ITicketData>();

            var containsBetTeam = data.Columns.Contains("betteam");
            var containsStake = data.Columns.Contains("stake");
            foreach (DataRow dataRow in data.Rows)
            {
                var ticket = new TicketData
                {
                    BetTeam = containsBetTeam ? dataRow["betteam"].ToString() : string.Empty,
                    RefNo = dataRow["refno"].ToString(),
                    Status = dataRow["status"].ToString(),
                    BetTypeId = Convert.ToInt32(dataRow["bettype"]),
                    Stake = containsStake ? Convert.ToDecimal(dataRow["stake" + baseCurrency]) : 0,
                    Odds = Convert.ToDecimal(dataRow["odds"])
                };

                result.Add(ticket);
            }

            return result;
        }

        /// <summary>
        /// Read boolean object.
        /// </summary>
        /// <param name="value"> Parameter value.</param>
        /// <returns>Result value.</returns>
        public static bool ReadBoolean(object value)
        {
            if (value == null || value == Convert.DBNull)
            {
                return false;
            }

            if (value.GetType() == typeof(bool))
            {
                return Convert.ToBoolean(value);
            }

            if (value.GetType() == typeof(int))
            {
                return Convert.ToInt32(value) > 0;
            }

            return false;
        }

        /// <summary>
        /// Get Bet List.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Ticket results.</returns>
        public TicketResult GetBetList(BetListCriteria criteria)
        {
            var result = new TicketResult();

            using (var objectDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var param = criteria.GetSpParams();
                var ds = objectDb.ExecuteDataSet(param);
                var baseCurrency = string.Empty;

                var mainTable = ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
                result.MainTicketList = ReadMainTicketData(mainTable, baseCurrency);

                var referenceTable = ds.Tables.Count > 1 ? ds.Tables[1] : new DataTable();
                result.TicketDataList = ReadReferneceTicket(referenceTable, baseCurrency);

                result.TotalRows = Converter.ToInt(param["totalrows"]);
            }

            return result;
        }

        /// <summary>
        /// Get Bet List Parlay.
        /// </summary>
        /// <param name="transId">Input transaction ID.</param>
        /// <returns>Ticket results.</returns>
        public TicketResult GetBetListParlay(long transId)
        {
            var result = new TicketResult();

            using (var objectDb = new ObjectDb("Aff_User_BetListParlay"))
            {
                objectDb.AddInParameter("TransId", DbType.Int64, transId);
                var ds = objectDb.ExecuteDataSet();

                var mainTable = ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
                result.MainTicketList = ReadMainTicketData(mainTable, string.Empty);

                var referenceTable = ds.Tables.Count > 1 ? ds.Tables[1] : new DataTable();
                result.TicketDataList = ReadReferneceTicket(referenceTable, string.Empty);
            }

            return result;
        }

        /// <summary>
        /// Get System Parlay Detail data.
        /// </summary>
        /// <param name="criteria">The metal model.</param>
        /// <returns>The system parlay.</returns>
        public SystemParlayData GetSystemParlayDetail(SystemParlayDetailCriteria criteria)
        {
            using (var objectDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var param = criteria.GetSpParams();
                var dataSet = objectDb.ExecuteDataSet(param);
                var ticketData = dataSet.Tables.Count > 0 ? dataSet.Tables[0] : new DataTable();
                var serialData = dataSet.Tables.Count > 1 ? dataSet.Tables[1] : new DataTable();
                var systemParlayData = new SystemParlayData();

                systemParlayData.TicketList = new List<ISystemParlayTicket>();
                foreach (DataRow row in ticketData.Rows)
                {
                    var ticket = new SystemParlayTicket
                    {
                        Comm = Converter.ToDecimal(row["comm"]),
                        MOdds = Converter.ToDecimal(row["modds"]),
                        PlayerComm = Converter.ToDecimal(row["playercomm"]),
                        Serial = Convert.ToInt64(row["serial"]),
                        Stake = Converter.ToDecimal(row["stake"]),
                        Status = Converter.ToString2(row["status"]),
                        StatusId = Converter.ToInt(row["statusid"]),
                        TransId = Convert.ToInt64(row["transid"]),
                        WinLost = Converter.ToDecimal(row["winlost"]),
                        WinLostDate = Converter.ToDateTime(row["winlostdate"])
                    };

                    systemParlayData.TicketList.Add(ticket);
                }

                systemParlayData.SerialList = new List<ISystemParlaySerial>();
                foreach (DataRow row in serialData.Rows)
                {
                    var serial = new SystemParlaySerial
                    {
                        Serial = Convert.ToInt64(row["serial"]),
                        HomeId = Converter.ToInt(row["homeid"]),
                        AwayId = Converter.ToInt(row["awayid"])
                    };

                    systemParlayData.SerialList.Add(serial);
                }

                systemParlayData.Outstanding = Converter.ToDecimal(param["outstanding"]);
                return systemParlayData;
            }
        }

        /// <summary>
        /// Get exchange rate.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Exchange rate.</returns>
        public float GetExchangeRate(ExchangeRateCriteria criteria)
        {
            using (var objectDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var param = criteria.GetSpParams();
                objectDb.ExecuteNonQuery(param);

                return string.IsNullOrEmpty(param["rate"].ToString())
                    ? 1
                    : float.Parse(param["rate"].ToString());
            }
        }

        /// <summary>
        /// Get balance up and move bet dates.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        public void GetBalanceUpMoveBetDate(BalanceUpMoveBetCriteria criteria)
        {
            using (var objectDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var param = criteria.GetSpParams();
                var dataSet = objectDb.ExecuteDataSet(param);
                criteria.BalanceUpDay = DateTime.Parse(dataSet.Tables[0].Rows[0]["BalanceUpDay"].ToString());
                criteria.MoveBetsDay = DateTime.Parse(dataSet.Tables[0].Rows[0]["MoveBetsDay"].ToString());
            }
        }

        /// <summary>
        /// Get player's username.
        /// </summary>
        /// <param name="playerUsername">Affiliate username.</param>
        /// <returns>Return player's Info.</returns>
        public CustomerInfoBetList GetPlayerIbcInfo(string playerUsername)
        {
            var hashParam = new Hashtable
            { 
                { "username", playerUsername },
                { "custid", 0 },
                { "ibcusername", string.Empty },
                { "currencyid", 0 }
            };
            var objectDb = new ObjectDb("Aff_User_DummyAccID");
            objectDb.ExecuteNonQuery(hashParam);

            return new CustomerInfoBetList
            {
                CustId = int.Parse(hashParam["custid"].ToString()),
                Username = playerUsername,
                IBCUsername = hashParam["ibcusername"].ToString(),
                CurrencyId = int.Parse(hashParam["currencyid"].ToString())
            };
        }

        #endregion

        #region BetList.Results

        /// <summary>
        /// Get Popup Match.
        /// </summary>
        /// <param name="matchId">Match ID value.</param>
        /// <param name="raceNo">Race Number value.</param>
        /// <param name="licenseeId">Licensee ID value.</param>
        /// <param name="userId">User ID value.</param>
        /// <returns>The result value.</returns>
        public DataTable GetPopUpMatch(int matchId, int raceNo, int licenseeId, int userId)
        {
            var param = new
            {
                matchid = matchId,
                raceno = raceNo > -1 ? raceNo.ToString(CultureInfo.InvariantCulture) : null,
                licenseeid = licenseeId,
                userid = userId
            };

            using (var objectDb = new ObjectDb("Aff_User_BetListResult_SelMatch"))
            {
                var dataset = objectDb.ExecuteDataSet(param);
                return dataset.Tables[0];
            }
        }

        /// <summary>
        /// Get Popup Parlay Match.
        /// </summary>
        /// <param name="refNo">Reference Number value.</param>
        /// <returns>The result value.</returns>
        public DataTable GetPopUpParalayMatch(long refNo)
        {
            using (var objectDb = new ObjectDb("Aff_User_BetListResult_SelMatchParlay"))
            {
                var dataset = objectDb.ExecuteDataSet(new { refno = refNo });
                return dataset.Tables[0];
            }
        }

        /// <summary>
        /// Get Popup Number game match.
        /// </summary>
        /// <param name="matchId">Match ID value.</param>
        /// <returns>The result value.</returns>
        public DataTable GetPopUpNumberGameMatch(int matchId)
        {
            using (var objectDb = new ObjectDb("Aff_User_BetListResult_SelMatchNumberGame"))
            {
                var dataset = objectDb.ExecuteDataSet(new { matchid = matchId });
                return dataset.Tables[0];
            }
        }

        #endregion
    }
}