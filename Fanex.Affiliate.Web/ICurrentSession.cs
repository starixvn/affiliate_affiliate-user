namespace Fanex.Affiliate.Web
{
    using DTO;

    /// <summary>
    /// Properties of an authenticated user.</summary>
    public interface ICurrentSession
    {
        int AffId { get; set; }

        string Username { get; set; }

        string Language { get; set; }

        string PreferredLanguage { get; set; }

        string FirstName { get; set; }

        string LastName { get; set; }

        string Email { get; set; }

        int AffLevel { get; set; }

        int AffType { get; set; }

        string Currency { get; set; }

        int CurrencyID { get; set; }

        int OnUserID { get; set; }

        bool IsInternalSite { get; set; }

        SiteDomainInfo SiteDomainInfo { get; set; }

        decimal ExchangeRate { get; set; }
    }
}