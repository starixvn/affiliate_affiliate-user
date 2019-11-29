namespace Fanex.Affiliate.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using DTO;

    /// <summary>
    /// Daily Earnings Kiosk View Model.
    /// </summary>
    [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Reviewed. Suppression is OK here")]
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