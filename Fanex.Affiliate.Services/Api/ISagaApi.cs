using Refit;
using System.Threading.Tasks;

namespace Fanex.Affiliate.Services.Api
{
    public interface ISagaApi
    {
        [Post("/GetLicenseeCustInfo")]
        Task<string> GetLicenseeCustInfo(LicenseCustInfoParam param);
    }

    public class LicenseCustInfoParam
    {
        public string StrSiteName { get; set; }

        public string StrLicenseeUserName { get; set; }
    }
}
