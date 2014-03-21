// ----------------------------------------------------------------------
// <copyright file="PaymentPlayerInfoCriteria.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/07/03">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.Criteria
{
    using NAP.Framework.Data;

    /// <summary>
    /// Criteria for getting the player's info for payout.
    /// </summary>
    public class PaymentPlayerInfoCriteria : CriteriaBase
    {
        public int AffId { get; set; }

        // Output parameters
        [SpParam(Ignored = true)]
        public string PlayerUsername { get; set; }
        
        [SpParam(Ignored = true)]
        public string SystemName { get; set; }

        [SpParam(Ignored = true)]
        public string DummyBalAcc { get; set; }

        [SpParam(Ignored = true)]
        public string AffCurrency { get; set; }

        [SpParam(Ignored = true)]
        public int ErrCode { get; set; }

        /// <summary>
        /// Get the reference SP's Name which is specified in Config file.
        /// </summary>
        /// <returns>Store procedure.</returns>
        public override string GetSettingKey()
        {
            return "Aff_User_PlayerPayoutInfoSel";
        }
    }
}