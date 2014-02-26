// ----------------------------------------------------------------------
// <copyright file="AffiliateInfoCriteria.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2014/02/24">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.WebServices.Criteria
{
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Affiliate Information Criteria.
    /// </summary>
    public class AffiliateInfoCriteria : CriteriaBase
    {
        public int AffID { get; set; }

        public int ErrorCode { get; set; }

        public string ErrorMsg { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <param name="siteName">Site Name.</param>
        /// <returns>Setting key.</returns>
        public string GetSettingKey(string siteName)
        {
            return "affiliate_" + siteName.ToLower() + "_GetAffiliateInfo";
        }
    }
}