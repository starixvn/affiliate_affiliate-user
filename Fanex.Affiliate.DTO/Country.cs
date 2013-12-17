// --------------------------------------------------------------------------
// <copyright file="Country.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.06.11">Created</change>
// </history>

namespace Fanex.Affiliate.DTO
{
    /// <summary>
    /// Country data object.
    /// </summary>
    public class Country
    {
        public string CountryId { get; set; }

        public string CountryName { get; set; }

        public string Description { get; set; }

        public string Icon { get; set; }

        public short DisplayOrder { get; set; }

        public bool IsEnabled { get; set; }
    }
}