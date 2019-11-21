using Fanex.Affiliate.Configurations;
using Fanex.Logging;
using Fanex.Logging.Sentry.RavenClient;
using SharpRaven;
using System.Collections.Generic;

namespace Fanex.Affiliate.Site
{
    public static class LoggingConfig
    {
        public static void RegisterSentry()
        {
            var env = AppSettings.Environment;
            var backEndDsn = AppSettings.SentryBackEndDsn;
            var frontEndDsn = AppSettings.SentryFrontEndDsn;
            var category = AppSettings.LoggingCategory;
            var isRegisterSentry = AppSettings.IsRegisterSentry;

            if (isRegisterSentry && !string.IsNullOrEmpty(backEndDsn) && !string.IsNullOrEmpty(frontEndDsn))
            {
                LogManager.SetDefaultLogCategory(category)
                    .Use(new List<ILogEngine>
                    {
                        new SentryLogging(new SentryEngineOptions
                        {
                            Dsn = new Dsn(backEndDsn),
                            FrontEndDsn = new Dsn(frontEndDsn),
                            Environment = env
                        })
                    });
            }
        }
    }
}