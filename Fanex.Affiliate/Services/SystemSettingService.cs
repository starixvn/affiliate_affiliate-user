namespace Fanex.Affiliate.Services
{
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Common.Helper;
    using Criteria;
    using Data;
    using DTO;
    using Fanex.Affiliate.Resources;
    using Fanex.Data.SqlServer;

    /// <summary>
    /// System Setting Service.
    /// </summary>
    public class SystemSettingService : QueryService, ISystemSettingService
    {
        #region Fields

        #endregion Fields

        #region Constructor

        #endregion Constructor

        /// <summary>
        /// Get list setting.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Return list system setting.</returns>
        public IEnumerable<SystemSetting> ListSystemSetting(SystemSettingsByTypeCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var result = GetData<SystemSetting>(criteria);

            foreach (var item in result)
            {
                item.DisplayText = item.SValue;

                var translated = Lang.ResourceManager.GetString(item.DisplayText.Replace(" ", string.Empty));
                item.DisplayText = string.IsNullOrEmpty(translated) ? item.DisplayText : translated;
            }

            return result;
        }

        /// <summary>
        /// Get System Date time.
        /// </summary>
        /// <param name="criteria">System Date time criteria.</param>
        /// <returns>Return system date time.</returns>
        public SystemDateTime GetSystemDateTime(SystemDateTimeCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            return GetData<SystemDateTime>(criteria).FirstOrDefault();
        }

        /// <summary>
        /// Get All Affiliate's System Settings.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Return DataSet.</returns>
        public DataSet GetAllSystemSettings(SystemSettingsCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            using (var objectDb = new SqlObjectDb(criteria.GetSettingKey()))
            {
                return objectDb.ExecuteDataSet(criteria);
            }
        }
    }
}