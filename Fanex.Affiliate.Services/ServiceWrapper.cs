namespace Fanex.Affiliate.Services
{
    using System;
    using System.Globalization;
    using System.Text;
    using System.Xml.Linq;
    using Criteria.Payment;
    using DTO;
    using Logging;
    using MemberSiteService;

    /// <summary>
    /// Service for getting info from Taiwan side's APIs.
    /// </summary>
    public static class ServiceWrapper
    {
        private static ServiceSoapClient _client;
        private static IDbLogger _dbLogger;

        private static ServiceSoapClient Client
        {
            get
            {
                return _client ??
                       (_client = new ServiceSoapClient("ServiceSoap"));
            }
        }

        private static IDbLogger DbLogger
        {
            get { return _dbLogger ?? (_dbLogger = new DbLogger()); }
        }

        /// <summary>
        /// Get Register List.
        /// </summary>
        /// <param name="siteName">Site name.</param>
        /// <param name="targetDate">Target date.</param>
        /// <returns>Xml element.</returns>
        public static XElement GetRegList(string siteName, DateTime targetDate)
        {
            return Client.GetRegList(siteName, targetDate);
        }

        /// <summary>
        /// Update B's tag of a link.
        /// </summary>
        /// <param name="siteName">Site name.</param>
        /// <param name="licenseeUserName">Licensee username.</param>
        /// <param name="newBtag">New B's tag.</param>
        /// <returns>Xml element.</returns>
        public static XElement UpdateBtage(string siteName, string licenseeUserName, string newBtag)
        {
            return Client.UpdateBtage(siteName, licenseeUserName, newBtag);
        }

        /// <summary>
        /// Get Licensee Customer Info.
        /// </summary>
        /// <param name="siteName">Site name.</param>
        /// <param name="licenseeUserName">Licensee username.</param>
        /// <returns>Xml element.</returns>
        public static XElement GetLicenseeCustInfo(string siteName, string licenseeUserName)
        {
            return Client.GetLicenseeCustInfo(siteName, licenseeUserName);
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
                    custInfo.ErrorMessage = xml.Element("ReturnMessage") != null ? xml.Element("ReturnMessage").Value : string.Empty;
                    custInfo.IbcUserName = xml.Element("IBCusername") != null ? xml.Element("IBCusername").Value : string.Empty;
                    custInfo.PlayerCurrency = xml.Element("Currency") != null ? xml.Element("Currency").Value : string.Empty;
                }
            }
            catch (Exception exception)
            {
                LogService.Error(exception.Message, exception);
            }

            return custInfo;
        }

        /// <summary>
        /// Transfer request.
        /// </summary>
        /// <param name="siteName">Site name.</param>
        /// <param name="licenseeUserName">Licensee username.</param>
        /// <param name="amount">Amount transfer.</param>
        /// <param name="dummyAccount">Account to transfer.</param>
        /// <returns>Xml element.</returns>
        public static XElement Transfer(string siteName, string licenseeUserName, double amount, int dummyAccount)
        {
            return Client.Transfer(siteName, licenseeUserName, amount, dummyAccount);
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