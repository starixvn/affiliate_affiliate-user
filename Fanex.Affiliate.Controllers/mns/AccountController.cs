// ----------------------------------------------------------------------
// <copyright file="AccountController.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/11/11">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.Controllers.Livebet365
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Net;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;
    using System.Web.Mvc;
    using Configurations;
    using Criterias;
    using DTO;
    using Enumerations;
    using Resources;
    using Services.Impl;
    using Services.Interface;
    using StackExchange.Profiling;
    using Utilities;
    using ViewModels;
    using Web;
    using Web.Filter;
    using Web.Pager;

    /// <summary>
    /// All action after login.
    /// </summary>
    [Authorization]
    public partial class AccountController : Fanex.Affiliate.Controllers.AccountController
    {
        #region Const

        #endregion Const

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController" /> class.
        /// </summary>
        /// <param name="accountService">Account Service.</param>
        /// <param name="systemSettingService">System Setting Service.</param>
        /// <param name="paymentService">System payment Service.</param>
        /// <param name="userService">User Service.</param>
        /// <param name="productService">Product Service.</param>
        /// <param name="marketingService">Marketing Service.</param>
        public AccountController(
            IAccountService accountService,
            ISystemSettingService systemSettingService,
            IPaymentService paymentService,
            IUserService userService,
            IProductService productService,
            IMarketingService marketingService)
            : base(
            accountService,
            systemSettingService,
            paymentService,
            userService,
            productService,
            marketingService)
        {
        }
        #endregion Constructor

        /// <summary>
        /// Payment Request.
        /// </summary>
        /// <param name="success">The result of payment.</param>
        /// <returns>Action result.</returns>
        [HttpGet]
        public override ActionResult PaymentRequest(int success = 0)
        {
            var result = new ResultViewModel { Success = true };

            var criteria = new PaymentCriteria
            {
                AffId = CurrentSession.AffId
            };
            PaymentViewModel payment;
            var profiler = MiniProfiler.Current;
            using (profiler.Step("DB Call"))
            {
                payment = PaymentService.GetPaymentRequest(criteria);
            }

            if (success != 1)
            {
                // check pending request
                if (payment.ListPayment.Where(p => p.Status.ToLower() == PaymentStatus.Pending.ToString().ToLower()).ToList().Any())
                {
                    result.Success = false;
                    result.Message = Lang.Msg130;
                }

                // check whether balance is zero
                if (payment.BalanceBroughtForward <= 0 || payment.BalanceBroughtForward < payment.MinimumWithdrawalAmount)
                {
                    result.Success = false;
                    result.Message = Lang.BalanceIsZero;
                }
            }
            else
            {
                result.Success = true;
                result.Message = Lang.PaymentRequestCancel;
            }

            ViewBag.Result = result;

            return View(payment);
        }

        /// <summary>
        /// Handle payout request event.
        /// </summary>
        /// <param name="paymentModel">Input view model.</param>
        /// <returns>Return view.</returns>
        [InternalChecking]
        [HttpPost]
        public override ActionResult PaymentRequest(PaymentViewModel paymentModel)
        {
            var result = new ResultViewModel { Success = false };
            result.Message = "Post: From Specific Affiliate.";

            ViewBag.Result = result;
            return View();
        }
    }
}