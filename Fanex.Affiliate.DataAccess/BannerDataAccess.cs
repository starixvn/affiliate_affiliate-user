namespace Fanex.Affiliate.DataAccess
{
    using Common.Extensions;
    using Criteria;
    using Data;
    using DTO;
    using Fanex.Data.SqlServer;

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
            using (var objectDb = new SqlObjectDb(criteria.GetSettingKey()))
            {
                var firstRowBannerInfo = objectDb.ExecuteReader(criteria);

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
            using (var objectDb = new SqlObjectDb(criteria.GetSettingKey()))
            {
                objectDb.ExecuteNonQuery(criteria);
            }
        }

        /// <summary>
        /// Get System Name.
        /// </summary>
        /// <param name="settingKey">SP string name.</param>
        /// <returns>System name.</returns>
        public string BannerGetSystemName(string settingKey)
        {
            using (var objectDb = new SqlObjectDb(settingKey))
            {
                return objectDb.ExecuteScalar().ToString();
            }
        }
    }
}