// --------------------------------------------------------------------------
// <copyright file="SystemDateTimeCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Muller.Nguyen" date="2013.07.10">Created</change>
// </history>

namespace Fanex.Affiliate.Criteria
{
    using Fanex.Data.Repository;

    /// <summary>
    /// System Date Time Criteria.
    /// </summary>
    public class SystemDateTimeCriteria : CriteriaBase
    {
        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_User_AffiliateSelDBTime";
        }
    }
}