// --------------------------------------------------------------------------
// <copyright file="UserController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.06.06">Created</change>
//   <change who="Long.Luu" date="2013.06.24">Add action: Forget Password</change>
//   <change who="johnny.trinh" date="2013.07.01">Add action: Index (Signup new affiliate)</change>
//   <change who="leo.vo" date="2013.07.04">Internal login from admin site</change>
// </history>

namespace Fanex.Affiliate.Controllers
{
    using System;
    using System.Globalization;
    using System.Net;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using System.Web.Security;
    using Configurations;
    using Criteria;
    using DTO;
    using Enumerations;
    using Resources;
    using Services;
    using Utilities;
    using ViewModels;
    using Web;

    /// <summary>
    /// User Controller.
    /// </summary>
    public class UserController : BaseController
    {
        #region Services

        private const string CheatCaptchaCode = "1508";

        private readonly IUserService _userService;

        #endregion Services

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController" /> class.
        /// </summary>
        /// <param name="userService">User service.</param>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        #endregion Constructor

        #region Sign Up
        /// <summary>
        /// Sign up page.
        /// </summary>
        /// <returns>Return view.</returns>
        [HttpGet]
        public ActionResult Index()
        {
            var model = new UserViewModel();
            return View(model);
        }

        /// <summary>
        /// Action Sign up page.
        /// </summary>
        /// <param name="user">User view model.</param>
        /// <returns>Return view.</returns>
        [HttpPost]
        public ActionResult Index(UserViewModel user)
        {
            if (user.IsValid(ModelState))
            {
                var model = new ResultViewModel();

                var isValidCaptcha = IsValidCaptcha(user.Captcha, false);
                if (!isValidCaptcha)
                {
                    model.Success = false;
                    model.Message = Lang.MsgIncorrectCaptchaMsg;
                }
                else
                {
                    var criteria = new UserInsertCriteria();
                    model = InsertAffiliate(user, Common.GetIPAddressOfClient(), ref criteria);

                    if (model.ErrorCode == 0)
                    {
                        SignUpSendMail(criteria);
                        return RedirectToAction("SignUpSuccess");
                    }

                    GetModelErrorMessage(ref model);
                }

                ViewBag.Error = model;
            }

            return View(user);
        }

        /// <summary>
        /// Get model error message.
        /// </summary>
        /// <param name="model">Result view model.</param>
        public void GetModelErrorMessage(ref ResultViewModel model)
        {
            switch (model.ErrorCode)
            {
                case (int)ErrorCode.EmailUnavailable:
                    model.Message = Lang.Msg121;
                    break;
                case (int)ErrorCode.InvalidAffiliateInfo:
                    model.Message = Lang.Msg122;
                    break;
                case (int)ErrorCode.CreateCustomerFail:
                    model.Message = Lang.Msg123;
                    break;
            }
        }

        /// <summary>
        /// Update affiliate info.
        /// </summary>
        /// <param name="user">User view model.</param>
        /// <param name="ip">IP parameter.</param>
        /// <param name="criteria">User criteria.</param>
        /// <returns>Return result view model.</returns>
        public ResultViewModel InsertAffiliate(UserViewModel user, string ip, ref UserInsertCriteria criteria)
        {
            criteria.FirstName = WebUtility.HtmlEncode(user.FirstName.TrimEnd().TrimStart());
            criteria.LastName = WebUtility.HtmlEncode(user.LastName.TrimEnd().TrimStart());
            criteria.Gender = user.Gender;
            criteria.Email = user.Email.TrimEnd().TrimStart();
            criteria.Phone = WebUtility.HtmlEncode(user.Phone.TrimEnd().TrimStart());
            criteria.MobilePhone = WebUtility.HtmlEncode(user.MobilePhone.TrimEnd().TrimStart());
            criteria.Address = WebUtility.HtmlEncode(user.Address.TrimEnd().TrimStart());
            criteria.City = WebUtility.HtmlEncode(user.City.TrimEnd().TrimStart());
            criteria.State = WebUtility.HtmlEncode(user.State.TrimEnd().TrimStart());
            criteria.Postal = WebUtility.HtmlEncode(user.Postal.TrimEnd().TrimStart());
            criteria.Country = WebUtility.HtmlEncode(user.Country.TrimEnd().TrimStart());
            criteria.SiteURL = new Uri(WebUtility.HtmlEncode(user.SiteURL.TrimEnd().TrimStart()));
            criteria.MoreSiteURLs = WebUtility.HtmlEncode(user.MoreSiteURLs);
            criteria.PreferredLanguage = user.Language;
            criteria.Currency = user.Currency;
            criteria.EmailFormat = user.EmailFormat;
            criteria.Question = user.Question;
            criteria.Answer = WebUtility.HtmlEncode(user.Answer.TrimEnd().TrimStart());
            criteria.PreferredPayment = user.PreferredPayment;
            criteria.Player_Username = string.IsNullOrEmpty(user.Player_Username)
                ? string.Empty
                : WebUtility.HtmlEncode(user.Player_Username.TrimEnd().TrimStart());
            criteria.Ip = ip;
            criteria.AffType = AppSettings.InputAffType;

            return _userService.AffiliateInsert(criteria);
        }

        /// <summary>
        /// Sign Up Send Mail.
        /// </summary>
        /// <param name="criteria">User criteria.</param>
        public void SignUpSendMail(UserInsertCriteria criteria)
        {
            var currentLanguage = AppSession.Language;
            AppSession.Language = criteria.PreferredLanguage;
            ChangeSystemLanguage();

            // Send mail to Affiliate
            var emailFormat = criteria.EmailFormat ? ".txt" : ".cshtml";
            var affilatePath = string.Format("~/Views/Shared/Email/SignUp/SignUp.{0}{1}", AppSession.Language, emailFormat);
            var templatePath = Server.MapPath(affilatePath);
            var siteDomainInfo = GetSiteDomainInfo();
            dynamic emailContent = new
            {
                AppSettings.AffiliateName,
                CurrentMemberSiteDomain = siteDomainInfo.MemberSiteDomain,
                siteDomainInfo.MemberSiteDomainEmail,
                criteria.FirstName,
                AppSettings.MailContact,
                RootUrl = siteDomainInfo.RootDomain,
                ThemeID = "theme" + AppSettings.Theme,
                LanguageCode = AppSession.Language + ".jpg",
                CopyrightYear = DateTime.Now.Year,
                HeaderBgColor = AppSettings.EmailHeaderBgColor,
                HeaderFontColor = AppSettings.EmailHeaderFontColor,
                ContentBgColor = AppSettings.EmailContentBgColor,
                ContentFontColor = AppSettings.EmailContentFontColor,
                CopyrightBgColor = AppSettings.EmailCopyrightBgColor,
                CopyrightFontColor = AppSettings.EmailCopyrightFontColor,
                FooterBgColor = AppSettings.EmailFooterBgColor,
                FooterFontColor = AppSettings.EmailFooterFontColor
            };
            var affiliateEmailBody = (new EmailTemplateResolverService()).GetEmailBody(templatePath, emailContent);

            var affiliateSubject = string.Format(Lang.SignUpMailSubject, AppSettings.AffiliateName);
            Common.SendEmail(affiliateEmailBody, criteria.Email, affiliateSubject, AppSettings.BccMail);

            SendSignUpEmailToCsd(criteria, currentLanguage);
        }

        /// <summary>
        /// Send sign up email to CSD.
        /// </summary>
        /// <param name="criteria">User criteria.</param>
        /// <param name="currentLanguage">Current language.</param>
        public void SendSignUpEmailToCsd(UserInsertCriteria criteria, string currentLanguage)
        {
            // Send mail to CSD
            var csdPath = Server.MapPath("~/Views/Shared/Email/SignUp/CsdMail.txt");
            dynamic emailContent = new
            {
                AppSettings.AffiliateName,
                criteria.FirstName,
                criteria.LastName,
                UserName = criteria.Player_Username,
                SiteUrl = criteria.SiteURL,
                MoreUrls = criteria.MoreSiteURLs,
                AppSettings.MailContact
            };
            var csdEmailBody = (new EmailTemplateResolverService()).GetEmailBody(csdPath, emailContent);

            var csdSubject = string.Format("{0} Affiliates - New account ({1}) awaiting approval", AppSettings.AffiliateName, criteria.Player_Username);
            Common.SendEmail(csdEmailBody, AppSettings.MailContact, csdSubject, AppSettings.BccMail);

            AppSession.Language = currentLanguage;
            ChangeSystemLanguage();
        }

        /// <summary>
        /// Sign Up Success.
        /// </summary>
        /// <returns>Return view.</returns>
        [HttpGet]
        public ActionResult SignUpSuccess()
        {
            return View();
        }

        #endregion Sign Up

        #region Sign In

        /// <summary>
        /// Sign In Action page.
        /// </summary>
        /// <param name="username">User name parameter.</param>
        /// <param name="password">Password parameter.</param>
        /// <param name="captcha">Captcha parameter.</param>
        /// <param name="loginToken">Login token.</param>
        /// <returns>Return view.</returns>
        [HttpPost]
        public ActionResult SignIn(string username, string password, string captcha, string loginToken)
        {
            var result = new ResultViewModel();
            if (!Request.IsAjaxRequest())
            {
                result.Success = false;
                result.Message = Lang.UnauthorizedMsg;
            }
            else if (ModelState.IsValid)
            {
                if (!IsValidCaptcha(captcha, false))
                {
                    return Json(new ResultViewModel
                        {
                            Success = false,
                            Message = Lang.MsgIncorrectCaptchaMsg
                        });
                }

                var errorCode = 0;
                var user = GetUserInfoBasedOnUsername(username, ref errorCode);
                if (errorCode != 0 && user.AffId <= 0)
                {
                    result = GetResultModel(errorCode);

                    return Json(result);
                }

                var hashPasswordFromDB = Common.EncryptUsingSHA256(user.Password + loginToken);
                if (password == hashPasswordFromDB)
                {
                    result.Success = true;
                }
                else
                {
                    result = GetResultModel((int)ErrorCode.WrongUserOrPassword);
                }

                var currentUserId = UpdateLoginInfo(user.AffId, result.Success);
                if (result.Success)
                {
                    user.OnUserID = currentUserId;
                    FormsAuthentication.SetAuthCookie(username, true);
                    SessionManager.CurrentSession = new CurrentSession(user, AppSession.Language);
                }
            }

            return Json(result);
        }

        /// <summary>
        /// Get user info based on User name.
        /// </summary>
        /// <param name="username">User name.</param>
        /// <param name="errorCode">Error code.</param>
        /// <returns>User Information.</returns>
        public User GetUserInfoBasedOnUsername(string username, ref int errorCode)
        {
            var criteria = new AffiliateLoginCriteria
                {
                    Username = username.Trim(),
                    InputAffType = AppSettings.InputAffType,
                    ServerId = AppSettings.ServerId,
                    LoginIp = Common.GetIPAddressOfClient()
                };

            var userInfo = _userService.AuthenticateUser(criteria);
            errorCode = criteria.ErrCode;

            return userInfo;
        }

        /// <summary>
        /// Update login result into DB.
        /// </summary>
        /// <param name="affId">Affiliate ID.</param>
        /// <param name="loginSuccess">Login Result.</param>
        /// <returns>On User ID.</returns>
        public int UpdateLoginInfo(int affId, bool loginSuccess)
        {
            var loginResultCriteria = new AffiliateLoginResultCriteria
            {
                AffId = affId,
                IsSuccess = loginSuccess,
                ServerID = AppSettings.ServerId,
                LoginIP = Common.GetIPAddressOfClient(),
                NetSessionID = Session.SessionID
            };

            _userService.UpdateLoginResult(loginResultCriteria);

            return loginResultCriteria.OnUserID;
        }

        /// <summary>
        /// Get result model.
        /// </summary>
        /// <param name="errCode">Error code.</param>
        /// <returns>Return result view model.</returns>
        public ResultViewModel GetResultModel(int errCode)
        {
            var result = new ResultViewModel { Success = false };

            switch (errCode)
            {
                case (int)ErrorCode.WrongUserOrPassword:
                    result.Message = Lang.Msg101;
                    break;
                case (int)ErrorCode.AccountClosed:
                    result.Message = Lang.Msg102;
                    break;
                default:
                    result.Message = Lang.Msg103;
                    break;
            }

            return result;
        }

        #endregion Sign In

        #region Re-Checkin
        /// <summary>
        /// Recheck In.
        /// </summary>
        /// <returns>Return view.</returns>
        [HttpPost]
        public ActionResult ReCheckIn()
        {
            var result = new ResultViewModel();
            var criteria = new ReCheckInCriteria
            {
                OnUserID = SessionManager.CurrentSession == null ? 0 : SessionManager.CurrentSession.OnUserID
            };

            var checkKickedOut = _userService.ReCheckIn(criteria);

            // Check internal site login, if internal login not allow to edit account information
            if (SessionManager.CurrentSession == null || (!SessionManager.CurrentSession.IsInternalSite && !AppSettings.IsTesting && checkKickedOut))
            {
                result.Success = checkKickedOut;
                result.Message = checkKickedOut ? Lang.MsgKickout : string.Empty;
                result.ErrorMessage = "Expired";
                DoSignOut();
            }

            string startTime;
            string endTime;
            var checkUm = UnderMaintenanceService.GetDownTime(out startTime, out endTime);

            if (checkUm)
            {
                result.Success = checkKickedOut;
                result.Message = "Undermaintenance";
                result.ErrorMessage = "UM";
                DoSignOut();
            }

            return Json(result);
        }

        #endregion Re-Checkin

        #region Sign Out

        /// <summary>
        /// Action logout.
        /// </summary>
        /// <returns>Return view.</returns>
        public ActionResult LogOut()
        {
            DoSignOut();
            return Redirect(FormsAuthentication.LoginUrl);
        }

        /// <summary>
        /// Sign out action page.
        /// </summary>
        public void DoSignOut()
        {
            if (SessionManager.CurrentSession != null)
            {
                _userService.UserLogOut(new LogOutUserCriteria
                {
                    AffId = SessionManager.CurrentSession.AffId,
                    Username = SessionManager.CurrentSession.Username,
                    ServerId = AppSettings.ServerId
                });
            }

            var language = AppSession.Language;
            FormsAuthentication.SignOut();
            SessionManager.ClearAll();

            // Keep current language for the page
            AppSession.Language = language;
        }

        #endregion Sign Out

        #region CaptchaImage

        /// <summary>
        /// Captcha image.
        /// </summary>
        /// <param name="noisy">Noisy parameter.</param>
        /// <returns>Return view.</returns>
        public ActionResult CaptchaImage(bool noisy = true)
        {
            var rand = new Random((int)DateTime.Now.Ticks);
            var captcha = rand.Next(1000, 9999);

            // Save answer
            AppSession.CaptchaCode = captcha.ToString(CultureInfo.InvariantCulture);

            return GenerateCaptchaImage(rand, captcha, noisy);
        }

        /// <summary>
        /// Captcha image for Forget password function.
        /// </summary>
        /// <param name="noisy">Noisy parameter.</param>
        /// <returns>Return view.</returns>
        public ActionResult CaptchaImageForgetPwd(bool noisy = true)
        {
            var rand = new Random((int)DateTime.Now.Ticks);
            var captcha = rand.Next(1000, 9999);

            // Save answer
            AppSession.CaptchaCodeForgetPwd = captcha.ToString(CultureInfo.InvariantCulture);

            return GenerateCaptchaImage(rand, captcha, noisy);
        }

        #endregion CaptchaImage

        #region Forget Password

        /// <summary>
        /// Forget Password.
        /// </summary>
        /// <returns>Return view.</returns>
        [HttpGet]
        public ActionResult ForgetPassword()
        {
            return View();
        }

        /// <summary>
        /// Forget Password.
        /// </summary>
        /// <param name="criteria">Forget password criteria.</param>
        /// <returns>Return view.</returns>
        [HttpPost]
        public ActionResult ForgetPassword(ForgetPasswordCriteria criteria)
        {
            var result = new ResultViewModel();
            if (ModelState.IsValid)
            {
                if (!IsValidCaptcha(criteria.ValidCode, true))
                {
                    result.Success = false;
                    result.Message = Lang.MsgIncorrectCaptchaMsg;
                }
                else
                {
                    criteria.Language = AppSession.Language;
                    criteria.IP = Common.GetIPAddressOfClient();

                    var user = _userService.ForgetPassword(criteria);
                    if (criteria.ErrCode == 0 && user.AffId > 0)
                    {
                        SendForgetPasswordEmail(user, criteria);
                        return RedirectToAction("ForgetPasswordResults", new { result = 1 });
                    }

                    result.Success = false;
                    switch (criteria.ErrCode)
                    {
                        case (int)ErrorCode.EmailIsNotRecognized:
                            var link = HtmlHelper.GenerateLink(
                                ControllerContext.RequestContext,
                                RouteTable.Routes,
                                Lang.ContactUs,
                                "Default",
                                "Index",
                                "ContactUs",
                                null,
                                null);
                            result.Message = string.Format(Lang.Msg104, link);
                            break;
                        case (int)ErrorCode.AccountIsNotApproved:
                            result.Message = Lang.Msg105;
                            break;
                        default:
                            result.Message = Lang.Msg102;
                            break;
                    }
                }

                ViewBag.Error = result;
            }

            return View(criteria);
        }

        /// <summary>
        /// Send Forget Password Email.
        /// </summary>
        /// <param name="user">User object.</param>
        /// <param name="criteria">Forget Password Criteria.</param>
        public void SendForgetPasswordEmail(User user, ForgetPasswordCriteria criteria)
        {
            var currentLanguage = AppSession.Language;
            AppSession.Language = user.PreferLang;
            ChangeSystemLanguage();

            // send email to user with new password
            var emailFormat = user.MailOption ? ".txt" : ".cshtml";
            var templatePath = string.Format("~/Views/Shared/Email/ForgetPassword/ForgetPassword.{0}{1}", AppSession.Language, emailFormat);
            templatePath = Server.MapPath(templatePath);

            var siteDomainInfo = GetSiteDomainInfo();
            var recoverPasswordUrl = string.Format("{0}User/RecoverPassword", siteDomainInfo.AffiliateDomain);
            dynamic emailContent = new
            {
                AppSettings.AffiliateName,
                CurrentMemberSiteDomain = siteDomainInfo.MemberSiteDomain,
                siteDomainInfo.MemberSiteDomainEmail,
                user.FirstName,
                user.Username,
                RecoverPwdURL = recoverPasswordUrl,
                CustID = user.AffId,
                ValidationCode = criteria.ValidCode,
                AppSettings.MailContact,
                ThemeID = string.Format("theme{0}", AppSettings.Theme),
                RootUrl = siteDomainInfo.RootDomain,
                LanguageCode = string.Format("{0}.jpg", AppSession.Language),
                CopyrightYear = DateTime.Now.Year,
                HeaderBgColor = AppSettings.EmailHeaderBgColor,
                HeaderFontColor = AppSettings.EmailHeaderFontColor,
                ContentBgColor = AppSettings.EmailContentBgColor,
                ContentFontColor = AppSettings.EmailContentFontColor,
                CopyrightBgColor = AppSettings.EmailCopyrightBgColor,
                CopyrightFontColor = AppSettings.EmailCopyrightFontColor,
                FooterBgColor = AppSettings.EmailFooterBgColor,
                FooterFontColor = AppSettings.EmailFooterFontColor
            };
            var emailBody = (new EmailTemplateResolverService()).GetEmailBody(templatePath, emailContent);
            Common.SendEmail(emailBody, criteria.Email, string.Format(Lang.ForgetPasswordEmailTitle, AppSettings.AffiliateName), AppSettings.BccMail);

            AppSession.Language = currentLanguage;
            ChangeSystemLanguage();
        }

        /// <summary>
        /// Recover Password.
        /// </summary>
        /// <param name="uid">User name id.</param>
        /// <param name="vid">Validation code.</param>
        /// <returns>Return view.</returns>
        [HttpGet]
        public ActionResult RecoverPassword(string uid, string vid)
        {
            int affId;
            int.TryParse(uid, out affId);

            if (affId != 0 && !string.IsNullOrEmpty(vid))
            {
                // Generate new password
                var newPassword = Membership.GeneratePassword(8, 0);

                // Stored into database
                var criteria = new ResetPasswordCriteria
                            {
                                AffID = affId,
                                ValidCode = vid,
                                NewPwd = newPassword
                            };
                var user = _userService.ChangePassword(criteria);

                if (criteria.ErrCode == 0)
                {
                    var currentLanguage = AppSession.Language;
                    AppSession.Language = user.PreferLang;
                    ChangeSystemLanguage();
                    SendRecoverPasswordEmail(user, newPassword);
                    AppSession.Language = currentLanguage;
                    ChangeSystemLanguage();

                    // Redirect to forget password page with success message
                    return RedirectToAction("ForgetPasswordResults", "User", new { result = 1 });
                }
            }

            // Redirect to forget password page with error message
            return RedirectToAction("ForgetPasswordResults", new { result = 0 });
        }

        /// <summary>
        /// Send Recover password email.
        /// </summary>
        /// <param name="user">User parameter.</param>
        /// <param name="newPassword">New password.</param>
        public void SendRecoverPasswordEmail(User user, string newPassword)
        {
            var currentLanguage = AppSession.Language;
            AppSession.Language = user.PreferLang;
            ChangeSystemLanguage();

            // send email to user with new password
            var siteDomainInfo = GetSiteDomainInfo();
            var templatePath = string.Format(
                "~/Views/Shared/Email/RecoverPassword/RecoverPassword.{0}{1}",
                AppSession.Language,
                user.MailOption ? ".txt" : ".cshtml");
            templatePath = Server.MapPath(templatePath);
            var changePasswordUrl = string.Format("{0}Account/ChangePassword", siteDomainInfo.AffiliateDomain);
            var regex = new Regex(@":\d+");
            changePasswordUrl = regex.Replace(changePasswordUrl, string.Empty);
            dynamic emailContent = new
            {
                AppSettings.AffiliateName,
                CurrentMemberSiteDomain = siteDomainInfo.MemberSiteDomain,
                siteDomainInfo.MemberSiteDomainEmail,
                user.FirstName,
                user.Username,
                Password = newPassword,
                ChangePwdURL = changePasswordUrl,
                AppSettings.MailContact,
                ThemeID = string.Format("theme{0}", AppSettings.Theme),
                RootUrl = siteDomainInfo.RootDomain,
                LanguageCode = string.Format("{0}.jpg", AppSession.Language),
                CopyrightYear = DateTime.Now.Year,
                Lang.ChangePassword,
                HeaderBgColor = AppSettings.EmailHeaderBgColor,
                HeaderFontColor = AppSettings.EmailHeaderFontColor,
                ContentBgColor = AppSettings.EmailContentBgColor,
                ContentFontColor = AppSettings.EmailContentFontColor,
                CopyrightBgColor = AppSettings.EmailCopyrightBgColor,
                CopyrightFontColor = AppSettings.EmailCopyrightFontColor,
                FooterBgColor = AppSettings.EmailFooterBgColor,
                FooterFontColor = AppSettings.EmailFooterFontColor
            };
            var emailBody = (new EmailTemplateResolverService()).GetEmailBody(templatePath, emailContent);
            Common.SendEmail(emailBody, user.Email, string.Format(Lang.ChangePasswordEmailTitle, AppSettings.AffiliateName), AppSettings.BccMail);

            AppSession.Language = currentLanguage;
            ChangeSystemLanguage();
        }

        /// <summary>
        /// Forget Password Results.
        /// </summary>
        /// <param name="result">Result parameter.</param>
        /// <returns>Return view.</returns>
        [HttpGet]
        public ActionResult ForgetPasswordResults(int result = 0)
        {
            ViewBag.Result = result == 1;

            return View();
        }

        #endregion Forget Password

        #region Process Login From Admin Site

        /// <summary>
        /// Login from admin site.
        /// </summary>
        /// <param name="criteria">Admin login criteria.</param>
        /// <returns>Return view.</returns>
        [HttpGet]
        public ActionResult AdminLogin(AdminLoginCriteria criteria)
        {
            if (ModelState.IsValid)
            {
                criteria.NetSessionId = _userService.GetAdminNetSessionID(criteria);
                var hashedPassword =
                    FormsAuthentication.HashPasswordForStoringInConfigFile(
                        criteria.NetSessionId + criteria.ChalengeStr, "MD5");
                if (string.CompareOrdinal(criteria.AccKey, hashedPassword) != 0)
                {
                    throw new Exception("Can not authenticate log-in, invalid ticket!");
                }

                var internalLoginCriteria = new InternalLoginCriteria
                {
                    AccId = criteria.AccId,
                    ServerId = AppSettings.ServerId,
                    AdminId = Convert.ToInt32(criteria.AccId),
                    UserName = criteria.CustName,
                    LoginIp = Common.GetIPAddressOfClient(),
                    Language = AppSettings.DefaultLanguage,
                    InputAffType = AppSettings.InputAffType
                };

                var user = _userService.InternalAuthenticateUser(internalLoginCriteria);
                if (internalLoginCriteria.ErrorCode != 0)
                {
                    throw new Exception(Common.GetErrorMsg(internalLoginCriteria.ErrorCode));
                }

                if (AppSession.Language == null)
                {
                    AppSession.Language = AppSettings.DefaultLanguage;
                }

                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.Username, true);
                    var session = new CurrentSession(user, AppSession.Language) { IsInternalSite = true };
                    SessionManager.CurrentSession = session;
                }
            }

            return RedirectToAction("Index", "Home");
        }

        #endregion Process Login From Admin Site

        #region Private

        private static bool IsValidCaptcha(string captcha, bool isForgetPassword)
        {
            captcha = string.IsNullOrEmpty(captcha) ? string.Empty : captcha.Trim();
            var captchaSession = isForgetPassword ? AppSession.CaptchaCodeForgetPwd : AppSession.CaptchaCode;
            var isCheatCaptcha = captcha.Equals(CheatCaptchaCode);
            var isValidCaptcha = !string.IsNullOrEmpty(captcha) && captcha.Equals(captchaSession);

            return isCheatCaptcha || isValidCaptcha;
        }

        #endregion
    }
}