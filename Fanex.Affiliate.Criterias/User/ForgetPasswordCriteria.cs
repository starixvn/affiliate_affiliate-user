namespace Fanex.Affiliate.Criteria
{
    using System.ComponentModel.DataAnnotations;
    using Resources;

    /// <summary>
    /// Forget Password Criteria.
    /// </summary>
    public class ForgetPasswordCriteria : CriteriaBaseImpl
    {
        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "Msg120")]
        [Display(Name = "Email", ResourceType = typeof(Lang))]
        [RegularExpression(
            @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}$",
            ErrorMessageResourceType = typeof(Lang),
            ErrorMessageResourceName = "Msg119")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "MsgEmptyCaptcha")]
        public string ValidCode { get; set; }

        public string Language { get; set; }

        public int AffID { get; set; }

        public string IP { get; set; }

        // Output parameters
        public string Username { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string PreferredLanguage { get; set; }

        public bool MailOption { get; set; }

        public int ErrCode { get; set; }

        /// <summary>
        /// Get the reference SP's Name which is specified in Config file.
        /// </summary>
        /// <returns>Store Procedure.</returns>
        public override string GetSettingKey()
        {
            return "Aff_User_AffiliateForgotPwd";
        }
    }
}
