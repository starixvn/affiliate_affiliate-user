// --------------------------------------------------------------------------
// <copyright file="ClicksLandingPagesCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Leo.Vo" date="2013.06.29">Created</change>
// </history>

namespace Fanex.Affiliate.Criterias
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using NAP.Framework.Data;
    using Resources;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ClicksLandingPagesCriteria : CriteriaBase
    {
        public int AffId { get; set; }

        [SpParam(SqlParamName = "SportCode")]
        public string PageSelected { get; set; }

        [SpParam(SqlParamName = "LanguageCode")]
        public string LanguageSelected { get; set; }

        public string Domain { get; set; }

        [SpParam(Ignored = true)]
        public bool IsDetail { get; set; }

        [Required(ErrorMessageResourceName = "FromDateIsRequired", ErrorMessageResourceType = typeof(Lang))]
        public DateTime FromDate { get; set; }

        [Required(ErrorMessageResourceName = "ToDateIsRequired", ErrorMessageResourceType = typeof(Lang))]
        public DateTime ToDate { get; set; }

        [SpParam(SqlParamName = "PageIndex")]
        public int Page { get; set; }

        public int TotalPages { get; set; }

        public int PageSize { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return IsDetail ? "Aff_ClickStatisticsDetailBySite" : "Aff_ClickStatisticsSelByPage";
        }

        public override bool IsValid()
        {
            return FromDate <= ToDate;
        }
    }
}