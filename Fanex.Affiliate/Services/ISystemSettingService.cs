namespace Fanex.Affiliate.Services
{
    using System.Collections.Generic;
    using System.Data;
    using Criteria;
    using DTO;

    public interface ISystemSettingService
    {
        /// <summary>
        /// Get list setting.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Return list system setting.</returns>
        IEnumerable<SystemSetting> ListSystemSetting(SystemSettingsByTypeCriteria criteria);
        
        /// <summary>
        /// Get Affiliate System Date Time.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Return System Date Time.</returns>
        SystemDateTime GetSystemDateTime(SystemDateTimeCriteria criteria);

        /// <summary>
        /// Get All Affiliate's System Settings.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Return DataSet.</returns>
        DataSet GetAllSystemSettings(SystemSettingsCriteria criteria);
    }
}