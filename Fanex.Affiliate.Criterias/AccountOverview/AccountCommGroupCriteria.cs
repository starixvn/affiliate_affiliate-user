// ----------------------------------------------------------------------
// <copyright file="AccountCommGroupCriteria.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2014/03/19">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.Criteria
{
    using System;
    using NAP.Framework.Data;

    /// <summary>
    /// Overview Criteria.
    /// </summary>
    public class AccountCommGroupCriteria : CriteriaBase
    {
        public int AffId { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_User_CommGroupSelDetail";
        }

        public override bool IsValid()
        {
            return AffId > 0;
        }
    }
}