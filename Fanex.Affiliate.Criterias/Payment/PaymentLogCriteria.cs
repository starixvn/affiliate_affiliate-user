namespace Fanex.Affiliate.Criteria.Payment
{

    public class PaymentLogCriteria : CriteriaBaseImpl
    {
        public int UserId { get; set; }

        public string LogContent { get; set; }

        public override string GetSettingKey()
        {
            return "Aff_WebAPI_WriteLog";
        }
    }
}