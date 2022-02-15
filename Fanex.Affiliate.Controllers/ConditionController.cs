namespace Fanex.Affiliate.Controllers
{
    using System.Web.Mvc;
    using ONEbook.UM.AspNetMVC.Filters;

    [CheckUMFilter]
    public class ConditionController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}