namespace Fanex.Affiliate.DTO
{
    /// <summary>
    /// Banner Type.
    /// </summary>
    public class BannerType
    {
        public short TypeId { get; set; }

        public string Value { get; set; }

        public short ParentId { get; set; }

        public int HasSubCategory { get; set; }
    }
}
