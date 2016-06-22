namespace Fanex.Affiliate.Web
{
    using System.Web;

    /// <summary>
    /// Session Manager.
    /// </summary>
    public static class SessionManager
    {
        private const string _AffiliateCurrentSessionKey = "Fanex.Affiliate.Web.CurrentSession";

        public static ICurrentSession CurrentSession
        {
            get
            {
                return (ICurrentSession)HttpContext.Current.Session[_AffiliateCurrentSessionKey];
            }

            set
            {
                HttpContext.Current.Session[_AffiliateCurrentSessionKey] = value;
            }
        }

        /// <summary>
        /// Clear All Session.
        /// </summary>
        public static void ClearAll()
        {
            if (HttpContext.Current != null && HttpContext.Current.Session != null)
            {
                HttpContext.Current.Session.Clear();
                HttpContext.Current.Response.Cookies.Clear();
            }
        }
    }
}