// ----------------------------------------------------------------------
// <copyright file="StyleImagePathBundle.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/08/21">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.Site.App_Start
{   
    using System.Web.Optimization;

    /// <summary>
    /// Style Image Path Bundle.
    /// </summary>
    internal class StyleImagePathBundle : Bundle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StyleImagePathBundle" /> class.
        /// </summary>
        /// <param name="virtualPath">Virtual Path.</param>
        public StyleImagePathBundle(string virtualPath)
            : base(virtualPath)
        {
            Transforms.Add(new StyleRelativePathTransform());
            Transforms.Add(new CssMinify());
        }
    }
}