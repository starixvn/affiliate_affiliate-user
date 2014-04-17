// --------------------------------------------------------------------------
// <copyright file="BaseController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.05.28">Created</change>
// </history>

namespace Fanex.Affiliate.Controllers
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Drawing.Text;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Web.Mvc;
    using Configurations;
    using DTO;
    using Utilities;
    using Web;

    /// <summary>
    /// Base Controller.
    /// </summary>
    public class BaseController : Controller
    {
        private readonly LanguageManager _langManager;

        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseController" /> class.
        /// </summary>
        public BaseController()
        {
            // Set default language
            _langManager = new LanguageManager();
            _langManager.SetCulture(AppSession.Language);

            // Set default currency
            if (CurrentSession != null)
            {
                FormaterCurrency.SetDefaultCurrency(CurrentSession.Currency);
            }
        }

        protected ICurrentSession CurrentSession
        {
            get { return SessionManager.CurrentSession; }
        }

        /// <summary>
        /// Gets the site domain info.
        /// </summary>
        /// <returns>Site domain information.</returns>
        protected SiteDomainInfo GetSiteDomainInfo()
        {
            var currentSession = CurrentSession;
            if (currentSession != null)
            {
                if (currentSession.SiteDomainInfo != null)
                {
                    return currentSession.SiteDomainInfo;
                }
            }

            var siteDomainInfo = SiteDomainInfo.Parse(this);
            if (currentSession != null)
            {
                currentSession.SiteDomainInfo = siteDomainInfo;
            }

            return siteDomainInfo;
        }

        /// <summary>
        /// Change System Language.
        /// </summary>
        protected void ChangeSystemLanguage()
        {
            _langManager.SetCulture(AppSession.Language);
        }

        /// <summary>
        /// Generate Captcha image function.
        /// </summary>
        /// <param name = "rand">Random number.</param>
        /// <param name = "captcha">Captcha number.</param>
        /// <param name = "noisy">Noisy parameter.</param>
        /// <returns>Return view.</returns>
        protected ActionResult GenerateCaptchaImage(Random rand, int captcha, bool noisy = true)
        {
            FileContentResult img = null; // image stream

            using (var memoryStream = new MemoryStream())
            using (var bitmap = Common.GenerateNoise(45, 20))
            using (var graphics = Graphics.FromImage((Image)bitmap))
            {
                graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                graphics.SmoothingMode = SmoothingMode.AntiAlias;

                // Add noise
                if (noisy)
                {
                    int i;
                    var pen = new Pen(Color.Yellow);
                    for (i = 1; i < 10; i++)
                    {
                        pen.Color = Color.FromArgb(
                        rand.Next(0, 255),
                        rand.Next(0, 255),
                        rand.Next(0, 255));
                        var r = rand.Next(0, 130 / 3);
                        var x = rand.Next(0, 130);
                        var y = rand.Next(0, 30);
                        graphics.DrawEllipse(pen, x - r, y - r, r, r);
                    }
                }

                // Add captcha
                graphics.DrawString(captcha.ToString(), new Font("Tahoma", 13), Brushes.White, 1, 0);

                // Render as JPEG
                bitmap.Save(memoryStream, ImageFormat.Jpeg);
                img = File(memoryStream.GetBuffer(), "image/Jpeg");
            }

            return img;
        }
        
        /// <summary>
        /// Generate email content.
        /// </summary>
        /// <param name="firstName">First name.</param>
        /// <param name="userName">User name.</param>
        /// <returns>Return an object.</returns>
        protected dynamic GenerateEmailContent(string firstName, string userName = null)
        {
            var siteDomainInfo = GetSiteDomainInfo();
            var changePasswordUrl = string.Format("{0}Account/ChangePassword", siteDomainInfo.AffiliateDomain);
            var regex = new Regex(@":\d+");
            changePasswordUrl = regex.Replace(changePasswordUrl, string.Empty);

            return new
            {
                AffiliateName = AppSettings.AffiliateName,
                CurrentMemberSiteDomain = siteDomainInfo.MemberSiteDomain,
                MemberSiteDomainEmail = siteDomainInfo.MemberSiteDomainEmail,
                RootUrl = siteDomainInfo.RootDomain,
                FirstName = firstName,
                Username = userName,
                RecoverPwdURL = string.Format("{0}User/RecoverPassword", siteDomainInfo.AffiliateDomain),
                ChangePwdURL = changePasswordUrl,
                MailContact = AppSettings.MailContact,
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
        }
    }
}