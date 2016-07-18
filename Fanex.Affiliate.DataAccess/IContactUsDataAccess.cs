namespace Fanex.Affiliate.DataAccess
{
    using Criteria;

    /// <summary>
    /// Contact Us Data Access.
    /// </summary>
    public interface IContactUsDataAccess
    {
        /// <summary>
        /// Contact us.
        /// </summary>
        /// <param name="criteria">Contact us criteria.</param>
        void ContactUs(ContactUsCriteria criteria);
    }
}