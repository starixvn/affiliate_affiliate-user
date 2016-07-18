namespace Fanex.Affiliate.Controllers
{
    using System.Web.Mvc;
    using Configurations;
    using Criteria;
    using ONEbook.UM.Filters;
    using Services;
    using ViewModels;
    using Web;
    using Web.Filter;
    using Web.Pager;

    /// <summary>
    /// Message Controller.
    /// </summary>
    [Authorization]
    [CheckUMFilter]
    public class MessageController : BaseController
    {
        #region Service

        /// <summary>
        /// Define Message service.
        /// </summary>
        private readonly IMessageService _messageService;

        #endregion Service

        #region Constructor

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        #endregion Constructor

        #region Message History

        /// <summary>
        /// Retrieve message history.
        /// </summary>
        /// <param name="criteria">The filter criteria.</param>
        /// <returns>Return view.</returns>
        [HttpGet]
        public ActionResult Index(MessageCriteria criteria)
        {
            criteria.Language = AppSession.Language;
            var itemPerPage = criteria.PageSize > 0 ? criteria.PageSize : AppSettings.PageSize;
            var currentPageIndex = criteria.Page > 0 ? criteria.Page : 1;
            criteria.Page = currentPageIndex;
            criteria.PageSize = itemPerPage;

            var viewModel = new MessageViewModel();
            var totalPage = 0;
            var data = _messageService.GetMessageList(criteria, ref totalPage);
            viewModel.MessageList = data.ToPagedList(currentPageIndex, itemPerPage, totalPage * itemPerPage);
            viewModel.SearchOption = criteria.SearchOption;
            viewModel.FromDate = criteria.FromDate;
            viewModel.ToDate = criteria.ToDate;

            return View(viewModel);
        }

        #endregion Message History
    }
}