namespace Fanex.Affiliate.Controllers
{
    using System.Web.Mvc;
    using ONEbook.UM.Filters;

    /// <summary>
    /// Condition Controller.
    /// </summary>
    [CheckUMFilter]
    public class ConditionController : BaseController
    {
        /// <summary>
        /// Get Condition page.
        /// </summary>
        /// <returns>Return View.</returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}