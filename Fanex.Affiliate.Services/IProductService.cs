namespace Fanex.Affiliate.Services
{
    using System.Collections.Generic;
    using Criteria;
    using DTO;

    /// <summary>
    /// Interface Product service.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Get product list of affiliate.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Product list.</returns>
        IEnumerable<Product> GetProductList(ProductCriteria criteria);
    }
}
