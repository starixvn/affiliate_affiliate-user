namespace Fanex.Affiliate.DTO
{
    using System;

    /// <summary>
    /// User data object.
    /// </summary>
    public class User
    {
        public int AffId { get; set; }

        public string Username { get; set; }
        
        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PreferLang { get; set; }

        public bool IsAllowedPersonalizedPage { get; set; }

        public bool HasSecurityQuestion { get; set; }

        public bool MailOption { get; set; }

        public int AffLevel { get; set; }

        public int AffType { get; set; }

        public string Currency { get; set; }

        public int CurrencyID { get; set; }

        public string Gender { get; set; }

        public string Phone { get; set; }

        public string MobilePhone { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string Postcode { get; set; }

        public bool EmailFormat { get; set; }

        public Uri SiteURL { get; set; }

        public string MoreSiteURLs { get; set; }

        public bool PreferredPayment { get; set; }

        public string Player_Username { get; set; }

        public string PlayerCurrency { get; set; }

        public string Question { get; set; }

        public string Answer { get; set; }

        public string Ip { get; set; }

        public string Browser { get; set; }

        public string Language { get; set; }

        public int OnUserID { get; set; }
    }
}