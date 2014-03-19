// --------------------------------------------------------------------------
// <copyright file="BannerDataAccess.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013.09.24">Created</change>
// </history>

namespace Fanex.Affiliate.DataAccess.Impl
{
    using Common.Extensions;
    using Criteria;
    using Data;
    using DTO;
    using Interface;

    /// <summary>
    /// Account data access.
    /// </summary>
    public class BannerDataAccess : IBannerDataAccess
    {
        /// <summary>
        /// Select Banner Information.
        /// </summary>
        /// <param name="criteria">Banner criteria.</param>
        /// <returns>Banner data object.</returns>
        public Banner BannerSelectInfo(BannerInfoCriteria criteria)
        {
            var bannerInfo = new Banner();
            using (var objectDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var param = criteria.GetSpParams();
                var firstRowBannerInfo = objectDb.ExecuteReader(param);

                // Get data to GroupSetting
                while (firstRowBannerInfo.Read())
                {
                    bannerInfo.BannerID = firstRowBannerInfo.ToInt("bannerid");
                    bannerInfo.Content = firstRowBannerInfo.ToString("content");
                    bannerInfo.ImageType = firstRowBannerInfo.ToShort("imagetype");
                    bannerInfo.Size = firstRowBannerInfo.ToString("size");
                    bannerInfo.Version = firstRowBannerInfo.ToInt("version");
                }
            }

            return bannerInfo;
        }
        
        /// <summary>
        /// Update Banner statistic.
        /// </summary>
        /// <param name="criteria">Banner criteria.</param>
        public void BannerUpdateStatistic(BannerUpdateStatisticCriteria criteria)
        {
            using (var objectDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var param = criteria.GetSpParams();
                objectDb.ExecuteNonQuery(param);
            }
        }

        /// <summary>
        /// Get System Name.
        /// </summary>
        /// <param name="settingKey">SP string name.</param>
        /// <returns>System name.</returns>
        public string BannerGetSystemName(string settingKey)
        {
            using (var objectDb = new ObjectDb(settingKey))
            {
                return objectDb.ExecuteScalar().ToString();
            }
        }
    }
}