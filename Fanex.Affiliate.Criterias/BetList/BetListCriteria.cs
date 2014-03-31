// ----------------------------------------------------------------------
// <copyright file="BetListCriteria.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/11/19">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.Criteria
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using NAP.Framework.Data;

    /// <summary>
    /// Bet List Criteria.
    /// </summary>
    public class BetListCriteria : CriteriaBase
    {
        public BetListCriteria()
        {
            BaseCurrency = "EUR";
            DrawFilter = 1;
            ShowSB = false;
            ShowRB = false;
            ShowNG = false;
            ShowBA = false;
            ShowLC = false;
            ShowVS = false;
            ShowCS = false;
            ShowBG = false;
        }

        public int LicenseeId { get; set; }

        [SpParam(Ignored = true)]
        public string BaseCurrency { get; set; }

        [Range(1, 3)]
        [DefaultValue(1)]
        public int DrawFilter { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }
        
        public int UserId { get; set; }

        public int CustId { get; set; }

        public DateTime MoveBetsDay { get; set; }

        public DateTime BalanceUpDay { get; set; }

        public bool ShowSB { get; set; }

        public bool ShowRB { get; set; }

        public bool ShowNG { get; set; }

        public bool ShowBA { get; set; }

        public bool ShowLC { get; set; }

        public bool ShowVS { get; set; }

        public bool ShowCS { get; set; }

        public bool ShowBG { get; set; }
        
        // Output parameters
        public decimal TotalRows { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_User_BetList";
        }
    }
}