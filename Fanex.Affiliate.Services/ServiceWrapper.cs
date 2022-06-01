namespace Fanex.Affiliate.Services
{
    using System;
    using System.Globalization;
    using System.Text;
    using System.Xml.Linq;
    using Criteria.Payment;
    using DataAccess;
    using DTO;
    using Fanex.Affiliate.Web.Helpers;
    using MemberSiteService;

    /// <summary>
    /// Service for getting info from Taiwan side's APIs.
    /// </summary>
    public static class ServiceWrapper
    {
        private static ServiceSoapClient _client;
        private static ILoggerDataAccess _dbLogger;

        private static ServiceSoapClient Client
        {
            get
            {
                return _client ??
                       (_client = new ServiceSoapClient("ServiceSoap"));
            }
        }

        private static ILoggerDataAccess DbLogger
        {
            get { return _dbLogger ?? (_dbLogger = new LoggerDataAccess()); }
        }

        public static ApiCustInfo GetLicenseeCustInfo(string siteName, string licenseeUserName, int userId)
        {
            var custInfo = new ApiCustInfo();

            try
            {
                var xml = Client.GetLicenseeCustInfo(siteName, licenseeUserName);
                WriteLogForCallingApi(siteName, licenseeUserName, xml?.ToString(), userId);

                var isValid = IsValidApiResult(xml);

                if (isValid)
                {
                    custInfo.IbcUserName = xml.Element("IBCusername")?.Value;
                    custInfo.PlayerCurrency = xml.Element("Currency")?.Value;
                }
                else
                {
                    custInfo.ErrorMessage = xml.Element("ReturnMessage")?.Value;
                }
            }
            catch (Exception exception)
            {
                LogHelper.WriteError(exception);
            }

            return custInfo;
        }

        private static void WriteLogForCallingApi(string siteName, string userName, string logContent, int userId)
        {
            var methodName = "GetLicenseeCustInfo";
            var paramInfo = $"siteName={siteName}, userName={userName}";
            var resultLogContent = BuildLogContentByResult(methodName, paramInfo, logContent);
            DbLogger.WritePaymentLog(new PaymentLogCriteria { UserId = userId, LogContent = resultLogContent });
        }

        private static string BuildLogContentByResult(string methodName, string paramInfo, string resultContent)
        {
            var builder = new StringBuilder();

            builder.AppendLine($"Affiliate User - Complete Call Method {methodName} at {DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture)}");
            builder.AppendLine($"Param Info: {paramInfo}");

            builder.AppendLine($"Result Content: {resultContent}");

            return builder.ToString();
        }

        private static bool IsValidApiResult(XElement xmlResult)
        {
            if (xmlResult == null || xmlResult.IsEmpty)
            {
                return false;
            }

            var xmlElement = xmlResult.Element("ReturnCode");
            if (xmlElement == null || Convert.ToInt32(xmlElement.Value) != 0)
            {
                return false;
            }

            return true;
        }
    }
}