namespace Fanex.Affiliate.Services
{
    using System.Collections.Generic;
    using Criteria;
    using DTO;

    /// <summary>
    /// Interface Message service.
    /// </summary>
    public interface IMessageService
    {
        /// <summary>
        /// Get message list.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <param name="totalPages">Total page for paging.</param>
        /// <returns>Message list.</returns>
        IEnumerable<Message> GetMessageList(MessageCriteria criteria, ref int totalPages);
    }
}