// ----------------------------------------------------------------------
// <copyright file="ProductService.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/08/20">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.Services.Impl
{
    using System.Collections.Generic;
    using Common.Helper;
    using Criterias;
    using DTO;
    using Interface;

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
