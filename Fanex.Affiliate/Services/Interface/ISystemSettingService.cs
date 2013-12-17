// --------------------------------------------------------------------------
// <copyright file="ISystemSettingService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.06.25">Created</change>
//   <change who="Muller.Nguyen" date="2013.07.04">Add properties ListCurrency</change>
// </history>
namespace Fanex.Affiliate.Services.Interface
{
    using System.Collections.Generic;
    using System.Data;
    using Criterias;
    using DTO;

    public interface ISystemSettingService
    {
        /// <summary>
        /// Get list setting.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Return list system setting.</returns>
        IEnumerable<SystemSetting> ListSystemSetting(SystemSettingCriteria criteria);
        
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