// ----------------------------------------------------------------------
// <copyright file="PaymentRequestCriteria.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/07/03">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.Criterias
{
    using System;
    using NAP.Framework.Data;

    /// <summary>
    /// Criteria for inserting the new payment request.
    /// </summary>
    public class PaymentRequestCriteria : CriteriaBase
    {
        public int AffId { get; set; }

        public string Player_UserName { get; set; }

        public string Player_IBCUserName { get; set; }

        public string Player_Currency { get; set; }

        public decimal Amount { get; set; }

        public string IP { get; set; }

        public string Language { get; set; }

        [SpParam(Ignored = true)]
        public string PreferredLanguage { get; set; }

        [SpParam(Ignored = true)]
        public bool MailOption { get; set; }

        [SpParam(Ignored = true)]
        public DateTime TransDate { get; set; }

        [SpParam(Ignored = true)]
        public long TransID { get; set; }

        [SpParam(Ignored = true)]
        public int ErrCode { get; set; }

        /// <summary>
        /// Get the reference SP's Name which is specified in Config file.
        /// </summary>
        /// <returns>Store procedure payment request insert.</returns>
        public override string GetSettingKey()
        {
            return "Aff_PaymentRequestIns";
        }
    }
}