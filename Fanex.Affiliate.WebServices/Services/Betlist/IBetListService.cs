// ----------------------------------------------------------------------
// <copyright file="IBetListService.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/12/03">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.WebServices.Services
{
    using System.Collections.Generic;
    using Criteria;
    using DTO;
    using DTO.BetList;
    using Fanex.BetList.Core.Entities;
    using Fanex.Result.Core.DTO;
    using ViewModels;

    /// <summary>
    /// Interface Account service.
    /// </summary>
    public interface IBetListService
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
        /// Get match for pop up pages.
        /// Only used for sport-book and racing.
        /// </summary>
        /// <param name="affiliateGroup">Affiliate Group.</param>
        /// <param name="matchId">The match id.</param>
        /// <returns>The match info.</returns>
        IList<Sportsbook> GetPopUpMatch(string affiliateGroup, int matchId);

        /// <summary>
        /// Get live casino match for pop up page.
        /// </summary>
        /// <param name="affiliateGroup">Affiliate Group.</param>
        /// <param name="matchId">The match id.</param>
        /// <returns>The match info.</returns>
        IList<LiveCasino> GetPopUpLiveCasinoMatch(string affiliateGroup, int matchId);

        /// <summary>
        /// Get the racing match for pop up page.
        /// </summary>
        /// <param name="affiliateGroup">Affiliate Group.</param>
        /// <param name="matchId">The match id.</param>
        /// <param name="raceNo">The race no.</param>
        /// <returns>The match info.</returns>
        IList<Racing> GetPopUpRacingMatch(string affiliateGroup, int matchId, int raceNo);

        /// <summary>
        /// Select matches from BetTransM by reference no.
        /// For view MixParlay result.
        /// </summary>
        /// <param name="affiliateGroup">Affiliate Group.</param>
        /// <param name="refNo">The reference no.</param>
        /// <returns>The parlay list.</returns>
        IList<Parlay> GetPopUpParlayMatch(string affiliateGroup, long refNo);

        /// <summary>
        ///  Get match result of number game.
        /// </summary>
        /// <param name="affiliateGroup">Affiliate Group.</param>
        /// <param name="matchId">The match id.</param>
        /// <returns>The match info.</returns>
        IList<NumberGame> GetPopUpNumberGameMatchs(string affiliateGroup, int matchId);

        #endregion

        #region BetList.Results.RNGCasino

        List<RNGCasinoResult> GetRNGCasinoResult(BetListResultsRNGCasinoCriteria criteria);

        BetListResultsRNGCasinoDetailViewModel GetRNGCasinoResultDetail(BetListResultsRNGCasinoDetailCriteria criteria);

        #endregion
    }
}