namespace Fanex.Affiliate.Services
{
    using Criteria;

    /// <summary>
    /// Contact Us Service.
    /// </summary>
    public interface IContactUsService
    {
        /// <summary>
        /// Contact us.
        /// </summary>
        /// <param name="criteria">Contact us criteria.</param>
        void ContactUs(ContactUsCriteria criteria);
    }
}
