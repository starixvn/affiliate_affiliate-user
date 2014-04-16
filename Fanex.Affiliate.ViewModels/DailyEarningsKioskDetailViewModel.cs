// ----------------------------------------------------------------------
// <copyright file="DailyEarningsKioskDetailViewModel.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/08/22">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.ViewModels
{
    using System;
    using System.Collections.Generic;
    using DTO;

    /// <summary>
    /// Daily Earnings Kiosk Detail View Model.
    /// </summary>
    public class DailyEarningsKioskDetailViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DailyEarningsKioskDetailViewModel" /> class.
        /// </summary>
        public DailyEarningsKioskDetailViewModel()
        {
            ListProduct = new List<Product>();
        }

        public DateTime MonthDate { get; set; }

        public int ProductId { get; set; }

        public List<Product> ListProduct { get; set; }

        public List<DailyEarningsDetail> ListDailyEarningDetail { get; set; }
    }
}