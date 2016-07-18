namespace Fanex.Affiliate.DTO
{
    /// <summary>
    /// Country data object.
    /// </summary>
    public class Country
    {
        public string CountryId { get; set; }

        public string CountryName { get; set; }

        public string Description { get; set; }

        public string Icon { get; set; }

        public short DisplayOrder { get; set; }

        public bool IsEnabled { get; set; }
    }
}