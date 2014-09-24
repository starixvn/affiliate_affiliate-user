// ----------------------------------------------------------------------
// <copyright file="AffiliateLoginResultCriteria.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2014/09/17">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.Criteria
{
    using System.Web.Mvc;
    using NAP.Framework.Data;

    /// <summary>
    /// Authenticate User Criteria.
    /// </summary>
    public class AffiliateLoginResultCriteria : CriteriaBase
    {
        public int AffId { get; set; }

        public bool IsSuccess { get; set; }

        public int ServerID { get; set; }

        public string LoginIP { get; set; }

        public string NetSessionID { get; set; }

        // Output parameters
        public int OnUserID { get; set; }

        public int ErrCode { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_User_SecureLoginUpdate";
        }

        public override bool IsValid()
        {
            return AffId != 0;
        }
    }
}