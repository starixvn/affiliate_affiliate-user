namespace Fanex.Affiliate.Services
{
    using System;
    using Fanex.Logging;

    /// <summary>
    /// Service base.
    /// </summary>
    public class ServiceBase
    {
        protected void HandleException(Exception exception)
        {
            LogService.Error(exception.Message, exception);
        }
    }
}