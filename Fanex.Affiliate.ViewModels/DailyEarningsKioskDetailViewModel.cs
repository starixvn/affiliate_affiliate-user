namespace Fanex.Affiliate.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using DTO;

    /// <summary>
    /// Daily Earnings Kiosk Detail View Model.
    /// </summary>
    [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Reviewed. Suppression is OK here")]
    [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Since Betlist.dll used that data-structure.")]
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

        public bool ShowPlaytechCasinoData { get; set; }
    }
}