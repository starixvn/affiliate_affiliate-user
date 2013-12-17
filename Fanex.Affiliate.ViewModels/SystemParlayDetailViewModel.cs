// ----------------------------------------------------------------------
// <copyright file="SystemParlayDetailViewModel.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/11/28">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.ViewModels
{
    using Fanex.Affiliate.Criterias;

    /// <summary>
    /// System Parlay Detail View Model class.
    /// </summary>
    public class SystemParlayDetailViewModel
    {
        public string DetailText { get; set; }

        public SystemParlayDetailCriteria Param { get; set; }
    }
}
