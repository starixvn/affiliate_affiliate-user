// ----------------------------------------------------------------------
// <copyright file="IHotNewsService.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Brian.Dinh" date="2013/07/15">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.Services.Interface
{
    using Criterias;
    using ViewModels;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IHotNewsService
    {
        HotNewsViewModel GetHotNews(HotNewsCriteria criteria);
    }
}
