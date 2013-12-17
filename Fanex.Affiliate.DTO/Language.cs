// --------------------------------------------------------------------------
// <copyright file="Language.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.06.14">Created</change>
// </history>
namespace Fanex.Affiliate.DTO
{
    using System;

    /// <summary>
    /// Language data object.
    /// </summary>
    public class Language
    {
        public string LanguageId { get; set; }

        public string LanguageName { get; set; }

        public string CountryID { get; set; }

        public int Creator { get; set; }

        public DateTime CreatedDate { get; set; }

        public short Order { get; set; }
    }
}