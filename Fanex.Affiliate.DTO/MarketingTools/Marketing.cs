namespace Fanex.Affiliate.DTO
{
    /// <summary>
    /// Marketing data object.
    /// </summary>
    public class Marketing
    {
        public int BannerId { get; set; }

        public string Content { get; set; }

        public string Category { get; set; }

        public string Language { get; set; }

        public string LanguageCode { get; set; }

        public string ImageType { get; set; }

        public string Size { get; set; }
    }
}