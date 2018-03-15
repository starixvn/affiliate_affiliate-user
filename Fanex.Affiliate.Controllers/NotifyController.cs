namespace Fanex.Affiliate.Controllers
{
    using System.Web.Mvc;
    using Configurations;
    using ONEbook.UM.Services;

    public class NotifyController : Controller
    {
        private readonly IMaintenanceService _maintenanceService;

        public NotifyController(IMaintenanceService maintenanceService)
        {
            _maintenanceService = maintenanceService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            if (!_maintenanceService.IsUnderMaintenance())
            {
                return RedirectToAction("Index", "Home");
            }

            var underMaintenanceTime = _maintenanceService.GetUnderMaintenanceTime();
            ViewBag.StartTime = underMaintenanceTime.From;
            ViewBag.EndTime = underMaintenanceTime.To;
            ViewBag.AffCustomerServiceUrl = AppSettings.AffCustomerServiceUrl;
            ViewBag.ContactEmail = string.Format(
                "mailto:{0}?subject=Enquiries%20sent%20from%20{1}%20website",
                AppSettings.MailContact,
                AppSettings.SystemDisplayName);

            return View();
        }

        [HttpGet]
        public ActionResult Error()
        {
            return View();
        }

        [HttpGet]
        public ActionResult PageNotFound()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}