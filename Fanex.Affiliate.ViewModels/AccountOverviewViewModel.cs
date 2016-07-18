namespace Fanex.Affiliate.ViewModels
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using DTO;

    /// <summary>
    /// Account View Model.
    /// </summary>
    [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Reviewed. Suppression is OK here")]
    [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Since Betlist.dll used that data-structure.")]
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