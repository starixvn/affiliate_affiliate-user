// ----------------------------------------------------------------------
// <copyright file="ContactUsService.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Jeff.Nguyen" date="2013/07/05">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.Services.Impl
{
    using Common.Helper;
    using Criterias;
    using DataAccess.Impl;
    using DataAccess.Interface; 
    using Interface;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ContactUsService : IContactUsService
    {
        #region Fields

        /// <summary>
        /// Contact Us DataAccess.
        /// </summary>
        private readonly IContactUsDataAccess _contactUsDataAccess;

        #endregion Fields

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactUsService" /> class.
        /// </summary>
        public ContactUsService()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactUsService" /> class.
        /// </summary>
        /// <param name="contactUsDataAccess">User DataAccess.</param>
        private ContactUsService(IContactUsDataAccess contactUsDataAccess)
        {
            _contactUsDataAccess = contactUsDataAccess ?? new ContactUsDataAccess();
        }

        #endregion Constructor
        /// <summary>
        /// Contact us.
        /// </summary>
        /// <param name="criteria">Contact us criteria.</param>
        public void ContactUs(ContactUsCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            _contactUsDataAccess.ContactUs(criteria);
        }
    }
}
