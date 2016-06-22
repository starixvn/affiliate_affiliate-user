namespace Fanex.Affiliate.Controllers
{
    using System.Web.Mvc;
    using ONEbook.UM.Filters;

    /// <summary>
    /// FAQs Controller.
    /// </summary>
    [CheckUMFilter]
    public class FAQsController : BaseController
    {
        /// <summary>
        /// Action FAQs.
        /// </summary>
        /// <returns>Return view.</returns>
        [HttpGet]
        public ActionResult Index()
        {
            var viewModel = GetSiteDomainInfo();
            return View(viewModel);
        }
    }
}