namespace Fanex.Affiliate.DTO
{
    /// <summary>
    /// Internal Login Info.
    /// </summary>
    public class InternalLoginInfo
    {
        public string AdminUserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int AffID { get; set; }

        public string PreferLang { get; set; }

        public string Prefix { get; set; }

        public bool IsAllowedPersonalizedPage { get; set; }

        public int AffLevel { get; set; }

        public int AffType { get; set; }

        public string Currency { get; set; }

        public int CurrencyID { get; set; }
    }
}
