namespace Fanex.Affiliate.Criteria
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using Fanex.Affiliate.Resources;
    using Fanex.Data.Repository;

    /// <summary>
    /// Contact Us Criteria.
    /// </summary>
    public class ContactUsCriteria : CriteriaBase
    {
        public int MID { get; set; }

        public int Creator { get; set; }

        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "CommentIsRequired")]
        [AllowHtml]
        public string Content { get; set; }

        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "NameIsRequired")]
        [AllowHtml]
        public string Sender { get; set; }

        [AllowHtml]
        public string Affiliate { get; set; }

        [Required(ErrorMessageResourceName = "Msg120", ErrorMessageResourceType = typeof(Lang))]
        [RegularExpression(
            @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}$",
            ErrorMessageResourceType = typeof(Lang),
            ErrorMessageResourceName = "Msg119")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "SubjectIsRequired")]
        [AllowHtml]
        public string Subject { get; set; }

        public string IP { get; set; }

        public string Browser { get; set; }

        public string SessionId { get; set; }

        public int ErrCode { get; set; }

        [SpParam(Ignored = true)]
        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "MsgEmptyCaptcha")]
        public string ValidCode { get; set; }

        /// <summary>
        /// Get the reference SP's Name which is specified in Config file.
        /// </summary>
        /// <returns>Store Procedure.</returns>
        public override string GetSettingKey()
        {
            return "Aff_User_EmailHistoryIns";
        }
    }
}
