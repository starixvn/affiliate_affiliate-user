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
        public DefaultCommissionCriteria()
        {
            Environment = string.Empty;
        }

        public string Environment { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_User_GetDefaultProductCommission";
        }
    }
}