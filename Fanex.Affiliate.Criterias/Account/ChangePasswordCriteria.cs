namespace Fanex.Affiliate.Criteria
{
    using Fanex.Data.Repository;
    using Resources;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    /// <summary>
    /// Change Password Criteria.
    /// </summary>
    public class ChangePasswordCriteria : CriteriaBase
    {
        public int AffId { get; set; }

        [Required(ErrorMessageResourceName = "OldPasswordRequired", ErrorMessageResourceType = typeof(Lang))]
        [SpParam(Ignored = true)]
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

        [SpParam(Ignored = true)]
        public string SecureToken { get; set; }

        public bool IsSuccess { get; set; }

        public string IP { get; set; }

        public string Language { get; set; }

        public int ErrCode { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_User_SecureUpdPasswordUpdate";
        }
    }
}