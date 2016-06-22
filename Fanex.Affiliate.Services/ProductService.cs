namespace Fanex.Affiliate.Services
{
    using System.Collections.Generic;
    using Common.Helper;
    using Criteria;
    using DTO;

    /// <summary>
    /// Product Service.
    /// </summary>
    public class ProductService : QueryService, IProductService
    {
        /// <summary>
        /// Get product list of affiliate.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Product list.</returns>
        public IEnumerable<Product> GetProductList(ProductCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            return GetData<Product>(criteria);
        }
    }
}
