// ----------------------------------------------------------------------
// <copyright file="IContactUsDataAccess.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Jeff.Nguyen" date="2013/07/05">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.DataAccess.Interface
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