using Fanex.Data.Repository;

namespace Fanex.Affiliate.Criteria
{
    public class CriteriaBaseImpl : CriteriaBase
    {
        public override string GetSettingKey()
        {
            return string.Empty;
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
