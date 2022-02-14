namespace Fanex.Affiliate.DataAccess
{
    using Common;
    using Criteria;
    using Data;
    using Fanex.Data.SqlServer;

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
            using (var objectDb = new SqlObjectDb(criteria.GetSettingKey()))
            {
                objectDb.ExecuteNonQuery(criteria);
                var currentParams = objectDb.GetOutputParams();
                criteria.ErrCode = Converter.ToInt(currentParams["errcode"]);
            }
        }
    }
}
