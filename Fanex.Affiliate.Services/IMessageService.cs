// ----------------------------------------------------------------------
// <copyright file="IMessageService.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/08/20">Created</change>
//   <change who="" date=""></change>
// </history>

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