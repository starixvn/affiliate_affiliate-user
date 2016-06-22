namespace Fanex.Affiliate.Criteria
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// Criteria for inserting the new payment request.
    /// </summary>
    public class PaymentRequestCriteria : CriteriaBase
    {
        public int AffId { get; set; }

        public string Player_UserName { get; set; }

        public string Player_IBCUserName { get; set; }

        public string Player_Currency { get; set; }

        public decimal Amount { get; set; }

        public string IP { get; set; }

        public string Language { get; set; }

        // Output parameters
        public string PreferredLanguage { get; set; }

        public bool MailOption { get; set; }

        public DateTime? TransDate { get; set; }

        public long TransID { get; set; }

        public int ErrCode { get; set; }

        /// <summary>
        /// Get the reference SP's Name which is specified in Config file.
        /// </summary>
        /// <returns>Store procedure payment request insert.</returns>
        public override string GetSettingKey()
        {
            return "Aff_User_PaymentRequestIns";
        }
    }
}