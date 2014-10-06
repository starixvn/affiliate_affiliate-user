// ----------------------------------------------------------------------
// <copyright file="Banner.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2014/03/26">Created</change>
//   <change who="" date=""></change>
// </history>

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
