// ----------------------------------------------------------------------
// <copyright file="AffiliateDataAccess.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2014/02/24">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.WebServices.DataAccess
{
    using Fanex.Data;

    /// <summary>
    /// Account data access.
    /// </summary>
    public class AffiliateDataAccess : IAffiliateDataAccess
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AffiliateDataAccess" /> class.
        /// </summary>
        public AffiliateDataAccess()
        {
        }

        /// <summary>
        /// Get Affiliate Information.
        /// </summary>
        /// <param name="criteria">Affiliate Information criteria.</param>
        /// <param name="siteName">Site Name.</param>
        /// <returns>Affiliate Information.</returns>
        public AffiliateEmailInfo GetAffiliateInfo(AffiliateInfoCriteria criteria, string siteName)
        {
            var result = new AffiliateEmailInfo();

            using (var objectDb = new ObjectDb(criteria.GetSettingKey(siteName)))
            {
                var param = criteria.GetSpParams();
                var dataSet = objectDb.ExecuteDataSet(param);
                
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    result.Username = dataSet.Tables[0].Rows[0]["username"].ToString();
                    result.Fullname = dataSet.Tables[0].Rows[0]["fullname"].ToString();
                    result.Email = dataSet.Tables[0].Rows[0]["email"].ToString();
                }
            }

            return result;
        }
    }
}