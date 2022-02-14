namespace Fanex.Affiliate.DataAccess
{
    using Fanex.Affiliate.Criteria.Payment;
    using Fanex.Data.SqlServer;

    public class LoggerDataAccess : ILoggerDataAccess
    {
        public void WritePaymentLog(PaymentLogCriteria criteria)
        {
            using (var objectDb = new SqlObjectDb(criteria.GetSettingKey()))
            {
                objectDb.ExecuteNonQuery(criteria);
            }
        }
    }
}