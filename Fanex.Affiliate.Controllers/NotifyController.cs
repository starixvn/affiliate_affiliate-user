namespace Fanex.Affiliate.Controllers
{
    using System.Web.Mvc;
    using Configurations;
    using ONEbook.UM.Services;

    /// <summary>
    /// Notify Controller.
    /// </summary>
    public class NotifyController : Controller
    {
        private readonly IMaintenanceService _maintenanceService;

        public NotifyController(IMaintenanceService maintenanceService)
        {
            _maintenanceService = maintenanceService;
        }

        /// <summary>
        /// Action Notify.
        /// </summary>
        /// <returns>Return view.</returns>
        [HttpGet]
        public ActionResult Index()
        {
            var underMaintenanceTime = _maintenanceService.GetUnderMaintenanceTime();
            if (!underMaintenanceTime.IsUnderMaintenanceTime)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.StartTime = underMaintenanceTime.From;
            ViewBag.EndTime = underMaintenanceTime.To;
            ViewBag.AffCustomerServiceUrl = AppSettings.AffCustomerServiceUrl;
            ViewBag.ContactEmail = string.Format(
                "mailto:{0}?subject=Enquiries%20sent%20from%20{1}%20website", 
                AppSettings.MailContact,
                AppSettings.SystemDisplayName);

            return View();
        }

        /// <summary>
        /// Action Error page.
        /// </summary>
        /// <returns>Return view.</returns>
        [HttpGet]
        public ActionResult Error()
        {
            return View();
        }

        /// <summary>
        /// Action page not found.
        /// </summary>
        /// <returns>Return view.</returns>
        [HttpGet]
        public ActionResult PageNotFound()
        {
            return View();
        }

        /// <summary>
        /// Action access denied.
        /// </summary>
        /// <returns>Return view.</returns>
        [HttpGet]
        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}