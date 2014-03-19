// ----------------------------------------------------------------------
// <copyright file="IHotNewsDataAccess.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Brian.Dinh" date="2013/07/15">Created</change>
//   <change who="" date=""></change>
// </history>
namespace Fanex.Affiliate.DataAccess.Interface
{
    using Criteria;
    using DTO;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IHotNewsDataAccess
    {
        /// <summary>
        /// Get hot news.
        /// </summary>
        /// <param name="criteria">Hot news criteria.</param>
        /// <returns>Hot news model.</returns>
        HotNews GetHotNews(HotNewsCriteria criteria);
    }
}
