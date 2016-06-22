namespace Fanex.Affiliate.Site
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Web.UI;
    using Configurations;
    using Criteria;
    using Services;
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
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Reviewed. Suppression is OK here.")]
        protected void Page_Load(object sender, EventArgs e)
        {
            string lang = Request.QueryString["lang"] ?? AppSettings.DefaultLanguage;

            string defaultLang;
            try
            {
                var systemSettings = _systemSettingService.ListSystemSetting(new SystemSettingsByTypeCriteria
                                                    {
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