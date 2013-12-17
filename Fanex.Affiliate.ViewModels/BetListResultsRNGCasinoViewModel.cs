// ----------------------------------------------------------------------
// <copyright file="BetListResultsRNGCasinoViewModel.cs" company="Nexcel Solutions Vietnam">
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
    using Criterias;
    using DTO.BetList;

    /// <summary>
    /// RNGCasinoResultViewModel class.
    /// </summary>
    public class BetListResultsRNGCasinoViewModel
    {
        public BetListResultsRNGCasinoViewModel()
        {
            Data = new List<RNGCasinoResult>();
        }

        public IList<RNGCasinoResult> Data { get; set; }

        public BetListResultsRNGCasinoCriteria Param { get; set; }

        public string ColumnName { get; set; }
    }
}