namespace Fanex.Affiliate.Criteria
{
    using System.ComponentModel.DataAnnotations;
    using Fanex.Data.Repository;

    /// <summary>
    /// Admin Login Criteria.
    /// </summary>
    public class AdminLoginCriteria : CriteriaBase
    {
        public AdminLoginCriteria()
        {
            NetSessionId = string.Empty;
        }

        [Required]
        [SpParam(Ignored = true)]
        public string ChalengeStr { get; set; }

        [Required]
        [SpParam(Ignored = true)]
        public string AccKey { get; set; }

        [Required]
        [SpParam(Ignored = true)]
        public string CustName { get; set; }

        [Required]
        [SpParam(Ignored = true)]
        public int AccId { get; set; }

        [Required]
        [SpParam(SqlParamName = "on_userid")]
        public int AccOnId { get; set; }

        // Output parameters
        [SpParam(SqlParamName = "netsessionid")]
        public string NetSessionId { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_User_OnUserSelNetSessionID";
        }
    }
}