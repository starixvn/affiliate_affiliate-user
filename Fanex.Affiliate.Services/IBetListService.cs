// ----------------------------------------------------------------------
// <copyright file="IBetListService.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/12/03">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.Services
{
    using System.Collections.Generic;
    using Criteria;
    using DTO;
    using DTO.BetList;
    using ViewModels;

    /// <summary>
    /// Interface Account service.
    /// </summary>
    public interface IBetListService
    {
        #region BetList

        /// <summary>
        /// Get exchange rate.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Exchange rate.</returns>
        float GetExchangeRate(ExchangeRateCriteria criteria);

        /// <summary>
        /// Get player's username.
        /// </summary>
        /// <param name="playerUsername">Affiliate username.</param>
        /// <returns>Return player's Info.</returns>
        CustomerInfoBetList GetPlayerIbcInfo(string playerUsername);

        #endregion

        #region BetList.Results

        #endregion

        #region BetList.Results.RNGCasino

        List<RNGCasinoResult> GetRNGCasinoResult(BetListResultsRNGCasinoCriteria criteria);

        BetListResultsRNGCasinoDetailViewModel GetRNGCasinoResultDetail(BetListResultsRNGCasinoDetailCriteria criteria);

        #endregion
    }
}