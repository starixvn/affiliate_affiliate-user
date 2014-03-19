// --------------------------------------------------------------------------
// <copyright file="ProductCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.06.14">Created</change>
// </history>
namespace Fanex.Affiliate.Criteria
{
    using System;
    using NAP.Framework.Data;

    /// <summary>
    /// Product Criteria.
    /// </summary>
    public class ProductCriteria : CriteriaBase
    {
        public ProductCriteria()
        {
            MonthDate = null;
            Language = "en-US";
        }

        public int AffId { get; set; }

        public DateTime? MonthDate { get; set; }

        public string Language { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_ProductSelByAffID";
        }
    }
}