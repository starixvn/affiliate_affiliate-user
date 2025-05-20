namespace Fanex.Affiliate.Services
{
    using System;
    using System.Configuration;
    using System.Globalization;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using Criteria.Payment;
    using DataAccess;
    using DTO;
    using Fanex.Affiliate.Helpers;
    using Fanex.Affiliate.Services.Api;
    using Fanex.Affiliate.Services.Helpers;
    using Refit;

    /// <summary>
    /// Service for getting info from Taiwan side's APIs.
    /// </summary>
    public static class ServiceWrapper
    {
        private static ILoggerDataAccess _dbLogger;

        private static readonly Lazy<ISagaApi> _instance = new Lazy<ISagaApi>(() =>
        {
            try
            {
                var apiUrl = ConfigurationManager.AppSettings["SagaApiUrl"];
                if (string.IsNullOrEmpty(apiUrl))
                {
                    throw new ConfigurationErrorsException("SagaApiUrl is not configured in AppSettings");
                }

                var isUsingProxy = Convert.ToBoolean(ConfigurationManager.AppSettings["IsUseProxyToGetSagaApi"]);
                var httpClientHandler = new HttpClientHandler
                {
                    Proxy = isUsingProxy ? GetConfiguredProxy() : null,
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                    UseCookies = false
                };

                var httpClient = new HttpClient(httpClientHandler)
                {
                    BaseAddress = new Uri(apiUrl)
                };

                return RestService.For<ISagaApi>(httpClient);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to initialize SagaApi client", ex);
            }
        });

        private static ISagaApi SagaApi => _instance.Value;

        private static ILoggerDataAccess DbLogger
        {
            get { return _dbLogger ?? (_dbLogger = new LoggerDataAccess()); }
        }

        private static async Task<XElement> GetLicenseeCustInfo(string siteName, string licenseeUserName)
        {
            var param = new LicenseCustInfoParam
            {
                StrSiteName = siteName,
                StrLicenseeUserName = licenseeUserName
            };

            var rawXml = await SagaApi.GetLicenseeCustInfo(param);

            return ParseXml(rawXml);
        }

        public static async Task<ApiCustInfo> GetLicenseeCustInfo(string siteName, string licenseeUserName, int userId)
        {
            var custInfo = new ApiCustInfo();

            try
            {
                var xml = await GetLicenseeCustInfo(siteName, licenseeUserName);
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
                LogWebHelper.WriteError(exception);
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
        private static IWebProxy GetConfiguredProxy()
        {
            var proxyUrl = ConfigurationManager.AppSettings["ApiProxyUrl"];
            if (string.IsNullOrEmpty(proxyUrl))
            {
                throw new ConfigurationErrorsException("ApiProxyUrl is not configured in AppSettings when proxy is enabled");
            }

            return new WebProxy(new Uri(proxyUrl));
        }

        private static XElement ParseXml(string xmlString)
        {
            try
            {
                return XDocument.Parse(xmlString).Root;
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex);
                return null;
            }
        }
    }
}