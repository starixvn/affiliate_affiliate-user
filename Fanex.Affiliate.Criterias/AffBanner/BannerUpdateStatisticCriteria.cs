// ----------------------------------------------------------------------
// <copyright file="BannerUpdateStatisticCriteria.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/09/23">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.Criteria
{
    using NAP.Framework.Data;

    /// <summary>
    /// Banner Update Statistic Criteria.
    /// </summary>
    public class BannerUpdateStatisticCriteria : CriteriaBase
    {
        public int BannerID { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_Banner_UpdateStatistic";
        }
    }
}
