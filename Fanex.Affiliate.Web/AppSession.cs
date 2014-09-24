// --------------------------------------------------------------------------
// <copyright file="AppSession.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Kaden.Trinh" date="2013.06.17">Created</change>
// </history>

namespace Fanex.Affiliate.Web
{
    using System;
    using System.Web;
    using Configurations;

    /// <summary>
    /// App Session.
    /// </summary>
    public static class AppSession
    {
        #region Const
        private const string _AFFILIATESESSIONCAPTCHA = "AffiliateCaptcha";
        
        private const string _AFFILIATEFORGETPWDSESSIONCAPTCHA = "AffiliateForgetPwdCaptcha";

        private const string _AFFILIATECONTACTUSSESSIONCAPTCHA = "AffiliateContactUsCaptcha";

        private const string _LANGUAGE = "AffiliateLanguage";

        private const string _SECURELOGINTOKEN = "SecureLoginToken";
        #endregion Const

        public static string Language
        {
            get
            {
                return HttpContext.Current != null && !string.IsNullOrEmpty(Convert.ToString(HttpContext.Current.Session[_LANGUAGE]))
                    ? Convert.ToString(HttpContext.Current.Session[_LANGUAGE])
                    : AppSettings.DefaultLanguage;
            }

            set
            {
                HttpContext.Current.Session[_LANGUAGE] = value;
            }
        }

        public static string CaptchaCode
        {
            get
            {
                return HttpContext.Current != null
                    ? Convert.ToString(HttpContext.Current.Session[_AFFILIATESESSIONCAPTCHA])
                    : string.Empty;
            }

            set
            {
                HttpContext.Current.Session[_AFFILIATESESSIONCAPTCHA] = value;
            }
        }

        public static string CaptchaCodeForgetPwd
        {
            get
            {
                return HttpContext.Current != null
                    ? Convert.ToString(HttpContext.Current.Session[_AFFILIATEFORGETPWDSESSIONCAPTCHA])
                    : string.Empty;
            }

            set
            {
                HttpContext.Current.Session[_AFFILIATEFORGETPWDSESSIONCAPTCHA] = value;
            }
        }

        public static string CaptchaContactUs
        {
            get
            {
                return HttpContext.Current != null
                    ? Convert.ToString(HttpContext.Current.Session[_AFFILIATECONTACTUSSESSIONCAPTCHA])
                    : string.Empty;
            }

            set
            {
                HttpContext.Current.Session[_AFFILIATECONTACTUSSESSIONCAPTCHA] = value;
            }
        }

        public static string SecureLoginToken
        {
            get
            {
                return HttpContext.Current != null
                    ? Convert.ToString(HttpContext.Current.Session[_SECURELOGINTOKEN])
                    : string.Empty;
            }

            set
            {
                HttpContext.Current.Session[_SECURELOGINTOKEN] = value;
            }
        }
    }
}