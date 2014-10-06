// ----------------------------------------------------------------------
// <copyright file="BetListResultsRNGCasinoDetailViewModel.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/12/04">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.ViewModels
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Criteria;
    using DTO.BetList;

    /// <summary>
    /// RNG Casino Result Detail class.
    /// </summary>
    [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Reviewed. Suppression is OK here")]
    [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Since Betlist.dll used that data-structure.")]
    public class BetListResultsRNGCasinoDetailViewModel
    {
        public BetListResultsRNGCasinoDetailViewModel()
        {
            CasinoDetailList = new List<RNGCasinoResultWinlossDetail>();
            CasinoDetailHeaderList = new List<RNGCasinoResultWinlossDetailHeader>();
        }

        public BetListResultsRNGCasinoDetailCriteria Param { get; set; }

        public IList<RNGCasinoResultWinlossDetail> CasinoDetailList { get; set; }

        public IList<RNGCasinoResultWinlossDetailHeader> CasinoDetailHeaderList { get; set; }
    }
}