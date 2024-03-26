using System.Web.Mvc;
using System.Web.Routing;

namespace Fanex.Affiliate.Helpers
{
    public static class RedirectHelper
    {
        public static RedirectToRouteResult RedirectToRouteResult(string controller, string action)
        {
            return new RedirectToRouteResult(new RouteValueDictionary {
                { "controller", controller },
                { "action", action }
            });
        }

        public static RedirectToRouteResult RedirectToAccessDeniedRouteResult()
        {
            return RedirectToRouteResult("notify", "accessdenied");
        }
    }
}
