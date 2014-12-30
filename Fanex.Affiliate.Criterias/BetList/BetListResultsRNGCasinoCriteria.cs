// ----------------------------------------------------------------------
// <copyright file="BetListResultsRNGCasinoCriteria.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/12/04">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.Criteria
{
    using System;
    using NAP.Framework.Data;

    /// <summary>
    /// Bet List Criteria.
    /// </summary>
    public class BetListResultsRNGCasinoCriteria : CriteriaBase
    {
        public string BetType
        {
            get
            {
                return "Casino";
            }
        }

        public long RefNo { get; set; }

        public bool IsShowPic { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string ExternalId { get; set; }

        public string SystemId { get; set; }

        public string UserName
        {
            get
            {
                return SystemId;
            }
        }

        public int CustId { get; set; }

        public int CurrencyId { get; set; }

        public string CurrencyName { get; set; }

        public string BaseCurrency { get; set; }

        public Uri BaseUrl { get; set; }

        public override bool IsValid()
        {
            return RefNo > 0
                && FromDate <= ToDate && CustId > 0
                && !string.IsNullOrEmpty(UserName)
                && !string.IsNullOrEmpty(BetType);
        }
    }
}