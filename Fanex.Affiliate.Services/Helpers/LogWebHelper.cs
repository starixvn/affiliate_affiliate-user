using Fanex.Affiliate.Helpers;
using Fanex.Affiliate.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Fanex.Affiliate.Services.Helpers
{
    public static class LogWebHelper
    {
        public static void WriteError(Exception exception, string additionalMessage = null)
        {
            var content = BuildSessionMessage();
            if (!string.IsNullOrWhiteSpace(additionalMessage))
            {
                content.AppendLine(additionalMessage);
            }

            LogHelper.WriteError(exception, content.ToString());
        }

        private static StringBuilder BuildSessionMessage()
        {
            var builder = new StringBuilder();

            var currenSession = SessionManager.CurrentSession;
            if (currenSession != null)
            {
                builder.AppendLine($"AffId: {currenSession.AffId}")
                    .AppendLine($"AffType: { currenSession.AffType}")
                    .AppendLine($"IsInternalSite: {currenSession.IsInternalSite}")
                    .AppendLine($"Username: {currenSession.Username}")
                    .AppendLine($"CurrencyID: {currenSession.CurrencyID}")
                    .AppendLine($"Currency: {currenSession.Currency}");
            }
            else
            {
                var context = HttpContext.Current;
                builder.AppendLine($"IdentityName: {context?.User?.Identity?.Name}");

                var session = context?.Session;

                if (session != null)
                {
                    builder.AppendLine($"SessionId: {session.SessionID}");

                    var sessionKeyList = new List<string>();
                    foreach (var key in session.Keys)
                    {
                        sessionKeyList.Add(key.ToString());
                    }
                    builder.AppendLine($"SessionKey: {string.Join(",", sessionKeyList)}");
                }

                var cookies = context.Request.Cookies;
                if (cookies != null)
                {
                    foreach (var key in cookies.AllKeys)
                    {
                        var cookie = cookies.Get(key);

                        if (cookie != null)
                        {
                            builder.AppendLine($"CookieKey:{key} && CookieValue={cookie.Value}");
                        }
                    }
                }
            }

            return builder;
        }
    }
}
