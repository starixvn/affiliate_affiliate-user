namespace Fanex.Affiliate.Web.Mvc
{
    using System.Threading;
    using System.Web.Mvc;
    using Configurations;

    /// <summary>
    /// Custom Razor View Engine.
    /// </summary>
    public class CustomRazorViewEngine : RazorViewEngine
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomRazorViewEngine"/> class.
        /// </summary>
        public CustomRazorViewEngine()
        {
            const string ControllerContextFormat = @"{1}/{0}";
            var affiliateName = AppSettings.AffiliateName.ToLower();
            var layoutId = AppSettings.Layout;
            var customViewFormat = string.Format("~/Views/{0}/{1}.cshtml", affiliateName, ControllerContextFormat);
            var customLayoutFormat = string.Format("~/Views/Shared/Layout/Layout{0}/{1}.cshtml", layoutId, ControllerContextFormat);

            ViewLocationFormats = new string[]
                                      {
                                          customViewFormat,
                                          "~/Views/Shared/{1}/{0}.cshtml",
                                      };

            MasterLocationFormats = new string[] { customLayoutFormat };
            PartialViewLocationFormats = new string[] { "~/Views/Shared/Controls/{0}.cshtml" };

            FileExtensions = new string[] { "cshtml" };
        }

        /// <summary>
        /// Find a View.
        /// First check if there is the view of current culture. Example: MyView.vi-VN.
        /// Finally, call base.FindView.
        /// </summary>
        /// <param name="controllerContext">Controller Context.</param>
        /// <param name="viewName">View name.</param>
        /// <param name="masterName">Master name.</param>
        /// <param name="useCache">Use cache or not.</param>
        /// <returns>View engine result.</returns>
        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            // First check if there is the view of current culture. Example: MyView.vi-VN
            var currentCultureName = Thread.CurrentThread.CurrentUICulture.Name;

            var localizedViewName = string.IsNullOrEmpty(viewName)
                    ? viewName
                    : viewName + "." + currentCultureName;

            var cultureView = base.FindView(controllerContext, localizedViewName, masterName, useCache);

            // If culture view is not found, try with no cache
            if (cultureView.View == null && useCache)
            {
                cultureView = base.FindView(controllerContext, localizedViewName, masterName, false);
            }

            // If the culture view is not found, call base.FindView
            return cultureView.View == null
                    ? base.FindView(controllerContext, viewName, masterName, useCache)
                    : cultureView;
        }
    }
}