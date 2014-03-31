// ----------------------------------------------------------------------
// <copyright file="PaymentCriteria.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2014/03/20">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.Criteria
{
    using NAP.Framework.Data;

    /// <summary>
    /// Criteria for getting the player's info.
    /// </summary>
    public class PaymentCriteria : CriteriaBase
    {
        public int AffId { get; set; }

        // Output parameters
        public string PreferredPayment { get; set; }

        [SpParam(SqlParamName = "BalBroughtForward")]
        public decimal BalanceBroughtForward { get; set; }

        [SpParam(SqlParamName = "MinAmount")]
        public decimal MinimumWithdrawalAmount { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_User_PaymentRequestSelInfoByAff";
        }
    }
}