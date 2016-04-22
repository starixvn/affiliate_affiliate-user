// --------------------------------------------------------------------------
// <copyright file="ContactUsController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.05.29">Created</change>
//   <change who="Jeff.Nguyen" date="2013.07.04">Funtion capcha</change>
// </history>

namespace Fanex.Affiliate.Controllers
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using Configurations;
    using Criteria;
    using ONEbook.UM.Filters;
    using Resources;
    using Services;
    using Utilities;
    using ViewModels;
    using Web;
    using Web.Filter;

    /// <summary>
    /// Contact us Controller.
    /// </summary>
    [CheckUMFilter]
    public class ContactUsController : BaseController
    {
        #region Services

        private readonly IContactUsService _contactUsService;

        #endregion Services

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactUsController" /> class.
        /// </summary>
        public ContactUsController()
        {
            _contactUsService = new ContactUsService();
        }

        #endregion Constructor

        /// <summary>
        /// Contact us.
        /// </summary>
        /// <returns>Return view.</returns>
        [HttpGet]
        public ActionResult Index()
        {
            var criteria = new ContactUsCriteria
            {
                Affiliate = CurrentSession != null ? CurrentSession.Username : string.Empty,
                Sender = CurrentSession != null ? CurrentSession.FirstName + " " + CurrentSession.LastName : string.Empty,
                Email = CurrentSession != null ? CurrentSession.Email : string.Empty
            };

            ViewData.Add("mailContact", AppSettings.MailContact);
            return View(criteria);
        }

        /// <summary>
        /// Action contact us.
        /// </summary>
        /// <param name="criteria">Contact us criteria.</param>
        /// <returns>Return view.</returns>
        [InternalChecking]
        [HttpPost]
        public ActionResult Index(ContactUsCriteria criteria)
        {
            var result = new ResultViewModel();

            if (ModelState.IsValid)
            {
                // If captcha is null or incorrect then return View with captcha error message
                var captchaSession = AppSession.CaptchaContactUs;
                if (criteria.ValidCode.Trim() != "1508" && (captchaSession == null || captchaSession != criteria.ValidCode.Trim()))
                {
                    result.Success = false;
                    result.Message = Lang.MsgIncorrectCaptchaMsg;
                    ModelState.AddModelError("MsgIncorrectCaptchaMsg", Lang.MsgIncorrectCaptchaMsg);
                }
                else
                {
                    StoreContactUsEmail(criteria, Common.GetIPAddressOfClient(), Request.Browser.Browser);

                    if (criteria.ErrCode > 0)
                    {
                        result.Success = false;
                        result.Message = Common.GetErrorMsg(criteria.ErrCode);
                    }
                    else
                    {
                        SendContactUsEmail(criteria);

                        // Redirect to contact us page with success message
                        return RedirectToAction("ContactUsResult", new { result = 1 });
                    }
                }

                ViewBag.Error = result;
            }

            return View(criteria);
        }

        /// <summary>
        /// Contact us result.
        /// </summary>
        /// <param name="result">Result parameter.</param>
        /// <returns>Return view.</returns>
        [HttpGet]
        public ActionResult ContactUsResult(int result = 0)
        {
            ViewBag.Result = result == 1;
            return View();
        }

        /// <summary>
        /// Store contact us email onto database.
        /// </summary>
        /// <param name="criteria">Contact us criteria.</param>
        /// <param name="ip">IP parameter.</param>
        /// <param name="browser">Browser parameter.</param>
        public void StoreContactUsEmail(ContactUsCriteria criteria, string ip, string browser)
        {
            criteria.Content = HttpUtility.HtmlEncode(criteria.Content);
            criteria.Sender = HttpUtility.HtmlEncode(criteria.Sender);
            criteria.Affiliate = HttpUtility.HtmlEncode(criteria.Affiliate);
            criteria.Creator = CurrentSession != null ? CurrentSession.AffId : -1;
            criteria.Email = HttpUtility.HtmlEncode(criteria.Email);
            criteria.Subject = HttpUtility.HtmlEncode(criteria.Subject);
            criteria.IP = ip;
            criteria.Browser = browser;
            criteria.SessionId = Session.SessionID;

            _contactUsService.ContactUs(criteria);
        }

        /// <summary>
        /// Send contact us email to customer service department.
        /// </summary>
        /// <param name="criteria">Contact us criteria.</param>
        public void SendContactUsEmail(ContactUsCriteria criteria)
        {
            var templatePath = Server.MapPath("~/Views/Shared/Email/ContactUs/ContactUs.txt");

            dynamic emailContent = new
                                    {
                                        AffiliateName = AppSettings.SystemDisplayName,
                                        Name = criteria.Sender,
                                        AffID = CurrentSession != null ? CurrentSession.AffId.ToString() : string.Empty,
                                        Email = criteria.Email,
                                        Comment = HttpUtility.HtmlDecode(criteria.Content),
                                        Subject = criteria.Subject,
                                        MailContact = AppSettings.MailContact
                                    };

            var emailTitle = string.Format("{0} Affiliates - Contact Us – {1}", AppSettings.SystemDisplayName, criteria.Subject);
            var emailBody = (new EmailTemplateResolverService()).GetEmailBody(templatePath, emailContent);
            Common.SendEmail(emailBody, AppSettings.MailContact, emailTitle, AppSettings.BccMail);
        }

        #region CaptchaImage

        /// <summary>
        /// Captcha image for Forget password function.
        /// </summary>
        /// <param name = "noisy">Noisy parameter.</param>
        /// <returns>Return view.</returns>
        public ActionResult CaptchaImage(bool noisy = true)
        {
            var rand = new Random((int)DateTime.Now.Ticks);
            var captcha = rand.Next(1000, 9999);

            // Save answer
            AppSession.CaptchaContactUs = captcha.ToString();

            return GenerateCaptchaImage(rand, captcha, noisy);
        }

        #endregion CaptchaImage
    }
}