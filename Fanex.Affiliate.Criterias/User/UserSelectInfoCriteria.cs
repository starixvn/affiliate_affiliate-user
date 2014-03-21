// ----------------------------------------------------------------------
// <copyright file="UserSelectInfoCriteria.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2014/03/20">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.Criteria
{
    using System.Web.Mvc;
    using NAP.Framework.Data;

    /// <summary>
    /// User Select Info Criteria.
    /// </summary>
    public class UserSelectInfoCriteria : CriteriaBase
    {
        public int AffID { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_User_AffiliateSelEditInfo";
        }
    }
}