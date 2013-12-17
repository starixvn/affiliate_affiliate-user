// -----------------------------------------------------------------------
// <copyright file="DefaultProductCommissionViewModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Brian.Dinh" date="2013.07.09">Created</change>
// </history>

namespace Fanex.Affiliate.ViewModels
{
    using System.Collections.Generic;
    using DTO;

    /// <summary>
    /// Default Product Commission View Model.
    /// </summary>
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