namespace Fanex.Affiliate.Controllers
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Drawing.Text;
    using System.IO;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;
    using DTO;
    using Utilities;
    using Web;

    public class BaseController : Controller
    {
        protected readonly string FormTokenName = "SecureLoginToken";
        private readonly LanguageManager _langManager;

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

            // Generate a session token
            var secureToken = Guid.NewGuid().ToString("N");
            AppSession.SecureLoginToken = secureToken;
            ViewBag.SecureToken = secureToken;
        }

        protected ICurrentSession CurrentSession
        {
            get { return SessionManager.CurrentSession; }
        }

        public void AddLoginTokenToCookie(string loginToken)
        {
            var responseCookie = new HttpCookie(FormTokenName)
            {
                HttpOnly = true,
                Value = loginToken
            };

            if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
            {
                responseCookie.Secure = true;
            }

            Response.Cookies.Set(responseCookie);
        }

        public void ClearLoginToken()
        {
            AppSession.SecureLoginToken = string.Empty;

            if (Request.Cookies[FormTokenName] != null)
            {
                var httpCookie = new HttpCookie(FormTokenName);
                httpCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(httpCookie);
            }
        }

        public bool CheckLoginToken(string loginToken)
        {
            return !string.IsNullOrWhiteSpace(loginToken)
                && AppSession.SecureLoginToken == loginToken
                && Request.Cookies[FormTokenName] != null
                && loginToken == Request.Cookies[FormTokenName].Value;
        }

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

        protected void ChangeSystemLanguage()
        {
            _langManager.SetCulture(AppSession.Language);
        }

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
    }
}