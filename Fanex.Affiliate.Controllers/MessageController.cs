// --------------------------------------------------------------------------
// <copyright file="MessageController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Brian.Dinh" date="2013.07.03">Created</change>
// </history>

namespace Fanex.Affiliate.Controllers
{
    using System.Web.Mvc;
    using Configurations;
    using Criteria;
    using Services.Interface;
    using ViewModels;
    using Web;
    using Web.Filter;
    using Web.Pager;

    /// <summary>
    /// Message Controller.
    /// </summary>
    [Authorization]
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

            var model = new MessageViewModel();
            var totalPage = 0;
            var data = _messageService.GetMessageList(criteria, ref totalPage);
            model.MessageList = data.ToPagedList(currentPageIndex, itemPerPage, totalPage * itemPerPage);

            model.SearchOption = criteria.SearchOption;
            model.FromDate = criteria.FromDate;
            model.ToDate = criteria.ToDate;

            return View(model);
        }

        #endregion Message History
    }
}