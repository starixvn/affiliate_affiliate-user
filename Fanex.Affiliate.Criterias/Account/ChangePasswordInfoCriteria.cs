// ----------------------------------------------------------------------
// <copyright file="ChangePasswordInfoCriteria.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2014/09/23">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.Criteria
{
    using System.Web.Mvc;
    using Fanex.Data.Repository;

    /// <summary>
    /// Change Password Criteria.
    /// </summary>
    public class ChangePasswordInfoCriteria : CriteriaBase
    {
        public int AffId { get; set; }

        public int ErrCode { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_User_SecureUpdPasswordInfo";
        }
    }
}