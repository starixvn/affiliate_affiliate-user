// --------------------------------------------------------------------------
// <copyright file="LogOutUserCriteria.cs" company="Nexcel Solutions Vietnam">
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
    /// Log Out User Criteria.
    /// </summary>
    public class LogOutUserCriteria : CriteriaBase
    {
        public int ServerId { get; set; }

        public string Username { get; set; }

        public int AffId { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_User_Logout";
        }
    }
}