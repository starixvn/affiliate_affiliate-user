// --------------------------------------------------------------------------
// <copyright file="Marketing.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Muller.Nguyen" date="2013.06.28">Created</change>
// </history>

namespace Fanex.Affiliate.DTO
{
    /// <summary>
    /// Marketing data object.
    /// </summary>
    public class Marketing
    {
        public int BannerId { get; set; }

        public string Content { get; set; }

        public string Category { get; set; }

        public string Language { get; set; }

        public string LanguageCode { get; set; }

        public string ImageType { get; set; }

        public string Size { get; set; }
    }
}