// --------------------------------------------------------------------------
// <copyright file="SystemSettingService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.06.25">Created</change>
//   <change who="Muller.Nguyen" date="2013.07.04">Add properties ListCurrency</change>
// </history>

namespace Fanex.Affiliate.Services
{
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Common.Helper;
    using Criteria;
    using Data;
    using DTO;

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
            return GetData<SystemSetting>(criteria);
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
            using (var objectDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var param = criteria.GetSpParams();
                return objectDb.ExecuteDataSet(param);
            }
        }
    }
}