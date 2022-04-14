namespace Fanex.Affiliate.Criteria
{
    using System;

    /// <summary>
    /// Criteria for cancelling the payment request.
    /// </summary>
    public class PaymentCancelCriteria : CriteriaBaseImpl
    {
        public long TransID { get; set; }

        public string Status { get; set; }

        public string AdminDesc { get; set; }

        public string AffDesc { get; set; }

        public int UserID { get; set; }

        public string IP { get; set; }

        public long RefNo { get; set; }

        public string Language { get; set; }

        public long IbcRefNo { get; set; }

        public long IbcTransId { get; set; }

        public string IBCUsername { get; set; }

        // Output parameters
        public string PreferredLanguage { get; set; }

        public bool MailOption { get; set; }

        public DateTime TransDate { get; set; }

        public decimal Amount { get; set; }

        public int ErrCode { get; set; }

        /// <summary>
        /// Get the reference SP's Name which is specified in Config file.
        /// </summary>
        /// <returns>Affiliate Payment Request Insert.</returns>
        public override string GetSettingKey()
        {
            return "Aff_User_PaymentRequestUpd";
        }
    }
}