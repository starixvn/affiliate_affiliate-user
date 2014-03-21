// --------------------------------------------------------------------------
// <copyright file="ReCheckInCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.06.07">Created</change>
// </history>

namespace Fanex.Affiliate.Criteria
{
    using NAP.Framework.Data;

    /// <summary>
    /// Re-Check In Criteria.
    /// </summary>
    public class ReCheckInCriteria : CriteriaBase
    {
        public int OnUserID { get; set; }

        public bool IsKickedOut { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_User_Recheckin";
        }
    }
}