namespace Fanex.Affiliate.Controllers
{
    using System.Web.Mvc;
    using Configurations;
    using Criteria;
    using ONEbook.UM.AspNetMVC.Filters;
    using Services;
    using Web;

    public class HomeController : BaseController
    {
        #region Services

        /// <summary>
        /// Define account service.
        /// </summary>
        private readonly IAccountService _accountService;

        #endregion Services

        #region Constructor

        public HomeController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        #endregion Constructor
        [HttpGet]
        [CheckUMFilter]
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