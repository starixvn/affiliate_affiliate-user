namespace Fanex.Affiliate.Services
{
    using System;
    using Common.Helper;
    using Fanex.Affiliate.Criteria.Payment;

    public class DbLogger : IDbLogger
    {
        public void WritePaymentLog(PaymentLogCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            throw new NotImplementedException();
        }
    }
}