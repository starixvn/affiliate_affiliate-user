// --------------------------------------------------------------------------
// <copyright file="SystemSettingCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.06.10">Created and add Properties</change>
// </history>

namespace Fanex.Affiliate.Criteria
{
    using NAP.Framework.Data;

    /// <summary>
    /// System Setting Criteria.
    /// </summary>
    public class SystemSettingCriteria : CriteriaBase
    {
        public string Name { get; set; }

        public string Type { get; set; }

        [SpParam(Ignored = true)]
        public bool IsType { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return IsType ? "Aff_SystemSettingsSelByType" : "Aff_SystemSettingsSelByName";
        }

        public override bool IsValid()
        {
            return !string.IsNullOrEmpty(Name) || (!string.IsNullOrEmpty(Type) && IsType);
        }
    }
}