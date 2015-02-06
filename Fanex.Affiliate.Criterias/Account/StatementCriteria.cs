// --------------------------------------------------------------------------
// <copyright file="StatementCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.06.03">Created and add Properties</change>
// </history>

namespace Fanex.Affiliate.Criteria
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Statement Criteria.
    /// </summary>
    public class StatementCriteria : CriteriaBase
    {
        public int AffId { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        // Output parameters
        public int TotalPages { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_User_AffiliateStatementHistorySel";
        }
    }
}