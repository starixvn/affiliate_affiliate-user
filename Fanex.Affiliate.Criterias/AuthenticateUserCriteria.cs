// --------------------------------------------------------------------------
// <copyright file="AuthenticateUserCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.05.30">Created</change>
// </history>
namespace Fanex.Affiliate.Criterias
{
    using System.Web.Mvc;
    using NAP.Framework.Data;

    /// <summary>
    /// Authenticate User Criteria.
    /// </summary>
    public class AuthenticateUserCriteria : CriteriaBase
    {
        [AllowHtml]
        public string Username { get; set; }

        [AllowHtml]
        public string Password { get; set; }

        public string LoginIp { get; set; }

        public string Language { get; set; }

        public int ServerId { get; set; }

        public int InputAffType { get; set; }

        public string NetSessionID { get; set; }

        [SpParam(Ignored = true)]
        public string Captcha { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_AffiliateLogin";
        }

        public override bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
        }
    }
}