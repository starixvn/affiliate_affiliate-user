// --------------------------------------------------------------------------
// <copyright file="MarketingCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.06.03">Created and add Properties</change>
//   <change who="Muller.Nguyen" date="2013.06.28">Add method GetSettingKey</change>
// </history>

namespace Fanex.Affiliate.Criteria
{
    using NAP.Framework.Data;

    /// <summary>
    /// Marketing Criteria.
    /// </summary>
    public class MarketingCriteria : CriteriaBase
    {
        public MarketingCriteria()
        {
            Page = 1;
            TotalPages = 0;
            Type = 0;
        }

        public short? Category
        {
            get
            {
                return BannerSubCategory.HasValue ? BannerSubCategory : BannerCategory;
            }
        }

        public string Language { get; set; }

        public short? ImageType { get; set; }

        public short? Size { get; set; }

        public short? Currency { get; set; }

        public short Type { get; set; }

        public int PageSize { get; set; }

        [SpParam(SqlParamName = "PageIndex")]
        public int Page { get; set; }

        [SpParam(Ignored = true)]
        public short? BannerCategory { get; set; }

        [SpParam(Ignored = true)]
        public short? BannerSubCategory { get; set; }

        public string Domain { get; set; }

        public string LinkTo { get; set; }

        [SpParam(Ignored = true)]
        public string Function { get; set; }

        // Output parameters
        public int TotalPages { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_User_BannerSel";
        }
    }
}