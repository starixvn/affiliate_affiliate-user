namespace Fanex.Affiliate.DTO
{
    public class LicenseeCustInfoCheckingResult
    {
        public string IbcUserName { get; set; } = string.Empty;

        public string PlayerCurrency { get; set; } = string.Empty;

        public string ErrorMessage { get; set; }

        public bool IsSuccess => string.IsNullOrEmpty(ErrorMessage);
    }
}