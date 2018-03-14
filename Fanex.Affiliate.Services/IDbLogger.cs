namespace Fanex.Affiliate.Services
{
    using Fanex.Affiliate.Criteria.Payment;

    public interface IDbLogger
    {
        void WritePaymentLog(PaymentLogCriteria criteria);
    }
}
