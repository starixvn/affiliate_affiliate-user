// ----------------------------------------------------------------------
// <copyright file="IBetListDataAccess.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/12/03">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.WebServices.DataAccess
{
    using System.Data;
    using BetList.Core.Entities;
    using DTO;
    using DTO.BetList;
    using Fanex.Affiliate.WebServices.Criteria;

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
        /// <param name="criteria">Bet list parlay.</param>
        /// <returns>Ticket results.</returns>
        TicketResult GetBetListParlay(BetListParlayCriteria criteria);

        /// <summary>
        /// Get System Parlay Detail data.
        /// </summary>
        /// <param name="criteria">The metal model.</param>
        /// <returns>The system parlay.</returns>
        SystemParlayData GetSystemParlayDetail(BetListSystemParlayDetailCriteria criteria);
        
        /// <summary>
        /// Get balance up and move bet dates.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        void GetBalanceUpMoveBetDate(BalanceUpMoveBetCriteria criteria);

        /// <summary>
        /// Get player's username.
        /// </summary>
        /// <param name="affiliateGroup">Affiliate group.</param>
        /// <param name="playerUsername">Affiliate username.</param>
        /// <returns>Return player's Info.</returns>
        CustomerInfoBetList GetPlayerIbcInfo(string affiliateGroup, string playerUsername);

        #endregion

        #region BetList.Results

        /// <summary>
        /// Get Popup Match.
        /// </summary>
        /// <param name="affiliateGroup">Affiliate Group.</param>
        /// <param name="matchId">Match ID value.</param>
        /// <param name="raceNo">Race Number value.</param>
        /// <returns>The result value.</returns>
        DataTable GetPopUpMatch(string affiliateGroup, int matchId, int raceNo);

        /// <summary>
        /// Get Popup Parlay Match.
        /// </summary>
        /// <param name="affiliateGroup">Affiliate Group.</param>
        /// <param name="refNo">Reference Number value.</param>
        /// <returns>The result value.</returns>
        DataTable GetPopUpParalayMatch(string affiliateGroup, long refNo);

        /// <summary>
        /// Get Popup Number game match.
        /// </summary>
        /// <param name="affiliateGroup">Affiliate Group.</param>
        /// <param name="matchId">Match ID value.</param>
        /// <returns>The result value.</returns>
        DataTable GetPopUpNumberGameMatch(string affiliateGroup, int matchId);

        #endregion
    }
}