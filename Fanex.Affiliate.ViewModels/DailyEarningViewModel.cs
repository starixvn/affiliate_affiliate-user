// --------------------------------------------------------------------------
// <copyright file="DailyEarningViewModel.cs" company="Nexcel Solutions Vietnam">
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
    /// Daily Earning View Model.
    /// </summary>
    public class DailyEarningViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DailyEarningViewModel" /> class.
        /// </summary>
        public DailyEarningViewModel()
        {
            ListProduct = new List<Product>();
        }

        public int ProductId { get; set; }

        public List<Product> ListProduct { get; set; }

        public List<DailyEarning> ListDailyEarning { get; set; }
    }
}