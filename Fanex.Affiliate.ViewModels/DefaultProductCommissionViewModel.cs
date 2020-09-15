namespace Fanex.Affiliate.ViewModels
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using DTO;

    /// <summary>
    /// Default Product Commission View Model.
    /// </summary>
    [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Reviewed. Suppression is OK here")]
    public class DefaultProductCommissionViewModel
    {
        public DefaultProductCommissionViewModel()
        {
            GroupSetting = new List<GroupSetting>();
            ProductList = new List<Product>();
        }
        
        public List<GroupSetting> GroupSetting { get; set; }

        public List<Product> ProductList { get; set; }
    }
}