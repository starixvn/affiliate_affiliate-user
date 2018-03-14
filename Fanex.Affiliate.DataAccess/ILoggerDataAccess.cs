namespace Fanex.Affiliate.DataAccess
{
    using Fanex.Affiliate.Criteria.Payment;

    public interface ILoggerDataAccess
    {
        void WritePaymentLog(PaymentLogCriteria criteria);
    }
}
