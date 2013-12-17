// --------------------------------------------------------------------------
// <copyright file="SystemSettingsCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013.07.23">Created and add Properties</change>
// </history>

namespace Fanex.Affiliate.Criterias
{
    using NAP.Framework.Data;

    /// <summary>
    /// System Settings Criteria.
    /// </summary>
    public class SystemSettingsCriteria : CriteriaBase
    {
        public string Environment { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_GetAllAffSystemSettings";
        }
    }
}