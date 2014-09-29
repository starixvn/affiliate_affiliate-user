// --------------------------------------------------------------------------
// <copyright file="AffiliateLoginCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.05.30">Created</change>
// </history>

namespace Fanex.Affiliate.Criteria
{
    using System.Web.Mvc;
    using NAP.Framework.Data;

    /// <summary>
    /// Authenticate User Criteria.
    /// </summary>
    public class AffiliateLoginCriteria : CriteriaBase
    {
        [AllowHtml]
        public string Username { get; set; }

        public int InputAffType { get; set; }

        public int ServerId { get; set; }

        public string LoginIp { get; set; }

        [SpParam(Ignored = true)]
        public string Captcha { get; set; }

        // Output parameters
        public int ErrCode { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_User_SecureLoginInfo";
        }

        public override bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Username);
        }
    }
}