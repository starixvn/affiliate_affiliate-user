// ----------------------------------------------------------------------
// <copyright file="InternalLoginCriteria.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2014/10/01">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.Criteria
{
    using NAP.Framework.Data;

    /// <summary>
    /// Internal Login Criteria.
    /// </summary>
    public class InternalLoginCriteria : CriteriaBase
    {
        public int AdminId { get; set; }

        public string UserName { get; set; }

        public string LoginIp { get; set; }

        public int ServerId { get; set; }

        public string Language { get; set; }

        public int InputAffType { get; set; }

        [SpParam(Ignored = true)]
        public int AccId { get; set; }

        // Output parameters
        public int ErrCode { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_User_AffiliateInternalLogin";
        }
    }
}