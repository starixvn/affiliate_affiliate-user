// ----------------------------------------------------------------------
// <copyright file="BannerCriteria.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/09/23">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.Criterias
{
    using NAP.Framework.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class BannerCriteria : CriteriaBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BannerCriteria"/> class.
        /// </summary>
        public BannerCriteria()
        {
            IsUpdateBanner = true;
        }

        public int BannerID { get; set; }

        [SpParam(Ignored = true)]
        public bool IsUpdateBanner { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return IsUpdateBanner ? "Aff_BannerUpdateStatistic" : "Aff_BannerSelInfo";
        }
    }
}
