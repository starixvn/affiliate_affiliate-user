namespace Fanex.Affiliate.Controllers
{
    using System.Web.Mvc;
    using Configurations;
    using Criteria;
    using ONEbook.UM.Filters;
    using Services;
    using Web;

    /// <summary>
    /// Home Controller.
    /// </summary>
    public class HomeController : BaseController
    {
        #region Services

        /// <summary>
        /// Define account service.
        /// </summary>
        private readonly IAccountService _accountService;

        #endregion Services

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController" /> class.
        /// </summary>
        /// <param name="accountService">Account Service.</param>
        public HomeController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        #endregion Constructor

        /// <summary>
        /// Action Home.
        /// </summary>
        /// <returns>Return view.</returns>
        [HttpGet]
        [UMBackdoorFilter]
        public ActionResult Index()
        {
            var criteria = new DefaultCommissionCriteria()
            {
                Environment = AppSettings.Environment
            };

            var model = _accountService.GetDefaultProductCommission(criteria);
            this.AddLoginTokenToCookie(AppSession.SecureLoginToken);

            return View(model);
        }
    }
}