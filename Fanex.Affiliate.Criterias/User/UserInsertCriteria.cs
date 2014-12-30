// ----------------------------------------------------------------------
// <copyright file="UserInsertCriteria.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2014/03/20">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.Criteria
{
    using System.Diagnostics.CodeAnalysis;
    using System.Web.Mvc;
    using NAP.Framework.Data;

    /// <summary>
    /// User Insert Criteria.
    /// </summary>
    public class UserInsertCriteria : CriteriaBase
    {
        public int AffID { get; set; }

        public int AffType { get; set; }

        [AllowHtml]
        public string FirstName { get; set; }

        [AllowHtml]
        public string LastName { get; set; }

        public bool Gender { get; set; }

        public string Email { get; set; }

        [AllowHtml]
        public string Phone { get; set; }

        [AllowHtml]
        public string MobilePhone { get; set; }

        [AllowHtml]
        public string Address { get; set; }

        [AllowHtml]
        public string City { get; set; }

        [AllowHtml]
        public string State { get; set; }

        [AllowHtml]
        public string Postal { get; set; }

        [AllowHtml]
        public string Country { get; set; }

        public bool EmailFormat { get; set; }

        public short Currency { get; set; }

        [SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Justification = "Reviewed.")]
        public string SiteURL { get; set; }

        public string MoreSiteURLs { get; set; }

        public string PreferredLanguage { get; set; }

        public string PreferredPayment { get; set; }

        [AllowHtml]
        public string Player_Username { get; set; }

        public string Question { get; set; }

        [AllowHtml]
        public string Answer { get; set; }

        public string Ip { get; set; }

        public string Browser { get; set; }
        
        public string Language { get; set; }

        // Output parameters
        public string Username { get; set; }

        public int ErrCode { get; set; }
        
        public string ErrMsg { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_User_AffiliateIns";
        }
    }
}