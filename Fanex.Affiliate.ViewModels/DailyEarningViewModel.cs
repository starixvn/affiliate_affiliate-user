namespace Fanex.Affiliate.ViewModels
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using DTO;

    /// <summary>
    /// Daily Earning View Model.
    /// </summary>
    [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Reviewed. Suppression is OK here")]
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