// --------------------------------------------------------------------------
// <copyright file="TrafficStatisticsViewModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.06.14">Created</change>
//   <change who="Muller.Nguyen" date="2013.06.25">Modefied</change>
// </history>

namespace Fanex.Affiliate.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using DTO;
    using Resources;

    /// <summary>
    /// Traffic Statistics View Model.
    /// </summary>
    public class TrafficStatisticsViewModel
    {
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string PageSelected { get; set; }

        public List<SystemSetting> PageList { get; set; }

        public string LanguageSelected { get; set; }

        public List<Language> LanguageList { get; set; }

        public IEnumerable<SelectListItem> PageSelectListItem
        {
            get
            {
                PageList.Insert(0, new SystemSetting { SValue = string.Empty, SName = Lang.All });
                var selectItemList = PageList.Select(page => new SelectListItem
                {
                    Value = page.SValue,
                    Text = string.IsNullOrEmpty(page.SValue) ? page.SName : page.SValue
                }).ToList<SelectListItem>();

                return selectItemList;
            }
        }

        public IEnumerable<SelectListItem> LanguageSelectListItem
        {
            get
            {
                LanguageList.Insert(0, new Language { LanguageId = string.Empty, LanguageName = Lang.All });
                var selectItemList = LanguageList.Select(lang => new SelectListItem
                {
                    Value = lang.LanguageId,
                    Text = lang.LanguageName
                }).ToList<SelectListItem>();

                LanguageList.RemoveAt(0);
                return selectItemList;
            }
        }

        public IEnumerable<TrafficStatistic> LandingPagesStatisticsList { get; set; }

        public IEnumerable<TrafficStatistic> ReferrersStatisticsList { get; set; }
    }
}