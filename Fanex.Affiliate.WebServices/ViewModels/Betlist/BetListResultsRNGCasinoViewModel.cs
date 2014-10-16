// ----------------------------------------------------------------------
// <copyright file="BetListResultsRNGCasinoViewModel.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/12/04">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.WebServices.ViewModels
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Criteria;
    using DTO.BetList;

    /// <summary>
    /// RNG Casino Result class.
    /// </summary>
    [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Reviewed. Suppression is OK here")]
    [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Since Betlist.dll used that data-structure.")]
    public class BetListResultsRNGCasinoViewModel
    {
        public BetListResultsRNGCasinoViewModel(
            IList<RNGCasinoResult> data,
            IList<RNGCasinoResult> subTotal,
            BetListResultsRNGCasinoCriteria criteria)
        {
            Data = data;
            SubTotal = subTotal;
            Param = criteria;
            RefNo = criteria.RefNo.ToString();
            IsShowPic = criteria.IsShowPic;
            Username = criteria.SystemId;
        }

        public IList<RNGCasinoResult> Data { get; set; }

        public IList<RNGCasinoResult> SubTotal { get; set; }

        public BetListResultsRNGCasinoCriteria Param { get; set; }

        public string Username { get; set; }

        public string RefNo { get; set; }

        public string Currency { get; set; }

        public bool IsShowPic { get; set; }
    }
}