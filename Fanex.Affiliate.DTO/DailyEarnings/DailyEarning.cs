namespace Fanex.Affiliate.DTO
{
    using System;

    /// <summary>
    /// Daily earnings.
    /// </summary>
    public class DailyEarning
    {
        public DateTime WinLostDate { get; set; }

        public decimal Deposit { get; set; }

        public decimal Withdrawal { get; set; }

        public decimal MemberWinlost { get; set; }

        public decimal MemWinlostFake { get; set; }

        public decimal Bonus { get; set; }

        public decimal Adjustment { get; set; }

        public decimal Turnover { get; set; }

        public decimal Psp { get; set; }

        public decimal PlayerComm { get; set; }

        public decimal PWin { get; set; }

        public decimal PShare { get; set; }

        public decimal NetGaming { get; set; }
    }
}