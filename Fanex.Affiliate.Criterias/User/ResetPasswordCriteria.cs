// ----------------------------------------------------------------------
// <copyright file="ResetPasswordCriteria.cs" company="Nexcel Solutions Vietnam">
//     Copyright (coffee) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/06/24">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.Criteria
{
    using System.ComponentModel.DataAnnotations;
    using Fanex.Data.Repository;
    using Resources;

    /// <summary>
    /// Criteria for changing the password.
    /// </summary>
    public class ResetPasswordCriteria : CriteriaBase
    {
        public int AffID { get; set; }

        public string ValidCode { get; set; }

        public string NewPwd { get; set; }

        public string Language { get; set; }

        // Output parameters
        [Required(ErrorMessageResourceName = "Msg120", ErrorMessageResourceType = typeof(Lang))]
        [RegularExpression(
            @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}$",
            ErrorMessageResourceType = typeof(Lang),
            ErrorMessageResourceName = "Msg119")]
        public string Email { get; set; }

        public string Username { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string PreferredLanguage { get; set; }

        public bool MailOption { get; set; }

        public int ErrCode { get; set; }

        /// <summary>
        /// Get the reference SP's Name which is specified in Config file.
        /// </summary>
        /// <returns>Store name.</returns>
        public override string GetSettingKey()
        {
            return "Aff_User_AffiliateRecoverPwd";
        }
    }
}
