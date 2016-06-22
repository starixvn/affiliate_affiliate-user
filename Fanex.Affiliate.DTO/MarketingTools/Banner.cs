namespace Fanex.Affiliate.DTO
{
    using System.Text;

    /// <summary>
    /// Banner class.
    /// </summary>
    public class Banner
    {
        public Banner()
        {
            BannerStringBuilder = new StringBuilder();
        }

        public int BannerID { get; set; }

        public string Content { get; set; }

        public short ImageType { get; set; }

        public string Size { get; set; }

        public int Version { get; set; }

        public StringBuilder BannerStringBuilder { get; set; }
    }
}
