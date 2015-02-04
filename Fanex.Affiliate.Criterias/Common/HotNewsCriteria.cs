// --------------------------------------------------------------------------
// <copyright file="HotNewsCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.06.10">Created and add Properties</change>
// </history>

namespace Fanex.Affiliate.Criteria
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Overview Criteria.
    /// </summary>
    public class HotNewsCriteria : CriteriaBase
    {
        public int? MsgId { get; set; }

        [SpParam(Ignored = true)]
        public string Language { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_User_HotNewsDetail_New";
        }
    }
}