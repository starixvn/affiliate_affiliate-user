namespace Fanex.Affiliate.DataAccess
{
    using Data;
    using Fanex.Affiliate.Criteria.Payment;

    public class LoggerDataAccess : ILoggerDataAccess
    {
        public void WritePaymentLog(PaymentLogCriteria criteria)
        {
            using (var objectDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var param = criteria.GetSpParams();
                objectDb.ExecuteNonQuery(param);
            }
        }
    }
}