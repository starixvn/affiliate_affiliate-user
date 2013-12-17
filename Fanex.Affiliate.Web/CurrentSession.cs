// --------------------------------------------------------------------------
// <copyright file="CurrentSession.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.05.28">Created</change>
// </history>

namespace Fanex.Affiliate.Web
{
    using System;
    using DTO;

    /// <summary>
    /// Properties of an authenticated user.
    /// The implementation is applied when user config is state server mode.
    /// </summary>
    [Serializable]
    public class CurrentSession : ICurrentSession
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CurrentSession" /> class.
        /// </summary>
        public CurrentSession() 
        { 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrentSession" /> class.
        /// </summary>
        /// <param name="user">User info.</param>
        /// <param name="language">Input language.</param>
        public CurrentSession(User user, string language)
        {
            AffId = user.AffId;
            Username = user.Username;
            Currency = user.Currency;
            CurrencyID = user.CurrencyID;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            AffLevel = user.AffLevel;
            AffType = user.AffType;
            Language = language;
            PreferredLanguage = user.PreferLang;
            OnUserID = user.OnUserID;
            ExchangeRate = -1;
        }

        public int AffId { get; set; }

        public string Username { get; set; }

        public string Language { get; set; }

        public string PreferredLanguage { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int AffLevel { get; set; }

        public int AffType { get; set; }

        public int OnUserID { get; set; }

        public string Currency { get; set; }

        public int CurrencyID { get; set; }

        public bool IsInternalSite { get; set; }

        public SiteDomainInfo SiteDomainInfo { get; set; }

        public decimal ExchangeRate { get; set; }
    }
}