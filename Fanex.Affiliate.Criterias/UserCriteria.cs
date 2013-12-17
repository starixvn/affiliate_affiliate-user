// --------------------------------------------------------------------------
// <copyright file="UserCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.06.10">Created and add Properties</change>
//   <change who="Kaden.Trinh" date="2013.07.01">One model for 3 Sps: select, insert, update</change>
// </history>
namespace Fanex.Affiliate.Criterias
{
    using System.Web.Mvc;
    using NAP.Framework.Data;

    public enum UserEnum
    {
        /// <summary>
        /// Affiliate Select By ID.
        /// </summary>
        AffiliateSelectByID = 0,

        /// <summary>
        /// Your account is closed.
        /// </summary>
        AffiliateRegister = 1,

        /// <summary>
        /// Your account is locked temporarily due to 5 consecutive failed login attempts.
        /// </summary>
        AffiliateUpdate = 2
    }

    /// <summary>
    /// Overview Criteria.
    /// </summary>
    public class UserCriteria : CriteriaBase
    {
        public int UserID { get; set; }

        public int AffID { get; set; }

        public int AffType { get; set; }

        [AllowHtml]
        public string FirstName { get; set; }

        [AllowHtml]
        public string LastName { get; set; }

        [AllowHtml]
        public string Password { get; set; }

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
        public string Country { get; set; }

        [AllowHtml]
        public string Postal { get; set; }

        public bool EmailFormat { get; set; }

        public short Currency { get; set; }

        public string SiteURL { get; set; }

        public string MoreSiteURLs { get; set; }

        public string PreferredLanguage { get; set; }

        public string PreferredPayment { get; set; }

        [AllowHtml]
        public string Player_Username { get; set; }
        
        public string PlayerCurrency { get; set; }

        public string Question { get; set; }

        [AllowHtml]
        public string Answer { get; set; }

        public string Ip { get; set; }

        public string Browser { get; set; }
        
        public string Language { get; set; }

        public string Username { get; set; }

        public int ErrCode { get; set; }
        
        public string ErrMsg { get; set; }

        [SpParam(Ignored = true)]
        public UserEnum Function { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            string settingKey = string.Empty;
            switch (Function)
            {
                case UserEnum.AffiliateSelectByID:
                    settingKey = "Aff_AffiliateSelEditInfo";
                    break;
                case UserEnum.AffiliateRegister:
                    settingKey = "Aff_AffiliateIns";
                    break;
                case UserEnum.AffiliateUpdate:
                    settingKey = "Aff_AffiliateUpdInfo";
                    break;
                default:
                    settingKey = "Aff_AffiliateIns";
                    break;
            }

            return settingKey;
        }
    }
}