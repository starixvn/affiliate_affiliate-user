// --------------------------------------------------------------------------
// <copyright file="AccountController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.05.29">Created</change>
//   <change who="Kaden.Trinh" date="2013.05.30">Implement SignIn action</change>
//   <change who="Kaden.Trinh" date="2013.06.11">Implement action: Overview account</change>
//   <change who="Johny.Trinh" date="2013.07.01">Implement action: My Account</change>
//   <change who="Long.Luu" date="2014.04.17">Refactor</change>
// </history>

namespace Fanex.Affiliate.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text.RegularExpressions;
    using System.Web.Mvc;
    using Configurations;
    using Criteria;
    using DTO;
    using Enumerations;
    using Resources;
    using Services;
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
    [RoleBasedMenu]
    public partial class AccountController : BaseController
    {
        #region Services

        /// <summary>
        /// Define account service.
        /// </summary>
        protected readonly IAccountService AccountService;

        /// <summary>
        /// Define System Setting service.
        /// </summary>
        protected readonly ISystemSettingService SystemSettingService;

        /// <summary>
        /// Define Payment service.
        /// </summary>
        protected readonly IPaymentService PaymentService;

        /// <summary>
        /// Define User service.
        /// </summary>
        protected readonly IUserService UserService;

        /// <summary>
        /// Define Product service.
        /// </summary>
        protected readonly IProductService ProductService;

        /// <summary>
        /// Define Marketing service.
        /// </summary>
        protected readonly IMarketingService MarketingService;

        /// <summary>
        /// Bet List service.
        /// </summary>
        protected readonly IBetListService BetListService;

        #endregion Services

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
        /// <param name="betlistService">Bet List Service.</param>
        public AccountController(
            IAccountService accountService,
            ISystemSettingService systemSettingService,
            IPaymentService paymentService,
            IUserService userService,
            IProductService productService,
            IMarketingService marketingService,
            IBetListService betlistService)
        {
            AccountService = accountService;
            SystemSettingService = systemSettingService;
            PaymentService = paymentService;
            UserService = userService;
            ProductService = productService;
            MarketingService = marketingService;
            BetListService = betlistService;
        }

        #endregion Constructor

        #region Statement History

        /// <summary>
        /// Action get statement history.
        /// </summary>
        /// <param name="pageSize">Input page size.</param>
        /// <param name="page">Input page index.</param>
        /// <returns>Return view.</returns>
        [HttpGet]
        public ActionResult StatementHistory(int pageSize = 0, int page = 1)
        {
            var itemPerPage = pageSize > 0 ? pageSize : AppSettings.PageSize;
            var currentPageIndex = page;
            var totalPage = 0;
            var criteria = new StatementCriteria
            {
                AffId = CurrentSession.AffId,
                PageIndex = currentPageIndex,
                PageSize = itemPerPage,
                TotalPages = totalPage
            };

            var statement = AccountService.GetStatementHistory(criteria, ref totalPage);
            return View(statement.ToPagedList(currentPageIndex, itemPerPage, totalPage * itemPerPage));
        }

        #endregion Statement History

        #region Daily Earnings

        /// <summary>
        /// Daily Earning.
        /// </summary>
        /// <param name="monthDate">Input Month Date.</param>
        /// <returns>Return view.</returns>
        [HttpGet]
        public ActionResult DailyEarnings(string monthDate = null)
        {
            var targetDate = string.IsNullOrEmpty(monthDate) ? DateTime.Now.Date : DateTime.Parse(monthDate);
            var startDate = string.IsNullOrEmpty(monthDate) ? targetDate.AddMonths(-1) : DateTime.Parse(monthDate);
            startDate = new DateTime(startDate.Year, startDate.Month, 1);
            DateTime? nullDateTime = null;
            var criteria = new DailyEarningCriteria
            {
                AffId = CurrentSession.AffId,
                ProductId = 0,
                FromDate = startDate,
                ToDate = targetDate,
                MonthDate = string.IsNullOrEmpty(monthDate) ? nullDateTime : targetDate
            };

            return DailyEarnings(criteria);
        }

        /// <summary>
        /// Daily Earning.
        /// </summary>
        /// <param name="criteria">Daily earning criteria.</param>
        /// <returns>Return view.</returns>
        [HttpPost]
        public ActionResult DailyEarnings(DailyEarningCriteria criteria)
        {
            var productCriteria = new ProductCriteria
            {
                Language = AppSession.Language,
                AffId = CurrentSession.AffId,
                MonthDate = criteria.MonthDate
            };

            var model = new DailyEarningsKioskViewModel();

            criteria.AffId = CurrentSession.AffId;
            if (criteria.FromDate != null)
            {
                model.FromDate = (DateTime)criteria.FromDate;
            }

            if (criteria.ToDate != null)
            {
                model.ToDate = (DateTime)criteria.ToDate;
            }

            model.ListProduct = ProductService.GetProductList(productCriteria).ToList();
            model.ListProduct.ForEach(l => l.OriginalProductName = l.ProductName);
            model.ListProduct.ForEach(i => i.ProductName = ProductsFormatter.Format(i.ProductName));
            model.ProductId = criteria.ProductId;
            model.ListDailyEarning = AccountService.GetDailyEarnings(criteria).ToList();
            model.ShowPlaytechCasinoData = (model.ProductId == 0 && model.ListProduct.Count(l => l.OriginalProductName == "Casino") > 0) 
                || (model.ProductId != 0 && model.ListProduct.Where(i => i.ProductId == model.ProductId).First().OriginalProductName == "Casino");
            FormaterCurrency.SetDefaultCurrency(criteria.AffCurrency);

            return View(model);
        }

        /// <summary>
        /// Daily Earning Detail.
        /// </summary>
        /// <param name="product">Initialize product ID.</param>
        /// <param name="winlostdate">Initialize Win lost date.</param>
        /// <returns>Return view.</returns>
        [HttpGet]
        public ActionResult DailyEarningsDetail(int product, string winlostdate)
        {
            DateTime winlostDate;
            DateTime.TryParse(winlostdate, out winlostDate);

            var criteria = new DailyEarningsDetailCriteria
            {
                AffId = CurrentSession.AffId,
                ProductId = product,
                MonthDate = winlostDate,
            };

            return DailyEarningsDetail(criteria);
        }

        /// <summary>
        /// Daily Earning Detail.
        /// </summary>
        /// <param name="criteria">Daily earning detail criteria.</param>
        /// <returns>Return view.</returns>
        [HttpPost]
        public ActionResult DailyEarningsDetail(DailyEarningsDetailCriteria criteria)
        {
            var productCriteria = new ProductCriteria
            {
                Language = AppSession.Language,
                AffId = CurrentSession.AffId,
                MonthDate = null
            };

            var model = new DailyEarningsKioskDetailViewModel();
            model.ListProduct = ProductService.GetProductList(productCriteria).ToList();
            model.ListProduct.ForEach(i => i.OriginalProductName = i.ProductName);
            model.ListProduct.ForEach(i => i.ProductName = ProductsFormatter.Format(i.ProductName));
            if (criteria.MonthDate != null)
            {
                model.MonthDate = (DateTime)criteria.MonthDate;
            }
            
            criteria.AffId = CurrentSession.AffId;
            model.ProductId = criteria.ProductId;
            model.ListDailyEarningDetail = AccountService.GetDailyEarningsDetail(criteria).ToList();
            model.ShowPlaytechCasinoData = HasShowPlaytechCasino(model);
            FormaterCurrency.SetDefaultCurrency(criteria.AffCurrency);
            ViewBag.AffiliateCurrency = CurrentSession.Currency;

            return View(model);
        }

        /// <summary>
        /// Has Show Play Tech Casino.
        /// </summary>
        /// <param name="model">Model input.</param>
        /// <returns>Show or don't show play tech.</returns>
        public bool HasShowPlaytechCasino(DailyEarningsKioskDetailViewModel model)
        {
            if (model.ProductId == 0 && model.ListProduct.Count(l => l.OriginalProductName == "Casino") > 0)
            {
                return true;
            }

            var firstProduct = model.ListProduct.FirstOrDefault(i => i.ProductId == model.ProductId);

            if (firstProduct == null)
            {
                return false;
            }

            return model.ProductId != 0 && firstProduct.OriginalProductName == "Casino";
        }

        #endregion Daily Earnings

        #region Customer Information Kiosk

        /// <summary>
        /// Action get customer information.
        /// </summary>
        /// <param name="pageSize">Input page size.</param>
        /// <param name="page">Input page index.</param>
        /// <returns>Return view.</returns>
        [HttpGet]
        public ActionResult CustomerInfoKiosk(int pageSize = 0, int page = 1)
        {
            var itemPerPage = pageSize > 0 ? pageSize : AppSettings.PageSize;
            var currentPageIndex = page;
            var username = CurrentSession.Username.Contains(": [")
                ? CurrentSession.Username.Substring(0, CurrentSession.Username.IndexOf(":") - 1)
                : CurrentSession.Username;
            var criteria = new CustomerInfoCriteria
            {
                AffUsername = username,
                Username = null,
                AffType = 0,
                PageIndex = currentPageIndex,
                PageSize = itemPerPage,
                TotalPages = 0
            };

            var customerInfo = AccountService.GetCustomerInfo(criteria);
            return View(customerInfo.ToPagedList(currentPageIndex, itemPerPage, criteria.TotalPages * itemPerPage));
        }

        #endregion Customer Information Kiosk

        #region Account Overview

        /// <summary>
        /// Action get account overview.
        /// </summary>
        /// <returns>Return view.</returns>
        [HttpGet]
        public ActionResult Overview()
        {
            var criteria = new AccountStatisticInfoCriteria
            {
                Month = DateTime.Now.Month,
                Year = DateTime.Now.Year
            };
            return Overview(criteria);
        }

        /// <summary>
        /// Action get account overview.
        /// </summary>
        /// <param name="criteria">Post criteria.</param>
        /// <returns>Return view.</returns>
        [HttpPost]
        public ActionResult Overview(AccountStatisticInfoCriteria criteria)
        {
            // Get statistics account
            criteria.AffId = CurrentSession.AffId;
            var listProduct = new List<Product>();
            var statistics = AccountService.GetStatisticsOverview(criteria, ref listProduct);
            listProduct.ForEach(i => i.ProductName = ProductsFormatter.Format(i.ProductName));

            // Get Current Commission Setting
            var commSettingCriteria = new AccountCommGroupCriteria { AffId = CurrentSession.AffId };
            var model = commSettingCriteria.IsValid() ? AccountService.GetCommissionSetting(commSettingCriteria) : new AccountOverviewViewModel();
            
            model.Month = criteria.Month;
            model.Year = criteria.Year;
            model.Statistics = statistics;
            model.ListProduct = listProduct;
            model.FirstListProduct = listProduct.Count > 0 ? listProduct[0].ProductName : Lang.NA;
            model.TotalqualifedMembers = listProduct.Count > 0 ? listProduct[0].TotalQualifiedMembers.ToString() : Lang.NA;

            return View(model);
        }

        #endregion Account Overview

        #region Payment Request

        /// <summary>
        /// Payment Information.
        /// </summary>
        /// <param name="success">Check whether showing the cancel request success.</param>
        /// <returns>Return view.</returns>
        [HttpGet]
        public virtual ActionResult PaymentRequest(int success = 0)
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
                // Check pending request
                if (payment.ListPayment.Where(p => p.Status.ToLower() == PaymentStatus.Pending.ToString().ToLower()).ToList().Any())
                {
                    result.Success = false;
                    result.Message = Lang.Msg130;
                }

                // Check whether balance is zero
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
        public virtual ActionResult PaymentRequest(PaymentViewModel paymentModel)
        {
            var result = new ResultViewModel { Success = true };

            if (ModelState.IsValid)
            {
                if (paymentModel.Amount > paymentModel.BalanceBroughtForward)
                {
                    result.Success = false;
                    result.Message = Lang.Msg131;
                }

                if (paymentModel.Amount < paymentModel.MinimumWithdrawalAmount)
                {
                    result.Success = false;
                    result.Message = Lang.Msg136;
                }

                if (result.Success)
                {
                    var paymentInfo = PaymentService.GetPaymentRequestPlayerInfo(new PaymentPlayerInfoCriteria
                    {
                        AffId = CurrentSession.AffId
                    });
                    
                    // Call API to check whether member is exists
                    string ibcUsername = string.Empty, playerCurrency = string.Empty, strErrorMsg = string.Empty;
                    if (!IsExistLicenseeCustInfo(paymentInfo.SystemName, paymentInfo.PlayerUsername, ref ibcUsername, ref playerCurrency, ref strErrorMsg))
                    {
                        result.Success = false;
                        result.Message = Lang.Msg142;
                    }
                    else
                    {
                        result = InsertPaymentRequest(paymentInfo.PlayerUsername, playerCurrency, paymentModel.Amount, Common.GetIPAddressOfClient());
                    }
                }
            }
            
            var freshPayment = PaymentService.GetPaymentRequest(new PaymentCriteria
            {
                AffId = CurrentSession.AffId
            });
            
            ViewBag.Result = result;

            return View(freshPayment);
        }

        /// <summary>
        /// Insert payment request and send mail.
        /// </summary>
        /// <param name="playerUsername">Player user name.</param>
        /// <param name="currency">Player currency.</param>
        /// <param name="amount">Request amount.</param>
        /// <param name="ip">IP parameter.</param>
        /// <returns>Return result view model.</returns>
        public ResultViewModel InsertPaymentRequest(string playerUsername, string currency, decimal amount, string ip)
        {
            var result = new ResultViewModel { Success = true };

            var criteria = new PaymentRequestCriteria
            {
                AffId = CurrentSession.AffId,
                Player_UserName = CurrentSession.Username,
                Player_IBCUserName = playerUsername,
                Player_Currency = currency,
                Amount = amount,
                IP = ip,
                Language = AppSession.Language
            };
            
            var paymentRequest = PaymentService.InsertPaymentRequest(criteria);

            if (criteria.ErrCode == 0)
            {
                if (string.IsNullOrEmpty(paymentRequest.PreferredLanguage))
                {
                    paymentRequest.PreferredLanguage = AppSettings.DefaultLanguage;
                }

                SendPaymentRequestEmail(paymentRequest, amount);
                result.Message = Lang.PaymentRequestSuccess;
            }
            else
            {
                result.Success = false;
                result.Message = Common.GetErrorMsg(criteria.ErrCode);
            }

            return result;
        }

        /// <summary>
        /// Send payment request email.
        /// </summary>
        /// <param name="paymentRequest">Payment view model.</param>
        /// <param name="amount">Request amount.</param>
        public void SendPaymentRequestEmail(PaymentViewModel paymentRequest, decimal amount)
        {
            var mapPath = string.Format(
                "~/Views/Shared/Email/PaymentRequest/PaymentRequest.{0}{1}",
                paymentRequest.PreferredLanguage,
                paymentRequest.MailOption ? ".txt" : ".cshtml");
            var templatePath = Server.MapPath(mapPath);
            var currentLanguage = AppSession.Language;
            AppSession.Language = paymentRequest.PreferredLanguage;
            ChangeSystemLanguage();

            var emailTitle = string.Format(
                "{0} Affiliates - {1} - {2}",
                AppSettings.SystemDisplayName,
                paymentRequest.TransID,
                Lang.RequestPaymentEmailTitle);
            var siteDomainInfo = GetSiteDomainInfo();
            dynamic emailContent = new
            {
                AffiliateName = AppSettings.SystemDisplayName,
                CurrentMemberSiteDomain = siteDomainInfo.MemberSiteDomain,
                MemberSiteDomainEmail = siteDomainInfo.MemberSiteDomainEmail,
                FirstName = CurrentSession.FirstName,
                PaymentRequestDate = paymentRequest.TransDate.ToString(),
                PaymentAmount = FormaterCurrency.Format(amount, SessionManager.CurrentSession.Currency, true, true),
                PaymentID = paymentRequest.TransID,
                MailContact = AppSettings.MailContact,
                ThemeID = "theme" + AppSettings.Theme,
                RootUrl = siteDomainInfo.RootDomain,
                LanguageCode = paymentRequest.PreferredLanguage + ".jpg",
                CopyrightYear = DateTime.Now.Year,
                HeaderBgColor = AppSettings.EmailHeaderBGColor,
                HeaderFontColor = AppSettings.EmailHeaderFontColor,
                ContentBgColor = AppSettings.EmailContentBGColor,
                ContentFontColor = AppSettings.EmailContentFontColor,
                CopyrightBgColor = AppSettings.EmailCopyrightBGColor,
                CopyrightFontColor = AppSettings.EmailCopyrightFontColor,
                FooterBgColor = AppSettings.EmailFooterBGColor,
                FooterFontColor = AppSettings.EmailFooterFontColor
            };
            var emailBody = (new EmailTemplateResolverService()).GetEmailBody(templatePath, emailContent);
            Common.SendEmail(emailBody, CurrentSession.Email, emailTitle, AppSettings.BccMail);

            AppSession.Language = currentLanguage;
            ChangeSystemLanguage();
        }

        /// <summary>
        /// Handle payout request cancel event.
        /// </summary>
        /// <param name="paymentModel">Input view model.</param>
        /// <returns>Return view.</returns>
        [InternalChecking]
        [HttpPost]
        public ActionResult CancelPaymentRequest(PaymentViewModel paymentModel)
        {
            var result = new ResultViewModel { Success = true };

            if (ModelState.IsValid)
            {
                var criteria = new PaymentCancelCriteria
                {
                    UserID = CurrentSession.AffId,
                    TransID = Convert.ToInt64(paymentModel.TransID),
                    IP = Common.GetIPAddressOfClient(),
                    Language = AppSession.Language,
                    Status = PaymentStatus.Cancelled.ToString(),
                    TransDate = DateTime.Now
                };
                var paymentCancel = PaymentService.CancelPaymentRequest(criteria);

                if (criteria.ErrCode == 0)
                {
                    result.Message = Lang.PaymentRequestCancel;

                    if (string.IsNullOrEmpty(paymentCancel.PreferredLanguage))
                    {
                        paymentCancel.PreferredLanguage = AppSettings.DefaultLanguage;
                    }

                    SendCancelPaymentEmail(paymentCancel, paymentModel.TransID);

                    return RedirectToAction("PaymentRequest", new { success = 1 });
                }
                else
                {
                    result.Success = false;
                    result.Message = Common.GetErrorMsg(criteria.ErrCode);
                }
            }

            var payment = PaymentService.GetPaymentRequest(new PaymentCriteria
            {
                AffId = CurrentSession.AffId
            });
            ViewBag.Result = result;

            return View("PaymentRequest", payment);
        }

        /// <summary>
        /// Send cancel payment email.
        /// </summary>
        /// <param name="paymentCancel">Payment view model.</param>
        /// <param name="transId">Transaction id.</param>
        public void SendCancelPaymentEmail(PaymentViewModel paymentCancel, long transId)
        {
            var mapPath = string.Format(
                "~/Views/Shared/Email/PaymentCancel/PaymentCancel.{0}{1}",
                paymentCancel.PreferredLanguage,
                paymentCancel.MailOption ? ".txt" : ".cshtml");
            var templatePath = Server.MapPath(mapPath);
            var currentLanguage = AppSession.Language;
            AppSession.Language = paymentCancel.PreferredLanguage;
            ChangeSystemLanguage();

            var emailTitle = string.Format("{0} Affiliates - {1} - {2}", AppSettings.SystemDisplayName, transId, Lang.RequestPaymentEmailTitle);
            var siteDomainInfo = GetSiteDomainInfo();
            dynamic emailContent = new
            {
                AffiliateName = AppSettings.SystemDisplayName,
                CurrentMemberSiteDomain = siteDomainInfo.MemberSiteDomain,
                MemberSiteDomainEmail = siteDomainInfo.MemberSiteDomainEmail,
                FirstName = CurrentSession.FirstName,
                PaymentRequestDate = paymentCancel.TransDate.ToString(),
                PaymentAmount = FormaterCurrency.Format(Math.Abs(paymentCancel.Amount), SessionManager.CurrentSession.Currency, true, true),
                PaymentID = transId,
                MailContact = AppSettings.MailContact,
                ThemeID = "theme" + AppSettings.Theme,
                RootUrl = siteDomainInfo.RootDomain,
                LanguageCode = paymentCancel.PreferredLanguage + ".jpg",
                CopyrightYear = DateTime.Now.Year,
                HeaderBgColor = AppSettings.EmailHeaderBGColor,
                HeaderFontColor = AppSettings.EmailHeaderFontColor,
                ContentBgColor = AppSettings.EmailContentBGColor,
                ContentFontColor = AppSettings.EmailContentFontColor,
                CopyrightBgColor = AppSettings.EmailCopyrightBGColor,
                CopyrightFontColor = AppSettings.EmailCopyrightFontColor,
                FooterBgColor = AppSettings.EmailFooterBGColor,
                FooterFontColor = AppSettings.EmailFooterFontColor
            };
            var emailBody = (new EmailTemplateResolverService()).GetEmailBody(templatePath, emailContent);
            Common.SendEmail(emailBody, CurrentSession.Email, emailTitle, AppSettings.BccMail);

            AppSession.Language = currentLanguage;
            ChangeSystemLanguage();
        }

        /// <summary>
        /// 0: username not existing.
        /// 1: existing in Licensee DB.
        /// 3: existing in both.
        /// </summary>
        /// <param name="siteName">Affiliate name.</param>
        /// <param name="userName">Affiliate username.</param>
        /// <param name="ibcUsername">Affiliate's IBC username.</param>
        /// <param name="playerCurrency">Affiliate currency.</param>
        /// <param name="errMsg">Return error message.</param>
        /// <returns>Boolean: success or not.</returns>
        public bool IsExistLicenseeCustInfo(string siteName, string userName, ref string ibcUsername, ref string playerCurrency, ref string errMsg)
        {
            try
            {
                var xml = ServiceWrapper.GetLicenseeCustInfo(siteName, userName);
                if (xml == null || xml.IsEmpty)
                {
                    return false;
                }

                var xmlElement = xml.Element("ReturnCode");
                if (xmlElement == null || Convert.ToInt32(xmlElement.Value) != 0)
                {
                    return false;
                }

                errMsg = xml.Element("ReturnMessage") != null ? xml.Element("ReturnMessage").Value : string.Empty;
                ibcUsername = xml.Element("IBCusername") != null ? xml.Element("IBCusername").Value : string.Empty;
                playerCurrency = xml.Element("Currency") != null ? xml.Element("Currency").Value : string.Empty;
                
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion Payment Request

        #region My Account

        /// <summary>
        /// My Account page - Update affiliate information.
        /// </summary>
        /// <returns>Return view.</returns>
        [HttpGet]
        public ActionResult MyAccount()
        {
            var criteria = new UserSelectInfoCriteria
            {
                AffID = CurrentSession.AffId
            };

            var user = UserService.AffiliateSelectByID(criteria);
            return View(user);
        }

        /// <summary>
        /// Update profile.
        /// </summary>
        /// <param name="user">Input User view model.</param>
        /// <returns>Return view.</returns>
        [InternalChecking]
        [HttpPost]
        public ActionResult MyAccount(UserViewModel user)
        {
            if (ModelState.IsValid && user.IsValid(ModelState))
            {
                var criteria = new UserUpdateInfoCriteria
                {
                    UserID = CurrentSession.AffId,
                    AffID = CurrentSession.AffId,
                    Gender = user.Gender,
                    Address = WebUtility.HtmlEncode(user.Address),
                    PreferredLanguage = user.Language,
                    PreferredPayment = user.PreferredPayment,
                    EmailFormat = user.EmailFormat,
                    MoreSiteURLs = WebUtility.HtmlEncode(user.MoreSiteURLs),
                    Ip = Common.GetIPAddressOfClient()
                };
                var result = UserService.AffiliateUpdate(criteria);

                if (result.Success)
                {
                    result.Message = Lang.UpdateAccSuccess;
                }

                ViewBag.Result = result;
            }

            return View(user);
        }

        #endregion My Account

        #region Change Password

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <returns>Return view.</returns>
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View(new ChangePasswordCriteria());
        }

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>Return view.</returns>
        [InternalChecking]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordCriteria criteria)
        {
            CheckPasswordFormat(criteria);

            if (ModelState.IsValid)
            {
                var passwordFromDB = AccountService.GetChangePasswordInfo(new ChangePasswordInfoCriteria
                {
                    AffId = CurrentSession.AffId
                });

                passwordFromDB = Common.EncryptUsingSHA256(passwordFromDB + criteria.SecureToken);
                criteria.AffId = CurrentSession.AffId;
                criteria.IP = Common.GetIPAddressOfClient();
                criteria.Language = CurrentSession.Language;
                criteria.IsSuccess = passwordFromDB == criteria.OldPassword;
                AccountService.UpdateChangePasswordResult(criteria);
                if (criteria.ErrCode == 0)
                {
                    ViewBag.Result = new ResultViewModel { Success = true, Message = Lang.PwdChanged };
                }
                else
                {
                    ModelState.AddModelError("OldPassword", Common.GetErrorMsg(criteria.ErrCode));
                }
            }

            return View();
        }

        /// <summary>
        /// Check whether the format of passwords inputted from user are correct.
        /// </summary>
        /// <param name="criteria">Change password criteria.</param>
        public void CheckPasswordFormat(ChangePasswordCriteria criteria)
        {
            criteria.OldPassword = criteria.OldPassword.Trim();
            criteria.NewPassword = criteria.NewPassword.Trim();
            criteria.ConfirmPassword = criteria.ConfirmPassword.Trim();
            if (criteria.NewPassword != criteria.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", Lang.PasswordNotMatched);
            }

            var digitCount = criteria.NewPassword.Count(letter => letter >= '0' && letter <= '9');

            if (criteria.NewPassword.Length < 6 || digitCount < 2 || (criteria.NewPassword.Length - digitCount) < 2)
            {
                ModelState.AddModelError("NewPassword", Lang.PasswordPattern);
            }
        }

        #endregion Change Password

        #region QCPreTest

        [HttpGet]
        public ActionResult QCPreTest()
        {
            return View();
        }

        /// <summary>
        /// Send test email.
        /// </summary>
        /// <param name="email">Input email.</param>
        /// <param name="preferLangs">Prefer language.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        public ActionResult QCPreTest(string email, string preferLangs)
        {
            var storeSessionEmail = CurrentSession.Email;
            CurrentSession.Email = string.IsNullOrEmpty(email) ? storeSessionEmail : email;

            try
            {
                if (preferLangs == "All")
                {
                    foreach (var item in AppSettings.BaseLanguages)
                    {
                        SendAllMails(item.LanguageId, email);
                    }
                }
                else
                {
                    SendAllMails(preferLangs, email);
                }

                ViewBag.Result = "Emails Sent Successfully.";
            }
            catch
            {
                ViewBag.Result = "Emails Sent Failed.";
            }

            CurrentSession.Email = storeSessionEmail;

            return View();
        }

        /// <summary>
        /// Send all emails.
        /// </summary>
        /// <param name="preferredLanguage">Prefer language.</param>
        /// <param name="email">Input email.</param>
        public void SendAllMails(string preferredLanguage, string email)
        {
            SignUpSendMail(preferredLanguage, email);
            PaymentSendMail(preferredLanguage);
            SendForgetPasswordEmail(preferredLanguage, email);
            SendRecoverPasswordEmail(preferredLanguage, email);
        }

        /// <summary>
        /// Payment Send Mail.
        /// </summary>
        /// <param name="preferredLanguage">Prefer language.</param>
        public void PaymentSendMail(string preferredLanguage)
        {
            var paymentModel = new PaymentViewModel
            {
                PreferredLanguage = string.IsNullOrEmpty(preferredLanguage) ? "en-US" : preferredLanguage,
                MailOption = false, // html
                TransDate = DateTime.Now,
                TransID = 220984
            };

            SendPaymentRequestEmail(paymentModel, 229);
            SendCancelPaymentEmail(paymentModel, 229);
        }

        /// <summary>
        /// Sign Up Send Mail.
        /// </summary>
        /// <param name="preferredLanguage">Prefer language.</param>
        /// <param name="email">Input email.</param>
        public void SignUpSendMail(string preferredLanguage, string email)
        {
            var currentLanguage = AppSession.Language;
            AppSession.Language = preferredLanguage;
            ChangeSystemLanguage();

            // Send mail to Affiliate
            var affilatePath = string.Format("~/Views/Shared/Email/SignUp/SignUp.{0}{1}", AppSession.Language, ".cshtml");
            var templatePath = Server.MapPath(affilatePath);
            var siteDomainInfo = GetSiteDomainInfo();
            dynamic emailContent = new
            {
                AffiliateName = AppSettings.SystemDisplayName,
                CurrentMemberSiteDomain = siteDomainInfo.MemberSiteDomain,
                MemberSiteDomainEmail = siteDomainInfo.MemberSiteDomainEmail,
                FirstName = "Mai Ngao Suong",
                MailContact = AppSettings.MailContact,
                RootUrl = siteDomainInfo.RootDomain,
                ThemeID = "theme" + AppSettings.Theme,
                LanguageCode = AppSession.Language + ".jpg",
                CopyrightYear = DateTime.Now.Year,
                HeaderBgColor = AppSettings.EmailHeaderBGColor,
                HeaderFontColor = AppSettings.EmailHeaderFontColor,
                ContentBgColor = AppSettings.EmailContentBGColor,
                ContentFontColor = AppSettings.EmailContentFontColor,
                CopyrightBgColor = AppSettings.EmailCopyrightBGColor,
                CopyrightFontColor = AppSettings.EmailCopyrightFontColor,
                FooterBgColor = AppSettings.EmailFooterBGColor,
                FooterFontColor = AppSettings.EmailFooterFontColor
            };
            var affiliateEmailBody = (new EmailTemplateResolverService()).GetEmailBody(templatePath, emailContent);

            var affiliateSubject = string.Format(Lang.SignUpMailSubject, AppSettings.SystemDisplayName);
            Common.SendEmail(affiliateEmailBody, email, affiliateSubject, AppSettings.BccMail);

            // Send mail to CSD
            SendSignUpEmailToCsd(email);

            AppSession.Language = currentLanguage;
            ChangeSystemLanguage();
        }

        /// <summary>
        /// Send sign up email to CSD.
        /// </summary>
        /// <param name="email">Input email.</param>
        public void SendSignUpEmailToCsd(string email)
        {
            var templatePath = Server.MapPath("~/Views/Shared/Email/SignUp/CsdMail.txt");

            dynamic emailContent = new
                                    {
                                        AffiliateName = AppSettings.SystemDisplayName,
                                        FirstName = "Suong",
                                        LastName = "Mai Ngao",
                                        UserName = "MaiNgaoSuong",
                                        SiteUrl = "www.maingaosuong.com",
                                        MoreUrls = "www.mnsmnl.com",
                                        MailContact = AppSettings.MailContact
                                    };

            var emailTitle = string.Format(Lang.CsdMailSubject, AppSettings.SystemDisplayName, "MaiNgaoSuong");
            var emailBody = (new EmailTemplateResolverService()).GetEmailBody(templatePath, emailContent);
            Common.SendEmail(emailBody, email, emailTitle, AppSettings.BccMail);
        }

        /// <summary>
        /// Send forget password email.
        /// </summary>
        /// <param name="preferredLanguage">Prefer language.</param>
        /// <param name="email">Input email.</param>
        public void SendForgetPasswordEmail(string preferredLanguage, string email)
        {
            var currentLanguage = AppSession.Language;
            AppSession.Language = preferredLanguage;
            ChangeSystemLanguage();

            // send email to user with new password
            var templatePath = string.Format("~/Views/Shared/Email/ForgetPassword/ForgetPassword.{0}{1}", AppSession.Language, ".cshtml");
            templatePath = Server.MapPath(templatePath);

            var siteDomainInfo = GetSiteDomainInfo();
            var recoverPasswordUrl = string.Format("{0}User/RecoverPassword", siteDomainInfo.AffiliateDomain);
            dynamic emailContent = new
            {
                AffiliateName = AppSettings.SystemDisplayName,
                CurrentMemberSiteDomain = siteDomainInfo.MemberSiteDomain,
                MemberSiteDomainEmail = siteDomainInfo.MemberSiteDomainEmail,
                FirstName = "Suong",
                Username = "MaiNgaoSuong",
                RecoverPwdURL = recoverPasswordUrl,
                CustID = "220984",
                ValidationCode = "2209",
                MailContact = AppSettings.MailContact,
                ThemeID = string.Format("theme{0}", AppSettings.Theme),
                RootUrl = siteDomainInfo.RootDomain,
                LanguageCode = string.Format("{0}.jpg", AppSession.Language),
                CopyrightYear = DateTime.Now.Year,
                HeaderBgColor = AppSettings.EmailHeaderBGColor,
                HeaderFontColor = AppSettings.EmailHeaderFontColor,
                ContentBgColor = AppSettings.EmailContentBGColor,
                ContentFontColor = AppSettings.EmailContentFontColor,
                CopyrightBgColor = AppSettings.EmailCopyrightBGColor,
                CopyrightFontColor = AppSettings.EmailCopyrightFontColor,
                FooterBgColor = AppSettings.EmailFooterBGColor,
                FooterFontColor = AppSettings.EmailFooterFontColor
            };
            var emailBody = (new EmailTemplateResolverService()).GetEmailBody(templatePath, emailContent);
            Common.SendEmail(emailBody, email, string.Format(Lang.ForgetPasswordEmailTitle, AppSettings.SystemDisplayName), AppSettings.BccMail);

            AppSession.Language = currentLanguage;
            ChangeSystemLanguage();
        }

        /// <summary>
        /// Send Recover password email.
        /// </summary>
        /// <param name="preferredLanguage">Prefer language.</param>
        /// <param name="email">Input email.</param>
        public void SendRecoverPasswordEmail(string preferredLanguage, string email)
        {
            var currentLanguage = AppSession.Language;
            AppSession.Language = preferredLanguage;
            ChangeSystemLanguage();

            // send email to user with new password
            var siteDomainInfo = GetSiteDomainInfo();
            var templatePath = string.Format("~/Views/Shared/Email/RecoverPassword/RecoverPassword.{0}{1}", AppSession.Language, ".cshtml");
            templatePath = Server.MapPath(templatePath);
            var changePasswordUrl = string.Format("{0}Account/ChangePassword", siteDomainInfo.AffiliateDomain);
            var regex = new Regex(@":\d+");
            changePasswordUrl = regex.Replace(changePasswordUrl, string.Empty);
            dynamic emailContent = new
            {
                AffiliateName = AppSettings.SystemDisplayName,
                CurrentMemberSiteDomain = siteDomainInfo.MemberSiteDomain,
                MemberSiteDomainEmail = siteDomainInfo.MemberSiteDomainEmail,
                FirstName = "Suong",
                Username = "MaiNgaoSuong",
                Password = "Whazzup?",
                ChangePwdURL = changePasswordUrl,
                MailContact = AppSettings.MailContact,
                ThemeID = string.Format("theme{0}", AppSettings.Theme),
                RootUrl = siteDomainInfo.RootDomain,
                LanguageCode = string.Format("{0}.jpg", AppSession.Language),
                CopyrightYear = DateTime.Now.Year,
                ChangePassword = Lang.ChangePassword,
                HeaderBgColor = AppSettings.EmailHeaderBGColor,
                HeaderFontColor = AppSettings.EmailHeaderFontColor,
                ContentBgColor = AppSettings.EmailContentBGColor,
                ContentFontColor = AppSettings.EmailContentFontColor,
                CopyrightBgColor = AppSettings.EmailCopyrightBGColor,
                CopyrightFontColor = AppSettings.EmailCopyrightFontColor,
                FooterBgColor = AppSettings.EmailFooterBGColor,
                FooterFontColor = AppSettings.EmailFooterFontColor
            };

            var emailBody = (new EmailTemplateResolverService()).GetEmailBody(templatePath, emailContent);

            Common.SendEmail(emailBody, email, string.Format(Lang.ChangePasswordEmailTitle, AppSettings.SystemDisplayName), AppSettings.BccMail);

            AppSession.Language = currentLanguage;
            ChangeSystemLanguage();
        }

        #endregion
    }
}