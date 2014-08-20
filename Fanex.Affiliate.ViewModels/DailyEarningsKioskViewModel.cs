// ----------------------------------------------------------------------
// <copyright file="DailyEarningsKioskViewModel.cs" company="Nexcel Solutions Vietnam">
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
    /// Daily Earnings Kiosk View Model.
    /// </summary>
    public class DailyEarningsKioskViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DailyEarningsKioskViewModel" /> class.
        /// </summary>
        public DailyEarningsKioskViewModel()
        {
            ListProduct = new List<Product>();
        }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public int ProductId { get; set; }

        public List<Product> ListProduct { get; set; }

        public List<DailyEarning> ListDailyEarning { get; set; }

        public bool ShowPlaytechCasinoData { get; set; }
    }
}