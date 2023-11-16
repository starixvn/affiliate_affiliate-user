namespace Fanex.Affiliate.Banner.Controllers
{
    using System.Web.Mvc;
    using Services;
    public class UnderMaintenanceController : Controller
    {
        private readonly IUnderMaintenanceService _underMaintenanceService;

        public UnderMaintenanceController()
        {
            _underMaintenanceService = new UnderMaintenanceService();
        }

        [HttpGet]
        public ActionResult Index()
        {
            var maintenanceInfo = _underMaintenanceService.GetUnderMaintenanceTime();

            if (!maintenanceInfo.IsUnderMaintenanceTime)
            {
                return Redirect("~/Home");
            }

            return JavaScript($"document.getElementById('bannerContainer').innerHTML = 'System is currently unavailable due to scheduled maintenance from { maintenanceInfo.From} → { maintenanceInfo.To} (HK Time GMT +8). Thanks for your patience!';");
        }
    }
}