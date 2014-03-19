// --------------------------------------------------------------------------
// <copyright file="DefaultCommissionCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Brian.Dinh" date="2013.07.09">Created</change>
// </history>

namespace Fanex.Affiliate.Criteria
{
    using NAP.Framework.Data;

    /// <summary>
    /// Default Group Commission Criteria.
    /// </summary>
    public class DefaultCommissionCriteria : CriteriaBase
    {
        private const int DEFAULTCOMMISSIONGROUPID = 101;

        public DefaultCommissionCriteria()
        {
            // Default commission group id
            GroupId = DEFAULTCOMMISSIONGROUPID;
        }

        public int GroupId { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_GetDefaultProductCommission";
        }
    }
}