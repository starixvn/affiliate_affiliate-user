namespace Fanex.Affiliate.DTO
{
    using System;

    /// <summary>
    /// Payment data object.
    /// </summary>
    public class Payment
    {
        public long TransID { get; set; }

        public long RefID { get; set; }

        public DateTime TransDate { get; set; }

        public DateTime CompletedDate { get; set; }

        public string Status { get; set; }

        public decimal Amount { get; set; }

        public string AffDesc { get; set; }
    }
}