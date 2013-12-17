// --------------------------------------------------------------------------
// <copyright file="PaymentCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.06.03">Created and add Properties</change>
// </history>
namespace Fanex.Affiliate.Criterias
{
    using NAP.Framework.Data;

    /// <summary>
    /// Criteria for getting the player's info.
    /// </summary>
    public class PaymentCriteria : CriteriaBase
    {
        public int AffId { get; set; }

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
            return "Aff_PaymentRequestSelInfoByAff";
        }
    }
}