// --------------------------------------------------------------------------
// <copyright file="InternalLoginCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Leo.Vo" date="2013.07.03">Created</change>
// </history>

namespace Fanex.Affiliate.Criteria
{
    using NAP.Framework.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class InternalLoginCriteria : CriteriaBase
    {
        public int AccId { get; set; }

        public int ServerId { get; set; }

        public int AdminId { get; set; }

        public string UserName { get; set; }

        public string LoginIp { get; set; }

        public string Language { get; set; }

        public int CurrencyId { get; set; }

        public int InputAffType { get; set; }

        public int ErrorCode { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_AffiliateInternalLogin";
        }
    }
}