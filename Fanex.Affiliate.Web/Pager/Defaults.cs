namespace Fanex.Affiliate.Web.Pager
{
    /// <summary>
    /// The static Defaults class allows you to set Pager defaults for the entire application.
    /// Set values at application startup.
    /// </summary>
    public static class Defaults
    {
        private static int _maxNrOfPages;

        private static string _displayTemplate;

        private static bool _alwaysAddFirstPageNumber;

        private static string _defaultPageRouteValueKey;

        public static int MaxNrOfPages
        {
            get
            {
                return _maxNrOfPages;
            }
        }

        public static string DisplayTemplate
        {
            get
            {
                return _displayTemplate;
            }
        }

        public static bool AlwaysAddFirstPageNumber
        {
            get
            {
                return _alwaysAddFirstPageNumber;
            }
        }

        public static string DefaultPageRouteValueKey
        {
            get
            {
                return _defaultPageRouteValueKey;
            }
        }

        public static void Reset()
        {
            _maxNrOfPages = DefaultDefaults.MAXNROFPAGES;
            _displayTemplate = DefaultDefaults.DISPLAYTEMPLATE;
            _alwaysAddFirstPageNumber = DefaultDefaults.ALWAYSADDFIRSTPAGENUMBER;
            _defaultPageRouteValueKey = DefaultDefaults.DEFAULTPAGEROUTEVALUEKEY;
        }
    }
}
