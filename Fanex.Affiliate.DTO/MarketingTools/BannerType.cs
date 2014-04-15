// -----------------------------------------------------------------------
// <copyright file="BannerType.cs" company="Nexcel Solutions Vietnam">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Muller.Nguyen" date="2013.06.28">Created</change>
// </history>

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
