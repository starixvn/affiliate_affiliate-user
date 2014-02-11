// --------------------------------------------------------------------------
// <copyright file="UnderMaintenanceService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.06.05">Created</change>
//   <change who="Johnny.Trinh" date="2013.07.03">Created</change>
// </history>

namespace Fanex.Affiliate.Services.Impl
{
    using System;
    using System.Runtime.Caching;
    using Fanex.Affiliate.Configurations;
    using Sunplus.UMControllerLibrary;

    /// <summary>
    /// Under Maintenance Service.
    /// </summary>
    public static class UnderMaintenanceService
    {
        private const int TimeCacheUm = 30;

        private static readonly MemoryCache MemoryCache = MemoryCache.Default;

        /// <summary>
        /// Get Down time.
        /// </summary>
        /// <param name="startTime">Start time.</param>
        /// <param name="endTime">End time parameter.</param>
        /// <returns>Is down time or not.</returns>
        public static bool GetDownTime(out string startTime, out string endTime)
        {
            startTime = endTime = null;
            var now = DateTime.Now;
            if (now >= AppSettings.UmStartTime && now <= AppSettings.UmEndTime)
            {
                startTime = AppSettings.UmStartTime.ToString("hh:mm tt");
                endTime = AppSettings.UmEndTime.ToString("hh:mm tt");
                return true;
            }

            if (string.IsNullOrEmpty(AppSettings.LbUrl))
            {
                return false;
            }

            bool checkUm = false;

            if (MemoryCache["IsUM"] == null)
            {
                // Get Agnet Site UM Status.
                var undermaintainCtrl = new UMBusiness();
                checkUm = undermaintainCtrl.isUnderMaintainence(Sites.Affiliate, AppSettings.LbUrl, ref startTime, ref endTime);
                MemoryCache.Add("IsUM", checkUm, DateTime.Now.AddSeconds(TimeCacheUm));
                MemoryCache.Add("StartTime", startTime, DateTime.Now.AddSeconds(TimeCacheUm));
                MemoryCache.Add("EndTime", endTime, DateTime.Now.AddSeconds(TimeCacheUm));
            }
            else
            {
                checkUm = Convert.ToBoolean(MemoryCache["IsUM"]);
                startTime = Convert.ToString(MemoryCache["StartTime"]);
                endTime = Convert.ToString(MemoryCache["EndTime"]);
            }

            return checkUm;
        }
    }
}