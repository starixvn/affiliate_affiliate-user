namespace Fanex.Affiliate.DataAccess
{
    using Common;
    using Criteria;
    using Data;

    /// <summary>
    /// Contact Us Data Access.
    /// </summary>
    public class ContactUsDataAccess : IContactUsDataAccess
    {
        /// <summary>
        /// Contact us.
        /// </summary>
        /// <param name="criteria">Contact us criteria.</param>
        public void ContactUs(ContactUsCriteria criteria)
        {
            using (var objectDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var currentParams = criteria.GetSpParams();
                objectDb.ExecuteNonQuery(currentParams);
                criteria.ErrCode = Converter.ToInt(currentParams["errcode"]);
            }
        }
    }
}
