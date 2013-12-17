// --------------------------------------------------------------------------
// <copyright file="ChangePasswordCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Leo.Vo" date="2013.07.05">Created</change>
// </history>

namespace Fanex.Affiliate.Criterias
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using NAP.Framework.Data;
    using Resources;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ChangePasswordCriteria : CriteriaBase
    {
        public int AffId { get; set; }

        public string Ip { get; set; }

        public string Language { get; set; }

        public int ErrCode { get; set; }

        [Required(ErrorMessageResourceName = "OldPasswordRequired", ErrorMessageResourceType = typeof(Lang))]
        [SpParam(SqlParamName = "oldpwd")]
        [AllowHtml]
        public string OldPassword { get; set; }

        [Required(ErrorMessageResourceName = "NewPasswordRequired", ErrorMessageResourceType = typeof(Lang))]
        [SpParam(SqlParamName = "affpwd")]
        [AllowHtml]
        public string NewPassword { get; set; }

        [Required(ErrorMessageResourceName = "ConfirmPasswordRequired", ErrorMessageResourceType = typeof(Lang))]
        [SpParam(Ignored = true)]
        [AllowHtml]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_AffiliateUpdPwd";
        }
    }
}