namespace Fanex.Affiliate.Controllers
{
    using System.Web.Mvc;
    using ONEbook.UM.Filters;

    /// <summary>
    /// Program Controller.
    /// </summary>
    [CheckUMFilter]
    public class ProgramController : BaseController
    {
        /// <summary>
        /// Action program.
        /// </summary>
        /// <returns>Return view.</returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}