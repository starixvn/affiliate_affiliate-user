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
    using System.Data;
    using System.Globalization;
    using Criteria;
    using Data;
    using DTO;

    /// <summary>
    /// Account data access.
    /// </summary>
    public class BetListDataAccess : IBetListDataAccess
    {
        #region BetList

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