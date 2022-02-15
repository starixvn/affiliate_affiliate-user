namespace Fanex.Affiliate.Controllers
{
    using System.Web.Mvc;
    using ONEbook.UM.AspNetMVC.Filters;

    [CheckUMFilter]
    public class FAQsController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            var viewModel = GetSiteDomainInfo();
            return View(viewModel);
        }
    }
}