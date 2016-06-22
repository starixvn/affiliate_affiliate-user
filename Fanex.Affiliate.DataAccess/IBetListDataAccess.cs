namespace Fanex.Affiliate.DataAccess
{
    using System.Data;
    using Criteria;
    using DTO;
    using Fanex.Affiliate.Criteria.BetList;

    /// <summary>
    /// Interface Account.
    /// </summary>
    public interface IBetListDataAccess
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

        void ValidatePermission(ValidationCriteria criteria);
    }
}