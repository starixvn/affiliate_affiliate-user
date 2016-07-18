namespace Fanex.Affiliate.Banner.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// Home controller of Affiliate Banner.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Main page of Affiliate Banner.
        /// </summary>
        /// <returns>Action Result.</returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}
