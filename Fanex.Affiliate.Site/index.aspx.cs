// ----------------------------------------------------------------------
// <copyright file="index.aspx.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/08/09">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.Site
{
    using System;
    using System.Linq;
    using System.Web.UI;
    using Fanex.Affiliate.Configurations;
    using Fanex.Affiliate.Criteria;
    using Fanex.Affiliate.Services.Impl;
    using Web;

    /// <summary>
    /// Index page for redirecting from affiliate's member site.
    /// </summary>
    public partial class Index : Page
    {
        private SystemSettingService _systemSettingService = new SystemSettingService();

        /// <summary>
        /// Page load.
        /// </summary>
        /// <param name="sender">Object sender.</param>
        /// <param name="e">Event arguments.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            string lang = Request.QueryString["lang"] ?? AppSettings.DefaultLanguage;

            string defaultLang;
            try
            {
                var systemSettings = _systemSettingService.ListSystemSetting(new SystemSettingCriteria
                                                    {
                                                        IsType = true,
                                                        Type = "LangMapping"
                                                    });
                defaultLang = systemSettings.FirstOrDefault(x => x.SValue == lang.Trim().ToLower()).SName;
            }
            catch
            {
                defaultLang = AppSettings.DefaultLanguage;
            }

            AppSession.Language = defaultLang;
            Response.Redirect("~/home");
        }
    }
}