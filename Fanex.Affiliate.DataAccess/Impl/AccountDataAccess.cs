// --------------------------------------------------------------------------
// <copyright file="AccountDataAccess.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.05.30">Created</change>
//   <change who="Kaden.Trinh" date="2013.06.10">Implement funcion get statistics overview account</change>
//   <change who="Kaden.Trinh" date="2013.06.11">Implement funcion get Current Commission Setting</change>
// </history>

namespace Fanex.Affiliate.DataAccess.Impl
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using BetList.Core.Entities;
    using Common;
    using Common.Extensions;
    using Criterias;
    using Data;
    using DTO;
    using DTO.BetList;
    using Interface;
    using ViewModels;

    /// <summary>
    /// Account data access.
    /// </summary>
    public class AccountDataAccess : IAccountDataAccess
    {
        #region Account Overview

        /// <summary>
        /// Get statistics for Affiliate.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <param name="listProduct">Out product list.</param>
        /// <returns>Return object.</returns>
        public StatisticsOverview GetStatisticsOverview(AccountOverviewCriteria criteria, ref List<Product> listProduct)
        {
            var statistics = new StatisticsOverview();
            using (var objectDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var param = criteria.GetSpParams();
                listProduct = objectDb.Query<Product>(param) as List<Product>;

                // get output parameter
                statistics.TotalActivePlayer = Convert.ToInt32(param["totalactiveplayer"]);
                statistics.TotalPlayerDepUp = Convert.ToInt32(param["totalplayerdepup"]);
                statistics.NewSignUpDep = Convert.ToInt32(param["newsignupdep"]);
                statistics.NewSignUp = Convert.ToInt32(param["newsignup"]);
                statistics.TotalPlayer = Convert.ToInt32(param["totalplayer"]);
                statistics.TotalUniqueClicks = Convert.ToInt32(param["totaluniqueclicks"]);
                statistics.TotalClicks = Convert.ToInt32(param["totalclicks"]);
                statistics.BalBroughtforward = Convert.ToDecimal(param["balbroughtforward"]);
                statistics.TotalDeposit = Convert.ToDecimal(param["totaldeposit"]);
                statistics.TotalDepositUp = Convert.ToDecimal(param["totaldepositup"]);
                statistics.TotalTurnover = Convert.ToDecimal(param["totalturnover"]);
                statistics.TotalTurnoverUp = Convert.ToDecimal(param["totalturnoverup"]);
                statistics.TotalWinlostUp = Convert.ToDecimal(param["totalwinlostup"]);
                statistics.TotalWinlost = Convert.ToDecimal(param["totalwinlost"]);
            }

            return statistics;
        }

        #endregion

        #region Commission Settings

        /// <summary>
        /// Get Current Commission Setting.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Return object.</returns>
        public AccountOverviewViewModel GetCommissionSetting(AccountOverviewCriteria criteria)
        {
            var commResult = new AccountOverviewViewModel();
            using (var objectDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var reader = objectDb.ExecuteReader(criteria.GetSpParams());
                
                // Get data to GroupSetting
                while (reader.Read())
                {
                    commResult.ListGroupSetting.Add(new GroupSetting
                    {
                        GroupId = reader.ToInt("groupid"),
                        ProductId = reader.ToByte("productid"),
                        TierId = reader.ToByte("tierid"),
                        MinAmount = reader.ToDecimal("minamount"),
                        MaxAmount = reader.ToDecimal("maxamount"),
                        MinQM = reader.ToInt("minqm"),
                        MaxQM = reader.ToInt("maxqm"),
                        Commission = reader.ToDecimal("commission")
                    });
                }

                // Get data to Product
                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        commResult.ListProductOnCommissionSetting.Add(new Product
                        {
                            ProductId = reader.ToByte("productid"),
                            ProductName = reader["productname"].ToString()
                        });
                    }
                }
            }

            return commResult;
        }

        #endregion

        #region Daily Earnings

        /// <summary>
        /// Get Daily Earnings.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <param name="affCurrency">Affiliate Currency.</param>
        /// <returns>List of daily earning.</returns>
        public IEnumerable<DailyEarning> GetDailyEarnings(DailyEarningCriteria criteria, out string affCurrency)
        {
            List<DailyEarning> list;
            using (var objectDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var param = criteria.GetSpParams();
                list = objectDb.Query<DailyEarning>(param) as List<DailyEarning>;
                affCurrency = Convert.ToString(param["affcurrency"]);
            }

            return list;
        }

        /// <summary>
        /// Get Daily Earnings Detail.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <param name="affCurrency">Out affiliate currency.</param>
        /// <returns>List of daily earning detail.</returns>
        public IEnumerable<DailyEarningsDetail> GetDailyEarningsDetail(DailyEarningsDetailCriteria criteria, out string affCurrency)
        {
            List<DailyEarningsDetail> list;
            using (var objectDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var param = criteria.GetSpParams();
                list = objectDb.Query<DailyEarningsDetail>(param) as List<DailyEarningsDetail>;
                affCurrency = Convert.ToString(param["affcurrency"]);
            }

            return list;
        }

        #endregion
/*
        #region BetList

        /// <summary>
        /// Get Bet List.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Ticket results.</returns>
        public TicketResult GetBetList(BetListCriteria criteria)
        {
            var result = new TicketResult();
            var param = CreateBetListParameters(criteria);

            using (var objectDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var ds = objectDb.ExecuteDataSet(param);
                var baseCurrency = criteria.BaseCurrency.ToLower();

                // Main ticket
                var mainTable = ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
                result.MainTicketList = this.ReadMainTicketData(mainTable, string.Empty);

                // Reference ticket
                var referenceTable = ds.Tables.Count > 1 ? ds.Tables[1] : new DataTable();
                result.TicketDataList = this.ReadReferneceTicket(referenceTable, string.Empty);

                // Sum value
                result.TotalRows = Converter.ToInt(param["totalrows"]);
                result.SumWinLoss = Converter.ToDecimal(param["sumwinlost"]);
                result.SumLicenseeWl = Converter.ToDecimal(param["sumlicenseewl" + baseCurrency]);
                result.SumComm = Converter.ToDecimal(param["sumcomm" + baseCurrency]);
                result.SumPlayerComm = Converter.ToDecimal(param["sumplayercomm" + baseCurrency]);
                result.SumStake = Converter.ToDecimal(param["sumstake" + baseCurrency]);
            }

            return result;
        }

        /// <summary>
        /// Create bet list parameters.
        /// </summary>
        /// <param name="criteria">Parameter value.</param>
        /// <returns>Result value.</returns>
        public Hashtable CreateBetListParameters(BetListCriteria criteria)
        {
            var param = new Hashtable
                {
                    // Input
                    { "licenseeid", criteria.LicenseeId },
                    { "userid", criteria.CustId },
                    { "custid", criteria.CustId },
                    { "fromdate", criteria.FromDate },
                    { "todate", criteria.ToDate },
                    { "movebetsday", criteria.MoveBetsDay },
                    { "balanceupday", criteria.BalanceUpDay },
                    { "drawfilter", criteria.DrawFilter },
                    { "showsb", criteria.ShowSB },
                    { "showrb", criteria.ShowRB },
                    { "showng", criteria.ShowNG },
                    { "showba", criteria.ShowBA },
                    { "showlc", criteria.ShowLC },
                    { "showvs", criteria.ShowVS },
                    { "showcs", criteria.ShowCS },
                    { "showbg", criteria.ShowBG },

                    // Output
                    { "totalrows", 0 },
                    { "sumwinlost", 0 },
                    { "sumwinlostusd", 0 },
                    { "sumwinlosteur", 0 },
                    { "sumlicenseewl", 0 },
                    { "sumlicenseewlusd", 0 },
                    { "sumlicenseewleur", 0 },
                    { "sumcomm", 0 },
                    { "sumcommusd", 0 },
                    { "sumcommeur", 0 },
                    { "sumplayercomm", 0 },
                    { "sumplayercommusd", 0 },
                    { "sumplayercommeur", 0 },
                    { "sumstake", 0 },
                    { "sumstakeusd", 0 },
                    { "sumstakeeur", 0 }
                };

            return param;
        }

        /// <summary>
        /// Get Bet List Parlay.
        /// </summary>
        /// <param name="transId">Input transaction ID.</param>
        /// <returns>Ticket results.</returns>
        public TicketResult GetBetListParlay(long transId)
        {
            var result = new TicketResult();

            using (var objectDb = new ObjectDb("Aff_BetListParlay"))
            {
                objectDb.AddInParameter("TransId", DbType.Int64, transId);
                var ds = objectDb.ExecuteDataSet();

                var mainTable = ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
                result.MainTicketList = this.ReadMainTicketData(mainTable, string.Empty);

                // Reference ticket
                var referenceTable = ds.Tables.Count > 1 ? ds.Tables[1] : new DataTable();
                result.TicketDataList = this.ReadReferneceTicket(referenceTable, string.Empty);
            }

            return result;
        }

        /// <summary>
        /// Get System Parlay Detail data.
        /// </summary>
        /// <param name="criteria">The metal model.</param>
        /// <returns>The system parlay.</returns>
        [SuppressMessage("StyleCop.CSharp.Nexcel.NexcelCustomRules", "SP2101:MethodMustNotContainMoreLinesThan",
            Justification = "Reviewed. Suppression is OK here.")]
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
                    ISystemParlayTicket ticket = new SystemParlayTicket();
                    ticket.Comm = Converter.ToDecimal(row["comm"]);
                    ticket.MOdds = Converter.ToDecimal(row["modds"]);
                    ticket.PlayerComm = Converter.ToDecimal(row["playercomm"]);
                    ticket.Serial = Convert.ToInt64(row["serial"]);
                    ticket.Stake = Converter.ToDecimal(row["stake"]);
                    ticket.Status = Converter.ToString2(row["status"]);
                    ticket.StatusId = Converter.ToInt(row["statusid"]);
                    ticket.TransId = Convert.ToInt64(row["transid"]);
                    ticket.WinLost = Converter.ToDecimal(row["winlost"]);
                    ticket.WinLostDate = Converter.ToDateTime(row["winlostdate"]);

                    systemParlayData.TicketList.Add(ticket);
                }

                systemParlayData.SerialList = new List<ISystemParlaySerial>();
                foreach (DataRow row in serialData.Rows)
                {
                    ISystemParlaySerial serial = new SystemParlaySerial();
                    serial.Serial = Convert.ToInt64(row["serial"]);
                    serial.HomeId = Converter.ToInt(row["homeid"]);
                    serial.AwayId = Converter.ToInt(row["awayid"]);

                    systemParlayData.SerialList.Add(serial);
                }

                systemParlayData.Outstanding = Converter.ToDecimal(param["outstanding"]);

                return systemParlayData;
            }
        }

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
        public IList<AffTicket> ReadMainTicketData(DataTable dataTable, string baseCurrency)
        {
            var playerCommCol = "playercomm" + baseCurrency;
            var commCol = "comm" + baseCurrency;
            var licenseeWlCol = "licenseewl" + baseCurrency;
            var winlostCol = "winlost" + baseCurrency;
            var stakeCol = "stake" + baseCurrency;
            var actualStakeCol = "actual_stake" + baseCurrency;

            var columnCollection = dataTable.Columns;

            // string
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

                // string
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
        public List<ITicketData> ReadReferneceTicket(DataTable data, string baseCurrency)
        {
            var result = new List<ITicketData>();

            var containsBetTeam = data.Columns.Contains("betteam");
            var containsStake = data.Columns.Contains("stake");
            foreach (DataRow dataRow in data.Rows)
            {
                var ticket = new TicketData();

                // string
                ticket.BetTeam = containsBetTeam ? dataRow["betteam"].ToString() : string.Empty;
                ticket.RefNo = dataRow["refno"].ToString();
                ticket.Status = dataRow["status"].ToString();

                // Int
                ticket.BetTypeId = Convert.ToInt32(dataRow["bettype"]);

                // Money
                ticket.Stake = containsStake ? Convert.ToDecimal(dataRow["stake" + baseCurrency]) : 0;

                // Decimal
                ticket.Odds = Convert.ToDecimal(dataRow["odds"]);

                result.Add(ticket);
            }

            return result;
        }

        /// <summary>
        /// Read boolean object.
        /// </summary>
        /// <param name="value"> Parameter value.</param>
        /// <returns>Result value.</returns>
        public bool ReadBoolean(object value)
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
                var ds = objectDb.ExecuteDataSet(param);
                criteria.BalanceUpDay = DateTime.Parse(ds.Tables[0].Rows[0]["BalanceUpDay"].ToString());
                criteria.MoveBetsDay = DateTime.Parse(ds.Tables[0].Rows[0]["MoveBetsDay"].ToString());
            }
        }

        /// <summary>
        /// Get player's username.
        /// </summary>
        /// <param name="playerUsername">Affiliate username.</param>
        /// <returns>Return player's ID.</returns>
        public int GetPlayerIbcid(string playerUsername)
        {
            var hashParam = new Hashtable { { "username", playerUsername }, { "custid", 0 } };
            var objectDb = new ObjectDb("Aff_DummyAccID");
            objectDb.ExecuteNonQuery(hashParam);

            return int.Parse(hashParam["custid"].ToString());
        }

        #endregion
*/
        #region Change password

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        public void ChangePassword(ChangePasswordCriteria criteria)
        {
            using (var objectDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var param = criteria.GetSpParams();
                objectDb.ExecuteNonQuery(param);
                criteria.ErrCode = Converter.ToInt(param["errcode"]);
            }
        }

        #endregion

        #region Get Default Product Comm

        /// <summary>
        /// Get Default Product Commission.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Default Product Commission Model.</returns>
        public DefaultProductCommissionViewModel GetDefaultProductCommission(DefaultCommissionCriteria criteria)
        {
            var result = new DefaultProductCommissionViewModel();

            using (var objectDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var reader = objectDb.ExecuteReader(criteria.GetSpParams());

                // Get data to GroupSetting
                while (reader.Read())
                {
                    result.GroupSetting.Add(new GroupSetting
                    {
                        GroupId = reader.ToInt("groupid"),
                        ProductId = reader.ToByte("productid"),
                        TierId = reader.ToByte("tierid"),
                        MinAmount = reader.ToDecimal("minamount"),
                        MaxAmount = reader.ToDecimal("maxamount"),
                        MinQM = reader.ToInt("minqm"),
                        MaxQM = reader.ToInt("maxqm"),
                        Commission = reader.ToDecimal("commission")
                    });
                }

                // Get data to Product
                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        result.ProductList.Add(new Product
                        {
                            ProductId = reader.ToByte("productid"),
                            ProductName = reader["productname"].ToString()
                        });
                    }
                }
            }

            return result;
        }

        #endregion

        #region Get Customer Info

        /// <summary>
        /// Get Customer Info.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <param name="totalPage">Total page for paging.</param>
        /// <returns>Customer Info Model.</returns>
        public IEnumerable<CustomerInfo> GetCustomerInfo(CustomerInfoCriteria criteria, out int totalPage)
        {
            List<CustomerInfo> list;
            using (var objectDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var param = criteria.GetSpParams();
                list = objectDb.Query<CustomerInfo>(param) as List<CustomerInfo>;
                totalPage = Converter.ToInt(param["totalpages"]);
            }

            return list;
        }

        #endregion
    }
}