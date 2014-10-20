// ----------------------------------------------------------------------
// <copyright file="IBetListDataAccess.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/12/03">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.DataAccess
{
    using System.Data;
    using BetList.Core.Entities;
    using DTO;
    using Fanex.Affiliate.Criteria;
    using Fanex.Affiliate.WebServices.Criteria;
    using Fanex.Affiliate.WebServices.DTO.BetList;

    /// <summary>
    /// Interface Account.
    /// </summary>
    public interface IBetListDataAccess
    {
        #region BetList

        /// <summary>
        /// Get Bet List.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Ticket results.</returns>
        TicketResult GetBetList(BetListCriteria criteria);

        /// <summary>
        /// Get Bet List Parlay.
        /// </summary>
        /// <param name="transId">Input transaction ID.</param>
        /// <returns>Ticket results.</returns>
        TicketResult GetBetListParlay(long transId);

        /// <summary>
        /// Get System Parlay Detail data.
        /// </summary>
        /// <param name="criteria">The metal model.</param>
        /// <returns>The system parlay.</returns>
        SystemParlayData GetSystemParlayDetail(BetListSystemParlayDetailCriteria criteria);

        /// <summary>
        /// Get exchange rate.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Exchange rate.</returns>
        float GetExchangeRate(ExchangeRateCriteria criteria);

        /// <summary>
        /// Get balance up and move bet dates.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        void GetBalanceUpMoveBetDate(BalanceUpMoveBetCriteria criteria);

        /// <summary>
        /// Get player's username.
        /// </summary>
        /// <param name="playerUsername">Affiliate username.</param>
        /// <returns>Return player's Info.</returns>
        CustomerInfoBetList GetPlayerIbcInfo(string playerUsername);

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
        DataTable GetPopUpMatch(int matchId, int raceNo, int licenseeId, int userId);

        /// <summary>
        /// Get Popup Parlay Match.
        /// </summary>
        /// <param name="refNo">Reference Number value.</param>
        /// <returns>The result value.</returns>
        DataTable GetPopUpParalayMatch(long refNo);

        /// <summary>
        /// Get Popup Number game match.
        /// </summary>
        /// <param name="matchId">Match ID value.</param>
        /// <returns>The result value.</returns>
        DataTable GetPopUpNumberGameMatch(int matchId);

        #endregion
    }
}