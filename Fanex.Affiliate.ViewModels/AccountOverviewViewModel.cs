// --------------------------------------------------------------------------
// <copyright file="AccountOverviewViewModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.06.10">Created</change>
// </history>

namespace Fanex.Affiliate.ViewModels
{
    using System.Collections.Generic;
    using DTO;

    /// <summary>
    /// Account View Model.
    /// </summary>
    public class AccountOverviewViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountOverviewViewModel" /> class.
        /// </summary>
        public AccountOverviewViewModel()
        {
            ListProductOnCommissionSetting = new List<Product>();
            ListGroupSetting = new List<GroupSetting>();
        }

        public int Month { get; set; }

        public int Year { get; set; }

        public StatisticsOverview Statistics { get; set; }

        public List<Product> ListProduct { get; set; }

        public List<Product> ListProductOnCommissionSetting { get; set; }

        public List<GroupSetting> ListGroupSetting { get; set; }

        public string FirstListProduct { get; set; }

        public string TotalqualifedMembers { get; set; }
    }
}