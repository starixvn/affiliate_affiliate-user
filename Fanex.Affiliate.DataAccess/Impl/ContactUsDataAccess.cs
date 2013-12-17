// ----------------------------------------------------------------------
// <copyright file="ContactUsDataAccess.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Jeff.Nguyen" date="2013/07/05">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.DataAccess.Impl
{
    using Common;
    using Criterias;
    using Data;
    using Interface;

    /// <summary>
    /// TODO: Update summary.
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
